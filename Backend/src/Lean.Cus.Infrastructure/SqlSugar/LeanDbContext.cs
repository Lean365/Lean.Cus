//===================================================
// 项目名称：Lean.Cus.Infrastructure.SqlSugar
// 文件名称：LeanDbContext
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：数据库上下文
//===================================================

using System;
using System.Linq;
using System.Reflection;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using Newtonsoft.Json;
using Lean.Cus.Domain.Entities;
using Lean.Cus.Domain.Interfaces;
using Lean.Cus.Domain.Configurations;

namespace Lean.Cus.Infrastructure.SqlSugar;

/// <summary>
/// 数据库上下文
/// </summary>
public class LeanDbContext
{
    private readonly ISqlSugarClient _db;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db">数据库客户端</param>
    /// <param name="configuration">配置</param>
    public LeanDbContext(ISqlSugarClient db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    /// <summary>
    /// 初始化数据库
    /// </summary>
    public void InitDatabase()
    {
        // 获取所有实体类型
        var domainAssembly = Assembly.Load("Lean.Cus.Domain");
        var entityTypes = domainAssembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ILeanBaseEntity<>)))
            .ToList();

        // 创建数据库
        _db.DbMaintenance.CreateDatabase();

        // 创建表
        foreach (var entityType in entityTypes)
        {
            _db.CodeFirst.InitTables(entityType);
        }

        // 初始化数据
        var dbSeed = new LeanDbSeed(_db);
        dbSeed.Initialize();
    }

    /// <summary>
    /// 配置数据库
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置</param>
    public static void Configure(IServiceCollection services, IConfiguration configuration)
    {
        var dbConfig = configuration.GetSection("Database").Get<LeanDatabaseConfig>();
        if (dbConfig == null)
        {
            throw new ArgumentNullException(nameof(dbConfig), "Database configuration is missing.");
        }

        services.AddScoped<ISqlSugarClient>(s =>
        {
            var connectionString = dbConfig.ConnectionString;
            if (dbConfig.DbType == DbType.SqlServer && dbConfig.TrustServerCertificate)
            {
                connectionString += connectionString.EndsWith(";") ? "TrustServerCertificate=true" : ";TrustServerCertificate=true";
            }

            var db = new SqlSugarClient(new ConnectionConfig()
            {
                ConnectionString = connectionString,
                DbType = dbConfig.DbType,
                IsAutoCloseConnection = dbConfig.IsAutoCloseConnection,
                InitKeyType = InitKeyType.Attribute,
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    EntityService = (property, column) =>
                    {
                        if (property.Name.Length > 1 && property.Name[0] == '_')
                        {
                            column.DbColumnName = property.Name.Substring(1);
                        }
                    }
                }
            });

            // 配置 SQL 日志
            if (dbConfig.LogConfig.EnableExecutionLog)
            {
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    try
                    {
                        if (string.IsNullOrEmpty(sql)) return;

                        // 判断是否是变更操作
                        var upperSql = sql.ToUpper().TrimStart();
                        if (!upperSql.StartsWith("INSERT") && !upperSql.StartsWith("UPDATE") && !upperSql.StartsWith("DELETE"))
                        {
                            // 只输出SQL到控制台
                            Console.WriteLine(sql);
                            return;
                        }

                        // 获取表名
                        var tableName = GetTableNameFromSql(sql);

                        // 检查是否需要记录此表的日志
                        if (dbConfig.LogConfig.IncludeTables.Any() && !dbConfig.LogConfig.IncludeTables.Contains(tableName))
                        {
                            return;
                        }
                        if (dbConfig.LogConfig.ExcludeTables.Contains(tableName))
                        {
                            return;
                        }

                        if (!dbConfig.LogConfig.EnableDiffLog)
                        {
                            return;
                        }

                        // 创建SQL差异日志实体
                        var sqlDiffLog = new Lean.Cus.Domain.Entities.Monitor.LeanSqlDiffLog
                        {
                            DatabaseName = db.CurrentConnectionConfig.ConnectionString.Split(';')[0].Split('=')[1],
                            TableName = tableName,
                            OperationType = GetOperationTypeFromSql(sql),
                            OldData = string.Empty,
                            NewData = dbConfig.LogConfig.LogParameterValues && pars?.Any() == true 
                                ? JsonConvert.SerializeObject(pars.ToDictionary(p => p.ParameterName, p => p.Value)) 
                                : string.Empty,
                            SqlStatement = sql,
                            OperatorId = 0,
                            OperatorName = "System",
                            IpAddress = ""
                        };

                        // 如果需要记录旧数据
                        if (dbConfig.LogConfig.LogOldData && (upperSql.StartsWith("UPDATE") || upperSql.StartsWith("DELETE")))
                        {
                            try
                            {
                                var oldData = db.Queryable<dynamic>().AS(tableName).Where(sql).ToList();
                                sqlDiffLog.OldData = JsonConvert.SerializeObject(oldData);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"获取旧数据失败：{ex.Message}");
                            }
                        }

                        // 异步插入日志
                        Task.Run(async () =>
                        {
                            try
                            {
                                await db.Insertable(sqlDiffLog).ExecuteCommandAsync();
                            }
                            catch (Exception ex)
                            {
                                if (dbConfig.LogConfig.LogErrors)
                                {
                                    Console.WriteLine($"记录数据变更日志失败：{ex.Message}");
                                }
                            }
                        });
                    }
                    catch (Exception ex)
                    {
                        if (dbConfig.LogConfig.LogErrors)
                        {
                            Console.WriteLine($"处理数据变更日志失败：{ex.Message}");
                        }
                    }
                };

                // 记录SQL执行时间
                if (dbConfig.LogConfig.LogExecutionTime)
                {
                    var startTime = DateTime.Now;
                    db.Aop.OnLogExecuted = (sql, args) =>
                    {
                        var endTime = DateTime.Now;
                        var executionTime = endTime - startTime;
                        Console.WriteLine($"SQL执行时间：{executionTime.TotalMilliseconds}ms");
                        startTime = DateTime.Now; // 重置开始时间，为下一次执行准备
                    };
                }
            }

            // 配置全局过滤器
            var domainAssembly = Assembly.Load("Lean.Cus.Domain");
            var entityTypes = domainAssembly.GetTypes()
                .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(ILeanBaseEntity<>)))
                .ToList();

            foreach (var entityType in entityTypes)
            {
                ConfigureGlobalFilter(db, entityType);
            }

            return db;
        });

        // 注册数据库上下文
        services.AddScoped<LeanDbContext>();
    }

    /// <summary>
    /// 配置全局过滤器
    /// </summary>
    /// <param name="db">数据库客户端</param>
    /// <param name="entityType">实体类型</param>
    private static void ConfigureGlobalFilter(ISqlSugarClient db, Type entityType)
    {
        // 软删除过滤器
        if (typeof(ILeanBaseEntity<>).IsAssignableFrom(entityType))
        {
            var method = typeof(LeanDbContext).GetMethod(nameof(CreateFilter), BindingFlags.NonPublic | BindingFlags.Static);
            if (method != null)
            {
                var genericMethod = method.MakeGenericMethod(entityType);
                genericMethod.Invoke(null, new object[] { db });
            }
        }
    }

    /// <summary>
    /// 创建过滤器
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="db">数据库客户端</param>
    private static void CreateFilter<T>(ISqlSugarClient db) where T : LeanBaseEntity<long>
    {
        Expression<Func<T, bool>> expression = it => it.IsDeleted == 0;
        db.QueryFilter.AddTableFilter(expression);
    }

    /// <summary>
    /// 从SQL语句中获取表名
    /// </summary>
    private static string GetTableNameFromSql(string sql)
    {
        if (string.IsNullOrEmpty(sql)) return string.Empty;

        sql = sql.ToUpper();
        string tableName = string.Empty;

        if (sql.Contains("INSERT INTO"))
        {
            tableName = sql.Split(new[] { "INSERT INTO" }, StringSplitOptions.None)[1].Split(' ')[1];
        }
        else if (sql.Contains("UPDATE"))
        {
            tableName = sql.Split(new[] { "UPDATE" }, StringSplitOptions.None)[1].Split(' ')[1];
        }
        else if (sql.Contains("DELETE FROM"))
        {
            tableName = sql.Split(new[] { "DELETE FROM" }, StringSplitOptions.None)[1].Split(' ')[1];
        }

        return tableName.Trim('[', ']', '`', '"');
    }

    /// <summary>
    /// 从SQL语句中获取操作类型
    /// </summary>
    private static string GetOperationTypeFromSql(string sql)
    {
        if (string.IsNullOrEmpty(sql)) return string.Empty;

        sql = sql.ToUpper();

        if (sql.StartsWith("INSERT"))
            return "Insert";
        if (sql.StartsWith("UPDATE"))
            return "Update";
        if (sql.StartsWith("DELETE"))
            return "Delete";

        return "Unknown";
    }
} 
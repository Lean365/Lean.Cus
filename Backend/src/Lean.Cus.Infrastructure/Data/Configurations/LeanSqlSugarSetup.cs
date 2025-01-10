using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SqlSugar;
using System.Reflection;
using Lean.Cus.Domain.Entities;
using Lean.Cus.Domain.Attributes;
using Lean.Cus.Infrastructure.Data.Logging;

namespace Lean.Cus.Infrastructure.Data.Configurations;

/// <summary>
/// SqlSugar配置
/// </summary>
public static class LeanSqlSugarSetup
{
    /// <summary>
    /// 添加SqlSugar服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="configuration">配置</param>
    public static void AddLeanSqlSugar(this IServiceCollection services, IConfiguration configuration)
    {
        // 获取数据库配置
        var dbConfig = configuration.GetSection("Database").Get<LeanDatabaseConfig>();
        if (dbConfig == null)
        {
            throw new ArgumentNullException(nameof(dbConfig), "数据库配置不能为空");
        }

        // 配置SqlSugar
        services.AddScoped<ISqlSugarClient>(sp =>
        {
            var logProvider = new LeanSqlSugarAopProvider();
            logProvider.LogInfo("开始配置SqlSugar...");
            logProvider.LogInfo($"数据库连接字符串: {dbConfig.Conn}");
            logProvider.LogInfo($"数据库类型: {dbConfig.DbType}");

            var db = new SqlSugarClient(new ConnectionConfig()
            {
                DbType = dbConfig.DbType,
                ConnectionString = dbConfig.Conn,
                IsAutoCloseConnection = dbConfig.IsAutoCloseConnection,
                InitKeyType = InitKeyType.Attribute,
                ConfigureExternalServices = new ConfigureExternalServices
                {
                    EntityService = (property, column) =>
                    {
                        // 驼峰转下划线
                        if (column.IsPrimarykey && property.Name.ToLower() == "id")
                        {
                            column.DbColumnName = "id";
                        }
                        else
                        {
                            column.DbColumnName = UtilMethods.ToUnderLine(property.Name);
                        }
                    }
                }
            });

            // 配置Aop
            logProvider.LogInfo("正在配置AOP...");
            logProvider.SetAopProvider(db);

            try
            {
                logProvider.LogInfo("开始初始化数据库...");

                // 获取所有实体类型
                var assemblies = new[]
                {
                    typeof(LeanBaseEntity).Assembly,                    // Domain
                    Assembly.Load("Lean.Cus.Generator"),               // Generator
                    Assembly.Load("Lean.Cus.Workflow")                 // Workflow
                };

                var entityTypes = assemblies
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(t => t.IsClass 
                        && !t.IsAbstract 
                        && t.IsPublic
                        && t.BaseType == typeof(LeanBaseEntity))
                    .ToArray();

                logProvider.LogInfo($"找到 {entityTypes.Length} 个实体类型");

                // 记录初始化开始
                logProvider.LogTableInitialization(entityTypes);

                // 创建数据库
                var database = dbConfig.Conn.Split(';')
                    .FirstOrDefault(x => x.StartsWith("Database=", StringComparison.OrdinalIgnoreCase))?
                    .Split('=').LastOrDefault();
                if (!string.IsNullOrEmpty(database))
                {
                    logProvider.LogInfo($"正在创建数据库: {database}");
                    db.DbMaintenance.CreateDatabase();
                    logProvider.LogDatabaseCreation(database);
                }

                // 配置CodeFirst
                logProvider.LogInfo("开始CodeFirst配置...");
                db.CodeFirst
                    .SetStringDefaultLength(200)  // 设置字符串类型字段默认长度
                    .BackupTable()               // 备份表
                    .InitTables(entityTypes);    // 初始化所有实体表

                // 输出所有表结构
                logProvider.LogInfo("正在获取表结构信息...");
                var allTables = db.DbMaintenance.GetTableInfoList();
                foreach (var entityType in entityTypes)
                {
                    var tableName = UtilMethods.ToUnderLine(entityType.Name);
                    var tableInfo = allTables.FirstOrDefault(it => 
                        it.Name.Equals(tableName, StringComparison.OrdinalIgnoreCase) || 
                        it.Name.Equals(entityType.Name, StringComparison.OrdinalIgnoreCase));
                    
                    if (tableInfo != null)
                    {
                        var columns = db.DbMaintenance.GetColumnInfosByTableName(tableInfo.Name);
                        logProvider.LogTableInfo(tableInfo, columns);
                    }
                    else
                    {
                        logProvider.LogInfo($"警告: 未找到表 {tableName} 的结构信息");
                    }
                }

                logProvider.LogInfo("数据库初始化完成");
            }
            catch (Exception ex)
            {
                logProvider.LogError(ex, "初始化数据库失败");
                throw;
            }

            return db;
        });
    }
}

/// <summary>
/// 数据库配置
/// </summary>
public class LeanDatabaseConfig
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    public string Conn { get; set; } = string.Empty;

    /// <summary>
    /// 数据库类型
    /// </summary>
    public SqlSugar.DbType DbType { get; set; }

    /// <summary>
    /// 多租户标识
    /// </summary>
    public string ConfigId { get; set; } = "0";

    /// <summary>
    /// 是否自动关闭连接
    /// </summary>
    public bool IsAutoCloseConnection { get; set; } = true;
} 
using System;
using System.Text;
using NLog;
using SqlSugar;

namespace Lean.Cus.Infrastructure.Data.Logging;

/// <summary>
/// SqlSugar日志提供程序
/// </summary>
public class LeanSqlSugarAopProvider
{
    private readonly ILogger _logger;

    public LeanSqlSugarAopProvider()
    {
        _logger = LogManager.GetLogger("SqlSugar.CodeFirst");
    }

    /// <summary>
    /// 设置Aop
    /// </summary>
    /// <param name="db">数据库实例</param>
    public void SetAopProvider(ISqlSugarClient db)
    {
        // SQL执行前
        db.Aop.OnLogExecuting = (sql, parameters) =>
        {
            _logger.Debug($"SQL语句: {sql}");
            if (parameters != null && parameters.Any())
            {
                var sb = new StringBuilder();
                foreach (var param in parameters)
                {
                    if (sb.Length > 0) sb.Append(", ");
                    sb.Append($"{param.ParameterName}={param.Value}");
                }
                _logger.Debug($"参数: {sb}");
            }
        };

        // SQL执行后
        db.Aop.OnLogExecuted = (sql, parameters) =>
        {
            _logger.Debug($"SQL执行耗时: {db.Ado.SqlExecutionTime}ms");
        };

        // SQL出错
        db.Aop.OnError = (exp) =>
        {
            _logger.Error(exp, $"SQL执行出错: {exp.Message}");
        };

        // 差异日志
        db.Aop.OnDiffLogEvent = log => 
        {
            _logger.Info($"数据变更: {log.Sql}");
            if (log.Parameters != null && log.Parameters.Any())
            {
                var sb = new StringBuilder();
                foreach (var param in log.Parameters)
                {
                    if (sb.Length > 0) sb.Append(", ");
                    sb.Append(param);
                }
                _logger.Info($"变更参数: {sb}");
            }
        };
    }

    /// <summary>
    /// 记录信息日志
    /// </summary>
    public void LogInfo(string message)
    {
        _logger.Info(message);
    }

    /// <summary>
    /// 记录错误日志
    /// </summary>
    public void LogError(Exception ex, string message)
    {
        _logger.Error(ex, message);
    }

    /// <summary>
    /// 记录表初始化信息
    /// </summary>
    public void LogTableInitialization(Type[] entityTypes)
    {
        _logger.Info($"开始初始化表结构，共有 {entityTypes.Length} 个实体类型:");
        foreach (var type in entityTypes)
        {
            _logger.Info($"- {type.Name}");
        }
    }

    /// <summary>
    /// 记录数据库创建信息
    /// </summary>
    public void LogDatabaseCreation(string database)
    {
        _logger.Info($"数据库 {database} 创建完成");
    }

    /// <summary>
    /// 记录表结构信息
    /// </summary>
    public void LogTableInfo(SqlSugar.DbTableInfo tableInfo, List<SqlSugar.DbColumnInfo> columns)
    {
        _logger.Info($"表 {tableInfo.Name} 结构信息:");
        foreach (var column in columns)
        {
            _logger.Info($"  - {column.DbColumnName} ({column.DataType}) {(column.IsPrimarykey ? "主键" : "")} {(column.IsNullable ? "可空" : "非空")}");
        }
    }
} 
using System;
using System.Text;
using System.Text.RegularExpressions;
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
            var maskedSql = MaskSensitiveInfo(sql);
            WriteColoredLog(ConsoleColor.Gray, "DEBUG", $"SQL语句: {maskedSql}");
            if (parameters != null && parameters.Any())
            {
                var sb = new StringBuilder();
                foreach (var param in parameters)
                {
                    if (sb.Length > 0) sb.Append(", ");
                    var maskedValue = MaskSensitiveInfo(param.Value?.ToString());
                    sb.Append($"{param.ParameterName}={maskedValue}");
                }
                WriteColoredLog(ConsoleColor.Gray, "DEBUG", $"参数: {sb}");
            }
        };

        // SQL执行后
        db.Aop.OnLogExecuted = (sql, parameters) =>
        {
            WriteColoredLog(ConsoleColor.Gray, "DEBUG", $"SQL执行耗时: {db.Ado.SqlExecutionTime}ms");
        };

        // SQL出错
        db.Aop.OnError = (exp) =>
        {
            var maskedMessage = MaskSensitiveInfo(exp.Message);
            WriteColoredLog(ConsoleColor.Red, "ERROR", $"SQL执行出错: {maskedMessage}");
        };

        // 差异日志
        db.Aop.OnDiffLogEvent = log => 
        {
            var maskedSql = MaskSensitiveInfo(log.Sql);
            WriteColoredLog(ConsoleColor.Green, "INFO", $"数据变更: {maskedSql}");
            if (log.Parameters != null && log.Parameters.Any())
            {
                var sb = new StringBuilder();
                foreach (var param in log.Parameters)
                {
                    if (sb.Length > 0) sb.Append(", ");
                    var maskedValue = MaskSensitiveInfo(param?.ToString());
                    sb.Append(maskedValue);
                }
                WriteColoredLog(ConsoleColor.Green, "INFO", $"变更参数: {sb}");
            }
        };
    }

    /// <summary>
    /// 记录信息日志
    /// </summary>
    public void LogInfo(string message)
    {
        var maskedMessage = MaskSensitiveInfo(message);
        WriteColoredLog(ConsoleColor.Green, "INFO", maskedMessage);
    }

    /// <summary>
    /// 记录错误日志
    /// </summary>
    public void LogError(Exception ex, string message)
    {
        var maskedMessage = MaskSensitiveInfo(message);
        WriteColoredLog(ConsoleColor.Red, "ERROR", maskedMessage);
    }

    /// <summary>
    /// 记录表初始化信息
    /// </summary>
    public void LogTableInitialization(Type[] entityTypes)
    {
        WriteColoredLog(ConsoleColor.Cyan, "INFO", $"开始初始化表结构，共有 {entityTypes.Length} 个实体类型:");
        foreach (var type in entityTypes)
        {
            WriteColoredLog(ConsoleColor.White, "INFO", $"- {type.Name}");
        }
    }

    /// <summary>
    /// 记录数据库创建信息
    /// </summary>
    public void LogDatabaseCreation(string database)
    {
        var maskedDatabase = MaskSensitiveInfo(database);
        WriteColoredLog(ConsoleColor.Green, "INFO", $"数据库 {maskedDatabase} 创建完成");
    }

    /// <summary>
    /// 记录表结构信息
    /// </summary>
    public void LogTableInfo(SqlSugar.DbTableInfo tableInfo, List<SqlSugar.DbColumnInfo> columns)
    {
        WriteColoredLog(ConsoleColor.Cyan, "INFO", $"表 {tableInfo.Name} 结构信息:");
        foreach (var column in columns)
        {
            WriteColoredLog(ConsoleColor.White, "INFO", $"  - {column.DbColumnName} ({column.DataType}) {(column.IsPrimarykey ? "主键" : "")} {(column.IsNullable ? "可空" : "非空")}");
        }
    }

    /// <summary>
    /// 写入彩色日志
    /// </summary>
    private void WriteColoredLog(ConsoleColor color, string level, string message)
    {
        // 根据不同的日志级别和消息内容,NLog配置文件会自动处理颜色
        switch (level.ToUpper())
        {
            case "ERROR":
                _logger.Error(message);
                break;
            case "WARN":
                _logger.Warn(message);
                break;
            case "INFO":
                _logger.Info(message);
                break;
            case "DEBUG":
                _logger.Debug(message);
                break;
            default:
                _logger.Info(message);
                break;
        }
    }

    /// <summary>
    /// 脱敏敏感信息
    /// </summary>
    private string MaskSensitiveInfo(string input)
    {
        if (string.IsNullOrEmpty(input))
            return input;

        var patterns = new Dictionary<string, string>
        {
            { @"Server=([^;]+)", "Server=XXX" },
            { @"Database=([^;]+)", "Database=XXX" },
            { @"User ID=([^;]+)", "User ID=XXX" },
            { @"Uid=([^;]+)", "Uid=XXX" },
            { @"Password=([^;]+)", "Password=XXX" },
            { @"Pwd=([^;]+)", "Pwd=XXX" },
            { @"Initial Catalog=([^;]+)", "Initial Catalog=XXX" },
            { @"Data Source=([^;]+)", "Data Source=XXX" },
            { @"Integrated Security=([^;]+)", "Integrated Security=XXX" },
            { @"User=([^;]+)", "User=XXX" }
        };

        var result = input;
        foreach (var pattern in patterns)
        {
            result = Regex.Replace(result, pattern.Key, pattern.Value, RegexOptions.IgnoreCase);
        }

        return result;
    }
} 
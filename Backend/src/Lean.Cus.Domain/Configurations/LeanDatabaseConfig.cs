using SqlSugar;
using System.Text.Json.Serialization;

namespace Lean.Cus.Domain.Configurations;

/// <summary>
/// 数据库配置
/// </summary>
public class LeanDatabaseConfig
{
    /// <summary>
    /// 连接字符串
    /// </summary>
    public string ConnectionString { get; set; } = string.Empty;

    /// <summary>
    /// 数据库类型
    /// </summary>
    public DbType DbType { get; set; } = DbType.SqlServer;

    /// <summary>
    /// 是否信任服务器证书（仅适用于SQL Server）
    /// </summary>
    public bool TrustServerCertificate { get; set; } = false;

    /// <summary>
    /// 是否自动关闭连接
    /// </summary>
    public bool IsAutoCloseConnection { get; set; } = true;

    /// <summary>
    /// 命令超时时间（秒）
    /// </summary>
    public int CommandTimeout { get; set; } = 30;

    /// <summary>
    /// 连接超时时间（秒）
    /// </summary>
    public int ConnectionTimeout { get; set; } = 15;

    /// <summary>
    /// 日志配置
    /// </summary>
    public LeanDatabaseLogConfig LogConfig { get; set; } = new();
}

/// <summary>
/// 数据库日志配置
/// </summary>
public class LeanDatabaseLogConfig
{
    /// <summary>
    /// 是否启用SQL执行日志
    /// </summary>
    public bool EnableExecutionLog { get; set; } = true;

    /// <summary>
    /// 是否启用SQL差异日志
    /// </summary>
    public bool EnableDiffLog { get; set; } = true;

    /// <summary>
    /// 是否记录参数值
    /// </summary>
    public bool LogParameterValues { get; set; } = true;

    /// <summary>
    /// 是否记录执行时间
    /// </summary>
    public bool LogExecutionTime { get; set; } = true;

    /// <summary>
    /// 是否记录错误日志
    /// </summary>
    public bool LogErrors { get; set; } = true;

    /// <summary>
    /// 需要记录的表名列表（为空则记录所有表）
    /// </summary>
    public List<string> IncludeTables { get; set; } = new();

    /// <summary>
    /// 排除记录的表名列表
    /// </summary>
    public List<string> ExcludeTables { get; set; } = new();

    /// <summary>
    /// 是否记录旧数据（更新和删除时）
    /// </summary>
    public bool LogOldData { get; set; } = true;

    /// <summary>
    /// 是否记录新数据（插入和更新时）
    /// </summary>
    public bool LogNewData { get; set; } = true;
} 
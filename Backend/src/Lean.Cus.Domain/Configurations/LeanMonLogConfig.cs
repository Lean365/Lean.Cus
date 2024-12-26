//===================================================
// 项目名称：Lean.Cus.Domain.Configurations
// 文件名称：LeanMonLogConfig
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：监控日志配置
//===================================================

namespace Lean.Cus.Domain.Configurations;

/// <summary>
/// 监控日志配置
/// </summary>
public class LeanMonLogConfig
{
    /// <summary>
    /// 系统日志配置
    /// </summary>
    public LogTypeConfig SystemLog { get; set; } = new();

    /// <summary>
    /// 登录日志配置
    /// </summary>
    public LogTypeConfig LoginLog { get; set; } = new();

    /// <summary>
    /// 审计日志配置
    /// </summary>
    public LogTypeConfig AuditLog { get; set; } = new();

    /// <summary>
    /// SQL差异日志配置
    /// </summary>
    public LogTypeConfig SqlDiffLog { get; set; } = new();
}

/// <summary>
/// 日志类型配置
/// </summary>
public class LogTypeConfig
{
    /// <summary>
    /// 是否启用
    /// </summary>
    public bool Enabled { get; set; } = true;

    /// <summary>
    /// 保留天数
    /// </summary>
    public int RetentionDays { get; set; } = 30;
} 
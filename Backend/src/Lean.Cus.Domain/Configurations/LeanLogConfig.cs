//===================================================
// 项目名称：Lean.Cus.Domain.Configurations
// 文件名称：LeanLogConfig
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：日志配置类
//===================================================

namespace Lean.Cus.Domain.Configurations;

/// <summary>
/// 日志配置类
/// </summary>
public class LeanLogConfig
{
    /// <summary>
    /// 日志级别（Trace/Debug/Info/Warn/Error/Fatal）
    /// </summary>
    public string Level { get; set; } = "Info";

    /// <summary>
    /// 日志存储路径
    /// </summary>
    public string LogPath { get; set; } = "Logs";

    /// <summary>
    /// 是否记录请求日志
    /// </summary>
    public bool LogRequest { get; set; } = true;

    /// <summary>
    /// 是否记录响应日志
    /// </summary>
    public bool LogResponse { get; set; } = true;

    /// <summary>
    /// 是否记录SQL日志
    /// </summary>
    public bool LogSql { get; set; } = true;

    /// <summary>
    /// 是否记录异常日志
    /// </summary>
    public bool LogException { get; set; } = true;
} 
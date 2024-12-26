//===================================================
// 项目名称：Lean.Cus.Application.Dtos.Monitor
// 文件名称：LeanAuditLogDto
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：审核日志DTO
//===================================================

using Lean.Cus.Common.Models;

namespace Lean.Cus.Application.Dtos.Monitor;

/// <summary>
/// 审核日志基础DTO
/// </summary>
public class LeanAuditLogBaseDto
{
    /// <summary>
    /// 日志ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// 客户端IP
    /// </summary>
    public string ClientIp { get; set; } = string.Empty;

    /// <summary>
    /// 命令
    /// </summary>
    public string Command { get; set; } = string.Empty;

    /// <summary>
    /// 数据库编号
    /// </summary>
    public int Database { get; set; }

    /// <summary>
    /// 键
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 值
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// 响应
    /// </summary>
    public string? Response { get; set; }

    /// <summary>
    /// 操作用户
    /// </summary>
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 操作状态（0成功 1失败）
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// 审核日志查询DTO
/// </summary>
public class LeanAuditLogQueryDto : LeanPage
{
    /// <summary>
    /// 操作用户
    /// </summary>
    public string? Username { get; set; }

    /// <summary>
    /// 命令
    /// </summary>
    public string? Command { get; set; }

    /// <summary>
    /// 操作状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
} 
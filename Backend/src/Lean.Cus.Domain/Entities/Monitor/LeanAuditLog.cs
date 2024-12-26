//===================================================
// 项目名称：Lean.Cus.Domain.Entities.Monitor
// 文件名称：LeanAuditLog
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：审核日志实体
//===================================================

using SqlSugar;

namespace Lean.Cus.Domain.Entities.Monitor;

/// <summary>
/// 审核日志实体
/// </summary>
[SugarTable("Lean_audit_log", "审核日志表")]
public class LeanAuditLog : LeanLongEntity
{
    /// <summary>
    /// 时间戳
    /// </summary>
    [SugarColumn(ColumnDescription = "时间戳", IsNullable = false)]
    public DateTime Timestamp { get; set; } = DateTime.Now;

    /// <summary>
    /// 客户端IP
    /// </summary>
    [SugarColumn(ColumnDescription = "客户端IP", IsNullable = false, Length = 50)]
    public string ClientIp { get; set; } = string.Empty;

    /// <summary>
    /// 命令
    /// </summary>
    [SugarColumn(ColumnDescription = "命令", IsNullable = false, Length = 100)]
    public string Command { get; set; } = string.Empty;

    /// <summary>
    /// 数据库编号
    /// </summary>
    [SugarColumn(ColumnDescription = "数据库编号", IsNullable = false)]
    public int Database { get; set; }

    /// <summary>
    /// 键
    /// </summary>
    [SugarColumn(ColumnDescription = "键", IsNullable = false, Length = 255)]
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 值
    /// </summary>
    [SugarColumn(ColumnDescription = "值", IsNullable = true, Length = 2000)]
    public string? Value { get; set; }

    /// <summary>
    /// 响应
    /// </summary>
    [SugarColumn(ColumnDescription = "响应", IsNullable = true, Length = 2000)]
    public string? Response { get; set; }

    /// <summary>
    /// 操作用户
    /// </summary>
    [SugarColumn(ColumnDescription = "操作用户", IsNullable = false, Length = 50)]
    public string Username { get; set; } = string.Empty;

    /// <summary>
    /// 操作状态（0成功 1失败）
    /// </summary>
    [SugarColumn(ColumnDescription = "操作状态（0成功 1失败）", IsNullable = false)]
    public int Status { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    [SugarColumn(ColumnDescription = "错误信息", IsNullable = true, Length = 500)]
    public string? ErrorMessage { get; set; }
} 
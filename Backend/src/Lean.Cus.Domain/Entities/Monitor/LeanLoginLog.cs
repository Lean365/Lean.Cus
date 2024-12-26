//===================================================
// 项目名称：Lean.Cus.Domain.Entities.Monitor
// 文件名称：LeanLoginLog
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：登录日志实体
//===================================================

using SqlSugar;
using Lean.Cus.Domain.Entities;

namespace Lean.Cus.Domain.Entities.Monitor;

/// <summary>
/// 登录日志实体
/// </summary>
[SugarTable("Lean_login_log", "登录日志表")]
public class LeanLoginLog : LeanLongEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, IsNullable = false)]
    public string UserName { get; set; } = null!;

    /// <summary>
    /// IP地址
    /// </summary>
    [SugarColumn(ColumnName = "ip_addr", ColumnDescription = "IP地址", Length = 50, IsNullable = true)]
    public string? IpAddr { get; set; }

    /// <summary>
    /// 登录地点
    /// </summary>
    [SugarColumn(ColumnName = "login_location", ColumnDescription = "登录地点", Length = 100, IsNullable = true)]
    public string? LoginLocation { get; set; }

    /// <summary>
    /// 浏览器类型
    /// </summary>
    [SugarColumn(ColumnName = "browser", ColumnDescription = "浏览器类型", Length = 50, IsNullable = true)]
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    [SugarColumn(ColumnName = "os", ColumnDescription = "操作系统", Length = 50, IsNullable = true)]
    public string? Os { get; set; }

    /// <summary>
    /// 登录状态（0=失败，1=成功）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "登录状态（0=失败，1=成功）", IsNullable = false)]
    public int Status { get; set; }

    /// <summary>
    /// 提示消息
    /// </summary>
    [SugarColumn(ColumnName = "msg", ColumnDescription = "提示消息", Length = 255, IsNullable = true)]
    public string? Msg { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    [SugarColumn(ColumnName = "login_time", ColumnDescription = "登录时间", IsNullable = false)]
    public DateTime LoginTime { get; set; }
} 
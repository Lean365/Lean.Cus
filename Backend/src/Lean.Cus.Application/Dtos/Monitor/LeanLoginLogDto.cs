//===================================================
// 项目名称：Lean.Cus.Application.Dtos.Monitor
// 文件名称：LeanLoginLogDto
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：登录日志 DTO
//===================================================

using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Application.Dtos.Monitor;

#region 基础
/// <summary>
/// 登录日志基础DTO
/// </summary>
public class LeanLoginLogBaseDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddr { get; set; }

    /// <summary>
    /// 登录地点
    /// </summary>
    public string? LoginLocation { get; set; }

    /// <summary>
    /// 浏览器类型
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 登录状态（0=失败，1=成功）
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 提示消息
    /// </summary>
    public string? Msg { get; set; }

    /// <summary>
    /// 登录时间
    /// </summary>
    public DateTime LoginTime { get; set; }
}
#endregion

#region 查询
/// <summary>
/// 登录日志查询 DTO
/// </summary>
public class LeanLoginLogQueryDto : LeanPage
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddr { get; set; }

    /// <summary>
    /// 状态（0成功 1失败）
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
#endregion 
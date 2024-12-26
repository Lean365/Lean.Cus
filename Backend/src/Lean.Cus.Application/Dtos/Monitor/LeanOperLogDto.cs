//===================================================
// 项目名称：Lean.Cus.Application.Dtos.Monitor
// 文件名称：LeanOperLogDto
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：操作日志 DTO
//===================================================

using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Application.Dtos.Monitor;

/// <summary>
/// 操作日志 DTO
/// </summary>
public class LeanOperLogDto
{
    /// <summary>
    /// 模块标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    public string? OperName { get; set; }

    /// <summary>
    /// 业务类型（0其它 1新增 2修改 3删除）
    /// </summary>
    public LeanBusinessTypeEnum? BusinessType { get; set; }

    /// <summary>
    /// 操作状态（0正常 1异常）
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
/// <summary>
/// 操作日志查询 DTO
/// </summary>
public class LeanOperLogQueryDto : LeanPage
{
    /// <summary>
    /// 模块标题
    /// </summary>
    public string? Title { get; set; }

    /// <summary>
    /// 操作人员
    /// </summary>
    public string? OperName { get; set; }

    /// <summary>
    /// 业务类型（0其它 1新增 2修改 3删除）
    /// </summary>
    public LeanBusinessTypeEnum? BusinessType { get; set; }

    /// <summary>
    /// 操作状态（0正常 1异常）
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
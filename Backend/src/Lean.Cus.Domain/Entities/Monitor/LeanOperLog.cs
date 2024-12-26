//===================================================
// 项目名称：Lean.Cus.Domain.Entities.Monitor
// 文件名称：LeanOperLog
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：操作日志实体
//===================================================

using System.ComponentModel;
using SqlSugar;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Domain.Entities.Monitor;

/// <summary>
/// 操作日志实体
/// </summary>
[SugarTable("Lean_oper_log", "操作日志表")]
public class LeanOperLog: LeanLongEntity
{
    /// <summary>
    /// 日志主键
    /// </summary>
    [Description("日志编号")]
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long OperId { get; set; }

    /// <summary>
    /// 模块标题
    /// </summary>
    [Description("模块标题")]
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// 业务类型（0其它 1新增 2修改 3删除）
    /// </summary>
    [Description("业务类型")]
    public int BusinessType { get; set; }

    /// <summary>
    /// 方法名称
    /// </summary>
    [Description("方法名称")]
    public string Method { get; set; } = string.Empty;

    /// <summary>
    /// 请求方式
    /// </summary>
    [Description("请求方式")]
    public string RequestMethod { get; set; } = string.Empty;

    /// <summary>
    /// 操作人员
    /// </summary>
    [Description("操作人员")]
    public string OperName { get; set; } = string.Empty;

    /// <summary>
    /// 请求URL
    /// </summary>
    [Description("请求地址")]
    public string OperUrl { get; set; } = string.Empty;

    /// <summary>
    /// 主机地址
    /// </summary>
    [Description("主机地址")]
    public string OperIp { get; set; } = string.Empty;

    /// <summary>
    /// 操作地点
    /// </summary>
    [Description("操作地点")]
    public string OperLocation { get; set; } = string.Empty;

    /// <summary>
    /// 请求参数
    /// </summary>
    [Description("请求参数")]
    public string OperParam { get; set; } = string.Empty;

    /// <summary>
    /// 返回参数
    /// </summary>
    [Description("返回参数")]
    public string JsonResult { get; set; } = string.Empty;

    /// <summary>
    /// 操作状态（0正常 1异常）
    /// </summary>
    [Description("状态")]
    public int Status { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    [Description("错误消息")]
    public string ErrorMsg { get; set; } = string.Empty;

    /// <summary>
    /// 操作时间
    /// </summary>
    [Description("操作时间")]
    public DateTime OperTime { get; set; }

    /// <summary>
    /// 消耗时间
    /// </summary>
    [Description("消耗时间(毫秒)")]
    public long CostTime { get; set; }
} 
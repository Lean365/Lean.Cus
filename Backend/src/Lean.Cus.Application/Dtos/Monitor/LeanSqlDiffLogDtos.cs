//===================================================
// 项目名称：Lean.Cus.Application.Dtos.Monitor
// 文件名称：LeanSqlDiffLogDtos
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：SQL差异日志相关DTO
//===================================================

using System;
using System.ComponentModel;
using SqlSugar;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Application.Dtos.Monitor;

/// <summary>
/// SQL差异日志DTO
/// </summary>
public class LeanSqlDiffLogDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 数据库名称
    /// </summary>
    public string DatabaseName { get; set; } = null!;

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = null!;

    /// <summary>
    /// 操作类型（Insert/Update/Delete）
    /// </summary>
    public string OperationType { get; set; } = null!;

    /// <summary>
    /// 原始数据（JSON格式）
    /// </summary>
    public string? OldData { get; set; }

    /// <summary>
    /// 新数据（JSON格式）
    /// </summary>
    public string? NewData { get; set; }

    /// <summary>
    /// SQL语句
    /// </summary>
    public string SqlStatement { get; set; } = null!;

    /// <summary>
    /// 操作人ID
    /// </summary>
    public long OperatorId { get; set; }

    /// <summary>
    /// 操作人名称
    /// </summary>
    public string OperatorName { get; set; } = null!;

    /// <summary>
    /// IP地址
    /// </summary>
    public string? IpAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// SQL差异日志查询DTO
/// </summary>
public class LeanSqlDiffLogQueryDto : LeanPage
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    [Description("数据库名称")]
    public string? DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Description("表名")]
    public string? TableName { get; set; }

    /// <summary>
    /// 操作类型（Insert/Update/Delete）
    /// </summary>
    [Description("操作类型")]
    public string? OperationType { get; set; }

    /// <summary>
    /// 操作��名称
    /// </summary>
    [Description("操作人名称")]
    public string? OperatorName { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [Description("IP地址")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [Description("开始时间")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [Description("结束时间")]
    public DateTime? EndTime { get; set; }
}

/// <summary>
/// SQL差异日志输入DTO
/// </summary>
public class LeanSqlDiffLogInputDto
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    [Description("数据库名称")]
    public string DatabaseName { get; set; } = null!;

    /// <summary>
    /// 表名
    /// </summary>
    [Description("表名")]
    public string TableName { get; set; } = null!;

    /// <summary>
    /// 操作类型（Insert/Update/Delete）
    /// </summary>
    [Description("操作类型")]
    public string OperationType { get; set; } = null!;

    /// <summary>
    /// 原始数据（JSON格式）
    /// </summary>
    [Description("原始数据")]
    public string? OldData { get; set; }

    /// <summary>
    /// 新数据（JSON格式）
    /// </summary>
    [Description("新数据")]
    public string? NewData { get; set; }

    /// <summary>
    /// SQL语句
    /// </summary>
    [Description("SQL语句")]
    public string SqlStatement { get; set; } = null!;

    /// <summary>
    /// 操作人ID
    /// </summary>
    [Description("操作人ID")]
    public long OperatorId { get; set; }

    /// <summary>
    /// 操作人名称
    /// </summary>
    [Description("操作人名称")]
    public string OperatorName { get; set; } = null!;

    /// <summary>
    /// IP地址
    /// </summary>
    [Description("IP地址")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Description("备注")]
    public string? Remark { get; set; }
} 
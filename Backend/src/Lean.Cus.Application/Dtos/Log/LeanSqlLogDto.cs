using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Log;

/// <summary>
/// SQL差异日志查询条件
/// </summary>
public class LeanSqlLogQueryDto : PagedQuery
{
    /// <summary>
    /// SQL类型
    /// </summary>
    public LeanSqlType? SqlType { get; set; }

    /// <summary>
    /// 数据库名
    /// </summary>
    public string? DatabaseName { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// 执行状态
    /// </summary>
    public LeanStatus? Status { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// 创建时间范围开始
    /// </summary>
    public DateTime? CreatedTimeStart { get; set; }

    /// <summary>
    /// 创建时间范围结束
    /// </summary>
    public DateTime? CreatedTimeEnd { get; set; }
}

/// <summary>
/// SQL差异日志DTO
/// </summary>
public class LeanSqlLogDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// SQL类型
    /// </summary>
    public LeanSqlType SqlType { get; set; }

    /// <summary>
    /// 数据库名
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// SQL语句
    /// </summary>
    public string SqlText { get; set; } = string.Empty;

    /// <summary>
    /// SQL参数
    /// </summary>
    public string? Parameters { get; set; }

    /// <summary>
    /// 影响行数
    /// </summary>
    public int AffectedRows { get; set; }

    /// <summary>
    /// 执行时间（毫秒）
    /// </summary>
    public long ExecutionTime { get; set; }

    /// <summary>
    /// 执行状态
    /// </summary>
    public LeanStatus Status { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// SQL差异日志创建DTO
/// </summary>
public class LeanSqlLogCreateDto
{
    /// <summary>
    /// SQL类型
    /// </summary>
    [Required(ErrorMessage = "SQL类型不能为空")]
    public LeanSqlType SqlType { get; set; }

    /// <summary>
    /// 数据库名
    /// </summary>
    [Required(ErrorMessage = "数据库名不能为空")]
    [StringLength(100, ErrorMessage = "数据库名长度不能超过100个字符")]
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// SQL语句
    /// </summary>
    [Required(ErrorMessage = "SQL语句不能为空")]
    [StringLength(4000, ErrorMessage = "SQL语句长度不能超过4000个字符")]
    public string SqlText { get; set; } = string.Empty;

    /// <summary>
    /// SQL参数
    /// </summary>
    [StringLength(4000, ErrorMessage = "SQL参数长度不能超过4000个字符")]
    public string? Parameters { get; set; }

    /// <summary>
    /// 影响行数
    /// </summary>
    [Required(ErrorMessage = "影响行数不能为空")]
    public int AffectedRows { get; set; }

    /// <summary>
    /// 执行时间（毫秒）
    /// </summary>
    [Required(ErrorMessage = "执行时间不能为空")]
    public long ExecutionTime { get; set; }

    /// <summary>
    /// 执行状态
    /// </summary>
    [Required(ErrorMessage = "执行状态不能为空")]
    public LeanStatus Status { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    [StringLength(4000, ErrorMessage = "错误信息长度不能超过4000个字符")]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [StringLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
    public string? UserName { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [StringLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
    public string? Ip { get; set; }

    /// <summary>
    /// 位置
    /// </summary>
    [StringLength(100, ErrorMessage = "位置长度不能超过100个字符")]
    public string? Location { get; set; }
}

/// <summary>
/// SQL差异日志导出DTO
/// </summary>
public class LeanSqlLogExportDto
{
    /// <summary>
    /// SQL类型名称
    /// </summary>
    public string SqlTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 数据库名
    /// </summary>
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// SQL语句
    /// </summary>
    public string SqlText { get; set; } = string.Empty;

    /// <summary>
    /// 影响行数
    /// </summary>
    public int AffectedRows { get; set; }

    /// <summary>
    /// 执行时间（毫秒）
    /// </summary>
    public long ExecutionTime { get; set; }

    /// <summary>
    /// 执行状态名称
    /// </summary>
    public string StatusName { get; set; } = string.Empty;

    /// <summary>
    /// 错误信息
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
} 
using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Log;

/// <summary>
/// 审计日志查询条件
/// </summary>
public class LeanAuditLogQueryDto : PagedQuery
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 审计类型
    /// </summary>
    public LeanAuditType? AuditType { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string? TableName { get; set; }

    /// <summary>
    /// 业务ID
    /// </summary>
    public long? BusinessId { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public LeanOperationType? OperationType { get; set; }

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
/// 审计日志DTO
/// </summary>
public class LeanAuditLogDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 审计类型
    /// </summary>
    public LeanAuditType AuditType { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 业务ID
    /// </summary>
    public long BusinessId { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    public string? BusinessName { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public LeanOperationType OperationType { get; set; }

    /// <summary>
    /// 操作描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 旧值
    /// </summary>
    public string? OldValue { get; set; }

    /// <summary>
    /// 新值
    /// </summary>
    public string? NewValue { get; set; }

    /// <summary>
    /// 变更字段
    /// </summary>
    public string? ChangedFields { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 审计日志创建DTO
/// </summary>
public class LeanAuditLogCreateDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空")]
    [StringLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 审计类型
    /// </summary>
    [Required(ErrorMessage = "审计类型不能为空")]
    public LeanAuditType AuditType { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [Required(ErrorMessage = "表名不能为空")]
    [StringLength(100, ErrorMessage = "表名长度不能超过100个字符")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 业务ID
    /// </summary>
    [Required(ErrorMessage = "业务ID不能为空")]
    public long BusinessId { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [StringLength(100, ErrorMessage = "业务名称长度不能超过100个字符")]
    public string? BusinessName { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    [Required(ErrorMessage = "操作类型不能为空")]
    public LeanOperationType OperationType { get; set; }

    /// <summary>
    /// 操作描述
    /// </summary>
    [StringLength(500, ErrorMessage = "操作描述长度不能超过500个字符")]
    public string? Description { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [Required(ErrorMessage = "IP地址不能为空")]
    [StringLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 位置
    /// </summary>
    [StringLength(100, ErrorMessage = "位置长度不能超过100个字符")]
    public string? Location { get; set; }

    /// <summary>
    /// 旧值
    /// </summary>
    [StringLength(4000, ErrorMessage = "旧值长度不能超过4000个字符")]
    public string? OldValue { get; set; }

    /// <summary>
    /// 新值
    /// </summary>
    [StringLength(4000, ErrorMessage = "新值长度不能超过4000个字符")]
    public string? NewValue { get; set; }

    /// <summary>
    /// 变更字段
    /// </summary>
    [StringLength(4000, ErrorMessage = "变更字段长度不能超过4000个字符")]
    public string? ChangedFields { get; set; }
}

/// <summary>
/// 审计日志导出DTO
/// </summary>
public class LeanAuditLogExportDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 审计类型名称
    /// </summary>
    public string AuditTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 业务名称
    /// </summary>
    public string? BusinessName { get; set; }

    /// <summary>
    /// 操作类型名称
    /// </summary>
    public string OperationTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 操作描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 位置
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 变更字段
    /// </summary>
    public string? ChangedFields { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
} 
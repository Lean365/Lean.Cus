using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Log;

/// <summary>
/// 登录日志查询条件
/// </summary>
public class LeanLoginLogQueryDto : PagedQuery
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
    /// IP地址
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// 登录方式
    /// </summary>
    public LeanLoginType? LoginType { get; set; }

    /// <summary>
    /// 登录状态
    /// </summary>
    public LeanStatus? Status { get; set; }

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
/// 登录日志DTO
/// </summary>
public class LeanLoginLogDto
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
    /// IP地址
    /// </summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 登录位置
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 登录方式
    /// </summary>
    public LeanLoginType LoginType { get; set; }

    /// <summary>
    /// 登录状态
    /// </summary>
    public LeanStatus Status { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 登录日志创建DTO
/// </summary>
public class LeanLoginLogCreateDto
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
    /// IP地址
    /// </summary>
    [Required(ErrorMessage = "IP地址不能为空")]
    [StringLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 登录位置
    /// </summary>
    [StringLength(100, ErrorMessage = "登录位置长度不能超过100个字符")]
    public string? Location { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    [StringLength(50, ErrorMessage = "浏览器长度不能超过50个字符")]
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    [StringLength(50, ErrorMessage = "操作系统长度不能超过50个字符")]
    public string? Os { get; set; }

    /// <summary>
    /// 登录方式
    /// </summary>
    [Required(ErrorMessage = "登录方式不能为空")]
    public LeanLoginType LoginType { get; set; }

    /// <summary>
    /// 登录状态
    /// </summary>
    [Required(ErrorMessage = "登录状态不能为空")]
    public LeanStatus Status { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    [StringLength(500, ErrorMessage = "错误消息长度不能超过500个字符")]
    public string? ErrorMessage { get; set; }
}

/// <summary>
/// 登录日志导出DTO
/// </summary>
public class LeanLoginLogExportDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// IP地址
    /// </summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 登录位置
    /// </summary>
    public string? Location { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 登录方式名称
    /// </summary>
    public string LoginTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 登录状态名称
    /// </summary>
    public string StatusName { get; set; } = string.Empty;

    /// <summary>
    /// 错误消息
    /// </summary>
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
} 
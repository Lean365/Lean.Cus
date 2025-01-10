using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Log;

/// <summary>
/// 错误日志查询条件
/// </summary>
public class LeanErrorLogQueryDto : PagedQuery
{
    /// <summary>
    /// 错误级别
    /// </summary>
    public LeanErrorLevel? ErrorLevel { get; set; }

    /// <summary>
    /// 错误来源
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// 请求URL
    /// </summary>
    public string? Url { get; set; }

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
/// 错误日志DTO
/// </summary>
public class LeanErrorLogDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 错误级别
    /// </summary>
    public LeanErrorLevel ErrorLevel { get; set; }

    /// <summary>
    /// 错误来源
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// 错误消息
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 堆栈跟踪
    /// </summary>
    public string? StackTrace { get; set; }

    /// <summary>
    /// 内部异常
    /// </summary>
    public string? InnerException { get; set; }

    /// <summary>
    /// 请求URL
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 请求方法
    /// </summary>
    public string? Method { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    public string? Parameters { get; set; }

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
    /// 浏览器
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 错误日志创建DTO
/// </summary>
public class LeanErrorLogCreateDto
{
    /// <summary>
    /// 错误级别
    /// </summary>
    [Required(ErrorMessage = "错误级别不能为空")]
    public LeanErrorLevel ErrorLevel { get; set; }

    /// <summary>
    /// 错误来源
    /// </summary>
    [Required(ErrorMessage = "错误来源不能为空")]
    [StringLength(200, ErrorMessage = "错误来源长度不能超过200个字符")]
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// 错误消息
    /// </summary>
    [Required(ErrorMessage = "错误消息不能为空")]
    [StringLength(4000, ErrorMessage = "错误消息长度不能超过4000个字符")]
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 堆栈跟踪
    /// </summary>
    [StringLength(4000, ErrorMessage = "堆栈跟踪长度不能超过4000个字符")]
    public string? StackTrace { get; set; }

    /// <summary>
    /// 内部异常
    /// </summary>
    [StringLength(4000, ErrorMessage = "内部异常长度不能超过4000个字符")]
    public string? InnerException { get; set; }

    /// <summary>
    /// 请求URL
    /// </summary>
    [StringLength(500, ErrorMessage = "请求URL长度不能超过500个字符")]
    public string? Url { get; set; }

    /// <summary>
    /// 请求方法
    /// </summary>
    [StringLength(50, ErrorMessage = "请求方法长度不能超过50个字符")]
    public string? Method { get; set; }

    /// <summary>
    /// 请求参数
    /// </summary>
    [StringLength(4000, ErrorMessage = "请求参数长度不能超过4000个字符")]
    public string? Parameters { get; set; }

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
    /// 浏览器
    /// </summary>
    [StringLength(50, ErrorMessage = "浏览器长度不能超过50个字符")]
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    [StringLength(50, ErrorMessage = "操作系统长度不能超过50个字符")]
    public string? Os { get; set; }
}

/// <summary>
/// 错误日志导出DTO
/// </summary>
public class LeanErrorLogExportDto
{
    /// <summary>
    /// 错误级别名称
    /// </summary>
    public string ErrorLevelName { get; set; } = string.Empty;

    /// <summary>
    /// 错误来源
    /// </summary>
    public string Source { get; set; } = string.Empty;

    /// <summary>
    /// 错误消息
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// 请求URL
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// 请求方法
    /// </summary>
    public string? Method { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string? Ip { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string? Os { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
} 
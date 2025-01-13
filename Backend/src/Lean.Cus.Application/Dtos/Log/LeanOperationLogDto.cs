using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Log
{
    /// <summary>
    /// 操作日志查询条件
    /// </summary>
    public class LeanOperationLogQueryDto : PagedQuery
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

        /// <summary>
        /// 状态
        /// </summary>
        public LeanStatus? Status { get; set; }
    }

    /// <summary>
    /// 操作日志DTO
    /// </summary>
    public class LeanOperationLogDto
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
        /// 操作类型
        /// </summary>
        public LeanOperationType OperationType { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 请求方法
        /// </summary>
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// 请求URL
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 请求参数
        /// </summary>
        public string? Parameters { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; } = string.Empty;

        /// <summary>
        /// 地理位置
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
        /// 返回结果
        /// </summary>
        public string? Result { get; set; }

        /// <summary>
        /// 执行时长(毫秒)
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public LeanStatus Status { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }

    /// <summary>
    /// 操作日志创建DTO
    /// </summary>
    public class LeanOperationLogCreateDto
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
        /// 操作类型
        /// </summary>
        [Required(ErrorMessage = "操作类型不能为空")]
        public LeanOperationType OperationType { get; set; }

        /// <summary>
        /// 操作描述
        /// </summary>
        [Required(ErrorMessage = "操作描述不能为空")]
        [StringLength(500, ErrorMessage = "操作描述长度不能超过500个字符")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 请求方法
        /// </summary>
        [Required(ErrorMessage = "请求方法不能为空")]
        [StringLength(50, ErrorMessage = "请求方法长度不能超过50个字符")]
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// 请求URL
        /// </summary>
        [Required(ErrorMessage = "请求URL不能为空")]
        [StringLength(500, ErrorMessage = "请求URL长度不能超过500个字符")]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// 请求参数
        /// </summary>
        [StringLength(4000, ErrorMessage = "请求参数长度不能超过4000个字符")]
        public string? Parameters { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        [Required(ErrorMessage = "IP地址不能为空")]
        [StringLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
        public string Ip { get; set; } = string.Empty;

        /// <summary>
        /// 地理位置
        /// </summary>
        [StringLength(100, ErrorMessage = "地理位置长度不能超过100个字符")]
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
        /// 返回结果
        /// </summary>
        [StringLength(4000, ErrorMessage = "返回结果长度不能超过4000个字符")]
        public string? Result { get; set; }

        /// <summary>
        /// 执行时长(毫秒)
        /// </summary>
        [Required(ErrorMessage = "执行时长不能为空")]
        public long Duration { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Required(ErrorMessage = "状态不能为空")]
        public LeanStatus Status { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        [StringLength(4000, ErrorMessage = "错误信息长度不能超过4000个字符")]
        public string? ErrorMessage { get; set; }
    }

    /// <summary>
    /// 操作日志导出DTO
    /// </summary>
    public class LeanOperationLogExportDto
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// 操作类型名称
        /// </summary>
        public string OperationTypeName { get; set; } = string.Empty;

        /// <summary>
        /// 操作描述
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// 请求方法
        /// </summary>
        public string Method { get; set; } = string.Empty;

        /// <summary>
        /// 请求URL
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; } = string.Empty;

        /// <summary>
        /// 地理位置
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
        /// 执行时长(毫秒)
        /// </summary>
        public long Duration { get; set; }

        /// <summary>
        /// 状态名称
        /// </summary>
        public string StatusName { get; set; } = string.Empty;

        /// <summary>
        /// 错误信息
        /// </summary>
        public string? ErrorMessage { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
} 
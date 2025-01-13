#nullable enable

//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件的所有修改。
// </auto-generated>
//
// <copyright file="LeanUserExtendDto.cs" company="Lean365">
//     Copyright (c) Lean365. All rights reserved.
// </copyright>
// <author>Lean365</author>
// <date>2024-01-13</date>
// <summary>
// 用户扩展信息DTO
// </summary>
//------------------------------------------------------------------------------

using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 用户扩展信息查询条件
/// </summary>
public class LeanUserExtendQueryDto : PagedQuery
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 登录IP
    /// </summary>
    public string? LoginIp { get; set; }

    /// <summary>
    /// 登录设备
    /// </summary>
    public string? LoginDevice { get; set; }

    /// <summary>
    /// 登录地点
    /// </summary>
    public string? LoginLocation { get; set; }

    /// <summary>
    /// 登录浏览器
    /// </summary>
    public string? LoginBrowser { get; set; }

    /// <summary>
    /// 登录操作系统
    /// </summary>
    public string? LoginOs { get; set; }

    /// <summary>
    /// 最后登录时间范围开始
    /// </summary>
    public DateTime? LastLoginTimeStart { get; set; }

    /// <summary>
    /// 最后登录时间范围结束
    /// </summary>
    public DateTime? LastLoginTimeEnd { get; set; }

    /// <summary>
    /// 最后登出时间范围开始
    /// </summary>
    public DateTime? LastLogoutTimeStart { get; set; }

    /// <summary>
    /// 最后登出时间范围结束
    /// </summary>
    public DateTime? LastLogoutTimeEnd { get; set; }

    /// <summary>
    /// 是否锁定
    /// </summary>
    public bool? IsLocked { get; set; }
}

/// <summary>
/// 用户扩展信息DTO
/// </summary>
public class LeanUserExtendDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    #region 首次登录信息
    /// <summary>
    /// 首次登录时间
    /// </summary>
    public DateTime? FirstLoginTime { get; set; }

    /// <summary>
    /// 首次登录IP
    /// </summary>
    public string? FirstLoginIp { get; set; }

    /// <summary>
    /// 首次登录设备
    /// </summary>
    public string? FirstLoginDevice { get; set; }

    /// <summary>
    /// 首次登录地点
    /// </summary>
    public string? FirstLoginLocation { get; set; }

    /// <summary>
    /// 首次登录浏览器
    /// </summary>
    public string? FirstLoginBrowser { get; set; }

    /// <summary>
    /// 首次登录操作系统
    /// </summary>
    public string? FirstLoginOs { get; set; }
    #endregion

    #region 末次登录信息
    /// <summary>
    /// 末次登录时间
    /// </summary>
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 末次登录IP
    /// </summary>
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// 末次登录设备
    /// </summary>
    public string? LastLoginDevice { get; set; }

    /// <summary>
    /// 末次登录地点
    /// </summary>
    public string? LastLoginLocation { get; set; }

    /// <summary>
    /// 末次登录浏览器
    /// </summary>
    public string? LastLoginBrowser { get; set; }

    /// <summary>
    /// 末次登录操作系统
    /// </summary>
    public string? LastLoginOs { get; set; }
    #endregion

    #region 末次登出信息
    /// <summary>
    /// 末次登出时间
    /// </summary>
    public DateTime? LastLogoutTime { get; set; }

    /// <summary>
    /// 末次登出IP
    /// </summary>
    public string? LastLogoutIp { get; set; }

    /// <summary>
    /// 末次登出方式
    /// </summary>
    public string? LastLogoutType { get; set; }
    #endregion

    #region 统计信息
    /// <summary>
    /// 登录总次数
    /// </summary>
    public int LoginCount { get; set; }

    /// <summary>
    /// 密码错误次数
    /// </summary>
    public int PasswordErrorCount { get; set; }

    /// <summary>
    /// 最后密码错误时间
    /// </summary>
    public DateTime? LastPasswordErrorTime { get; set; }

    /// <summary>
    /// 锁定结束时间
    /// </summary>
    public DateTime? LockoutEndTime { get; set; }
    #endregion
}

/// <summary>
/// 用户登录信息更新DTO
/// </summary>
public class LeanUserExtendLoginUpdateDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long UserId { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [StringLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// 设备
    /// </summary>
    [StringLength(200, ErrorMessage = "设备长度不能超过200个字符")]
    public string? Device { get; set; }

    /// <summary>
    /// 地理位置
    /// </summary>
    [StringLength(200, ErrorMessage = "地理位置长度不能超过200个字符")]
    public string? Location { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    [StringLength(200, ErrorMessage = "浏览器长度不能超过200个字符")]
    public string? Browser { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    [StringLength(200, ErrorMessage = "操作系统长度不能超过200个字符")]
    public string? Os { get; set; }
}

/// <summary>
/// 用户登出信息更新DTO
/// </summary>
public class LeanUserExtendLogoutUpdateDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long UserId { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [StringLength(50, ErrorMessage = "IP地址长度不能超过50个字符")]
    public string? IpAddress { get; set; }

    /// <summary>
    /// 登出方式（主动登出/超时登出/强制登出）
    /// </summary>
    [Required(ErrorMessage = "登出方式不能为空")]
    [StringLength(50, ErrorMessage = "登出方式长度不能超过50个字符")]
    public string LogoutType { get; set; } = string.Empty;
}

/// <summary>
/// 用户密码错误信息更新DTO
/// </summary>
public class LeanUserExtendPasswordErrorUpdateDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long UserId { get; set; }

    /// <summary>
    /// 锁定结束时间
    /// </summary>
    public DateTime? LockoutEndTime { get; set; }
}

/// <summary>
/// 用户扩展信息导出DTO
/// </summary>
public class LeanUserExtendExportDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 首次登录时间
    /// </summary>
    public DateTime? FirstLoginTime { get; set; }

    /// <summary>
    /// 首次登录IP
    /// </summary>
    public string? FirstLoginIp { get; set; }

    /// <summary>
    /// 首次登录设备
    /// </summary>
    public string? FirstLoginDevice { get; set; }

    /// <summary>
    /// 首次登录地点
    /// </summary>
    public string? FirstLoginLocation { get; set; }

    /// <summary>
    /// 首次登录浏览器
    /// </summary>
    public string? FirstLoginBrowser { get; set; }

    /// <summary>
    /// 首次登录操作系统
    /// </summary>
    public string? FirstLoginOs { get; set; }

    /// <summary>
    /// 末次登录时间
    /// </summary>
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 末次登录IP
    /// </summary>
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// 末次登录设备
    /// </summary>
    public string? LastLoginDevice { get; set; }

    /// <summary>
    /// 末次登录地点
    /// </summary>
    public string? LastLoginLocation { get; set; }

    /// <summary>
    /// 末次登录浏览器
    /// </summary>
    public string? LastLoginBrowser { get; set; }

    /// <summary>
    /// 末次登录操作系统
    /// </summary>
    public string? LastLoginOs { get; set; }

    /// <summary>
    /// 末次登出时间
    /// </summary>
    public DateTime? LastLogoutTime { get; set; }

    /// <summary>
    /// 末次登出IP
    /// </summary>
    public string? LastLogoutIp { get; set; }

    /// <summary>
    /// 末次登出方式
    /// </summary>
    public string? LastLogoutType { get; set; }

    /// <summary>
    /// 登录总次数
    /// </summary>
    public int LoginCount { get; set; }

    /// <summary>
    /// 密码错误次数
    /// </summary>
    public int PasswordErrorCount { get; set; }

    /// <summary>
    /// 最后密码错误时间
    /// </summary>
    public DateTime? LastPasswordErrorTime { get; set; }

    /// <summary>
    /// 锁定结束时间
    /// </summary>
    public DateTime? LockoutEndTime { get; set; }
} 
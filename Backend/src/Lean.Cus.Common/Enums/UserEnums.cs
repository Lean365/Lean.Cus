using System;
using System.ComponentModel;

namespace Lean.Cus.Common.Enums;

/// <summary>
/// 用户类型枚举
/// </summary>
public enum LeanUserType
{
    /// <summary>
    /// 超级管理员
    /// </summary>
    [Description("超级管理员")]
    SuperAdmin = 0,

    /// <summary>
    /// 管理员
    /// </summary>
    [Description("管理员")]
    Admin = 1,

    /// <summary>
    /// 系统用户
    /// </summary>
    [Description("系统用户")]
    System = 2,

    /// <summary>
    /// 普通用户
    /// </summary>
    [Description("普通用户")]
    Normal = 3,

    /// <summary>
    /// 访客
    /// </summary>
    [Description("访客")]
    Guest = 4,

    /// <summary>
    /// VIP用户
    /// </summary>
    [Description("VIP用户")]
    VIP = 5
}

/// <summary>
/// 用户状态枚举
/// </summary>
public enum LeanUserStatus
{
    /// <summary>
    /// 禁用
    /// </summary>
    [Description("禁用")]
    Disabled = 0,

    /// <summary>
    /// 启用
    /// </summary>
    [Description("启用")]
    Enabled = 1,

    /// <summary>
    /// 锁定
    /// </summary>
    [Description("锁定")]
    Locked = 2,

    /// <summary>
    /// 过期
    /// </summary>
    [Description("过期")]
    Expired = 3,

    /// <summary>
    /// 待审核
    /// </summary>
    [Description("待审核")]
    PendingReview = 4,

    /// <summary>
    /// 审核拒绝
    /// </summary>
    [Description("审核拒绝")]
    ReviewRejected = 5,

    /// <summary>
    /// 注销
    /// </summary>
    [Description("注销")]
    Cancelled = 6
} 
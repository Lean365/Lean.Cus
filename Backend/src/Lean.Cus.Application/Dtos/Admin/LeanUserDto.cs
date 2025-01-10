using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 用户查询条件
/// </summary>
public class LeanUserQueryDto : PagedQuery
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanUserStatus? Status { get; set; }

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
/// 用户DTO
/// </summary>
public class LeanUserDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanUserStatus Status { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}

/// <summary>
/// 用户创建DTO
/// </summary>
public class LeanUserCreateDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空")]
    [StringLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    [StringLength(100, ErrorMessage = "密码长度不能超过100个字符")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 真实姓名
    /// </summary>
    [Required(ErrorMessage = "真实姓名不能为空")]
    [StringLength(50, ErrorMessage = "真实姓名长度不能超过50个字符")]
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名称
    /// </summary>
    [StringLength(50, ErrorMessage = "英文名称长度不能超过50个字符")]
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    /// 手机号码
    /// </summary>
    [Required(ErrorMessage = "手机号码不能为空")]
    [StringLength(20, ErrorMessage = "手机号码长度不能超过20个字符")]
    [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "手机号码格式不正确")]
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱
    /// </summary>
    [Required(ErrorMessage = "邮箱不能为空")]
    [StringLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 用户类型
    /// </summary>
    [Required(ErrorMessage = "用户类型不能为空")]
    public LeanUserType UserType { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public LeanUserStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }
}

/// <summary>
/// 用户更新DTO
/// </summary>
public class LeanUserUpdateDto : LeanUserCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 密码（可选，不修改密码时不传）
    /// </summary>
    [StringLength(100, ErrorMessage = "密码长度不能超过100个字符")]
    public new string? Password { get; set; }
}

/// <summary>
/// 用户状态更新DTO
/// </summary>
public class LeanUserStatusUpdateDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public LeanUserStatus Status { get; set; }
}

/// <summary>
/// 重置密码DTO
/// </summary>
public class LeanUserResetPasswordDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 修改密码DTO
/// </summary>
public class LeanUserChangePasswordDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 旧密码
    /// </summary>
    [Required(ErrorMessage = "旧密码不能为空")]
    [StringLength(100, ErrorMessage = "密码长度不能超过100个字符")]
    public string OldPassword { get; set; } = string.Empty;

    /// <summary>
    /// 新密码
    /// </summary>
    [Required(ErrorMessage = "新密码不能为空")]
    [StringLength(100, ErrorMessage = "密码长度不能超过100个字符")]
    public string NewPassword { get; set; } = string.Empty;
}

/// <summary>
/// 用户导入DTO
/// </summary>
public class LeanUserImportDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空")]
    [StringLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    [StringLength(100, ErrorMessage = "密码长度不能超过100个字符")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// 真实姓名
    /// </summary>
    [Required(ErrorMessage = "真实姓名不能为空")]
    [StringLength(50, ErrorMessage = "真实姓名长度不能超过50个字符")]
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名称
    /// </summary>
    [StringLength(50, ErrorMessage = "英文名称长度不能超过50个字符")]
    public string EnglishName { get; set; } = string.Empty;

    /// <summary>
    /// 手机号码
    /// </summary>
    [Required(ErrorMessage = "手机号码不能为空")]
    [StringLength(20, ErrorMessage = "手机号码长度不能超过20个字符")]
    [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "手机号码格式不正确")]
    public string Mobile { get; set; } = string.Empty;

    /// <summary>
    /// 邮箱
    /// </summary>
    [Required(ErrorMessage = "邮箱不能为空")]
    [StringLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string Email { get; set; } = string.Empty;

    /// <summary>
    /// 用户类型(0：超级管理员，1：管理员，2：系统用户，3：普通用户，4：访客，5：VIP用户)
    /// </summary>
    [Required(ErrorMessage = "用户类型不能为空")]
    public LeanUserType UserType { get; set; } = LeanUserType.Normal;

    /// <summary>
    /// 状态(0：禁用，1：启用，2：锁定，3：过期，4：待审核，5：审核拒绝，6：注销)
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public LeanUserStatus Status { get; set; } = LeanUserStatus.Enabled;

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }
}

/// <summary>
/// 用户导出DTO
/// </summary>
public class LeanUserExportDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 英文名称
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 用户类型名称
    /// </summary>
    public string UserTypeName { get; set; }

    /// <summary>
    /// 状态名称
    /// </summary>
    public string StatusName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
}

/// <summary>
/// 用户导入模板DTO
/// </summary>
public class LeanUserImportTemplateDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    public string Password { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    /// 英文名称
    /// </summary>
    public string EnglishName { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    public string Mobile { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// 用户类型(0：超级管理员，1：管理员，2：系统用户，3：普通用户，4：访客，5：VIP用户)
    /// </summary>
    public int UserType { get; set; }

    /// <summary>
    /// 状态(0：禁用，1：启用，2：锁定，3：过期，4：待审核，5：审核拒绝，6：注销)
    /// </summary>
    public int Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
} 
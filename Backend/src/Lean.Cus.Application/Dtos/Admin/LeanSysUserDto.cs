//===================================================
// 项目名称：Lean.Cus.Application
// 文件名称：LeanSysUserDto
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.14
// 更新时间：2024.01.14
// 备注说明：系统用户DTO
//===================================================

using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Application.Dtos.Admin;

#region 基础DTO
/// <summary>
/// 系统用户基础DTO
/// </summary>
public class LeanSysUserBaseDto
{
    /// <summary>
    /// 用户编码
    /// </summary>
    [Required(ErrorMessage = "用户编码不能为空")]
    [StringLength(50, ErrorMessage = "用户编码长度不能超过50个字符")]
    public string UserCode { get; set; } = null!;

    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空")]
    [StringLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
    public string UserName { get; set; } = null!;

    /// <summary>
    /// 昵称
    /// </summary>
    [StringLength(50, ErrorMessage = "昵称长度不能超过50个字符")]
    public string? NickName { get; set; }

    /// <summary>
    /// 英文名
    /// </summary>
    [StringLength(50, ErrorMessage = "英文名长度不能超过50个字符")]
    public string? EnglishName { get; set; }

    /// <summary>
    /// 用户类型（0=超级管理员，1=管理员，2=普通用户）
    /// </summary>
    [Required(ErrorMessage = "用户类型不能为空")]
    [Range(0, 2, ErrorMessage = "用户类型只能是0、1、2")]
    public int UserType { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    [StringLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
    public string? Email { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [Phone(ErrorMessage = "手机号码格式不正确")]
    [StringLength(20, ErrorMessage = "手机号码长度不能超过20个字符")]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
    /// </summary>
    [Required(ErrorMessage = "性别不能为空")]
    [Range(0, 2, ErrorMessage = "性别只能是0、1、2")]
    public int Gender { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    [StringLength(500, ErrorMessage = "头像地址长度不能超过500个字符")]
    public string? Avatar { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    [Range(0, 1, ErrorMessage = "状态只能是0、1")]
    public int Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }
}
#endregion

#region 查询条件
/// <summary>
/// 系统用户查询DTO
/// </summary>
public class LeanSysUserQueryDto : LeanPage
{
    /// <summary>
    /// 关键字
    /// </summary>
    public string? Keyword { get; set; }

    /// <summary>
    /// 用户类型
    /// </summary>
    public int? UserType { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public int? Status { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }
}
#endregion

#region 新增
/// <summary>
/// 系统用户新增DTO
/// </summary>
public class LeanSysUserCreateDto : LeanSysUserBaseDto
{
    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "密码长度必须在6-100个字符之间")]
    public string Password { get; set; } = null!;
}
#endregion

#region 更新
/// <summary>
/// 系统用户更新DTO
/// </summary>
public class LeanSysUserUpdateDto : LeanSysUserBaseDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long Id { get; set; }
}
#endregion

#region 密码
/// <summary>
/// 系统用户密码修改DTO
/// </summary>
public class LeanSysUserPasswordDto
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
    [StringLength(100, MinimumLength = 6, ErrorMessage = "旧密码长度必须在6-100个字符之间")]
    public string OldPassword { get; set; } = null!;

    /// <summary>
    /// 新密码
    /// </summary>
    [Required(ErrorMessage = "新密码不能为空")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "新密码长度必须在6-100个字符之间")]
    public string NewPassword { get; set; } = null!;

    /// <summary>
    /// 确认密码
    /// </summary>
    [Required(ErrorMessage = "确认密码不能为空")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "确认密码长度必须在6-100个字符之间")]
    [Compare("NewPassword", ErrorMessage = "两次输入的密码不一致")]
    public string ConfirmPassword { get; set; } = null!;
}

/// <summary>
/// 系统用户密码重置DTO
/// </summary>
public class LeanSysUserResetPasswordDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 新密码
    /// </summary>
    [Required(ErrorMessage = "新密码不能为空")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "新密码长度必须在6-100个字符之间")]
    public string NewPassword { get; set; } = null!;
}
#endregion

#region 状态
/// <summary>
/// 系统用户状态修改DTO
/// </summary>
public class LeanSysUserStatusDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启用）
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    [Range(0, 1, ErrorMessage = "状态只能是0、1")]
    public int Status { get; set; }
}
#endregion 
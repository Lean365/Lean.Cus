using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 用户DTO
/// </summary>
public class LeanUserDto
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名
    /// </summary>
    public string? EnglishName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public LeanGender Gender { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 是否管理员
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 岗位ID列表
    /// </summary>
    public List<long> PostIds { get; set; } = new();

    /// <summary>
    /// 岗位名称列表
    /// </summary>
    public List<string> PostNames { get; set; } = new();

    /// <summary>
    /// 角色ID列表
    /// </summary>
    public List<long> RoleIds { get; set; } = new();

    /// <summary>
    /// 角色名称列表
    /// </summary>
    public List<string> RoleNames { get; set; } = new();

    /// <summary>
    /// 最后登录IP
    /// </summary>
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime? LastLoginTime { get; set; }
}

/// <summary>
/// 用户查询DTO
/// </summary>
public class LeanUserQueryDto : LeanPagedDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string? UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus? Status { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 岗位ID
    /// </summary>
    public long? PostId { get; set; }
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
    /// 昵称
    /// </summary>
    [Required(ErrorMessage = "昵称不能为空")]
    [StringLength(50, ErrorMessage = "昵称长度不能超过50个字符")]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名
    /// </summary>
    [StringLength(50, ErrorMessage = "英文名长度不能超过50个字符")]
    public string? EnglishName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    [StringLength(200, ErrorMessage = "头像地址长度不能超过200个字符")]
    public string? Avatar { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [StringLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [StringLength(20, ErrorMessage = "手机号长度不能超过20个字符")]
    [Phone(ErrorMessage = "手机号格式不正确")]
    public string? Phone { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public LeanGender Gender { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; } = LeanEnabledStatus.Enabled;

    /// <summary>
    /// 是否管理员
    /// </summary>
    public bool IsAdmin { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    public long? DeptId { get; set; }

    /// <summary>
    /// 岗位ID列表
    /// </summary>
    public List<long> PostIds { get; set; } = new();

    /// <summary>
    /// 角色ID列表
    /// </summary>
    public List<long> RoleIds { get; set; } = new();
}

/// <summary>
/// 用户更新DTO
/// </summary>
public class LeanUserUpdateDto : LeanUserCreateDto
{
    /// <summary>
    /// ID
    /// </summary>
    [Required(ErrorMessage = "ID不能为空")]
    public long Id { get; set; }
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
    /// 昵称
    /// </summary>
    [Required(ErrorMessage = "昵称不能为空")]
    [StringLength(50, ErrorMessage = "昵称长度不能超过50个字符")]
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名
    /// </summary>
    [StringLength(50, ErrorMessage = "英文名长度不能超过50个字符")]
    public string? EnglishName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [StringLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    [StringLength(20, ErrorMessage = "手机号长度不能超过20个字符")]
    [Phone(ErrorMessage = "手机号格式不正确")]
    public string? Phone { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public LeanGender Gender { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; } = LeanEnabledStatus.Enabled;

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 岗位名称列表，多个岗位用逗号分隔
    /// </summary>
    public string? PostNames { get; set; }

    /// <summary>
    /// 角色名称列表，多个角色用逗号分隔
    /// </summary>
    public string? RoleNames { get; set; }
}

/// <summary>
/// 用户导出DTO
/// </summary>
public class LeanUserExportDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = string.Empty;

    /// <summary>
    /// 英文名
    /// </summary>
    public string? EnglishName { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 手机号
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 性别名称
    /// </summary>
    public string GenderName { get; set; } = string.Empty;

    /// <summary>
    /// 状态名称
    /// </summary>
    public string StatusName { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DeptName { get; set; }

    /// <summary>
    /// 岗位名称列表，多个岗位用逗号分隔
    /// </summary>
    public string? PostNames { get; set; }

    /// <summary>
    /// 角色名称列表，多个角色用逗号分隔
    /// </summary>
    public string? RoleNames { get; set; }

    /// <summary>
    /// 最后登录IP
    /// </summary>
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    public DateTime? LastLoginTime { get; set; }
}

/// <summary>
/// 用户导入模板DTO
/// </summary>
public class LeanUserImportTemplateDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = "请输入用户名";

    /// <summary>
    /// 昵称
    /// </summary>
    public string NickName { get; set; } = "请输入昵称";

    /// <summary>
    /// 英文名
    /// </summary>
    public string EnglishName { get; set; } = "请输入英文名";

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; } = "请输入邮箱";

    /// <summary>
    /// 手机号
    /// </summary>
    public string Phone { get; set; } = "请输入手机号";

    /// <summary>
    /// 性别
    /// </summary>
    public string Gender { get; set; } = "请输入性别：未知、男、女";

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; } = "请输入状态：启用、禁用";

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DeptName { get; set; } = "请输入部门名称";

    /// <summary>
    /// 岗位名称列表
    /// </summary>
    public string PostNames { get; set; } = "请输入岗位名称，多个岗位用逗号分隔";

    /// <summary>
    /// 角色名称列表
    /// </summary>
    public string RoleNames { get; set; } = "请输入角色名称，多个角色用逗号分隔";
} 
using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 用户角色关联查询条件
/// </summary>
public class LeanUserRoleQueryDto : PagedQuery
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
    /// 角色ID
    /// </summary>
    public long? RoleId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string? RoleName { get; set; }

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
/// 用户角色关联DTO
/// </summary>
public class LeanUserRoleDto
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
    /// 角色ID
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 用户角色关联创建DTO
/// </summary>
public class LeanUserRoleCreateDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long UserId { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    [Required(ErrorMessage = "角色ID不能为空")]
    public long RoleId { get; set; }
}

/// <summary>
/// 用户角色关联更新DTO
/// </summary>
public class LeanUserRoleUpdateDto : LeanUserRoleCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "用户角色关联ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 用户角色关联导入DTO
/// </summary>
public class LeanUserRoleImportDto
{
    /// <summary>
    /// 用户工号
    /// </summary>
    [Required(ErrorMessage = "用户工号不能为空")]
    [StringLength(50, ErrorMessage = "用户工号长度不能超过50个字符")]
    public string UserCode { get; set; } = string.Empty;

    /// <summary>
    /// 角色编码
    /// </summary>
    [Required(ErrorMessage = "角色编码不能为空")]
    [StringLength(50, ErrorMessage = "角色编码长度不能超过50个字符")]
    public string RoleCode { get; set; } = string.Empty;
}

/// <summary>
/// 用户角色关联导出DTO
/// </summary>
public class LeanUserRoleExportDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 用户角色关联导入模板DTO
/// </summary>
public class LeanUserRoleImportTemplateDto
{
    /// <summary>
    /// 用户工号
    /// </summary>
    public string UserCode { get; set; } = string.Empty;

    /// <summary>
    /// 角色编码
    /// </summary>
    public string RoleCode { get; set; } = string.Empty;
} 
using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 角色权限关联查询条件
/// </summary>
public class LeanRolePermissionQueryDto : PagedQuery
{
    /// <summary>
    /// 角色ID
    /// </summary>
    public long? RoleId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string? RoleName { get; set; }

    /// <summary>
    /// 权限ID
    /// </summary>
    public long? PermissionId { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    public string? PermissionName { get; set; }

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
/// 角色权限关联DTO
/// </summary>
public class LeanRolePermissionDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    public long RoleId { get; set; }

    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 权限ID
    /// </summary>
    public long PermissionId { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    public string PermissionName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 角色权限关联创建DTO
/// </summary>
public class LeanRolePermissionCreateDto
{
    /// <summary>
    /// 角色ID
    /// </summary>
    [Required(ErrorMessage = "角色ID不能为空")]
    public long RoleId { get; set; }

    /// <summary>
    /// 权限ID
    /// </summary>
    [Required(ErrorMessage = "权限ID不能为空")]
    public long PermissionId { get; set; }
}

/// <summary>
/// 角色权限关联更新DTO
/// </summary>
public class LeanRolePermissionUpdateDto : LeanRolePermissionCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "角色权限关联ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 角色权限关联导入DTO
/// </summary>
public class LeanRolePermissionImportDto
{
    /// <summary>
    /// 角色编码
    /// </summary>
    [Required(ErrorMessage = "角色编码不能为空")]
    [StringLength(50, ErrorMessage = "角色编码长度不能超过50个字符")]
    public string RoleCode { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    [Required(ErrorMessage = "权限编码不能为空")]
    [StringLength(50, ErrorMessage = "权限编码长度不能超过50个字符")]
    public string PermissionCode { get; set; } = string.Empty;
}

/// <summary>
/// 角色权限关联导出DTO
/// </summary>
public class LeanRolePermissionExportDto
{
    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 权限名称
    /// </summary>
    public string PermissionName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 角色权限关联导入模板DTO
/// </summary>
public class LeanRolePermissionImportTemplateDto
{
    /// <summary>
    /// 角色编码
    /// </summary>
    public string RoleCode { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    public string PermissionCode { get; set; } = string.Empty;
} 
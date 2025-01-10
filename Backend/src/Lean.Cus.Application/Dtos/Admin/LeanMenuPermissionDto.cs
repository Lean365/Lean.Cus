using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 菜单权限关联查询条件
/// </summary>
public class LeanMenuPermissionQueryDto : PagedQuery
{
    /// <summary>
    /// 菜单ID
    /// </summary>
    public long? MenuId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string? MenuName { get; set; }

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
/// 菜单权限关联DTO
/// </summary>
public class LeanMenuPermissionDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 菜单ID
    /// </summary>
    public long MenuId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; } = string.Empty;

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
/// 菜单权限关联创建DTO
/// </summary>
public class LeanMenuPermissionCreateDto
{
    /// <summary>
    /// 菜单ID
    /// </summary>
    [Required(ErrorMessage = "菜单ID不能为空")]
    public long MenuId { get; set; }

    /// <summary>
    /// 权限ID
    /// </summary>
    [Required(ErrorMessage = "权限ID不能为空")]
    public long PermissionId { get; set; }
}

/// <summary>
/// 菜单权限关联更新DTO
/// </summary>
public class LeanMenuPermissionUpdateDto : LeanMenuPermissionCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "菜单权限关联ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 菜单权限关联导入DTO
/// </summary>
public class LeanMenuPermissionImportDto
{
    /// <summary>
    /// 菜单编码
    /// </summary>
    [Required(ErrorMessage = "菜单编码不能为空")]
    [StringLength(50, ErrorMessage = "菜单编码长度不能超过50个字符")]
    public string MenuCode { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    [Required(ErrorMessage = "权限编码不能为空")]
    [StringLength(50, ErrorMessage = "权限编码长度不能超过50个字符")]
    public string PermissionCode { get; set; } = string.Empty;
}

/// <summary>
/// 菜单权限关联导出DTO
/// </summary>
public class LeanMenuPermissionExportDto
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; } = string.Empty;

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
/// 菜单权限关联导入模板DTO
/// </summary>
public class LeanMenuPermissionImportTemplateDto
{
    /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    public string PermissionCode { get; set; } = string.Empty;
} 
using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 角色部门关联查询条件
/// </summary>
public class LeanRoleDepartmentQueryDto : PagedQuery
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
    /// 部门ID
    /// </summary>
    public long? DepartmentId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DepartmentName { get; set; }

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
/// 角色部门关联DTO
/// </summary>
public class LeanRoleDepartmentDto
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
    /// 部门ID
    /// </summary>
    public long DepartmentId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 角色部门关联创建DTO
/// </summary>
public class LeanRoleDepartmentCreateDto
{
    /// <summary>
    /// 角色ID
    /// </summary>
    [Required(ErrorMessage = "角色ID不能为空")]
    public long RoleId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [Required(ErrorMessage = "部门ID不能为空")]
    public long DepartmentId { get; set; }
}

/// <summary>
/// 角色部门关联更新DTO
/// </summary>
public class LeanRoleDepartmentUpdateDto : LeanRoleDepartmentCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "角色部门关联ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 角色部门关联导入DTO
/// </summary>
public class LeanRoleDepartmentImportDto
{
    /// <summary>
    /// 角色编码
    /// </summary>
    [Required(ErrorMessage = "角色编码不能为空")]
    [StringLength(50, ErrorMessage = "角色编码长度不能超过50个字符")]
    public string RoleCode { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    [Required(ErrorMessage = "部门编码不能为空")]
    [StringLength(50, ErrorMessage = "部门编码长度不能超过50个字符")]
    public string DepartmentCode { get; set; } = string.Empty;
}

/// <summary>
/// 角色部门关联导出DTO
/// </summary>
public class LeanRoleDepartmentExportDto
{
    /// <summary>
    /// 角色名称
    /// </summary>
    public string RoleName { get; set; } = string.Empty;

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 角色部门关联导入模板DTO
/// </summary>
public class LeanRoleDepartmentImportTemplateDto
{
    /// <summary>
    /// 角色编码
    /// </summary>
    public string RoleCode { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    public string DepartmentCode { get; set; } = string.Empty;
} 
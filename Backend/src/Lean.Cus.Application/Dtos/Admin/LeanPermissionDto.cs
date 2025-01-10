using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 权限查询条件
/// </summary>
public class LeanPermissionQueryDto : PagedQuery
{
    /// <summary>
    /// 权限名称
    /// </summary>
    public string? PermissionName { get; set; }

    /// <summary>
    /// 权限编码
    /// </summary>
    public string? PermissionCode { get; set; }

    /// <summary>
    /// 菜单ID
    /// </summary>
    public long? MenuId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanStatus? Status { get; set; }

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
/// 权限DTO
/// </summary>
public class LeanPermissionDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    public string PermissionName { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    public string PermissionCode { get; set; } = string.Empty;

    /// <summary>
    /// 菜单ID
    /// </summary>
    public long MenuId { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// 权限类型（1：菜单，2：按钮，3：接口）
    /// </summary>
    public LeanPermissionType PermissionType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanStatus Status { get; set; }

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
/// 权限创建DTO
/// </summary>
public class LeanPermissionCreateDto
{
    /// <summary>
    /// 权限名称
    /// </summary>
    [Required(ErrorMessage = "权限名称不能为空")]
    [StringLength(50, ErrorMessage = "权限名称长度不能超过50个字符")]
    public string PermissionName { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    [Required(ErrorMessage = "权限编码不能为空")]
    [StringLength(50, ErrorMessage = "权限编码长度不能超过50个字符")]
    public string PermissionCode { get; set; } = string.Empty;

    /// <summary>
    /// 菜单ID
    /// </summary>
    [Required(ErrorMessage = "菜单ID不能为空")]
    public long MenuId { get; set; }

    /// <summary>
    /// 权限类型
    /// </summary>
    [Required(ErrorMessage = "权限类型不能为空")]
    public LeanPermissionType PermissionType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Required(ErrorMessage = "排序不能为空")]
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public LeanStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }
}

/// <summary>
/// 权限更新DTO
/// </summary>
public class LeanPermissionUpdateDto : LeanPermissionCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "权限ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 权限状态更新DTO
/// </summary>
public class LeanPermissionStatusUpdateDto
{
    /// <summary>
    /// 权限ID
    /// </summary>
    [Required(ErrorMessage = "权限ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public LeanStatus Status { get; set; }
}

/// <summary>
/// 权限导入DTO
/// </summary>
public class LeanPermissionImportDto
{
    /// <summary>
    /// 权限名称
    /// </summary>
    [Required(ErrorMessage = "权限名称不能为空")]
    [StringLength(50, ErrorMessage = "权限名称长度不能超过50个字符")]
    public string PermissionName { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    [Required(ErrorMessage = "权限编码不能为空")]
    [StringLength(50, ErrorMessage = "权限编码长度不能超过50个字符")]
    public string PermissionCode { get; set; } = string.Empty;

    /// <summary>
    /// 菜单ID
    /// </summary>
    [Required(ErrorMessage = "菜单ID不能为空")]
    public long MenuId { get; set; }

    /// <summary>
    /// 权限类型
    /// </summary>
    [Required(ErrorMessage = "权限类型不能为空")]
    public LeanPermissionType PermissionType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Required(ErrorMessage = "排序不能为空")]
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public LeanStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }
}

/// <summary>
/// 权限导出DTO
/// </summary>
public class LeanPermissionExportDto
{
    /// <summary>
    /// 权限名称
    /// </summary>
    public string PermissionName { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    public string PermissionCode { get; set; } = string.Empty;

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// 权限类型名称
    /// </summary>
    public string PermissionTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态名称
    /// </summary>
    public string StatusName { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 权限导入模板DTO
/// </summary>
public class LeanPermissionImportTemplateDto
{
    /// <summary>
    /// 权限名称
    /// </summary>
    public string PermissionName { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    public string PermissionCode { get; set; } = string.Empty;

    /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; } = string.Empty;

    /// <summary>
    /// 权限类型（1：菜单，2：按钮，3：接口）
    /// </summary>
    public LeanPermissionType PermissionType { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态(0：禁用，1：启用)
    /// </summary>
    public LeanStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
} 
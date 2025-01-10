using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;
using System.Collections.Generic;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 菜单查询条件
/// </summary>
public class LeanMenuQueryDto : PagedQuery
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    public string? MenuName { get; set; }

    /// <summary>
    /// 菜单编码
    /// </summary>
    public string? MenuCode { get; set; }

    /// <summary>
    /// 菜单类型
    /// </summary>
    public LeanMenuType? MenuType { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    public string? PermissionCode { get; set; }

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
/// 菜单DTO
/// </summary>
public class LeanMenuDto
{
    /// <summary>
    /// 菜单ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 菜单名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 菜单编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 菜单图标
    /// </summary>
    public string Icon { get; set; }

    /// <summary>
    /// 菜单路由
    /// </summary>
    public string Route { get; set; }

    /// <summary>
    /// 菜单组件
    /// </summary>
    public string Component { get; set; }

    /// <summary>
    /// 父级菜单ID
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    public bool IsVisible { get; set; }

    /// <summary>
    /// 是否外链
    /// </summary>
    public bool IsExternal { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 子菜单列表
    /// </summary>
    public List<LeanMenuDto> Children { get; set; } = new List<LeanMenuDto>();
}

/// <summary>
/// 菜单创建DTO
/// </summary>
public class LeanMenuCreateDto
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    [Required(ErrorMessage = "菜单名称不能为空")]
    [StringLength(50, ErrorMessage = "菜单名称长度不能超过50个字符")]
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// 菜单编码
    /// </summary>
    [Required(ErrorMessage = "菜单编码不能为空")]
    [StringLength(50, ErrorMessage = "菜单编码长度不能超过50个字符")]
    public string MenuCode { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID
    /// </summary>
    [Required(ErrorMessage = "父级ID不能为空")]
    public long ParentId { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    [StringLength(200, ErrorMessage = "路由地址长度不能超过200个字符")]
    public string? Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [StringLength(200, ErrorMessage = "组件路径长度不能超过200个字符")]
    public string? Component { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    [StringLength(200, ErrorMessage = "重定向地址长度不能超过200个字符")]
    public string? Redirect { get; set; }

    /// <summary>
    /// 菜单类型
    /// </summary>
    [Required(ErrorMessage = "菜单类型不能为空")]
    public LeanMenuType MenuType { get; set; }

    /// <summary>
    /// 菜单图标
    /// </summary>
    [StringLength(100, ErrorMessage = "菜单图标长度不能超过100个字符")]
    public string? Icon { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    [StringLength(100, ErrorMessage = "权限标识长度不能超过100个字符")]
    public string? PermissionCode { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Required(ErrorMessage = "排序不能为空")]
    public int Sort { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    [Required(ErrorMessage = "是否可见不能为空")]
    public bool Visible { get; set; } = true;

    /// <summary>
    /// 是否缓存
    /// </summary>
    [Required(ErrorMessage = "是否缓存不能为空")]
    public bool KeepAlive { get; set; } = true;

    /// <summary>
    /// 是否外链
    /// </summary>
    [Required(ErrorMessage = "是否外链不能为空")]
    public bool IsFrame { get; set; }

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

    /// <summary>
    /// 翻译键
    /// </summary>
    [StringLength(100, ErrorMessage = "翻译键长度不能超过100个字符")]
    public string? TransKey { get; set; }
}

/// <summary>
/// 菜单更新DTO
/// </summary>
public class LeanMenuUpdateDto : LeanMenuCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "菜单ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 菜单状态更新DTO
/// </summary>
public class LeanMenuStatusUpdateDto
{
    /// <summary>
    /// 菜单ID
    /// </summary>
    [Required(ErrorMessage = "菜单ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public LeanStatus Status { get; set; }
}

/// <summary>
/// 菜单导入DTO
/// </summary>
public class LeanMenuImportDto
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    [Required(ErrorMessage = "菜单名称不能为空")]
    [StringLength(50, ErrorMessage = "菜单名称长度不能超过50个字符")]
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// 菜单编码
    /// </summary>
    [Required(ErrorMessage = "菜单编码不能为空")]
    [StringLength(50, ErrorMessage = "菜单编码长度不能超过50个字符")]
    public string MenuCode { get; set; } = string.Empty;

    /// <summary>
    /// 父级编码
    /// </summary>
    [Required(ErrorMessage = "父级编码不能为空")]
    [StringLength(50, ErrorMessage = "父级编码长度不能超过50个字符")]
    public string ParentCode { get; set; } = string.Empty;

    /// <summary>
    /// 路由地址
    /// </summary>
    [StringLength(200, ErrorMessage = "路由地址长度不能超过200个字符")]
    public string? Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [StringLength(200, ErrorMessage = "组件路径长度不能超过200个字符")]
    public string? Component { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    [StringLength(200, ErrorMessage = "重定向地址长度不能超过200个字符")]
    public string? Redirect { get; set; }

    /// <summary>
    /// 菜单类型
    /// </summary>
    [Required(ErrorMessage = "菜单类型不能为空")]
    public LeanMenuType MenuType { get; set; }

    /// <summary>
    /// 菜单图标
    /// </summary>
    [StringLength(100, ErrorMessage = "菜单图标长度不能超过100个字符")]
    public string? Icon { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    [StringLength(100, ErrorMessage = "权限标识长度不能超过100个字符")]
    public string? PermissionCode { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Required(ErrorMessage = "排序不能为空")]
    public int Sort { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    [Required(ErrorMessage = "是否可见不能为空")]
    public bool Visible { get; set; } = true;

    /// <summary>
    /// 是否缓存
    /// </summary>
    [Required(ErrorMessage = "是否缓存不能为空")]
    public bool KeepAlive { get; set; } = true;

    /// <summary>
    /// 是否外链
    /// </summary>
    [Required(ErrorMessage = "是否外链不能为空")]
    public bool IsFrame { get; set; }

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

    /// <summary>
    /// 翻译键
    /// </summary>
    [StringLength(100, ErrorMessage = "翻译键长度不能超过100个字符")]
    public string? TransKey { get; set; }
}

/// <summary>
/// 菜单导出DTO
/// </summary>
public class LeanMenuExportDto
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; } = string.Empty;

    /// <summary>
    /// 父级名称
    /// </summary>
    public string ParentName { get; set; } = string.Empty;

    /// <summary>
    /// 路由地址
    /// </summary>
    public string? Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    public string? Component { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    public string? Redirect { get; set; }

    /// <summary>
    /// 菜单类型名称
    /// </summary>
    public string MenuTypeName { get; set; } = string.Empty;

    /// <summary>
    /// 菜单图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    public string? PermissionCode { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 是否可见
    /// </summary>
    public string VisibleName { get; set; } = string.Empty;

    /// <summary>
    /// 是否缓存
    /// </summary>
    public string KeepAliveName { get; set; } = string.Empty;

    /// <summary>
    /// 是否外链
    /// </summary>
    public string IsFrameName { get; set; } = string.Empty;

    /// <summary>
    /// 状态名称
    /// </summary>
    public string StatusName { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 翻译键
    /// </summary>
    public string? TransKey { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 菜单导入模板DTO
/// </summary>
public class LeanMenuImportTemplateDto
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    public string MenuName { get; set; } = "示例菜单";

    /// <summary>
    /// 菜单编码
    /// </summary>
    public string MenuCode { get; set; } = "example_menu";

    /// <summary>
    /// 父级编码
    /// </summary>
    public string ParentCode { get; set; } = "parent_menu";

    /// <summary>
    /// 路由地址
    /// </summary>
    public string Path { get; set; } = "/example/path";

    /// <summary>
    /// 组件路径
    /// </summary>
    public string Component { get; set; } = "example/Example";

    /// <summary>
    /// 重定向地址
    /// </summary>
    public string Redirect { get; set; } = "/redirect/path";

    /// <summary>
    /// 菜单类型（1：目录，2：菜单，3：按钮）
    /// </summary>
    public string MenuType { get; set; } = "1";

    /// <summary>
    /// 菜单图标
    /// </summary>
    public string Icon { get; set; } = "example-icon";

    /// <summary>
    /// 权限标识
    /// </summary>
    public string PermissionCode { get; set; } = "example:menu:list";

    /// <summary>
    /// 排序
    /// </summary>
    public string Sort { get; set; } = "1";

    /// <summary>
    /// 是否可见（true/false）
    /// </summary>
    public string Visible { get; set; } = "true";

    /// <summary>
    /// 是否缓存（true/false）
    /// </summary>
    public string KeepAlive { get; set; } = "true";

    /// <summary>
    /// 是否外链（true/false）
    /// </summary>
    public string IsFrame { get; set; } = "false";

    /// <summary>
    /// 状态（0：禁用，1：启用）
    /// </summary>
    public string Status { get; set; } = "1";

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; } = "示例备注";

    /// <summary>
    /// 翻译键
    /// </summary>
    public string TransKey { get; set; } = "menu.example";
} 
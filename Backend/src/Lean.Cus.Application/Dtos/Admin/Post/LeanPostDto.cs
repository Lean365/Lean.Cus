using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 岗位DTO
/// </summary>
public class LeanPostDto
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 岗位编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    public List<long> UserIds { get; set; } = new();

    /// <summary>
    /// 用户名称列表
    /// </summary>
    public List<string> UserNames { get; set; } = new();
}

/// <summary>
/// 岗位查询DTO
/// </summary>
public class LeanPostQueryDto : LeanPagedDto
{
    /// <summary>
    /// 岗位名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 岗位编码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus? Status { get; set; }
}

/// <summary>
/// 岗位创建DTO
/// </summary>
public class LeanPostCreateDto
{
    /// <summary>
    /// 岗位名称
    /// </summary>
    [Required(ErrorMessage = "岗位名称不能为空")]
    [StringLength(50, ErrorMessage = "岗位名称长度不能超过50个字符")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 岗位编码
    /// </summary>
    [Required(ErrorMessage = "岗位编码不能为空")]
    [StringLength(50, ErrorMessage = "岗位编码长度不能超过50个字符")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; } = LeanEnabledStatus.Enabled;

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 用户ID列表
    /// </summary>
    public List<long> UserIds { get; set; } = new();
}

/// <summary>
/// 岗位更新DTO
/// </summary>
public class LeanPostUpdateDto : LeanPostCreateDto
{
    /// <summary>
    /// ID
    /// </summary>
    [Required(ErrorMessage = "ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 岗位导入DTO
/// </summary>
public class LeanPostImportDto
{
    /// <summary>
    /// 岗位名称
    /// </summary>
    [Required(ErrorMessage = "岗位名称不能为空")]
    [StringLength(50, ErrorMessage = "岗位名称长度不能超过50个字符")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 岗位编码
    /// </summary>
    [Required(ErrorMessage = "岗位编码不能为空")]
    [StringLength(50, ErrorMessage = "岗位编码长度不能超过50个字符")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; } = LeanEnabledStatus.Enabled;

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(500, ErrorMessage = "备注长度不能超过500个字符")]
    public string? Remark { get; set; }

    /// <summary>
    /// 用户名称列表，多个用户用逗号分隔
    /// </summary>
    public string? UserNames { get; set; }
}

/// <summary>
/// 岗位导出DTO
/// </summary>
public class LeanPostExportDto
{
    /// <summary>
    /// 岗位名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 岗位编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

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
    /// 用户名称列表，多个用户用逗号分隔
    /// </summary>
    public string? UserNames { get; set; }
}

/// <summary>
/// 岗位导入模板DTO
/// </summary>
public class LeanPostImportTemplateDto
{
    /// <summary>
    /// 岗位名称
    /// </summary>
    public string Name { get; set; } = "请输入岗位名称";

    /// <summary>
    /// 岗位编码
    /// </summary>
    public string Code { get; set; } = "请输入岗位编码";

    /// <summary>
    /// 排序
    /// </summary>
    public string Sort { get; set; } = "请输入排序值";

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; } = "请输入状态：启用、禁用";

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; } = "请输入备注";

    /// <summary>
    /// 用户名称列表
    /// </summary>
    public string UserNames { get; set; } = "请输入用户名称，多个用户用逗号分隔";
} 
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 部门DTO
/// </summary>
public class LeanDeptDto
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 负责人
    /// </summary>
    public string? Leader { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

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
    /// 父级部门名称
    /// </summary>
    public string? ParentName { get; set; }

    /// <summary>
    /// 子部门列表
    /// </summary>
    public List<LeanDeptDto> Children { get; set; } = new();
}

/// <summary>
/// 部门查询DTO
/// </summary>
public class LeanDeptQueryDto : LeanPagedDto
{
    /// <summary>
    /// 部门名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus? Status { get; set; }
}

/// <summary>
/// 部门创建DTO
/// </summary>
public class LeanDeptCreateDto
{
    /// <summary>
    /// 父级ID
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [Required(ErrorMessage = "部门名称不能为空")]
    [StringLength(50, ErrorMessage = "部门名称长度不能超过50个字符")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    [Required(ErrorMessage = "部门编码不能为空")]
    [StringLength(50, ErrorMessage = "部门编码长度不能超过50个字符")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 负责人
    /// </summary>
    [StringLength(50, ErrorMessage = "负责人长度不能超过50个字符")]
    public string? Leader { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [StringLength(20, ErrorMessage = "联系电话长度不能超过20个字符")]
    [Phone(ErrorMessage = "联系电话格式不正确")]
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [StringLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string? Email { get; set; }

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
}

/// <summary>
/// 部门更新DTO
/// </summary>
public class LeanDeptUpdateDto : LeanDeptCreateDto
{
    /// <summary>
    /// ID
    /// </summary>
    [Required(ErrorMessage = "ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 部门导入DTO
/// </summary>
public class LeanDeptImportDto
{
    /// <summary>
    /// 父级部门名称
    /// </summary>
    public string? ParentName { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    [Required(ErrorMessage = "部门名称不能为空")]
    [StringLength(50, ErrorMessage = "部门名称长度不能超过50个字符")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    [Required(ErrorMessage = "部门编码不能为空")]
    [StringLength(50, ErrorMessage = "部门编码长度不能超过50个字符")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 负责人
    /// </summary>
    [StringLength(50, ErrorMessage = "负责人长度不能超过50个字符")]
    public string? Leader { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [StringLength(20, ErrorMessage = "联系电话长度不能超过20个字符")]
    [Phone(ErrorMessage = "联系电话格式不正确")]
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [StringLength(50, ErrorMessage = "邮箱长度不能超过50个字符")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string? Email { get; set; }

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
}

/// <summary>
/// 部门导出DTO
/// </summary>
public class LeanDeptExportDto
{
    /// <summary>
    /// 部门名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 父级部门名称
    /// </summary>
    public string? ParentName { get; set; }

    /// <summary>
    /// 负责人
    /// </summary>
    public string? Leader { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

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
}

/// <summary>
/// 部门导入模板DTO
/// </summary>
public class LeanDeptImportTemplateDto
{
    /// <summary>
    /// 父级部门名称
    /// </summary>
    public string ParentName { get; set; } = "请输入父级部门名称";

    /// <summary>
    /// 部门名称
    /// </summary>
    public string Name { get; set; } = "请输入部门名称";

    /// <summary>
    /// 部门编码
    /// </summary>
    public string Code { get; set; } = "请输入部门编码";

    /// <summary>
    /// 负责人
    /// </summary>
    public string Leader { get; set; } = "请输入负责人";

    /// <summary>
    /// 联系电话
    /// </summary>
    public string Phone { get; set; } = "请输入联系电话";

    /// <summary>
    /// 邮箱
    /// </summary>
    public string Email { get; set; } = "请输入邮箱";

    /// <summary>
    /// 排序
    /// </summary>
    public string Sort { get; set; } = "请输入排序值";

    /// <summary>
    /// 状态
    /// </summary>
    public string Status { get; set; } = "请选择状态(0:禁用,1:启用)";

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; } = "请输入备注";
} 
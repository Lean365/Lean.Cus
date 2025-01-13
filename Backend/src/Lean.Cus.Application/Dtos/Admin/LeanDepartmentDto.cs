#nullable enable

using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 部门查询条件
/// </summary>
public class LeanDepartmentQueryDto : PagedQuery
{
    /// <summary>
    /// 部门名称
    /// </summary>
    public string? DepartmentName { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string? DepartmentCode { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    public long? ParentId { get; set; }

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
/// 部门DTO
/// </summary>
public class LeanDepartmentDto
{
    /// <summary>
    /// 部门ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    public string DepartmentName { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    public string DepartmentCode { get; set; }

    /// <summary>
    /// 父级部门ID
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 子部门列表
    /// </summary>
    public List<LeanDepartmentDto> Children { get; set; } = new List<LeanDepartmentDto>();
}

/// <summary>
/// 部门创建DTO
/// </summary>
public class LeanDepartmentCreateDto
{
    /// <summary>
    /// 部门名称
    /// </summary>
    [Required(ErrorMessage = "部门名称不能为空")]
    [StringLength(50, ErrorMessage = "部门名称长度不能超过50个字符")]
    public string DepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    [Required(ErrorMessage = "部门编码不能为空")]
    [StringLength(50, ErrorMessage = "部门编码长度不能超过50个字符")]
    public string DepartmentCode { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID
    /// </summary>
    [Required(ErrorMessage = "父级ID不能为空")]
    public long ParentId { get; set; }

    /// <summary>
    /// 负责人ID
    /// </summary>
    public long? LeaderId { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [StringLength(20, ErrorMessage = "联系电话长度不能超过20个字符")]
    [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "联系电话格式不正确")]
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [StringLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string? Email { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required(ErrorMessage = "排序号不能为空")]
    public int OrderNum { get; set; }

    /// <summary>
    /// 状态(0：禁用，1：启用)
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
/// 部门更新DTO
/// </summary>
public class LeanDepartmentUpdateDto : LeanDepartmentCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "部门ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 部门状态更新DTO
/// </summary>
public class LeanDepartmentStatusUpdateDto
{
    /// <summary>
    /// 部门ID
    /// </summary>
    [Required(ErrorMessage = "部门ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public LeanStatus Status { get; set; }
}

/// <summary>
/// 部门导入DTO
/// </summary>
public class LeanDepartmentImportDto
{
    /// <summary>
    /// 部门名称
    /// </summary>
    [Required(ErrorMessage = "部门名称不能为空")]
    [StringLength(50, ErrorMessage = "部门名称长度不能超过50个字符")]
    public string DepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    [Required(ErrorMessage = "部门编码不能为空")]
    [StringLength(50, ErrorMessage = "部门编码长度不能超过50个字符")]
    public string DepartmentCode { get; set; } = string.Empty;

    /// <summary>
    /// 父级编码
    /// </summary>
    [Required(ErrorMessage = "父级编码不能为空")]
    [StringLength(50, ErrorMessage = "父级编码长度不能超过50个字符")]
    public string ParentCode { get; set; } = string.Empty;

    /// <summary>
    /// 负责人工号
    /// </summary>
    [StringLength(50, ErrorMessage = "负责人工号长度不能超过50个字符")]
    public string? LeaderCode { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [StringLength(20, ErrorMessage = "联系电话长度不能超过20个字符")]
    [RegularExpression(@"^1[3-9]\d{9}$", ErrorMessage = "联系电话格式不正确")]
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [StringLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
    [EmailAddress(ErrorMessage = "邮箱格式不正确")]
    public string? Email { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [Required(ErrorMessage = "排序号不能为空")]
    public int OrderNum { get; set; }

    /// <summary>
    /// 状态(0：禁用，1：启用)
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
/// 部门导出DTO
/// </summary>
public class LeanDepartmentExportDto
{
    /// <summary>
    /// 部门名称
    /// </summary>
    public string DepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    public string DepartmentCode { get; set; } = string.Empty;

    /// <summary>
    /// 父级名称
    /// </summary>
    public string ParentName { get; set; } = string.Empty;

    /// <summary>
    /// 负责人名称
    /// </summary>
    public string? LeaderName { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

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
/// 部门导入模板DTO
/// </summary>
public class LeanDepartmentImportTemplateDto
{
    /// <summary>
    /// 部门名称
    /// </summary>
    public string DepartmentName { get; set; } = string.Empty;

    /// <summary>
    /// 部门编码
    /// </summary>
    public string DepartmentCode { get; set; } = string.Empty;

    /// <summary>
    /// 父级编码
    /// </summary>
    public string ParentCode { get; set; } = string.Empty;

    /// <summary>
    /// 负责人工号
    /// </summary>
    public string? LeaderCode { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    public int OrderNum { get; set; }

    /// <summary>
    /// 状态(0：禁用，1：启用)
    /// </summary>
    public LeanStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}
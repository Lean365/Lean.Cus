using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 职位查询条件
/// </summary>
public class LeanPositionQueryDto : PagedQuery
{
    /// <summary>
    /// 职位名称
    /// </summary>
    public string? PositionName { get; set; }

    /// <summary>
    /// 职位编码
    /// </summary>
    public string? PositionCode { get; set; }

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
/// 职位DTO
/// </summary>
public class LeanPositionDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 职位名称
    /// </summary>
    public string PositionName { get; set; } = string.Empty;

    /// <summary>
    /// 职位编码
    /// </summary>
    public string PositionCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位类型（1：高层，2：中层，3：基层）
    /// </summary>
    public LeanPositionType PositionType { get; set; }

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
/// 职位创建DTO
/// </summary>
public class LeanPositionCreateDto
{
    /// <summary>
    /// 职位名称
    /// </summary>
    [Required(ErrorMessage = "职位名称不能为空")]
    [StringLength(50, ErrorMessage = "职位名称长度不能超过50个字符")]
    public string PositionName { get; set; } = string.Empty;

    /// <summary>
    /// 职位编码
    /// </summary>
    [Required(ErrorMessage = "职位编码不能为空")]
    [StringLength(50, ErrorMessage = "职位编码长度不能超过50个字符")]
    public string PositionCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位类型
    /// </summary>
    [Required(ErrorMessage = "职位类型不能为空")]
    public LeanPositionType PositionType { get; set; }

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
/// 职位更新DTO
/// </summary>
public class LeanPositionUpdateDto : LeanPositionCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "职位ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 职位状态更新DTO
/// </summary>
public class LeanPositionStatusUpdateDto
{
    /// <summary>
    /// 职位ID
    /// </summary>
    [Required(ErrorMessage = "职位ID不能为空")]
    public long Id { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Required(ErrorMessage = "状态不能为空")]
    public LeanStatus Status { get; set; }
}

/// <summary>
/// 职位导入DTO
/// </summary>
public class LeanPositionImportDto
{
    /// <summary>
    /// 职位名称
    /// </summary>
    [Required(ErrorMessage = "职位名称不能为空")]
    [StringLength(50, ErrorMessage = "职位名称长度不能超过50个字符")]
    public string PositionName { get; set; } = string.Empty;

    /// <summary>
    /// 职位编码
    /// </summary>
    [Required(ErrorMessage = "职位编码不能为空")]
    [StringLength(50, ErrorMessage = "职位编码长度不能超过50个字符")]
    public string PositionCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位类型
    /// </summary>
    [Required(ErrorMessage = "职位类型不能为空")]
    public LeanPositionType PositionType { get; set; }

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
/// 职位导出DTO
/// </summary>
public class LeanPositionExportDto
{
    /// <summary>
    /// 职位名称
    /// </summary>
    public string PositionName { get; set; } = string.Empty;

    /// <summary>
    /// 职位编码
    /// </summary>
    public string PositionCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位类型名称
    /// </summary>
    public string PositionTypeName { get; set; } = string.Empty;

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
/// 职位导入模板DTO
/// </summary>
public class LeanPositionImportTemplateDto
{
    /// <summary>
    /// 职位名称
    /// </summary>
    public string PositionName { get; set; } = string.Empty;

    /// <summary>
    /// 职位编码
    /// </summary>
    public string PositionCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位类型（1：高层，2：中层，3：基层）
    /// </summary>
    public LeanPositionType PositionType { get; set; }

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
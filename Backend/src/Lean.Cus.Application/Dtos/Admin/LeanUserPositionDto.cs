using System;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 用户职位关联查询条件
/// </summary>
public class LeanUserPositionQueryDto : PagedQuery
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
    /// 职位ID
    /// </summary>
    public long? PositionId { get; set; }

    /// <summary>
    /// 职位名称
    /// </summary>
    public string? PositionName { get; set; }

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
/// 用户职位关联DTO
/// </summary>
public class LeanUserPositionDto
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
    /// 职位ID
    /// </summary>
    public long PositionId { get; set; }

    /// <summary>
    /// 职位名称
    /// </summary>
    public string PositionName { get; set; } = string.Empty;

    /// <summary>
    /// 是否主职位
    /// </summary>
    public bool IsPrimary { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 用户职位关联创建DTO
/// </summary>
public class LeanUserPositionCreateDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long UserId { get; set; }

    /// <summary>
    /// 职位ID
    /// </summary>
    [Required(ErrorMessage = "职位ID不能为空")]
    public long PositionId { get; set; }

    /// <summary>
    /// 是否主职位
    /// </summary>
    [Required(ErrorMessage = "是否主职位不能为空")]
    public bool IsPrimary { get; set; }
}

/// <summary>
/// 用户职位关联更新DTO
/// </summary>
public class LeanUserPositionUpdateDto : LeanUserPositionCreateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    [Required(ErrorMessage = "用户职位关联ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 用户职位关联导入DTO
/// </summary>
public class LeanUserPositionImportDto
{
    /// <summary>
    /// 用户工号
    /// </summary>
    [Required(ErrorMessage = "用户工号不能为空")]
    [StringLength(50, ErrorMessage = "用户工号长度不能超过50个字符")]
    public string UserCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位编码
    /// </summary>
    [Required(ErrorMessage = "职位编码不能为空")]
    [StringLength(50, ErrorMessage = "职位编码长度不能超过50个字符")]
    public string PositionCode { get; set; } = string.Empty;

    /// <summary>
    /// 是否主职位
    /// </summary>
    [Required(ErrorMessage = "是否主职位不能为空")]
    public bool IsPrimary { get; set; }
}

/// <summary>
/// 用户职位关联导出DTO
/// </summary>
public class LeanUserPositionExportDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 职位名称
    /// </summary>
    public string PositionName { get; set; } = string.Empty;

    /// <summary>
    /// 是否主职位
    /// </summary>
    public string IsPrimaryName { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }
}

/// <summary>
/// 用户职位关联导入模板DTO
/// </summary>
public class LeanUserPositionImportTemplateDto
{
    /// <summary>
    /// 用户工号
    /// </summary>
    public string UserCode { get; set; } = string.Empty;

    /// <summary>
    /// 职位编码
    /// </summary>
    public string PositionCode { get; set; } = string.Empty;

    /// <summary>
    /// 是否主职位
    /// </summary>
    public bool IsPrimary { get; set; }
} 
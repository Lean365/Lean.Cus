using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 用户岗位关联DTO
/// </summary>
public class LeanUserPostDto
{
    /// <summary>
    /// ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    public long UserId { get; set; }

    /// <summary>
    /// 岗位ID
    /// </summary>
    public long PostId { get; set; }

    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; } = string.Empty;
}

/// <summary>
/// 用户岗位关联查询DTO
/// </summary>
public class LeanUserPostQueryDto : LeanPagedDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long? UserId { get; set; }

    /// <summary>
    /// 岗位ID
    /// </summary>
    public long? PostId { get; set; }
}

/// <summary>
/// 用户岗位关联创建DTO
/// </summary>
public class LeanUserPostCreateDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [Required(ErrorMessage = "用户ID不能为空")]
    public long UserId { get; set; }

    /// <summary>
    /// 岗位ID
    /// </summary>
    [Required(ErrorMessage = "岗位ID不能为空")]
    public long PostId { get; set; }
}

/// <summary>
/// 用户岗位关联更新DTO
/// </summary>
public class LeanUserPostUpdateDto : LeanUserPostCreateDto
{
    /// <summary>
    /// ID
    /// </summary>
    [Required(ErrorMessage = "ID不能为空")]
    public long Id { get; set; }
}

/// <summary>
/// 用户岗位关联导入DTO
/// </summary>
public class LeanUserPostImportDto
{
    /// <summary>
    /// 用户名称
    /// </summary>
    [Required(ErrorMessage = "用户名称不能为空")]
    [StringLength(50, ErrorMessage = "用户名称长度不能超过50个字符")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    [Required(ErrorMessage = "岗位名称不能为空")]
    [StringLength(50, ErrorMessage = "岗位名称长度不能超过50个字符")]
    public string PostName { get; set; } = string.Empty;
}

/// <summary>
/// 用户岗位关联导出DTO
/// </summary>
public class LeanUserPostExportDto
{
    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; } = string.Empty;
}

/// <summary>
/// 用户岗位关联导入模板DTO
/// </summary>
public class LeanUserPostImportTemplateDto
{
    /// <summary>
    /// 用户名称
    /// </summary>
    public string UserName { get; set; } = "请输入用户名称";

    /// <summary>
    /// 岗位名称
    /// </summary>
    public string PostName { get; set; } = "请输入岗位名称";
} 
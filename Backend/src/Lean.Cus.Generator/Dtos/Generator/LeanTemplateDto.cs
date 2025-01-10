using System.ComponentModel.DataAnnotations;

namespace Lean.Cus.Generator.Dtos.Generator;

/// <summary>
/// 模板DTO
/// </summary>
public class LeanTemplateDto
{
    /// <summary>
    /// 编号
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 模板类型
    /// </summary>
    [Required(ErrorMessage = "模板类型不能为空")]
    public string TemplateType { get; set; } = string.Empty;

    /// <summary>
    /// 文件名
    /// </summary>
    [Required(ErrorMessage = "文件名不能为空")]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 模板内容
    /// </summary>
    [Required(ErrorMessage = "模板内容不能为空")]
    public string TemplateContent { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
} 
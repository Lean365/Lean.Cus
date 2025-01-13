using SqlSugar;
using Lean.Cus.Domain.Entities;

namespace Lean.Cus.Generator.Entities.Generator;

/// <summary>
/// 模板实体
/// </summary>
[SugarTable("lean_gen_template", "代码生成模板表")]
public class LeanTemplate : LeanBaseEntity
{
    /// <summary>
    /// 模板类型
    /// </summary>
    [SugarColumn(ColumnName = "template_type", ColumnDescription = "模板类型", 
        IsNullable = false, Length = 50, ColumnDataType = "nvarchar")]
    public string TemplateType { get; set; } = string.Empty;

    /// <summary>
    /// 文件名
    /// </summary>
    [SugarColumn(ColumnName = "file_name", ColumnDescription = "文件名", 
        IsNullable = false, Length = 200, ColumnDataType = "nvarchar")]
    public string FileName { get; set; } = string.Empty;

    /// <summary>
    /// 模板内容
    /// </summary>
    [SugarColumn(ColumnName = "template_content", ColumnDescription = "模板内容", 
        IsNullable = false, ColumnDataType = "text")]
    public string TemplateContent { get; set; } = string.Empty;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", 
        IsNullable = true, Length = 500, ColumnDataType = "nvarchar")]
    public string? Remark { get; set; }
}
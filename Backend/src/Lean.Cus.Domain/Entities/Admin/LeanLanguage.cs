using Lean.Cus.Common.Entities;
using Lean.Cus.Common.Enums;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 语言实体
/// </summary>
/// <remarks>
/// 语言用于存储系统支持的语言信息。
/// 每种语言都有唯一的编码(Code)，用于标识语言。
/// 语言可以启用或禁用，禁用的语言在系统中不可选择。
/// 语言可以设置排序，影响在界面上的显示顺序。
/// 每种语言都可以设置图标，用于在界面上展示。
/// </remarks>
[SugarTable("lean_language", "语言表")]
[SugarIndex("idx_language_code", nameof(Code), OrderByType.Asc, true)]
public class LeanLanguage : LeanEntity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanLanguage()
    {
        Name = string.Empty;
        Code = string.Empty;
    }

    /// <summary>
    /// 语言名称
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：语言的显示名称，如：简体中文、English
    /// 建议使用该语言的本地化名称
    /// </remarks>
    [SugarColumn(ColumnName = "name", ColumnDescription = "语言名称", Length = 50, IsNullable = false, ColumnDataType = "varchar")]
    public string Name { get; set; }

    /// <summary>
    /// 语言编码
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：10
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：语言的标准编码，如：zh-CN、en-US
    /// 建议使用ISO 639-1语言代码
    /// 已建立唯一索引：idx_language_code
    /// </remarks>
    [SugarColumn(ColumnName = "code", ColumnDescription = "语言编码", Length = 10, IsNullable = false, ColumnDataType = "varchar")]
    public string Code { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：100
    /// 允许为空：是
    /// 默认值：null
    /// 说明：语言的图标URL或图标类名
    /// 支持图片URL或Font Awesome类名
    /// </remarks>
    [SugarColumn(ColumnName = "icon", ColumnDescription = "图标", Length = 100, IsNullable = true, ColumnDataType = "varchar")]
    public string? Icon { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：0
    /// 说明：显示顺序，值越小越靠前
    /// 默认按此字段排序
    /// </remarks>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", IsNullable = false, ColumnDataType = "int")]
    public int OrderNum { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：1
    /// 说明：语言状态：启用(1)、禁用(0)
    /// 禁用的语言在系统中不可选择
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, ColumnDataType = "int")]
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：500
    /// 允许为空：是
    /// 默认值：null
    /// 说明：语言的补充说明信息
    /// 最多500个字符
    /// </remarks>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, IsNullable = true, ColumnDataType = "varchar")]
    public string? Remark { get; set; }
} 
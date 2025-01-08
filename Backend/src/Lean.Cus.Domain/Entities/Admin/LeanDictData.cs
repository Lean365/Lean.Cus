using Lean.Cus.Common.Entities;
using Lean.Cus.Common.Enums;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 字典数据实体
/// </summary>
/// <remarks>
/// 字典数据用于存储系统中各种枚举值和固定选项。
/// 每个字典数据都属于一个字典类型，用于对数据进行分类。
/// 字典数据包含标签和值，标签用于显示，值用于存储。
/// 字典数据可以设置排序，影响在界面上的显示顺序。
/// 字典数据可以启用或禁用，禁用的数据在系统中不可选择。
/// 字典数据可以设置样式类型，用于在界面上展示不同的样式。
/// </remarks>
[SugarTable("lean_dict_data", "字典数据表")]
[SugarIndex("idx_dict_data_type", nameof(DictTypeId), OrderByType.Asc)]
[SugarIndex("idx_dict_data_value", nameof(Value), OrderByType.Asc)]
public class LeanDictData : LeanEntity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanDictData()
    {
        Label = string.Empty;
        Value = string.Empty;
    }

    /// <summary>
    /// 字典类型ID
    /// </summary>
    /// <remarks>
    /// 数据类型：长整型（bigint）
    /// 允许为空：否
    /// 默认值：无
    /// 说明：关联的字典类型ID，必须是字典类型表中存在的ID
    /// 已建立索引：idx_dict_data_type
    /// </remarks>
    [SugarColumn(ColumnName = "dict_type_id", ColumnDescription = "字典类型ID", IsNullable = false, ColumnDataType = "bigint")]
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典标签
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：100
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：字典数据的显示名称，用于界面展示
    /// 建议使用易于理解的名称
    /// </remarks>
    [SugarColumn(ColumnName = "label", ColumnDescription = "字典标签", Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string Label { get; set; }

    /// <summary>
    /// 字典值
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：100
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：字典数据的实际值，用于存储和比较
    /// 同一字典类型下不允许重复
    /// 已建立索引：idx_dict_data_value
    /// </remarks>
    [SugarColumn(ColumnName = "value", ColumnDescription = "字典值", Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string Value { get; set; }

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
    /// 说明：字典数据状态：启用(1)、禁用(0)
    /// 禁用的字典数据在系统中不可选择
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, ColumnDataType = "int")]
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 样式类型
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：100
    /// 允许为空：是
    /// 默认值：null
    /// 说明：字典数据的样式类名，用于界面展示
    /// 支持CSS类名或预定义的样式类型
    /// </remarks>
    [SugarColumn(ColumnName = "css_class", ColumnDescription = "样式类型", Length = 100, IsNullable = true, ColumnDataType = "varchar")]
    public string? CssClass { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：500
    /// 允许为空：是
    /// 默认值：null
    /// 说明：字典数据的补充说明信息
    /// 最多500个字符
    /// </remarks>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, IsNullable = true, ColumnDataType = "varchar")]
    public string? Remark { get; set; }

    /// <summary>
    /// 字典类型
    /// </summary>
    /// <remarks>
    /// 导航属性：关联的字典类型信息
    /// 关联类型：一对一
    /// 关联字段：DictTypeId
    /// 说明：通过DictTypeId关联到字典类型表
    /// </remarks>
    [Navigate(NavigateType.OneToOne, nameof(DictTypeId))]
    public LeanDictType? DictType { get; set; }
} 
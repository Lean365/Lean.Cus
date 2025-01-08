using Lean.Cus.Common.Entities;
using Lean.Cus.Common.Enums;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 字典类型实体
/// </summary>
/// <remarks>
/// 字典类型用于对字典数据进行分类管理。
/// 每个字典类型包含多个字典数据。
/// 字典类型有唯一的编码(Code)，用于标识字典类型。
/// 字典类型可以启用或禁用，禁用的字典类型及其数据在系统中不可用。
/// </remarks>
[SugarTable("lean_dict_type", "字典类型表")]
[SugarIndex("idx_dict_type_code", nameof(Code), OrderByType.Asc, true)]
public class LeanDictType : LeanEntity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanDictType()
    {
        Name = string.Empty;
        Code = string.Empty;
    }

    /// <summary>
    /// 字典名称
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：100
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：字典类型的显示名称，用于界面展示
    /// 建议使用易于理解的名称
    /// </remarks>
    [SugarColumn(ColumnName = "name", ColumnDescription = "字典名称", Length = 100, IsNullable = false)]
    public string Name { get; set; }

    /// <summary>
    /// 字典编码
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：100
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：字典类型的唯一标识，系统中不允许重复
    /// 建议使用有意义的编码，如：sys_user_sex
    /// 已建立唯一索引：idx_dict_type_code
    /// </remarks>
    [SugarColumn(ColumnName = "code", ColumnDescription = "字典编码", Length = 100, IsNullable = false)]
    public string Code { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：1
    /// 说明：字典类型状态：启用(1)、禁用(0)
    /// 禁用后，该类型下的所有字典数据都不可用
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false)]
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：500
    /// 允许为空：是
    /// 默认值：null
    /// 说明：字典类型的补充说明信息
    /// 最多500个字符
    /// </remarks>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, IsNullable = true)]
    public string? Remark { get; set; }
} 
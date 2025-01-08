using Lean.Cus.Common.Entities;
using Lean.Cus.Common.Enums;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 配置实体
/// </summary>
/// <remarks>
/// 系统配置用于存储应用程序的各种配置项。
/// 配置项包括系统参数、业务参数等。
/// 每个配置项都有唯一的键(Key)，用于标识配置项。
/// 配置项可以按类型分类，便于管理。
/// 配置项可以启用或禁用，禁用的配置项在系统中不生效。
/// </remarks>
[SugarTable("lean_config", "配置表")]
[SugarIndex("idx_config_key", nameof(Key), OrderByType.Asc, true)]
public class LeanConfig : LeanEntity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanConfig()
    {
        Name = string.Empty;
        Key = string.Empty;
        Value = string.Empty;
    }

    /// <summary>
    /// 配置名称
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：100
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：配置项的显示名称，用于界面展示
    /// 建议使用易于理解的名称
    /// </remarks>
    [SugarColumn(ColumnName = "name", ColumnDescription = "配置名称", Length = 100, IsNullable = false)]
    public string Name { get; set; }

    /// <summary>
    /// 配置键
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：100
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：配置项的唯一标识，系统中不允许重复
    /// 建议使用有意义的键名，如：system.title
    /// 已建立唯一索引：idx_config_key
    /// </remarks>
    [SugarColumn(ColumnName = "key", ColumnDescription = "配置键",  Length = 100, IsNullable = false)]
    public string Key { get; set; }

    /// <summary>
    /// 配置值
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：500
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：配置项的值，根据配置类型可以存储不同格式的数据
    /// 如：字符串、数字、JSON等
    /// </remarks>
    [SugarColumn(ColumnName = "value", ColumnDescription = "配置值", Length = 500, IsNullable = false)]
    public string Value { get; set; }

    /// <summary>
    /// 配置类型
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：0
    /// 说明：配置项的数据类型，用于界面展示和数据验证
    /// 类型包括：字符串(0)、数字(1)、布尔(2)、JSON(3)等
    /// </remarks>
    [SugarColumn(ColumnName = "type", ColumnDescription = "配置类型", IsNullable = false)]
    public LeanConfigType Type { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：1
    /// 说明：配置项状态：启用(1)、禁用(0)
    /// 禁用的配置项在系统中不生效
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
    /// 说明：配置项的补充说明信息
    /// 最多500个字符
    /// </remarks>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, IsNullable = true)]
    public string? Remark { get; set; }
} 
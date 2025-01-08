using Lean.Cus.Common.Entities;
using Lean.Cus.Common.Enums;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 翻译实体
/// </summary>
/// <remarks>
/// 翻译用于存储系统的多语言翻译内容。
/// 每个翻译项都关联到一个语言，并且有唯一的键(Key)。
/// 翻译项可以按类型分类，如：菜单、按钮、消息等。
/// 系统根据当前语言环境自动加载对应的翻译内容。
/// </remarks>
[SugarTable("lean_translation", "翻译表")]
[SugarIndex("idx_translation_key", nameof(Key), OrderByType.Asc)]
[SugarIndex("idx_translation_language", nameof(LanguageId), OrderByType.Asc)]
public class LeanTranslation : LeanEntity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanTranslation()
    {
        Key = string.Empty;
        Value = string.Empty;
    }

    /// <summary>
    /// 语言ID
    /// </summary>
    /// <remarks>
    /// 数据类型：长整型（bigint）
    /// 允许为空：否
    /// 默认值：无
    /// 说明：关联的语言ID，必须是语言表中存在的ID
    /// 已建立索引：idx_translation_language
    /// </remarks>
    [SugarColumn(ColumnName = "language_id", ColumnDescription = "语言ID", IsNullable = false, ColumnDataType = "bigint")]
    public long LanguageId { get; set; }

    /// <summary>
    /// 翻译键
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：100
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：翻译项的标识，同一语言下不允许重复
    /// 建议使用有意义的键名，如：menu.dashboard
    /// 已建立索引：idx_translation_key
    /// </remarks>
    [SugarColumn(ColumnName = "key", ColumnDescription = "翻译键", Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string Key { get; set; }

    /// <summary>
    /// 翻译值
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：500
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：翻译项的实际内容，使用对应语言的文本
    /// 支持HTML标签和特殊字符
    /// </remarks>
    [SugarColumn(ColumnName = "value", ColumnDescription = "翻译值", Length = 500, IsNullable = false, ColumnDataType = "varchar")]
    public string Value { get; set; }

    /// <summary>
    /// 翻译类型
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：0
    /// 说明：翻译项的分类：菜单(0)、按钮(1)、消息(2)等
    /// 用于对翻译内容进行分类管理
    /// </remarks>
    [SugarColumn(ColumnName = "type", ColumnDescription = "翻译类型", IsNullable = false, ColumnDataType = "int")]
    public LeanTranslationType Type { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：500
    /// 允许为空：是
    /// 默认值：null
    /// 说明：翻译项的补充说明信息
    /// 最多500个字符
    /// </remarks>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, IsNullable = true, ColumnDataType = "varchar")]
    public string? Remark { get; set; }

    /// <summary>
    /// 语言
    /// </summary>
    /// <remarks>
    /// 导航属性：关联的语言信息
    /// 关联类型：一对一
    /// 关联字段：LanguageId
    /// 说明：通过LanguageId关联到语言表
    /// </remarks>
    [Navigate(NavigateType.OneToOne, nameof(LanguageId))]
    public LeanLanguage? Language { get; set; }
} 
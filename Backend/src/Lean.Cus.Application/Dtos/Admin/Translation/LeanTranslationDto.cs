using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 翻译DTO
/// </summary>
public class LeanTranslationDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 语言ID
    /// </summary>
    public long LanguageId { get; set; }

    /// <summary>
    /// 翻译键
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 翻译值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 翻译类型
    /// </summary>
    public LeanTranslationType Type { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    public string? LanguageName { get; set; }
}

/// <summary>
/// 翻译查询DTO
/// </summary>
public class LeanTranslationQueryDto : LeanPagedDto
{
    /// <summary>
    /// 语言ID
    /// </summary>
    public long? LanguageId { get; set; }

    /// <summary>
    /// 翻译键
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// 翻译值
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// 翻译类型
    /// </summary>
    public LeanTranslationType? Type { get; set; }
}

/// <summary>
/// 翻译创建DTO
/// </summary>
public class LeanTranslationCreateDto
{
    /// <summary>
    /// 语言ID
    /// </summary>
    public long LanguageId { get; set; }

    /// <summary>
    /// 翻译键
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 翻译值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 翻译类型
    /// </summary>
    public LeanTranslationType Type { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 翻译更新DTO
/// </summary>
public class LeanTranslationUpdateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 语言ID
    /// </summary>
    public long LanguageId { get; set; }

    /// <summary>
    /// 翻译键
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 翻译值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 翻译类型
    /// </summary>
    public LeanTranslationType Type { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
} 
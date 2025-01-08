using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 语言DTO
/// </summary>
public class LeanLanguageDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 语言编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 语言查询DTO
/// </summary>
public class LeanLanguageQueryDto : LeanPagedDto
{
    /// <summary>
    /// 语言名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 语言编码
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus? Status { get; set; }
}

/// <summary>
/// 语言创建DTO
/// </summary>
public class LeanLanguageCreateDto
{
    /// <summary>
    /// 语言名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 语言编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 语言更新DTO
/// </summary>
public class LeanLanguageUpdateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 语言名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 语言编码
    /// </summary>
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 图标
    /// </summary>
    public string? Icon { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
} 
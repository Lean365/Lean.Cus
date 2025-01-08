using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 字典数据DTO
/// </summary>
public class LeanDictDataDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 字典类型ID
    /// </summary>
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典标签
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// 字典值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 样式类型
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    /// 字典类型名称
    /// </summary>
    public string? DictTypeName { get; set; }
}

/// <summary>
/// 字典数据查询DTO
/// </summary>
public class LeanDictDataQueryDto : LeanPagedDto
{
    /// <summary>
    /// 字典类型ID
    /// </summary>
    public long? DictTypeId { get; set; }

    /// <summary>
    /// 字典标签
    /// </summary>
    public string? Label { get; set; }

    /// <summary>
    /// 字典值
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus? Status { get; set; }
}

/// <summary>
/// 字典数据创建DTO
/// </summary>
public class LeanDictDataCreateDto
{
    /// <summary>
    /// 字典类型ID
    /// </summary>
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典标签
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// 字典值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 样式类型
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
}

/// <summary>
/// 字典数据更新DTO
/// </summary>
public class LeanDictDataUpdateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 字典类型ID
    /// </summary>
    public long DictTypeId { get; set; }

    /// <summary>
    /// 字典标签
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// 字典值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 样式类型
    /// </summary>
    public string? CssClass { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
} 
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Dtos.Admin;

/// <summary>
/// 配置DTO
/// </summary>
public class LeanConfigDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 配置名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 配置键
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 配置值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 配置类型
    /// </summary>
    public LeanConfigType Type { get; set; }

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
/// 配置查询DTO
/// </summary>
public class LeanConfigQueryDto : LeanPagedDto
{
    /// <summary>
    /// 配置名称
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// 配置键
    /// </summary>
    public string? Key { get; set; }

    /// <summary>
    /// 配置类型
    /// </summary>
    public LeanConfigType? Type { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus? Status { get; set; }
}

/// <summary>
/// 配置创建DTO
/// </summary>
public class LeanConfigCreateDto
{
    /// <summary>
    /// 配置名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 配置键
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 配置值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 配置类型
    /// </summary>
    public LeanConfigType Type { get; set; }

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
/// 配置更新DTO
/// </summary>
public class LeanConfigUpdateDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 配置名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 配置键
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// 配置值
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// 配置类型
    /// </summary>
    public LeanConfigType Type { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string? Remark { get; set; }
} 
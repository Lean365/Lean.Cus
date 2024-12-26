using Newtonsoft.Json;

namespace Lean.Cus.Domain.Interfaces;

/// <summary>
/// 基础实体接口
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public interface ILeanBaseEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    TKey Id { get; set; }

    /// <summary>
    /// 创建者ID
    /// </summary>
    long CreateId { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    string CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新者ID
    /// </summary>
    long? UpdateId { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 删除者ID
    /// </summary>
    long? DeleteId { get; set; }

    /// <summary>
    /// 删除者
    /// </summary>
    string? DeleteBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    DateTime? DeleteTime { get; set; }

    /// <summary>
    /// 是否已删除（0代表存在 1代表删除）
    /// </summary>
    int IsDeleted { get; set; }

    /// <summary>
    /// 租户ID
    /// </summary>
    long? TenantId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    string? Remark { get; set; }
} 
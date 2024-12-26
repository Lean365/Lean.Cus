//===================================================
// 项目名称：Lean.Cus.Domain.Entities
// 文件名称：LeanAggregateRoot
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：聚合根基类
//===================================================

using Lean.Cus.Common.Events;

namespace Lean.Cus.Domain.Entities;

/// <summary>
/// 聚合根基类
/// <para>作为系统中所有聚合根的基类</para>
/// <para>提供聚合根的基本属性和行为，包括领域事件的管理</para>
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public abstract class LeanAggregateRoot<TKey> : LeanBaseEntity<TKey>
{
    private readonly List<LeanDomainEvent> _domainEvents = new();

    /// <summary>
    /// 领域事件集合
    /// <para>只读集合，包含所有未处理的领域事件</para>
    /// </summary>
    public IReadOnlyCollection<LeanDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    /// <summary>
    /// 默认构造函数
    /// </summary>
    protected LeanAggregateRoot() : base() { }

    /// <summary>
    /// 使用指定ID初始化聚合根
    /// </summary>
    /// <param name="id">聚合根ID</param>
    protected LeanAggregateRoot(TKey id) : base(id) { }

    /// <summary>
    /// 添加领域事件
    /// </summary>
    /// <param name="domainEvent">领域事件</param>
    protected void AddDomainEvent(LeanDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    /// <summary>
    /// 移除领域事件
    /// </summary>
    /// <param name="domainEvent">领域事件</param>
    protected void RemoveDomainEvent(LeanDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    /// <summary>
    /// 清空领域事件
    /// </summary>
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
} 
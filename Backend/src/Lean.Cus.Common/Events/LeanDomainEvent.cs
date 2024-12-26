//===================================================
// 项目名称：Lean.Cus.Common.Events
// 文件名称：LeanDomainEvent
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：领域事件基类
//===================================================

namespace Lean.Cus.Common.Events;

/// <summary>
/// 领域事件基类
/// <para>作为系统中所有领域事件的基类</para>
/// <para>提供事件的基本属性和行为</para>
/// </summary>
public abstract class LeanDomainEvent
{
    /// <summary>
    /// 事件ID
    /// <para>用于唯一标识一个事件实例</para>
    /// </summary>
    public Guid Id { get; }

    /// <summary>
    /// 事件发生时间
    /// <para>事件创建的UTC时间</para>
    /// </summary>
    public DateTimeOffset OccurredOn { get; }

    /// <summary>
    /// 事件类型
    /// <para>事件的完整类型名称</para>
    /// </summary>
    public string EventType { get; }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    protected LeanDomainEvent()
    {
        Id = Guid.NewGuid();
        OccurredOn = DateTimeOffset.UtcNow;
        EventType = GetType().Name;
    }

    /// <summary>
    /// 获取事件的字符串表示
    /// </summary>
    /// <returns>事件的字符串表示</returns>
    public override string ToString()
    {
        return $"{EventType} - {Id} - {OccurredOn:yyyy-MM-dd HH:mm:ss.fff}";
    }
} 
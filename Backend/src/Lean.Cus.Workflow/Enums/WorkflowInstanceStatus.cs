namespace Lean.Cus.Workflow.Enums;

/// <summary>
/// 流程实例状态
/// </summary>
public enum WorkflowInstanceStatus
{
    /// <summary>
    /// 运行中
    /// </summary>
    Running = 0,

    /// <summary>
    /// 已完成
    /// </summary>
    Completed = 1,

    /// <summary>
    /// 已终止
    /// </summary>
    Terminated = 2,

    /// <summary>
    /// 已挂起
    /// </summary>
    Suspended = 3
} 
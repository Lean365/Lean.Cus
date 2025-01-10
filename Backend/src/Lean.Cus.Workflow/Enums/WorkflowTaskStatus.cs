namespace Lean.Cus.Workflow.Enums;

/// <summary>
/// 流程任务状态
/// </summary>
public enum WorkflowTaskStatus
{
    /// <summary>
    /// 待处理
    /// </summary>
    Pending = 0,

    /// <summary>
    /// 已完成
    /// </summary>
    Completed = 1,

    /// <summary>
    /// 已取消
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// 已转办
    /// </summary>
    Transferred = 3,

    /// <summary>
    /// 已委托
    /// </summary>
    Delegated = 4
} 
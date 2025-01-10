using Lean.Cus.Workflow.Enums;

namespace Lean.Cus.Workflow.Dtos.Task;

/// <summary>
/// 流程任务DTO
/// </summary>
public class LeanWorkflowTaskDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 流程实例ID
    /// </summary>
    public required long InstanceId { get; set; }

    /// <summary>
    /// 节点ID
    /// </summary>
    public required string NodeId { get; set; }

    /// <summary>
    /// 节点名称
    /// </summary>
    public required string NodeName { get; set; }

    /// <summary>
    /// 任务类型
    /// </summary>
    public required string TaskType { get; set; }

    /// <summary>
    /// 处理人ID
    /// </summary>
    public required long AssigneeId { get; set; }

    /// <summary>
    /// 处理人名称
    /// </summary>
    public required string AssigneeName { get; set; }

    /// <summary>
    /// 原处理人ID(转办/委托时使用)
    /// </summary>
    public long? OriginalAssigneeId { get; set; }

    /// <summary>
    /// 原处理人名称(转办/委托时使用)
    /// </summary>
    public string? OriginalAssigneeName { get; set; }

    /// <summary>
    /// 任务状态
    /// </summary>
    public WorkflowTaskStatus Status { get; set; }

    /// <summary>
    /// 处理意见
    /// </summary>
    public string? Comment { get; set; }

    /// <summary>
    /// 到期时间
    /// </summary>
    public DateTime? DueTime { get; set; }

    /// <summary>
    /// 完成时间
    /// </summary>
    public DateTime? CompleteTime { get; set; }

    /// <summary>
    /// 创建人
    /// </summary>
    public string? CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新人
    /// </summary>
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdateTime { get; set; }
} 
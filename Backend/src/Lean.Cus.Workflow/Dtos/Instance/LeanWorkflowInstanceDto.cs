using Lean.Cus.Workflow.Enums;

namespace Lean.Cus.Workflow.Dtos.Instance;

/// <summary>
/// 流程实例DTO
/// </summary>
public class LeanWorkflowInstanceDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 流程定义ID
    /// </summary>
    public required long DefinitionId { get; set; }

    /// <summary>
    /// 业务ID
    /// </summary>
    public required long BusinessId { get; set; }

    /// <summary>
    /// 业务类型
    /// </summary>
    public required string BusinessType { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// 流程发起人ID
    /// </summary>
    public required long InitiatorId { get; set; }

    /// <summary>
    /// 流程发起人名称
    /// </summary>
    public required string InitiatorName { get; set; }

    /// <summary>
    /// 当前节点ID
    /// </summary>
    public required string CurrentNodeId { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    public required string CurrentNodeName { get; set; }

    /// <summary>
    /// 流程实例状态
    /// </summary>
    public WorkflowInstanceStatus Status { get; set; }

    /// <summary>
    /// 流程变量(JSON格式)
    /// </summary>
    public string? Variables { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public required DateTime StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    public DateTime? EndTime { get; set; }

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
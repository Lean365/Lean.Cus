namespace Lean.Cus.Workflow.Dtos.History;

/// <summary>
/// 流程历史记录DTO
/// </summary>
public class LeanWorkflowHistoryDto
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
    /// 操作类型
    /// </summary>
    public required string OperationType { get; set; }

    /// <summary>
    /// 操作人ID
    /// </summary>
    public required long OperatorId { get; set; }

    /// <summary>
    /// 操作人名称
    /// </summary>
    public required string OperatorName { get; set; }

    /// <summary>
    /// 操作说明
    /// </summary>
    public string? OperationComment { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public required DateTime OperationTime { get; set; }

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
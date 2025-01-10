using Lean.Cus.Workflow.Enums;

namespace Lean.Cus.Workflow.Dtos.Definition;

/// <summary>
/// 流程定义DTO
/// </summary>
public class LeanWorkflowDefinitionDto
{
    /// <summary>
    /// 主键
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 流程名称
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// 流程描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// 流程定义(JSON格式)
    /// </summary>
    public required string Definition { get; set; }

    /// <summary>
    /// 表单ID
    /// </summary>
    public long? FormId { get; set; }

    /// <summary>
    /// 流程状态
    /// </summary>
    public WorkflowStatus Status { get; set; }

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
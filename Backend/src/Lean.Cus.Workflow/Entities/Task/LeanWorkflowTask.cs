using Lean.Cus.Domain.Entities;
using Lean.Cus.Workflow.Enums;
using SqlSugar;

namespace Lean.Cus.Workflow.Entities.Task;

/// <summary>
/// 流程任务实体
/// </summary>
[SugarTable("lean_workflow_task", "流程任务")]
public class LeanWorkflowTask : LeanBaseEntity
{
    /// <summary>
    /// 流程实例ID
    /// </summary>
    [SugarColumn(ColumnName = "instance_id", ColumnDescription = "流程实例ID", 
        ColumnDataType = "bigint", IsNullable = false)]
    public long InstanceId { get; set; }

    /// <summary>
    /// 节点ID
    /// </summary>
    [SugarColumn(ColumnName = "node_id", ColumnDescription = "节点ID", 
        ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string NodeId { get; set; }

    /// <summary>
    /// 节点名称
    /// </summary>
    [SugarColumn(ColumnName = "node_name", ColumnDescription = "节点名称", 
        ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string NodeName { get; set; }

    /// <summary>
    /// 任务类型
    /// </summary>
    [SugarColumn(ColumnName = "task_type", ColumnDescription = "任务类型", 
        ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string TaskType { get; set; }

    /// <summary>
    /// 处理人ID
    /// </summary>
    [SugarColumn(ColumnName = "assignee_id", ColumnDescription = "处理人ID", 
        ColumnDataType = "bigint", IsNullable = false)]
    public long AssigneeId { get; set; }

    /// <summary>
    /// 处理人名称
    /// </summary>
    [SugarColumn(ColumnName = "assignee_name", ColumnDescription = "处理人名称", 
        ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string AssigneeName { get; set; }

    /// <summary>
    /// 原处理人ID(转办/委托时使用)
    /// </summary>
    [SugarColumn(ColumnName = "original_assignee_id", ColumnDescription = "原处理人ID", 
        ColumnDataType = "bigint", IsNullable = true)]
    public long? OriginalAssigneeId { get; set; }

    /// <summary>
    /// 原处理人名称(转办/委托时使用)
    /// </summary>
    [SugarColumn(ColumnName = "original_assignee_name", ColumnDescription = "原处理人名称", 
        ColumnDataType = "nvarchar", Length = 50, IsNullable = true)]
    public string? OriginalAssigneeName { get; set; }

    /// <summary>
    /// 任务状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "任务状态", 
        ColumnDataType = "int", IsNullable = false)]
    public WorkflowTaskStatus Status { get; set; }

    /// <summary>
    /// 处理意见
    /// </summary>
    [SugarColumn(ColumnName = "comment", ColumnDescription = "处理意见", 
        ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Comment { get; set; }

    /// <summary>
    /// 到期时间
    /// </summary>
    [SugarColumn(ColumnName = "due_time", ColumnDescription = "到期时间", 
        ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? DueTime { get; set; }

    /// <summary>
    /// 完成时间
    /// </summary>
    [SugarColumn(ColumnName = "complete_time", ColumnDescription = "完成时间", 
        ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? CompleteTime { get; set; }
} 
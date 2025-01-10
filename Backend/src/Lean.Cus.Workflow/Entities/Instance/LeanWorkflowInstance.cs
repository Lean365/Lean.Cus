using Lean.Cus.Domain.Entities;
using Lean.Cus.Workflow.Enums;
using SqlSugar;

namespace Lean.Cus.Workflow.Entities.Instance;

/// <summary>
/// 流程实例实体
/// </summary>
[SugarTable("lean_workflow_instance", "流程实例")]
public class LeanWorkflowInstance : LeanBaseEntity
{
    /// <summary>
    /// 流程定义ID
    /// </summary>
    [SugarColumn(ColumnName = "definition_id", ColumnDescription = "流程定义ID", 
        ColumnDataType = "bigint", IsNullable = false)]
    public long DefinitionId { get; set; }

    /// <summary>
    /// 业务ID
    /// </summary>
    [SugarColumn(ColumnName = "business_id", ColumnDescription = "业务ID", 
        ColumnDataType = "bigint", IsNullable = false)]
    public long BusinessId { get; set; }

    /// <summary>
    /// 业务类型
    /// </summary>
    [SugarColumn(ColumnName = "business_type", ColumnDescription = "业务类型", 
        ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string BusinessType { get; set; }

    /// <summary>
    /// 流程标题
    /// </summary>
    [SugarColumn(ColumnName = "title", ColumnDescription = "流程标题", 
        ColumnDataType = "nvarchar", Length = 200, IsNullable = false)]
    public string Title { get; set; }

    /// <summary>
    /// 流程发起人ID
    /// </summary>
    [SugarColumn(ColumnName = "initiator_id", ColumnDescription = "流程发起人ID", 
        ColumnDataType = "bigint", IsNullable = false)]
    public long InitiatorId { get; set; }

    /// <summary>
    /// 流程发起人名称
    /// </summary>
    [SugarColumn(ColumnName = "initiator_name", ColumnDescription = "流程发起人名称", 
        ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string InitiatorName { get; set; }

    /// <summary>
    /// 当前节点ID
    /// </summary>
    [SugarColumn(ColumnName = "current_node_id", ColumnDescription = "当前节点ID", 
        ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string CurrentNodeId { get; set; }

    /// <summary>
    /// 当前节点名称
    /// </summary>
    [SugarColumn(ColumnName = "current_node_name", ColumnDescription = "当前节点名称", 
        ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string CurrentNodeName { get; set; }

    /// <summary>
    /// 流程实例状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "流程实例状态", 
        ColumnDataType = "int", IsNullable = false)]
    public WorkflowInstanceStatus Status { get; set; }

    /// <summary>
    /// 流程变量(JSON格式)
    /// </summary>
    [SugarColumn(ColumnName = "variables", ColumnDescription = "流程变量", 
        ColumnDataType = "text", IsNullable = true)]
    public string? Variables { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [SugarColumn(ColumnName = "start_time", ColumnDescription = "开始时间", 
        ColumnDataType = "datetime", IsNullable = false)]
    public DateTime StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [SugarColumn(ColumnName = "end_time", ColumnDescription = "结束时间", 
        ColumnDataType = "datetime", IsNullable = true)]
    public DateTime? EndTime { get; set; }
} 
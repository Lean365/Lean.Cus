using Lean.Cus.Domain.Entities;
using SqlSugar;

namespace Lean.Cus.Workflow.Entities.History;

/// <summary>
/// 流程历史记录实体
/// </summary>
[SugarTable("lean_workflow_history", "流程历史记录")]
public class LeanWorkflowHistory : LeanBaseEntity
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
    /// 操作类型
    /// </summary>
    [SugarColumn(ColumnName = "operation_type", ColumnDescription = "操作类型", 
        ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OperationType { get; set; }

    /// <summary>
    /// 操作人ID
    /// </summary>
    [SugarColumn(ColumnName = "operator_id", ColumnDescription = "操作人ID", 
        ColumnDataType = "bigint", IsNullable = false)]
    public long OperatorId { get; set; }

    /// <summary>
    /// 操作人名称
    /// </summary>
    [SugarColumn(ColumnName = "operator_name", ColumnDescription = "操作人名称", 
        ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string OperatorName { get; set; }

    /// <summary>
    /// 操作说明
    /// </summary>
    [SugarColumn(ColumnName = "operation_comment", ColumnDescription = "操作说明", 
        ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? OperationComment { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    [SugarColumn(ColumnName = "operation_time", ColumnDescription = "操作时间", 
        ColumnDataType = "datetime", IsNullable = false)]
    public DateTime OperationTime { get; set; }
} 
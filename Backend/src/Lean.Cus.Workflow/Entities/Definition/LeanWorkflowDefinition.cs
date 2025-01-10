using Lean.Cus.Domain.Entities;
using Lean.Cus.Workflow.Enums;
using SqlSugar;

namespace Lean.Cus.Workflow.Entities.Definition;

/// <summary>
/// 流程定义实体
/// </summary>
[SugarTable("lean_workflow_definition", "流程定义")]
public class LeanWorkflowDefinition : LeanBaseEntity
{
    /// <summary>
    /// 流程名称
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDescription = "流程名称", 
        ColumnDataType = "nvarchar", Length = 100, IsNullable = false)]
    public string Name { get; set; }

    /// <summary>
    /// 流程描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "流程描述", 
        ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Description { get; set; }

    /// <summary>
    /// 流程定义(JSON格式)
    /// </summary>
    [SugarColumn(ColumnName = "definition", ColumnDescription = "流程定义", 
        ColumnDataType = "text", IsNullable = false)]
    public string Definition { get; set; }

    /// <summary>
    /// 表单ID
    /// </summary>
    [SugarColumn(ColumnName = "form_id", ColumnDescription = "表单ID", 
        ColumnDataType = "bigint", IsNullable = true)]
    public long? FormId { get; set; }

    /// <summary>
    /// 流程状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "流程状态", 
        ColumnDataType = "int", IsNullable = false)]
    public WorkflowStatus Status { get; set; }
} 
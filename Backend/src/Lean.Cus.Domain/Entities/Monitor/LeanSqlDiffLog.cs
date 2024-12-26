//===================================================
// 项目名称：Lean.Cus.Domain.Entities.Monitor
// 文件名称：LeanSqlDiffLog
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：SQL差异日志实体
//===================================================

using SqlSugar;
using System;
using System.ComponentModel;

namespace Lean.Cus.Domain.Entities.Monitor;

/// <summary>
/// SQL差异日志实体
/// </summary>
[SugarTable("Lean_sql_diff_log", "SQL差异日志表")]
public class LeanSqlDiffLog : LeanLongEntity
{
    /// <summary>
    /// 数据库名称
    /// </summary>
    [Description("数据库名称")]
    [SugarColumn(ColumnName = "database_name", ColumnDescription = "数据库名称", Length = 50, IsNullable = false)]
    public string DatabaseName { get; set; } = null!;

    /// <summary>
    /// 表名
    /// </summary>
    [Description("表名")]
    [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", Length = 100, IsNullable = false)]
    public string TableName { get; set; } = null!;

    /// <summary>
    /// 操作类型（Insert/Update/Delete）
    /// </summary>
    [Description("操作类型")]
    [SugarColumn(ColumnName = "operation_type", ColumnDescription = "操作类型（Insert/Update/Delete）", Length = 20, IsNullable = false)]
    public string OperationType { get; set; } = null!;

    /// <summary>
    /// 原始数据（JSON格式）
    /// </summary>
    [Description("原始数据")]
    [SugarColumn(ColumnName = "old_data", ColumnDescription = "原始数据（JSON格式）", ColumnDataType = "text", IsNullable = true)]
    public string? OldData { get; set; }

    /// <summary>
    /// 新数据（JSON格式）
    /// </summary>
    [Description("新数据")]
    [SugarColumn(ColumnName = "new_data", ColumnDescription = "新数据（JSON格式）", ColumnDataType = "text", IsNullable = true)]
    public string? NewData { get; set; }

    /// <summary>
    /// SQL语句
    /// </summary>
    [Description("SQL语句")]
    [SugarColumn(ColumnName = "sql_statement", ColumnDescription = "SQL语句", ColumnDataType = "text", IsNullable = false)]
    public string SqlStatement { get; set; } = null!;

    /// <summary>
    /// 操作人ID
    /// </summary>
    [Description("操作人ID")]
    [SugarColumn(ColumnName = "operator_id", ColumnDescription = "操作人ID", IsNullable = false)]
    public long OperatorId { get; set; }

    /// <summary>
    /// 操作人名称
    /// </summary>
    [Description("操作人名称")]
    [SugarColumn(ColumnName = "operator_name", ColumnDescription = "操作人名称", Length = 50, IsNullable = false)]
    public string OperatorName { get; set; } = null!;

    /// <summary>
    /// IP地址
    /// </summary>
    [Description("IP地址")]
    [SugarColumn(ColumnName = "ip_address", ColumnDescription = "IP地址", Length = 50, IsNullable = true)]
    public string? IpAddress { get; set; }


} 
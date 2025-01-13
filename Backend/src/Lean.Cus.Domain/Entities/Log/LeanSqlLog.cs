//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件的所有修改。
// </auto-generated>
//
// <copyright file="LeanSqlLog.cs" company="Lean365">
//     Copyright (c) Lean365. All rights reserved.
// </copyright>
// <author>Lean365</author>
// <date>2024-01-09</date>
// <summary>
// SQL差异日志实体类
// </summary>
//------------------------------------------------------------------------------

using System;
using SqlSugar;
using Lean.Cus.Domain.Entities;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Domain.Entities.Log;

/// <summary>
/// SQL差异日志实体
/// </summary>
[SugarTable("lean_mon_sql_log", "SQL差异日志表")]
[SugarIndex("idx_sql_log_type", nameof(SqlType), OrderByType.Asc)]
[SugarIndex("idx_sql_log_table", nameof(TableName), OrderByType.Asc)]
[SugarIndex("idx_sql_log_status", nameof(Status), OrderByType.Asc)]
public class LeanSqlLog : LeanBaseEntity
{
    /// <summary>
    /// SQL类型（1：INSERT，2：UPDATE，3：DELETE，4：BATCH）
    /// </summary>
    [SugarColumn(ColumnName = "sql_type", ColumnDescription = "SQL类型（1：INSERT，2：UPDATE，3：DELETE，4：BATCH）", 
        IsNullable = false, ColumnDataType = "int")]
    public LeanSqlType SqlType { get; set; }

    /// <summary>
    /// 数据库名
    /// </summary>
    [SugarColumn(ColumnName = "database_name", ColumnDescription = "数据库名", 
        Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string DatabaseName { get; set; } = string.Empty;

    /// <summary>
    /// 表名
    /// </summary>
    [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", 
        Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// SQL语句
    /// </summary>
    [SugarColumn(ColumnName = "sql_text", ColumnDescription = "SQL语句", 
        Length = 4000, IsNullable = false, ColumnDataType = "nvarchar")]
    public string SqlText { get; set; } = string.Empty;

    /// <summary>
    /// SQL参数
    /// </summary>
    [SugarColumn(ColumnName = "parameters", ColumnDescription = "SQL参数", 
        Length = 4000, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? Parameters { get; set; }

    /// <summary>
    /// 变更前数据
    /// </summary>
    [SugarColumn(ColumnName = "before_data", ColumnDescription = "变更前数据", 
        Length = 4000, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? BeforeData { get; set; }

    /// <summary>
    /// 变更后数据
    /// </summary>
    [SugarColumn(ColumnName = "after_data", ColumnDescription = "变更后数据", 
        Length = 4000, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? AfterData { get; set; }

    /// <summary>
    /// 变更字段
    /// </summary>
    [SugarColumn(ColumnName = "changed_fields", ColumnDescription = "变更字段", 
        Length = 4000, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? ChangedFields { get; set; }

    /// <summary>
    /// 影响行数
    /// </summary>
    [SugarColumn(ColumnName = "affected_rows", ColumnDescription = "影响行数", 
        IsNullable = false, ColumnDataType = "int")]
    public int AffectedRows { get; set; }

    /// <summary>
    /// 执行时间（毫秒）
    /// </summary>
    [SugarColumn(ColumnName = "execution_time", ColumnDescription = "执行时间（毫秒）", 
        IsNullable = false, ColumnDataType = "bigint")]
    public long ExecutionTime { get; set; }

    /// <summary>
    /// 执行状态（1：成功，0：失败）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "执行状态（1：成功，0：失败）", 
        IsNullable = false, ColumnDataType = "int")]
    public LeanStatus Status { get; set; } = LeanStatus.Enabled;

    /// <summary>
    /// 错误信息
    /// </summary>
    [SugarColumn(ColumnName = "error_message", ColumnDescription = "错误信息", 
        Length = 4000, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? ErrorMessage { get; set; }

    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", 
        IsNullable = true, ColumnDataType = "bigint")]
    public long? UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", 
        Length = 50, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? UserName { get; set; }

    /// <summary>
    /// 请求IP
    /// </summary>
    [SugarColumn(ColumnName = "ip", ColumnDescription = "请求IP", 
        Length = 50, IsNullable = true, ColumnDataType = "varchar")]
    public string? Ip { get; set; }

    /// <summary>
    /// 请求位置
    /// </summary>
    [SugarColumn(ColumnName = "location", ColumnDescription = "请求位置", 
        Length = 100, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? Location { get; set; }
} 
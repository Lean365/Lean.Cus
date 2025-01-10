//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件的所有修改。
// </auto-generated>
//
// <copyright file="LeanAuditLog.cs" company="Lean365">
//     Copyright (c) Lean365. All rights reserved.
// </copyright>
// <author>Lean365</author>
// <date>2024-01-09</date>
// <summary>
// 审计日志实体类
// </summary>
//------------------------------------------------------------------------------

using System;
using SqlSugar;
using Lean.Cus.Domain.Entities;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Domain.Entities.Log;

/// <summary>
/// 审计日志实体
/// </summary>
[SugarTable("lean_mon_audit_log", "审计日志表")]
[SugarIndex("idx_audit_log_user", nameof(UserId), OrderByType.Asc)]
[SugarIndex("idx_audit_log_type", nameof(AuditType), OrderByType.Asc)]
public class LeanAuditLog : LeanBaseEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", 
        IsNullable = false, ColumnDataType = "bigint")]
    public long UserId { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", 
        Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 审计类型（0：数据变更，1：权限变更，2：配置变更，3：安全变更）
    /// </summary>
    [SugarColumn(ColumnName = "audit_type", ColumnDescription = "审计类型（0：数据变更，1：权限变更，2：配置变更，3：安全变更）", 
        IsNullable = false, ColumnDataType = "int")]
    public LeanAuditType AuditType { get; set; }

    /// <summary>
    /// 表名
    /// </summary>
    [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名", 
        Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string TableName { get; set; } = string.Empty;

    /// <summary>
    /// 业务ID
    /// </summary>
    [SugarColumn(ColumnName = "business_id", ColumnDescription = "业务ID", 
        IsNullable = false, ColumnDataType = "bigint")]
    public long BusinessId { get; set; }

    /// <summary>
    /// 业务名称
    /// </summary>
    [SugarColumn(ColumnName = "business_name", ColumnDescription = "业务名称", 
        Length = 100, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? BusinessName { get; set; }

    /// <summary>
    /// 操作类型（0：查询，1：新增，2：修改，3：删除，4：导入，5：导出）
    /// </summary>
    [SugarColumn(ColumnName = "operation_type", ColumnDescription = "操作类型（0：查询，1：新增，2：修改，3：删除，4：导入，5：导出）", 
        IsNullable = false, ColumnDataType = "int")]
    public LeanOperationType OperationType { get; set; }

    /// <summary>
    /// 操作描述
    /// </summary>
    [SugarColumn(ColumnName = "description", ColumnDescription = "操作描述", 
        Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? Description { get; set; }

    /// <summary>
    /// 请求IP
    /// </summary>
    [SugarColumn(ColumnName = "ip", ColumnDescription = "请求IP", 
        Length = 50, IsNullable = false, ColumnDataType = "varchar")]
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 请求位置
    /// </summary>
    [SugarColumn(ColumnName = "location", ColumnDescription = "请求位置", 
        Length = 100, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? Location { get; set; }

    /// <summary>
    /// 旧值
    /// </summary>
    [SugarColumn(ColumnName = "old_value", ColumnDescription = "旧值", 
        Length = 4000, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? OldValue { get; set; }

    /// <summary>
    /// 新值
    /// </summary>
    [SugarColumn(ColumnName = "new_value", ColumnDescription = "新值", 
        Length = 4000, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? NewValue { get; set; }

    /// <summary>
    /// 变更字段
    /// </summary>
    [SugarColumn(ColumnName = "changed_fields", ColumnDescription = "变更字段", 
        Length = 4000, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? ChangedFields { get; set; }
} 
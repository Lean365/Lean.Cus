//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件的所有修改。
// </auto-generated>
//
// <copyright file="LeanUserRole.cs" company="Lean365">
//     Copyright (c) Lean365. All rights reserved.
// </copyright>
// <author>Lean365</author>
// <date>2024-01-09</date>
// <summary>
// 用户-角色关系实体类
// </summary>
//------------------------------------------------------------------------------

using System;
using SqlSugar;
using Lean.Cus.Domain.Entities;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 用户-角色关系实体
/// </summary>
[SugarTable("lean_admin_user_role", "用户-角色关系表")]
[SugarIndex("IX_LeanUserRole", nameof(UserId), OrderByType.Asc, nameof(RoleId), OrderByType.Asc, true)]
public class LeanUserRole : LeanBaseEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", 
        IsNullable = false, ColumnDataType = "bigint")]
    public long UserId { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", 
        IsNullable = false, ColumnDataType = "bigint")]
    public long RoleId { get; set; }
} 
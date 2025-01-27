//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件的所有修改。
// </auto-generated>
//
// <copyright file="LeanRolePermission.cs" company="Lean365">
//     Copyright (c) Lean365. All rights reserved.
// </copyright>
// <author>Lean365</author>
// <date>2024-01-09</date>
// <summary>
// 角色-权限关系实体类
// </summary>
//------------------------------------------------------------------------------

using System;
using SqlSugar;
using Lean.Cus.Domain.Entities;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 角色-权限关系实体
/// </summary>
[SugarTable("lean_admin_role_permission", "角色-权限关系表")]
[SugarIndex("IX_LeanRolePermission", nameof(RoleId), OrderByType.Asc, nameof(PermissionId), OrderByType.Asc, true)]
public class LeanRolePermission : LeanBaseEntity
{
    /// <summary>
    /// 角色ID
    /// </summary>
    [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", 
        IsNullable = false, ColumnDataType = "bigint")]
    public long RoleId { get; set; }

    /// <summary>
    /// 权限ID
    /// </summary>
    [SugarColumn(ColumnName = "permission_id", ColumnDescription = "权限ID", 
        IsNullable = false, ColumnDataType = "bigint")]
    public long PermissionId { get; set; }
} 
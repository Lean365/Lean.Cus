using SqlSugar;
using System;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 菜单权限关联表
/// </summary>
[SugarTable("lean_admin_menu_permission", "菜单权限关联表")]
[SugarIndex("IX_LeanMenuPermission", nameof(MenuId), OrderByType.Asc, nameof(PermissionId), OrderByType.Asc, true)]
public class LeanMenuPermission : LeanBaseEntity
{
    /// <summary>
    /// 菜单ID
    /// </summary>
    [SugarColumn(ColumnName = "menu_id", ColumnDescription = "菜单ID", 
        IsNullable = false, ColumnDataType = "bigint")]
    public long MenuId { get; set; }

    /// <summary>
    /// 权限ID
    /// </summary>
    [SugarColumn(ColumnName = "permission_id", ColumnDescription = "权限ID", 
        IsNullable = false, ColumnDataType = "bigint")]
    public long PermissionId { get; set; }
} 
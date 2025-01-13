using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 角色菜单关联表
/// </summary>
[SugarTable("lean_admin_role_menu", "角色菜单关联表")]
[SugarIndex("IX_LeanRoleMenu", nameof(RoleId), OrderByType.Asc, nameof(MenuId), OrderByType.Asc, true)]
public class LeanRoleMenu : LeanBaseEntity
{
    /// <summary>
    /// 角色ID
    /// </summary>
    [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID")]
    public long RoleId { get; set; }

    /// <summary>
    /// 菜单ID
    /// </summary>
    [SugarColumn(ColumnName = "menu_id", ColumnDescription = "菜单ID")]
    public long MenuId { get; set; }
} 
using Lean.Cus.Common.Entities;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 角色菜单关联实体
/// </summary>
/// <remarks>
/// 角色菜单关联表用于存储角色与菜单的多对多关系。
/// 一个角色可以分配多个菜单权限。
/// 一个菜单可以被分配给多个角色。
/// 通过此表可以实现角色的菜单权限控制。
/// 当角色被删除时，相关的菜单关联记录也会被级联删除。
/// 当菜单被删除时，相关的角色关联记录也会被级联删除。
/// </remarks>
[SugarTable("lean_role_menu", "角色菜单关联表")]
[SugarIndex("idx_role_menu_role_id", nameof(RoleId), OrderByType.Asc)]
[SugarIndex("idx_role_menu_menu_id", nameof(MenuId), OrderByType.Asc)]
public class LeanRoleMenu : LeanEntity
{
    /// <summary>
    /// 角色ID
    /// </summary>
    /// <remarks>
    /// 数据类型：长整型（bigint）
    /// 允许为空：否
    /// 默认值：无
    /// 说明：关联到角色表的主键ID
    /// 已建立索引：idx_role_menu_role_id
    /// 外键关系：lean_role.id
    /// </remarks>
    [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
    public long RoleId { get; set; }

    /// <summary>
    /// 菜单ID
    /// </summary>
    /// <remarks>
    /// 数据类型：长整型（bigint）
    /// 允许为空：否
    /// 默认值：无
    /// 说明：关联到菜单表的主键ID
    /// 已建立索引：idx_role_menu_menu_id
    /// 外键关系：lean_menu.id
    /// </remarks>
    [SugarColumn(ColumnName = "menu_id", ColumnDescription = "菜单ID", ColumnDataType = "bigint", IsNullable = false)]
    public long MenuId { get; set; }

    /// <summary>
    /// 角色导航属性
    /// </summary>
    /// <remarks>
    /// 导航属性：关联的角色信息
    /// 关联类型：多对一
    /// 关联字段：RoleId
    /// 说明：通过RoleId关联到角色表
    /// </remarks>
    [Navigate(NavigateType.ManyToOne, nameof(RoleId))]
    public LeanRole? Role { get; set; }

    /// <summary>
    /// 菜单导航属性
    /// </summary>
    /// <remarks>
    /// 导航属性：关联的菜单信息
    /// 关联类型：多对一
    /// 关联字段：MenuId
    /// 说明：通过MenuId关联到菜单表
    /// </remarks>
    [Navigate(NavigateType.ManyToOne, nameof(MenuId))]
    public LeanMenu? Menu { get; set; }
} 
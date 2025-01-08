using System.Collections.Generic;
using Lean.Cus.Common.Entities;
using Lean.Cus.Common.Enums;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 角色实体
/// </summary>
/// <remarks>
/// 角色是系统权限的载体，用于对用户进行权限分组。
/// 每个角色可以分配多个菜单权限。
/// 每个用户可以分配多个角色。
/// 角色有唯一的编码(Code)，用于标识角色。
/// 角色可以启用或禁用，禁用的角色不参与权限控制。
/// 角色可以设置数据范围，控制用户可以访问的数据范围。
/// 系统角色不允许删除，以保证系统的基本功能。
/// </remarks>
[SugarTable("lean_role", "角色表")]
[SugarIndex("idx_role_code", nameof(Code), OrderByType.Asc, true)]
public class LeanRole : LeanEntity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanRole()
    {
        Name = string.Empty;
        Code = string.Empty;
    }

    /// <summary>
    /// 角色名称
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（nvarchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：角色的显示名称，支持多语言
    /// 建议使用易于理解的名称
    /// </remarks>
    [SugarColumn(ColumnName = "name", ColumnDescription = "角色名称", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string Name { get; set; }

    /// <summary>
    /// 角色编码
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：角色的唯一标识，系统中不允许重复
    /// 建议使用有意义的编码，如：admin、user
    /// 已建立唯一索引：idx_role_code
    /// </remarks>
    [SugarColumn(ColumnName = "code", ColumnDescription = "角色编码", ColumnDataType = "varchar", Length = 50, IsNullable = false)]
    public string Code { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：0
    /// 说明：显示顺序，值越小越靠前
    /// 默认按此字段排序
    /// </remarks>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", ColumnDataType = "int", IsNullable = false)]
    public int OrderNum { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：1
    /// 说明：角色状态：启用(1)、禁用(0)
    /// 禁用的角色不参与权限控制
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 是否系统角色
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：0
    /// 说明：是否是系统内置角色：是(1)、否(0)
    /// 系统角色不允许删除，以保证系统的基本功能
    /// </remarks>
    [SugarColumn(ColumnName = "is_system", ColumnDescription = "是否系统角色", ColumnDataType = "int", IsNullable = false)]
    public int IsSystem { get; set; }

    /// <summary>
    /// 数据范围
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：1
    /// 说明：数据权限范围：全部数据(1)、本部门及以下数据(2)、本部门数据(3)、仅本人数据(4)、自定义数据(5)
    /// 用于控制用户可以访问的数据范围
    /// </remarks>
    [SugarColumn(ColumnName = "data_scope", ColumnDescription = "数据范围", ColumnDataType = "int", IsNullable = false)]
    public LeanDataScope DataScope { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（nvarchar）
    /// 长度：500
    /// 允许为空：是
    /// 默认值：null
    /// 说明：角色的补充说明信息
    /// 最多500个字符
    /// </remarks>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Remark { get; set; }

    /// <summary>
    /// 用户角色关联列表
    /// </summary>
    /// <remarks>
    /// 导航属性：角色的用户关联列表
    /// 关联类型：一对多
    /// 关联字段：RoleId
    /// 说明：通过RoleId关联到用户角色关联表
    /// </remarks>
    [Navigate(NavigateType.OneToMany, nameof(LeanUserRole.RoleId))]
    public List<LeanUserRole> UserRoles { get; set; } = new();

    /// <summary>
    /// 角色菜单关联列表
    /// </summary>
    /// <remarks>
    /// 导航属性：角色的菜单关联列表
    /// 关联类型：一对多
    /// 关联字段：RoleId
    /// 说明：通过RoleId关联到角色菜单关联表
    /// </remarks>
    [Navigate(NavigateType.OneToMany, nameof(LeanRoleMenu.RoleId))]
    public List<LeanRoleMenu> RoleMenus { get; set; } = new();
} 
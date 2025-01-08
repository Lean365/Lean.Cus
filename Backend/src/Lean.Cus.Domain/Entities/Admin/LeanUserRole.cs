using Lean.Cus.Common.Entities;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 用户角色关联实体
/// </summary>
/// <remarks>
/// 用户角色关联表用于存储用户与角色的多对多关系。
/// 一个用户可以分配多个角色。
/// 一个角色可以分配给多个用户。
/// 通过此表可以实现用户的角色权限控制。
/// 当用户被删除时，相关的角色关联记录也会被级联删除。
/// 当角色被删除时，相关的用户关联记录也会被级联删除。
/// </remarks>
[SugarTable("lean_user_role", "用户角色关联表")]
[SugarIndex("idx_user_role_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("idx_user_role_role_id", nameof(RoleId), OrderByType.Asc)]
public class LeanUserRole : LeanEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    /// <remarks>
    /// 数据类型：长整型（bigint）
    /// 允许为空：否
    /// 默认值：无
    /// 说明：关联到用户表的主键ID
    /// 已建立索引：idx_user_role_user_id
    /// 外键关系：lean_user.id
    /// </remarks>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long UserId { get; set; }

    /// <summary>
    /// 角色ID
    /// </summary>
    /// <remarks>
    /// 数据类型：长整型（bigint）
    /// 允许为空：否
    /// 默认值：无
    /// 说明：关联到角色表的主键ID
    /// 已建立索引：idx_user_role_role_id
    /// 外键关系：lean_role.id
    /// </remarks>
    [SugarColumn(ColumnName = "role_id", ColumnDescription = "角色ID", ColumnDataType = "bigint", IsNullable = false)]
    public long RoleId { get; set; }

    /// <summary>
    /// 用户导航属性
    /// </summary>
    /// <remarks>
    /// 导航属性：关联的用户信息
    /// 关联类型：多对一
    /// 关联字段：UserId
    /// 说明：通过UserId关联到用户表
    /// </remarks>
    [Navigate(NavigateType.ManyToOne, nameof(UserId))]
    public LeanUser? User { get; set; }

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
} 
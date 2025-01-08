using Lean.Cus.Common.Entities;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 用户岗位关联实体
/// </summary>
/// <remarks>
/// 用户岗位关联表用于存储用户与岗位的多对多关系。
/// 一个用户可以分配多个岗位。
/// 一个岗位可以分配给多个用户。
/// 通过此表可以实现用户的岗位管理。
/// 当用户被删除时，相关的岗位关联记录也会被级联删除。
/// 当岗位被删除时，相关的用户关联记录也会被级联删除。
/// </remarks>
[SugarTable("lean_user_post", "用户岗位关联表")]
[SugarIndex("idx_user_post_user_id", nameof(UserId), OrderByType.Asc)]
[SugarIndex("idx_user_post_post_id", nameof(PostId), OrderByType.Asc)]
public class LeanUserPost : LeanEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    /// <remarks>
    /// 数据类型：长整型（bigint）
    /// 允许为空：否
    /// 默认值：无
    /// 说明：关联到用户表的主键ID
    /// 已建立索引：idx_user_post_user_id
    /// 外键关系：lean_user.id
    /// </remarks>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID", ColumnDataType = "bigint", IsNullable = false)]
    public long UserId { get; set; }

    /// <summary>
    /// 岗位ID
    /// </summary>
    /// <remarks>
    /// 数据类型：长整型（bigint）
    /// 允许为空：否
    /// 默认值：无
    /// 说明：关联到岗位表的主键ID
    /// 已建立索引：idx_user_post_post_id
    /// 外键关系：lean_post.id
    /// </remarks>
    [SugarColumn(ColumnName = "post_id", ColumnDescription = "岗位ID", ColumnDataType = "bigint", IsNullable = false)]
    public long PostId { get; set; }

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
    /// 岗位导航属性
    /// </summary>
    /// <remarks>
    /// 导航属性：关联的岗位信息
    /// 关联类型：多对一
    /// 关联字段：PostId
    /// 说明：通过PostId关联到岗位表
    /// </remarks>
    [Navigate(NavigateType.ManyToOne, nameof(PostId))]
    public LeanPost? Post { get; set; }
} 
using System.Collections.Generic;
using Lean.Cus.Common.Entities;
using Lean.Cus.Common.Enums;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 岗位实体
/// </summary>
/// <remarks>
/// 岗位是组织中的职位设置，用于定义用户在组织中的职责和权限。
/// 一个岗位可以分配给多个用户。
/// 一个用户可以担任多个岗位。
/// 岗位有唯一的编码(Code)，用于标识岗位。
/// 岗位可以启用或禁用，禁用的岗位不参与业务处理。
/// 系统岗位不允许删除，以保证系统的基本功能。
/// </remarks>
[SugarTable("lean_post", "岗位表")]
[SugarIndex("idx_post_code", nameof(Code), OrderByType.Asc, true)]
public class LeanPost : LeanEntity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanPost()
    {
        Name = string.Empty;
        Code = string.Empty;
    }

    /// <summary>
    /// 岗位名称
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（nvarchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：岗位的显示名称，支持多语言
    /// 建议使用易于理解的名称
    /// </remarks>
    [SugarColumn(ColumnName = "name", ColumnDescription = "岗位名称", ColumnDataType = "nvarchar", Length = 50, IsNullable = false)]
    public string Name { get; set; }

    /// <summary>
    /// 岗位编码
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：岗位的唯一标识，系统中不允许重复
    /// 建议使用有意义的编码，如：manager、developer
    /// 已建立唯一索引：idx_post_code
    /// </remarks>
    [SugarColumn(ColumnName = "code", ColumnDescription = "岗位编码", ColumnDataType = "varchar", Length = 50, IsNullable = false)]
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
    /// 说明：岗位状态：启用(1)、禁用(0)
    /// 禁用的岗位不参与业务处理
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", ColumnDataType = "int", IsNullable = false)]
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 是否系统岗位
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：0
    /// 说明：是否是系统内置岗位：是(1)、否(0)
    /// 系统岗位不允许删除，以保证系统的基本功能
    /// </remarks>
    [SugarColumn(ColumnName = "is_system", ColumnDescription = "是否系统岗位", ColumnDataType = "int", IsNullable = false)]
    public int IsSystem { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（nvarchar）
    /// 长度：500
    /// 允许为空：是
    /// 默认值：null
    /// 说明：岗位的补充说明信息
    /// 最多500个字符
    /// </remarks>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", ColumnDataType = "nvarchar", Length = 500, IsNullable = true)]
    public string? Remark { get; set; }

    /// <summary>
    /// 用户岗位关联列表
    /// </summary>
    /// <remarks>
    /// 导航属性：岗位的用户关联列表
    /// 关联类型：一对多
    /// 关联字段：PostId
    /// 说明：通过PostId关联到用户岗位关联表
    /// </remarks>
    [Navigate(NavigateType.OneToMany, nameof(LeanUserPost.PostId))]
    public List<LeanUserPost> UserPosts { get; set; } = new();

    /// <summary>
    /// 用户列表
    /// </summary>
    /// <remarks>
    /// 导航属性：岗位的用户列表
    /// 关联类型：多对多
    /// 关联字段：PostId
    /// 说明：通过UserPost中间表关联到用户列表
    /// </remarks>
    [Navigate(NavigateType.OneToMany, nameof(LeanUserPost.PostId))]
    public List<LeanUser> Users { get; set; } = new();
} 
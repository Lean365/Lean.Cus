using System.Collections.Generic;
using Lean.Cus.Common.Entities;
using Lean.Cus.Common.Enums;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 部门实体
/// </summary>
/// <remarks>
/// 部门是组织架构中的基本单位，用于管理企业的组织结构。
/// 支持多级部门管理，通过ParentId构建树形结构。
/// 部门可以包含多个用户，一个用户只能属于一个部门。
/// </remarks>
[SugarTable("lean_dept", "部门表")]
[SugarIndex("idx_dept_code", nameof(Code), OrderByType.Asc, true)]
[SugarIndex("idx_dept_parent", nameof(ParentId), OrderByType.Asc)]
public class LeanDept : LeanEntity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanDept()
    {
        Name = string.Empty;
        Code = string.Empty;
        Children = new List<LeanDept>();
        Users = new List<LeanUser>();
    }

    /// <summary>
    /// 父级ID
    /// </summary>
    /// <remarks>
    /// 数据类型：长整型（bigint）
    /// 允许为空：是
    /// 默认值：null
    /// 说明：上级部门的ID，如果为null则表示是顶级部门
    /// 删除部门时需要确保没有子部门
    /// </remarks>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", IsNullable = true, ColumnDataType = "bigint")]
    public long? ParentId { get; set; }

    /// <summary>
    /// 部门名称
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：部门的显示名称，同一父级下不允许重名
    /// 建议长度在2-50个字符之间
    /// </remarks>
    [SugarColumn(ColumnName = "name", ColumnDescription = "部门名称", Length = 50, IsNullable = false, ColumnDataType = "varchar")]
    public string Name { get; set; }

    /// <summary>
    /// 部门编码
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：部门的唯一标识码，整个系统中不允许重复
    /// 建议使用有意义的编码规则，如：总部:HQ，分部:BR-001
    /// 已建立唯一索引：idx_dept_code
    /// </remarks>
    [SugarColumn(ColumnName = "code", ColumnDescription = "部门编码", Length = 50, IsNullable = false, ColumnDataType = "varchar")]
    public string Code { get; set; }

    /// <summary>
    /// 负责人
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：50
    /// 允许为空：是
    /// 默认值：null
    /// 说明：部门负责人姓名，可选字段
    /// 建议与用户表进行关联
    /// </remarks>
    [SugarColumn(ColumnName = "leader", ColumnDescription = "负责人", Length = 50, IsNullable = true, ColumnDataType = "varchar")]
    public string? Leader { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：20
    /// 允许为空：是
    /// 默认值：null
    /// 说明：部门联系电话，可选字段
    /// 支持座机或手机号码格式
    /// </remarks>
    [SugarColumn(ColumnName = "phone", ColumnDescription = "联系电话", Length = 20, IsNullable = true, ColumnDataType = "varchar")]
    public string? Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：50
    /// 允许为空：是
    /// 默认值：null
    /// 说明：部门联系邮箱，可选字段
    /// 需要验证邮箱格式的有效性
    /// </remarks>
    [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", Length = 50, IsNullable = true, ColumnDataType = "varchar")]
    public string? Email { get; set; }

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
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序", IsNullable = false, DefaultValue = "0", ColumnDataType = "int")]
    public int OrderNum { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：1
    /// 说明：部门状态：启用(1)、禁用(0)
    /// 禁用后，该部门下的用户将无法登录系统
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, DefaultValue = "1", ColumnDataType = "int")]
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：500
    /// 允许为空：是
    /// 默认值：null
    /// 说明：部门的补充说明信息
    /// 最多500个字符
    /// </remarks>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, IsNullable = true, ColumnDataType = "varchar")]
    public string? Remark { get; set; }

    /// <summary>
    /// 父级部门
    /// </summary>
    /// <remarks>
    /// 导航属性：上级部门
    /// 关联类型：一对一
    /// 关联字段：ParentId
    /// 说明：通过ParentId关联到父级部门
    /// </remarks>
    [Navigate(NavigateType.OneToOne, nameof(ParentId))]
    public LeanDept? Parent { get; set; }

    /// <summary>
    /// 子部门
    /// </summary>
    /// <remarks>
    /// 导航属性：下级部门列表
    /// 关联类型：一对多
    /// 关联字段：ParentId
    /// 说明：通过ParentId关联到子部门列表
    /// </remarks>
    [Navigate(NavigateType.OneToMany, nameof(ParentId))]
    public List<LeanDept> Children { get; set; }

    /// <summary>
    /// 用户列表
    /// </summary>
    /// <remarks>
    /// 导航属性：部门下的用户列表
    /// 关联类型：一对多
    /// 关联字段：DeptId
    /// 说明：通过User表的DeptId关联到用户列表
    /// </remarks>
    [Navigate(NavigateType.OneToMany, nameof(LeanUser.DeptId))]
    public List<LeanUser> Users { get; set; }
} 
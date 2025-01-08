using System;
using System.Collections.Generic;
using Lean.Cus.Common.Entities;
using Lean.Cus.Common.Enums;
using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 用户实体
/// </summary>
/// <remarks>
/// 用户是系统的操作主体，用于记录系统使用者的基本信息。
/// 用户可以分配多个角色，通过角色获取相应的权限。
/// 用户可以分配多个岗位，表示其在组织中的职责。
/// 用户必须属于某个部门，部门用于组织架构管理。
/// 用户可以启用或禁用，禁用的用户无法登录系统。
/// 系统用户不允许删除，以保证系统的基本功能。
/// 用户密码采用加密存储，不可逆。
/// </remarks>
[SugarTable("lean_user", "用户表")]
[SugarIndex("idx_user_username", nameof(Username), OrderByType.Asc, true)]
[SugarIndex("idx_user_dept_id", nameof(DeptId), OrderByType.Asc)]
public class LeanUser : LeanEntity
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanUser()
    {
        Username = string.Empty;
        Password = string.Empty;
        Nickname = string.Empty;
        RealName = string.Empty;
        Avatar = string.Empty;
        Phone = string.Empty;
        Email = string.Empty;
    }

    /// <summary>
    /// 用户名
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：用户登录账号，系统中唯一
    /// 建议使用字母、数字组合
    /// 已建立唯一索引：idx_user_username
    /// </remarks>
    [SugarColumn(ColumnName = "username", ColumnDescription = "用户名", Length = 50, IsNullable = false, ColumnDataType = "varchar")]
    public string Username { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：100
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：用户登录密码，采用加密存储
    /// 密码规则：字母、数字、特殊字符组合，长度8-20位
    /// </remarks>
    [SugarColumn(ColumnName = "password", ColumnDescription = "密码", Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string Password { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（nvarchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：用户昵称，用于显示
    /// 支持中文等特殊字符
    /// </remarks>
    [SugarColumn(ColumnName = "nickname", ColumnDescription = "昵称", Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string Nickname { get; set; }

    /// <summary>
    /// 真实姓名
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（nvarchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：用户真实姓名
    /// 用于实名认证和管理
    /// </remarks>
    [SugarColumn(ColumnName = "real_name", ColumnDescription = "真实姓名", Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string RealName { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：500
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：用户头像URL地址
    /// 支持相对路径或完整URL
    /// </remarks>
    [SugarColumn(ColumnName = "avatar", ColumnDescription = "头像", Length = 500, IsNullable = false, ColumnDataType = "varchar")]
    public string Avatar { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：0
    /// 说明：用户性别：未知(0)、男(1)、女(2)
    /// </remarks>
    [SugarColumn(ColumnName = "gender", ColumnDescription = "性别", IsNullable = false, ColumnDataType = "int")]
    public LeanGender Gender { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：20
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：用户手机号码
    /// 用于登录和通知
    /// </remarks>
    [SugarColumn(ColumnName = "phone", ColumnDescription = "手机号码", Length = 20, IsNullable = false, ColumnDataType = "varchar")]
    public string Phone { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：50
    /// 允许为空：否
    /// 默认值：空字符串
    /// 说明：用户邮箱地址
    /// 用于登录和通知
    /// </remarks>
    [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", Length = 50, IsNullable = false, ColumnDataType = "varchar")]
    public string Email { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    /// <remarks>
    /// 数据类型：长整型（bigint）
    /// 允许为空：否
    /// 默认值：无
    /// 说明：用户所属部门ID
    /// 已建立索引：idx_user_dept_id
    /// 外键关系：lean_dept.id
    /// </remarks>
    [SugarColumn(ColumnName = "dept_id", ColumnDescription = "部门ID", IsNullable = false, ColumnDataType = "bigint")]
    public long DeptId { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：1
    /// 说明：用户状态：启用(1)、禁用(0)
    /// 禁用的用户无法登录系统
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", IsNullable = false, ColumnDataType = "int")]
    public LeanEnabledStatus Status { get; set; }

    /// <summary>
    /// 是否系统用户
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：0
    /// 说明：是否是系统内置用户：是(1)、否(0)
    /// 系统用户不允许删除，以保证系统的基本功能
    /// </remarks>
    [SugarColumn(ColumnName = "is_system", ColumnDescription = "是否系统用户", IsNullable = false, ColumnDataType = "int")]
    public int IsSystem { get; set; }

    /// <summary>
    /// 最后登录IP
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（varchar）
    /// 长度：50
    /// 允许为空：是
    /// 默认值：null
    /// 说明：用户最后一次登录的IP地址
    /// 用于安全审计
    /// </remarks>
    [SugarColumn(ColumnName = "last_login_ip", ColumnDescription = "最后登录IP", Length = 50, IsNullable = true, ColumnDataType = "varchar")]
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    /// <remarks>
    /// 数据类型：日期时间（datetime）
    /// 允许为空：是
    /// 默认值：null
    /// 说明：用户最后一次登录的时间
    /// 用于安全审计
    /// </remarks>
    [SugarColumn(ColumnName = "last_login_time", ColumnDescription = "最后登录时间", IsNullable = true, ColumnDataType = "datetime")]
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    /// <remarks>
    /// 数据类型：字符串（nvarchar）
    /// 长度：500
    /// 允许为空：是
    /// 默认值：null
    /// 说明：用户的补充说明信息
    /// 最多500个字符
    /// </remarks>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? Remark { get; set; }

    /// <summary>
    /// 部门导航属性
    /// </summary>
    /// <remarks>
    /// 导航属性：关联的部门信息
    /// 关联类型：多对一
    /// 关联字段：DeptId
    /// 说明：通过DeptId关联到部门表
    /// </remarks>
    [Navigate(NavigateType.ManyToOne, nameof(DeptId))]
    public LeanDept? Dept { get; set; }

    /// <summary>
    /// 用户角色关联列表
    /// </summary>
    /// <remarks>
    /// 导航属性：用户的角色关联列表
    /// 关联类型：一对多
    /// 关联字段：UserId
    /// 说明：通过UserId关联到用户角色关联表
    /// </remarks>
    [Navigate(NavigateType.OneToMany, nameof(LeanUserRole.UserId))]
    public List<LeanUserRole> UserRoles { get; set; } = new();

    /// <summary>
    /// 用户岗位列表
    /// </summary>
    /// <remarks>
    /// 导航属性：用户的岗位列表
    /// 关联类型：多对多
    /// 关联字段：PostId
    /// 说明：通过UserPost中间表关联到岗位列表
    /// </remarks>
    [Navigate(NavigateType.ManyToMany, nameof(LeanUserPost))]
    public List<LeanPost> Posts { get; set; } = new();
} 
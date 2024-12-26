//===================================================
// 项目名称：Lean.Cus.Domain
// 文件名称：LeanSysUser
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.14
// 更新时间：2024.01.14
// 备注说明：系统用户实体
//===================================================

using SqlSugar;
using Lean.Cus.Domain.Entities;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 系统用户实体
/// </summary>
[SugarTable("Lean_sys_user", "系统用户表")]
public class LeanSysUser : LeanBaseEntity<long>
{
    /// <summary>
    /// 用户编码
    /// </summary>
    [SugarColumn(ColumnName = "user_code", ColumnDescription = "用户编码", Length = 50, IsNullable = false)]
    public string UserCode { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", Length = 50, IsNullable = false)]
    public string UserName { get; set; }

    /// <summary>
    /// 昵称
    /// </summary>
    [SugarColumn(ColumnName = "nick_name", ColumnDescription = "昵称", Length = 50, IsNullable = true)]
    public string? NickName { get; set; }

    /// <summary>
    /// 英文名
    /// </summary>
    [SugarColumn(ColumnName = "english_name", ColumnDescription = "英文名", Length = 50, IsNullable = true)]
    public string? EnglishName { get; set; }

    /// <summary>
    /// 用户类型（0=超级管理员，1=管理员，2=普通用户）
    /// </summary>
    [SugarColumn(ColumnName = "user_type", ColumnDescription = "用户类型（0=超级管理员，1=管理员，2=普通用户）", IsNullable = false)]
    public int UserType { get; set; }

    /// <summary>
    /// 邮箱
    /// </summary>
    [SugarColumn(ColumnName = "email", ColumnDescription = "邮箱", Length = 100, IsNullable = true)]
    public string? Email { get; set; }

    /// <summary>
    /// 手机号码
    /// </summary>
    [SugarColumn(ColumnName = "phone_number", ColumnDescription = "手机号码", Length = 20, IsNullable = true)]
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// 性别（0=未知，1=男，2=女）
    /// </summary>
    [SugarColumn(ColumnName = "gender", ColumnDescription = "性别（0=未知，1=男，2=女）", IsNullable = false)]
    public int Gender { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    [SugarColumn(ColumnName = "avatar", ColumnDescription = "头像", Length = 500, IsNullable = true)]
    public string? Avatar { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnName = "password", ColumnDescription = "密码", Length = 100, IsNullable = false)]
    public string Password { get; set; }

    /// <summary>
    /// 盐值
    /// </summary>
    [SugarColumn(ColumnName = "salt", ColumnDescription = "密码盐值", Length = 100, IsNullable = false)]
    public string Salt { get; set; }

    /// <summary>
    /// 状态（0=禁用，1=启��）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0=禁用，1=启用）", IsNullable = false)]
    public int Status { get; set; }

    /// <summary>
    /// 首次登录IP
    /// </summary>
    [SugarColumn(ColumnName = "first_login_ip", ColumnDescription = "首次登录IP", Length = 50, IsNullable = true)]
    public string? FirstLoginIp { get; set; }

    /// <summary>
    /// 首次登录时间
    /// </summary>
    [SugarColumn(ColumnName = "first_login_time", ColumnDescription = "首次登录时间", IsNullable = true)]
    public DateTime? FirstLoginTime { get; set; }

    /// <summary>
    /// 首次登录地点
    /// </summary>
    [SugarColumn(ColumnName = "first_login_location", ColumnDescription = "首次登录地点", Length = 100, IsNullable = true)]
    public string? FirstLoginLocation { get; set; }

    /// <summary>
    /// 首次登录设备
    /// </summary>
    [SugarColumn(ColumnName = "first_login_device", ColumnDescription = "首次登录设备", Length = 100, IsNullable = true)]
    public string? FirstLoginDevice { get; set; }

    /// <summary>
    /// 最后登录IP
    /// </summary>
    [SugarColumn(ColumnName = "last_login_ip", ColumnDescription = "最后登录IP", Length = 50, IsNullable = true)]
    public string? LastLoginIp { get; set; }

    /// <summary>
    /// 最后登录时间
    /// </summary>
    [SugarColumn(ColumnName = "last_login_time", ColumnDescription = "最后登录时间", IsNullable = true)]
    public DateTime? LastLoginTime { get; set; }

    /// <summary>
    /// 最后登录地点
    /// </summary>
    [SugarColumn(ColumnName = "last_login_location", ColumnDescription = "最后登录地点", Length = 100, IsNullable = true)]
    public string? LastLoginLocation { get; set; }

    /// <summary>
    /// 最后登录设备
    /// </summary>
    [SugarColumn(ColumnName = "last_login_device", ColumnDescription = "最后登录设备", Length = 100, IsNullable = true)]
    public string? LastLoginDevice { get; set; }

    /// <summary>
    /// 最后退出时间
    /// </summary>
    [SugarColumn(ColumnName = "last_logout_time", ColumnDescription = "最后退出时间", IsNullable = true)]
    public DateTime? LastLogoutTime { get; set; }

    /// <summary>
    /// 登录次数
    /// </summary>
    [SugarColumn(ColumnName = "login_count", ColumnDescription = "登录次数", IsNullable = false)]
    public int LoginCount { get; set; }
} 
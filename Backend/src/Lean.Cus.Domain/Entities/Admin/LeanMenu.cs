//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件的所有修改。
// </auto-generated>
//
// <copyright file="LeanMenu.cs" company="Lean365">
//     Copyright (c) Lean365. All rights reserved.
// </copyright>
// <author>Lean365</author>
// <date>2024-01-09</date>
// <summary>
// 菜单实体类
// </summary>
//------------------------------------------------------------------------------

using System;
using SqlSugar;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 菜单实体
/// </summary>
[SugarTable("lean_admin_menu", "菜单表")]
[SugarIndex("idx_menu_code", nameof(MenuCode), OrderByType.Asc, true)]
public class LeanMenu : LeanBaseEntity
{
    /// <summary>
    /// 菜单名称
    /// </summary>
    [SugarColumn(ColumnName = "name", ColumnDescription = "菜单名称", 
        Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string MenuName { get; set; } = string.Empty;

    /// <summary>
    /// 菜单编码
    /// </summary>
    [SugarColumn(ColumnName = "code", ColumnDescription = "菜单编码", 
        Length = 50, IsNullable = false, ColumnDataType = "varchar")]
    public string MenuCode { get; set; } = string.Empty;

    /// <summary>
    /// 父级ID
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", 
        IsNullable = false, ColumnDataType = "bigint")]
    public long ParentId { get; set; }

    /// <summary>
    /// 路由地址
    /// </summary>
    [SugarColumn(ColumnName = "path", ColumnDescription = "路由地址", 
        Length = 200, IsNullable = true, ColumnDataType = "varchar")]
    public string? Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [SugarColumn(ColumnName = "component", ColumnDescription = "组件路径", 
        Length = 200, IsNullable = true, ColumnDataType = "varchar")]
    public string? Component { get; set; }

    /// <summary>
    /// 重定向地址
    /// </summary>
    [SugarColumn(ColumnName = "redirect", ColumnDescription = "重定向地址", 
        Length = 200, IsNullable = true, ColumnDataType = "varchar")]
    public string? Redirect { get; set; }

    /// <summary>
    /// 菜单类型（1：目录，2：菜单，3：按钮）
    /// </summary>
    [SugarColumn(ColumnName = "menu_type", ColumnDescription = "菜单类型（1：目录，2：菜单，3：按钮）", 
        IsNullable = false, ColumnDataType = "int")]
    public LeanMenuType MenuType { get; set; }

    /// <summary>
    /// 菜单图标
    /// </summary>
    [SugarColumn(ColumnName = "icon", ColumnDescription = "菜单图标", 
        Length = 100, IsNullable = true, ColumnDataType = "varchar")]
    public string? Icon { get; set; }

    /// <summary>
    /// 权限标识
    /// </summary>
    [SugarColumn(ColumnName = "permission_code", ColumnDescription = "权限标识", 
        Length = 100, IsNullable = true, ColumnDataType = "varchar")]
    public string? PermissionCode { get; set; }

    /// <summary>
    /// 排序号
    /// </summary>
    [SugarColumn(ColumnName = "order_num", ColumnDescription = "排序号", 
        IsNullable = false, ColumnDataType = "int")]
    public int OrderNum { get; set; }

    /// <summary>
    /// 是否可见（0隐藏 1显示）
    /// </summary>
    [SugarColumn(ColumnName = "visible", ColumnDescription = "是否可见（0隐藏 1显示）", 
        IsNullable = false, ColumnDataType = "int")]
    public int Visible { get; set; } = 0;

    /// <summary>
    /// 是否缓存（0不缓存 1缓存）
    /// </summary>
    [SugarColumn(ColumnName = "is_cache", ColumnDescription = "是否缓存（0不缓存 1缓存）",
        IsNullable = false, ColumnDataType = "int")]
    public int IsCache { get; set; } = 0;

    /// <summary>
    /// 是否外链（0否 1是）
    /// </summary>
    [SugarColumn(ColumnName = "is_frame", ColumnDescription = "是否外链（0否 1是）",
        IsNullable = false, ColumnDataType = "int")]
    public int IsFrame { get; set; } = 0;

    /// <summary>
    /// 状态（0：禁用，1：启用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0：禁用，1：启用）", 
        IsNullable = false, ColumnDataType = "int")]
    public LeanStatus Status { get; set; } = LeanStatus.Enabled;

    /// <summary>
    /// 翻译键
    /// </summary>
    [SugarColumn(ColumnName = "trans_key", ColumnDescription = "翻译键",
        Length = 100, IsNullable = true, ColumnDataType = "varchar")]
    public string? TransKey { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", 
        Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? Remark { get; set; }
} 
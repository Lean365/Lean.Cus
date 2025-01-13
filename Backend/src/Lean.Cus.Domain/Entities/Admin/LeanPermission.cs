using SqlSugar;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 权限实体
/// </summary>
[SugarTable("lean_admin_permission", "权限表")]
[SugarIndex("idx_permission_code", nameof(Code), OrderByType.Asc, true)]
public class LeanPermission : LeanBaseEntity
{
    /// <summary>
    /// 权限名称
    /// </summary>
    [SugarColumn(ColumnName = "permission_name", ColumnDescription = "权限名称", 
        Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    [SugarColumn(ColumnName = "permission_code", ColumnDescription = "权限编码", 
        Length = 50, IsNullable = false, ColumnDataType = "varchar")]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 权限类型（1：菜单，2：按钮，3：接口）
    /// </summary>
    [SugarColumn(ColumnName = "permission_type", ColumnDescription = "权限类型（1：菜单，2：按钮，3：接口）", 
        IsNullable = false, ColumnDataType = "int")]
    public LeanPermissionType Type { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    [SugarColumn(ColumnName = "parent_id", ColumnDescription = "父级ID", 
        IsNullable = true, ColumnDataType = "bigint")]
    public long? ParentId { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [SugarColumn(ColumnName = "sort", ColumnDescription = "排序", 
        IsNullable = false, ColumnDataType = "int")]
    public int Sort { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [SugarColumn(ColumnName = "icon", ColumnDescription = "图标", 
        Length = 50, IsNullable = true, ColumnDataType = "varchar")]
    public string? Icon { get; set; }

    /// <summary>
    /// 路由路径
    /// </summary>
    [SugarColumn(ColumnName = "path", ColumnDescription = "路由路径", 
        Length = 200, IsNullable = true, ColumnDataType = "varchar")]
    public string? Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [SugarColumn(ColumnName = "component", ColumnDescription = "组件路径", 
        Length = 200, IsNullable = true, ColumnDataType = "varchar")]
    public string? Component { get; set; }

    /// <summary>
    /// 状态（0：禁用，1：启用）
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态（0：禁用，1：启用）", 
        IsNullable = false, ColumnDataType = "int")]
    public LeanStatus Status { get; set; } = LeanStatus.Enabled;

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnName = "remark", ColumnDescription = "备注", 
        Length = 500, IsNullable = true, ColumnDataType = "nvarchar")]
    public string? Remark { get; set; }
} 
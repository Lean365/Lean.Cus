using System;
using SqlSugar;

namespace Lean.Cus.Domain.Entities;

/// <summary>
/// 实体基类
/// </summary>
public abstract class LeanBaseEntity
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id", 
        ColumnDescription = "主键", ColumnDataType = "bigint")]
    public long Id { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    [SugarColumn(ColumnName = "create_by", ColumnDescription = "创建者", 
        IsNullable = false, Length = 50, ColumnDataType = "nvarchar")]
    public string CreateBy { get; set; } = string.Empty;

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnName = "create_time", ColumnDescription = "创建时间", 
        IsNullable = false, ColumnDataType = "datetime")]
    public DateTime CreateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新者
    /// </summary>
    [SugarColumn(ColumnName = "update_by", ColumnDescription = "更新者", 
        IsNullable = true, Length = 50, ColumnDataType = "nvarchar")]
    public string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [SugarColumn(ColumnName = "update_time", ColumnDescription = "更新时间", 
        IsNullable = true, ColumnDataType = "datetime")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 是否删除（0：未删除，1：已删除）
    /// </summary>
    [SugarColumn(ColumnName = "is_deleted", ColumnDescription = "是否删除（0：未删除，1：已删除）", 
        IsNullable = false, ColumnDataType = "int")]
    public int IsDeleted { get; set; } = 0;

    /// <summary>
    /// 删除者
    /// </summary>
    [SugarColumn(ColumnName = "delete_by", ColumnDescription = "删除者", 
        IsNullable = true, Length = 50, ColumnDataType = "nvarchar")]
    public string? DeleteBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [SugarColumn(ColumnName = "delete_time", ColumnDescription = "删除时间", 
        IsNullable = true, ColumnDataType = "datetime")]
    public DateTime? DeleteTime { get; set; }
} 
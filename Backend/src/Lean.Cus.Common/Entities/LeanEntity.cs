using System;
using SqlSugar;

namespace Lean.Cus.Common.Entities;

/// <summary>
/// 基础实体
/// </summary>
public abstract class LeanEntity
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(ColumnName = "id", ColumnDescription = "主键", IsPrimaryKey = true, IsIdentity = true, ColumnDataType = "bigint")]
    public long Id { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    [SugarColumn(ColumnName = "create_by", ColumnDescription = "创建者", IsNullable = false, ColumnDataType = "bigint")]
    public long CreateBy { get; set; }

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnName = "create_time", ColumnDescription = "创建时间", IsNullable = false, ColumnDataType = "datetime")]
    public DateTime CreateTime { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    [SugarColumn(ColumnName = "update_by", ColumnDescription = "更新者", IsNullable = true, ColumnDataType = "bigint")]
    public long? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [SugarColumn(ColumnName = "update_time", ColumnDescription = "更新时间", IsNullable = true, ColumnDataType = "datetime")]
    public DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 删除者
    /// </summary>
    [SugarColumn(ColumnName = "delete_by", ColumnDescription = "删除者", IsNullable = true, ColumnDataType = "bigint")]
    public long? DeleteBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [SugarColumn(ColumnName = "delete_time", ColumnDescription = "删除时间", IsNullable = true, ColumnDataType = "datetime")]
    public DateTime? DeleteTime { get; set; }

    /// <summary>
    /// 是否删除
    /// </summary>
    /// <remarks>
    /// 数据类型：整型（int）
    /// 允许为空：否
    /// 默认值：0
    /// 说明：标记记录是否已删除，0=未删除，1=已删除
    /// </remarks>
    [SugarColumn(ColumnName = "is_deleted", ColumnDescription = "是否删除", IsNullable = false, ColumnDataType = "int", DefaultValue = "0")]
    public int IsDeleted { get; set; }
} 
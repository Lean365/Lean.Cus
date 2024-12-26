//===================================================
// 项目名称：Lean.Cus.Domain.Entities
// 文件名称：LeanBaseEntity
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：实体基类
//===================================================

using System.ComponentModel.DataAnnotations;
using Lean.Cus.Common.Utils;
using SqlSugar;
using Lean.Cus.Domain.Interfaces;
using Newtonsoft.Json;
namespace Lean.Cus.Domain.Entities;

/// <summary>
/// 实体基类
/// </summary>
/// <typeparam name="TKey">主键类型</typeparam>
public abstract class LeanBaseEntity<TKey> : ILeanBaseEntity<TKey>
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(ColumnDescription = "主键", IsPrimaryKey = true)]
    public virtual TKey Id { get; set; }

    /// <summary>
    /// 创建者ID
    /// </summary>
    [SugarColumn(ColumnDescription = "创建者ID", IsNullable = false)]
    public virtual long CreateId { get; set; }

    /// <summary>
    /// 创建者
    /// </summary>
    [SugarColumn(ColumnDescription = "创建者", IsNullable = false, Length = 50)]
    public virtual string CreateBy { get; set; } = "System";

    /// <summary>
    /// 创建时间
    /// </summary>
    [SugarColumn(ColumnDescription = "创建时间", IsNullable = false)]
    public virtual DateTime CreateTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新者ID
    /// </summary>
    [SugarColumn(ColumnDescription = "更新者ID", IsNullable = true)]
    public virtual long? UpdateId { get; set; }

    /// <summary>
    /// 更新者
    /// </summary>
    [SugarColumn(ColumnDescription = "更新者", IsNullable = true, Length = 50)]
    public virtual string? UpdateBy { get; set; }

    /// <summary>
    /// 更新时间
    /// </summary>
    [SugarColumn(ColumnDescription = "更新时间", IsNullable = true)]
    public virtual DateTime? UpdateTime { get; set; }

    /// <summary>
    /// 删除者ID
    /// </summary>
    [SugarColumn(ColumnDescription = "删除者ID", IsNullable = true)]
    public virtual long? DeleteId { get; set; }

    /// <summary>
    /// 删除者
    /// </summary>
    [SugarColumn(ColumnDescription = "删除者", IsNullable = true, Length = 50)]
    public virtual string? DeleteBy { get; set; }

    /// <summary>
    /// 删除时间
    /// </summary>
    [SugarColumn(ColumnDescription = "删除时间", IsNullable = true)]
    public virtual DateTime? DeleteTime { get; set; }

    /// <summary>
    /// 是否已删除（0代表存在 1代表删除）
    /// </summary>
    [SugarColumn(ColumnDescription = "是否删除（0=未删除，1=已删除）", IsNullable = false)]
    public virtual int IsDeleted { get; set; } = 0;

    /// <summary>
    /// 租户ID
    /// </summary>
    [SugarColumn(ColumnDescription = "租户ID", IsNullable = true)]
    public virtual long? TenantId { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [SugarColumn(ColumnDescription = "备注", IsNullable = true, Length = 500)]
    public virtual string? Remark { get; set; }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    protected LeanBaseEntity() { }

    /// <summary>
    /// 使用指定ID初始化实体
    /// </summary>
    /// <param name="id">实体ID</param>
    protected LeanBaseEntity(TKey id)
    {
        Id = id;
    }
}

/// <summary>
/// Int类型主键实体基类（自增长）
/// </summary>
public abstract class LeanIntEntity : LeanBaseEntity<int>
{
    /// <summary>
    /// 主键（自增长）
    /// </summary>
    [SugarColumn(ColumnDescription = "自增主键",IsPrimaryKey = true, IsIdentity = true)]
    public override int Id { get; set; }
}

/// <summary>
/// Long类型主键实体基类（雪花算法）
/// </summary>
public abstract class LeanLongEntity : LeanBaseEntity<long>
{
    /// <summary>
    /// 主键（雪花算法）
    /// </summary>
    [SugarColumn(ColumnDescription = "雪花主键",IsPrimaryKey = true)]
    [JsonConverter(typeof(ValueToStringConverter))]
    public override long Id { get; set; }

    /// <summary>
    /// 构造函数，使用雪花算法生成ID
    /// </summary>
    protected LeanLongEntity()
    {
        Id = LeanSnowflake.Instance.NextId();
    }
}

/// <summary>
/// String类型主键实体基类（GUID）
/// </summary>
public abstract class LeanGuidEntity : LeanBaseEntity<string>
{
    /// <summary>
    /// 主键（GUID）
    /// </summary>
    [SugarColumn(ColumnDescription = "Guid主键",IsPrimaryKey = true, Length = 36)]
    public override string Id { get; set; }

    /// <summary>
    /// 构造函数，使用GUID生成ID
    /// </summary>
    protected LeanGuidEntity()
    {
        Id = Guid.NewGuid().ToString("N");
    }
} 
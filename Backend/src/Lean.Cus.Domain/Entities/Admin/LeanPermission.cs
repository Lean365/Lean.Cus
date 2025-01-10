using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 权限实体
/// </summary>
[SugarTable("lean_admin_permission")]
public class LeanPermission
{
    /// <summary>
    /// 主键
    /// </summary>
    [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
    public long Id { get; set; }

    /// <summary>
    /// 权限名称
    /// </summary>
    [SugarColumn(Length = 50)]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 权限编码
    /// </summary>
    [SugarColumn(Length = 50)]
    public string Code { get; set; } = string.Empty;

    /// <summary>
    /// 权限类型（菜单、按钮）
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    public long? ParentId { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 图标
    /// </summary>
    [SugarColumn(Length = 50)]
    public string? Icon { get; set; }

    /// <summary>
    /// 路由路径
    /// </summary>
    [SugarColumn(Length = 200)]
    public string? Path { get; set; }

    /// <summary>
    /// 组件路径
    /// </summary>
    [SugarColumn(Length = 200)]
    public string? Component { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsEnabled { get; set; } = true;

    /// <summary>
    /// 创建时间
    /// </summary>
    public DateTime CreatedTime { get; set; } = DateTime.Now;

    /// <summary>
    /// 更新时间
    /// </summary>
    public DateTime? UpdatedTime { get; set; }
} 
namespace Lean.Cus.Common.Paging;

/// <summary>
/// 分页DTO基类
/// </summary>
public class LeanPagedDto
{
    /// <summary>
    /// 当前页码
    /// </summary>
    public int Current { get; set; } = 1;

    /// <summary>
    /// 每页大小
    /// </summary>
    public int Size { get; set; } = 10;

    /// <summary>
    /// 排序字段
    /// </summary>
    public string? OrderBy { get; set; }

    /// <summary>
    /// 是否降序
    /// </summary>
    public bool IsDesc { get; set; }
} 
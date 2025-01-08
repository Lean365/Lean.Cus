using System.Collections.Generic;

namespace Lean.Cus.Common.Paging;

/// <summary>
/// 分页列表
/// </summary>
/// <typeparam name="T">数据类型</typeparam>
public class LeanPagedList<T>
{
    /// <summary>
    /// 当前页码
    /// </summary>
    public int Current { get; set; }

    /// <summary>
    /// 每页大小
    /// </summary>
    public int Size { get; set; }

    /// <summary>
    /// 总记录数
    /// </summary>
    public long Total { get; set; }

    /// <summary>
    /// 总页数
    /// </summary>
    public long Pages { get; set; }

    /// <summary>
    /// 数据列表
    /// </summary>
    public List<T> Records { get; set; } = new();
} 
using System.ComponentModel.DataAnnotations;

namespace Lean.Cus.Common.Paging;

/// <summary>
/// 分页查询参数
/// </summary>
public class PagedQuery
{
    /// <summary>
    /// 页码(从1开始)
    /// </summary>
    [Range(1, int.MaxValue)]
    public int PageIndex { get; set; } = 1;

    /// <summary>
    /// 每页记录数
    /// </summary>
    [Range(1, int.MaxValue)]
    public int PageSize { get; set; } = 10;
} 
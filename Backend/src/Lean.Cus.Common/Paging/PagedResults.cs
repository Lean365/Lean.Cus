using System.Collections.Generic;

namespace Lean.Cus.Common.Paging
{
    /// <summary>
    /// 分页结果
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class PagedResults<T>
    {
        /// <summary>
        /// 数据列表
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int Total { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount => Total == 0 ? 0 : (Total + PageSize - 1) / PageSize;
    }
} 
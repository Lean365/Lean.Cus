//===================================================
// 项目名称：Lean.Cus.Common
// 文件名称：LeanPageResult
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.14
// 更新时间：2024.01.14
// 备注说明：通用分页结果
//===================================================

namespace Lean.Cus.Common.Models;

/// <summary>
/// 分页结果
/// </summary>
/// <typeparam name="T">数据类型</typeparam>
public class LeanPageResult<T>
{
    /// <summary>
    /// 总记录数
    /// </summary>
    public int Total { get; set; }

    /// <summary>
    /// 当前页数据
    /// </summary>
    public IList<T> Records { get; set; } = new List<T>();
} 
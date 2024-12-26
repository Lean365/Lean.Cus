//===================================================
// 项目名称：Lean.Cus.Common
// 文件名称：LeanPage
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.14
// 更新时间：2024.01.14
// 备注说明：通用分页参数
//===================================================

namespace Lean.Cus.Common.Models;

/// <summary>
/// 分页参数
/// </summary>
public class LeanPage
{
    /// <summary>
    /// 当前页码
    /// </summary>
    public int Current { get; set; } = 1;

    /// <summary>
    /// 每页大小
    /// </summary>
    public int Size { get; set; } = 10;
} 
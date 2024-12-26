//===================================================
// 项目名称：Lean.Cus.Api.Attributes
// 文件名称：LeanOperLogAttribute
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：操作日志特性
//===================================================

using Lean.Cus.Common.Enums;

namespace Lean.Cus.Api.Attributes;

/// <summary>
/// 操作日志特性
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class LeanOperLogAttribute : Attribute
{
    /// <summary>
    /// 模块标题
    /// </summary>
    public string Title { get; }

    /// <summary>
    /// 业务类型
    /// </summary>
    public LeanBusinessTypeEnum BusinessType { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="title">模块标题</param>
    /// <param name="businessType">业务类型</param>
    public LeanOperLogAttribute(string title, LeanBusinessTypeEnum businessType = LeanBusinessTypeEnum.Other)
    {
        Title = title;
        BusinessType = businessType;
    }
} 
namespace Lean.Cus.Common.Enums;

/// <summary>
/// 数据范围枚举
/// </summary>
public enum LeanDataScope
{
    /// <summary>
    /// 全部数据
    /// </summary>
    All = 1,

    /// <summary>
    /// 本部门数据
    /// </summary>
    Department = 2,

    /// <summary>
    /// 本部门及以下数据
    /// </summary>
    DepartmentAndBelow = 3,

    /// <summary>
    /// 仅本人数据
    /// </summary>
    Self = 4,

    /// <summary>
    /// 自定义数据
    /// </summary>
    Custom = 5
} 
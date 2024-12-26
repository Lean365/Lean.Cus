//===================================================
// 项目名称：Lean.Cus.Common.Enums
// 文件名称：LeanDataScopeEnum
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：数据权限范围枚举
//===================================================

namespace Lean.Cus.Common.Enums;

/// <summary>
/// 数据权限范围枚举
/// </summary>
public enum LeanDataScopeEnum
{
    /// <summary>
    /// 全部数据权限
    /// <para>可以访问所有数据</para>
    /// </summary>
    All = 1,

    /// <summary>
    /// 自定义数据权限
    /// <para>可以访问自定义的数据范围</para>
    /// </summary>
    Custom = 2,

    /// <summary>
    /// 部门数据权限
    /// <para>可以访问本部门的数据</para>
    /// </summary>
    Dept = 3,

    /// <summary>
    /// 部门及以下数据权限
    /// <para>可以访问本部门及下级部门的数据</para>
    /// </summary>
    DeptAndChild = 4,

    /// <summary>
    /// 仅本人数据权限
    /// <para>仅可以访问本人的数据</para>
    /// </summary>
    Self = 5
} 
//===================================================
// 项目名称：Lean.Cus.Api.Authorization
// 文件名称：LeanRequirement
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：授权要求
//===================================================

using Microsoft.AspNetCore.Authorization;

namespace Lean.Cus.Api.Authorization;

/// <summary>
/// 授权要求
/// </summary>
public class LeanRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// 角色列表，多个角色用逗号分隔
    /// </summary>
    public string? Roles { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roles">角色列表</param>
    public LeanRequirement(string? roles = null)
    {
        Roles = roles;
    }
} 
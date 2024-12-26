//===================================================
// 项目名称：Lean.Cus.Api.Authorization
// 文件名称：LeanPermissionHandler
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：权限处理器
//===================================================

using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Lean.Cus.Api.Authorization;

/// <summary>
/// 权限要求
/// </summary>
public class LeanPermissionRequirement : IAuthorizationRequirement
{
    /// <summary>
    /// 权限编码
    /// </summary>
    public string PermissionCode { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="permissionCode">权限编码</param>
    public LeanPermissionRequirement(string permissionCode)
    {
        PermissionCode = permissionCode;
    }
}

/// <summary>
/// 权限处理器
/// </summary>
public class LeanPermissionHandler : AuthorizationHandler<LeanPermissionRequirement>
{
    /// <summary>
    /// 处理权限要求
    /// </summary>
    /// <param name="context">上��文</param>
    /// <param name="requirement">要求</param>
    /// <returns>任务</returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeanPermissionRequirement requirement)
    {
        // 获取用户权限声明
        var permissionClaim = context.User.FindFirst("Permissions");
        if (permissionClaim == null)
        {
            return Task.CompletedTask;
        }

        // 解析权限列表
        var permissions = permissionClaim.Value.Split(',');

        // 检查是否包含所需权限
        if (permissions.Contains(requirement.PermissionCode))
        {
            context.Succeed(requirement);
        }

        return Task.CompletedTask;
    }
} 
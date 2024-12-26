//===================================================
// 项目名称：Lean.Cus.Api.Authorization
// 文件名称：LeanAuthorizationHandler
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：授权处理程序
//===================================================

using System.Linq;
using Microsoft.AspNetCore.Authorization;

namespace Lean.Cus.Api.Authorization;

/// <summary>
/// 授权处理程序
/// </summary>
public class LeanAuthorizationHandler : AuthorizationHandler<LeanRequirement>
{
    /// <summary>
    /// 处理授权
    /// </summary>
    /// <param name="context">授权上下文</param>
    /// <param name="requirement">授权要求</param>
    /// <returns></returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, LeanRequirement requirement)
    {
        // 检查用户是否已认证
        if (!context.User.Identity?.IsAuthenticated ?? false)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        // 检查角色
        if (!string.IsNullOrEmpty(requirement.Roles))
        {
            var roles = requirement.Roles.Split(',');
            if (!roles.Any(role => context.User.IsInRole(role)))
            {
                context.Fail();
                return Task.CompletedTask;
            }
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
} 
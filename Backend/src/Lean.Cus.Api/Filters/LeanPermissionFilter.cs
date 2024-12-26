//===================================================
// 项目名称：Lean.Cus.Api.Filters
// 文件名称：LeanPermissionFilter
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：权限过滤器
//===================================================

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Authorization;
using Lean.Cus.Api.Attributes;
using Lean.Cus.Api.Authorization;

namespace Lean.Cus.Api.Filters;

/// <summary>
/// 权限过滤器
/// </summary>
public class LeanPermissionFilter : IAsyncAuthorizationFilter
{
    private readonly IAuthorizationService _authorizationService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="authorizationService">授权服务</param>
    public LeanPermissionFilter(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
    }

    /// <summary>
    /// 授权过滤
    /// </summary>
    /// <param name="context">上下文</param>
    /// <returns>任务</returns>
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        // 获取权限特性
        var permissionAttributes = context.ActionDescriptor.EndpointMetadata
            .OfType<LeanPermissionAttribute>()
            .ToList();

        // 如果没有权限特性，则跳过
        if (!permissionAttributes.Any())
        {
            return;
        }

        // 检查用户是否已认证
        if (!context.HttpContext.User.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        // 检查每个权限
        foreach (var attribute in permissionAttributes)
        {
            var requirement = new LeanPermissionRequirement(attribute.PermissionCode);
            var result = await _authorizationService.AuthorizeAsync(context.HttpContext.User, null, requirement);

            if (!result.Succeeded)
            {
                context.Result = new ForbidResult();
                return;
            }
        }
    }
} 
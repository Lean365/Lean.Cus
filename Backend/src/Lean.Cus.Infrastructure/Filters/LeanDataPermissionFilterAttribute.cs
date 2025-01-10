using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Lean.Cus.Common.Security;

namespace Lean.Cus.Infrastructure.Filters;

/// <summary>
/// 数据权限过滤器
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LeanDataPermissionFilterAttribute : LeanActionFilterAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanDataPermissionFilterAttribute(ILogger<LeanDataPermissionFilterAttribute> logger)
        : base(logger)
    {
    }

    /// <summary>
    /// 执行前检查数据权限
    /// </summary>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var user = context.HttpContext.User;
        if (!user.Identity?.IsAuthenticated ?? true)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userId = LeanJwtHelper.GetUserId(user);
        var roles = LeanJwtHelper.GetRoles(user);

        // 超级管理员跳过权限检查
        if (roles.Contains("Admin"))
        {
            return;
        }

        // 检查数据权限
        foreach (var parameter in context.ActionArguments)
        {
            if (parameter.Value is IDataPermission dataPermission)
            {
                if (!HasDataPermission(userId, roles, dataPermission))
                {
                    Logger.LogWarning($"用户 {userId} 没有数据权限");
                    context.Result = new ForbidResult();
                    return;
                }
            }
        }
    }

    private bool HasDataPermission(long userId, string[] roles, IDataPermission dataPermission)
    {
        // 检查数据所有者
        if (dataPermission.CreatorId == userId)
        {
            return true;
        }

        // 检查部门权限
        if (dataPermission.DepartmentId.HasValue)
        {
            // TODO: 实现部门权限检查逻辑
            return true;
        }

        // 检查自定义权限
        if (dataPermission.PermissionCode != null)
        {
            // TODO: 实现自定义权限检查逻辑
            return roles.Any(r => r == dataPermission.PermissionCode);
        }

        return false;
    }
}

/// <summary>
/// 数据权限接口
/// </summary>
public interface IDataPermission
{
    /// <summary>
    /// 创建者ID
    /// </summary>
    long CreatorId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    long? DepartmentId { get; set; }

    /// <summary>
    /// 权限代码
    /// </summary>
    string? PermissionCode { get; set; }
} 
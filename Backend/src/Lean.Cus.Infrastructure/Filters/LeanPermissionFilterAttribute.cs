using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Lean.Cus.Domain.Security;
using Lean.Cus.Common.Security;

namespace Lean.Cus.Infrastructure.Filters;

/// <summary>
/// 权限过滤器
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LeanPermissionFilterAttribute : LeanActionFilterAttribute
{
    private readonly string[] _permissions;
    private readonly bool _requireAll;
    private readonly ILeanPermissionProvider _permissionProvider;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="permissions">所需权限列表，使用冒号分隔的权限代码，如：admin:user:view</param>
    /// <param name="requireAll">是否需要满足所有权限，true表示需要满足所有权限，false表示满足任一权限即可</param>
    /// <param name="permissionProvider">权限提供者</param>
    /// <param name="logger">日志记录器</param>
    public LeanPermissionFilterAttribute(
        string[] permissions,
        bool requireAll = false,
        ILeanPermissionProvider? permissionProvider = null,
        ILogger<LeanPermissionFilterAttribute>? logger = null)
        : base(logger ?? throw new ArgumentNullException(nameof(logger)))
    {
        _permissions = permissions ?? throw new ArgumentNullException(nameof(permissions));
        _requireAll = requireAll;
        _permissionProvider = permissionProvider ?? throw new ArgumentNullException(nameof(permissionProvider));
    }

    /// <summary>
    /// 执行前检查权限
    /// </summary>
    public override async void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var user = context.HttpContext.User;
        if (!user.Identity?.IsAuthenticated ?? true)
        {
            Logger.LogWarning("未经授权的访问");
            context.Result = new UnauthorizedResult();
            return;
        }

        var hasPermission = await _permissionProvider.ValidatePermissionsAsync(user, _permissions, _requireAll);
        if (!hasPermission)
        {
            Logger.LogWarning($"用户 {user.Identity.Name} 缺少所需权限: {string.Join(", ", _permissions)}");
            context.Result = new ForbidResult();
            return;
        }
    }
} 
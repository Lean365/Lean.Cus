using System.Security.Claims;

namespace Lean.Cus.Domain.Security;

/// <summary>
/// 权限提供者接口
/// </summary>
public interface ILeanPermissionProvider
{
    /// <summary>
    /// 获取用户权限
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>权限列表</returns>
    Task<IEnumerable<string>> GetUserPermissionsAsync(long userId);

    /// <summary>
    /// 获取角色权限
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>权限列表</returns>
    Task<IEnumerable<string>> GetRolePermissionsAsync(string roleId);

    /// <summary>
    /// 验证权限
    /// </summary>
    /// <param name="user">用户主体</param>
    /// <param name="requiredPermissions">所需权限</param>
    /// <param name="requireAll">是否需要满足所有权限</param>
    /// <returns>是否有权限</returns>
    Task<bool> ValidatePermissionsAsync(ClaimsPrincipal user, string[] requiredPermissions, bool requireAll = false);
} 
using System.Security.Claims;
using Microsoft.Extensions.Logging;
using Lean.Cus.Domain.Security;
using Lean.Cus.Domain.IRepositories;
using Lean.Cus.Common.Security;
using Lean.Cus.Domain.Entities.Admin;
using System.Linq.Expressions;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Infrastructure.Security;

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

/// <summary>
/// 默认权限提供者
/// </summary>
public class LeanPermissionProvider : ILeanPermissionProvider
{
    private readonly ILeanRepository<LeanPermission> _permissionRepository;
    private readonly ILeanRepository<LeanUserRole> _userRoleRepository;
    private readonly ILeanRepository<LeanRolePermission> _rolePermissionRepository;
    private readonly ILogger<LeanPermissionProvider> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanPermissionProvider(
        ILeanRepository<LeanPermission> permissionRepository,
        ILeanRepository<LeanUserRole> userRoleRepository,
        ILeanRepository<LeanRolePermission> rolePermissionRepository,
        ILogger<LeanPermissionProvider> logger)
    {
        _permissionRepository = permissionRepository;
        _userRoleRepository = userRoleRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _logger = logger;
    }

    /// <summary>
    /// 获取用户权限
    /// </summary>
    public async Task<IEnumerable<string>> GetUserPermissionsAsync(long userId)
    {
        try
        {
            // 获取用户角色
            var userRoles = await _userRoleRepository.GetListAsync(ur => ur.UserId == userId);
            var roleIds = userRoles.Select(ur => ur.RoleId).ToList();

            // 获取角色权限
            var rolePermissions = await _rolePermissionRepository.GetListAsync(rp => roleIds.Contains(rp.RoleId));
            var permissionIds = rolePermissions.Select(rp => rp.PermissionId).Distinct().ToList();

            // 获取权限
            var permissions = await _permissionRepository.GetListAsync(p => permissionIds.Contains(p.Id));
            return permissions.Where(p => p.Status == LeanStatus.Enabled).Select(p => p.Code);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取用户权限失败");
            return Array.Empty<string>();
        }
    }

    /// <summary>
    /// 获取角色权限
    /// </summary>
    public async Task<IEnumerable<string>> GetRolePermissionsAsync(string roleId)
    {
        try
        {
            if (!long.TryParse(roleId, out long roleIdLong))
            {
                _logger.LogWarning("Invalid roleId format: {roleId}", roleId);
                return Array.Empty<string>();
            }

            // 获取角色权限
            var rolePermissions = await _rolePermissionRepository.GetListAsync(rp => rp.RoleId == roleIdLong);
            var permissionIds = rolePermissions.Select(rp => rp.PermissionId).ToList();

            // 获取权限
            var permissions = await _permissionRepository.GetListAsync(p => permissionIds.Contains(p.Id));
            return permissions.Where(p => p.Status == LeanStatus.Enabled).Select(p => p.Code);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取角色权限失败");
            return Array.Empty<string>();
        }
    }

    /// <summary>
    /// 验证权限
    /// </summary>
    public async Task<bool> ValidatePermissionsAsync(ClaimsPrincipal user, string[] requiredPermissions, bool requireAll = false)
    {
        if (!user.Identity?.IsAuthenticated ?? true)
        {
            return false;
        }

        var userId = LeanJwtHelper.GetUserId(user);
        var roles = LeanJwtHelper.GetRoles(user);

        // 超级管理员跳过权限检查
        if (roles.Contains("admin"))
        {
            return true;
        }

        try
        {
            // 获取用户权限
            var userPermissions = await GetUserPermissionsAsync(userId);

            // 获取角色权限
            var rolePermissions = new List<string>();
            foreach (var role in roles)
            {
                var permissions = await GetRolePermissionsAsync(role);
                rolePermissions.AddRange(permissions);
            }

            // 合并权限
            var allPermissions = userPermissions.Union(rolePermissions).Distinct();

            // 检查权限
            return requireAll
                ? requiredPermissions.All(p => allPermissions.Contains(p))
                : requiredPermissions.Any(p => allPermissions.Contains(p));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "验证权限失败");
            return false;
        }
    }
} 
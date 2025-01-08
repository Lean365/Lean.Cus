using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Application.Dtos.Admin;

namespace Lean.Cus.Application.IServices.Admin;

/// <summary>
/// 角色服务接口
/// </summary>
public interface ILeanRoleService : ILeanService<LeanRole, LeanRoleDto, LeanRoleQueryDto, LeanRoleCreateDto, LeanRoleUpdateDto>
{
    /// <summary>
    /// 获取用户的角色列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>角色列表</returns>
    Task<List<LeanRoleDto>> GetUserRolesAsync(long userId);

    /// <summary>
    /// 分配角色给用户
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleIds">角色ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> AssignRolesToUserAsync(long userId, List<long> roleIds);

    /// <summary>
    /// 移除用户的角色
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="roleIds">角色ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> RemoveUserRolesAsync(long userId, List<long> roleIds);
} 
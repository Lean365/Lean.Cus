using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lean.Cus.Application.IServices.Admin;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.Repositories;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 角色服务实现
/// </summary>
public class LeanRoleService : LeanService<LeanRole, LeanRoleDto, LeanRoleQueryDto, LeanRoleCreateDto, LeanRoleUpdateDto>, ILeanRoleService
{
    private readonly ILeanRepository<LeanUserRole> _userRoleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储</param>
    /// <param name="userRoleRepository">用户角色关联仓储</param>
    /// <param name="logger">日志记录器</param>
    public LeanRoleService(
        ILeanRepository<LeanRole> repository,
        ILeanRepository<LeanUserRole> userRoleRepository,
        ILogger<LeanRoleService> logger)
        : base(repository, logger)
    {
        _userRoleRepository = userRoleRepository;
    }

    /// <inheritdoc/>
    public async Task<List<LeanRoleDto>> GetUserRolesAsync(long userId)
    {
        try
        {
            // 获取用户的角色ID列表
            var roleIds = await _userRoleRepository.AsQueryable()
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            if (roleIds.Count == 0)
            {
                return new List<LeanRoleDto>();
            }

            // 获取角色列表
            var roles = await Repository.AsQueryable()
                .Where(r => roleIds.Contains(r.Id))
                .ToListAsync();

            return roles.Select(MapToDto).ToList();
        }
        catch (System.Exception ex)
        {
            Logger.LogError(ex, "获取用户角色列表异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> AssignRolesToUserAsync(long userId, List<long> roleIds)
    {
        try
        {
            // 获取用户当前的角色ID列表
            var existingRoleIds = await _userRoleRepository.AsQueryable()
                .Where(ur => ur.UserId == userId)
                .Select(ur => ur.RoleId)
                .ToListAsync();

            // 需要新增的角色ID列表
            var roleIdsToAdd = roleIds.Except(existingRoleIds).ToList();

            if (roleIdsToAdd.Count > 0)
            {
                // 创建用户角色关联
                var userRoles = roleIdsToAdd.Select(roleId => new LeanUserRole
                {
                    UserId = userId,
                    RoleId = roleId
                }).ToList();

                return await _userRoleRepository.InsertRangeAsync(userRoles);
            }

            return true;
        }
        catch (System.Exception ex)
        {
            Logger.LogError(ex, "分配角色给用户异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> RemoveUserRolesAsync(long userId, List<long> roleIds)
    {
        try
        {
            return await _userRoleRepository.DeleteAsync(ur => ur.UserId == userId && roleIds.Contains(ur.RoleId));
        }
        catch (System.Exception ex)
        {
            Logger.LogError(ex, "移除用户角色异常");
            throw;
        }
    }

    /// <summary>
    /// 将实体映射为DTO
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>DTO</returns>
    protected override LeanRoleDto MapToDto(LeanRole entity)
    {
        return new LeanRoleDto
        {
            Id = entity.Id,
            RoleName = entity.Name,
            RoleKey = entity.Code,
            SortOrder = entity.Sort,
            Status = entity.Status,
            Description = entity.Remark
        };
    }

    /// <summary>
    /// 将DTO映射为实体
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns>实体</returns>
    protected override LeanRole MapToEntity(LeanRoleCreateDto dto)
    {
        return new LeanRole
        {
            Name = dto.RoleName,
            Code = dto.RoleKey,
            Sort = dto.SortOrder,
            Status = dto.Status,
            Remark = dto.Description
        };
    }

    /// <summary>
    /// 将DTO映射为实体
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <param name="entity">实体</param>
    protected override void MapToEntity(LeanRoleUpdateDto dto, LeanRole entity)
    {
        entity.Name = dto.RoleName;
        entity.Code = dto.RoleKey;
        entity.Sort = dto.SortOrder;
        entity.Status = dto.Status;
        entity.Remark = dto.Description;
    }

    /// <summary>
    /// 获取主键
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns>主键</returns>
    protected override long GetId(LeanRoleUpdateDto dto)
    {
        return dto.Id;
    }
} 
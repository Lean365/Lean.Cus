//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件的所有修改。
// </auto-generated>
//
// <copyright file="LeanUserRoleService.cs" company="Lean365">
//     Copyright (c) Lean365. All rights reserved.
// </copyright>
// <author>Lean365</author>
// <date>2024-01-08</date>
// <summary>
// 用户角色关联服务实现
// </summary>
//------------------------------------------------------------------------------

using System;
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
/// 用户角色关联服务实现
/// </summary>
public class LeanUserRoleService : LeanService<LeanUserRole, LeanUserRoleDto, LeanUserRoleQueryDto, LeanUserRoleCreateDto, LeanUserRoleUpdateDto>, ILeanUserRoleService
{
    private readonly ILeanRepository<LeanUser> _userRepository;
    private readonly ILeanRepository<LeanRole> _roleRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">用户角色关联仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="logger">日志记录器</param>
    public LeanUserRoleService(
        ILeanRepository<LeanUserRole> repository,
        ILeanRepository<LeanUser> userRepository,
        ILeanRepository<LeanRole> roleRepository,
        ILogger<LeanUserRoleService> logger)
        : base(repository, logger)
    {
        _userRepository = userRepository;
        _roleRepository = roleRepository;
    }

    /// <inheritdoc/>
    public async Task<List<LeanUserRoleDto>> GetUserRolesAsync(long userId)
    {
        try
        {
            var userRoles = await Repository.AsQueryable()
                .Where(ur => ur.UserId == userId)
                .ToListAsync();

            var dtos = new List<LeanUserRoleDto>();

            foreach (var userRole in userRoles)
            {
                var dto = MapToDto(userRole);

                var user = await _userRepository.GetByIdAsync(userRole.UserId);
                if (user != null)
                {
                    dto.UserName = user.UserName;
                }

                var role = await _roleRepository.GetByIdAsync(userRole.RoleId);
                if (role != null)
                {
                    dto.RoleName = role.Name;
                }

                dtos.Add(dto);
            }

            return dtos;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "获取用户角色关联异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> AssignRolesAsync(long userId, List<long> roleIds)
    {
        try
        {
            // 删除现有角色
            await Repository.DeleteAsync(ur => ur.UserId == userId);

            if (roleIds.Count > 0)
            {
                // 添加新角色
                var userRoles = roleIds.Select(roleId => new LeanUserRole
                {
                    UserId = userId,
                    RoleId = roleId
                }).ToList();

                return await Repository.InsertRangeAsync(userRoles);
            }

            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "分配角色异常");
            throw;
        }
    }

    /// <summary>
    /// 将实体映射为DTO
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>DTO</returns>
    protected override LeanUserRoleDto MapToDto(LeanUserRole entity)
    {
        return new LeanUserRoleDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            RoleId = entity.RoleId
        };
    }

    /// <summary>
    /// 将DTO映射为实体
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns>实体</returns>
    protected override LeanUserRole MapToEntity(LeanUserRoleCreateDto dto)
    {
        return new LeanUserRole
        {
            UserId = dto.UserId,
            RoleId = dto.RoleId
        };
    }

    /// <summary>
    /// 将DTO映射为实体
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <param name="entity">实体</param>
    protected override void MapToEntity(LeanUserRoleUpdateDto dto, LeanUser entity)
    {
        entity.UserId = dto.UserId;
        entity.RoleId = dto.RoleId;
    }

    /// <summary>
    /// 获取主键
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns>主键</returns>
    protected override long GetId(LeanUserRoleUpdateDto dto)
    {
        return dto.Id;
    }
} 
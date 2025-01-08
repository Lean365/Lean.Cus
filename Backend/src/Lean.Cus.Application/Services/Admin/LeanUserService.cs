//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件的所有修改。
// </auto-generated>
//
// <copyright file="LeanUserService.cs" company="Lean365">
//     Copyright (c) Lean365. All rights reserved.
// </copyright>
// <author>Lean365</author>
// <date>2024-01-08</date>
// <summary>
// 用户服务实现
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
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Services;
using Lean.Cus.Common.Repositories;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 用户服务实现
/// </summary>
public class LeanUserService : LeanService<LeanUser, LeanUserDto, LeanUserQueryDto, LeanUserCreateDto, LeanUserUpdateDto>, ILeanUserService
{
    private readonly ILeanRepository<LeanUserRole> _userRoleRepository;
    private readonly ILeanRepository<LeanUserPost> _userPostRepository;
    private readonly ILeanRepository<LeanRole> _roleRepository;
    private readonly ILeanRepository<LeanDept> _deptRepository;
    private readonly ILeanRepository<LeanPost> _postRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">用户仓储</param>
    /// <param name="userRoleRepository">用户角色关联仓储</param>
    /// <param name="userPostRepository">用户岗位关联仓储</param>
    /// <param name="roleRepository">角色仓储</param>
    /// <param name="deptRepository">部门仓储</param>
    /// <param name="postRepository">岗位仓储</param>
    /// <param name="logger">日志记录器</param>
    public LeanUserService(
        ILeanRepository<LeanUser> repository,
        ILeanRepository<LeanUserRole> userRoleRepository,
        ILeanRepository<LeanUserPost> userPostRepository,
        ILeanRepository<LeanRole> roleRepository,
        ILeanRepository<LeanDept> deptRepository,
        ILeanRepository<LeanPost> postRepository,
        ILogger<LeanUserService> logger)
        : base(repository, logger)
    {
        _userRoleRepository = userRoleRepository;
        _userPostRepository = userPostRepository;
        _roleRepository = roleRepository;
        _deptRepository = deptRepository;
        _postRepository = postRepository;
    }

    /// <inheritdoc/>
    public override async Task<bool> CreateAsync(LeanUserCreateDto createDto)
    {
        // 创建用户
        var result = await base.CreateAsync(createDto);
        if (!result)
        {
            return false;
        }

        // 获取用户ID
        var user = await Repository.AsQueryable()
            .Where(u => u.UserName == createDto.UserName)
            .FirstAsync();
        if (user == null)
        {
            return false;
        }

        // 创建用户角色关联
        if (createDto.RoleIds.Any())
        {
            var userRoles = createDto.RoleIds.Select(roleId => new LeanUserRole
            {
                UserId = user.Id,
                RoleId = roleId
            }).ToList();

            result = await _userRoleRepository.InsertAsync(userRoles);
            if (!result)
            {
                return false;
            }
        }

        // 创建用户岗位关联
        if (createDto.PostIds.Any())
        {
            var userPosts = createDto.PostIds.Select(postId => new LeanUserPost
            {
                UserId = user.Id,
                PostId = postId
            }).ToList();

            result = await _userPostRepository.InsertAsync(userPosts);
            if (!result)
            {
                return false;
            }
        }

        return true;
    }

    /// <inheritdoc/>
    public override async Task<bool> UpdateAsync(LeanUserUpdateDto updateDto)
    {
        // 更新用户
        var result = await base.UpdateAsync(updateDto);
        if (!result)
        {
            return false;
        }

        // 更新用户角色关联
        await _userRoleRepository.DeleteAsync(ur => ur.UserId == updateDto.Id);
        if (updateDto.RoleIds.Any())
        {
            var userRoles = updateDto.RoleIds.Select(roleId => new LeanUserRole
            {
                UserId = updateDto.Id,
                RoleId = roleId
            }).ToList();

            result = await _userRoleRepository.InsertAsync(userRoles);
            if (!result)
            {
                return false;
            }
        }

        // 更新用户岗位关联
        await _userPostRepository.DeleteAsync(up => up.UserId == updateDto.Id);
        if (updateDto.PostIds.Any())
        {
            var userPosts = updateDto.PostIds.Select(postId => new LeanUserPost
            {
                UserId = updateDto.Id,
                PostId = postId
            }).ToList();

            result = await _userPostRepository.InsertAsync(userPosts);
            if (!result)
            {
                return false;
            }
        }

        return true;
    }

    /// <inheritdoc/>
    public override async Task<bool> DeleteAsync(long id)
    {
        // 删除用户角色关联
        await _userRoleRepository.DeleteAsync(ur => ur.UserId == id);

        // 删除用户岗位关联
        await _userPostRepository.DeleteAsync(up => up.UserId == id);

        // 删除用户
        return await base.DeleteAsync(id);
    }

    /// <inheritdoc/>
    public async Task<LeanUserDto> GetUserInfoAsync(long userId)
    {
        try
        {
            // 获取用户信息
            var user = await Repository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception($"用户不存在：{userId}");
            }

            var userDto = MapToDto(user);

            // 获取部门信息
            if (user.DeptId.HasValue)
            {
                var dept = await _deptRepository.GetByIdAsync(user.DeptId.Value);
                if (dept != null)
                {
                    userDto.DeptName = dept.Name;
                }
            }

            // 获取岗位信息
            var userPosts = await _userPostRepository.AsQueryable()
                .Where(up => up.UserId == userId)
                .ToListAsync();
            if (userPosts.Any())
            {
                var postIds = userPosts.Select(up => up.PostId).ToList();
                userDto.PostIds = postIds;

                var posts = await _postRepository.AsQueryable()
                    .Where(p => postIds.Contains(p.Id))
                    .ToListAsync();
                userDto.PostNames = posts.Select(p => p.Name).ToList();
            }

            // 获取角色信息
            var userRoles = await _userRoleRepository.AsQueryable()
                .Where(ur => ur.UserId == userId)
                .ToListAsync();
            if (userRoles.Any())
            {
                var roleIds = userRoles.Select(ur => ur.RoleId).ToList();
                userDto.RoleIds = roleIds;

                var roles = await _roleRepository.AsQueryable()
                    .Where(r => roleIds.Contains(r.Id))
                    .ToListAsync();
                userDto.RoleNames = roles.Select(r => r.Name).ToList();
            }

            return userDto;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "获取用户信息失败");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<(int success, int fail)> ImportAsync(List<LeanUserImportDto> importDtos)
    {
        var success = 0;
        var fail = 0;

        foreach (var importDto in importDtos)
        {
            try
            {
                // 检查用户名是否存在
                if (await Repository.AsQueryable().AnyAsync(u => u.UserName == importDto.UserName))
                {
                    fail++;
                    continue;
                }

                // 获取部门ID
                long? deptId = null;
                if (!string.IsNullOrEmpty(importDto.DeptName))
                {
                    var dept = await _deptRepository.AsQueryable()
                        .Where(d => d.Name == importDto.DeptName)
                        .FirstAsync();
                    if (dept != null)
                    {
                        deptId = dept.Id;
                    }
                }

                // 创建用户
                var user = new LeanUser
                {
                    UserName = importDto.UserName,
                    Password = "123456", // TODO: 密码加密
                    NickName = importDto.NickName,
                    EnglishName = importDto.EnglishName,
                    Email = importDto.Email,
                    Phone = importDto.Phone,
                    Gender = importDto.Gender,
                    Status = importDto.Status,
                    DeptId = deptId
                };

                var result = await Repository.InsertAsync(user);
                if (!result)
                {
                    fail++;
                    continue;
                }

                // 创建用户岗位关联
                if (!string.IsNullOrEmpty(importDto.PostNames))
                {
                    var postNames = importDto.PostNames.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    var posts = await _postRepository.AsQueryable()
                        .Where(p => postNames.Contains(p.Name))
                        .ToListAsync();

                    if (posts.Any())
                    {
                        var userPosts = posts.Select(p => new LeanUserPost
                        {
                            UserId = user.Id,
                            PostId = p.Id
                        }).ToList();

                        result = await _userPostRepository.InsertAsync(userPosts);
                        if (!result)
                        {
                            fail++;
                            continue;
                        }
                    }
                }

                // 创建用户角色关联
                if (!string.IsNullOrEmpty(importDto.RoleNames))
                {
                    var roleNames = importDto.RoleNames.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    var roles = await _roleRepository.AsQueryable()
                        .Where(r => roleNames.Contains(r.Name))
                        .ToListAsync();

                    if (roles.Any())
                    {
                        var userRoles = roles.Select(r => new LeanUserRole
                        {
                            UserId = user.Id,
                            RoleId = r.Id
                        }).ToList();

                        result = await _userRoleRepository.InsertAsync(userRoles);
                        if (!result)
                        {
                            fail++;
                            continue;
                        }
                    }
                }

                success++;
            }
            catch
            {
                fail++;
            }
        }

        return (success, fail);
    }

    /// <inheritdoc/>
    public async Task<List<LeanUserExportDto>> ExportAsync(LeanUserQueryDto queryDto)
    {
        var query = Repository.AsQueryable()
            .WhereIF(!string.IsNullOrEmpty(queryDto.UserName), u => u.UserName.Contains(queryDto.UserName!))
            .WhereIF(!string.IsNullOrEmpty(queryDto.NickName), u => u.NickName.Contains(queryDto.NickName!))
            .WhereIF(!string.IsNullOrEmpty(queryDto.Phone), u => u.Phone!.Contains(queryDto.Phone!))
            .WhereIF(queryDto.Status.HasValue, u => u.Status == queryDto.Status)
            .WhereIF(queryDto.DeptId.HasValue, u => u.DeptId == queryDto.DeptId);

        var users = await query.ToListAsync();
        var exportDtos = new List<LeanUserExportDto>();

        foreach (var user in users)
        {
            var dto = new LeanUserExportDto
            {
                UserName = user.UserName,
                NickName = user.NickName,
                EnglishName = user.EnglishName,
                Email = user.Email,
                Phone = user.Phone,
                GenderName = user.Gender.ToString(),
                StatusName = user.Status.ToString(),
                LastLoginIp = user.LastLoginIp,
                LastLoginTime = user.LastLoginTime
            };

            // 获取部门名称
            if (user.DeptId.HasValue)
            {
                var dept = await _deptRepository.GetByIdAsync(user.DeptId.Value);
                if (dept != null)
                {
                    dto.DeptName = dept.Name;
                }
            }

            // 获取岗位名称
            var userPosts = await _userPostRepository.AsQueryable()
                .Where(up => up.UserId == user.Id)
                .ToListAsync();
            if (userPosts.Any())
            {
                var postIds = userPosts.Select(up => up.PostId).ToList();
                var posts = await _postRepository.AsQueryable()
                    .Where(p => postIds.Contains(p.Id))
                    .ToListAsync();
                dto.PostNames = string.Join(",", posts.Select(p => p.Name));
            }

            // 获取角色名称
            var userRoles = await _userRoleRepository.AsQueryable()
                .Where(ur => ur.UserId == user.Id)
                .ToListAsync();
            if (userRoles.Any())
            {
                var roleIds = userRoles.Select(ur => ur.RoleId).ToList();
                var roles = await _roleRepository.AsQueryable()
                    .Where(r => roleIds.Contains(r.Id))
                    .ToListAsync();
                dto.RoleNames = string.Join(",", roles.Select(r => r.Name));
            }

            exportDtos.Add(dto);
        }

        return exportDtos;
    }

    /// <inheritdoc/>
    public Task<LeanUserImportTemplateDto> GetImportTemplateAsync()
    {
        return Task.FromResult(new LeanUserImportTemplateDto());
    }

    /// <summary>
    /// 将实体映射为DTO
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>DTO</returns>
    protected override LeanUserDto MapToDto(LeanUser entity)
    {
        return new LeanUserDto
        {
            Id = entity.Id,
            UserName = entity.UserName,
            NickName = entity.NickName,
            EnglishName = entity.EnglishName,
            Avatar = entity.Avatar,
            Email = entity.Email,
            Phone = entity.Phone,
            Gender = entity.Gender,
            Status = entity.Status,
            IsAdmin = entity.IsAdmin,
            DeptId = entity.DeptId,
            LastLoginIp = entity.LastLoginIp,
            LastLoginTime = entity.LastLoginTime
        };
    }

    /// <summary>
    /// 将DTO映射为实体
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns>实体</returns>
    protected override LeanUser MapToEntity(LeanUserCreateDto dto)
    {
        return new LeanUser
        {
            UserName = dto.UserName,
            Password = dto.Password, // TODO: 密码加密
            NickName = dto.NickName,
            EnglishName = dto.EnglishName,
            Avatar = dto.Avatar,
            Email = dto.Email,
            Phone = dto.Phone,
            Gender = dto.Gender,
            Status = dto.Status,
            IsAdmin = dto.IsAdmin,
            DeptId = dto.DeptId
        };
    }

    /// <summary>
    /// 将DTO映射为实体
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <param name="entity">实体</param>
    protected override void MapToEntity(LeanUserUpdateDto dto, LeanUser entity)
    {
        entity.NickName = dto.NickName;
        entity.EnglishName = dto.EnglishName;
        entity.Avatar = dto.Avatar;
        entity.Email = dto.Email;
        entity.Phone = dto.Phone;
        entity.Gender = dto.Gender;
        entity.Status = dto.Status;
        entity.IsAdmin = dto.IsAdmin;
        entity.DeptId = dto.DeptId;

        if (!string.IsNullOrEmpty(dto.Password))
        {
            entity.Password = dto.Password; // TODO: 密码加密
        }
    }

    /// <summary>
    /// 获取主键
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns>主键</returns>
    protected override long GetId(LeanUserUpdateDto dto)
    {
        return dto.Id;
    }
} 
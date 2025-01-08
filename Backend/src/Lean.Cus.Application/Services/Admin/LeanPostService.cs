using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Lean.Cus.Common.Services;
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.Repositories;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 岗位服务实现
/// </summary>
public class LeanPostService : LeanService<LeanPost, LeanPostDto, LeanPostQueryDto, LeanPostCreateDto, LeanPostUpdateDto>, ILeanPostService
{
    private readonly ILeanRepository<LeanUserPost> _userPostRepository;
    private readonly ILeanRepository<LeanUser> _userRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">岗位仓储</param>
    /// <param name="userPostRepository">用户岗位关联仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="logger">日志记录器</param>
    public LeanPostService(
        ILeanRepository<LeanPost> repository,
        ILeanRepository<LeanUserPost> userPostRepository,
        ILeanRepository<LeanUser> userRepository,
        ILogger<LeanPostService> logger)
        : base(repository, logger)
    {
        _userPostRepository = userPostRepository;
        _userRepository = userRepository;
    }

    /// <inheritdoc/>
    public override async Task<bool> CreateAsync(LeanPostCreateDto createDto)
    {
        // 创建岗位
        var result = await base.CreateAsync(createDto);
        if (!result)
        {
            return false;
        }

        // 获取岗位ID
        var post = await Repository.AsQueryable()
            .Where(p => p.Code == createDto.Code)
            .FirstAsync();
        if (post == null)
        {
            return false;
        }

        // 创建用户岗位关联
        if (createDto.UserIds.Any())
        {
            var userPosts = createDto.UserIds.Select(userId => new LeanUserPost
            {
                UserId = userId,
                PostId = post.Id
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
    public override async Task<bool> UpdateAsync(LeanPostUpdateDto updateDto)
    {
        // 更新岗位
        var result = await base.UpdateAsync(updateDto);
        if (!result)
        {
            return false;
        }

        // 更新用户岗位关联
        await _userPostRepository.DeleteAsync(up => up.PostId == updateDto.Id);
        if (updateDto.UserIds.Any())
        {
            var userPosts = updateDto.UserIds.Select(userId => new LeanUserPost
            {
                UserId = userId,
                PostId = updateDto.Id
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
        // 删除用户岗位关联
        await _userPostRepository.DeleteAsync(up => up.PostId == id);

        // 删除岗位
        return await base.DeleteAsync(id);
    }

    /// <inheritdoc/>
    public async Task<(int success, int fail)> ImportAsync(List<LeanPostImportDto> importDtos)
    {
        var success = 0;
        var fail = 0;

        foreach (var importDto in importDtos)
        {
            try
            {
                // 检查岗位编码是否存在
                if (await Repository.AsQueryable().AnyAsync(p => p.Code == importDto.Code))
                {
                    fail++;
                    continue;
                }

                // 创建岗位
                var post = new LeanPost
                {
                    Name = importDto.Name,
                    Code = importDto.Code,
                    Sort = importDto.Sort,
                    Status = importDto.Status,
                    Remark = importDto.Remark
                };

                var result = await Repository.InsertAsync(post);
                if (!result)
                {
                    fail++;
                    continue;
                }

                // 创建用户岗位关联
                if (!string.IsNullOrEmpty(importDto.UserNames))
                {
                    var userNames = importDto.UserNames.Split(',', StringSplitOptions.RemoveEmptyEntries);
                    var users = await _userRepository.AsQueryable()
                        .Where(u => userNames.Contains(u.UserName))
                        .ToListAsync();

                    if (users.Any())
                    {
                        var userPosts = users.Select(u => new LeanUserPost
                        {
                            UserId = u.Id,
                            PostId = post.Id
                        }).ToList();

                        result = await _userPostRepository.InsertAsync(userPosts);
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
    public async Task<List<LeanPostExportDto>> ExportAsync(LeanPostQueryDto queryDto)
    {
        var query = Repository.AsQueryable()
            .WhereIF(!string.IsNullOrEmpty(queryDto.Name), p => p.Name.Contains(queryDto.Name!))
            .WhereIF(!string.IsNullOrEmpty(queryDto.Code), p => p.Code.Contains(queryDto.Code!))
            .WhereIF(queryDto.Status.HasValue, p => p.Status == queryDto.Status);

        var posts = await query.ToListAsync();
        var exportDtos = new List<LeanPostExportDto>();

        foreach (var post in posts)
        {
            var dto = new LeanPostExportDto
            {
                Name = post.Name,
                Code = post.Code,
                Sort = post.Sort,
                StatusName = post.Status.ToString(),
                Remark = post.Remark
            };

            // 获取用户名称
            var userPosts = await _userPostRepository.AsQueryable()
                .Where(up => up.PostId == post.Id)
                .ToListAsync();
            if (userPosts.Any())
            {
                var userIds = userPosts.Select(up => up.UserId).ToList();
                var users = await _userRepository.AsQueryable()
                    .Where(u => userIds.Contains(u.Id))
                    .ToListAsync();
                dto.UserNames = string.Join(",", users.Select(u => u.UserName));
            }

            exportDtos.Add(dto);
        }

        return exportDtos;
    }

    /// <inheritdoc/>
    protected override LeanPostDto MapToDto(LeanPost entity)
    {
        return new LeanPostDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Code = entity.Code,
            Sort = entity.Sort,
            Status = entity.Status,
            Remark = entity.Remark
        };
    }

    /// <inheritdoc/>
    protected override LeanPost MapToEntity(LeanPostCreateDto dto)
    {
        return new LeanPost
        {
            Name = dto.Name,
            Code = dto.Code,
            Sort = dto.Sort,
            Status = dto.Status,
            Remark = dto.Remark
        };
    }

    /// <inheritdoc/>
    protected override void MapToEntity(LeanPostUpdateDto dto, LeanPost entity)
    {
        entity.Name = dto.Name;
        entity.Code = dto.Code;
        entity.Sort = dto.Sort;
        entity.Status = dto.Status;
        entity.Remark = dto.Remark;
    }

    /// <inheritdoc/>
    protected override long GetId(LeanPostUpdateDto dto)
    {
        return dto.Id;
    }
} 
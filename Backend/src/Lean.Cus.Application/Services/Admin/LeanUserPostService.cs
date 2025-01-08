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
/// 用户岗位关联服务实现
/// </summary>
public class LeanUserPostService : LeanService<LeanUserPost, LeanUserPostDto, LeanUserPostQueryDto, LeanUserPostCreateDto, LeanUserPostUpdateDto>, ILeanUserPostService
{
    private readonly ILeanRepository<LeanUser> _userRepository;
    private readonly ILeanRepository<LeanPost> _postRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储</param>
    /// <param name="userRepository">用户仓储</param>
    /// <param name="postRepository">岗位仓储</param>
    /// <param name="logger">日志</param>
    public LeanUserPostService(
        ILeanRepository<LeanUserPost> repository,
        ILeanRepository<LeanUser> userRepository,
        ILeanRepository<LeanPost> postRepository,
        ILogger<LeanUserPostService> logger)
        : base(repository, logger)
    {
        _userRepository = userRepository;
        _postRepository = postRepository;
    }

    /// <inheritdoc/>
    public async Task<bool> BatchCreateAsync(long userId, List<long> postIds)
    {
        // 删除原有关联
        await BatchDeleteByUserIdAsync(userId);

        // 创建新关联
        var userPosts = postIds.Select(postId => new LeanUserPost
        {
            UserId = userId,
            PostId = postId
        }).ToList();

        return await Repository.InsertAsync(userPosts);
    }

    /// <inheritdoc/>
    public async Task<bool> BatchDeleteByUserIdAsync(long userId)
    {
        return await Repository.DeleteAsync(up => up.UserId == userId);
    }

    /// <inheritdoc/>
    public async Task<List<long>> GetPostIdsByUserIdAsync(long userId)
    {
        var userPosts = await Repository.AsQueryable()
            .Where(up => up.UserId == userId)
            .ToListAsync();

        return userPosts.Select(up => up.PostId).ToList();
    }

    /// <inheritdoc/>
    public async Task<List<long>> GetUserIdsByPostIdAsync(long postId)
    {
        var userPosts = await Repository.AsQueryable()
            .Where(up => up.PostId == postId)
            .ToListAsync();

        return userPosts.Select(up => up.UserId).ToList();
    }

    /// <inheritdoc/>
    public async Task<(int success, int fail)> ImportAsync(List<LeanUserPostImportDto> importDtos)
    {
        var success = 0;
        var fail = 0;

        foreach (var importDto in importDtos)
        {
            try
            {
                // 获取用户ID
                var user = await _userRepository.AsQueryable()
                    .Where(u => u.UserName == importDto.UserName)
                    .FirstAsync();
                if (user == null)
                {
                    fail++;
                    continue;
                }

                // 获取岗位ID
                var post = await _postRepository.AsQueryable()
                    .Where(p => p.Name == importDto.PostName)
                    .FirstAsync();
                if (post == null)
                {
                    fail++;
                    continue;
                }

                // 创建关联
                var userPost = new LeanUserPost
                {
                    UserId = user.Id,
                    PostId = post.Id
                };

                var result = await Repository.InsertAsync(userPost);
                if (result)
                {
                    success++;
                }
                else
                {
                    fail++;
                }
            }
            catch
            {
                fail++;
            }
        }

        return (success, fail);
    }

    /// <inheritdoc/>
    public async Task<List<LeanUserPostExportDto>> ExportAsync(LeanUserPostQueryDto queryDto)
    {
        var query = Repository.AsQueryable()
            .LeftJoin<LeanUser>((up, u) => up.UserId == u.Id)
            .LeftJoin<LeanPost>((up, u, p) => up.PostId == p.Id)
            .WhereIF(queryDto.UserId.HasValue, (up, u, p) => up.UserId == queryDto.UserId)
            .WhereIF(queryDto.PostId.HasValue, (up, u, p) => up.PostId == queryDto.PostId)
            .Select((up, u, p) => new LeanUserPostExportDto
            {
                UserName = u.UserName,
                PostName = p.Name
            });

        return await query.ToListAsync();
    }

    /// <inheritdoc/>
    protected override LeanUserPostDto MapToDto(LeanUserPost entity)
    {
        return new LeanUserPostDto
        {
            Id = entity.Id,
            UserId = entity.UserId,
            PostId = entity.PostId,
            UserName = entity.User?.UserName ?? string.Empty,
            PostName = entity.Post?.Name ?? string.Empty
        };
    }

    /// <inheritdoc/>
    protected override LeanUserPost MapToEntity(LeanUserPostCreateDto dto)
    {
        return new LeanUserPost
        {
            UserId = dto.UserId,
            PostId = dto.PostId
        };
    }

    /// <inheritdoc/>
    protected override void MapToEntity(LeanUserPostUpdateDto dto, LeanUserPost entity)
    {
        entity.UserId = dto.UserId;
        entity.PostId = dto.PostId;
    }

    /// <inheritdoc/>
    protected override long GetId(LeanUserPostUpdateDto dto)
    {
        return dto.Id;
    }
} 
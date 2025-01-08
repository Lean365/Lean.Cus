using System;
using System.Linq;
using System.Threading.Tasks;
using Lean.Cus.Application.IServices;
using Lean.Cus.Common.Entities;
using Lean.Cus.Common.Paging;
using Lean.Cus.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace Lean.Cus.Application.Services;

/// <summary>
/// 服务实现
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDto">DTO类型</typeparam>
/// <typeparam name="TQueryDto">查询DTO类型</typeparam>
/// <typeparam name="TCreateDto">创建DTO类型</typeparam>
/// <typeparam name="TUpdateDto">更新DTO类型</typeparam>
public abstract class LeanService<TEntity, TDto, TQueryDto, TCreateDto, TUpdateDto>
    : ILeanService<TEntity, TDto, TQueryDto, TCreateDto, TUpdateDto>
    where TEntity : LeanEntity, new()
    where TDto : class, new()
    where TQueryDto : LeanPagedDto
    where TCreateDto : class, new()
    where TUpdateDto : class, new()
{
    /// <summary>
    /// 仓储
    /// </summary>
    protected readonly ILeanRepository<TEntity> Repository;

    /// <summary>
    /// 日志记录器
    /// </summary>
    protected readonly ILogger<LeanService<TEntity, TDto, TQueryDto, TCreateDto, TUpdateDto>> Logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储</param>
    /// <param name="logger">日志记录器</param>
    protected LeanService(ILeanRepository<TEntity> repository, ILogger<LeanService<TEntity, TDto, TQueryDto, TCreateDto, TUpdateDto>> logger)
    {
        Repository = repository;
        Logger = logger;
    }

    /// <inheritdoc/>
    public virtual async Task<LeanPagedList<TDto>> GetListAsync(TQueryDto queryDto)
    {
        try
        {
            var query = Repository.AsQueryable();

            // 排序
            if (!string.IsNullOrEmpty(queryDto.OrderBy))
            {
                query = queryDto.IsDesc
                    ? query.OrderBy(queryDto.OrderBy + " DESC")
                    : query.OrderBy(queryDto.OrderBy);
            }

            // 分页
            var total = await query.CountAsync();
            var records = await query
                .Skip((queryDto.Current - 1) * queryDto.Size)
                .Take(queryDto.Size)
                .ToListAsync();

            // 转换为DTO
            var dtos = records.Select(MapToDto).ToList();

            return new LeanPagedList<TDto>
            {
                Current = queryDto.Current,
                Size = queryDto.Size,
                Total = total,
                Pages = (total + queryDto.Size - 1) / queryDto.Size,
                Records = dtos
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "获取列表异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public virtual async Task<TDto> GetAsync(long id)
    {
        try
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception($"数据不存在：{id}");
            }

            return MapToDto(entity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "获取详情异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public virtual async Task<long> CreateAsync(TCreateDto createDto)
    {
        try
        {
            var entity = MapToEntity(createDto);
            var success = await Repository.InsertAsync(entity);
            if (!success)
            {
                throw new Exception("创建失败");
            }

            return entity.Id;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "创建异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public virtual async Task<bool> UpdateAsync(TUpdateDto updateDto)
    {
        try
        {
            var id = GetId(updateDto);
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception($"数据不存在：{id}");
            }

            MapToEntity(updateDto, entity);
            return await Repository.UpdateAsync(entity);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "更新异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public virtual async Task<bool> DeleteAsync(long id)
    {
        try
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new Exception($"数据不存在：{id}");
            }

            return await Repository.DeleteAsync(id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "删除异常");
            throw;
        }
    }

    /// <summary>
    /// 将实体映射为DTO
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>DTO</returns>
    protected abstract TDto MapToDto(TEntity entity);

    /// <summary>
    /// 将DTO映射为实体
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns>实体</returns>
    protected abstract TEntity MapToEntity(TCreateDto dto);

    /// <summary>
    /// 将DTO映射为实体
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <param name="entity">实体</param>
    protected abstract void MapToEntity(TUpdateDto dto, TEntity entity);

    /// <summary>
    /// 获取主键
    /// </summary>
    /// <param name="dto">DTO</param>
    /// <returns>主键</returns>
    protected abstract long GetId(TUpdateDto dto);
} 
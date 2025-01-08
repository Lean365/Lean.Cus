using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lean.Cus.Domain.Repositories;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Lean.Cus.Infrastructure.Repositories;

/// <summary>
/// 仓储实现
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public class LeanRepository<TEntity> : ILeanRepository<TEntity> where TEntity : class, new()
{
    /// <summary>
    /// 数据库上下文
    /// </summary>
    protected readonly ISqlSugarClient DbContext;

    /// <summary>
    /// 日志记录器
    /// </summary>
    protected readonly ILogger<LeanRepository<TEntity>> Logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dbContext">数据库上下文</param>
    /// <param name="logger">日志记录器</param>
    public LeanRepository(ISqlSugarClient dbContext, ILogger<LeanRepository<TEntity>> logger)
    {
        DbContext = dbContext;
        Logger = logger;
    }

    /// <inheritdoc/>
    public ISugarQueryable<TEntity> AsQueryable()
    {
        return DbContext.Queryable<TEntity>();
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetByIdAsync(long id)
    {
        try
        {
            return await DbContext.Queryable<TEntity>().InSingleAsync(id);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "根据ID获取实体异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return await DbContext.Queryable<TEntity>().FirstAsync(expression);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "根据条件获取实体异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> InsertAsync(TEntity entity)
    {
        try
        {
            return await DbContext.Insertable(entity).ExecuteCommandAsync() > 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "新增实体异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> InsertRangeAsync(List<TEntity> entities)
    {
        try
        {
            return await DbContext.Insertable(entities).ExecuteCommandAsync() > 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "批量新增实体异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateAsync(TEntity entity)
    {
        try
        {
            return await DbContext.Updateable(entity).ExecuteCommandAsync() > 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "更新实体异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> UpdateRangeAsync(List<TEntity> entities)
    {
        try
        {
            return await DbContext.Updateable(entities).ExecuteCommandAsync() > 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "批量更新实体异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(long id)
    {
        try
        {
            return await DbContext.Deleteable<TEntity>().In(id).ExecuteCommandAsync() > 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "删除实体异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteRangeAsync(List<long> ids)
    {
        try
        {
            return await DbContext.Deleteable<TEntity>().In(ids).ExecuteCommandAsync() > 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "批量删除实体异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return await DbContext.Deleteable<TEntity>().Where(expression).ExecuteCommandAsync() > 0;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "根据条件删除实体异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return await DbContext.Queryable<TEntity>().AnyAsync(expression);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "根据条件判断是否存在异常");
            throw;
        }
    }

    /// <inheritdoc/>
    public async Task<int> CountAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            return await DbContext.Queryable<TEntity>().CountAsync(expression);
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "根据条件获取数量异常");
            throw;
        }
    }
} 
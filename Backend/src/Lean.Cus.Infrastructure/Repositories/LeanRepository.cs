using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Lean.Cus.Domain.IRepositories;
using SqlSugar;

namespace Lean.Cus.Infrastructure.Repositories;

/// <summary>
/// 通用仓储实现
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public class LeanRepository<TEntity> : ILeanRepository<TEntity> where TEntity : class, new()
{
    protected readonly ISqlSugarClient _db;

    public LeanRepository(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 新增实体
    /// </summary>
    public virtual async Task<int> InsertAsync(TEntity entity)
    {
        return await _db.Insertable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 批量新增实体
    /// </summary>
    public virtual async Task<int> InsertRangeAsync(IEnumerable<TEntity> entities)
    {
        return await _db.Insertable(entities.ToList()).ExecuteCommandAsync();
    }

    /// <summary>
    /// 更新实体
    /// </summary>
    public virtual async Task<int> UpdateAsync(TEntity entity)
    {
        return await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 批量更新实体
    /// </summary>
    public virtual async Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities)
    {
        return await _db.Updateable(entities.ToList()).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除实体
    /// </summary>
    public virtual async Task<int> DeleteAsync(TEntity entity)
    {
        return await _db.Deleteable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 批量删除实体
    /// </summary>
    public virtual async Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities)
    {
        return await _db.Deleteable(entities.ToList()).ExecuteCommandAsync();
    }

    /// <summary>
    /// 根据条件删除实体
    /// </summary>
    public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _db.Deleteable<TEntity>().Where(predicate).ExecuteCommandAsync();
    }

    /// <summary>
    /// 根据主键获取实体
    /// </summary>
    public virtual async Task<TEntity> GetByIdAsync(object id)
    {
        return await _db.Queryable<TEntity>().InSingleAsync(id);
    }

    /// <summary>
    /// 根据条件获取实体
    /// </summary>
    public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _db.Queryable<TEntity>().FirstAsync(predicate);
    }

    /// <summary>
    /// 获取所有实体
    /// </summary>
    public virtual async Task<List<TEntity>> GetListAsync()
    {
        return await _db.Queryable<TEntity>().ToListAsync();
    }

    /// <summary>
    /// 根据条件获取实体集合
    /// </summary>
    public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _db.Queryable<TEntity>().Where(predicate).ToListAsync();
    }

    /// <summary>
    /// 分页获取实体集合
    /// </summary>
    public virtual async Task<List<TEntity>> GetPageListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, RefAsync<int> total)
    {
        return await _db.Queryable<TEntity>()
            .Where(predicate)
            .ToPageListAsync(pageIndex, pageSize, total);
    }

    /// <summary>
    /// 检查是否存在
    /// </summary>
    public virtual async Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _db.Queryable<TEntity>().AnyAsync(predicate);
    }
} 
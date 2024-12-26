using Lean.Cus.Application.Interfaces;
using Lean.Cus.Domain.Entities;
using SqlSugar;
using Lean.Cus.Infrastructure.Extensions;

namespace Lean.Cus.Application.Services;

/// <summary>
/// 基础服务实现
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public abstract class BaseService<TEntity> : IBaseService<TEntity> where TEntity : LeanBaseEntity<long>, new()
{
    protected readonly ISqlSugarClient _db;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db">数据库客户端</param>
    protected BaseService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 获取实体
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>实体</returns>
    public virtual async Task<TEntity> GetAsync(long id)
    {
        return await _db.Queryable<TEntity>().GetByIdAsync(id);
    }

    /// <summary>
    /// 获取实体列表
    /// </summary>
    /// <returns>实体列表</returns>
    public virtual async Task<List<TEntity>> GetListAsync()
    {
        return await _db.Queryable<TEntity>().ToListAsync();
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>结果</returns>
    public virtual async Task<bool> AddAsync(TEntity entity)
    {
        return await _db.Insertable(entity).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>结果</returns>
    public virtual async Task<bool> UpdateAsync(TEntity entity)
    {
        return await _db.Updateable(entity).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>结果</returns>
    public virtual async Task<bool> DeleteAsync(long id)
    {
        return await _db.Deleteable<TEntity>()
            .Where(e => e.Id == id)
            .ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids">主键集合</param>
    /// <returns>结果</returns>
    public virtual async Task<bool> DeleteAsync(params long[] ids)
    {
        return await _db.Deleteable<TEntity>()
            .Where(e => ids.Contains(e.Id))
            .ExecuteCommandHasChangeAsync();
    }
} 
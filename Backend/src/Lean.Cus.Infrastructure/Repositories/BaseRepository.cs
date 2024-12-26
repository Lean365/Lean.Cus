using System.Linq.Expressions;
using Lean.Cus.Domain.IRepositories;
using SqlSugar;

namespace Lean.Cus.Infrastructure.Repositories;

public class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class, new()
{
    private readonly ISqlSugarClient _db;

    public BaseRepository(ISqlSugarClient db)
    {
        _db = db;
    }

    public async Task<TEntity?> GetAsync(TKey id)
    {
        return await _db.Queryable<TEntity>().InSingleAsync(id);
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _db.Queryable<TEntity>().FirstAsync(predicate);
    }

    public async Task<List<TEntity>> GetListAsync()
    {
        return await _db.Queryable<TEntity>().ToListAsync();
    }

    public async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate)
    {
        return await _db.Queryable<TEntity>().Where(predicate).ToListAsync();
    }

    public async Task<bool> CreateAsync(TEntity entity)
    {
        return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
    }

    public async Task<bool> UpdateAsync(TEntity entity)
    {
        return await _db.Updateable(entity).ExecuteCommandAsync() > 0;
    }

    public async Task<bool> DeleteAsync(TKey id)
    {
        return await _db.Deleteable<TEntity>().In(id).ExecuteCommandAsync() > 0;
    }

    public async Task<bool> DeleteAsync(TEntity entity)
    {
        return await _db.Deleteable(entity).ExecuteCommandAsync() > 0;
    }
} 
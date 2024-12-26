using SqlSugar;

namespace Lean.Cus.Infrastructure.Extensions;

/// <summary>
/// SqlSugar 扩展方法
/// </summary>
public static class SqlSugarExtensions
{
    /// <summary>
    /// 执行命令并返回是否有变更
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="insertable">插入对象</param>
    /// <returns>是否有变更</returns>
    public static async Task<bool> ExecuteCommandHasChangeAsync<T>(this IInsertable<T> insertable) where T : class, new()
    {
        return await insertable.ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 执行命令并返回是否有变更
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    /// <param name="updateable">更新对象</param>
    /// <returns>是否有变更</returns>
    public static async Task<bool> ExecuteCommandHasChangeAsync<T>(this IUpdateable<T> updateable) where T : class, new()
    {
        return await updateable.ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 执行命令并返回是否有变更
    /// </summary>
    /// <typeparam name="T">实体类���</typeparam>
    /// <param name="deleteable">删除对象</param>
    /// <returns>是否有变更</returns>
    public static async Task<bool> ExecuteCommandHasChangeAsync<T>(this IDeleteable<T> deleteable) where T : class, new()
    {
        return await deleteable.ExecuteCommandAsync() > 0;
    }

    public static async Task<T> GetByIdAsync<T>(this ISugarQueryable<T> queryable, long id) where T : class, new()
    {
        return await queryable.InSingleAsync(id);
    }

    public static async Task<T> GetFirstAsync<T>(this ISugarQueryable<T> queryable) where T : class, new()
    {
        return await queryable.FirstAsync();
    }

    public static async Task<bool> InsertAsync<T>(this ISugarQueryable<T> queryable, T entity) where T : class, new()
    {
        return await queryable.Context.Insertable(entity).ExecuteCommandHasChangeAsync();
    }

    public static async Task<bool> UpdateAsync<T>(this ISugarQueryable<T> queryable, T entity) where T : class, new()
    {
        return await queryable.Context.Updateable(entity).ExecuteCommandHasChangeAsync();
    }

    public static async Task<bool> DeleteAsync<T>(this ISugarQueryable<T> queryable, T entity) where T : class, new()
    {
        return await queryable.Context.Deleteable(entity).ExecuteCommandHasChangeAsync();
    }

    public static async Task<bool> DeleteAsync<T>(this ISugarQueryable<T> queryable, params long[] ids) where T : class, new()
    {
        return await queryable.Context.Deleteable<T>().In(ids).ExecuteCommandHasChangeAsync();
    }
} 
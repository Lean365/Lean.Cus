using Lean.Cus.Domain.Entities;

namespace Lean.Cus.Application.Interfaces;

/// <summary>
/// 基础服务接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface IBaseService<TEntity> where TEntity : LeanBaseEntity<long>
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>结果</returns>
    Task<bool> AddAsync(TEntity entity);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>结果</returns>
    Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>结果</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <param name="ids">主键集合</param>
    /// <returns>结果</returns>
    Task<bool> DeleteAsync(long[] ids);

    /// <summary>
    /// 获取实体
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>实体</returns>
    Task<TEntity> GetAsync(long id);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns>列表</returns>
    Task<List<TEntity>> GetListAsync();
} 
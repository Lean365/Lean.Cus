using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SqlSugar;

namespace Lean.Cus.Domain.Repositories;

/// <summary>
/// 仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface ILeanRepository<TEntity> where TEntity : class, new()
{
    /// <summary>
    /// 获取查询对象
    /// </summary>
    /// <returns>查询对象</returns>
    ISugarQueryable<TEntity> AsQueryable();

    /// <summary>
    /// 根据ID获取实体
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <returns>实体</returns>
    Task<TEntity?> GetByIdAsync(long id);

    /// <summary>
    /// 根据条件获取实体
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <returns>实体</returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// 新增实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>是否成功</returns>
    Task<bool> InsertAsync(TEntity entity);

    /// <summary>
    /// 批量新增实体
    /// </summary>
    /// <param name="entities">实体集合</param>
    /// <returns>是否成功</returns>
    Task<bool> InsertRangeAsync(List<TEntity> entities);

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// 批量更新实体
    /// </summary>
    /// <param name="entities">实体集合</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateRangeAsync(List<TEntity> entities);

    /// <summary>
    /// 删除实体
    /// </summary>
    /// <param name="id">主键ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除实体
    /// </summary>
    /// <param name="ids">主键ID集合</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteRangeAsync(List<long> ids);

    /// <summary>
    /// 根据条件删除实体
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// 根据条件判断是否存在
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <returns>是否存在</returns>
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> expression);

    /// <summary>
    /// 根据条件获取数量
    /// </summary>
    /// <param name="expression">条件表达式</param>
    /// <returns>数量</returns>
    Task<int> CountAsync(Expression<Func<TEntity, bool>> expression);
} 
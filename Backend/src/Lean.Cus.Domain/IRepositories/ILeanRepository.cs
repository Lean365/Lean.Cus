using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using SqlSugar;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Domain.IRepositories;

/// <summary>
/// 通用仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
public interface ILeanRepository<TEntity> where TEntity : class
{
    /// <summary>
    /// 新增实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>影响行数</returns>
    Task<int> InsertAsync(TEntity entity);

    /// <summary>
    /// 批量新增实体
    /// </summary>
    /// <param name="entities">实体集合</param>
    /// <returns>影响行数</returns>
    Task<int> InsertRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 更新实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>影响行数</returns>
    Task<int> UpdateAsync(TEntity entity);

    /// <summary>
    /// 批量更新实体
    /// </summary>
    /// <param name="entities">实体集合</param>
    /// <returns>影响行数</returns>
    Task<int> UpdateRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 删除实体
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>影响行数</returns>
    Task<int> DeleteAsync(TEntity entity);

    /// <summary>
    /// 批量删除实体
    /// </summary>
    /// <param name="entities">实体集合</param>
    /// <returns>影响行数</returns>
    Task<int> DeleteRangeAsync(IEnumerable<TEntity> entities);

    /// <summary>
    /// 根据条件删除实体
    /// </summary>
    /// <param name="predicate">条件表达式</param>
    /// <returns>影响行数</returns>
    Task<int> DeleteAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 根据主键获取实体
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>实体</returns>
    Task<TEntity> GetByIdAsync(object id);

    /// <summary>
    /// 根据条件获取实体
    /// </summary>
    /// <param name="predicate">条件表达式</param>
    /// <returns>实体</returns>
    Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 获取所有实体
    /// </summary>
    /// <returns>实体集合</returns>
    Task<List<TEntity>> GetListAsync();

    /// <summary>
    /// 根据条件获取实体集合
    /// </summary>
    /// <param name="predicate">条件表达式</param>
    /// <returns>实体集合</returns>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 分页获取实体集合
    /// </summary>
    /// <param name="predicate">条件表达式</param>
    /// <param name="pageIndex">页码</param>
    /// <param name="pageSize">页大小</param>
    /// <param name="total">总记录数</param>
    /// <returns>实体集合</returns>
    Task<List<TEntity>> GetPageListAsync(Expression<Func<TEntity, bool>> predicate, int pageIndex, int pageSize, RefAsync<int> total);

    /// <summary>
    /// 检查是否存在
    /// </summary>
    /// <param name="predicate">条件表达式</param>
    /// <returns>是否存在</returns>
    Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 根据条件获取第一个实体
    /// </summary>
    /// <param name="predicate">条件表达式</param>
    /// <returns>实体</returns>
    Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 分页查询实体集合
    /// </summary>
    /// <param name="predicate">条件表达式</param>
    /// <param name="query">分页查询参数</param>
    /// <returns>分页结果</returns>
    Task<PagedResults<TEntity>> GetPagedListAsync(Expression<Func<TEntity, bool>> predicate, PagedQuery query);
} 
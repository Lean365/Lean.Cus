//===================================================
// 项目名称：Lean.Cus.Domain
// 文件名称：IBaseRepository
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.14
// 更新时间：2024.01.14
// 备注说明：基础仓储接口
//===================================================

using System.Linq.Expressions;

namespace Lean.Cus.Domain.IRepositories;

/// <summary>
/// 基础仓储接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TKey">主键类型</typeparam>
public interface IBaseRepository<TEntity, TKey> where TEntity : class
{
    /// <summary>
    /// 获取单个实体
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>实体</returns>
    Task<TEntity?> GetAsync(TKey id);

    /// <summary>
    /// 获取单个实体
    /// </summary>
    /// <param name="predicate">条件</param>
    /// <returns>实体</returns>
    Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns>列表</returns>
    Task<List<TEntity>> GetListAsync();

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="predicate">条件</param>
    /// <returns>列表</returns>
    Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> predicate);

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>是否成功</returns>
    Task<bool> CreateAsync(TEntity entity);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(TEntity entity);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(TKey id);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="entity">实体</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(TEntity entity);
} 
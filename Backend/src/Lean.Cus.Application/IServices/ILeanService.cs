using System.Threading.Tasks;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.IServices;

/// <summary>
/// 服务接口
/// </summary>
/// <typeparam name="TEntity">实体类型</typeparam>
/// <typeparam name="TDto">DTO类型</typeparam>
/// <typeparam name="TQueryDto">查询DTO类型</typeparam>
/// <typeparam name="TCreateDto">创建DTO类型</typeparam>
/// <typeparam name="TUpdateDto">更新DTO类型</typeparam>
public interface ILeanService<TEntity, TDto, TQueryDto, TCreateDto, TUpdateDto>
    where TEntity : class, new()
    where TDto : class, new()
    where TQueryDto : LeanPagedDto
    where TCreateDto : class, new()
    where TUpdateDto : class, new()
{
    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>列表</returns>
    Task<LeanPagedList<TDto>> GetListAsync(TQueryDto queryDto);

    /// <summary>
    /// 获取详情
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>详情</returns>
    Task<TDto> GetAsync(long id);

    /// <summary>
    /// 创建
    /// </summary>
    /// <param name="createDto">创建对象</param>
    /// <returns>主键</returns>
    Task<long> CreateAsync(TCreateDto createDto);

    /// <summary>
    /// 更新
    /// </summary>
    /// <param name="updateDto">更新对象</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(TUpdateDto updateDto);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id">主键</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);
} 
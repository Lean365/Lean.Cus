using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Cus.Application.Dtos.Log;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Interfaces.Log
{
    /// <summary>
    /// 操作日志服务接口
    /// </summary>
    public interface ILeanOperationLogService
    {
        /// <summary>
        /// 新增操作日志
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>结果</returns>
        Task<LeanOperationLogDto> CreateAsync(LeanOperationLogCreateDto input);

        /// <summary>
        /// 删除操作日志
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>结果</returns>
        Task<bool> DeleteAsync(long id);

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>结果</returns>
        Task<LeanOperationLogDto> GetAsync(long id);

        /// <summary>
        /// 获取操作日志列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>结果</returns>
        Task<List<LeanOperationLogDto>> GetListAsync(LeanOperationLogQueryDto query);

        /// <summary>
        /// 分页查询操作日志
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>结果</returns>
        Task<PagedResults<LeanOperationLogDto>> GetPagedListAsync(LeanOperationLogQueryDto query);

        /// <summary>
        /// 导出操作日志
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>结果</returns>
        Task<byte[]> ExportAsync(LeanOperationLogQueryDto query);

        /// <summary>
        /// 清空操作日志
        /// </summary>
        /// <returns>结果</returns>
        Task<bool> ClearAsync();
    }
} 
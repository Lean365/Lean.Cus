using Lean.Cus.Application.Dtos.Log;
using Lean.Cus.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Application.Interfaces.Log;

/// <summary>
/// 登录日志服务接口
/// </summary>
public interface ILeanLoginLogService
{
    /// <summary>
    /// 新增登录日志
    /// </summary>
    Task<LeanLoginLogDto> AddAsync(LeanLoginLogDto dto);

    /// <summary>
    /// 更新登录日志
    /// </summary>
    Task<bool> UpdateAsync(LeanLoginLogDto dto);

    /// <summary>
    /// 删除登录日志
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 获取登录日志
    /// </summary>
    Task<LeanLoginLogDto> GetAsync(long id);

    /// <summary>
    /// 获取登录日志列表
    /// </summary>
    Task<List<LeanLoginLogDto>> GetListAsync(LeanLoginLogQueryDto query);

    /// <summary>
    /// 分页查询登录日志
    /// </summary>
    Task<PagedResults<LeanLoginLogDto>> GetPagedListAsync(LeanLoginLogQueryDto query);

    /// <summary>
    /// 导入登录日志数据
    /// </summary>
    Task<(List<LeanLoginLogDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes);

    /// <summary>
    /// 导出登录日志数据
    /// </summary>
    Task<byte[]> ExportAsync(LeanLoginLogQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<byte[]> GetImportTemplateAsync();

    /// <summary>
    /// 清空登录日志
    /// </summary>
    Task<bool> ClearAsync();
} 
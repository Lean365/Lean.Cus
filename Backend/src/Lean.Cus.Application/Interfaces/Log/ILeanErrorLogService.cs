using Lean.Cus.Application.Dtos.Log;
using Lean.Cus.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Application.Interfaces.Log;

/// <summary>
/// 错误日志服务接口
/// </summary>
public interface ILeanErrorLogService
{
    /// <summary>
    /// 新增错误日志
    /// </summary>
    Task<LeanErrorLogDto> AddAsync(LeanErrorLogDto dto);

    /// <summary>
    /// 更新错误日志
    /// </summary>
    Task<bool> UpdateAsync(LeanErrorLogDto dto);

    /// <summary>
    /// 删除错误日志
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 获取错误日志
    /// </summary>
    Task<LeanErrorLogDto> GetAsync(long id);

    /// <summary>
    /// 获取错误日志列表
    /// </summary>
    Task<List<LeanErrorLogDto>> GetListAsync(LeanErrorLogQueryDto query);

    /// <summary>
    /// 分页查询错误日志
    /// </summary>
    Task<PagedResults<LeanErrorLogDto>> GetPagedListAsync(LeanErrorLogQueryDto query);

    /// <summary>
    /// 导入错误日志数据
    /// </summary>
    Task<(List<LeanErrorLogDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes);

    /// <summary>
    /// 导出错误日志数据
    /// </summary>
    Task<byte[]> ExportAsync(LeanErrorLogQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<byte[]> GetImportTemplateAsync();

    /// <summary>
    /// 清空错误日志
    /// </summary>
    Task<bool> ClearAsync();
} 
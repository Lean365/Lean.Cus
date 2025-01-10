using Lean.Cus.Application.Dtos.Log;
using Lean.Cus.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Application.Interfaces.Log;

/// <summary>
/// SQL日志服务接口
/// </summary>
public interface ILeanSqlLogService
{
    /// <summary>
    /// 新增SQL日志
    /// </summary>
    Task<LeanSqlLogDto> AddAsync(LeanSqlLogDto dto);

    /// <summary>
    /// 更新SQL日志
    /// </summary>
    Task<bool> UpdateAsync(LeanSqlLogDto dto);

    /// <summary>
    /// 删除SQL日志
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 获取SQL日志
    /// </summary>
    Task<LeanSqlLogDto> GetAsync(long id);

    /// <summary>
    /// 获取SQL日志列表
    /// </summary>
    Task<List<LeanSqlLogDto>> GetListAsync(LeanSqlLogQueryDto query);

    /// <summary>
    /// 分页查询SQL日志
    /// </summary>
    Task<PagedResults<LeanSqlLogDto>> GetPagedListAsync(LeanSqlLogQueryDto query);

    /// <summary>
    /// 导入SQL日志数据
    /// </summary>
    Task<(List<LeanSqlLogDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes);

    /// <summary>
    /// 导出SQL日志数据
    /// </summary>
    Task<byte[]> ExportAsync(LeanSqlLogQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<byte[]> GetImportTemplateAsync();

    /// <summary>
    /// 清空SQL日志
    /// </summary>
    Task<bool> ClearAsync();
} 
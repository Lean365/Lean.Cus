using Lean.Cus.Application.Dtos.Log;
using Lean.Cus.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Application.Interfaces.Log;

/// <summary>
/// 审计日志服务接口
/// </summary>
public interface ILeanAuditLogService
{
    /// <summary>
    /// 新增审计日志
    /// </summary>
    Task<LeanAuditLogDto> AddAsync(LeanAuditLogDto dto);

    /// <summary>
    /// 更新审计日志
    /// </summary>
    Task<bool> UpdateAsync(LeanAuditLogDto dto);

    /// <summary>
    /// 删除审计日志
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 获取审计日志
    /// </summary>
    Task<LeanAuditLogDto> GetAsync(long id);

    /// <summary>
    /// 获取审计日志列表
    /// </summary>
    Task<List<LeanAuditLogDto>> GetListAsync(LeanAuditLogQueryDto query);

    /// <summary>
    /// 分页查询审计日志
    /// </summary>
    Task<PagedResults<LeanAuditLogDto>> GetPagedListAsync(LeanAuditLogQueryDto query);

    /// <summary>
    /// 导入审计日志数据
    /// </summary>
    Task<(List<LeanAuditLogDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes);

    /// <summary>
    /// 导出审计日志数据
    /// </summary>
    Task<byte[]> ExportAsync(LeanAuditLogQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<byte[]> GetImportTemplateAsync();

    /// <summary>
    /// 清空审计日志
    /// </summary>
    Task<bool> ClearAsync();
} 
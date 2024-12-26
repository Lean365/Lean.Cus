//===================================================
// 项目名称：Lean.Cus.Application.Services.Monitor
// 文件名称：ILeanSqlDiffLogService
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：SQL差异日志服务接口
//===================================================

using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Monitor;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Application.Services.Monitor;

/// <summary>
/// SQL差异日志服务接口
/// </summary>
public interface ILeanSqlDiffLogService
{
    /// <summary>
    /// 获取分页列表
    /// </summary>
    Task<LeanPageResult<LeanSqlDiffLogDto>> GetPageListAsync(LeanSqlDiffLogQueryDto queryDto);

    /// <summary>
    /// 获取详情
    /// </summary>
    Task<LeanSqlDiffLogDto> GetByIdAsync(long id);

    /// <summary>
    /// 记录SQL差异日志
    /// </summary>
    Task<bool> RecordAsync(LeanSqlDiffLogInputDto inputDto);

    /// <summary>
    /// 删除日志
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除日志
    /// </summary>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 清空所有日志
    /// </summary>
    Task<bool> ClearAllAsync();
} 
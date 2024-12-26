//===================================================
// 项目名称：Lean.Cus.Domain.IRepositories.Monitor
// 文件名称：ILeanAuditLogRepository
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：审核日志仓储接口
//===================================================

using Lean.Cus.Domain.Entities.Monitor;

namespace Lean.Cus.Domain.IRepositories.Monitor;

/// <summary>
/// 审核日志仓储接口
/// </summary>
public interface ILeanAuditLogRepository
{
    /// <summary>
    /// 创建审核日志
    /// </summary>
    /// <param name="auditLog">审核日志信息</param>
    /// <returns>创建结果</returns>
    Task<bool> CreateAsync(LeanAuditLog auditLog);

    /// <summary>
    /// 删除审核日志
    /// </summary>
    /// <param name="ids">日志ID数组</param>
    /// <returns>删除结果</returns>
    Task<bool> DeleteAsync(long[] ids);

    /// <summary>
    /// 清空审核日志
    /// </summary>
    /// <returns>清空结果</returns>
    Task<bool> ClearAsync();
} 
//===================================================
// 项目名称：Lean.Cus.Infrastructure.Repositories.Monitor
// 文件名称：LeanAuditLogRepository
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：审核日志仓储实现
//===================================================

using Lean.Cus.Domain.Entities.Monitor;
using Lean.Cus.Domain.IRepositories.Monitor;
using SqlSugar;

namespace Lean.Cus.Infrastructure.Repositories.Monitor;

/// <summary>
/// 审核日志仓储实现
/// </summary>
public class LeanAuditLogRepository : ILeanAuditLogRepository
{
    private readonly ISqlSugarClient _db;

    public LeanAuditLogRepository(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 创建审核日志
    /// </summary>
    /// <param name="auditLog">审核日志信息</param>
    /// <returns>创建结果</returns>
    public async Task<bool> CreateAsync(LeanAuditLog auditLog)
    {
        return await _db.Insertable(auditLog).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 删除审核日志
    /// </summary>
    /// <param name="ids">日志ID数组</param>
    /// <returns>删除结果</returns>
    public async Task<bool> DeleteAsync(long[] ids)
    {
        return await _db.Deleteable<LeanAuditLog>().In(l => l.Id, ids).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 清空审核日志
    /// </summary>
    /// <returns>清空结果</returns>
    public async Task<bool> ClearAsync()
    {
        return await _db.Deleteable<LeanAuditLog>().ExecuteCommandAsync() > 0;
    }
} 
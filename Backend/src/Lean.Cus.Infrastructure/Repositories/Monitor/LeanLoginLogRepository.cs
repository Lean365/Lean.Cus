//===================================================
// 项目名称：Lean.Cus.Infrastructure.Repositories.Monitor
// 文件名称：LeanLoginLogRepository
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：登录日志仓储实现
//===================================================

using Lean.Cus.Domain.Entities.Monitor;
using Lean.Cus.Domain.IRepositories.Monitor;
using SqlSugar;

namespace Lean.Cus.Infrastructure.Repositories.Monitor;

/// <summary>
/// 登录日志仓储实现
/// </summary>
public class LeanLoginLogRepository : ILeanLoginLogRepository
{
    private readonly ISqlSugarClient _db;

    public LeanLoginLogRepository(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 添加登录日志
    /// </summary>
    /// <param name="log">登录日志</param>
    /// <returns>是否成功</returns>
    public async Task<bool> AddAsync(LeanLoginLog log)
    {
        return await _db.Insertable(log).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 清空登录日志
    /// </summary>
    /// <returns>是否成功</returns>
    public async Task<bool> ClearAsync()
    {
        return await _db.Deleteable<LeanLoginLog>().ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 删除登录日志
    /// </summary>
    /// <param name="ids">日志ID集合</param>
    /// <returns>是否成功</returns>
    public async Task<bool> DeleteAsync(long[] ids)
    {
        return await _db.Deleteable<LeanLoginLog>()
            .Where(l => ids.Contains(l.Id))
            .ExecuteCommandAsync() > 0;
    }
} 
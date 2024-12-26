//===================================================
// 项目名称：Lean.Cus.Domain.IRepositories.Monitor
// 文件名称：ILeanLoginLogRepository
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：登录日志仓储接口
//===================================================

using Lean.Cus.Domain.Entities.Monitor;

namespace Lean.Cus.Domain.IRepositories.Monitor;

/// <summary>
/// 登录日志仓储接口
/// </summary>
public interface ILeanLoginLogRepository
{
    /// <summary>
    /// 添加登录日志
    /// </summary>
    /// <param name="log">登录日志</param>
    /// <returns>是否成功</returns>
    Task<bool> AddAsync(LeanLoginLog log);

    /// <summary>
    /// 清空登录日志
    /// </summary>
    /// <returns>是否成功</returns>
    Task<bool> ClearAsync();

    /// <summary>
    /// 删除登录日志
    /// </summary>
    /// <param name="ids">日志ID集合</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long[] ids);
} 
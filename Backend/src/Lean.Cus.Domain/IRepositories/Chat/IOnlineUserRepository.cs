using Lean.Cus.Domain.Entities.Chat;
using Lean.Cus.Domain.IRepositories;

namespace Lean.Cus.Domain.IRepositories.Chat;

/// <summary>
/// 在线用户仓储接口
/// </summary>
public interface IOnlineUserRepository : IBaseRepository<LeanOnlineUser, long>
{
    /// <summary>
    /// 获取在线用户列表
    /// </summary>
    /// <returns>在线用户列表</returns>
    Task<List<LeanOnlineUser>> GetOnlineUsersAsync();

    /// <summary>
    /// 更新用户状态
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <param name="status">状态</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateUserStatusAsync(string connectionId, int status);

    /// <summary>
    /// 更新用户最后活动时间
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateLastActiveTimeAsync(string connectionId);

    /// <summary>
    /// 更新客户端信息
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <param name="resolution">分辨率</param>
    /// <param name="networkType">网络类型</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateClientInfoAsync(string connectionId, string resolution, string networkType);
} 
using Lean.Cus.Domain.Entities.Chat;
using Lean.Cus.Domain.IRepositories.Chat;
using SqlSugar;

namespace Lean.Cus.Infrastructure.Repositories.Chat;

/// <summary>
/// 在线用户仓储实现
/// </summary>
public class OnlineUserRepository : BaseRepository<LeanOnlineUser, long>, IOnlineUserRepository
{
    private readonly ISqlSugarClient _db;

    public OnlineUserRepository(ISqlSugarClient db) : base(db)
    {
        _db = db;
    }

    /// <summary>
    /// 获取在线用户列表
    /// </summary>
    /// <returns>在线用户列表</returns>
    public async Task<List<LeanOnlineUser>> GetOnlineUsersAsync()
    {
        return await _db.Queryable<LeanOnlineUser>()
            .Where(u => u.Status == 1)
            .ToListAsync();
    }

    /// <summary>
    /// 更新用户状态
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <param name="status">状态</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateUserStatusAsync(string connectionId, int status)
    {
        return await _db.Updateable<LeanOnlineUser>()
            .SetColumns(u => new LeanOnlineUser { Status = status })
            .Where(u => u.ConnectionId == connectionId)
            .ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 更新用户最后活动时间
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateLastActiveTimeAsync(string connectionId)
    {
        return await _db.Updateable<LeanOnlineUser>()
            .SetColumns(u => new LeanOnlineUser { LastActiveTime = DateTime.Now })
            .Where(u => u.ConnectionId == connectionId)
            .ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 更新客户端信息
    /// </summary>
    /// <param name="connectionId">连接ID</param>
    /// <param name="resolution">分辨率</param>
    /// <param name="networkType">网络类型</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateClientInfoAsync(string connectionId, string resolution, string networkType)
    {
        return await _db.Updateable<LeanOnlineUser>()
            .SetColumns(u => new LeanOnlineUser 
            { 
                Resolution = resolution,
                NetworkType = networkType,
                LastActiveTime = DateTime.Now
            })
            .Where(u => u.ConnectionId == connectionId)
            .ExecuteCommandHasChangeAsync();
    }
} 
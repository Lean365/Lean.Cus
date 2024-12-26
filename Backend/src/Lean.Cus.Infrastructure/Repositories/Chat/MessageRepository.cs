using Lean.Cus.Domain.Entities.Chat;
using Lean.Cus.Domain.IRepositories.Chat;
using SqlSugar;

namespace Lean.Cus.Infrastructure.Repositories.Chat;

/// <summary>
/// 消息仓储实现
/// </summary>
public class MessageRepository : BaseRepository<LeanMessage, long>, IMessageRepository
{
    private readonly ISqlSugarClient _db;

    public MessageRepository(ISqlSugarClient db) : base(db)
    {
        _db = db;
    }

    /// <summary>
    /// 获取用户的未读消息列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>未读消息列表</returns>
    public async Task<List<LeanMessage>> GetUnreadMessagesAsync(long userId)
    {
        return await _db.Queryable<LeanMessage>()
            .Where(m => m.ReceiverId == userId && m.Status == 0)
            .OrderBy(m => m.CreateTime)
            .ToListAsync();
    }

    /// <summary>
    /// 获取用户的历史消息列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="pageIndex">页码</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>历史消息列表</returns>
    public async Task<List<LeanMessage>> GetHistoryMessagesAsync(long userId, int pageIndex, int pageSize)
    {
        return await _db.Queryable<LeanMessage>()
            .Where(m => m.ReceiverId == userId || m.SenderId == userId)
            .OrderByDescending(m => m.CreateTime)
            .ToPageListAsync(pageIndex, pageSize);
    }

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    /// <param name="messageId">消息ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> MarkAsReadAsync(long messageId)
    {
        return await _db.Updateable<LeanMessage>()
            .SetColumns(m => new LeanMessage { Status = 1 })
            .Where(m => m.Id == messageId)
            .ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 批量标记消息为已读
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> MarkAllAsReadAsync(long userId)
    {
        return await _db.Updateable<LeanMessage>()
            .SetColumns(m => new LeanMessage { Status = 1 })
            .Where(m => m.ReceiverId == userId && m.Status == 0)
            .ExecuteCommandHasChangeAsync();
    }
} 
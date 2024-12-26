using Lean.Cus.Domain.Entities.Chat;
using Lean.Cus.Domain.IRepositories;

namespace Lean.Cus.Domain.IRepositories.Chat;

/// <summary>
/// 消息仓储接口
/// </summary>
public interface IMessageRepository : IBaseRepository<LeanMessage, long>
{
    /// <summary>
    /// 获取用户的未读消息列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>未读消息列表</returns>
    Task<List<LeanMessage>> GetUnreadMessagesAsync(long userId);

    /// <summary>
    /// 获取用户的历史消息列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="pageIndex">页码</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>历史消息列表</returns>
    Task<List<LeanMessage>> GetHistoryMessagesAsync(long userId, int pageIndex, int pageSize);

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    /// <param name="messageId">消息ID</param>
    /// <returns>是否成功</returns>
    Task<bool> MarkAsReadAsync(long messageId);

    /// <summary>
    /// 批量标记消息为已读
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> MarkAllAsReadAsync(long userId);
} 
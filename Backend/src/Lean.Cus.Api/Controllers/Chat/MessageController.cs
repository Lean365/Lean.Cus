using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Exceptions;
using Lean.Cus.Common.Results;
using Lean.Cus.Domain.Entities.Chat;
using Lean.Cus.Domain.IRepositories.Chat;
using Lean.Cus.Api.Authorization;
using Lean.Cus.Api.Attributes;

namespace Lean.Cus.Api.Controllers.Chat;

/// <summary>
/// 消息控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageRepository _messageRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="messageRepository">消息仓储</param>
    public MessageController(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository;
    }

    /// <summary>
    /// 获取历史消息
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="pageIndex">页码</param>
    /// <param name="pageSize">每页大小</param>
    /// <returns>消息列表</returns>
    [HttpGet("history")]
    [LeanPermission("chat:message:query")]
    [LeanOperLog("查询历史消息", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<List<LeanMessage>>> GetHistoryMessages(
        [FromQuery] long userId,
        [FromQuery] int pageIndex = 1,
        [FromQuery] int pageSize = 20)
    {
        if (userId <= 0)
            throw new LeanException("用户ID无效", LeanErrorCodeEnum.ValidationError);

        var messages = await _messageRepository.GetHistoryMessagesAsync(userId, pageIndex, pageSize);
        return LeanApiResult<List<LeanMessage>>.Ok(messages);
    }

    /// <summary>
    /// 获取未读消息
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>未读消息列表</returns>
    [HttpGet("unread")]
    [LeanPermission("chat:message:query")]
    [LeanOperLog("查询未读消息", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<List<LeanMessage>>> GetUnreadMessages([FromQuery] long userId)
    {
        if (userId <= 0)
            throw new LeanException("用户ID无效", LeanErrorCodeEnum.ValidationError);

        var messages = await _messageRepository.GetUnreadMessagesAsync(userId);
        return LeanApiResult<List<LeanMessage>>.Ok(messages);
    }

    /// <summary>
    /// 标记消息为已读
    /// </summary>
    /// <param name="messageId">消息ID</param>
    /// <returns>操作结果</returns>
    [HttpPost("{messageId}/mark-read")]
    [LeanPermission("chat:message:update")]
    [LeanOperLog("标记消息已读", LeanBusinessTypeEnum.Update)]
    public async Task<LeanApiResult<object>> MarkAsRead(long messageId)
    {
        if (messageId <= 0)
            throw new LeanException("消息ID无效", LeanErrorCodeEnum.ValidationError);

        await _messageRepository.MarkAsReadAsync(messageId);
        return LeanApiResult<object>.Ok(new { messageId });
    }

    /// <summary>
    /// 批量标记消息为已读
    /// </summary>
    /// <param name="messageIds">消息ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("batch-mark-read")]
    [LeanPermission("chat:message:update")]
    [LeanOperLog("批量标记消息已读", LeanBusinessTypeEnum.Update)]
    public async Task<LeanApiResult<object>> BatchMarkAsRead([FromBody] List<long> messageIds)
    {
        if (messageIds == null || !messageIds.Any())
            throw new LeanException("消息ID列表不能为空", LeanErrorCodeEnum.ValidationError);

        foreach (var messageId in messageIds)
        {
            await _messageRepository.MarkAsReadAsync(messageId);
        }
        return LeanApiResult<object>.Ok(new { messageIds });
    }

    /// <summary>
    /// 获取消息统计信息
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>统计信息</returns>
    [HttpGet("statistics")]
    [LeanPermission("chat:message:query")]
    [LeanOperLog("查询消息统计", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<object>> GetStatistics([FromQuery] long userId)
    {
        if (userId <= 0)
            throw new LeanException("用户ID无效", LeanErrorCodeEnum.ValidationError);

        var unreadMessages = await _messageRepository.GetUnreadMessagesAsync(userId);
        var allMessages = await _messageRepository.GetHistoryMessagesAsync(userId, 1, int.MaxValue);

        var statistics = new
        {
            TotalMessages = allMessages.Count,
            UnreadCount = unreadMessages.Count,
            MessagesByType = allMessages.GroupBy(m => m.Type)
                .Select(g => new { Type = g.Key, Count = g.Count() })
        };

        return LeanApiResult<object>.Ok(statistics);
    }

    /// <summary>
    /// 删除消息
    /// </summary>
    /// <param name="messageId">消息ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{messageId}")]
    [LeanPermission("chat:message:delete")]
    [LeanOperLog("删除消息", LeanBusinessTypeEnum.Delete)]
    public async Task<LeanApiResult<object>> DeleteMessage(long messageId)
    {
        if (messageId <= 0)
            throw new LeanException("消息ID无效", LeanErrorCodeEnum.ValidationError);

        await _messageRepository.DeleteAsync(messageId);
        return LeanApiResult<object>.Ok(new { messageId });
    }

    /// <summary>
    /// 批量删除消息
    /// </summary>
    /// <param name="messageIds">消息ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("batch-delete")]
    [LeanPermission("chat:message:delete")]
    [LeanOperLog("批量删除消息", LeanBusinessTypeEnum.Delete)]
    public async Task<LeanApiResult<object>> BatchDeleteMessages([FromBody] List<long> messageIds)
    {
        if (messageIds == null || !messageIds.Any())
            throw new LeanException("消息ID列表不能为空", LeanErrorCodeEnum.ValidationError);

        foreach (var messageId in messageIds)
        {
            await _messageRepository.DeleteAsync(messageId);
        }
        return LeanApiResult<object>.Ok(new { messageIds });
    }
}
using Microsoft.AspNetCore.SignalR;
using Lean.Cus.Domain.IRepositories.Chat;
using Lean.Cus.Domain.Entities.Chat;
using System.Runtime.InteropServices;

namespace Lean.Cus.Api.Hubs;

/// <summary>
/// 聊天集线器
/// </summary>
public class ChatHub : Hub
{
    private readonly IOnlineUserRepository _onlineUserRepository;
    private readonly IMessageRepository _messageRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineUserRepository">在线用户仓储</param>
    /// <param name="messageRepository">消息仓储</param>
    public ChatHub(IOnlineUserRepository onlineUserRepository, IMessageRepository messageRepository)
    {
        _onlineUserRepository = onlineUserRepository;
        _messageRepository = messageRepository;
    }

    /// <summary>
    /// 连接建立时
    /// </summary>
    /// <returns></returns>
    public override async Task OnConnectedAsync()
    {
        var connectionId = Context.ConnectionId;
        var user = new LeanOnlineUser
        {
            ConnectionId = connectionId,
            Status = 1,
            LastActiveTime = DateTime.Now
        };

        // 保存在线用户信息
        await _onlineUserRepository.CreateAsync(user);

        // 广播用户上线消息
        await Clients.All.SendAsync("UserOnline", user);

        await base.OnConnectedAsync();
    }

    /// <summary>
    /// 发送消息
    /// </summary>
    /// <param name="message">消息内容</param>
    /// <returns></returns>
    public async Task SendMessage(string message)
    {
        var connectionId = Context.ConnectionId;
        var user = await _onlineUserRepository.GetAsync(u => u.ConnectionId == connectionId);

        if (user != null)
        {
            var msg = new LeanMessage
            {
                SenderId = user.Id,
                SenderName = user.UserName,
                Content = message,
                ReceiverId = 0, // 群发
                ReceiverName = "All",
                Type = 1, // 文本消息
                Status = 0 // 未读
            };

            // 保存消息
            await _messageRepository.CreateAsync(msg);

            // 广播消息
            await Clients.All.SendAsync("ReceiveMessage", msg);
        }
    }

    /// <summary>
    /// 更新客户端信息
    /// </summary>
    /// <param name="resolution">分辨率</param>
    /// <param name="networkType">网络类型</param>
    /// <returns></returns>
    public async Task UpdateClientInfo(string resolution, string networkType)
    {
        var connectionId = Context.ConnectionId;
        var user = await _onlineUserRepository.GetAsync(u => u.ConnectionId == connectionId);

        if (user != null)
        {
            await _onlineUserRepository.UpdateClientInfoAsync(connectionId, resolution, networkType);
        }
    }

    /// <summary>
    /// 强制用户下线
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns></returns>
    public async Task ForceOffline(long userId)
    {
        var targetUser = await _onlineUserRepository.GetAsync(userId);

        if (targetUser != null)
        {
            // 更新用户状态为离线
            await _onlineUserRepository.UpdateUserStatusAsync(targetUser.ConnectionId, 0);

            // 发送强制下线通知
            await Clients.Client(targetUser.ConnectionId).SendAsync("ForceOffline", "您已被管理员强制下线");

            // 断开用户连接
            await Clients.Client(targetUser.ConnectionId).SendAsync("Disconnect");
        }
    }

    /// <summary>
    /// 心跳检测
    /// </summary>
    /// <returns></returns>
    public async Task Heartbeat()
    {
        var connectionId = Context.ConnectionId;
        var user = await _onlineUserRepository.GetAsync(u => u.ConnectionId == connectionId);

        if (user != null)
        {
            await _onlineUserRepository.UpdateLastActiveTimeAsync(connectionId);
        }
    }

    /// <summary>
    /// 连接断开时
    /// </summary>
    /// <param name="exception">异常信息</param>
    /// <returns></returns>
    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var connectionId = Context.ConnectionId;
        var user = await _onlineUserRepository.GetAsync(u => u.ConnectionId == connectionId);

        if (user != null)
        {
            // 更新用户状态为离线
            await _onlineUserRepository.UpdateUserStatusAsync(connectionId, 0);

            // 广播用户下线消息
            await Clients.All.SendAsync("UserOffline", user);
        }

        await base.OnDisconnectedAsync(exception);
    }
} 
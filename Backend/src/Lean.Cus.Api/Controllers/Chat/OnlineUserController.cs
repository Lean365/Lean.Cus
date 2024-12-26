using Lean.Cus.Common.Results;
using Lean.Cus.Domain.Entities.Chat;
using Lean.Cus.Domain.IRepositories.Chat;

namespace Lean.Cus.Api.Controllers.Chat;

/// <summary>
/// 在线用户控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class OnlineUserController : ControllerBase
{
    private readonly IOnlineUserRepository _onlineUserRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="onlineUserRepository">在线用户仓储</param>
    public OnlineUserController(IOnlineUserRepository onlineUserRepository)
    {
        _onlineUserRepository = onlineUserRepository;
    }

    /// <summary>
    /// 获取在线用户列表
    /// </summary>
    /// <returns>在线用户列表</returns>
    [HttpGet]
    public async Task<ActionResult<List<LeanOnlineUser>>> GetOnlineUsers()
    {
        var users = await _onlineUserRepository.GetOnlineUsersAsync();
        return Ok(users);
    }

    /// <summary>
    /// 获取用户详细信息
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>用户信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanOnlineUser>> GetUserById(long id)
    {
        var user = await _onlineUserRepository.GetAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    /// <summary>
    /// 强制下线
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>操作结果</returns>
    [HttpPost("{id}/force-offline")]
    public async Task<ActionResult<LeanApiResult<bool>>> ForceOffline(long id)
    {
        var user = await _onlineUserRepository.GetAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        var result = await _onlineUserRepository.UpdateUserStatusAsync(user.ConnectionId, 0);
        return Ok(LeanApiResult<bool>.Ok(result));
    }

    /// <summary>
    /// 批量强制下线
    /// </summary>
    /// <param name="userIds">用户ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("batch-force-offline")]
    public async Task<ActionResult<LeanApiResult<bool>>> BatchForceOffline([FromBody] List<long> userIds)
    {
        var success = true;
        foreach (var id in userIds)
        {
            var user = await _onlineUserRepository.GetAsync(id);
            if (user != null)
            {
                success &= await _onlineUserRepository.UpdateUserStatusAsync(user.ConnectionId, 0);
            }
        }
        return Ok(LeanApiResult<bool>.Ok(success));
    }

    /// <summary>
    /// 获取用户统计信息
    /// </summary>
    /// <returns>统计信息</returns>
    [HttpGet("statistics")]
    public async Task<ActionResult<object>> GetStatistics()
    {
        var users = await _onlineUserRepository.GetOnlineUsersAsync();
        var statistics = new
        {
            TotalOnline = users.Count,
            ByClientType = users.GroupBy(u => u.ClientType)
                .Select(g => new { Type = g.Key, Count = g.Count() }),
            ByNetworkType = users.GroupBy(u => u.NetworkType)
                .Select(g => new { Type = g.Key, Count = g.Count() })
        };
        return Ok(statistics);
    }
}
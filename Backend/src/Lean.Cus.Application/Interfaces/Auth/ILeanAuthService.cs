using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Auth;

namespace Lean.Cus.Application.Interfaces.Auth;

/// <summary>
/// 认证服务接口
/// </summary>
public interface ILeanAuthService
{
    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="input">登录信息</param>
    /// <returns>登录结果</returns>
    Task<LeanLoginResultDto> LoginAsync(LeanLoginDto input);

    /// <summary>
    /// 登出
    /// </summary>
    /// <returns>是否成功</returns>
    Task<bool> LogoutAsync();

    /// <summary>
    /// 刷新令牌
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <returns>登录结果</returns>
    Task<LeanLoginResultDto> RefreshTokenAsync(string refreshToken);
} 
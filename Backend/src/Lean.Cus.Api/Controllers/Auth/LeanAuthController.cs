using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Auth;
using Lean.Cus.Application.Interfaces.Auth;

namespace Lean.Cus.Api.Controllers.Auth;

/// <summary>
/// 认证控制器
/// </summary>
[Route("api/auth")]
[ApiController]
public class LeanAuthController : ControllerBase
{
    private readonly ILeanAuthService _authService;

    /// <summary>
    /// 初始化认证控制器
    /// </summary>
    public LeanAuthController(ILeanAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="input">登录信息</param>
    /// <returns>登录结果</returns>
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LeanLoginResultDto>> LoginAsync([FromBody] LeanLoginDto input)
    {
        var result = await _authService.LoginAsync(input);
        return Ok(result);
    }

    /// <summary>
    /// 登出
    /// </summary>
    /// <returns>是否成功</returns>
    [HttpPost("logout")]
    [Authorize]
    public async Task<ActionResult<bool>> LogoutAsync()
    {
        var result = await _authService.LogoutAsync();
        return Ok(result);
    }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    /// <param name="refreshToken">刷新令牌</param>
    /// <returns>登录结果</returns>
    [HttpPost("refresh-token")]
    [AllowAnonymous]
    public async Task<ActionResult<LeanLoginResultDto>> RefreshTokenAsync([FromBody] string refreshToken)
    {
        var result = await _authService.RefreshTokenAsync(refreshToken);
        return Ok(result);
    }
} 
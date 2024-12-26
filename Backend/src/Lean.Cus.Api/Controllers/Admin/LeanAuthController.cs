//===================================================
// 项目名称：Lean.Cus.Api.Controllers.Admin
// 文件名称：LeanAuthController
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：认证控制器
//===================================================

using Lean.Cus.Application.Dtos.Auth;
using Lean.Cus.Application.Services.Auth;
using Lean.Cus.Common.Results;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 认证控制器
/// </summary>
[ApiController]
[Route("api/admin/[controller]")]
public class LeanAuthController : ControllerBase
{
    private readonly ILeanAuthService _authService;

    public LeanAuthController(ILeanAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    [HttpPost("login")]
    public async Task<LeanApiResult<LeanLoginResultDto>> Login([FromBody] LeanLoginDto dto)
    {
        return await _authService.LoginAsync(dto);
    }

    /// <summary>
    /// 强制退出
    /// </summary>
    /// <param name="userId">用户ID</param>
    [HttpPost("force-logout/{userId}")]
    public async Task<LeanApiResult<bool>> ForceLogout(long userId)
    {
        return await _authService.ForceLogoutAsync(userId);
    }
}
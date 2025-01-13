using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Auth;
using Lean.Cus.Application.Interfaces.Auth;

namespace Lean.Cus.Api.Controllers.Auth;

/// <summary>
/// 验证码控制器
/// </summary>
[Route("api/captcha")]
[ApiController]
public class LeanCaptchaController : ControllerBase
{
    private readonly ILeanCaptchaService _captchaService;

    /// <summary>
    /// 初始化验证码控制器
    /// </summary>
    public LeanCaptchaController(ILeanCaptchaService captchaService)
    {
        _captchaService = captchaService;
    }

    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <returns>验证码信息</returns>
    [HttpGet("generate")]
    [AllowAnonymous]
    public async Task<ActionResult<LeanCaptchaGenerateDto>> GenerateAsync()
    {
        var result = await _captchaService.GenerateAsync();
        return Ok(result);
    }

    /// <summary>
    /// 校验验证码
    /// </summary>
    /// <param name="input">验证参数</param>
    /// <returns>校验结果</returns>
    [HttpPost("verify")]
    [AllowAnonymous]
    public async Task<ActionResult<LeanCaptchaVerifyResultDto>> VerifyAsync([FromBody] LeanCaptchaVerifyDto input)
    {
        var result = await _captchaService.VerifyAsync(input);
        return Ok(result);
    }
} 
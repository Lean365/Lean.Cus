using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Auth;

namespace Lean.Cus.Application.Interfaces.Auth;

/// <summary>
/// 验证码服务接口
/// </summary>
public interface ILeanCaptchaService
{
    /// <summary>
    /// 生成验证码
    /// </summary>
    /// <returns>验证码信息</returns>
    Task<LeanCaptchaGenerateDto> GenerateAsync();

    /// <summary>
    /// 校验验证码
    /// </summary>
    /// <param name="input">验证参数</param>
    /// <returns>校验结果</returns>
    Task<LeanCaptchaVerifyResultDto> VerifyAsync(LeanCaptchaVerifyDto input);
} 
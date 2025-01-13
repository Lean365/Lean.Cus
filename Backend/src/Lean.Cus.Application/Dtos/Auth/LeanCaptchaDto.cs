using System;

namespace Lean.Cus.Application.Dtos.Auth;

/// <summary>
/// 验证码生成响应DTO
/// </summary>
public class LeanCaptchaGenerateDto
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public string Uuid { get; set; } = string.Empty;

    /// <summary>
    /// 背景图片Base64
    /// </summary>
    public string BackgroundImage { get; set; } = string.Empty;

    /// <summary>
    /// 滑块图片Base64
    /// </summary>
    public string SliderImage { get; set; } = string.Empty;

    /// <summary>
    /// y轴坐标
    /// </summary>
    public int Y { get; set; }
}

/// <summary>
/// 验证码校验请求DTO
/// </summary>
public class LeanCaptchaVerifyDto
{
    /// <summary>
    /// 唯一标识
    /// </summary>
    public string Uuid { get; set; } = string.Empty;

    /// <summary>
    /// x轴坐标
    /// </summary>
    public int X { get; set; }

    /// <summary>
    /// y轴坐标
    /// </summary>
    public int Y { get; set; }
}

/// <summary>
/// 验证码校验响应DTO
/// </summary>
public class LeanCaptchaVerifyResultDto
{
    /// <summary>
    /// 是否验证通过
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; } = string.Empty;
} 
namespace Lean.Cus.Common.Configs;

/// <summary>
/// 验证码配置
/// </summary>
public class LeanCaptchaConfig
{
    /// <summary>
    /// 验证码缓存前缀
    /// </summary>
    public const string CachePrefix = "lean:captcha:";

    /// <summary>
    /// 验证码过期时间(秒)
    /// </summary>
    public int ExpireSeconds { get; set; } = 120;

    /// <summary>
    /// 容错像素
    /// </summary>
    public int TolerancePixels { get; set; } = 5;

    /// <summary>
    /// 背景图片宽度
    /// </summary>
    public int BackgroundWidth { get; set; } = 280;

    /// <summary>
    /// 背景图片高度
    /// </summary>
    public int BackgroundHeight { get; set; } = 155;

    /// <summary>
    /// 滑块宽度
    /// </summary>
    public int SliderWidth { get; set; } = 55;

    /// <summary>
    /// 滑块高度
    /// </summary>
    public int SliderHeight { get; set; } = 55;

    /// <summary>
    /// 背景图片路径
    /// </summary>
    public string BackgroundImagePath { get; set; } = "wwwroot/images/captcha/background";

    /// <summary>
    /// 模板图片路径
    /// </summary>
    public string TemplateImagePath { get; set; } = "wwwroot/images/captcha/template";
} 
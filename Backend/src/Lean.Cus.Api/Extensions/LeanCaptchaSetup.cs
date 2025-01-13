using System;
using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Lean.Cus.Common.Configs;
using Lean.Cus.Common.Utils;

namespace Lean.Cus.Api.Extensions;

/// <summary>
/// 验证码配置扩展类
/// </summary>
public static class LeanCaptchaSetup
{
    /// <summary>
    /// 初始化验证码资源
    /// </summary>
    public static IApplicationBuilder UseLeanCaptcha(this IApplicationBuilder app)
    {
        var config = app.ApplicationServices.GetRequiredService<IOptions<LeanCaptchaConfig>>().Value;

        // 确保目录存在
        Directory.CreateDirectory(config.BackgroundImagePath);
        Directory.CreateDirectory(config.TemplateImagePath);

        // 检查是否需要生成图片
        if (!HasImages(config.BackgroundImagePath) || !HasImages(config.TemplateImagePath))
        {
            // 生成背景图片
            LeanImageGenerator.GenerateBackgroundImage(
                config.BackgroundImagePath,
                config.BackgroundWidth,
                config.BackgroundHeight);

            // 生成滑块模板图片
            LeanImageGenerator.GenerateTemplateImage(
                config.TemplateImagePath,
                config.SliderWidth,
                config.SliderHeight);
        }

        return app;
    }

    private static bool HasImages(string path)
    {
        return Directory.Exists(path) && Directory.GetFiles(path, "*.png").Length > 0;
    }
} 
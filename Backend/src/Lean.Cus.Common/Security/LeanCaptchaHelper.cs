using System;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using Lean.Cus.Common.Configs;

namespace Lean.Cus.Common.Security;

/// <summary>
/// 验证码帮助类
/// </summary>
public static class LeanCaptchaHelper
{
    /// <summary>
    /// 生成滑块验证码
    /// </summary>
    public static (string backgroundImage, string sliderImage, int x, int y) Generate(LeanCaptchaConfig config)
    {
        // 随机选择背景图片
        var backgroundPath = GetRandomImage(config.BackgroundImagePath);
        var templatePath = GetRandomImage(config.TemplateImagePath);

        using var background = new Bitmap(backgroundPath);
        using var template = new Bitmap(templatePath);

        // 随机生成滑块位置
        var random = new Random();
        var x = random.Next(template.Width, background.Width - template.Width);
        var y = random.Next(0, background.Height - template.Height);

        // 在背景图上绘制滑块
        using var graphics = Graphics.FromImage(background);
        graphics.DrawImage(template, x, y);

        // 转换为Base64
        var backgroundBase64 = ImageToBase64(background);
        var templateBase64 = ImageToBase64(template);

        return (backgroundBase64, templateBase64, x, y);
    }

    /// <summary>
    /// 验证滑块位置
    /// </summary>
    public static bool Verify(int targetX, int targetY, int x, int y, int tolerance)
    {
        return Math.Abs(targetX - x) <= tolerance && Math.Abs(targetY - y) <= tolerance;
    }

    private static string GetRandomImage(string path)
    {
        var files = Directory.GetFiles(path, "*.png");
        if (files.Length == 0)
            throw new Exception($"目录 {path} 中没有图片文件");

        var random = new Random();
        return files[random.Next(files.Length)];
    }

    private static string ImageToBase64(Image image)
    {
        using var ms = new MemoryStream();
        image.Save(ms, ImageFormat.Png);
        var bytes = ms.ToArray();
        return Convert.ToBase64String(bytes);
    }
} 
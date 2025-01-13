using System;
using System.IO;
using SkiaSharp;

namespace Lean.Cus.Common.Utils;

/// <summary>
/// 图片生成工具类
/// </summary>
public static class LeanImageGenerator
{
    /// <summary>
    /// 生成背景图片
    /// </summary>
    public static void GenerateBackgroundImage(string savePath, int width, int height, int count = 5)
    {
        for (int i = 0; i < count; i++)
        {
            using var bitmap = new SKBitmap(width, height);
            using var canvas = new SKCanvas(bitmap);

            // 清空画布
            canvas.Clear(SKColors.White);

            // 填充渐变背景
            using var paint = new SKPaint
            {
                IsAntialias = true,
                Shader = SKShader.CreateLinearGradient(
                    new SKPoint(0, 0),
                    new SKPoint(width, height),
                    new[] { GetRandomColor(), GetRandomColor() },
                    null,
                    SKShaderTileMode.Clamp)
            };
            canvas.DrawRect(0, 0, width, height, paint);

            // 添加一些随机图形
            AddRandomShapes(canvas, width, height);

            // 保存图片
            var fileName = Path.Combine(savePath, $"bg_{i + 1}.png");
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(fileName);
            data.SaveTo(stream);
        }
    }

    /// <summary>
    /// 生成滑块模板图片
    /// </summary>
    public static void GenerateTemplateImage(string savePath, int width, int height, int count = 3)
    {
        for (int i = 0; i < count; i++)
        {
            using var bitmap = new SKBitmap(width, height, SKColorType.Rgba8888, SKAlphaType.Premul);
            using var canvas = new SKCanvas(bitmap);

            // 清空画布为透明
            canvas.Clear(SKColors.Transparent);

            // 创建拼图形状路径
            using var path = CreatePuzzlePath(width, height);

            // 填充拼图形状
            using var fillPaint = new SKPaint
            {
                IsAntialias = true,
                Color = new SKColor(255, 255, 255, 128),
                Style = SKPaintStyle.Fill
            };
            canvas.DrawPath(path, fillPaint);

            // 绘制边框
            using var strokePaint = new SKPaint
            {
                IsAntialias = true,
                Color = SKColors.White,
                Style = SKPaintStyle.Stroke,
                StrokeWidth = 2
            };
            canvas.DrawPath(path, strokePaint);

            // 保存图片
            var fileName = Path.Combine(savePath, $"template_{i + 1}.png");
            using var image = SKImage.FromBitmap(bitmap);
            using var data = image.Encode(SKEncodedImageFormat.Png, 100);
            using var stream = File.OpenWrite(fileName);
            data.SaveTo(stream);
        }
    }

    private static SKColor GetRandomColor()
    {
        var random = new Random();
        return new SKColor(
            (byte)random.Next(100, 256),
            (byte)random.Next(100, 256),
            (byte)random.Next(100, 256));
    }

    private static void AddRandomShapes(SKCanvas canvas, int width, int height)
    {
        var random = new Random();
        var count = random.Next(3, 7);

        for (int i = 0; i < count; i++)
        {
            var x = random.Next(width);
            var y = random.Next(height);
            var size = random.Next(20, 50);
            var color = GetRandomColor();

            using var paint = new SKPaint
            {
                IsAntialias = true,
                Color = color,
                Style = SKPaintStyle.Fill
            };

            switch (random.Next(3))
            {
                case 0: // 圆形
                    canvas.DrawCircle(x + size / 2, y + size / 2, size / 2, paint);
                    break;
                case 1: // 矩形
                    canvas.DrawRect(x, y, size, size, paint);
                    break;
                case 2: // 三角形
                    DrawTriangle(canvas, x, y, size, paint);
                    break;
            }
        }
    }

    private static void DrawTriangle(SKCanvas canvas, int x, int y, int size, SKPaint paint)
    {
        using var path = new SKPath();
        path.MoveTo(x, y + size);
        path.LineTo(x + size / 2, y);
        path.LineTo(x + size, y + size);
        path.Close();
        canvas.DrawPath(path, paint);
    }

    private static SKPath CreatePuzzlePath(int width, int height)
    {
        var path = new SKPath();

        // 创建基本矩形
        path.AddRect(new SKRect(0, 0, width, height));

        // 添加凸起部分
        var bulgeSize = width / 3;
        var bulgeHeight = height / 3;
        var centerY = height / 2;

        var points = new[]
        {
            new SKPoint(width - bulgeSize, centerY - bulgeHeight),
            new SKPoint(width, centerY),
            new SKPoint(width - bulgeSize, centerY + bulgeHeight)
        };

        path.AddPoly(points, true);

        return path;
    }
} 
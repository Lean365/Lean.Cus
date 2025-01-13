using System;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Auth;
using Lean.Cus.Application.Interfaces.Auth;
using Lean.Cus.Common.Configs;
using Lean.Cus.Common.Security;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Lean.Cus.Application.Services.Auth;

/// <summary>
/// 验证码服务实现
/// </summary>
public class LeanCaptchaService : ILeanCaptchaService
{
    private readonly IDistributedCache _cache;
    private readonly LeanCaptchaConfig _config;

    /// <summary>
    /// 初始化验证码服务
    /// </summary>
    public LeanCaptchaService(
        IDistributedCache cache,
        IOptions<LeanCaptchaConfig> config)
    {
        _cache = cache;
        _config = config.Value;
    }

    /// <summary>
    /// 生成验证码
    /// </summary>
    public async Task<LeanCaptchaGenerateDto> GenerateAsync()
    {
        // 生成验证码
        var (backgroundImage, sliderImage, x, y) = LeanCaptchaHelper.Generate(_config);

        // 生成唯一标识
        var uuid = Guid.NewGuid().ToString("N");

        // 缓存验证码信息
        var cacheKey = $"{LeanCaptchaConfig.CachePrefix}{uuid}";
        var cacheValue = JsonSerializer.Serialize(new { X = x, Y = y });
        var cacheOptions = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(_config.ExpireSeconds)
        };

        await _cache.SetStringAsync(cacheKey, cacheValue, cacheOptions);

        // 返回结果
        return new LeanCaptchaGenerateDto
        {
            Uuid = uuid,
            BackgroundImage = backgroundImage,
            SliderImage = sliderImage,
            Y = y
        };
    }

    /// <summary>
    /// 校验验证码
    /// </summary>
    public async Task<LeanCaptchaVerifyResultDto> VerifyAsync(LeanCaptchaVerifyDto input)
    {
        // 获取缓存的验证码信息
        var cacheKey = $"{LeanCaptchaConfig.CachePrefix}{input.Uuid}";
        var cacheValue = await _cache.GetStringAsync(cacheKey);

        if (string.IsNullOrEmpty(cacheValue))
        {
            return new LeanCaptchaVerifyResultDto
            {
                Success = false,
                Message = "验证码已过期"
            };
        }

        try
        {
            // 删除缓存
            await _cache.RemoveAsync(cacheKey);

            // 解析缓存值
            var captchaInfo = JsonSerializer.Deserialize<dynamic>(cacheValue);
            var targetX = (int)captchaInfo.GetProperty("X").GetInt32();
            var targetY = (int)captchaInfo.GetProperty("Y").GetInt32();

            // 验证位置
            var success = LeanCaptchaHelper.Verify(targetX, targetY, input.X, input.Y, _config.TolerancePixels);

            return new LeanCaptchaVerifyResultDto
            {
                Success = success,
                Message = success ? "验证通过" : "验证失败"
            };
        }
        catch (Exception ex)
        {
            return new LeanCaptchaVerifyResultDto
            {
                Success = false,
                Message = $"验证失败: {ex.Message}"
            };
        }
    }
} 
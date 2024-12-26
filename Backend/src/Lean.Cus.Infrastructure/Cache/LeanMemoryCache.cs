//===================================================
// 项目名称：Lean.Cus.Infrastructure.Cache
// 文件名称：LeanMemoryCache
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：内存缓存实现
//===================================================

using Microsoft.Extensions.Caching.Memory;
using Lean.Cus.Domain.Interfaces;
using Lean.Cus.Domain.Configurations;

namespace Lean.Cus.Infrastructure.Cache;

/// <summary>
/// 内存缓存实现
/// </summary>
public class LeanMemoryCache : ILeanCache
{
    private readonly IMemoryCache _cache;
    private readonly LeanMemoryOptions _options;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="cache">内存缓存</param>
    /// <param name="options">配置选项</param>
    public LeanMemoryCache(IMemoryCache cache, LeanMemoryOptions options)
    {
        _cache = cache;
        _options = options;
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns>缓存值</returns>
    public T? Get<T>(string key)
    {
        return _cache.Get<T>(key);
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns>缓存值</returns>
    public Task<T?> GetAsync<T>(string key)
    {
        return Task.FromResult(Get<T>(key));
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存值</param>
    /// <param name="expireMinutes">过期时间（分钟）</param>
    public void Set<T>(string key, T value, int expireMinutes = 30)
    {
        var options = new MemoryCacheEntryOptions
        {
            Size = 1,
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(expireMinutes)
        };

        if (_options.EnableSlidingExpiration)
        {
            options.SlidingExpiration = TimeSpan.FromMinutes(expireMinutes);
        }

        if (_options.CompactOnMemoryPressure)
        {
            options.Priority = CacheItemPriority.Normal;
        }

        _cache.Set(key, value, options);
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存值</param>
    /// <param name="expireMinutes">过期时间（分钟）</param>
    public Task SetAsync<T>(string key, T value, int expireMinutes = 30)
    {
        Set(key, value, expireMinutes);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 移除缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    public void Remove(string key)
    {
        _cache.Remove(key);
    }

    /// <summary>
    /// 移除缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    public Task RemoveAsync(string key)
    {
        Remove(key);
        return Task.CompletedTask;
    }

    /// <summary>
    /// 清空缓存
    /// </summary>
    public void Clear()
    {
        if (_cache is MemoryCache memoryCache)
        {
            memoryCache.Compact(1.0);
        }
    }

    /// <summary>
    /// 清空缓存
    /// </summary>
    public Task ClearAsync()
    {
        Clear();
        return Task.CompletedTask;
    }
} 
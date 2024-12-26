//===================================================
// 项目名称：Lean.Cus.Infrastructure.Cache
// 文件名称：LeanCacheFactory
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：缓存工厂
//===================================================

using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Lean.Cus.Domain.Configurations;
using Lean.Cus.Domain.Interfaces;

namespace Lean.Cus.Infrastructure.Cache;

/// <summary>
/// 缓存工厂
/// </summary>
public static class LeanCacheFactory
{
    /// <summary>
    /// 添加缓存服务
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <param name="config">缓存配置</param>
    public static void AddLeanCache(this IServiceCollection services, LeanCacheConfig config)
    {
        if (config.Provider.Equals("Redis", StringComparison.OrdinalIgnoreCase))
        {
            if (config.Redis == null)
            {
                throw new ArgumentException("Redis配置不能为空");
            }

            services.AddSingleton<ILeanCache>(provider => new LeanRedisCache(
                config.Redis.ConnectionString,
                config.Redis.InstanceName,
                config.Redis.DefaultDb,
                new LeanRedisOptions
                {
                    ConnectTimeout = config.Redis.ConnectTimeout,
                    OperationTimeout = config.Redis.OperationTimeout,
                    Password = config.Redis.Password,
                    UseSsl = config.Redis.UseSsl,
                    AllowAdmin = config.Redis.AllowAdmin,
                    EnableCompression = config.Redis.EnableCompression,
                    RetryCount = config.Redis.RetryCount,
                    RetryInterval = config.Redis.RetryInterval,
                    PoolSize = config.Redis.PoolSize,
                    KeepAlive = config.Redis.KeepAlive,
                    AbortOnConnectFail = config.Redis.AbortOnConnectFail
                }
            ));
        }
        else
        {
            services.AddMemoryCache();
            services.AddSingleton<ILeanCache>(provider => new LeanMemoryCache(
                provider.GetRequiredService<IMemoryCache>(),
                new LeanMemoryOptions
                {
                    DefaultExpirationMinutes = config.DefaultExpirationMinutes
                }
            ));
        }
    }
} 
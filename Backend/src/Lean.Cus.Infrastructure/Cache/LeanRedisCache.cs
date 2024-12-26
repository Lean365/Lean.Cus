//===================================================
// 项目名称：Lean.Cus.Infrastructure.Cache
// 文件名称：LeanRedisCache
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：Redis缓存实现
//===================================================

using StackExchange.Redis;
using System.Text.Json;
using Lean.Cus.Domain.Interfaces;
using Lean.Cus.Domain.Configurations;

namespace Lean.Cus.Infrastructure.Cache;

/// <summary>
/// Redis缓存实现
/// </summary>
public class LeanRedisCache : ILeanCache
{
    private readonly IConnectionMultiplexer _redis;
    private readonly IDatabase _db;
    private readonly string _instanceName;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="connectionString">连接字符串</param>
    /// <param name="instanceName">实例名称</param>
    /// <param name="defaultDb">默认数据库</param>
    /// <param name="options">Redis选项</param>
    public LeanRedisCache(string connectionString, string instanceName, int defaultDb, LeanRedisOptions options)
    {
        var config = ConfigurationOptions.Parse(connectionString);
        config.DefaultDatabase = defaultDb;
        config.ConnectTimeout = options.ConnectTimeout;
        config.SyncTimeout = options.OperationTimeout;
        config.Password = options.Password;
        config.Ssl = options.UseSsl;
        config.AllowAdmin = options.AllowAdmin;
        config.ConnectRetry = options.RetryCount;
        config.ConnectTimeout = options.RetryInterval;
        config.KeepAlive = options.KeepAlive;
        config.AbortOnConnectFail = options.AbortOnConnectFail;

        _redis = ConnectionMultiplexer.Connect(config);
        _db = _redis.GetDatabase();
        _instanceName = instanceName;
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns>缓存值</returns>
    public T? Get<T>(string key)
    {
        var value = _db.StringGet(GetKey(key));
        if (!value.HasValue)
            return default;

        return JsonSerializer.Deserialize<T>(value!);
    }

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns>缓存值</returns>
    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _db.StringGetAsync(GetKey(key));
        if (!value.HasValue)
            return default;

        return JsonSerializer.Deserialize<T>(value!);
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
        var expiry = TimeSpan.FromMinutes(expireMinutes);
        _db.StringSet(GetKey(key), JsonSerializer.Serialize(value), expiry);
    }

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存值</param>
    /// <param name="expireMinutes">过期时间（分钟）</param>
    public async Task SetAsync<T>(string key, T value, int expireMinutes = 30)
    {
        var expiry = TimeSpan.FromMinutes(expireMinutes);
        await _db.StringSetAsync(GetKey(key), JsonSerializer.Serialize(value), expiry);
    }

    /// <summary>
    /// 移除缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    public void Remove(string key)
    {
        _db.KeyDelete(GetKey(key));
    }

    /// <summary>
    /// 移除缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    public async Task RemoveAsync(string key)
    {
        await _db.KeyDeleteAsync(GetKey(key));
    }

    /// <summary>
    /// 清空缓存
    /// </summary>
    public void Clear()
    {
        var endpoints = _redis.GetEndPoints();
        foreach (var endpoint in endpoints)
        {
            var server = _redis.GetServer(endpoint);
            var keys = server.Keys(pattern: $"{_instanceName}*");
            foreach (var key in keys)
            {
                _db.KeyDelete(key);
            }
        }
    }

    /// <summary>
    /// 清空缓存
    /// </summary>
    public async Task ClearAsync()
    {
        var endpoints = _redis.GetEndPoints();
        foreach (var endpoint in endpoints)
        {
            var server = _redis.GetServer(endpoint);
            var keys = server.Keys(pattern: $"{_instanceName}*");
            foreach (var key in keys)
            {
                await _db.KeyDeleteAsync(key);
            }
        }
    }

    /// <summary>
    /// 获取完整键名
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>完整键名</returns>
    private string GetKey(string key) => $"{_instanceName}{key}";
} 
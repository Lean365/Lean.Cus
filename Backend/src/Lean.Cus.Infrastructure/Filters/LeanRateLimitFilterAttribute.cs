using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Lean.Cus.Infrastructure.Filters;

/// <summary>
/// 接口限流过滤器
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LeanRateLimitFilterAttribute : LeanActionFilterAttribute
{
    private static readonly ConcurrentDictionary<string, TokenBucket> TokenBuckets = new();
    private readonly int _capacity;
    private readonly int _refillRate;
    private readonly TimeSpan _refillTime;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="capacity">令牌桶容量</param>
    /// <param name="refillRate">令牌补充速率</param>
    /// <param name="refillTimeInSeconds">令牌补充时间间隔(秒)</param>
    /// <param name="logger">日志记录器</param>
    public LeanRateLimitFilterAttribute(
        int capacity = 100,
        int refillRate = 10,
        int refillTimeInSeconds = 1,
        ILogger<LeanRateLimitFilterAttribute>? logger = null)
        : base(logger ?? throw new ArgumentNullException(nameof(logger)))
    {
        _capacity = capacity;
        _refillRate = refillRate;
        _refillTime = TimeSpan.FromSeconds(refillTimeInSeconds);
    }

    /// <summary>
    /// 执行前检查限流
    /// </summary>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        var key = GetClientKey(context);
        var bucket = TokenBuckets.GetOrAdd(key, _ => new TokenBucket(_capacity, _refillRate, _refillTime));

        if (!bucket.TryTake())
        {
            Logger.LogWarning($"请求频率超限: {key}");
            context.Result = new StatusCodeResult(429); // Too Many Requests
            return;
        }
    }

    private static string GetClientKey(ActionExecutingContext context)
    {
        var ip = context.HttpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var path = context.HttpContext.Request.Path.Value ?? "/";
        var user = context.HttpContext.User.Identity?.Name ?? "anonymous";
        return $"{ip}:{path}:{user}";
    }

    private class TokenBucket
    {
        private readonly int _capacity;
        private readonly int _refillRate;
        private readonly TimeSpan _refillTime;
        private int _tokens;
        private DateTime _lastRefill;
        private readonly object _lock = new();

        public TokenBucket(int capacity, int refillRate, TimeSpan refillTime)
        {
            _capacity = capacity;
            _refillRate = refillRate;
            _refillTime = refillTime;
            _tokens = capacity;
            _lastRefill = DateTime.UtcNow;
        }

        public bool TryTake()
        {
            lock (_lock)
            {
                RefillTokens();

                if (_tokens <= 0)
                {
                    return false;
                }

                _tokens--;
                return true;
            }
        }

        private void RefillTokens()
        {
            var now = DateTime.UtcNow;
            var timePassed = now - _lastRefill;

            if (timePassed < _refillTime)
            {
                return;
            }

            var periods = (int)(timePassed.TotalSeconds / _refillTime.TotalSeconds);
            var tokensToAdd = periods * _refillRate;

            _tokens = Math.Min(_capacity, _tokens + tokensToAdd);
            _lastRefill = now;
        }
    }
} 
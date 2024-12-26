//===================================================
// 项目名称：Lean.Cus.Api.Attributes
// 文件名称：LeanRateLimitAttribute
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：接口限流特性
//===================================================

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Lean.Cus.Domain.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace Lean.Cus.Api.Attributes;

/// <summary>
/// 接口限流特性
/// </summary>
[AttributeUsage(AttributeTargets.Method)]
public class LeanRateLimitAttribute : ActionFilterAttribute
{
    private readonly string _endpoint;
    private static readonly MemoryCache Cache = new(new MemoryCacheOptions());
    private LeanRateLimitConfig? _config;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="endpoint">端点名称</param>
    public LeanRateLimitAttribute(string endpoint = "*")
    {
        _endpoint = endpoint;
    }

    /// <summary>
    /// 执行前
    /// </summary>
    /// <param name="context">上下文</param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (_config == null)
        {
            var options = context.HttpContext.RequestServices.GetRequiredService<IOptions<LeanRateLimitConfig>>();
            _config = options.Value;
        }

        if (!_config.EnableEndpointRateLimiting)
        {
            base.OnActionExecuting(context);
            return;
        }

        var clientIp = GetClientIp(context.HttpContext);
        var key = GenerateClientKey(context, clientIp);

        // 获取适用的规则
        var rules = GetApplicableRules(clientIp);
        if (!rules.Any())
        {
            base.OnActionExecuting(context);
            return;
        }

        // 检查每个规则
        foreach (var rule in rules)
        {
            var ruleKey = $"{key}_{rule.Period}";
            var timeWindow = ParsePeriod(rule.Period);

            // 获取计数器
            var counter = Cache.GetOrCreate(ruleKey, entry =>
            {
                entry.SetAbsoluteExpiration(timeWindow);
                return new RateLimitCounter
                {
                    Timestamp = DateTime.UtcNow,
                    Count = 0
                };
            });

            // 增加计数
            var currentCount = Interlocked.Increment(ref counter.Count);

            // 检查是否超过限制
            if (currentCount > rule.Limit)
            {
                var retryAfter = (int)timeWindow.TotalSeconds;
                context.Result = new ObjectResult(new
                {
                    message = $"请求过于频繁，请{retryAfter}秒后重试",
                    retryAfter
                })
                {
                    StatusCode = _config.HttpStatusCode
                };

                // 添加重试时间头
                context.HttpContext.Response.Headers.Add("Retry-After", retryAfter.ToString());
                return;
            }
        }

        base.OnActionExecuting(context);
    }

    /// <summary>
    /// 获取客户端IP
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>IP地址</returns>
    private string GetClientIp(HttpContext context)
    {
        var realIp = context.Request.Headers[_config.RealIpHeader].ToString();
        if (!string.IsNullOrEmpty(realIp))
        {
            return realIp;
        }

        return context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
    }

    /// <summary>
    /// 生成客户端键
    /// </summary>
    /// <param name="context">上下文</param>
    /// <param name="clientIp">客户端IP</param>
    /// <returns>键</returns>
    private string GenerateClientKey(ActionExecutingContext context, string clientIp)
    {
        var request = context.HttpContext.Request;
        var path = request.Path.Value?.ToLower() ?? "";
        var clientId = request.Headers[_config.ClientIdHeader].ToString();
        var userId = context.HttpContext.User.Identity?.Name ?? "anonymous";

        return $"ratelimit_{clientIp}_{clientId}_{path}_{_endpoint}_{userId}";
    }

    /// <summary>
    /// 获取适用的规则
    /// </summary>
    /// <param name="clientIp">客户端IP</param>
    /// <returns>规则列表</returns>
    private List<LeanRateLimitRule> GetApplicableRules(string clientIp)
    {
        var rules = new List<LeanRateLimitRule>();

        // 添加IP特定规则
        var ipRule = _config.IpRules.FirstOrDefault(r => r.Ip == clientIp);
        if (ipRule != null)
        {
            rules.AddRange(ipRule.Rules.Where(r => r.Endpoint == "*" || r.Endpoint == _endpoint));
        }

        // 添加通用规则
        rules.AddRange(_config.GeneralRules.Where(r => r.Endpoint == "*" || r.Endpoint == _endpoint));

        return rules;
    }

    /// <summary>
    /// 解析时间段
    /// </summary>
    /// <param name="period">时间段字符串</param>
    /// <returns>时间跨度</returns>
    private TimeSpan ParsePeriod(string period)
    {
        var value = int.Parse(period[..^1]);
        return period[^1] switch
        {
            's' => TimeSpan.FromSeconds(value),
            'm' => TimeSpan.FromMinutes(value),
            'h' => TimeSpan.FromHours(value),
            'd' => TimeSpan.FromDays(value),
            _ => TimeSpan.FromSeconds(value)
        };
    }
}

/// <summary>
/// 限流计数器
/// </summary>
public class RateLimitCounter
{
    /// <summary>
    /// 时间戳
    /// </summary>
    public DateTime Timestamp { get; set; }

    /// <summary>
    /// 计数
    /// </summary>
    public int Count;
} 
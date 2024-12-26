//===================================================
// 项目名称：Lean.Cus.Api.Middlewares
// 文件名称：LeanAntiForgeryMiddleware
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：CSRF防护中间件
//===================================================

using Microsoft.Extensions.Logging;

namespace Lean.Cus.Api.Middlewares;

/// <summary>
/// CSRF防护中间件
/// </summary>
public class LeanAntiForgeryMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LeanAntiForgeryMiddleware> _logger;
    private readonly string _tokenHeaderName;
    private readonly HashSet<string> _allowedOrigins;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">请求委托</param>
    /// <param name="logger">日志记录器</param>
    public LeanAntiForgeryMiddleware(RequestDelegate next, ILogger<LeanAntiForgeryMiddleware> logger)
    {
        _next = next;
        _logger = logger;
        _tokenHeaderName = "X-CSRF-TOKEN";
        _allowedOrigins = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "http://localhost:5173",
            "https://localhost:5173"
            // 添加其他允许的源
        };
    }

    /// <summary>
    /// 处理请求
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // 检查请求方法
        if (!IsModifyingMethod(context.Request.Method))
        {
            await _next(context);
            return;
        }

        // 检查请求路径
        var path = context.Request.Path.Value?.ToLower();
        if (string.IsNullOrEmpty(path) || path.StartsWith("/swagger"))
        {
            await _next(context);
            return;
        }

        // 验证Origin
        var origin = context.Request.Headers.Origin.ToString();
        if (string.IsNullOrEmpty(origin) || !_allowedOrigins.Contains(origin))
        {
            _logger.LogWarning($"检测到CSRF攻击 - 无效的Origin: {origin}");
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new { message = "无效的请求来源" });
            return;
        }

        // 验证Referer
        var referer = context.Request.Headers.Referer.ToString();
        if (!string.IsNullOrEmpty(referer) && !IsValidReferer(referer))
        {
            _logger.LogWarning($"检测到CSRF攻击 - 无效的Referer: {referer}");
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new { message = "无效的请求来源" });
            return;
        }

        // 验证CSRF令牌
        var token = context.Request.Headers[_tokenHeaderName].ToString();
        if (string.IsNullOrEmpty(token) || !IsValidToken(token))
        {
            _logger.LogWarning("检测到CSRF攻击 - 无效的CSRF令牌");
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsJsonAsync(new { message = "无效的CSRF令牌" });
            return;
        }

        await _next(context);
    }

    /// <summary>
    /// 检查是否为修改数据的请求方法
    /// </summary>
    /// <param name="method">请求方法</param>
    /// <returns>是否为修改数据的请求方法</returns>
    private bool IsModifyingMethod(string method)
    {
        return method is "POST" or "PUT" or "DELETE" or "PATCH";
    }

    /// <summary>
    /// 检查Referer是否有效
    /// </summary>
    /// <param name="referer">Referer</param>
    /// <returns>是否有效</returns>
    private bool IsValidReferer(string referer)
    {
        try
        {
            var uri = new Uri(referer);
            var host = $"{uri.Scheme}://{uri.Authority}";
            return _allowedOrigins.Contains(host);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 验证CSRF令牌
    /// </summary>
    /// <param name="token">令牌</param>
    /// <returns>是否有效</returns>
    private bool IsValidToken(string token)
    {
        // TODO: 实现令牌验证逻辑
        // 1. 从缓存或数据库中获取用户的CSRF令牌
        // 2. 比较令牌是否匹配
        // 3. 检查令牌是否过期
        return !string.IsNullOrEmpty(token);
    }
} 
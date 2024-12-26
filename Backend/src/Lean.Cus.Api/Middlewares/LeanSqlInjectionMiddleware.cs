//===================================================
// 项目名称：Lean.Cus.Api.Middlewares
// 文件名称：LeanSqlInjectionMiddleware
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：SQL注入防护中间件
//===================================================

using System.Text.RegularExpressions;
using Microsoft.Extensions.Logging;

namespace Lean.Cus.Api.Middlewares;

/// <summary>
/// SQL注入防护中间件
/// </summary>
public class LeanSqlInjectionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LeanSqlInjectionMiddleware> _logger;
    private readonly string[] _blacklist = new[]
    {
        "exec[\\s]", "execute[\\s]", "delete[\\s]", "update[\\s]", "insert[\\s]",
        "select[\\s]", "drop[\\s]", "truncate[\\s]", "xp_cmdshell", "sp_configure",
        "restore[\\s]", "backup[\\s]", "net[\\s]+user", "net[\\s]+localgroup",
        "--", ";", "/*", "*/", "@@", "@", "char[\\s]", "nchar[\\s]",
        "varchar[\\s]", "nvarchar[\\s]", "alter[\\s]", "begin[\\s]", "cast[\\s]",
        "create[\\s]", "cursor[\\s]", "declare[\\s]", "fetch[\\s]", "kill[\\s]",
        "sysobjects", "syscolumns", "table[\\s]", "view[\\s]"
    };

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">请求委托</param>
    /// <param name="logger">日志记录器</param>
    public LeanSqlInjectionMiddleware(RequestDelegate next, ILogger<LeanSqlInjectionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// 处理请求
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>任务</returns>
    public async Task InvokeAsync(HttpContext context)
    {
        // 检查请求方法
        if (context.Request.Method != "GET" && context.Request.Method != "POST")
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

        // 检查查询字符串
        var query = context.Request.QueryString.Value;
        if (!string.IsNullOrEmpty(query) && ContainsSqlInjection(query))
        {
            _logger.LogWarning($"检测到SQL注入攻击 - 查询字符串: {query}");
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            await context.Response.WriteAsJsonAsync(new { message = "检测到潜在的SQL注入攻击" });
            return;
        }

        // 检查请求体
        if (context.Request.Method == "POST" && context.Request.ContentType?.StartsWith("application/json") == true)
        {
            context.Request.EnableBuffering();
            using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
            var body = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;

            if (!string.IsNullOrEmpty(body) && ContainsSqlInjection(body))
            {
                _logger.LogWarning($"检测到SQL注入攻击 - 请求体: {body}");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
                await context.Response.WriteAsJsonAsync(new { message = "检测到潜在的SQL注入攻击" });
                return;
            }
        }

        await _next(context);
    }

    /// <summary>
    /// 检查是否包含SQL注入攻击
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>是否包含SQL注入</returns>
    private bool ContainsSqlInjection(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return false;
        }

        input = input.ToLower();

        // 检查SQL注入黑名单
        foreach (var pattern in _blacklist)
        {
            if (Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase))
            {
                return true;
            }
        }

        // 检查特殊字符组合
        if (Regex.IsMatch(input, @"(\s|=|%|/|;|,|\.|\*|\band\b|\bor\b|\bunion\b|\bselect\b|\bfrom\b|\bwhere\b|\bgroup\b|\bhaving\b|\border\b|\bby\b|\bcount\b|\bdrop\b|\bdelete\b|\bupdate\b|\binsert\b)"))
        {
            return true;
        }

        return false;
    }
} 
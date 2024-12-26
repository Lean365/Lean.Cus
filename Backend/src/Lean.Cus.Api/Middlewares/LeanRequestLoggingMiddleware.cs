//===================================================
// 项目名称：Lean.Cus.Api.Middlewares
// 文件名称：LeanRequestLoggingMiddleware
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：请求日志中间件
//===================================================

using System.Text;
using Microsoft.Extensions.Logging;

namespace Lean.Cus.Api.Middlewares;

/// <summary>
/// 请求日志中间件
/// </summary>
public class LeanRequestLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LeanRequestLoggingMiddleware> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="next">请求委托</param>
    /// <param name="logger">日志记录器</param>
    public LeanRequestLoggingMiddleware(RequestDelegate next, ILogger<LeanRequestLoggingMiddleware> logger)
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
        try
        {
            // 记录请求信息
            await LogRequest(context);

            // 启用响应体读取
            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            // 继续处理请求
            await _next(context);

            // 记录响应信息
            await LogResponse(context, responseBody);

            // 复制响应体到原始流
            await responseBody.CopyToAsync(originalBodyStream);
        }
        catch (Exception ex)
        {
            // 记录异常信息
            _logger.LogError(ex, "请求处理异常");
            throw;
        }
    }

    /// <summary>
    /// 记录请求信息
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <returns>任务</returns>
    private async Task LogRequest(HttpContext context)
    {
        var request = context.Request;
        var requestBody = string.Empty;

        // 如果请求体可读，则读取请求体
        if (request.Body.CanRead)
        {
            request.EnableBuffering();
            using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 1024, true);
            requestBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;
        }

        // 构建请求日志
        var logMessage = new StringBuilder()
            .AppendLine("=== 请求信息 ===")
            .AppendLine($"请求时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}")
            .AppendLine($"请求方法: {request.Method}")
            .AppendLine($"请求路径: {request.Path}")
            .AppendLine($"请求查询: {request.QueryString}")
            .AppendLine($"客户端IP: {context.Connection.RemoteIpAddress}")
            .AppendLine($"请求头: {string.Join(", ", request.Headers.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");

        // 如果有请求体，则添加到日志
        if (!string.IsNullOrEmpty(requestBody))
        {
            logMessage.AppendLine($"请求体: {requestBody}");
        }

        _logger.LogInformation(logMessage.ToString());
    }

    /// <summary>
    /// 记录响应信息
    /// </summary>
    /// <param name="context">HTTP上下文</param>
    /// <param name="responseBody">响应体流</param>
    /// <returns>任务</returns>
    private async Task LogResponse(HttpContext context, MemoryStream responseBody)
    {
        var response = context.Response;
        responseBody.Position = 0;
        var responseContent = await new StreamReader(responseBody).ReadToEndAsync();
        responseBody.Position = 0;

        // 构建响应日志
        var logMessage = new StringBuilder()
            .AppendLine("=== 响应信息 ===")
            .AppendLine($"响应时间: {DateTime.Now:yyyy-MM-dd HH:mm:ss.fff}")
            .AppendLine($"状态码: {response.StatusCode}")
            .AppendLine($"响应头: {string.Join(", ", response.Headers.Select(h => $"{h.Key}={string.Join(",", h.Value)}"))}");

        // 如果有响应体，则添加到日志
        if (!string.IsNullOrEmpty(responseContent))
        {
            logMessage.AppendLine($"响应体: {responseContent}");
        }

        _logger.LogInformation(logMessage.ToString());
    }
} 
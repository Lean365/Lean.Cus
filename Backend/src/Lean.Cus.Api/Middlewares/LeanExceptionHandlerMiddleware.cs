using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Lean.Cus.Common.Exceptions;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Lean.Cus.Api.Middlewares;

/// <summary>
/// 全局异常处理中间件
/// </summary>
public class LeanExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LeanExceptionHandlerMiddleware> _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanExceptionHandlerMiddleware(RequestDelegate next, ILogger<LeanExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// 处理请求
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var result = LeanApiResult.Fail(LeanErrorType.SystemError, "服务器内部错误");

        switch (exception)
        {
            case LeanValidationException validationException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = LeanApiResult.Fail(LeanErrorType.InvalidParameter, validationException.Message);
                _logger.LogWarning(validationException, "验证异常: {Message}", validationException.Message);
                break;

            case LeanBusinessException businessException:
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                result = LeanApiResult.Fail(LeanErrorType.BusinessError, businessException.Message);
                _logger.LogWarning(businessException, "业务异常: {Message}", businessException.Message);
                break;

            case LeanException leanException:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = LeanApiResult.Fail(LeanErrorType.SystemError, leanException.Message);
                _logger.LogError(leanException, "Lean异常: {Message}", leanException.Message);
                break;

            default:
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result = LeanApiResult.Fail(LeanErrorType.UnknownError, "服务器内部错误");
                _logger.LogError(exception, "未处理异常: {Message}", exception.Message);
                break;
        }

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(result, options));
    }
} 
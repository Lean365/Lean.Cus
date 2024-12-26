using System.Net;
using System.Text.Json;
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Exceptions;
using Lean.Cus.Common.Results;
using Microsoft.AspNetCore.Http;
using SqlSugar;

namespace Lean.Cus.Api.Middlewares;

/// <summary>
/// 全局异常处理中间件
/// </summary>
public class LeanGlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<LeanGlobalExceptionMiddleware> _logger;

    public LeanGlobalExceptionMiddleware(RequestDelegate next, ILogger<LeanGlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

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
        var response = new LeanApiResult<object>();

        switch (exception)
        {
            case LeanException customException:
                response = LeanApiResult<object>.Fail(customException.Message, (int)customException.ErrorCode);
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                break;

            case UnauthorizedAccessException:
                response = LeanApiResult<object>.Unauthorized();
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                break;

            case SqlSugarException:
                response = LeanApiResult<object>.Fail(exception.Message, (int)LeanErrorCodeEnum.DbError);
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                _logger.LogError(exception, "数据库异常");
                break;

            default:
                response = LeanApiResult<object>.Fail(exception.Message, (int)LeanErrorCodeEnum.ServerError);
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                _logger.LogError(exception, "系统异常");
                break;
        }

        var result = JsonSerializer.Serialize(response);
        await context.Response.WriteAsync(result);
    }
} 
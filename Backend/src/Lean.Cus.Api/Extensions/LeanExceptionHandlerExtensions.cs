using Lean.Cus.Api.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Lean.Cus.Api.Extensions;

/// <summary>
/// 异常处理中间件扩展
/// </summary>
public static class LeanExceptionHandlerExtensions
{
    /// <summary>
    /// 使用全局异常处理中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    public static IApplicationBuilder UseLeanExceptionHandler(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LeanExceptionHandlerMiddleware>();
    }
} 
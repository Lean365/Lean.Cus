//===================================================
// 项目名称：Lean.Cus.Api.Extensions
// 文件名称：LeanMiddlewareExtensions
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：中间件扩展方法
//===================================================

using Lean.Cus.Api.Middlewares;

namespace Lean.Cus.Api.Extensions;

/// <summary>
/// 中间件扩展方法
/// </summary>
public static class LeanMiddlewareExtensions
{
    /// <summary>
    /// 使用请求日志中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    public static IApplicationBuilder UseLeanRequestLogging(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LeanRequestLoggingMiddleware>();
    }

    /// <summary>
    /// 使用SQL注入防护中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    public static IApplicationBuilder UseLeanSqlInjectionPrevention(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LeanSqlInjectionMiddleware>();
    }

    /// <summary>
    /// 使用CSRF防护中间件
    /// </summary>
    /// <param name="app">应用程序构建器</param>
    /// <returns>应用程序构建器</returns>
    public static IApplicationBuilder UseLeanAntiForgery(this IApplicationBuilder app)
    {
        return app.UseMiddleware<LeanAntiForgeryMiddleware>();
    }
} 
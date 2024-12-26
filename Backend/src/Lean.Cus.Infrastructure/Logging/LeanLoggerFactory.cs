//===================================================
// 项目名称：Lean.Cus.Infrastructure.Logging
// 文件名称：LeanLoggerFactory
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：日志工厂
//===================================================

using NLog;
using NLog.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Lean.Cus.Domain.Configurations;
using Lean.Cus.Domain.Interfaces;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace Lean.Cus.Infrastructure.Logging;

/// <summary>
/// 日志工厂
/// </summary>
public static class LeanLoggerFactory
{
    /// <summary>
    /// 添加NLog日志
    /// </summary>
    /// <param name="services">服务集合</param>
    /// <returns>服务集合</returns>
    public static IServiceCollection AddNLog(this IServiceCollection services)
    {
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
            builder.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
            builder.AddNLog();
        });

        return services;
    }
} 
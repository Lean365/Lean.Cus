using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace Lean.Cus.Common.Logging;

/// <summary>
/// NLog配置类
/// </summary>
public static class LeanNLogSetup
{
    /// <summary>
    /// 配置NLog
    /// </summary>
    /// <param name="builder">Web应用程序构建器</param>
    public static void AddLeanNLog(this IHostBuilder builder)
    {
        builder.ConfigureLogging(logging =>
        {
            logging.ClearProviders();
        }).UseNLog(new NLogAspNetCoreOptions
        {
            RemoveLoggerFactoryFilter = true,
            RegisterHttpContextAccessor = true
        });
    }
} 
//===================================================
// 项目名称：Lean.Cus.Infrastructure.Logging
// 文件名称：LeanLogger
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：日志实现类
//===================================================

using Lean.Cus.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Lean.Cus.Infrastructure.Logging;

/// <summary>
/// 日志实现类
/// </summary>
public class LeanLogger : ILeanLogger
{
    private readonly ILogger _logger;

    public LeanLogger(ILogger<LeanLogger> logger)
    {
        _logger = logger;
    }

    public void Trace(string message, params object[] args)
    {
        _logger.LogTrace(message, args);
    }

    public void Debug(string message, params object[] args)
    {
        _logger.LogDebug(message, args);
    }

    public void Info(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void Warn(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    public void Error(string message, params object[] args)
    {
        _logger.LogError(message, args);
    }

    public void Error(Exception ex, string message, params object[] args)
    {
        _logger.LogError(ex, message, args);
    }

    public void Fatal(string message, params object[] args)
    {
        _logger.LogCritical(message, args);
    }

    public void Fatal(Exception ex, string message, params object[] args)
    {
        _logger.LogCritical(ex, message, args);
    }
} 
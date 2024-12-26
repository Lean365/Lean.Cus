//===================================================
// 项目名称：Lean.Cus.Infrastructure.Logging
// 文件名称：LeanNLogger
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：NLog日志实现
//===================================================

using NLog;
using Lean.Cus.Domain.Interfaces;

namespace Lean.Cus.Infrastructure.Logging;

/// <summary>
/// NLog日志实现
/// </summary>
public class LeanNLogger : ILeanLogger
{
    private readonly ILogger _logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">日志名称</param>
    public LeanNLogger(string? name = null)
    {
        _logger = LogManager.GetLogger(name ?? GetType().FullName);
    }

    /// <summary>
    /// 记录跟踪日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public void Trace(string message, params object[] args)
    {
        _logger.Trace(message, args);
    }

    /// <summary>
    /// 记录调试日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public void Debug(string message, params object[] args)
    {
        _logger.Debug(message, args);
    }

    /// <summary>
    /// 记录信息日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public void Info(string message, params object[] args)
    {
        _logger.Info(message, args);
    }

    /// <summary>
    /// 记录警告日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public void Warn(string message, params object[] args)
    {
        _logger.Warn(message, args);
    }

    /// <summary>
    /// 记录错误日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public void Error(string message, params object[] args)
    {
        _logger.Error(message, args);
    }

    /// <summary>
    /// 记录错误日志
    /// </summary>
    /// <param name="ex">异常对象</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public void Error(Exception ex, string message, params object[] args)
    {
        _logger.Error(ex, message, args);
    }

    /// <summary>
    /// 记录致命错误日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public void Fatal(string message, params object[] args)
    {
        _logger.Fatal(message, args);
    }

    /// <summary>
    /// 记录致命错误日志
    /// </summary>
    /// <param name="ex">异常对象</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    public void Fatal(Exception ex, string message, params object[] args)
    {
        _logger.Fatal(ex, message, args);
    }
} 
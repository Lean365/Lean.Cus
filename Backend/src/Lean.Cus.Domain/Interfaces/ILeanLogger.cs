//===================================================
// 项目名称：Lean.Cus.Domain.Interfaces
// 文件名称：ILeanLogger
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：日志接口
//===================================================

namespace Lean.Cus.Domain.Interfaces;

/// <summary>
/// 日志接口
/// </summary>
public interface ILeanLogger
{
    /// <summary>
    /// 记录跟踪日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    void Trace(string message, params object[] args);

    /// <summary>
    /// 记录调试日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    void Debug(string message, params object[] args);

    /// <summary>
    /// 记录信息日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    void Info(string message, params object[] args);

    /// <summary>
    /// 记录警告日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    void Warn(string message, params object[] args);

    /// <summary>
    /// 记录错误日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    void Error(string message, params object[] args);

    /// <summary>
    /// 记录错误日志
    /// </summary>
    /// <param name="ex">异常信息</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    void Error(Exception ex, string message, params object[] args);

    /// <summary>
    /// 记录致命错误日志
    /// </summary>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    void Fatal(string message, params object[] args);

    /// <summary>
    /// 记录致命错误日志
    /// </summary>
    /// <param name="ex">异常信息</param>
    /// <param name="message">日志消息</param>
    /// <param name="args">格式化参数</param>
    void Fatal(Exception ex, string message, params object[] args);
} 
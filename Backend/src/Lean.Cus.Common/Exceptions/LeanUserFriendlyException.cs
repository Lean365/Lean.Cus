using System;

namespace Lean.Cus.Common.Exceptions;

/// <summary>
/// 用户友好异常
/// </summary>
public class LeanUserFriendlyException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanUserFriendlyException() : base() { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public LeanUserFriendlyException(string message) : base(message) { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public LeanUserFriendlyException(string message, Exception innerException) : base(message, innerException) { }
} 
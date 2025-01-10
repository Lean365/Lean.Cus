using System;

namespace Lean.Cus.Common.Exceptions;

/// <summary>
/// Lean基础异常类
/// </summary>
public class LeanException : Exception
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanException() : base() { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public LeanException(string message) : base(message) { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public LeanException(string message, Exception innerException) : base(message, innerException) { }
} 
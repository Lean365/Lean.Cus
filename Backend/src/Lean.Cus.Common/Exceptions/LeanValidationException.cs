using System;

namespace Lean.Cus.Common.Exceptions;

/// <summary>
/// 验证异常类
/// </summary>
public class LeanValidationException : LeanException
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanValidationException() : base() { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public LeanValidationException(string message) : base(message) { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public LeanValidationException(string message, Exception innerException) : base(message, innerException) { }
} 
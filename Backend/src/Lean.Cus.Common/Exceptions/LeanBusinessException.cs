using System;

namespace Lean.Cus.Common.Exceptions;

/// <summary>
/// 业务异常类
/// </summary>
public class LeanBusinessException : LeanException
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanBusinessException() : base() { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    public LeanBusinessException(string message) : base(message) { }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">异常消息</param>
    /// <param name="innerException">内部异常</param>
    public LeanBusinessException(string message, Exception innerException) : base(message, innerException) { }
} 
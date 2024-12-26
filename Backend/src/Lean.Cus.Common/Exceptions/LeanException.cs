using Lean.Cus.Common.Enums;

namespace Lean.Cus.Common.Exceptions;

/// <summary>
/// 自定义异常
/// </summary>
public class LeanException : Exception
{
    /// <summary>
    /// 错误代码
    /// </summary>
    public LeanErrorCodeEnum ErrorCode { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="errorCode">错误代码</param>
    public LeanException(string message, LeanErrorCodeEnum errorCode = LeanErrorCodeEnum.BusinessError)
        : base(message)
    {
        ErrorCode = errorCode;
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="innerException">内部异常</param>
    /// <param name="errorCode">错误代码</param>
    public LeanException(string message, Exception innerException, LeanErrorCodeEnum errorCode = LeanErrorCodeEnum.BusinessError)
        : base(message, innerException)
    {
        ErrorCode = errorCode;
    }
} 
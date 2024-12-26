//===================================================
// 项目名称：Lean.Cus.Domain.Exceptions
// 文件名称：LeanException
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：自定义异常基类
//===================================================

namespace Lean.Cus.Domain.Exceptions;

/// <summary>
/// 自定义异常基类
/// <para>作为系统中所有自定义异常的基类</para>
/// <para>提供统一的异常处理机制</para>
/// </summary>
public class LeanException : Exception
{
    /// <summary>
    /// 错误代码
    /// <para>用于标识具体的错误类型</para>
    /// </summary>
    public int Code { get; }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public LeanException() : base() { }

    /// <summary>
    /// 使用指定错误消息初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    public LeanException(string message) : base(message) { }

    /// <summary>
    /// 使用指定错误消息和错误代码初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="code">错误代码</param>
    public LeanException(string message, int code) : base(message)
    {
        Code = code;
    }

    /// <summary>
    /// 使用指定错误消息和内部异常初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="innerException">内部异常</param>
    public LeanException(string message, Exception innerException) : base(message, innerException) { }

    /// <summary>
    /// 使用指定错误消息、错误代码和内部异常初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="code">错误代码</param>
    /// <param name="innerException">内部异常</param>
    public LeanException(string message, int code, Exception innerException) : base(message, innerException)
    {
        Code = code;
    }
} 
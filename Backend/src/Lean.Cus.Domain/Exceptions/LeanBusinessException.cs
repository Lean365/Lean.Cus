//===================================================
// 项目名称：Lean.Cus.Domain.Exceptions
// 文件名称：LeanBusinessException
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：业务异常类
//===================================================

using Lean.Cus.Common.Constants;

namespace Lean.Cus.Domain.Exceptions;

/// <summary>
/// 业务异常类
/// <para>用于处理业务逻辑相关的异常情况</para>
/// <para>继承自LeanException基类</para>
/// </summary>
public class LeanBusinessException : LeanException
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public LeanBusinessException() : base("业务处理失败") { }

    /// <summary>
    /// 使用指定错误消息初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    public LeanBusinessException(string message) : base(message) { }

    /// <summary>
    /// 使用指定错误消息和错误代码初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="code">错误代码</param>
    public LeanBusinessException(string message, int code) : base(message, code) { }

    /// <summary>
    /// 使用指定错误消息和内部异常初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="innerException">内部异常</param>
    public LeanBusinessException(string message, Exception innerException) : base(message, innerException) { }

    /// <summary>
    /// 使用指定错误消息、错误代码和内部异常初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="code">错误代码</param>
    /// <param name="innerException">内部异常</param>
    public LeanBusinessException(string message, int code, Exception innerException) : base(message, code, innerException) { }

    /// <summary>
    /// 创建参数错误异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <returns>参数错误异常</returns>
    public static LeanBusinessException InvalidParameter(string message = "参数错误")
    {
        return new LeanBusinessException(message, LeanHttpStatus.BadRequest);
    }

    /// <summary>
    /// 创建未授权异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <returns>未授权异常</returns>
    public static LeanBusinessException Unauthorized(string message = "未授权")
    {
        return new LeanBusinessException(message, LeanHttpStatus.Unauthorized);
    }

    /// <summary>
    /// 创建禁止访问异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <returns>禁止访问异常</returns>
    public static LeanBusinessException Forbidden(string message = "禁止访问")
    {
        return new LeanBusinessException(message, LeanHttpStatus.Forbidden);
    }

    /// <summary>
    /// 创建资源未找到异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <returns>资源未找到异常</returns>
    public static LeanBusinessException NotFound(string message = "未找到")
    {
        return new LeanBusinessException(message, LeanHttpStatus.NotFound);
    }

    /// <summary>
    /// 创建数据已存在异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <returns>数据已存在异常</returns>
    public static LeanBusinessException DataExists(string message = "数据已存在")
    {
        return new LeanBusinessException(message, LeanHttpStatus.Conflict);
    }

    /// <summary>
    /// 创建数据验证失败异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <returns>数据验证失败异常</returns>
    public static LeanBusinessException ValidationFailed(string message = "数据验证失败")
    {
        return new LeanBusinessException(message, LeanHttpStatus.BadRequest);
    }
} 
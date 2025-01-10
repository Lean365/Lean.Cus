using System.ComponentModel;

namespace Lean.Cus.Common.Enums;

/// <summary>
/// 错误类型枚举
/// </summary>
public enum LeanErrorType
{
    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    Success = 0,

    /// <summary>
    /// 参数错误
    /// </summary>
    [Description("参数错误")]
    InvalidParameter = 400,

    /// <summary>
    /// 未授权
    /// </summary>
    [Description("未授权")]
    Unauthorized = 401,

    /// <summary>
    /// 禁止访问
    /// </summary>
    [Description("禁止访问")]
    Forbidden = 403,

    /// <summary>
    /// 未找到
    /// </summary>
    [Description("未找到")]
    NotFound = 404,

    /// <summary>
    /// 方法不允许
    /// </summary>
    [Description("方法不允许")]
    MethodNotAllowed = 405,

    /// <summary>
    /// 业务错误
    /// </summary>
    [Description("业务错误")]
    BusinessError = 600,

    /// <summary>
    /// 系统错误
    /// </summary>
    [Description("系统错误")]
    SystemError = 500,

    /// <summary>
    /// 网络错误
    /// </summary>
    [Description("网络错误")]
    NetworkError = 503,

    /// <summary>
    /// 数据库错误
    /// </summary>
    [Description("数据库错误")]
    DatabaseError = 504,

    /// <summary>
    /// 未知错误
    /// </summary>
    [Description("未知错误")]
    UnknownError = 999
} 
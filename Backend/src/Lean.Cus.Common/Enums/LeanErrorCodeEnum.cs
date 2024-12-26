namespace Lean.Cus.Common.Enums;

/// <summary>
/// 错误代码枚举
/// </summary>
public enum LeanErrorCodeEnum
{
    /// <summary>
    /// 成功
    /// </summary>
    Success = 200,

    /// <summary>
    /// 参数错误
    /// </summary>
    BadRequest = 400,

    /// <summary>
    /// 未授权
    /// </summary>
    Unauthorized = 401,

    /// <summary>
    /// 禁止访问
    /// </summary>
    Forbidden = 403,

    /// <summary>
    /// 未找到
    /// </summary>
    NotFound = 404,

    /// <summary>
    /// 服务器错误
    /// </summary>
    ServerError = 500,

    /// <summary>
    /// 数据库错误
    /// </summary>
    DbError = 501,

    /// <summary>
    /// 验证错误
    /// </summary>
    ValidationError = 502,

    /// <summary>
    /// 业务错误
    /// </summary>
    BusinessError = 503,

    /// <summary>
    /// 并发错误
    /// </summary>
    ConcurrencyError = 504,

    /// <summary>
    /// 外部服务错误
    /// </summary>
    ExternalServiceError = 505
} 
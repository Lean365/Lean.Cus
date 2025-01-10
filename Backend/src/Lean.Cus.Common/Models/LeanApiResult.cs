using System.Text.Json.Serialization;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Common.Models;

/// <summary>
/// API返回结果
/// </summary>
public class LeanApiResult
{
    /// <summary>
    /// 是否成功
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// 错误代码
    /// </summary>
    [JsonPropertyName("code")]
    public LeanErrorType Code { get; set; }

    /// <summary>
    /// 错误消息
    /// </summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }

    /// <summary>
    /// 成功
    /// </summary>
    public static LeanApiResult Ok()
    {
        return new LeanApiResult
        {
            Success = true,
            Code = LeanErrorType.Success
        };
    }

    /// <summary>
    /// 成功
    /// </summary>
    public static LeanApiResult Ok(string message)
    {
        return new LeanApiResult
        {
            Success = true,
            Code = LeanErrorType.Success,
            Message = message
        };
    }

    /// <summary>
    /// 失败
    /// </summary>
    public static LeanApiResult Fail()
    {
        return new LeanApiResult
        {
            Success = false,
            Code = LeanErrorType.UnknownError,
            Message = "操作失败"
        };
    }

    /// <summary>
    /// 失败
    /// </summary>
    public static LeanApiResult Fail(string message)
    {
        return new LeanApiResult
        {
            Success = false,
            Code = LeanErrorType.UnknownError,
            Message = message
        };
    }

    /// <summary>
    /// 失败
    /// </summary>
    public static LeanApiResult Fail(LeanErrorType errorType, string message)
    {
        return new LeanApiResult
        {
            Success = false,
            Code = errorType,
            Message = message
        };
    }
}

/// <summary>
/// API返回结果(泛型)
/// </summary>
public class LeanApiResult<T> : LeanApiResult
{
    /// <summary>
    /// 返回数据
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    /// <summary>
    /// 成功
    /// </summary>
    public static LeanApiResult<T> Ok(T data)
    {
        return new LeanApiResult<T>
        {
            Success = true,
            Code = LeanErrorType.Success,
            Data = data
        };
    }

    /// <summary>
    /// 成功
    /// </summary>
    public static LeanApiResult<T> Ok(T data, string message)
    {
        return new LeanApiResult<T>
        {
            Success = true,
            Code = LeanErrorType.Success,
            Message = message,
            Data = data
        };
    }

    /// <summary>
    /// 失败
    /// </summary>
    public new static LeanApiResult<T> Fail()
    {
        return new LeanApiResult<T>
        {
            Success = false,
            Code = LeanErrorType.UnknownError,
            Message = "操作失败"
        };
    }

    /// <summary>
    /// 失败
    /// </summary>
    public new static LeanApiResult<T> Fail(string message)
    {
        return new LeanApiResult<T>
        {
            Success = false,
            Code = LeanErrorType.UnknownError,
            Message = message
        };
    }

    /// <summary>
    /// 失败
    /// </summary>
    public new static LeanApiResult<T> Fail(LeanErrorType errorType, string message)
    {
        return new LeanApiResult<T>
        {
            Success = false,
            Code = errorType,
            Message = message
        };
    }
} 
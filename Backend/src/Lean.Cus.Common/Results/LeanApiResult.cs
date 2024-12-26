using System.Text.Json.Serialization;

namespace Lean.Cus.Common.Results;

/// <summary>
/// API统一返回结果
/// </summary>
public class LeanApiResult<T>
{
    /// <summary>
    /// 状态码
    /// </summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    [JsonPropertyName("msg")]
    public string Msg { get; set; } = string.Empty;

    /// <summary>
    /// 数据
    /// </summary>
    [JsonPropertyName("data")]
    public T? Data { get; set; }

    /// <summary>
    /// 成功
    /// </summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>
    /// 成功
    /// </summary>
    /// <param name="data">数据</param>
    /// <param name="msg">消息</param>
    /// <returns>API结果</returns>
    public static LeanApiResult<T> Ok(T data, string msg = "操作成功")
    {
        return new LeanApiResult<T>
        {
            Code = 200,
            Msg = msg,
            Data = data,
            Success = true
        };
    }

    /// <summary>
    /// 失败
    /// </summary>
    /// <param name="msg">消息</param>
    /// <param name="code">状态码</param>
    /// <returns>API结果</returns>
    public static LeanApiResult<T> Fail(string msg = "操作失败", int code = 500)
    {
        return new LeanApiResult<T>
        {
            Code = code,
            Msg = msg,
            Success = false
        };
    }

    /// <summary>
    /// 未找到
    /// </summary>
    /// <param name="msg">消息</param>
    /// <returns>API结果</returns>
    public static LeanApiResult<T> NotFound(string msg = "未找到数据")
    {
        return new LeanApiResult<T>
        {
            Code = 404,
            Msg = msg,
            Success = false
        };
    }

    /// <summary>
    /// 未授权
    /// </summary>
    /// <param name="msg">消息</param>
    /// <returns>API结果</returns>
    public static LeanApiResult<T> Unauthorized(string msg = "未授权")
    {
        return new LeanApiResult<T>
        {
            Code = 401,
            Msg = msg,
            Success = false
        };
    }

    /// <summary>
    /// 禁止访问
    /// </summary>
    /// <param name="msg">消息</param>
    /// <returns>API结果</returns>
    public static LeanApiResult<T> Forbidden(string msg = "禁止访问")
    {
        return new LeanApiResult<T>
        {
            Code = 403,
            Msg = msg,
            Success = false
        };
    }

    /// <summary>
    /// 参数错误
    /// </summary>
    /// <param name="msg">消息</param>
    /// <returns>API结果</returns>
    public static LeanApiResult<T> BadRequest(string msg = "参数错误")
    {
        return new LeanApiResult<T>
        {
            Code = 400,
            Msg = msg,
            Success = false
        };
    }
} 
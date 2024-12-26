//===================================================
// 项目名称：Lean.Cus.Common.Utils
// 文件名称：JsonUtil
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：JSON工具类
//===================================================

using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;

namespace Lean.Cus.Common.Utils;

/// <summary>
/// JSON工具类
/// </summary>
public static class JsonUtil
{
    /// <summary>
    /// 默认序列化选项
    /// </summary>
    private static readonly JsonSerializerOptions _defaultOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        PropertyNameCaseInsensitive = true,
        DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
        Encoder = JavaScriptEncoder.Create(UnicodeRanges.All),
        WriteIndented = true
    };

    /// <summary>
    /// 序列化对象为JSON字符串
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="obj">对象</param>
    /// <param name="options">序列化选项</param>
    /// <returns>JSON字符串</returns>
    public static string Serialize<T>(T obj, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Serialize(obj, options ?? _defaultOptions);
    }

    /// <summary>
    /// 反序列化JSON字符串为对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="json">JSON字符串</param>
    /// <param name="options">序列化选项</param>
    /// <returns>对象</returns>
    public static T? Deserialize<T>(string json, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize<T>(json, options ?? _defaultOptions);
    }

    /// <summary>
    /// 反序列化JSON字符串为对象
    /// </summary>
    /// <param name="json">JSON字符串</param>
    /// <param name="type">对象类型</param>
    /// <param name="options">序列化选项</param>
    /// <returns>对象</returns>
    public static object? Deserialize(string json, Type type, JsonSerializerOptions? options = null)
    {
        return JsonSerializer.Deserialize(json, type, options ?? _defaultOptions);
    }

    /// <summary>
    /// 将对象转换为另一个类型
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <typeparam name="TTarget">目标类型</typeparam>
    /// <param name="source">源对象</param>
    /// <param name="options">序列化选项</param>
    /// <returns>目标对象</returns>
    public static TTarget? Convert<TSource, TTarget>(TSource source, JsonSerializerOptions? options = null)
    {
        var json = Serialize(source, options);
        return Deserialize<TTarget>(json, options);
    }

    /// <summary>
    /// 将对象转换为另一个类型
    /// </summary>
    /// <typeparam name="TSource">源类型</typeparam>
    /// <param name="source">源对象</param>
    /// <param name="targetType">目标类型</param>
    /// <param name="options">序列化选项</param>
    /// <returns>目标对象</returns>
    public static object? Convert<TSource>(TSource source, Type targetType, JsonSerializerOptions? options = null)
    {
        var json = Serialize(source, options);
        return Deserialize(json, targetType, options);
    }

    /// <summary>
    /// 深度克隆对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="source">源对象</param>
    /// <param name="options">序列化选项</param>
    /// <returns>克隆对象</returns>
    public static T? Clone<T>(T source, JsonSerializerOptions? options = null)
    {
        return Convert<T, T>(source, options);
    }

    /// <summary>
    /// 合并JSON对象
    /// </summary>
    /// <param name="source">源JSON字符串</param>
    /// <param name="target">目标JSON字符串</param>
    /// <param name="options">序列化选项</param>
    /// <returns>合并后的JSON字符串</returns>
    public static string Merge(string source, string target, JsonSerializerOptions? options = null)
    {
        var sourceDoc = JsonDocument.Parse(source);
        var targetDoc = JsonDocument.Parse(target);

        var result = new Dictionary<string, JsonElement>();

        foreach (var property in sourceDoc.RootElement.EnumerateObject())
        {
            result[property.Name] = property.Value;
        }

        foreach (var property in targetDoc.RootElement.EnumerateObject())
        {
            result[property.Name] = property.Value;
        }

        return Serialize(result, options);
    }

    /// <summary>
    /// 格式化JSON字符串
    /// </summary>
    /// <param name="json">JSON字符串</param>
    /// <param name="options">序列化选项</param>
    /// <returns>格式化后的JSON字符串</returns>
    public static string Format(string json, JsonSerializerOptions? options = null)
    {
        var obj = JsonDocument.Parse(json);
        return Serialize(obj, options);
    }

    /// <summary>
    /// 压缩JSON字符串
    /// </summary>
    /// <param name="json">JSON字符串</param>
    /// <returns>压缩后的JSON字符串</returns>
    public static string Compress(string json)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = false
        };

        var obj = JsonDocument.Parse(json);
        return Serialize(obj, options);
    }

    /// <summary>
    /// 判断字符串是否为有效的JSON格式
    /// </summary>
    /// <param name="json">JSON字符串</param>
    /// <returns>是否为有效的JSON格式</returns>
    public static bool IsValid(string json)
    {
        try
        {
            JsonDocument.Parse(json);
            return true;
        }
        catch
        {
            return false;
        }
    }
} 
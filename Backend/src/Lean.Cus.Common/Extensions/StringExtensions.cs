//===================================================
// 项目名称：Lean.Cus.Common.Extensions
// 文件名称：StringExtensions
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：字符串扩展方法
//===================================================

using System.Text;
using System.Text.RegularExpressions;

namespace Lean.Cus.Common.Extensions;

/// <summary>
/// 字符串扩展方法
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// 判断字符串是否为空
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>是否为空</returns>
    public static bool IsNullOrEmpty(this string str)
    {
        return string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// 判断字符串是否为空或空白
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>是否为空或空白</returns>
    public static bool IsNullOrWhiteSpace(this string str)
    {
        return string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// 判断字符串是���不为空
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>是否不为空</returns>
    public static bool NotNullOrEmpty(this string str)
    {
        return !string.IsNullOrEmpty(str);
    }

    /// <summary>
    /// 判断字符串是否不为空或空白
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>是否不为空或空白</returns>
    public static bool NotNullOrWhiteSpace(this string str)
    {
        return !string.IsNullOrWhiteSpace(str);
    }

    /// <summary>
    /// 转换为驼峰命名
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>驼峰命名</returns>
    public static string ToCamelCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        if (str.Length == 1)
            return str.ToLower();

        return char.ToLower(str[0]) + str.Substring(1);
    }

    /// <summary>
    /// 转换为帕斯卡命名
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>帕斯卡命名</returns>
    public static string ToPascalCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        if (str.Length == 1)
            return str.ToUpper();

        return char.ToUpper(str[0]) + str.Substring(1);
    }

    /// <summary>
    /// 转换为下划线命名
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>下划线命名</returns>
    public static string ToSnakeCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        var builder = new StringBuilder();
        for (var i = 0; i < str.Length; i++)
        {
            if (i > 0 && char.IsUpper(str[i]))
                builder.Append('_');
            builder.Append(char.ToLower(str[i]));
        }

        return builder.ToString();
    }

    /// <summary>
    /// 转换为短横线命名
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>短横线命名</returns>
    public static string ToKebabCase(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        var builder = new StringBuilder();
        for (var i = 0; i < str.Length; i++)
        {
            if (i > 0 && char.IsUpper(str[i]))
                builder.Append('-');
            builder.Append(char.ToLower(str[i]));
        }

        return builder.ToString();
    }

    /// <summary>
    /// 移除HTML标签
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>移除HTML标签后的字符串</returns>
    public static string RemoveHtml(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        return Regex.Replace(str, "<[^>]+>", "");
    }

    /// <summary>
    /// 截取字符串
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="length">长度</param>
    /// <param name="suffix">后缀</param>
    /// <returns>截取后的字符串</returns>
    public static string Cut(this string str, int length, string suffix = "...")
    {
        if (string.IsNullOrEmpty(str))
            return str;

        if (str.Length <= length)
            return str;

        return str.Substring(0, length) + suffix;
    }

    /// <summary>
    /// 格式化字符串
    /// </summary>
    /// <param name="str">字符串</param>
    /// <param name="args">参数</param>
    /// <returns>格式化后的字符串</returns>
    public static string Format(this string str, params object[] args)
    {
        return string.Format(str, args);
    }

    /// <summary>
    /// 转换为Base64字符串
    /// </summary>
    /// <param name="str">字符串</param>
    /// <returns>Base64字符串</returns>
    public static string ToBase64(this string str)
    {
        if (string.IsNullOrEmpty(str))
            return str;

        var bytes = Encoding.UTF8.GetBytes(str);
        return Convert.ToBase64String(bytes);
    }

    /// <summary>
    /// 从Base64字符串转换
    /// </summary>
    /// <param name="base64">Base64字符串</param>
    /// <returns>字符串</returns>
    public static string FromBase64(this string base64)
    {
        if (string.IsNullOrEmpty(base64))
            return base64;

        var bytes = Convert.FromBase64String(base64);
        return Encoding.UTF8.GetString(bytes);
    }
} 
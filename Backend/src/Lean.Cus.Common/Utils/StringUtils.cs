using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Lean.Cus.Common.Utils;

/// <summary>
/// 字符串工具类
/// </summary>
public static class StringUtils
{
    /// <summary>
    /// 转换为帕斯卡命名
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>帕斯卡命名字符串</returns>
    public static string ToPascalCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        input = input.ToLower();
        var words = input.Split(new[] { '_', '-', ' ' }, StringSplitOptions.RemoveEmptyEntries);
        return string.Join("", words.Select(word => char.ToUpper(word[0]) + word.Substring(1)));
    }

    /// <summary>
    /// 转换为驼峰命名
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>驼峰命名字符串</returns>
    public static string ToCamelCase(string input)
    {
        var pascal = ToPascalCase(input);
        if (string.IsNullOrEmpty(pascal))
        {
            return pascal;
        }

        return char.ToLower(pascal[0]) + pascal.Substring(1);
    }

    /// <summary>
    /// 转换为下划线命名
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>下划线命名字符串</returns>
    public static string ToSnakeCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
        return string.Join("_", pattern.Matches(input)).ToLower();
    }

    /// <summary>
    /// 转换为短横线命名
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>短横线命名字符串</returns>
    public static string ToKebabCase(string input)
    {
        return ToSnakeCase(input).Replace('_', '-');
    }

    /// <summary>
    /// 首字母大写
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>首字母大写字符串</returns>
    public static string ToTitleCase(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        return char.ToUpper(input[0]) + input.Substring(1);
    }

    /// <summary>
    /// 首字母小写
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>首字母小写字符串</returns>
    public static string ToLowerFirst(string input)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        return char.ToLower(input[0]) + input.Substring(1);
    }
} 
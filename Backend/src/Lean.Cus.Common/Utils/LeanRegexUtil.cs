using System.Text.RegularExpressions;

namespace Lean.Cus.Common.Utils;

/// <summary>
/// 正则表达式工具类
/// </summary>
public static class LeanRegexUtil
{
    #region 正则表达式常量

    /// <summary>
    /// 中国大陆手机号正则（11位数字，以1开头）
    /// </summary>
    private const string ChinaMobilePattern = @"^1[3-9]\d{9}$";

    /// <summary>
    /// 国际手机号正则（允许+号开头，6-15位数字）
    /// </summary>
    private const string GlobalMobilePattern = @"^\+?[1-9]\d{5,14}$";

    /// <summary>
    /// 中国大陆固定电话正则（区号+号码，可带分机号）
    /// </summary>
    private const string ChinaPhonePattern = @"^(0\d{2,3}-?)?[1-9]\d{6,7}(-\d{1,4})?$";

    /// <summary>
    /// 国际固定电话正则（允许+号开头，6-20位数字，可带分机号）
    /// </summary>
    private const string GlobalPhonePattern = @"^\+?[1-9]\d{5,19}(-\d{1,4})?$";

    /// <summary>
    /// 邮箱正则
    /// </summary>
    private const string EmailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

    #endregion

    #region 验证方法

    /// <summary>
    /// 验证中国大陆手机号
    /// </summary>
    public static bool IsValidChinaMobile(string mobile)
    {
        return !string.IsNullOrEmpty(mobile) && Regex.IsMatch(mobile, ChinaMobilePattern);
    }

    /// <summary>
    /// 验证国际手机号
    /// </summary>
    public static bool IsValidGlobalMobile(string mobile)
    {
        return !string.IsNullOrEmpty(mobile) && Regex.IsMatch(mobile, GlobalMobilePattern);
    }

    /// <summary>
    /// 验证中国大陆固定电话
    /// </summary>
    public static bool IsValidChinaPhone(string phone)
    {
        return !string.IsNullOrEmpty(phone) && Regex.IsMatch(phone, ChinaPhonePattern);
    }

    /// <summary>
    /// 验证国际固定电话
    /// </summary>
    public static bool IsValidGlobalPhone(string phone)
    {
        return !string.IsNullOrEmpty(phone) && Regex.IsMatch(phone, GlobalPhonePattern);
    }

    /// <summary>
    /// 验证邮箱
    /// </summary>
    public static bool IsValidEmail(string email)
    {
        return !string.IsNullOrEmpty(email) && Regex.IsMatch(email, EmailPattern);
    }

    /// <summary>
    /// 验证手机号（根据国家自动选择验证规则）
    /// </summary>
    public static bool IsValidMobileByCountry(string mobile, string country)
    {
        if (string.IsNullOrEmpty(mobile)) return false;

        // 如果是中国，使用中国大陆手机号规则
        if (string.Equals(country, "中国", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(country, "China", StringComparison.OrdinalIgnoreCase))
        {
            return IsValidChinaMobile(mobile);
        }

        // 其他国家使用国际手机号规则
        return IsValidGlobalMobile(mobile);
    }

    /// <summary>
    /// 验证固定电话（根据国家自动选择验证规则）
    /// </summary>
    public static bool IsValidPhoneByCountry(string phone, string country)
    {
        if (string.IsNullOrEmpty(phone)) return false;

        // 如果是中国，使用中国大陆固定电话规则
        if (string.Equals(country, "中国", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(country, "China", StringComparison.OrdinalIgnoreCase))
        {
            return IsValidChinaPhone(phone);
        }

        // 其他国家使用国际固定电话规则
        return IsValidGlobalPhone(phone);
    }

    /// <summary>
    /// 标准化手机号格式（根据国家自动处理）
    /// </summary>
    public static string NormalizeMobileByCountry(string mobile, string country)
    {
        if (string.IsNullOrEmpty(mobile)) return string.Empty;

        // 去除所有空格和特殊字符
        mobile = Regex.Replace(mobile, @"[\s\-\(\)]", "");

        // 如果已经有国际区号前缀，直接返回
        if (mobile.StartsWith("+")) return mobile;

        // 根据国家选择处理方式
        if (string.Equals(country, "中国", StringComparison.OrdinalIgnoreCase) ||
            string.Equals(country, "China", StringComparison.OrdinalIgnoreCase))
        {
            return $"+86{mobile}";
        }

        // 其他国家的区号映射
        var countryCodeMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "美国", "1" },
            { "USA", "1" },
            { "加拿大", "1" },
            { "Canada", "1" },
            { "英国", "44" },
            { "UK", "44" },
            { "澳大利亚", "61" },
            { "Australia", "61" },
            { "日本", "81" },
            { "Japan", "81" },
            { "韩国", "82" },
            { "Korea", "82" }
        };

        if (countryCodeMap.TryGetValue(country, out string countryCode))
        {
            return $"+{countryCode}{mobile}";
        }

        // 如果找不到对应的国家区号，保持原样返回
        return mobile;
    }

    #endregion
} 
using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Lean.Cus.Common.Security;

/// <summary>
/// 密码处理助手类
/// </summary>
public static class LeanPasswordHelper
{
    /// <summary>
    /// 默认密码
    /// </summary>
    public const string DefaultPassword = "123456";

    /// <summary>
    /// 密码最小长度
    /// </summary>
    public const int MinPasswordLength = 8;

    /// <summary>
    /// 密码最大长度
    /// </summary>
    public const int MaxPasswordLength = 32;

    /// <summary>
    /// PBKDF2迭代次数
    /// </summary>
    private const int Iterations = 10000;

    /// <summary>
    /// 哈希长度
    /// </summary>
    private const int HashSize = 32;

    /// <summary>
    /// 盐值长度
    /// </summary>
    private const int SaltSize = 32;

    /// <summary>
    /// 常见密码字典
    /// </summary>
    private static readonly HashSet<string> CommonPasswords = new()
    {
        "password", "123456", "12345678", "qwerty", "abc123",
        "football", "letmein", "monkey", "696969", "shadow",
        "111111", "123123", "654321", "superman", "qazwsx"
    };

    /// <summary>
    /// 生成随机盐值
    /// </summary>
    /// <returns>盐值</returns>
    public static string GenerateSalt()
    {
        byte[] salt = new byte[SaltSize];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    /// <summary>
    /// 加密密码
    /// </summary>
    /// <param name="password">原始密码</param>
    /// <param name="salt">盐值</param>
    /// <returns>加密后的密码</returns>
    public static string EncryptPassword(string password, string salt)
    {
        if (string.IsNullOrEmpty(password))
            throw new ArgumentNullException(nameof(password));
        if (string.IsNullOrEmpty(salt))
            throw new ArgumentNullException(nameof(salt));

        byte[] saltBytes = Convert.FromBase64String(salt);
        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            saltBytes,
            Iterations,
            HashAlgorithmName.SHA256);

        byte[] hash = pbkdf2.GetBytes(HashSize);
        return Convert.ToBase64String(hash);
    }

    /// <summary>
    /// 验证密码
    /// </summary>
    /// <param name="password">原始密码</param>
    /// <param name="salt">盐值</param>
    /// <param name="encryptedPassword">加密后的密码</param>
    /// <returns>是否验证通过</returns>
    public static bool ValidatePassword(string password, string salt, string encryptedPassword)
    {
        if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(encryptedPassword))
            return false;

        try
        {
            string newEncryptedPassword = EncryptPassword(password, salt);
            return newEncryptedPassword == encryptedPassword;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 验证密码强度
    /// </summary>
    /// <param name="password">密码</param>
    /// <returns>密码强度是否合格</returns>
    public static (bool IsValid, string Message) ValidatePasswordStrength(string password)
    {
        if (string.IsNullOrEmpty(password))
            return (false, "密码不能为空");

        if (password.Length < MinPasswordLength)
            return (false, $"密码长度不能小于{MinPasswordLength}位");

        if (password.Length > MaxPasswordLength)
            return (false, $"密码长度不能大于{MaxPasswordLength}位");

        if (CommonPasswords.Contains(password.ToLower()))
            return (false, "密码过于简单，请使用更复杂的密码");

        bool hasNumber = false;
        bool hasLower = false;
        bool hasUpper = false;
        bool hasSpecial = false;

        foreach (char c in password)
        {
            if (char.IsDigit(c)) hasNumber = true;
            else if (char.IsLower(c)) hasLower = true;
            else if (char.IsUpper(c)) hasUpper = true;
            else if (!char.IsLetterOrDigit(c)) hasSpecial = true;
        }

        var requirements = new List<string>();
        if (!hasNumber) requirements.Add("数字");
        if (!hasLower) requirements.Add("小写字母");
        if (!hasUpper) requirements.Add("大写字母");
        if (!hasSpecial) requirements.Add("特殊字符");

        if (requirements.Count > 0)
            return (false, $"密码必须包含{string.Join("、", requirements)}");

        return (true, "密码强度合格");
    }

    /// <summary>
    /// 生成随机密码
    /// </summary>
    /// <param name="length">密码长度，默认12位</param>
    /// <returns>随机密码</returns>
    public static string GenerateRandomPassword(int length = 12)
    {
        if (length < MinPasswordLength)
            throw new ArgumentException($"密码长度不能小于{MinPasswordLength}位");

        if (length > MaxPasswordLength)
            throw new ArgumentException($"密码长度不能大于{MaxPasswordLength}位");

        const string numbers = "0123456789";
        const string lowerLetters = "abcdefghijklmnopqrstuvwxyz";
        const string upperLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string specialChars = "!@#$%^&*()_+-=[]{}|;:,.<>?";

        using var rng = RandomNumberGenerator.Create();
        var password = new char[length];

        // 确保包含至少一个数字、小写字母、大写字母和特殊字符
        password[0] = GetRandomChar(numbers, rng);
        password[1] = GetRandomChar(lowerLetters, rng);
        password[2] = GetRandomChar(upperLetters, rng);
        password[3] = GetRandomChar(specialChars, rng);

        // 生成剩余字符
        string allChars = numbers + lowerLetters + upperLetters + specialChars;
        for (int i = 4; i < length; i++)
        {
            password[i] = GetRandomChar(allChars, rng);
        }

        // 打乱密码字符顺序
        for (int i = length - 1; i > 0; i--)
        {
            int j = GetRandomInt(rng, i + 1);
            (password[i], password[j]) = (password[j], password[i]);
        }

        return new string(password);
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <returns>重置后的密码</returns>
    public static (string Password, string Salt, string EncryptedPassword) ResetPassword()
    {
        var password = DefaultPassword;
        var salt = GenerateSalt();
        var encryptedPassword = EncryptPassword(password, salt);
        return (password, salt, encryptedPassword);
    }

    private static char GetRandomChar(string chars, RandomNumberGenerator rng)
    {
        return chars[GetRandomInt(rng, chars.Length)];
    }

    private static int GetRandomInt(RandomNumberGenerator rng, int max)
    {
        byte[] randomInt = new byte[4];
        rng.GetBytes(randomInt);
        return Math.Abs(BitConverter.ToInt32(randomInt, 0)) % max;
    }
} 
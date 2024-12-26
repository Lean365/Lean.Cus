//===================================================
// 项目名称：Lean.Cus.Common.Utils
// 文件名称：EncryptionUtil
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：加密解密工具类
//===================================================

using System.Security.Cryptography;
using System.Text;

namespace Lean.Cus.Common.Utils;

/// <summary>
/// 加密解密工具类
/// </summary>
public static class EncryptionUtil
{
    /// <summary>
    /// MD5加密
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>MD5加密字符串</returns>
    public static string MD5Encrypt(string input)
    {
        using var md5 = MD5.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = md5.ComputeHash(inputBytes);
        var builder = new StringBuilder();
        foreach (var b in hashBytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }

    /// <summary>
    /// SHA1加密
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>SHA1加密字符串</returns>
    public static string SHA1Encrypt(string input)
    {
        using var sha1 = SHA1.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha1.ComputeHash(inputBytes);
        var builder = new StringBuilder();
        foreach (var b in hashBytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }

    /// <summary>
    /// SHA256加密
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>SHA256加密字符串</returns>
    public static string SHA256Encrypt(string input)
    {
        using var sha256 = SHA256.Create();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var hashBytes = sha256.ComputeHash(inputBytes);
        var builder = new StringBuilder();
        foreach (var b in hashBytes)
        {
            builder.Append(b.ToString("x2"));
        }
        return builder.ToString();
    }

    /// <summary>
    /// AES加密
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="key">密钥</param>
    /// <returns>AES加密字符串</returns>
    public static string AESEncrypt(string input, string key)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32, '0').Substring(0, 32));
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.PKCS7;

        using var encryptor = aes.CreateEncryptor();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
        return Convert.ToBase64String(encryptedBytes);
    }

    /// <summary>
    /// AES解密
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="key">密钥</param>
    /// <returns>AES解密字符串</returns>
    public static string AESDecrypt(string input, string key)
    {
        using var aes = Aes.Create();
        aes.Key = Encoding.UTF8.GetBytes(key.PadRight(32, '0').Substring(0, 32));
        aes.Mode = CipherMode.ECB;
        aes.Padding = PaddingMode.PKCS7;

        using var decryptor = aes.CreateDecryptor();
        var inputBytes = Convert.FromBase64String(input);
        var decryptedBytes = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    /// <summary>
    /// DES加密
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="key">密钥</param>
    /// <returns>DES加密字符串</returns>
    public static string DESEncrypt(string input, string key)
    {
        using var des = DES.Create();
        des.Key = Encoding.UTF8.GetBytes(key.PadRight(8, '0').Substring(0, 8));
        des.Mode = CipherMode.ECB;
        des.Padding = PaddingMode.PKCS7;

        using var encryptor = des.CreateEncryptor();
        var inputBytes = Encoding.UTF8.GetBytes(input);
        var encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
        return Convert.ToBase64String(encryptedBytes);
    }

    /// <summary>
    /// DES解密
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <param name="key">密钥</param>
    /// <returns>DES解密字符串</returns>
    public static string DESDecrypt(string input, string key)
    {
        using var des = DES.Create();
        des.Key = Encoding.UTF8.GetBytes(key.PadRight(8, '0').Substring(0, 8));
        des.Mode = CipherMode.ECB;
        des.Padding = PaddingMode.PKCS7;

        using var decryptor = des.CreateDecryptor();
        var inputBytes = Convert.FromBase64String(input);
        var decryptedBytes = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);
        return Encoding.UTF8.GetString(decryptedBytes);
    }

    /// <summary>
    /// Base64编码
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>Base64编码字符串</returns>
    public static string Base64Encode(string input)
    {
        var inputBytes = Encoding.UTF8.GetBytes(input);
        return Convert.ToBase64String(inputBytes);
    }

    /// <summary>
    /// Base64解码
    /// </summary>
    /// <param name="input">输入字符串</param>
    /// <returns>Base64解码字符串</returns>
    public static string Base64Decode(string input)
    {
        var inputBytes = Convert.FromBase64String(input);
        return Encoding.UTF8.GetString(inputBytes);
    }

    #region PBKDF2 密码哈希

    /// <summary>
    /// PBKDF2 迭代次数
    /// </summary>
    private const int PBKDF2_ITERATIONS = 10000;

    /// <summary>
    /// PBKDF2 哈希长度（字节）
    /// </summary>
    private const int PBKDF2_HASH_SIZE = 32;

    /// <summary>
    /// 盐值长度（字节）
    /// </summary>
    private const int SALT_SIZE = 16;

    /// <summary>
    /// 使用 PBKDF2 生成密码哈希
    /// </summary>
    /// <param name="password">密码</param>
    /// <returns>格式化的哈希结果（盐值:哈希:迭代次数）</returns>
    public static string HashPassword(string password)
    {
        // 生成随机盐值
        byte[] salt = new byte[SALT_SIZE];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // 使用 PBKDF2 生成哈希
        byte[] hash = GetPbkdf2Hash(password, salt, PBKDF2_ITERATIONS);

        // 格式化结果：Base64(salt):Base64(hash):iterations
        string saltString = Convert.ToBase64String(salt);
        string hashString = Convert.ToBase64String(hash);
        return $"{saltString}:{hashString}:{PBKDF2_ITERATIONS}";
    }

    /// <summary>
    /// 验证密码是否匹配存储的哈希
    /// </summary>
    /// <param name="password">要验证的密码</param>
    /// <param name="storedHash">存储的哈希值（格式：盐值:哈希:迭代次数）</param>
    /// <returns>密码是否匹配</returns>
    public static bool VerifyPassword(string password, string storedHash)
    {
        try
        {
            // 解析存储的哈希值
            string[] parts = storedHash.Split(':');
            if (parts.Length != 3)
            {
                return false;
            }

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);
            int iterations = int.Parse(parts[2]);

            // 使用相同的参数生成哈希
            byte[] testHash = GetPbkdf2Hash(password, salt, iterations);

            // 比较哈希值
            return hash.SequenceEqual(testHash);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 使用 PBKDF2 生成哈希
    /// </summary>
    private static byte[] GetPbkdf2Hash(string password, byte[] salt, int iterations)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(PBKDF2_HASH_SIZE);
    }

    #endregion
} 
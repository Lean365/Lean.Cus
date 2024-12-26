//===================================================
// 项目名称：Lean.Cus.Common.Security
// 文件名称：LeanSecurityHelper
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：安全助手类
//===================================================

using System.Security.Cryptography;
using System.Text;

namespace Lean.Cus.Common.Security;

/// <summary>
/// 安全助手类
/// </summary>
public static class LeanSecurityHelper
{
    // PBKDF2 参数
    private const int SALT_SIZE = 16; // 盐值大小（字节）
    private const int HASH_SIZE = 32; // 哈希大小（字节）
    private const int ITERATIONS = 10000; // 迭代次数
    private const char DELIMITER = ':'; // 分隔符

    /// <summary>
    /// 生成随机盐值
    /// </summary>
    /// <returns>盐值</returns>
    public static string GenerateSalt()
    {
        var salt = new byte[SALT_SIZE];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);
        return Convert.ToBase64String(salt);
    }

    /// <summary>
    /// 加密密码
    /// </summary>
    /// <param name="password">原始密码</param>
    /// <param name="salt">盐值（Base64字符串）</param>
    /// <returns>加密后的密码（格式：iterations:salt:hash）</returns>
    public static string EncryptPassword(string password, string salt)
    {
        // 将Base64盐值转换回字节数组
        var saltBytes = Convert.FromBase64String(salt);
        
        // 使用PBKDF2派生密钥
        using var pbkdf2 = new Rfc2898DeriveBytes(
            password,
            saltBytes,
            ITERATIONS,
            HashAlgorithmName.SHA256);
        
        var hash = pbkdf2.GetBytes(HASH_SIZE);

        // 返回格式化的字符串：iterations:salt:hash
        return string.Join(DELIMITER,
            ITERATIONS,
            salt,
            Convert.ToBase64String(hash));
    }

    /// <summary>
    /// 验证密码
    /// </summary>
    /// <param name="password">原始密码</param>
    /// <param name="salt">盐值（Base64字符串）</param>
    /// <param name="hashedPassword">加密后的密码（格式：iterations:salt:hash）</param>
    /// <returns>是否匹配</returns>
    public static bool VerifyPassword(string password, string salt, string hashedPassword)
    {
        try
        {
            // 分解哈希字符串
            var parts = hashedPassword.Split(DELIMITER);
            if (parts.Length != 3)
                return false;

            var iterations = int.Parse(parts[0]);
            var storedSalt = parts[1];
            var storedHash = parts[2];

            // 验证盐值是否匹配
            if (salt != storedSalt)
                return false;

            // 使用相同参数重新计算哈希
            var saltBytes = Convert.FromBase64String(salt);
            using var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                saltBytes,
                iterations,
                HashAlgorithmName.SHA256);
            
            var computedHash = pbkdf2.GetBytes(HASH_SIZE);
            var computedHashString = Convert.ToBase64String(computedHash);

            // 比较哈希值
            return computedHashString == storedHash;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 生成完整的密码哈希（包含迭代次数、盐值和哈希值）
    /// </summary>
    /// <param name="password">原始密码</param>
    /// <returns>完整的密码哈希</returns>
    public static (string hash, string salt) CreatePasswordHash(string password)
    {
        var salt = GenerateSalt();
        var hash = EncryptPassword(password, salt);
        return (hash, salt);
    }
} 
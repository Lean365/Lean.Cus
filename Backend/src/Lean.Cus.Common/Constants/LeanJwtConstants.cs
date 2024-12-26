//===================================================
// 项目名称：Lean.Cus.Common.Constants
// 文件名称：LeanJwtConstants
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：JWT常量
//===================================================

namespace Lean.Cus.Common.Constants;

/// <summary>
/// JWT常量
/// </summary>
public static class LeanJwtConstants
{
    /// <summary>
    /// 密钥
    /// </summary>
    public const string SecurityKey = "LeanJwtSecurityKey2024";

    /// <summary>
    /// 发行人
    /// </summary>
    public const string Issuer = "LeanJwtIssuer";

    /// <summary>
    /// 订阅人
    /// </summary>
    public const string Audience = "LeanJwtAudience";

    /// <summary>
    /// 过期时间（分钟）
    /// </summary>
    public const int ExpirationMinutes = 120;

    /// <summary>
    /// 刷新令牌过期时间（天）
    /// </summary>
    public const int RefreshTokenExpirationDays = 7;
} 
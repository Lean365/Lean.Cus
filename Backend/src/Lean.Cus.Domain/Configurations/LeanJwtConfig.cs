//===================================================
// 项目名称：Lean.Cus.Domain.Configurations
// 文件名称：LeanJwtConfig
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：JWT配置
//===================================================

namespace Lean.Cus.Domain.Configurations;

/// <summary>
/// JWT配置
/// </summary>
public class LeanJwtConfig
{
    /// <summary>
    /// 密钥
    /// </summary>
    public required string SecretKey { get; set; }

    /// <summary>
    /// 发行者
    /// </summary>
    public required string Issuer { get; set; }

    /// <summary>
    /// 受众
    /// </summary>
    public required string Audience { get; set; }

    /// <summary>
    /// 过期时间（分钟）
    /// </summary>
    public int ExpirationMinutes { get; set; } = 60;

    /// <summary>
    /// 刷新令牌过期时间（天）
    /// </summary>
    public int RefreshTokenExpirationDays { get; set; } = 7;
} 
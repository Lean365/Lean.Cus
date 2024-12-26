//===================================================
// 项目名称：Lean.Cus.Infrastructure.Authentication
// 文件名称：LeanJwtService
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：JWT服务实现
//===================================================

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Lean.Cus.Domain.Configurations;
using Lean.Cus.Domain.Interfaces;

namespace Lean.Cus.Infrastructure.Authentication;

/// <summary>
/// JWT服务实现
/// </summary>
public class LeanJwtService : ILeanJwtService
{
    private readonly LeanJwtConfig _jwtConfig;
    private readonly JwtSecurityTokenHandler _tokenHandler;
    private readonly SigningCredentials _signingCredentials;
    private readonly TokenValidationParameters _validationParameters;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="jwtConfig">JWT配置</param>
    public LeanJwtService(LeanJwtConfig jwtConfig)
    {
        _jwtConfig = jwtConfig;
        _tokenHandler = new JwtSecurityTokenHandler();

        var key = Encoding.UTF8.GetBytes(_jwtConfig.SecretKey);
        var securityKey = new SymmetricSecurityKey(key);
        _signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        _validationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtConfig.Issuer,
            ValidAudience = _jwtConfig.Audience,
            IssuerSigningKey = securityKey,
            ClockSkew = TimeSpan.Zero
        };
    }

    /// <summary>
    /// 创建访问令牌
    /// </summary>
    /// <param name="claims">声明集合</param>
    /// <returns>访问令牌</returns>
    public string CreateAccessToken(IEnumerable<Claim> claims)
    {
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_jwtConfig.ExpirationMinutes),
            SigningCredentials = _signingCredentials,
            Issuer = _jwtConfig.Issuer,
            Audience = _jwtConfig.Audience
        };

        var token = _tokenHandler.CreateToken(tokenDescriptor);
        return _tokenHandler.WriteToken(token);
    }

    /// <summary>
    /// 创建刷新令牌
    /// </summary>
    /// <returns>刷新令牌</returns>
    public string CreateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }

    /// <summary>
    /// 验证访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <returns>声明主体</returns>
    public ClaimsPrincipal? ValidateAccessToken(string token)
    {
        try
        {
            return _tokenHandler.ValidateToken(token, _validationParameters, out _);
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// 从访问令牌中获取声明
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <returns>声明集合</returns>
    public IEnumerable<Claim>? GetClaimsFromAccessToken(string token)
    {
        try
        {
            var principal = ValidateAccessToken(token);
            return principal?.Claims;
        }
        catch
        {
            return null;
        }
    }
} 
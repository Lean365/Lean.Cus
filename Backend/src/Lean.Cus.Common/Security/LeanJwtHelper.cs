using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Lean.Cus.Common.Configs;

namespace Lean.Cus.Common.Security;

/// <summary>
/// JWT帮助类
/// </summary>
public static class LeanJwtHelper
{
    /// <summary>
    /// 生成Token
    /// </summary>
    public static string GenerateToken(LeanJwtConfig jwtConfig, long userId, string userName, string[] roles)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.UniqueName, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, string.Join(",", roles))
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.Now.AddMinutes(jwtConfig.ExpireMinutes);

        var token = new JwtSecurityToken(
            issuer: jwtConfig.Issuer,
            audience: jwtConfig.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// 生成刷新Token
    /// </summary>
    public static string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString("N");
    }

    /// <summary>
    /// 验证Token
    /// </summary>
    public static ClaimsPrincipal ValidateToken(string token, LeanJwtConfig jwtConfig)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(jwtConfig.SecretKey);

        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(key),
            ValidateIssuer = true,
            ValidIssuer = jwtConfig.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtConfig.Audience,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };

        return tokenHandler.ValidateToken(token, tokenValidationParameters, out _);
    }

    /// <summary>
    /// 从Token中获取用户ID
    /// </summary>
    public static long GetUserId(ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
        return long.TryParse(userIdClaim, out var userId) ? userId : 0;
    }

    /// <summary>
    /// 从Token中获取用户名
    /// </summary>
    public static string GetUserName(ClaimsPrincipal user)
    {
        return user.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value ?? string.Empty;
    }

    /// <summary>
    /// 从Token中获取角色
    /// </summary>
    public static string[] GetRoles(ClaimsPrincipal user)
    {
        var rolesClaim = user.FindFirst(ClaimTypes.Role)?.Value;
        return rolesClaim?.Split(',', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();
    }
} 
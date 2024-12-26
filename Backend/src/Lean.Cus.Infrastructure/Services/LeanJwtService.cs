//===================================================
// 项目名称：Lean.Cus.Infrastructure.Services
// 文件名称：LeanJwtService
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：JWT服务实现类
//===================================================

namespace Lean.Cus.Infrastructure.Services;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

/// <summary>
/// JWT服务实现类
/// <para>用于生成和验证JWT令牌</para>
/// <para>提供访问令牌和刷新令牌的管理</para>
/// </summary>
public class LeanJwtService
{
    /// <summary>
    /// 生成访问令牌
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="userName">用户名</param>
    /// <param name="roles">角色列表</param>
    /// <returns>访问令牌</returns>
    public string GenerateAccessToken(string userId, string userName, IEnumerable<string> roles)
    {
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, userId),
            new(JwtRegisteredClaimNames.UniqueName, userName),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(LeanJwtConstants.SecurityKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: LeanJwtConstants.Issuer,
            audience: LeanJwtConstants.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(LeanJwtConstants.ExpirationMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    /// <summary>
    /// 生成刷新令牌
    /// </summary>
    /// <returns>刷新令牌</returns>
    public string GenerateRefreshToken()
    {
        return Guid.NewGuid().ToString();
    }

    /// <summary>
    /// 验证访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <returns>如果验证成功返回true，否则返回false</returns>
    public bool ValidateToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(LeanJwtConstants.SecurityKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = LeanJwtConstants.Issuer,
                ValidateAudience = true,
                ValidAudience = LeanJwtConstants.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out _);

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 从令牌中获取用户ID
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <returns>用户ID</returns>
    public string GetUserIdFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.Sub).Value;
    }

    /// <summary>
    /// 从令牌中获取用户名
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <returns>用户名</returns>
    public string GetUserNameFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        return jwtToken.Claims.First(x => x.Type == JwtRegisteredClaimNames.UniqueName).Value;
    }

    /// <summary>
    /// 从令牌中获取角色列表
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <returns>角色列表</returns>
    public IEnumerable<string> GetRolesFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var jwtToken = tokenHandler.ReadJwtToken(token);
        return jwtToken.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);
    }
} 
//===================================================
// 项目名称：Lean.Cus.Domain.Interfaces
// 文件名称：ILeanJwtService
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：JWT服务接口
//===================================================

using System.Security.Claims;

namespace Lean.Cus.Domain.Interfaces;

/// <summary>
/// JWT服务接口
/// </summary>
public interface ILeanJwtService
{
    /// <summary>
    /// 创建访问令牌
    /// </summary>
    /// <param name="claims">声明集合</param>
    /// <returns>访问令牌</returns>
    string CreateAccessToken(IEnumerable<Claim> claims);

    /// <summary>
    /// 创建刷新令牌
    /// </summary>
    /// <returns>刷新令牌</returns>
    string CreateRefreshToken();

    /// <summary>
    /// 验证访问令牌
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <returns>声明主体</returns>
    ClaimsPrincipal? ValidateAccessToken(string token);

    /// <summary>
    /// 从访问令牌中获取声明
    /// </summary>
    /// <param name="token">访问令牌</param>
    /// <returns>声明集合</returns>
    IEnumerable<Claim>? GetClaimsFromAccessToken(string token);
} 
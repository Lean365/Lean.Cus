//===================================================
// 项目名称：Lean.Cus.Application.Services.Auth
// 文件名称：ILeanAuthService
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：认证服务接口
//===================================================

using Lean.Cus.Application.Dtos.Auth;
using Lean.Cus.Common.Results;

namespace Lean.Cus.Application.Services.Auth;

/// <summary>
/// 认证服务接口
/// </summary>
public interface ILeanAuthService
{
    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="dto">登录信息</param>
    /// <returns>登录结果</returns>
    Task<LeanApiResult<LeanLoginResultDto>> LoginAsync(LeanLoginDto dto);

    /// <summary>
    /// 强制退出
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>退出结果</returns>
    Task<LeanApiResult<bool>> ForceLogoutAsync(long userId);
}
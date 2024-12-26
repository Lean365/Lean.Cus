//===================================================
// 项目名称：Lean.Cus.Application.Dtos.Auth
// 文件名称：LeanLoginResultDto
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：登录结果DTO
//===================================================

namespace Lean.Cus.Application.Dtos.Auth;

/// <summary>
/// 登录结果DTO
/// </summary>
public class LeanLoginResultDto
{
    /// <summary>
    /// 用户ID
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// 用户名
    /// </summary>
    public string UserName { get; set; } = null!;

    /// <summary>
    /// 昵称
    /// </summary>
    public string? NickName { get; set; }

    /// <summary>
    /// 用户类型（0=超级管理员，1=管理员，2=普通用户）
    /// </summary>
    public int UserType { get; set; }

    /// <summary>
    /// 头像
    /// </summary>
    public string? Avatar { get; set; }
} 
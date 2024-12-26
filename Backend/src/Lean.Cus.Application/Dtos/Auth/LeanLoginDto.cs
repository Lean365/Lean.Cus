//===================================================
// 项目名称：Lean.Cus.Application.Dtos.Auth
// 文件名称：LeanLoginDto
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：登录DTO
//===================================================

using System.ComponentModel.DataAnnotations;

namespace Lean.Cus.Application.Dtos.Auth;

/// <summary>
/// 登录DTO
/// </summary>
public class LeanLoginDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空")]
    public required string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    public required string Password { get; set; }
} 
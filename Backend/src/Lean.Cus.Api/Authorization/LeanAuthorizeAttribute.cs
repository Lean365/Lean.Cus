//===================================================
// 项目名称：Lean.Cus.Api.Authorization
// 文件名称：LeanAuthorizeAttribute
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：自定义授权特性
//===================================================

using Microsoft.AspNetCore.Authorization;

namespace Lean.Cus.Api.Authorization;

/// <summary>
/// 自定义授权特性
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class LeanAuthorizeAttribute : AuthorizeAttribute
{
    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanAuthorizeAttribute()
    {
        Policy = "LeanPolicy";
    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roles">角色列表，多个角色用逗号分隔</param>
    public LeanAuthorizeAttribute(string roles) : this()
    {
        Roles = roles;
    }
} 
//===================================================
// 项目名称：Lean.Cus.Api.Attributes
// 文件名称：LeanPermissionAttribute
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：权限特性
//===================================================

using Microsoft.AspNetCore.Authorization;

namespace Lean.Cus.Api.Attributes;

/// <summary>
/// 权限特性
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public class LeanPermissionAttribute : AuthorizeAttribute
{
    /// <summary>
    /// 权限编码
    /// </summary>
    public string PermissionCode { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="permissionCode">权限编码，格式：{目录}:{实体}:{操作}</param>
    public LeanPermissionAttribute(string permissionCode)
    {
        PermissionCode = permissionCode;
        Policy = $"Permission_{permissionCode}";
    }
} 
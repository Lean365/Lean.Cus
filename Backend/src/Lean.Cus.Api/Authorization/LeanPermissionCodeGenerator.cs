//===================================================
// 项目名称：Lean.Cus.Api.Authorization
// 文件名称：LeanPermissionCodeGenerator
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：权限编码生成器
//===================================================

using System.Reflection;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Api.Authorization;

/// <summary>
/// 权限编码生成器
/// </summary>
public static class LeanPermissionCodeGenerator
{
    /// <summary>
    /// 生成权限编码
    /// </summary>
    /// <param name="type">控制器类型</param>
    /// <param name="businessType">业务类型</param>
    /// <returns>权限编码</returns>
    public static string Generate(Type type, LeanBusinessTypeEnum businessType)
    {
        // 获取目录名（从命名空间中提取）
        var directory = type.Namespace?.Split('.').LastOrDefault()?.ToLower() ?? string.Empty;

        // 获取实体名（从控制器名中提取）
        var entityName = type.Name.Replace("Controller", "").Replace("Lean", "").Replace("Sys", "").ToLower();

        // 获取操作类型（从枚举值中提取）
        var operation = businessType.ToString().ToLower();

        // 返回格式化的权限编码
        return $"{directory}:{entityName}:{operation}";
    }
} 
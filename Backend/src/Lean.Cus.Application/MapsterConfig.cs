using Mapster;
using System.Reflection;

namespace Lean.Cus.Application;

/// <summary>
/// Mapster 配置类
/// </summary>
public static class MapsterConfig
{
    /// <summary>
    /// 初始化 Mapster 配置
    /// </summary>
    public static void Initialize()
    {
        // 获取所有程序集
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => a.FullName != null && a.FullName.Contains("Lean.Cus"))
            .ToArray();

        // 全局映射配置
        TypeAdapterConfig.GlobalSettings
            .Default
            .NameMatchingStrategy(NameMatchingStrategy.Flexible)
            .PreserveReference(true)
            .IgnoreNullValues(true);

        // 扫描所有程序集，注册映射配置
        TypeAdapterConfig.GlobalSettings.Scan(assemblies);
    }
} 
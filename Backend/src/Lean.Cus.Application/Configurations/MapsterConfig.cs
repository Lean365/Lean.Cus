using Mapster;
using System.Reflection;

namespace Lean.Cus.Application.Configurations
{
    /// <summary>
    /// Mapster 配置类
    /// </summary>
    public static class MapsterConfig
    {
        /// <summary>
        /// 注册 Mapster 全局配置
        /// </summary>
        public static void RegisterMappings()
        {
            // 扫描程序集，注册所有映射配置
            TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

            // 配置全局映射规则
            TypeAdapterConfig.GlobalSettings.Default
                .NameMatchingStrategy(NameMatchingStrategy.Flexible)
                .PreserveReference(true)
                .MaxDepth(3);

            // 在这里添加自定义的映射规则
            // 例如：
            // TypeAdapterConfig<TSource, TDestination>.NewConfig()
            //     .Map(dest => dest.Property, src => src.Property);
        }
    }
} 
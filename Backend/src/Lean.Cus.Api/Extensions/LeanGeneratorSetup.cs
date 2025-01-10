using Lean.Cus.Generator.IServices.Designer;
using Lean.Cus.Generator.IServices.Generator;
using Lean.Cus.Generator.IServices.Import;
using Lean.Cus.Generator.Services.Designer;
using Lean.Cus.Generator.Services.Generator;
using Lean.Cus.Generator.Services.Import;
using Microsoft.Extensions.DependencyInjection;

namespace Lean.Cus.Api.Extensions;

/// <summary>
/// 代码生成器服务注册扩展
/// </summary>
public static class LeanGeneratorSetup
{
    /// <summary>
    /// 添加代码生成器服务
    /// </summary>
    /// <param name="services">服务集合</param>
    public static void AddLeanGenerator(this IServiceCollection services)
    {
        // 注册代码生成器服务
        services.AddScoped<ILeanGeneratorService, LeanGeneratorService>();
        services.AddScoped<ILeanDesignerService, LeanDesignerService>();
        services.AddScoped<ILeanImportService, LeanImportService>();
        services.AddScoped<ILeanTemplateService, LeanTemplateService>();
    }
}
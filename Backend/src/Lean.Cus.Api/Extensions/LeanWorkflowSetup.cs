using Lean.Cus.Workflow.Services;
using Lean.Cus.Workflow.Services.Impl;

namespace Lean.Cus.Api.Extensions;

/// <summary>
/// 工作流服务注册扩展
/// </summary>
public static class LeanWorkflowSetup
{
    /// <summary>
    /// 添加工作流服务
    /// </summary>
    /// <param name="services">服务集合</param>
    public static void AddLeanWorkflow(this IServiceCollection services)
    {
        // 注册工作流服务
        services.AddScoped<ILeanWorkflowDefinitionService, LeanWorkflowDefinitionService>();
        services.AddScoped<ILeanWorkflowInstanceService, LeanWorkflowInstanceService>();
        services.AddScoped<ILeanWorkflowTaskService, LeanWorkflowTaskService>();
        services.AddScoped<ILeanWorkflowHistoryService, LeanWorkflowHistoryService>();
    }
}
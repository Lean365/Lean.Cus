using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Lean.Cus.Infrastructure.Filters;

/// <summary>
/// 全局过滤器基类
/// </summary>
public abstract class LeanActionFilterAttribute : ActionFilterAttribute
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    protected readonly ILogger Logger;

    /// <summary>
    /// 构造函数
    /// </summary>
    protected LeanActionFilterAttribute(ILogger logger)
    {
        Logger = logger;
    }

    /// <summary>
    /// 执行前
    /// </summary>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        Logger.LogInformation($"执行{context.ActionDescriptor.DisplayName}开始");
        base.OnActionExecuting(context);
    }

    /// <summary>
    /// 执行后
    /// </summary>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        Logger.LogInformation($"执行{context.ActionDescriptor.DisplayName}结束");
        base.OnActionExecuted(context);
    }
} 
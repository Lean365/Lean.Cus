using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Lean.Cus.Infrastructure.Filters;

/// <summary>
/// 防CSRF攻击过滤器
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LeanAntiForgeryFilterAttribute : LeanActionFilterAttribute
{
    private readonly IAntiforgery _antiforgery;

    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanAntiForgeryFilterAttribute(IAntiforgery antiforgery, ILogger<LeanAntiForgeryFilterAttribute> logger)
        : base(logger)
    {
        _antiforgery = antiforgery;
    }

    /// <summary>
    /// 执行前验证防伪令牌
    /// </summary>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        if (context.HttpContext.Request.Method == HttpMethods.Get ||
            context.HttpContext.Request.Method == HttpMethods.Head ||
            context.HttpContext.Request.Method == HttpMethods.Options ||
            context.HttpContext.Request.Method == HttpMethods.Trace)
        {
            return;
        }

        try
        {
            _antiforgery.ValidateRequestAsync(context.HttpContext).GetAwaiter().GetResult();
        }
        catch (AntiforgeryValidationException ex)
        {
            Logger.LogWarning(ex, "CSRF验证失败");
            context.Result = new BadRequestObjectResult("无效的请求");
        }
    }

    /// <summary>
    /// 执行后生成新的防伪令牌
    /// </summary>
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        base.OnActionExecuted(context);

        // 生成新的防伪令牌
        var tokens = _antiforgery.GetAndStoreTokens(context.HttpContext);
        context.HttpContext.Response.Cookies.Append(
            "XSRF-TOKEN",
            tokens.RequestToken!,
            new CookieOptions
            {
                HttpOnly = false,
                Secure = true,
                SameSite = SameSiteMode.Strict
            });
    }
} 
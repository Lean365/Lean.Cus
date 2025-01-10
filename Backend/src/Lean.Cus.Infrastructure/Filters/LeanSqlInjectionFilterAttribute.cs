using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace Lean.Cus.Infrastructure.Filters;

/// <summary>
/// SQL注入过滤器
/// </summary>
[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class LeanSqlInjectionFilterAttribute : LeanActionFilterAttribute
{
    private static readonly string[] SqlKeywords = {
        "select", "insert", "delete", "update", "drop", "truncate", "exec", "execute",
        "--", "/*", "*/", ";", "@@", "@", "char", "nchar", "varchar", "nvarchar",
        "alter", "begin", "cast", "create", "cursor", "declare", "fetch", "kill",
        "open", "schema", "sysobjects", "syscolumns"
    };

    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanSqlInjectionFilterAttribute(ILogger<LeanSqlInjectionFilterAttribute> logger) : base(logger)
    {
    }

    /// <summary>
    /// 执行前检查SQL注入
    /// </summary>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);

        // 检查请求参数
        foreach (var parameter in context.ActionArguments)
        {
            if (parameter.Value == null) continue;

            var paramValue = parameter.Value.ToString();
            if (string.IsNullOrEmpty(paramValue)) continue;

            // 检查SQL注入
            if (IsSqlInjection(paramValue))
            {
                Logger.LogWarning($"检测到SQL注入攻击: {paramValue}");
                context.Result = new BadRequestObjectResult("检测到非法字符");
                return;
            }
        }
    }

    private static bool IsSqlInjection(string value)
    {
        if (string.IsNullOrEmpty(value)) return false;

        // 转换为小写进行检查
        value = value.ToLower();

        // 检查SQL关键字
        foreach (var keyword in SqlKeywords)
        {
            if (value.Contains(keyword))
            {
                // 进一步验证是否是完整的单词
                var pattern = $@"\b{keyword}\b";
                if (Regex.IsMatch(value, pattern))
                {
                    return true;
                }
            }
        }

        // 检查特殊字符组合
        var specialPatterns = new[]
        {
            @"(\s+and\s+[\w\s-]+=[^'])",
            @"(\s+or\s+[\w\s-]+=[^'])",
            @"(/\*|\*/|;|\bor\b|\band\b|--)",
            @"(xp_cmdshell|sp_executesql)",
            @"(declare\s+@|exec\s+@|execute\s+@)"
        };

        return specialPatterns.Any(pattern => Regex.IsMatch(value, pattern, RegexOptions.IgnoreCase));
    }
} 
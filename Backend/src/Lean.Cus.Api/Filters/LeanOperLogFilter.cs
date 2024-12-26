//===================================================
// 项目名称：Lean.Cus.Api.Filters
// 文件名称：LeanOperLogFilter
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：操作日志过滤器
//===================================================

using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Lean.Cus.Common.Enums;
using Lean.Cus.Domain.Entities.Monitor;
using Lean.Cus.Api.Attributes;
using SqlSugar;

namespace Lean.Cus.Api.Filters;

/// <summary>
/// 操作日志过滤器
/// </summary>
public class LeanOperLogFilter : IAsyncActionFilter
{
    private readonly ISqlSugarClient _db;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public LeanOperLogFilter(ISqlSugarClient db, IHttpContextAccessor httpContextAccessor)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// 执行操作日志记录
    /// </summary>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 获取控制器特性
        var controllerActionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
        if (controllerActionDescriptor == null)
        {
            await next();
            return;
        }

        // 获取操作日志特性
        var logAttribute = controllerActionDescriptor.MethodInfo.GetCustomAttributes(typeof(LeanOperLogAttribute), false)
            .FirstOrDefault() as LeanOperLogAttribute;
        if (logAttribute == null)
        {
            await next();
            return;
        }

        var stopwatch = new Stopwatch();
        stopwatch.Start();
        var operLog = new LeanOperLog();

        try
        {
            // 记录请求信息
            var httpContext = _httpContextAccessor.HttpContext;
            operLog.Title = logAttribute.Title;
            operLog.BusinessType = (int)logAttribute.BusinessType;
            operLog.Method = $"{controllerActionDescriptor.ControllerName}/{controllerActionDescriptor.ActionName}";
            operLog.RequestMethod = httpContext?.Request.Method ?? string.Empty;
            operLog.OperUrl = httpContext?.Request.Path.Value ?? string.Empty;
            operLog.OperIp = httpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            operLog.OperName = httpContext?.User.Identity?.Name ?? string.Empty;
            operLog.OperParam = JsonSerializer.Serialize(context.ActionArguments);
            operLog.OperTime = DateTime.Now;

            // 执行操作
            var result = await next();

            // 记录响应信息
            if (result.Result != null)
            {
                operLog.JsonResult = JsonSerializer.Serialize(result.Result);
            }
            operLog.Status = 0; // 正常
        }
        catch (Exception ex)
        {
            operLog.Status = 1; // 异常
            operLog.ErrorMsg = ex.Message;
            throw;
        }
        finally
        {
            stopwatch.Stop();
            operLog.CostTime = stopwatch.ElapsedMilliseconds;

            // 异步保存日志
            _ = _db.Insertable(operLog).ExecuteCommandAsync();
        }
    }
} 
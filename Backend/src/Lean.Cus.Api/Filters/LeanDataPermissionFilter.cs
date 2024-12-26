//===================================================
// 项目名称：Lean.Cus.Api.Filters
// 文件名称：LeanDataPermissionFilter
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：数据权限过滤器
//===================================================

using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Api.Filters;

/// <summary>
/// 数据权限过滤器
/// </summary>
public class LeanDataPermissionFilter : IAsyncActionFilter
{
    /// <summary>
    /// 执行过滤器
    /// </summary>
    /// <param name="context">上下文</param>
    /// <param name="next">委托</param>
    /// <returns>任务</returns>
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // 获取当前用户信息
        var user = context.HttpContext.User;
        if (user.Identity?.IsAuthenticated != true)
        {
            await next();
            return;
        }

        // 获取用户数据权限范围
        var dataScopeClaim = user.FindFirst("DataScope")?.Value;
        if (string.IsNullOrEmpty(dataScopeClaim) || !Enum.TryParse<LeanDataScopeEnum>(dataScopeClaim, true, out var dataScope))
        {
            await next();
            return;
        }

        // 获取用户部门ID
        var deptId = user.FindFirst("DeptId")?.Value;
        var userId = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        // 设置数据权限过滤条件
        if (context.ActionArguments.FirstOrDefault().Value is IDataPermission dataPermission)
        {
            switch (dataScope)
            {
                case LeanDataScopeEnum.All:
                    // 全部数据权限，不需要设置过滤条件
                    break;

                case LeanDataScopeEnum.Custom:
                    // 自定义数据权限
                    dataPermission.DeptIds = await GetCustomDeptIdsAsync(userId);
                    break;

                case LeanDataScopeEnum.Dept:
                    // 本部门数据权限
                    dataPermission.DeptId = deptId;
                    break;

                case LeanDataScopeEnum.DeptAndChild:
                    // 本部门及以下数据权限
                    dataPermission.DeptIds = await GetDeptAndChildIdsAsync(deptId);
                    break;

                case LeanDataScopeEnum.Self:
                    // 仅本人数据权限
                    dataPermission.UserId = userId;
                    break;
            }
        }

        await next();
    }

    /// <summary>
    /// 获取部门及子部门ID列表
    /// </summary>
    /// <param name="deptId">部门ID</param>
    /// <returns>部门ID列表</returns>
    private async Task<List<string>> GetDeptAndChildIdsAsync(string deptId)
    {
        // TODO: 从数据库或缓存中获取部门及其子部门ID列表
        // 这里应该调用实际的数据访问层或缓存服务
        await Task.CompletedTask; // 添加await以确保异步操作
        return new List<string> { deptId };
    }

    /// <summary>
    /// 获取自定义数据权限部门ID列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>部门ID列表</returns>
    private async Task<List<string>> GetCustomDeptIdsAsync(string userId)
    {
        // TODO: 从数据库或缓存中获取用户自定义数据权限的部门ID列表
        // 这里应该调用实际的数据访问层或缓存服务
        await Task.CompletedTask; // 添加await以确保异步操作
        return new List<string>();
    }
}

/// <summary>
/// 数据权限接口
/// </summary>
public interface IDataPermission
{
    /// <summary>
    /// 用户ID
    /// </summary>
    string UserId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    string DeptId { get; set; }

    /// <summary>
    /// 部门ID列表
    /// </summary>
    List<string> DeptIds { get; set; }
} 
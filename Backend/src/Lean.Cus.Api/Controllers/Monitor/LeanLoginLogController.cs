//===================================================
// 项目名称：Lean.Cus.Api.Controllers.Monitor
// 文件名称：LeanLoginLogController
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：登录日志控制器
//===================================================

using Lean.Cus.Application.Dtos.Monitor;
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Exceptions;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Results;
using Lean.Cus.Domain.Entities.Monitor;
using Lean.Cus.Domain.IRepositories.Monitor;
using Lean.Cus.Api.Authorization;
using Lean.Cus.Api.Attributes;
using Microsoft.AspNetCore.Mvc;
using SqlSugar;

namespace Lean.Cus.Api.Controllers.Monitor;

/// <summary>
/// 登录日志控制器
/// </summary>
[ApiController]
[Route("api/monitor/[controller]")]
[LeanAuthorize("Monitor")]
public class LeanLoginLogController : ControllerBase
{
    private readonly ISqlSugarClient _db;
    private readonly ILeanLoginLogRepository _loginLogRepository;
    private readonly ILogger<LeanLoginLogController> _logger;

    public LeanLoginLogController(
        ISqlSugarClient db,
        ILeanLoginLogRepository loginLogRepository,
        ILogger<LeanLoginLogController> logger)
    {
        _db = db;
        _loginLogRepository = loginLogRepository;
        _logger = logger;
    }

    /// <summary>
    /// 获取登录日志分页列表
    /// </summary>
    [HttpGet("page")]
    [LeanPermission("monitor:loginlog:query")]
    [LeanOperLog("查询登录日志", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<LeanPageResult<LeanLoginLogBaseDto>>> GetPage([FromQuery] LeanLoginLogQueryDto query)
    {
        if (query == null || query.Current < 1 || query.Size < 1)
        {
            throw new LeanException("分页参数错误", LeanErrorCodeEnum.ValidationError);
        }

        RefAsync<int> total = 0;
        var records = await _db.Queryable<LeanLoginLog>()
            .WhereIF(!string.IsNullOrEmpty(query.UserName), l => 
                !string.IsNullOrEmpty(l.UserName) && l.UserName.Contains(query.UserName))
            .WhereIF(!string.IsNullOrEmpty(query.IpAddr), l => 
                !string.IsNullOrEmpty(l.IpAddr) && l.IpAddr.Contains(query.IpAddr))
            .WhereIF(query.Status.HasValue, l => l.Status == query.Status.Value)
            .WhereIF(query.StartTime.HasValue, l => l.LoginTime >= query.StartTime.Value)
            .WhereIF(query.EndTime.HasValue, l => l.LoginTime <= query.EndTime.Value)
            .OrderByDescending(l => l.LoginTime)
            .Select<LeanLoginLogBaseDto>()
            .ToPageListAsync(query.Current, query.Size, total);

        return LeanApiResult<LeanPageResult<LeanLoginLogBaseDto>>.Ok(new LeanPageResult<LeanLoginLogBaseDto>
        {
            Total = total,
            Records = records
        });
    }

    /// <summary>
    /// 删除登录日志
    /// </summary>
    [HttpDelete("{ids}")]
    [LeanPermission("monitor:loginlog:delete")]
    [LeanOperLog("删除登录日志", LeanBusinessTypeEnum.Delete)]
    public async Task<LeanApiResult<bool>> Delete(string ids)
    {
        if (string.IsNullOrEmpty(ids))
        {
            throw new LeanException("日志ID不能为空", LeanErrorCodeEnum.ValidationError);
        }

        var idArray = ids.Split(',').Select(long.Parse).ToArray();
        var success = await _loginLogRepository.DeleteAsync(idArray);

        if (!success)
        {
            throw new LeanException("删除登录日志失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }

    /// <summary>
    /// 清空登录日志
    /// </summary>
    [HttpDelete("clear")]
    [LeanPermission("monitor:loginlog:clean")]
    [LeanOperLog("清空登录日志", LeanBusinessTypeEnum.Clean)]
    public async Task<LeanApiResult<bool>> Clear()
    {
        var success = await _loginLogRepository.ClearAsync();

        if (!success)
        {
            throw new LeanException("清空登录日志失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }
}
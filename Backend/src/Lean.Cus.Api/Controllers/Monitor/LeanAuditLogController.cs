//===================================================
// 项目名称：Lean.Cus.Api.Controllers.Monitor
// 文件名称：LeanAuditLogController
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：审核日志控制器
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
/// 审核日志控制器
/// </summary>
[ApiController]
[Route("api/monitor/[controller]")]
[LeanAuthorize("Monitor")]
public class LeanAuditLogController : ControllerBase
{
    private readonly ISqlSugarClient _db;
    private readonly ILeanAuditLogRepository _auditLogRepository;
    private readonly ILogger<LeanAuditLogController> _logger;

    public LeanAuditLogController(
        ISqlSugarClient db,
        ILeanAuditLogRepository auditLogRepository,
        ILogger<LeanAuditLogController> logger)
    {
        _db = db;
        _auditLogRepository = auditLogRepository;
        _logger = logger;
    }

    /// <summary>
    /// 获取审核日志分页列表
    /// </summary>
    [HttpGet("page")]
    [LeanPermission("monitor:auditlog:query")]
    [LeanOperLog("查询审核日志", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<LeanPageResult<LeanAuditLogBaseDto>>> GetPage([FromQuery] LeanAuditLogQueryDto query)
    {
        if (query == null || query.Current < 1 || query.Size < 1)
        {
            throw new LeanException("分页参数错误", LeanErrorCodeEnum.ValidationError);
        }

        RefAsync<int> total = 0;
        var records = await _db.Queryable<LeanAuditLog>()
            .WhereIF(!string.IsNullOrEmpty(query.Username), l => 
                !string.IsNullOrEmpty(l.Username) && l.Username.Contains(query.Username))
            .WhereIF(!string.IsNullOrEmpty(query.Command), l => 
                !string.IsNullOrEmpty(l.Command) && l.Command.Contains(query.Command))
            .WhereIF(query.Status.HasValue, l => l.Status == query.Status.Value)
            .WhereIF(query.StartTime.HasValue, l => l.Timestamp >= query.StartTime.Value)
            .WhereIF(query.EndTime.HasValue, l => l.Timestamp <= query.EndTime.Value)
            .OrderByDescending(l => l.Timestamp)
            .Select<LeanAuditLogBaseDto>()
            .ToPageListAsync(query.Current, query.Size, total);

        return LeanApiResult<LeanPageResult<LeanAuditLogBaseDto>>.Ok(new LeanPageResult<LeanAuditLogBaseDto>
        {
            Total = total,
            Records = records
        });
    }

    /// <summary>
    /// 删除审核日志
    /// </summary>
    [HttpDelete("{ids}")]
    [LeanPermission("monitor:auditlog:delete")]
    [LeanOperLog("删除审核日志", LeanBusinessTypeEnum.Delete)]
    public async Task<LeanApiResult<bool>> Delete(string ids)
    {
        if (string.IsNullOrEmpty(ids))
        {
            throw new LeanException("日志ID不能为空", LeanErrorCodeEnum.ValidationError);
        }

        var idArray = ids.Split(',').Select(long.Parse).ToArray();
        var success = await _auditLogRepository.DeleteAsync(idArray);

        if (!success)
        {
            throw new LeanException("删除审核日志失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }

    /// <summary>
    /// 清空审核日志
    /// </summary>
    [HttpDelete("clear")]
    [LeanPermission("monitor:auditlog:clean")]
    [LeanOperLog("清空审核日志", LeanBusinessTypeEnum.Clean)]
    public async Task<LeanApiResult<bool>> Clear()
    {
        var success = await _auditLogRepository.ClearAsync();

        if (!success)
        {
            throw new LeanException("清空审核日志失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }
} 
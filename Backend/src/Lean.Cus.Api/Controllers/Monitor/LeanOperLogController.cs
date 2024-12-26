//===================================================
// 项目名称：Lean.Cus.Api.Controllers.Monitor
// 文件名称：LeanOperLogController
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：操作日志控制器
//===================================================

using Microsoft.AspNetCore.Mvc;
using Lean.Cus.Api.Authorization;
using Lean.Cus.Api.Attributes;
using Lean.Cus.Common.Models;
using Lean.Cus.Domain.Entities.Monitor;
using Lean.Cus.Common.Excel;
using Lean.Cus.Common.Enums;
using Lean.Cus.Application.Dtos.Monitor;
using Lean.Cus.Common.Results;
using Lean.Cus.Common.Exceptions;
using SqlSugar;

namespace Lean.Cus.Api.Controllers.Monitor;

/// <summary>
/// 操作日志控制器
/// </summary>
[ApiController]
[Route("api/monitor/operlog")]
[LeanAuthorize("Monitor")]
public class LeanOperLogController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public LeanOperLogController(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 获取操作日志分页列表
    /// </summary>
    [HttpGet("page")]
    [LeanPermission("monitor:operlog:query")]
    [LeanOperLog("查询操作日志", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<LeanPageResult<LeanOperLogDto>>> GetPage([FromQuery] LeanOperLogQueryDto query)
    {
        if (query == null || query.Current < 1 || query.Size < 1)
        {
            throw new LeanException("分页参数错误", LeanErrorCodeEnum.ValidationError);
        }

        RefAsync<int> total = 0;
        var records = await _db.Queryable<LeanOperLog>()
            .WhereIF(!string.IsNullOrEmpty(query.Title), l => 
                !string.IsNullOrEmpty(l.Title) && l.Title.Contains(query.Title))
            .WhereIF(!string.IsNullOrEmpty(query.OperName), l => 
                !string.IsNullOrEmpty(l.OperName) && l.OperName.Contains(query.OperName))
            .WhereIF(query.BusinessType.HasValue, l => l.BusinessType == (int)query.BusinessType)
            .WhereIF(query.Status.HasValue, l => l.Status == query.Status.Value)
            .WhereIF(query.StartTime.HasValue, l => l.OperTime >= query.StartTime.Value)
            .WhereIF(query.EndTime.HasValue, l => l.OperTime <= query.EndTime.Value)
            .OrderByDescending(l => l.OperTime)
            .Select<LeanOperLogDto>()
            .ToPageListAsync(query.Current, query.Size, total);

        return LeanApiResult<LeanPageResult<LeanOperLogDto>>.Ok(new LeanPageResult<LeanOperLogDto>
        {
            Total = total,
            Records = records
        });
    }

    /// <summary>
    /// 获取操作日志详情
    /// </summary>
    [HttpGet("{id}")]
    [LeanPermission("monitor:operlog:query")]
    [LeanOperLog("查询操作日志详情", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<LeanOperLog>> Get(long id)
    {
        if (id < 1)
        {
            throw new LeanException("ID不能小于1", LeanErrorCodeEnum.ValidationError);
        }

        var operLog = await _db.Queryable<LeanOperLog>()
            .Where(u => u.OperId == id)
            .FirstAsync();
            
        if (operLog == null)
        {
            throw new LeanException("操作日志不存在", LeanErrorCodeEnum.NotFound);
        }

        return LeanApiResult<LeanOperLog>.Ok(operLog);
    }

    /// <summary>
    /// 删除操作日志
    /// </summary>
    [HttpDelete("{id}")]
    [LeanPermission("monitor:operlog:delete")]
    [LeanOperLog("删除操作日志", LeanBusinessTypeEnum.Delete)]
    public async Task<LeanApiResult<bool>> Delete(long id)
    {
        if (id < 1)
        {
            throw new LeanException("ID不能小于1", LeanErrorCodeEnum.ValidationError);
        }

        var exists = await _db.Queryable<LeanOperLog>().AnyAsync(u => u.OperId == id);
        if (!exists)
        {
            throw new LeanException("操作日志不存在", LeanErrorCodeEnum.NotFound);
        }

        var success = await _db.Deleteable<LeanOperLog>().Where(u => u.OperId == id).ExecuteCommandAsync() > 0;
        if (!success)
        {
            throw new LeanException("删除操作日志失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }

    /// <summary>
    /// 清空操作日志
    /// </summary>
    [HttpDelete("clean")]
    [LeanPermission("monitor:operlog:clean")]
    [LeanOperLog("清空操作日志", LeanBusinessTypeEnum.Clean)]
    public async Task<LeanApiResult<bool>> Clean()
    {
        var success = await _db.Deleteable<LeanOperLog>().ExecuteCommandAsync() > 0;
        if (!success)
        {
            throw new LeanException("清空操作日志失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }

    /// <summary>
    /// 导出操作日志
    /// </summary>
    [HttpGet("export")]
    [LeanPermission("monitor:operlog:export")]
    [LeanOperLog("导出操作日志", LeanBusinessTypeEnum.Export)]
    public async Task<IActionResult> Export([FromQuery] LeanOperLogDto query)
    {
        var list = await _db.Queryable<LeanOperLog>()
            .WhereIF(!string.IsNullOrEmpty(query.Title), u => u.Title.Contains(query.Title))
            .WhereIF(!string.IsNullOrEmpty(query.OperName), u => u.OperName.Contains(query.OperName))
            .WhereIF(query.BusinessType.HasValue, u => u.BusinessType == (int)query.BusinessType.Value)
            .WhereIF(query.Status.HasValue, u => u.Status == query.Status.Value)
            .WhereIF(query.StartTime.HasValue, u => u.OperTime >= query.StartTime.Value)
            .WhereIF(query.EndTime.HasValue, u => u.OperTime <= query.EndTime.Value)
            .OrderByDescending(u => u.OperTime)
            .ToListAsync();

        if (!list.Any())
        {
            throw new LeanException("没有数据可导出", LeanErrorCodeEnum.ValidationError);
        }

        var excelBytes = LeanExcelHelper.ExportExcel(list, "操作日志");
        return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "操作日志.xlsx");
    }
} 
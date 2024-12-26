//===================================================
// 项目名称：Lean.Cus.Api.Controllers.Monitor
// 文件名称：LeanSqlDiffLogController
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：SQL差异日志控制器
//===================================================

using Lean.Cus.Api.Authorization;
using Lean.Cus.Api.Attributes;
using Lean.Cus.Application.Dtos.Monitor;
using Lean.Cus.Application.Services.Monitor;
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Exceptions;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Results;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lean.Cus.Api.Controllers.Monitor;

/// <summary>
/// SQL差异日志控制器
/// </summary>
[ApiController]
[Route("api/monitor/[controller]")]
public class LeanSqlDiffLogController : ControllerBase
{
    private readonly ILeanSqlDiffLogService _sqlDiffLogService;

    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanSqlDiffLogController(ILeanSqlDiffLogService sqlDiffLogService)
    {
        _sqlDiffLogService = sqlDiffLogService;
    }

    /// <summary>
    /// 获取SQL差异日志分页列表
    /// </summary>
    [HttpGet("page")]
    [LeanPermission("monitor:sqldifflog:query")]
    [LeanOperLog("查询SQL差异日志", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<LeanPageResult<LeanSqlDiffLogDto>>> GetPageList([FromQuery] LeanSqlDiffLogQueryDto queryDto)
    {
        var result = await _sqlDiffLogService.GetPageListAsync(queryDto);
        return LeanApiResult<LeanPageResult<LeanSqlDiffLogDto>>.Ok(result);
    }

    /// <summary>
    /// 获取SQL差异日志详情
    /// </summary>
    [HttpGet("{id}")]
    [LeanPermission("monitor:sqldifflog:query")]
    [LeanOperLog("查询SQL差异日志��情", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<LeanSqlDiffLogDto>> GetById(long id)
    {
        if (id <= 0)
        {
            throw new LeanException("ID不能小于1", LeanErrorCodeEnum.ValidationError);
        }

        var log = await _sqlDiffLogService.GetByIdAsync(id);
        if (log == null)
        {
            throw new LeanException("日志不存在", LeanErrorCodeEnum.NotFound);
        }

        return LeanApiResult<LeanSqlDiffLogDto>.Ok(log);
    }

    /// <summary>
    /// 删除SQL差异日志
    /// </summary>
    [HttpDelete("{id}")]
    [LeanPermission("monitor:sqldifflog:delete")]
    [LeanOperLog("删除SQL差异日志", LeanBusinessTypeEnum.Delete)]
    public async Task<LeanApiResult<bool>> Delete(long id)
    {
        if (id <= 0)
        {
            throw new LeanException("ID不能小于1", LeanErrorCodeEnum.ValidationError);
        }

        var success = await _sqlDiffLogService.DeleteAsync(id);
        if (!success)
        {
            throw new LeanException("删除失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }

    /// <summary>
    /// 批量删除SQL差异日志
    /// </summary>
    [HttpPost("batch-delete")]
    [LeanPermission("monitor:sqldifflog:delete")]
    [LeanOperLog("批量删除SQL差异日志", LeanBusinessTypeEnum.Delete)]
    public async Task<LeanApiResult<bool>> BatchDelete([FromBody] long[] ids)
    {
        if (ids == null || ids.Length == 0)
        {
            throw new LeanException("ID列表不能为空", LeanErrorCodeEnum.ValidationError);
        }

        var success = await _sqlDiffLogService.BatchDeleteAsync(ids);
        if (!success)
        {
            throw new LeanException("批量删除失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }

    /// <summary>
    /// 清空SQL差异日志
    /// </summary>
    [HttpPost("clear")]
    [LeanPermission("monitor:sqldifflog:delete")]
    [LeanOperLog("清空SQL差异日志", LeanBusinessTypeEnum.Delete)]
    public async Task<LeanApiResult<bool>> Clear()
    {
        var success = await _sqlDiffLogService.ClearAllAsync();
        if (!success)
        {
            throw new LeanException("清空失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }
} 
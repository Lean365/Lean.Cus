using Lean.Cus.Application.Interfaces.Log;
using Lean.Cus.Application.Dtos.Log;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Common.Paging;
using Microsoft.AspNetCore.Authorization;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Api.Controllers.Log;

/// <summary>
/// 审计日志控制器
/// </summary>
[Route("api/log/audit")]
[ApiController]
[Authorize]
public class LeanAuditLogController : ControllerBase
{
    private readonly ILeanAuditLogService _auditLogService;

    /// <summary>
    /// 初始化审计日志控制器
    /// </summary>
    /// <param name="auditLogService">审计日志服务</param>
    public LeanAuditLogController(ILeanAuditLogService auditLogService)
    {
        _auditLogService = auditLogService;
    }

    /// <summary>
    /// 新增审计日志
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<LeanAuditLogDto>>> Add([FromBody] LeanAuditLogDto dto)
    {
        var result = await _auditLogService.CreateAsync(dto);
        return Ok(LeanApiResult<LeanAuditLogDto>.Ok(result));
    }

    /// <summary>
    /// 删除审计日志
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> Delete(long id)
    {
        var result = await _auditLogService.DeleteAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取审计日志
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanAuditLogDto>>> Get(long id)
    {
        var result = await _auditLogService.GetAsync(id);
        return Ok(LeanApiResult<LeanAuditLogDto>.Ok(result));
    }

    /// <summary>
    /// 获取审计日志列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanAuditLogDto>>>> GetList([FromQuery] LeanAuditLogQueryDto query)
    {
        var result = await _auditLogService.GetListAsync(query);
        return Ok(LeanApiResult<List<LeanAuditLogDto>>.Ok(result));
    }

    /// <summary>
    /// 分页查询审计日志
    /// </summary>
    [HttpGet("page")]
    public async Task<ActionResult<LeanApiResult<PagedResults<LeanAuditLogDto>>>> GetPageList([FromQuery] LeanAuditLogQueryDto query)
    {
        var result = await _auditLogService.GetPagedListAsync(query);
        return Ok(LeanApiResult<PagedResults<LeanAuditLogDto>>.Ok(result));
    }

    /// <summary>
    /// 导出审计日志数据
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] LeanAuditLogQueryDto query)
    {
        var result = await _auditLogService.ExportAsync(query);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "audit_logs.xlsx");
    }

    /// <summary>
    /// 清空审计日志
    /// </summary>
    [HttpPost("clear")]
    public async Task<ActionResult<LeanApiResult>> Clear()
    {
        var result = await _auditLogService.ClearAsync();
        return Ok(result ? LeanApiResult.Ok("清空成功") : LeanApiResult.Fail("清空失败"));
    }
} 
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
/// SQL日志控制器
/// </summary>
[Route("api/log/sql")]
[ApiController]
[Authorize]
public class LeanSqlLogController : ControllerBase
{
    private readonly ILeanSqlLogService _sqlLogService;

    /// <summary>
    /// 初始化SQL日志控制器
    /// </summary>
    /// <param name="sqlLogService">SQL日志服务</param>
    public LeanSqlLogController(ILeanSqlLogService sqlLogService)
    {
        _sqlLogService = sqlLogService;
    }

    /// <summary>
    /// 新增SQL日志
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<LeanSqlLogDto>>> Add([FromBody] LeanSqlLogCreateDto dto)
    {
        var result = await _sqlLogService.CreateAsync(dto);
        return Ok(LeanApiResult<LeanSqlLogDto>.Ok(result));
    }

    /// <summary>
    /// 删除SQL日志
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> Delete(long id)
    {
        var result = await _sqlLogService.DeleteAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取SQL日志
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanSqlLogDto>>> Get(long id)
    {
        var result = await _sqlLogService.GetAsync(id);
        return Ok(LeanApiResult<LeanSqlLogDto>.Ok(result));
    }

    /// <summary>
    /// 获取SQL日志列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanSqlLogDto>>>> GetList([FromQuery] LeanSqlLogQueryDto query)
    {
        var result = await _sqlLogService.GetListAsync(query);
        return Ok(LeanApiResult<List<LeanSqlLogDto>>.Ok(result));
    }

    /// <summary>
    /// 分页查询SQL日志
    /// </summary>
    [HttpGet("page")]
    public async Task<ActionResult<LeanApiResult<PagedResults<LeanSqlLogDto>>>> GetPageList([FromQuery] LeanSqlLogQueryDto query)
    {
        var result = await _sqlLogService.GetPagedListAsync(query);
        return Ok(LeanApiResult<PagedResults<LeanSqlLogDto>>.Ok(result));
    }

    /// <summary>
    /// 导出SQL日志数据
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] LeanSqlLogQueryDto query)
    {
        var result = await _sqlLogService.ExportAsync(query);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "sql_logs.xlsx");
    }

    /// <summary>
    /// 清空SQL日志
    /// </summary>
    [HttpPost("clear")]
    public async Task<ActionResult<LeanApiResult>> Clear()
    {
        var result = await _sqlLogService.ClearAsync();
        return Ok(result ? LeanApiResult.Ok("清空成功") : LeanApiResult.Fail("清空失败"));
    }
} 
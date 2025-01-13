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
/// 错误日志控制器
/// </summary>
[Route("api/log/error")]
[ApiController]
[Authorize]
public class LeanErrorLogController : ControllerBase
{
    private readonly ILeanErrorLogService _errorLogService;

    /// <summary>
    /// 初始化错误日志控制器
    /// </summary>
    /// <param name="errorLogService">错误日志服务</param>
    public LeanErrorLogController(ILeanErrorLogService errorLogService)
    {
        _errorLogService = errorLogService;
    }

    /// <summary>
    /// 新增错误日志
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<LeanErrorLogDto>>> Add([FromBody] LeanErrorLogDto dto)
    {
        var result = await _errorLogService.CreateAsync(dto);
        return Ok(LeanApiResult<LeanErrorLogDto>.Ok(result));
    }

    /// <summary>
    /// 删除错误日志
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> Delete(long id)
    {
        var result = await _errorLogService.DeleteAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取错误日志
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanErrorLogDto>>> Get(long id)
    {
        var result = await _errorLogService.GetAsync(id);
        return Ok(LeanApiResult<LeanErrorLogDto>.Ok(result));
    }

    /// <summary>
    /// 获取错误日志列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanErrorLogDto>>>> GetList([FromQuery] LeanErrorLogQueryDto query)
    {
        var result = await _errorLogService.GetListAsync(query);
        return Ok(LeanApiResult<List<LeanErrorLogDto>>.Ok(result));
    }

    /// <summary>
    /// 分页查询错误日志
    /// </summary>
    [HttpGet("page")]
    public async Task<ActionResult<LeanApiResult<PagedResults<LeanErrorLogDto>>>> GetPageList([FromQuery] LeanErrorLogQueryDto query)
    {
        var result = await _errorLogService.GetPagedListAsync(query);
        return Ok(LeanApiResult<PagedResults<LeanErrorLogDto>>.Ok(result));
    }

    /// <summary>
    /// 导出错误日志数据
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] LeanErrorLogQueryDto query)
    {
        var result = await _errorLogService.ExportAsync(query);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "error_logs.xlsx");
    }

    /// <summary>
    /// 清空错误日志
    /// </summary>
    [HttpPost("clear")]
    public async Task<ActionResult<LeanApiResult>> Clear()
    {
        var result = await _errorLogService.ClearAsync();
        return Ok(result ? LeanApiResult.Ok("清空成功") : LeanApiResult.Fail("清空失败"));
    }
} 
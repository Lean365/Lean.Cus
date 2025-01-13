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
/// 登录日志控制器
/// </summary>
[Route("api/log/login")]
[ApiController]
[Authorize]
public class LeanLoginLogController : ControllerBase
{
    private readonly ILeanLoginLogService _loginLogService;

    /// <summary>
    /// 初始化登录日志控制器
    /// </summary>
    /// <param name="loginLogService">登录日志服务</param>
    public LeanLoginLogController(ILeanLoginLogService loginLogService)
    {
        _loginLogService = loginLogService;
    }

    /// <summary>
    /// 新增登录日志
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<LeanLoginLogDto>>> Add([FromBody] LeanLoginLogDto dto)
    {
        var result = await _loginLogService.CreateAsync(dto);
        return Ok(LeanApiResult<LeanLoginLogDto>.Ok(result));
    }

    /// <summary>
    /// 删除登录日志
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> Delete(long id)
    {
        var result = await _loginLogService.DeleteAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取登录日志
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanLoginLogDto>>> Get(long id)
    {
        var result = await _loginLogService.GetAsync(id);
        return Ok(LeanApiResult<LeanLoginLogDto>.Ok(result));
    }

    /// <summary>
    /// 获取登录日志列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanLoginLogDto>>>> GetList([FromQuery] LeanLoginLogQueryDto query)
    {
        var result = await _loginLogService.GetListAsync(query);
        return Ok(LeanApiResult<List<LeanLoginLogDto>>.Ok(result));
    }

    /// <summary>
    /// 分页查询登录日志
    /// </summary>
    [HttpGet("page")]
    public async Task<ActionResult<LeanApiResult<PagedResults<LeanLoginLogDto>>>> GetPageList([FromQuery] LeanLoginLogQueryDto query)
    {
        var result = await _loginLogService.GetPagedListAsync(query);
        return Ok(LeanApiResult<PagedResults<LeanLoginLogDto>>.Ok(result));
    }

    /// <summary>
    /// 导出登录日志数据
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] LeanLoginLogQueryDto query)
    {
        var result = await _loginLogService.ExportAsync(query);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "login_logs.xlsx");
    }

    /// <summary>
    /// 清空登录日志
    /// </summary>
    [HttpPost("clear")]
    public async Task<ActionResult<LeanApiResult>> Clear()
    {
        var result = await _loginLogService.ClearAsync();
        return Ok(result ? LeanApiResult.Ok("清空成功") : LeanApiResult.Fail("清空失败"));
    }
} 
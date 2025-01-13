using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Application.Dtos.Admin;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 职位管理控制器
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeanPositionController : ControllerBase
{
    private readonly ILeanPositionService _positionService;

    /// <summary>
    /// 初始化职位控制器
    /// </summary>
    public LeanPositionController(ILeanPositionService positionService)
    {
        _positionService = positionService;
    }

    /// <summary>
    /// 新增职位
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<LeanPositionDto>>> Add([FromBody] LeanPositionDto dto)
    {
        var result = await _positionService.CreateAsync(dto);
        return Ok(LeanApiResult<LeanPositionDto>.Ok(result));
    }

    /// <summary>
    /// 更新职位
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<LeanApiResult>> Update([FromBody] LeanPositionDto dto)
    {
        var result = await _positionService.UpdateAsync(dto);
        return Ok(result ? LeanApiResult.Ok("更新成功") : LeanApiResult.Fail("更新失败"));
    }

    /// <summary>
    /// 删除职位
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> Delete(long id)
    {
        var result = await _positionService.DeleteAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取职位
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanPositionDto>>> Get(long id)
    {
        var result = await _positionService.GetAsync(id);
        return Ok(LeanApiResult<LeanPositionDto>.Ok(result));
    }

    /// <summary>
    /// 获取职位列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanPositionDto>>>> GetList([FromQuery] LeanPositionQueryDto queryDto)
    {
        var result = await _positionService.GetListAsync(queryDto);
        return Ok(LeanApiResult<List<LeanPositionDto>>.Ok(result));
    }

    /// <summary>
    /// 分页查询职位
    /// </summary>
    [HttpGet("paged")]
    public async Task<ActionResult<LeanApiResult<PagedResults<LeanPositionDto>>>> GetPagedList([FromQuery] LeanPositionQueryDto queryDto)
    {
        var result = await _positionService.GetPagedListAsync(queryDto);
        return Ok(LeanApiResult<PagedResults<LeanPositionDto>>.Ok(result));
    }

    /// <summary>
    /// 导入职位数据
    /// </summary>
    [HttpPost("import")]
    public async Task<ActionResult<LeanApiResult<(List<LeanPositionDto> SuccessItems, List<string> ErrorMessages)>>> Import([FromForm] IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var result = await _positionService.ImportAsync(ms.ToArray());
        return Ok(LeanApiResult<(List<LeanPositionDto> SuccessItems, List<string> ErrorMessages)>.Ok(result));
    }

    /// <summary>
    /// 导出职位数据
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] LeanPositionQueryDto queryDto)
    {
        var result = await _positionService.ExportAsync(queryDto);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "positions.xlsx");
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("import-template")]
    public async Task<IActionResult> GetImportTemplate()
    {
        var result = await _positionService.GetImportTemplateAsync();
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "position_import_template.xlsx");
    }

    /// <summary>
    /// 获取用户职位列表
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<List<LeanPositionDto>>> GetUserPositions(long userId)
    {
        var result = await _positionService.GetUserPositionsAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// 更新职位状态
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<LeanApiResult>> UpdateStatus(long id, [FromBody] LeanPositionStatusUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(LeanApiResult.Fail("职位ID不匹配"));
        }
        var result = await _positionService.UpdateStatusAsync(dto);
        return Ok(result ? LeanApiResult.Ok("更新状态成功") : LeanApiResult.Fail("更新状态失败"));
    }
} 
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Generator.Dtos.Generator;
using Lean.Cus.Generator.IServices.Generator;
using Microsoft.AspNetCore.Mvc;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Api.Controllers.Generator;

/// <summary>
/// 模板控制器
/// </summary>
[Route("api/generator/template")]
[ApiController]
public class LeanTemplateController : ControllerBase
{
    private readonly ILeanTemplateService _templateService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="templateService">模板服务</param>
    public LeanTemplateController(ILeanTemplateService templateService)
    {
        _templateService = templateService;
    }

    /// <summary>
    /// 创建模板
    /// </summary>
    /// <param name="dto">模板DTO</param>
    /// <returns>创建结果</returns>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult>> CreateTemplate([FromBody] LeanTemplateDto dto)
    {
        var result = await _templateService.CreateTemplateAsync(dto);
        return Ok(result ? LeanApiResult.Ok("创建成功") : LeanApiResult.Fail("创建失败"));
    }

    /// <summary>
    /// 更新模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <param name="dto">模板DTO</param>
    /// <returns>更新结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<LeanApiResult>> UpdateTemplate([FromRoute] long id, [FromBody] LeanTemplateDto dto)
    {
        var result = await _templateService.UpdateTemplateAsync(id, dto);
        return Ok(result ? LeanApiResult.Ok("更新成功") : LeanApiResult.Fail("更新失败"));
    }

    /// <summary>
    /// 删除模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <returns>删除结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> DeleteTemplate([FromRoute] long id)
    {
        var result = await _templateService.DeleteTemplateAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <returns>模板DTO</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanTemplateDto>>> GetTemplate([FromRoute] long id)
    {
        var result = await _templateService.GetTemplateAsync(id);
        return Ok(LeanApiResult<LeanTemplateDto>.Ok(result));
    }

    /// <summary>
    /// 获取模板列表
    /// </summary>
    /// <param name="templateType">模板类型</param>
    /// <returns>模板DTO列表</returns>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanTemplateDto>>>> GetTemplateList([FromQuery] string templateType)
    {
        var result = await _templateService.GetTemplateListAsync(templateType);
        return Ok(LeanApiResult<List<LeanTemplateDto>>.Ok(result));
    }

    /// <summary>
    /// 预览模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <param name="tableId">表编号</param>
    /// <returns>预览结果</returns>
    [HttpGet("{id}/preview")]
    public async Task<string> PreviewTemplate([FromRoute] long id, [FromQuery] long tableId)
    {
        return await _templateService.PreviewTemplateAsync(id, tableId);
    }
} 
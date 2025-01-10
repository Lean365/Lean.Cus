using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Generator.Dtos.Generator;
using Lean.Cus.Generator.IServices.Generator;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<bool> CreateTemplate([FromBody] LeanTemplateDto dto)
    {
        return await _templateService.CreateTemplateAsync(dto);
    }

    /// <summary>
    /// 更新模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <param name="dto">模板DTO</param>
    /// <returns>更新结果</returns>
    [HttpPut("{id}")]
    public async Task<bool> UpdateTemplate([FromRoute] long id, [FromBody] LeanTemplateDto dto)
    {
        return await _templateService.UpdateTemplateAsync(id, dto);
    }

    /// <summary>
    /// 删除模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <returns>删除结果</returns>
    [HttpDelete("{id}")]
    public async Task<bool> DeleteTemplate([FromRoute] long id)
    {
        return await _templateService.DeleteTemplateAsync(id);
    }

    /// <summary>
    /// 获取模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <returns>模板DTO</returns>
    [HttpGet("{id}")]
    public async Task<LeanTemplateDto> GetTemplate([FromRoute] long id)
    {
        return await _templateService.GetTemplateAsync(id);
    }

    /// <summary>
    /// 获取模板列表
    /// </summary>
    /// <param name="templateType">模板类型</param>
    /// <returns>模板DTO列表</returns>
    [HttpGet]
    public async Task<List<LeanTemplateDto>> GetTemplateList([FromQuery] string templateType)
    {
        return await _templateService.GetTemplateListAsync(templateType);
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
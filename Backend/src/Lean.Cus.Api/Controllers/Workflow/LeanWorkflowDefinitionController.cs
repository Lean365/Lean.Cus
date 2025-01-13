using Lean.Cus.Workflow.Dtos.Definition;
using Lean.Cus.Workflow.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Api.Controllers.Workflow;

/// <summary>
/// 流程定义控制器
/// </summary>
[ApiController]
[Route("api/workflow/definition")]
public class LeanWorkflowDefinitionController : ControllerBase
{
    private readonly ILeanWorkflowDefinitionService _service;

    /// <summary>
    /// 初始化流程定义控制器
    /// </summary>
    /// <param name="service">流程定义服务</param>
    public LeanWorkflowDefinitionController(ILeanWorkflowDefinitionService service)
    {
        _service = service;
    }

    /// <summary>
    /// 创建流程定义
    /// </summary>
    /// <param name="dto">流程定义信息</param>
    /// <returns>流程定义ID</returns>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<long>>> CreateAsync([FromBody] LeanWorkflowDefinitionDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(LeanApiResult<long>.Ok(result));
    }

    /// <summary>
    /// 更新流程定义
    /// </summary>
    /// <param name="dto">流程定义信息</param>
    [HttpPut]
    public async Task<ActionResult<LeanApiResult>> UpdateAsync([FromBody] LeanWorkflowDefinitionDto dto)
    {
        await _service.UpdateAsync(dto);
        return Ok(LeanApiResult.Ok("更新成功"));
    }

    /// <summary>
    /// 删除流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> DeleteAsync(long id)
    {
        await _service.DeleteAsync(id);
        return Ok(LeanApiResult.Ok("删除成功"));
    }

    /// <summary>
    /// 获取流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    /// <returns>流程定义信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanWorkflowDefinitionDto>>> GetAsync(long id)
    {
        var result = await _service.GetAsync(id);
        return Ok(LeanApiResult<LeanWorkflowDefinitionDto>.Ok(result));
    }

    /// <summary>
    /// 获取流程定义列表
    /// </summary>
    /// <returns>流程定义列表</returns>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanWorkflowDefinitionDto>>>> GetListAsync()
    {
        var result = await _service.GetListAsync();
        return Ok(LeanApiResult<List<LeanWorkflowDefinitionDto>>.Ok(result));
    }

    /// <summary>
    /// 发布流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    [HttpPost("{id}/publish")]
    public async Task<ActionResult<LeanApiResult>> PublishAsync(long id)
    {
        await _service.PublishAsync(id);
        return Ok(LeanApiResult.Ok("发布成功"));
    }

    /// <summary>
    /// 停用流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    [HttpPost("{id}/disable")]
    public async Task<ActionResult<LeanApiResult>> DisableAsync(long id)
    {
        await _service.DisableAsync(id);
        return Ok(LeanApiResult.Ok("停用成功"));
    }
} 
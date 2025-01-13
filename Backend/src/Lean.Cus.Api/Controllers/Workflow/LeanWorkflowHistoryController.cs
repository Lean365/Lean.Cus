using Lean.Cus.Workflow.Dtos.History;
using Lean.Cus.Workflow.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Api.Controllers.Workflow;

/// <summary>
/// 流程历史记录控制器
/// </summary>
[ApiController]
[Route("api/workflow/history")]
public class LeanWorkflowHistoryController : ControllerBase
{
    private readonly ILeanWorkflowHistoryService _service;

    /// <summary>
    /// 初始化流程历史记录控制器
    /// </summary>
    /// <param name="service">流程历史记录服务</param>
    public LeanWorkflowHistoryController(ILeanWorkflowHistoryService service)
    {
        _service = service;
    }

    /// <summary>
    /// 添加历史记录
    /// </summary>
    /// <param name="dto">历史记录信息</param>
    /// <returns>历史记录ID</returns>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<long>>> CreateAsync([FromBody] LeanWorkflowHistoryDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(LeanApiResult<long>.Ok(result));
    }

    /// <summary>
    /// 获取历史记录
    /// </summary>
    /// <param name="id">历史记录ID</param>
    /// <returns>历史记录信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanWorkflowHistoryDto>>> GetAsync(long id)
    {
        var result = await _service.GetAsync(id);
        return Ok(LeanApiResult<LeanWorkflowHistoryDto>.Ok(result));
    }

    /// <summary>
    /// 获取流程实例的历史记录列表
    /// </summary>
    /// <param name="instanceId">流程实例ID</param>
    /// <returns>历史记录列表</returns>
    [HttpGet("instance/{instanceId}")]
    public async Task<ActionResult<LeanApiResult<List<LeanWorkflowHistoryDto>>>> GetListByInstanceAsync(long instanceId)
    {
        var result = await _service.GetListByInstanceAsync(instanceId);
        return Ok(LeanApiResult<List<LeanWorkflowHistoryDto>>.Ok(result));
    }
} 
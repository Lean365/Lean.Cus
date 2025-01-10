using Lean.Cus.Workflow.Dtos.History;
using Lean.Cus.Workflow.Services;
using Microsoft.AspNetCore.Mvc;

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
    public Task<long> AddAsync([FromBody] LeanWorkflowHistoryDto dto)
    {
        return _service.AddAsync(dto);
    }

    /// <summary>
    /// 获取历史记录
    /// </summary>
    /// <param name="id">历史记录ID</param>
    /// <returns>历史记录信息</returns>
    [HttpGet("{id}")]
    public Task<LeanWorkflowHistoryDto> GetAsync(long id)
    {
        return _service.GetAsync(id);
    }

    /// <summary>
    /// 获取流程实例的历史记录列表
    /// </summary>
    /// <param name="instanceId">流程实例ID</param>
    /// <returns>历史记录列表</returns>
    [HttpGet("instance/{instanceId}")]
    public Task<List<LeanWorkflowHistoryDto>> GetListByInstanceAsync(long instanceId)
    {
        return _service.GetListByInstanceAsync(instanceId);
    }
} 
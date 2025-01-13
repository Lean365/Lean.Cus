using Lean.Cus.Workflow.Dtos.Instance;
using Lean.Cus.Workflow.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Api.Controllers.Workflow;

/// <summary>
/// 流程实例控制器
/// </summary>
[ApiController]
[Route("api/workflow/instance")]
public class LeanWorkflowInstanceController : ControllerBase
{
    private readonly ILeanWorkflowInstanceService _service;

    /// <summary>
    /// 初始化流程实例控制器
    /// </summary>
    /// <param name="service">流程实例服务</param>
    public LeanWorkflowInstanceController(ILeanWorkflowInstanceService service)
    {
        _service = service;
    }

    /// <summary>
    /// 启动流程实例
    /// </summary>
    /// <param name="dto">流程实例信息</param>
    /// <returns>流程实例ID</returns>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<long>>> StartAsync([FromBody] LeanWorkflowInstanceDto dto)
    {
        var result = await _service.StartAsync(dto);
        return Ok(LeanApiResult<long>.Ok(result));
    }

    /// <summary>
    /// 终止流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    /// <param name="reason">终止原因</param>
    [HttpPost("{id}/terminate")]
    public async Task<ActionResult<LeanApiResult>> TerminateAsync(long id, [FromBody] string reason)
    {
        await _service.TerminateAsync(id, reason);
        return Ok(LeanApiResult.Ok("终止成功"));
    }

    /// <summary>
    /// 挂起流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    [HttpPost("{id}/suspend")]
    public Task SuspendAsync(long id)
    {
        return _service.SuspendAsync(id);
    }

    /// <summary>
    /// 恢复流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    [HttpPost("{id}/resume")]
    public Task ResumeAsync(long id)
    {
        return _service.ResumeAsync(id);
    }

    /// <summary>
    /// 获取流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    /// <returns>流程实例信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanWorkflowInstanceDto>>> GetAsync(long id)
    {
        var result = await _service.GetAsync(id);
        return Ok(LeanApiResult<LeanWorkflowInstanceDto>.Ok(result));
    }

    /// <summary>
    /// 获取流程实例列表
    /// </summary>
    /// <returns>流程实例列表</returns>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanWorkflowInstanceDto>>>> GetListAsync()
    {
        var result = await _service.GetListAsync();
        return Ok(LeanApiResult<List<LeanWorkflowInstanceDto>>.Ok(result));
    }

    /// <summary>
    /// 获取我发起的流程实例列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>流程实例列表</returns>
    [HttpGet("my-started")]
    public Task<List<LeanWorkflowInstanceDto>> GetMyStartedListAsync([FromQuery] long userId)
    {
        return _service.GetMyStartedListAsync(userId);
    }

    /// <summary>
    /// 获取我参与的流程实例列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>流程实例列表</returns>
    [HttpGet("my-involved")]
    public Task<List<LeanWorkflowInstanceDto>> GetMyInvolvedListAsync([FromQuery] long userId)
    {
        return _service.GetMyInvolvedListAsync(userId);
    }
} 
using Lean.Cus.Workflow.Dtos.Instance;
using Lean.Cus.Workflow.Services;
using Microsoft.AspNetCore.Mvc;

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
    public Task<long> StartAsync([FromBody] LeanWorkflowInstanceDto dto)
    {
        return _service.StartAsync(dto);
    }

    /// <summary>
    /// 终止流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    /// <param name="reason">终止原因</param>
    [HttpPost("{id}/terminate")]
    public Task TerminateAsync(long id, [FromBody] string reason)
    {
        return _service.TerminateAsync(id, reason);
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
    public Task<LeanWorkflowInstanceDto> GetAsync(long id)
    {
        return _service.GetAsync(id);
    }

    /// <summary>
    /// 获取流程实例列表
    /// </summary>
    /// <returns>流程实例列表</returns>
    [HttpGet]
    public Task<List<LeanWorkflowInstanceDto>> GetListAsync()
    {
        return _service.GetListAsync();
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
using Lean.Cus.Workflow.Dtos.Task;
using Lean.Cus.Workflow.Services;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Workflow;

/// <summary>
/// 流程任务控制器
/// </summary>
[ApiController]
[Route("api/workflow/task")]
public class LeanWorkflowTaskController : ControllerBase
{
    private readonly ILeanWorkflowTaskService _service;

    /// <summary>
    /// 初始化流程任务控制器
    /// </summary>
    /// <param name="service">流程任务服务</param>
    public LeanWorkflowTaskController(ILeanWorkflowTaskService service)
    {
        _service = service;
    }

    /// <summary>
    /// 完成任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="comment">处理意见</param>
    [HttpPost("{id}/complete")]
    public Task CompleteAsync(long id, [FromBody] string? comment)
    {
        return _service.CompleteAsync(id, comment);
    }

    /// <summary>
    /// 取消任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="reason">取消原因</param>
    [HttpPost("{id}/cancel")]
    public Task CancelAsync(long id, [FromBody] string reason)
    {
        return _service.CancelAsync(id, reason);
    }

    /// <summary>
    /// 转办任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="userId">转办人ID</param>
    /// <param name="userName">转办人名称</param>
    /// <param name="comment">转办说明</param>
    [HttpPost("{id}/transfer")]
    public Task TransferAsync(long id, [FromQuery] long userId, [FromQuery] string userName, [FromBody] string? comment)
    {
        return _service.TransferAsync(id, userId, userName, comment);
    }

    /// <summary>
    /// 委托任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="userId">委托人ID</param>
    /// <param name="userName">委托人名称</param>
    /// <param name="comment">委托说明</param>
    [HttpPost("{id}/delegate")]
    public Task DelegateAsync(long id, [FromQuery] long userId, [FromQuery] string userName, [FromBody] string? comment)
    {
        return _service.DelegateAsync(id, userId, userName, comment);
    }

    /// <summary>
    /// 获取任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <returns>任务信息</returns>
    [HttpGet("{id}")]
    public Task<LeanWorkflowTaskDto> GetAsync(long id)
    {
        return _service.GetAsync(id);
    }

    /// <summary>
    /// 获取任务列表
    /// </summary>
    /// <returns>任务列表</returns>
    [HttpGet]
    public Task<List<LeanWorkflowTaskDto>> GetListAsync()
    {
        return _service.GetListAsync();
    }

    /// <summary>
    /// 获取我的待办任务列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>任务列表</returns>
    [HttpGet("my-todo")]
    public Task<List<LeanWorkflowTaskDto>> GetMyTodoListAsync([FromQuery] long userId)
    {
        return _service.GetMyTodoListAsync(userId);
    }

    /// <summary>
    /// 获取我的已办任务列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>任务列表</returns>
    [HttpGet("my-done")]
    public Task<List<LeanWorkflowTaskDto>> GetMyDoneListAsync([FromQuery] long userId)
    {
        return _service.GetMyDoneListAsync(userId);
    }
} 
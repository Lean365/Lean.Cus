using Lean.Cus.Workflow.Dtos.Definition;
using Lean.Cus.Workflow.Services;
using Microsoft.AspNetCore.Mvc;

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
    public Task<long> CreateAsync([FromBody] LeanWorkflowDefinitionDto dto)
    {
        return _service.CreateAsync(dto);
    }

    /// <summary>
    /// 更新流程定义
    /// </summary>
    /// <param name="dto">流程定义信息</param>
    [HttpPut]
    public Task UpdateAsync([FromBody] LeanWorkflowDefinitionDto dto)
    {
        return _service.UpdateAsync(dto);
    }

    /// <summary>
    /// 删除流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    [HttpDelete("{id}")]
    public Task DeleteAsync(long id)
    {
        return _service.DeleteAsync(id);
    }

    /// <summary>
    /// 获取流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    /// <returns>流程定义信息</returns>
    [HttpGet("{id}")]
    public Task<LeanWorkflowDefinitionDto> GetAsync(long id)
    {
        return _service.GetAsync(id);
    }

    /// <summary>
    /// 获取流程定义列表
    /// </summary>
    /// <returns>流程定义列表</returns>
    [HttpGet]
    public Task<List<LeanWorkflowDefinitionDto>> GetListAsync()
    {
        return _service.GetListAsync();
    }

    /// <summary>
    /// 发布流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    [HttpPost("{id}/publish")]
    public Task PublishAsync(long id)
    {
        return _service.PublishAsync(id);
    }

    /// <summary>
    /// 停用流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    [HttpPost("{id}/disable")]
    public Task DisableAsync(long id)
    {
        return _service.DisableAsync(id);
    }
} 
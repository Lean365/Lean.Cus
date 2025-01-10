using Lean.Cus.Workflow.Dtos.Definition;

namespace Lean.Cus.Workflow.Services;

/// <summary>
/// 流程定义服务接口
/// </summary>
public interface ILeanWorkflowDefinitionService
{
    /// <summary>
    /// 创建流程定义
    /// </summary>
    /// <param name="dto">流程定义信息</param>
    /// <returns>流程定义ID</returns>
    Task<long> CreateAsync(LeanWorkflowDefinitionDto dto);

    /// <summary>
    /// 更新流程定义
    /// </summary>
    /// <param name="dto">流程定义信息</param>
    Task UpdateAsync(LeanWorkflowDefinitionDto dto);

    /// <summary>
    /// 删除流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    Task DeleteAsync(long id);

    /// <summary>
    /// 获取流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    /// <returns>流程定义信息</returns>
    Task<LeanWorkflowDefinitionDto> GetAsync(long id);

    /// <summary>
    /// 获取流程定义列表
    /// </summary>
    /// <returns>流程定义列表</returns>
    Task<List<LeanWorkflowDefinitionDto>> GetListAsync();

    /// <summary>
    /// 发布流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    Task PublishAsync(long id);

    /// <summary>
    /// 停用流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    Task DisableAsync(long id);
} 
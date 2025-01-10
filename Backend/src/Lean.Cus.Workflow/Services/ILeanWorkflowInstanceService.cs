using Lean.Cus.Workflow.Dtos.Instance;

namespace Lean.Cus.Workflow.Services;

/// <summary>
/// 流程实例服务接口
/// </summary>
public interface ILeanWorkflowInstanceService
{
    /// <summary>
    /// 启动流程实例
    /// </summary>
    /// <param name="dto">流程实例信息</param>
    /// <returns>流程实例ID</returns>
    Task<long> StartAsync(LeanWorkflowInstanceDto dto);

    /// <summary>
    /// 终止流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    /// <param name="reason">终止原因</param>
    Task TerminateAsync(long id, string reason);

    /// <summary>
    /// 挂起流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    Task SuspendAsync(long id);

    /// <summary>
    /// 恢复流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    Task ResumeAsync(long id);

    /// <summary>
    /// 获取流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    /// <returns>流程实例信息</returns>
    Task<LeanWorkflowInstanceDto> GetAsync(long id);

    /// <summary>
    /// 获取流程实例列表
    /// </summary>
    /// <returns>流程实例列表</returns>
    Task<List<LeanWorkflowInstanceDto>> GetListAsync();

    /// <summary>
    /// 获取我发起的流程实例列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>流程实例列表</returns>
    Task<List<LeanWorkflowInstanceDto>> GetMyStartedListAsync(long userId);

    /// <summary>
    /// 获取我参与的流程实例列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>流程实例列表</returns>
    Task<List<LeanWorkflowInstanceDto>> GetMyInvolvedListAsync(long userId);
} 
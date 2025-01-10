using Lean.Cus.Workflow.Dtos.History;

namespace Lean.Cus.Workflow.Services;

/// <summary>
/// 流程历史记录服务接口
/// </summary>
public interface ILeanWorkflowHistoryService
{
    /// <summary>
    /// 添加历史记录
    /// </summary>
    /// <param name="dto">历史记录信息</param>
    /// <returns>历史记录ID</returns>
    Task<long> AddAsync(LeanWorkflowHistoryDto dto);

    /// <summary>
    /// 获取历史记录
    /// </summary>
    /// <param name="id">历史记录ID</param>
    /// <returns>历史记录信息</returns>
    Task<LeanWorkflowHistoryDto> GetAsync(long id);

    /// <summary>
    /// 获取流程实例的历史记录列表
    /// </summary>
    /// <param name="instanceId">流程实例ID</param>
    /// <returns>历史记录列表</returns>
    Task<List<LeanWorkflowHistoryDto>> GetListByInstanceAsync(long instanceId);
} 
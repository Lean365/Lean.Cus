using Lean.Cus.Workflow.Dtos.Task;

namespace Lean.Cus.Workflow.Services;

/// <summary>
/// 流程任务服务接口
/// </summary>
public interface ILeanWorkflowTaskService
{
    /// <summary>
    /// 完成任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="comment">处理意见</param>
    Task CompleteAsync(long id, string? comment);

    /// <summary>
    /// 取消任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="reason">取消原因</param>
    Task CancelAsync(long id, string reason);

    /// <summary>
    /// 转办任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="userId">转办人ID</param>
    /// <param name="userName">转办人名称</param>
    /// <param name="comment">转办说明</param>
    Task TransferAsync(long id, long userId, string userName, string? comment);

    /// <summary>
    /// 委托任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="userId">委托人ID</param>
    /// <param name="userName">委托人名称</param>
    /// <param name="comment">委托说明</param>
    Task DelegateAsync(long id, long userId, string userName, string? comment);

    /// <summary>
    /// 获取任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <returns>任务信息</returns>
    Task<LeanWorkflowTaskDto> GetAsync(long id);

    /// <summary>
    /// 获取任务列表
    /// </summary>
    /// <returns>任务列表</returns>
    Task<List<LeanWorkflowTaskDto>> GetListAsync();

    /// <summary>
    /// 获取我的待办任务列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>任务列表</returns>
    Task<List<LeanWorkflowTaskDto>> GetMyTodoListAsync(long userId);

    /// <summary>
    /// 获取我的已办任务列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>任务列表</returns>
    Task<List<LeanWorkflowTaskDto>> GetMyDoneListAsync(long userId);

    /// <summary>
    /// 获取用户任务列表
    /// </summary>
    Task<List<LeanWorkflowTaskDto>> GetUserTasksAsync(long userId);

    /// <summary>
    /// 获取角色任务列表
    /// </summary>
    Task<List<LeanWorkflowTaskDto>> GetRoleTasksAsync(long roleId);
} 
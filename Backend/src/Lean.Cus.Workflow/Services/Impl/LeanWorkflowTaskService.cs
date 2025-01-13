using Lean.Cus.Workflow.Dtos.Task;
using Lean.Cus.Workflow.Entities.Task;
using Lean.Cus.Workflow.Enums;
using Mapster;
using SqlSugar;

namespace Lean.Cus.Workflow.Services.Impl;

/// <summary>
/// 流程任务服务实现
/// </summary>
public class LeanWorkflowTaskService : ILeanWorkflowTaskService
{
    private readonly ISqlSugarClient _db;

    public LeanWorkflowTaskService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 完成任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="comment">处理意见</param>
    public async Task CompleteAsync(long id, string? comment)
    {
        var entity = await _db.Queryable<LeanWorkflowTask>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"任务不存在: {id}");

        if (entity.Status != WorkflowTaskStatus.Pending)
        {
            throw new Exception("只能完成待处理状态的任务");
        }

        entity.Status = WorkflowTaskStatus.Completed;
        entity.Comment = comment;
        entity.CompleteTime = DateTime.Now;
        await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 取消任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="reason">取消原因</param>
    public async Task CancelAsync(long id, string reason)
    {
        var entity = await _db.Queryable<LeanWorkflowTask>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"任务不存在: {id}");

        if (entity.Status != WorkflowTaskStatus.Pending)
        {
            throw new Exception("只能取消待处理状态的任务");
        }

        entity.Status = WorkflowTaskStatus.Cancelled;
        entity.Comment = reason;
        entity.CompleteTime = DateTime.Now;
        await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 转办任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="userId">转办人ID</param>
    /// <param name="userName">转办人名称</param>
    /// <param name="comment">转办说明</param>
    public async Task TransferAsync(long id, long userId, string userName, string? comment)
    {
        var entity = await _db.Queryable<LeanWorkflowTask>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"任务不存在: {id}");

        if (entity.Status != WorkflowTaskStatus.Pending)
        {
            throw new Exception("只能转办待处理状态的任务");
        }

        entity.Status = WorkflowTaskStatus.Transferred;
        entity.OriginalAssigneeId = entity.AssigneeId;
        entity.OriginalAssigneeName = entity.AssigneeName;
        entity.AssigneeId = userId;
        entity.AssigneeName = userName;
        entity.Comment = comment;
        await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 委托任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <param name="userId">委托人ID</param>
    /// <param name="userName">委托人名称</param>
    /// <param name="comment">委托说明</param>
    public async Task DelegateAsync(long id, long userId, string userName, string? comment)
    {
        var entity = await _db.Queryable<LeanWorkflowTask>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"任务不存在: {id}");

        if (entity.Status != WorkflowTaskStatus.Pending)
        {
            throw new Exception("只能委托待处理状态的任务");
        }

        entity.Status = WorkflowTaskStatus.Delegated;
        entity.OriginalAssigneeId = entity.AssigneeId;
        entity.OriginalAssigneeName = entity.AssigneeName;
        entity.AssigneeId = userId;
        entity.AssigneeName = userName;
        entity.Comment = comment;
        await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取任务
    /// </summary>
    /// <param name="id">任务ID</param>
    /// <returns>任务信息</returns>
    public async Task<LeanWorkflowTaskDto> GetAsync(long id)
    {
        var entity = await _db.Queryable<LeanWorkflowTask>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"任务不存在: {id}");

        return entity.Adapt<LeanWorkflowTaskDto>();
    }

    /// <summary>
    /// 获取任务列表
    /// </summary>
    /// <returns>任务列表</returns>
    public async Task<List<LeanWorkflowTaskDto>> GetListAsync()
    {
        var list = await _db.Queryable<LeanWorkflowTask>()
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();

        return list.Adapt<List<LeanWorkflowTaskDto>>();
    }

    /// <summary>
    /// 获取我的待办任务列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>任务列表</returns>
    public async Task<List<LeanWorkflowTaskDto>> GetMyTodoListAsync(long userId)
    {
        var list = await _db.Queryable<LeanWorkflowTask>()
            .Where(x => x.AssigneeId == userId && x.Status == WorkflowTaskStatus.Pending)
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();

        return list.Adapt<List<LeanWorkflowTaskDto>>();
    }

    /// <summary>
    /// 获取我的已办任务列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>任务列表</returns>
    public async Task<List<LeanWorkflowTaskDto>> GetMyDoneListAsync(long userId)
    {
        var list = await _db.Queryable<LeanWorkflowTask>()
            .Where(x => x.AssigneeId == userId && x.Status != WorkflowTaskStatus.Pending)
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();

        return list.Adapt<List<LeanWorkflowTaskDto>>();
    }

    /// <summary>
    /// 获取用户任务列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>任务列表</returns>
    public async Task<List<LeanWorkflowTaskDto>> GetUserTasksAsync(long userId)
    {
        var list = await _db.Queryable<LeanWorkflowTask>()
            .Where(x => x.AssigneeId == userId)
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();

        return list.Adapt<List<LeanWorkflowTaskDto>>();
    }

    /// <summary>
    /// 获取角色任务列表
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>任务列表</returns>
    public async Task<List<LeanWorkflowTaskDto>> GetRoleTasksAsync(long roleId)
    {
        var list = await _db.Queryable<LeanWorkflowTask>()
            .Where(x => x.RoleId == roleId)
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();

        return list.Adapt<List<LeanWorkflowTaskDto>>();
    }
} 
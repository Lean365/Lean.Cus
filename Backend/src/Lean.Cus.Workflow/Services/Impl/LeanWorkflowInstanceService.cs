using Lean.Cus.Workflow.Dtos.Instance;
using Lean.Cus.Workflow.Entities.Instance;
using Lean.Cus.Workflow.Enums;
using Mapster;
using SqlSugar;

namespace Lean.Cus.Workflow.Services.Impl;

/// <summary>
/// 流程实例服务实现
/// </summary>
public class LeanWorkflowInstanceService : ILeanWorkflowInstanceService
{
    private readonly ISqlSugarClient _db;

    public LeanWorkflowInstanceService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 启动流程实例
    /// </summary>
    /// <param name="dto">流程实例信息</param>
    /// <returns>流程实例ID</returns>
    public async Task<long> StartAsync(LeanWorkflowInstanceDto dto)
    {
        var entity = dto.Adapt<LeanWorkflowInstance>();
        entity.Status = WorkflowInstanceStatus.Running;
        entity.StartTime = DateTime.Now;
        return await _db.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
    }

    /// <summary>
    /// 终止流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    /// <param name="reason">终止原因</param>
    public async Task TerminateAsync(long id, string reason)
    {
        var entity = await _db.Queryable<LeanWorkflowInstance>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"流程实例不存在: {id}");

        if (entity.Status != WorkflowInstanceStatus.Running)
        {
            throw new Exception("只能终止运行中的流程实例");
        }

        entity.Status = WorkflowInstanceStatus.Terminated;
        entity.EndTime = DateTime.Now;
        await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 挂起流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    public async Task SuspendAsync(long id)
    {
        var entity = await _db.Queryable<LeanWorkflowInstance>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"流程实例不存在: {id}");

        if (entity.Status != WorkflowInstanceStatus.Running)
        {
            throw new Exception("只能挂起运行中的流程实例");
        }

        entity.Status = WorkflowInstanceStatus.Suspended;
        await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 恢复流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    public async Task ResumeAsync(long id)
    {
        var entity = await _db.Queryable<LeanWorkflowInstance>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"流程实例不存在: {id}");

        if (entity.Status != WorkflowInstanceStatus.Suspended)
        {
            throw new Exception("只能恢复已挂起的流程实例");
        }

        entity.Status = WorkflowInstanceStatus.Running;
        await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取流程实例
    /// </summary>
    /// <param name="id">流程实例ID</param>
    /// <returns>流程实例信息</returns>
    public async Task<LeanWorkflowInstanceDto> GetAsync(long id)
    {
        var entity = await _db.Queryable<LeanWorkflowInstance>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"流程实例不存在: {id}");

        return entity.Adapt<LeanWorkflowInstanceDto>();
    }

    /// <summary>
    /// 获取流程实例列表
    /// </summary>
    /// <returns>流程实例列表</returns>
    public async Task<List<LeanWorkflowInstanceDto>> GetListAsync()
    {
        var list = await _db.Queryable<LeanWorkflowInstance>()
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();

        return list.Adapt<List<LeanWorkflowInstanceDto>>();
    }

    /// <summary>
    /// 获取我发起的流程实例列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>流程实例列表</returns>
    public async Task<List<LeanWorkflowInstanceDto>> GetMyStartedListAsync(long userId)
    {
        var list = await _db.Queryable<LeanWorkflowInstance>()
            .Where(x => x.InitiatorId == userId)
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();

        return list.Adapt<List<LeanWorkflowInstanceDto>>();
    }

    /// <summary>
    /// 获取我参与的流程实例列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>流程实例列表</returns>
    public async Task<List<LeanWorkflowInstanceDto>> GetMyInvolvedListAsync(long userId)
    {
        var list = await _db.Queryable<LeanWorkflowInstance>()
            .Where(x => x.InitiatorId == userId)
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();

        return list.Adapt<List<LeanWorkflowInstanceDto>>();
    }
} 
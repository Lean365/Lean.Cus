using Lean.Cus.Workflow.Dtos.Definition;
using Lean.Cus.Workflow.Entities.Definition;
using Lean.Cus.Workflow.Enums;
using Mapster;
using SqlSugar;

namespace Lean.Cus.Workflow.Services.Impl;

/// <summary>
/// 流程定义服务实现
/// </summary>
public class LeanWorkflowDefinitionService : ILeanWorkflowDefinitionService
{
    private readonly ISqlSugarClient _db;

    public LeanWorkflowDefinitionService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 创建流程定义
    /// </summary>
    /// <param name="dto">流程定义信息</param>
    /// <returns>流程定义ID</returns>
    public async Task<long> CreateAsync(LeanWorkflowDefinitionDto dto)
    {
        var entity = dto.Adapt<LeanWorkflowDefinition>();
        entity.Status = WorkflowStatus.Draft;
        return await _db.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
    }

    /// <summary>
    /// 更新流程定义
    /// </summary>
    /// <param name="dto">流程定义信息</param>
    public async Task UpdateAsync(LeanWorkflowDefinitionDto dto)
    {
        var entity = await _db.Queryable<LeanWorkflowDefinition>()
            .Where(x => x.Id == dto.Id)
            .FirstAsync() ?? throw new Exception($"流程定义不存在: {dto.Id}");

        if (entity.Status != WorkflowStatus.Draft)
        {
            throw new Exception("只能修改草稿状态的流程定义");
        }

        entity = dto.Adapt(entity);
        await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 删除流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    public async Task DeleteAsync(long id)
    {
        var entity = await _db.Queryable<LeanWorkflowDefinition>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"流程定义不存在: {id}");

        if (entity.Status != WorkflowStatus.Draft)
        {
            throw new Exception("只能删除草稿状态的流程定义");
        }

        await _db.Deleteable<LeanWorkflowDefinition>()
            .Where(x => x.Id == id)
            .ExecuteCommandAsync();
    }

    /// <summary>
    /// 获取流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    /// <returns>流程定义信息</returns>
    public async Task<LeanWorkflowDefinitionDto> GetAsync(long id)
    {
        var entity = await _db.Queryable<LeanWorkflowDefinition>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"流程定义不存在: {id}");

        return entity.Adapt<LeanWorkflowDefinitionDto>();
    }

    /// <summary>
    /// 获取流程定义列表
    /// </summary>
    /// <returns>流程定义列表</returns>
    public async Task<List<LeanWorkflowDefinitionDto>> GetListAsync()
    {
        var list = await _db.Queryable<LeanWorkflowDefinition>()
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();

        return list.Adapt<List<LeanWorkflowDefinitionDto>>();
    }

    /// <summary>
    /// 发布流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    public async Task PublishAsync(long id)
    {
        var entity = await _db.Queryable<LeanWorkflowDefinition>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"流程定义不存在: {id}");

        if (entity.Status != WorkflowStatus.Draft)
        {
            throw new Exception("只能发布草稿状态的流程定义");
        }

        entity.Status = WorkflowStatus.Published;
        await _db.Updateable(entity).ExecuteCommandAsync();
    }

    /// <summary>
    /// 停用流程定义
    /// </summary>
    /// <param name="id">流程定义ID</param>
    public async Task DisableAsync(long id)
    {
        var entity = await _db.Queryable<LeanWorkflowDefinition>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"流程定义不存在: {id}");

        if (entity.Status != WorkflowStatus.Published)
        {
            throw new Exception("只能停用已发布状态的流程定义");
        }

        entity.Status = WorkflowStatus.Disabled;
        await _db.Updateable(entity).ExecuteCommandAsync();
    }
} 
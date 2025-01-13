using Lean.Cus.Workflow.Dtos.History;
using Lean.Cus.Workflow.Entities.History;
using Mapster;
using SqlSugar;

namespace Lean.Cus.Workflow.Services.Impl;

/// <summary>
/// 流程历史记录服务实现
/// </summary>
public class LeanWorkflowHistoryService : ILeanWorkflowHistoryService
{
    private readonly ISqlSugarClient _db;

    public LeanWorkflowHistoryService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 添加历史记录
    /// </summary>
    /// <param name="dto">历史记录信息</param>
    /// <returns>历史记录ID</returns>
    public async Task<long> CreateAsync(LeanWorkflowHistoryDto dto)
    {
        var entity = dto.Adapt<LeanWorkflowHistory>();
        entity.OperationTime = DateTime.Now;
        return await _db.Insertable(entity).ExecuteReturnSnowflakeIdAsync();
    }

    /// <summary>
    /// 获取历史记录
    /// </summary>
    /// <param name="id">历史记录ID</param>
    /// <returns>历史记录信息</returns>
    public async Task<LeanWorkflowHistoryDto> GetAsync(long id)
    {
        var entity = await _db.Queryable<LeanWorkflowHistory>()
            .Where(x => x.Id == id)
            .FirstAsync() ?? throw new Exception($"历史记录不存在: {id}");

        return entity.Adapt<LeanWorkflowHistoryDto>();
    }

    /// <summary>
    /// 获取流程实例的历史记录列表
    /// </summary>
    /// <param name="instanceId">流程实例ID</param>
    /// <returns>历史记录列表</returns>
    public async Task<List<LeanWorkflowHistoryDto>> GetListByInstanceAsync(long instanceId)
    {
        var list = await _db.Queryable<LeanWorkflowHistory>()
            .Where(x => x.InstanceId == instanceId)
            .OrderByDescending(x => x.OperationTime)
            .ToListAsync();

        return list.Adapt<List<LeanWorkflowHistoryDto>>();
    }
} 
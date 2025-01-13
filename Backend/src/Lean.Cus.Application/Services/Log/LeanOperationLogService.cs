using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Lean.Cus.Application.Interfaces.Log;
using Lean.Cus.Application.Dtos.Log;
using Lean.Cus.Domain.Entities.Log;
using Lean.Cus.Common.Paging;
using SqlSugar;
using Mapster;
using OfficeOpenXml;
using System.IO;
using Lean.Cus.Common.Excel;

namespace Lean.Cus.Application.Services.Log;

/// <summary>
/// 操作日志服务实现
/// </summary>
public class LeanOperationLogService : ILeanOperationLogService
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 初始化操作日志服务
    /// </summary>
    public LeanOperationLogService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 新增操作日志
    /// </summary>
    public async Task<LeanOperationLogDto> CreateAsync(LeanOperationLogCreateDto dto)
    {
        var entity = dto.Adapt<LeanOperationLog>();
        var result = await _db.Insertable(entity).ExecuteReturnEntityAsync();
        return result.Adapt<LeanOperationLogDto>();
    }

    /// <summary>
    /// 删除操作日志
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _db.Deleteable<LeanOperationLog>().Where(x => x.Id == id).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 获取操作日志
    /// </summary>
    public async Task<LeanOperationLogDto> GetAsync(long id)
    {
        var result = await _db.Queryable<LeanOperationLog>().FirstAsync(x => x.Id == id);
        return result?.Adapt<LeanOperationLogDto>();
    }

    /// <summary>
    /// 获取操作日志列表
    /// </summary>
    public async Task<List<LeanOperationLogDto>> GetListAsync(LeanOperationLogQueryDto query)
    {
        var result = await _db.Queryable<LeanOperationLog>()
            .WhereIF(query.UserId.HasValue, x => x.UserId == query.UserId.Value)
            .WhereIF(!string.IsNullOrEmpty(query.UserName), x => x.UserName.Contains(query.UserName))
            .WhereIF(query.OperationType.HasValue, x => x.OperationType == query.OperationType.Value)
            .WhereIF(!string.IsNullOrEmpty(query.Ip), x => x.Ip == query.Ip)
            .WhereIF(query.CreatedTimeStart.HasValue, x => x.CreateTime >= query.CreatedTimeStart.Value)
            .WhereIF(query.CreatedTimeEnd.HasValue, x => x.CreateTime <= query.CreatedTimeEnd.Value)
            .WhereIF(query.Status.HasValue, x => x.Status == query.Status.Value)
            .OrderByDescending(x => x.CreateTime)
            .ToListAsync();

        return result.Adapt<List<LeanOperationLogDto>>();
    }

    /// <summary>
    /// 分页查询操作日志
    /// </summary>
    public async Task<PagedResults<LeanOperationLogDto>> GetPagedListAsync(LeanOperationLogQueryDto query)
    {
        RefAsync<int> total = 0;
        var list = await _db.Queryable<LeanOperationLog>()
            .WhereIF(query.UserId.HasValue, x => x.UserId == query.UserId.Value)
            .WhereIF(!string.IsNullOrEmpty(query.UserName), x => x.UserName.Contains(query.UserName))
            .WhereIF(query.OperationType.HasValue, x => x.OperationType == query.OperationType.Value)
            .WhereIF(!string.IsNullOrEmpty(query.Ip), x => x.Ip == query.Ip)
            .WhereIF(query.CreatedTimeStart.HasValue, x => x.CreateTime >= query.CreatedTimeStart.Value)
            .WhereIF(query.CreatedTimeEnd.HasValue, x => x.CreateTime <= query.CreatedTimeEnd.Value)
            .WhereIF(query.Status.HasValue, x => x.Status == query.Status.Value)
            .OrderByDescending(x => x.CreateTime)
            .ToPageListAsync(query.PageIndex, query.PageSize, total);

        var items = list.Adapt<List<LeanOperationLogDto>>();
        var result = new PagedResults<LeanOperationLogDto>
        {
            Items = items,
            Total = total.Value,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
        return result;
    }

    /// <summary>
    /// 导出操作日志数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanOperationLogQueryDto query)
    {
        var list = await GetListAsync(query);
        var columns = new Dictionary<string, string>
        {
            { nameof(LeanOperationLogDto.UserName), "操作人" },
            { nameof(LeanOperationLogDto.OperationType), "操作类型" },
            { nameof(LeanOperationLogDto.Description), "操作描述" },
            { nameof(LeanOperationLogDto.Ip), "IP地址" },
            { nameof(LeanOperationLogDto.CreateTime), "操作时间" },
            { nameof(LeanOperationLogDto.Status), "状态" },
            { nameof(LeanOperationLogDto.ErrorMessage), "错误信息" }
        };
        return await LeanExcelHelper.ExportAsync(list, columns, "操作日志");
    }

    /// <summary>
    /// 清空操作日志
    /// </summary>
    public async Task<bool> ClearAsync()
    {
        return await _db.Deleteable<LeanOperationLog>().ExecuteCommandHasChangeAsync();
    }
}
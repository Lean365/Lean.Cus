using Lean.Cus.Domain.Entities.Log;
using Lean.Cus.Domain.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Common.Excel;
using Lean.Cus.Common.Paging;
using System.IO;
using Lean.Cus.Application.Interfaces.Log;
using Lean.Cus.Application.Dtos.Log;
using Mapster;
using SqlSugar;
using System;
using System.Linq;

namespace Lean.Cus.Application.Services.Log;

/// <summary>
/// SQL日志服务实现
/// </summary>
public class LeanSqlLogService : ILeanSqlLogService
{
    private readonly ILeanRepository<LeanSqlLog> _sqlLogRepository;

    /// <summary>
    /// 初始化SQL日志服务
    /// </summary>
    public LeanSqlLogService(ILeanRepository<LeanSqlLog> sqlLogRepository)
    {
        _sqlLogRepository = sqlLogRepository;
    }

    /// <summary>
    /// 新增SQL日志
    /// </summary>
    public async Task<LeanSqlLogDto> CreateAsync(LeanSqlLogCreateDto dto)
    {
        var sqlLog = dto.Adapt<LeanSqlLog>();
        await _sqlLogRepository.InsertAsync(sqlLog);
        return sqlLog.Adapt<LeanSqlLogDto>();
    }

    /// <summary>
    /// 删除SQL日志
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _sqlLogRepository.DeleteAsync(u => u.Id == id) > 0;
    }

    /// <summary>
    /// 获取SQL日志
    /// </summary>
    public async Task<LeanSqlLogDto> GetAsync(long id)
    {
        var sqlLog = await _sqlLogRepository.GetAsync(x => x.Id == id);
        return sqlLog.Adapt<LeanSqlLogDto>();
    }

    /// <summary>
    /// 获取SQL日志列表
    /// </summary>
    public async Task<List<LeanSqlLogDto>> GetListAsync(LeanSqlLogQueryDto queryDto)
    {
        var sqlLogs = await _sqlLogRepository.GetListAsync(u =>
            (!queryDto.SqlType.HasValue || u.SqlType == queryDto.SqlType.Value) &&
            (string.IsNullOrEmpty(queryDto.DatabaseName) || u.DatabaseName.Contains(queryDto.DatabaseName)) &&
            (string.IsNullOrEmpty(queryDto.TableName) || u.TableName.Contains(queryDto.TableName)) &&
            (!queryDto.Status.HasValue || u.Status == queryDto.Status.Value) &&
            (!queryDto.UserId.HasValue || u.UserId == queryDto.UserId.Value) &&
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (string.IsNullOrEmpty(queryDto.Ip) || u.Ip.Contains(queryDto.Ip)) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value));
        return sqlLogs.Adapt<List<LeanSqlLogDto>>();
    }

    /// <summary>
    /// 分页查询SQL日志
    /// </summary>
    public async Task<PagedResults<LeanSqlLogDto>> GetPagedListAsync(LeanSqlLogQueryDto queryDto)
    {
        RefAsync<int> total = 0;
        var list = await _sqlLogRepository.GetPageListAsync(u =>
            (!queryDto.SqlType.HasValue || u.SqlType == queryDto.SqlType.Value) &&
            (string.IsNullOrEmpty(queryDto.DatabaseName) || u.DatabaseName.Contains(queryDto.DatabaseName)) &&
            (string.IsNullOrEmpty(queryDto.TableName) || u.TableName.Contains(queryDto.TableName)) &&
            (!queryDto.Status.HasValue || u.Status == queryDto.Status.Value) &&
            (!queryDto.UserId.HasValue || u.UserId == queryDto.UserId.Value) &&
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (string.IsNullOrEmpty(queryDto.Ip) || u.Ip.Contains(queryDto.Ip)) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value),
            queryDto.PageIndex,
            queryDto.PageSize,
            total);

        var sqlLogDtos = list.Adapt<List<LeanSqlLogDto>>();
        return new PagedResults<LeanSqlLogDto>
        {
            Items = sqlLogDtos,
            Total = total,
            PageIndex = queryDto.PageIndex,
            PageSize = queryDto.PageSize
        };
    }

    /// <summary>
    /// 导出SQL日志数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanSqlLogQueryDto queryDto)
    {
        var list = await GetListAsync(queryDto);
        var exportList = list.Adapt<List<LeanSqlLogExportDto>>();
        return await LeanExcelHelper.ExportAsync(exportList);
    }

    /// <summary>
    /// 清空SQL日志
    /// </summary>
    public async Task<bool> ClearAsync()
    {
        return await _sqlLogRepository.DeleteAsync(u => true) > 0;
    }
} 
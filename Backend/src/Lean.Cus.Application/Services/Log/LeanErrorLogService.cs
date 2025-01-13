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
/// 错误日志服务实现
/// </summary>
public class LeanErrorLogService : ILeanErrorLogService
{
    private readonly ILeanRepository<LeanErrorLog> _errorLogRepository;

    /// <summary>
    /// 初始化错误日志服务
    /// </summary>
    public LeanErrorLogService(ILeanRepository<LeanErrorLog> errorLogRepository)
    {
        _errorLogRepository = errorLogRepository;
    }

    /// <summary>
    /// 新增错误日志
    /// </summary>
    public async Task<LeanErrorLogDto> CreateAsync(LeanErrorLogDto dto)
    {
        var errorLog = dto.Adapt<LeanErrorLog>();
        await _errorLogRepository.InsertAsync(errorLog);
        return errorLog.Adapt<LeanErrorLogDto>();
    }

    /// <summary>
    /// 删除错误日志
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _errorLogRepository.DeleteAsync(u => u.Id == id) > 0;
    }

    /// <summary>
    /// 获取错误日志
    /// </summary>
    public async Task<LeanErrorLogDto> GetAsync(long id)
    {
        var errorLog = await _errorLogRepository.GetAsync(x => x.Id == id);
        return errorLog.Adapt<LeanErrorLogDto>();
    }

    /// <summary>
    /// 获取错误日志列表
    /// </summary>
    public async Task<List<LeanErrorLogDto>> GetListAsync(LeanErrorLogQueryDto queryDto)
    {
        var errorLogs = await _errorLogRepository.GetListAsync(u =>
            (!queryDto.ErrorLevel.HasValue || u.ErrorLevel == queryDto.ErrorLevel.Value) &&
            (string.IsNullOrEmpty(queryDto.Source) || u.Source.Contains(queryDto.Source)) &&
            (string.IsNullOrEmpty(queryDto.Message) || u.Message.Contains(queryDto.Message)) &&
            (string.IsNullOrEmpty(queryDto.Url) || u.Url.Contains(queryDto.Url)) &&
            (!queryDto.UserId.HasValue || u.UserId == queryDto.UserId.Value) &&
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (string.IsNullOrEmpty(queryDto.Ip) || u.Ip.Contains(queryDto.Ip)) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value));
        return errorLogs.Adapt<List<LeanErrorLogDto>>();
    }

    /// <summary>
    /// 分页查询错误日志
    /// </summary>
    public async Task<PagedResults<LeanErrorLogDto>> GetPagedListAsync(LeanErrorLogQueryDto queryDto)
    {
        RefAsync<int> total = 0;
        var list = await _errorLogRepository.GetPageListAsync(u =>
            (!queryDto.ErrorLevel.HasValue || u.ErrorLevel == queryDto.ErrorLevel.Value) &&
            (string.IsNullOrEmpty(queryDto.Source) || u.Source.Contains(queryDto.Source)) &&
            (string.IsNullOrEmpty(queryDto.Message) || u.Message.Contains(queryDto.Message)) &&
            (string.IsNullOrEmpty(queryDto.Url) || u.Url.Contains(queryDto.Url)) &&
            (!queryDto.UserId.HasValue || u.UserId == queryDto.UserId.Value) &&
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (string.IsNullOrEmpty(queryDto.Ip) || u.Ip.Contains(queryDto.Ip)) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value),
            queryDto.PageIndex,
            queryDto.PageSize,
            total);

        var errorLogDtos = list.Adapt<List<LeanErrorLogDto>>();
        return new PagedResults<LeanErrorLogDto>
        {
            Items = errorLogDtos,
            Total = total,
            PageIndex = queryDto.PageIndex,
            PageSize = queryDto.PageSize
        };
    }

    /// <summary>
    /// 导出错误日志数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanErrorLogQueryDto queryDto)
    {
        var list = await GetListAsync(queryDto);
        var exportList = list.Adapt<List<LeanErrorLogExportDto>>();
        return await LeanExcelHelper.ExportAsync(exportList);
    }

    /// <summary>
    /// 清空错误日志
    /// </summary>
    public async Task<bool> ClearAsync()
    {
        return await _errorLogRepository.DeleteAsync(u => true) > 0;
    }
} 
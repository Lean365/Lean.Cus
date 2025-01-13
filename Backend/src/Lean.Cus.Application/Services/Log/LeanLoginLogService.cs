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
/// 登录日志服务实现
/// </summary>
public class LeanLoginLogService : ILeanLoginLogService
{
    private readonly ILeanRepository<LeanLoginLog> _loginLogRepository;

    /// <summary>
    /// 初始化登录日志服务
    /// </summary>
    public LeanLoginLogService(ILeanRepository<LeanLoginLog> loginLogRepository)
    {
        _loginLogRepository = loginLogRepository;
    }

    /// <summary>
    /// 新增登录日志
    /// </summary>
    public async Task<LeanLoginLogDto> CreateAsync(LeanLoginLogDto dto)
    {
        var loginLog = dto.Adapt<LeanLoginLog>();
        await _loginLogRepository.InsertAsync(loginLog);
        return loginLog.Adapt<LeanLoginLogDto>();
    }

    /// <summary>
    /// 删除登录日志
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _loginLogRepository.DeleteAsync(u => u.Id == id) > 0;
    }

    /// <summary>
    /// 获取登录日志
    /// </summary>
    public async Task<LeanLoginLogDto> GetAsync(long id)
    {
        var loginLog = await _loginLogRepository.GetAsync(x => x.Id == id);
        return loginLog.Adapt<LeanLoginLogDto>();
    }

    /// <summary>
    /// 获取登录日志列表
    /// </summary>
    public async Task<List<LeanLoginLogDto>> GetListAsync(LeanLoginLogQueryDto queryDto)
    {
        var loginLogs = await _loginLogRepository.GetListAsync(u =>
            (!queryDto.UserId.HasValue || u.UserId == queryDto.UserId.Value) &&
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (string.IsNullOrEmpty(queryDto.Ip) || u.Ip.Contains(queryDto.Ip)) &&
            (!queryDto.LoginType.HasValue || u.LoginType == queryDto.LoginType.Value) &&
            (!queryDto.Status.HasValue || u.Status == queryDto.Status.Value) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value));
        return loginLogs.Adapt<List<LeanLoginLogDto>>();
    }

    /// <summary>
    /// 分页查询登录日志
    /// </summary>
    public async Task<PagedResults<LeanLoginLogDto>> GetPagedListAsync(LeanLoginLogQueryDto queryDto)
    {
        RefAsync<int> total = 0;
        var list = await _loginLogRepository.GetPageListAsync(u =>
            (!queryDto.UserId.HasValue || u.UserId == queryDto.UserId.Value) &&
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (string.IsNullOrEmpty(queryDto.Ip) || u.Ip.Contains(queryDto.Ip)) &&
            (!queryDto.LoginType.HasValue || u.LoginType == queryDto.LoginType.Value) &&
            (!queryDto.Status.HasValue || u.Status == queryDto.Status.Value) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value),
            queryDto.PageIndex,
            queryDto.PageSize,
            total);

        var loginLogDtos = list.Adapt<List<LeanLoginLogDto>>();
        return new PagedResults<LeanLoginLogDto>
        {
            Items = loginLogDtos,
            Total = total,
            PageIndex = queryDto.PageIndex,
            PageSize = queryDto.PageSize
        };
    }

    /// <summary>
    /// 导出登录日志数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanLoginLogQueryDto queryDto)
    {
        var list = await GetListAsync(queryDto);
        var exportList = list.Adapt<List<LeanLoginLogExportDto>>();
        return await LeanExcelHelper.ExportAsync(exportList);
    }

    /// <summary>
    /// 清空登录日志
    /// </summary>
    public async Task<bool> ClearAsync()
    {
        return await _loginLogRepository.DeleteAsync(u => true) > 0;
    }
} 
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
/// 审计日志服务实现
/// </summary>
public class LeanAuditLogService : ILeanAuditLogService
{
    private readonly ILeanRepository<LeanAuditLog> _auditLogRepository;

    /// <summary>
    /// 初始化审计日志服务
    /// </summary>
    public LeanAuditLogService(ILeanRepository<LeanAuditLog> auditLogRepository)
    {
        _auditLogRepository = auditLogRepository;
    }

    /// <summary>
    /// 新增审计日志
    /// </summary>
    public async Task<LeanAuditLogDto> CreateAsync(LeanAuditLogDto dto)
    {
        var auditLog = dto.Adapt<LeanAuditLog>();
        await _auditLogRepository.InsertAsync(auditLog);
        return auditLog.Adapt<LeanAuditLogDto>();
    }

    /// <summary>
    /// 删除审计日志
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _auditLogRepository.DeleteAsync(u => u.Id == id) > 0;
    }

    /// <summary>
    /// 获取审计日志
    /// </summary>
    public async Task<LeanAuditLogDto> GetAsync(long id)
    {
        var auditLog = await _auditLogRepository.GetAsync(x => x.Id == id);
        return auditLog.Adapt<LeanAuditLogDto>();
    }

    /// <summary>
    /// 获取审计日志列表
    /// </summary>
    public async Task<List<LeanAuditLogDto>> GetListAsync(LeanAuditLogQueryDto queryDto)
    {
        var auditLogs = await _auditLogRepository.GetListAsync(u =>
            (!queryDto.UserId.HasValue || u.UserId == queryDto.UserId.Value) &&
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (!queryDto.AuditType.HasValue || u.AuditType == queryDto.AuditType.Value) &&
            (string.IsNullOrEmpty(queryDto.TableName) || u.TableName.Contains(queryDto.TableName)) &&
            (!queryDto.BusinessId.HasValue || u.BusinessId == queryDto.BusinessId.Value) &&
            (!queryDto.OperationType.HasValue || u.OperationType == queryDto.OperationType.Value) &&
            (string.IsNullOrEmpty(queryDto.Ip) || u.Ip.Contains(queryDto.Ip)) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value));
        return auditLogs.Adapt<List<LeanAuditLogDto>>();
    }

    /// <summary>
    /// 分页查询审计日志
    /// </summary>
    public async Task<PagedResults<LeanAuditLogDto>> GetPagedListAsync(LeanAuditLogQueryDto queryDto)
    {
        RefAsync<int> total = 0;
        var list = await _auditLogRepository.GetPageListAsync(u =>
            (!queryDto.UserId.HasValue || u.UserId == queryDto.UserId.Value) &&
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (!queryDto.AuditType.HasValue || u.AuditType == queryDto.AuditType.Value) &&
            (string.IsNullOrEmpty(queryDto.TableName) || u.TableName.Contains(queryDto.TableName)) &&
            (!queryDto.BusinessId.HasValue || u.BusinessId == queryDto.BusinessId.Value) &&
            (!queryDto.OperationType.HasValue || u.OperationType == queryDto.OperationType.Value) &&
            (string.IsNullOrEmpty(queryDto.Ip) || u.Ip.Contains(queryDto.Ip)) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value),
            queryDto.PageIndex,
            queryDto.PageSize,
            total);

        var auditLogDtos = list.Adapt<List<LeanAuditLogDto>>();
        return new PagedResults<LeanAuditLogDto>
        {
            Items = auditLogDtos,
            Total = total,
            PageIndex = queryDto.PageIndex,
            PageSize = queryDto.PageSize
        };
    }

    /// <summary>
    /// 导出审计日志数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanAuditLogQueryDto queryDto)
    {
        var list = await GetListAsync(queryDto);
        var exportList = list.Adapt<List<LeanAuditLogExportDto>>();
        return await LeanExcelHelper.ExportAsync(exportList);
    }

    /// <summary>
    /// 清空审计日志
    /// </summary>
    public async Task<bool> ClearAsync()
    {
        return await _auditLogRepository.DeleteAsync(u => true) > 0;
    }
} 
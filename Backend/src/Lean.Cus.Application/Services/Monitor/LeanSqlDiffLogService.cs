//===================================================
// 项目名称：Lean.Cus.Application.Services.Monitor
// 文件名称：LeanSqlDiffLogService
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：SQL差异日志服务实现
//===================================================

using System;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Monitor;
using Lean.Cus.Domain.Entities.Monitor;
using Lean.Cus.Domain.IRepositories.Monitor;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Exceptions;
using Mapster;
using System.Collections.Generic;

namespace Lean.Cus.Application.Services.Monitor;

/// <summary>
/// SQL差异日志服务实现
/// </summary>
public class LeanSqlDiffLogService : ILeanSqlDiffLogService
{
    private readonly ISqlDiffLogRepository _sqlDiffLogRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    public LeanSqlDiffLogService(ISqlDiffLogRepository sqlDiffLogRepository)
    {
        _sqlDiffLogRepository = sqlDiffLogRepository;
    }

    /// <summary>
    /// 获取分页列表
    /// </summary>
    public async Task<LeanPageResult<LeanSqlDiffLogDto>> GetPageListAsync(LeanSqlDiffLogQueryDto queryDto)
    {
        if (queryDto == null || queryDto.Current < 1 || queryDto.Size < 1)
        {
            throw new LeanException("分页参数错误", LeanErrorCodeEnum.ValidationError);
        }

        var (list, total) = await _sqlDiffLogRepository.GetPageListAsync(
            queryDto.DatabaseName,
            queryDto.TableName,
            queryDto.OperationType,
            queryDto.OperatorName,
            queryDto.IpAddress,
            queryDto.StartTime,
            queryDto.EndTime,
            queryDto.Current,
            queryDto.Size);

        return new LeanPageResult<LeanSqlDiffLogDto>
        {
            Total = total,
            Records = list.Adapt<List<LeanSqlDiffLogDto>>()
        };
    }

    /// <summary>
    /// 获取详情
    /// </summary>
    public async Task<LeanSqlDiffLogDto> GetByIdAsync(long id)
    {
        var entity = await _sqlDiffLogRepository.GetByIdAsync(id);
        return entity.Adapt<LeanSqlDiffLogDto>();
    }

    /// <summary>
    /// 记录SQL差异日志
    /// </summary>
    public async Task<bool> RecordAsync(LeanSqlDiffLogInputDto inputDto)
    {
        var entity = inputDto.Adapt<LeanSqlDiffLog>();
        entity.CreateTime = DateTime.Now;
        return await _sqlDiffLogRepository.AddAsync(entity);
    }

    /// <summary>
    /// 删除日志
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _sqlDiffLogRepository.DeleteAsync(id);
    }

    /// <summary>
    /// 批量删除日志
    /// </summary>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        return await _sqlDiffLogRepository.BatchDeleteAsync(ids);
    }

    /// <summary>
    /// 清空所有日志
    /// </summary>
    public async Task<bool> ClearAllAsync()
    {
        return await _sqlDiffLogRepository.ClearAllAsync();
    }
} 
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lean.Cus.Application.IServices.Admin;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.Repositories;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 字典类型服务实现
/// </summary>
public class LeanDictTypeService : LeanService<LeanDictType, LeanDictTypeDto, LeanDictTypeQueryDto, LeanDictTypeCreateDto, LeanDictTypeUpdateDto>, ILeanDictTypeService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储</param>
    /// <param name="logger">日志记录器</param>
    public LeanDictTypeService(
        ILeanRepository<LeanDictType> repository,
        ILogger<LeanDictTypeService> logger)
        : base(repository, logger)
    {
    }

    /// <inheritdoc/>
    public async Task<bool> CheckDictTypeCodeUniqueAsync(string code, long? id = null)
    {
        var query = Repository.AsQueryable().Where(d => d.Code == code);
        if (id.HasValue)
        {
            query = query.Where(d => d.Id != id.Value);
        }

        return await query.AnyAsync();
    }

    /// <inheritdoc/>
    public async Task<List<LeanDictTypeDto>> ExportDictTypesAsync(LeanDictTypeQueryDto queryDto)
    {
        var query = Repository.AsQueryable();

        // 构建查询条件
        if (!string.IsNullOrEmpty(queryDto.DictName))
        {
            query = query.Where(d => d.Name.Contains(queryDto.DictName));
        }

        if (!string.IsNullOrEmpty(queryDto.DictCode))
        {
            query = query.Where(d => d.Code.Contains(queryDto.DictCode));
        }

        if (queryDto.Status.HasValue)
        {
            query = query.Where(d => d.Status == queryDto.Status.Value);
        }

        // 获取数据
        var list = await query.ToListAsync();

        // 转换为DTO
        return list.Select(MapToDto).ToList();
    }

    /// <inheritdoc/>
    protected override long GetId(LeanDictTypeUpdateDto dto)
    {
        return dto.Id;
    }

    /// <inheritdoc/>
    protected override LeanDictTypeDto MapToDto(LeanDictType entity)
    {
        return new LeanDictTypeDto
        {
            Id = entity.Id,
            DictName = entity.Name,
            DictCode = entity.Code,
            Status = entity.Status,
            Remark = entity.Remark
        };
    }

    /// <inheritdoc/>
    protected override LeanDictType MapToEntity(LeanDictTypeCreateDto dto)
    {
        return new LeanDictType
        {
            Name = dto.DictName,
            Code = dto.DictCode,
            Status = dto.Status,
            Remark = dto.Remark
        };
    }

    /// <inheritdoc/>
    protected override void MapToEntity(LeanDictTypeUpdateDto dto, LeanDictType entity)
    {
        entity.Name = dto.DictName;
        entity.Code = dto.DictCode;
        entity.Status = dto.Status;
        entity.Remark = dto.Remark;
    }
} 
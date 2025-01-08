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
/// 字典数据服务实现
/// </summary>
public class LeanDictDataService : LeanService<LeanDictData, LeanDictDataDto, LeanDictDataQueryDto, LeanDictDataCreateDto, LeanDictDataUpdateDto>, ILeanDictDataService
{
    private readonly ILeanRepository<LeanDictType> _dictTypeRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储</param>
    /// <param name="dictTypeRepository">字典类型仓储</param>
    /// <param name="logger">日志记录器</param>
    public LeanDictDataService(
        ILeanRepository<LeanDictData> repository,
        ILeanRepository<LeanDictType> dictTypeRepository,
        ILogger<LeanDictDataService> logger)
        : base(repository, logger)
    {
        _dictTypeRepository = dictTypeRepository;
    }

    /// <inheritdoc/>
    public async Task<List<LeanDictDataDto>> GetDictDataListByTypeCodeAsync(string dictTypeCode)
    {
        // 获取字典类型
        var dictType = await _dictTypeRepository.AsQueryable()
            .FirstAsync(d => d.Code == dictTypeCode);

        if (dictType == null)
        {
            throw new System.Exception($"字典类型不存在：{dictTypeCode}");
        }

        // 获取字典数据
        var list = await Repository.AsQueryable()
            .Where(d => d.DictTypeId == dictType.Id)
            .OrderBy(d => d.Sort)
            .ToListAsync();

        return list.Select(MapToDto).ToList();
    }

    /// <inheritdoc/>
    public async Task<List<LeanDictDataDto>> ExportDictDataAsync(LeanDictDataQueryDto queryDto)
    {
        var query = Repository.AsQueryable()
            .LeftJoin<LeanDictType>((d, t) => d.DictTypeId == t.Id)
            .Select((d, t) => new LeanDictData
            {
                Id = d.Id,
                DictTypeId = d.DictTypeId,
                Label = d.Label,
                Value = d.Value,
                Sort = d.Sort,
                Status = d.Status,
                CssClass = d.CssClass,
                Remark = d.Remark,
                DictType = t
            });

        // 构建查询条件
        if (queryDto.DictTypeId.HasValue)
        {
            query = query.Where(d => d.DictTypeId == queryDto.DictTypeId.Value);
        }

        if (!string.IsNullOrEmpty(queryDto.Label))
        {
            query = query.Where(d => d.Label.Contains(queryDto.Label));
        }

        if (!string.IsNullOrEmpty(queryDto.Value))
        {
            query = query.Where(d => d.Value.Contains(queryDto.Value));
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
    protected override long GetId(LeanDictDataUpdateDto dto)
    {
        return dto.Id;
    }

    /// <inheritdoc/>
    protected override LeanDictDataDto MapToDto(LeanDictData entity)
    {
        return new LeanDictDataDto
        {
            Id = entity.Id,
            DictTypeId = entity.DictTypeId,
            Label = entity.Label,
            Value = entity.Value,
            Sort = entity.Sort,
            Status = entity.Status,
            CssClass = entity.CssClass,
            Remark = entity.Remark,
            DictTypeName = entity.DictType?.Name
        };
    }

    /// <inheritdoc/>
    protected override LeanDictData MapToEntity(LeanDictDataCreateDto dto)
    {
        return new LeanDictData
        {
            DictTypeId = dto.DictTypeId,
            Label = dto.Label,
            Value = dto.Value,
            Sort = dto.Sort,
            Status = dto.Status,
            CssClass = dto.CssClass,
            Remark = dto.Remark
        };
    }

    /// <inheritdoc/>
    protected override void MapToEntity(LeanDictDataUpdateDto dto, LeanDictData entity)
    {
        entity.DictTypeId = dto.DictTypeId;
        entity.Label = dto.Label;
        entity.Value = dto.Value;
        entity.Sort = dto.Sort;
        entity.Status = dto.Status;
        entity.CssClass = dto.CssClass;
        entity.Remark = dto.Remark;
    }
} 
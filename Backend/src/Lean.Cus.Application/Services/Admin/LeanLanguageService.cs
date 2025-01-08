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
/// 语言服务实现
/// </summary>
public class LeanLanguageService : LeanService<LeanLanguage, LeanLanguageDto, LeanLanguageQueryDto, LeanLanguageCreateDto, LeanLanguageUpdateDto>, ILeanLanguageService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储</param>
    /// <param name="logger">日志记录器</param>
    public LeanLanguageService(
        ILeanRepository<LeanLanguage> repository,
        ILogger<LeanLanguageService> logger)
        : base(repository, logger)
    {
    }

    /// <inheritdoc/>
    public async Task<bool> CheckLanguageCodeUniqueAsync(string code, long? id = null)
    {
        var query = Repository.AsQueryable().Where(l => l.Code == code);
        if (id.HasValue)
        {
            query = query.Where(l => l.Id != id.Value);
        }

        return await query.AnyAsync();
    }

    /// <inheritdoc/>
    public async Task<List<LeanLanguageDto>> ExportLanguagesAsync(LeanLanguageQueryDto queryDto)
    {
        var query = Repository.AsQueryable();

        // 构建查询条件
        if (!string.IsNullOrEmpty(queryDto.Name))
        {
            query = query.Where(l => l.Name.Contains(queryDto.Name));
        }

        if (!string.IsNullOrEmpty(queryDto.Code))
        {
            query = query.Where(l => l.Code.Contains(queryDto.Code));
        }

        if (queryDto.Status.HasValue)
        {
            query = query.Where(l => l.Status == queryDto.Status.Value);
        }

        // 获取数据
        var list = await query.ToListAsync();

        // 转换为DTO
        return list.Select(MapToDto).ToList();
    }

    /// <inheritdoc/>
    protected override long GetId(LeanLanguageUpdateDto dto)
    {
        return dto.Id;
    }

    /// <inheritdoc/>
    protected override LeanLanguageDto MapToDto(LeanLanguage entity)
    {
        return new LeanLanguageDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Code = entity.Code,
            Icon = entity.Icon,
            Sort = entity.Sort,
            Status = entity.Status,
            Remark = entity.Remark
        };
    }

    /// <inheritdoc/>
    protected override LeanLanguage MapToEntity(LeanLanguageCreateDto dto)
    {
        return new LeanLanguage
        {
            Name = dto.Name,
            Code = dto.Code,
            Icon = dto.Icon,
            Sort = dto.Sort,
            Status = dto.Status,
            Remark = dto.Remark
        };
    }

    /// <inheritdoc/>
    protected override void MapToEntity(LeanLanguageUpdateDto dto, LeanLanguage entity)
    {
        entity.Name = dto.Name;
        entity.Code = dto.Code;
        entity.Icon = dto.Icon;
        entity.Sort = dto.Sort;
        entity.Status = dto.Status;
        entity.Remark = dto.Remark;
    }
} 
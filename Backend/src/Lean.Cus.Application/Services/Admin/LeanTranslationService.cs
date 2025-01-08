using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lean.Cus.Application.IServices.Admin;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Enums;
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.Repositories;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 翻译服务实现
/// </summary>
public class LeanTranslationService : LeanService<LeanTranslation, LeanTranslationDto, LeanTranslationQueryDto, LeanTranslationCreateDto, LeanTranslationUpdateDto>, ILeanTranslationService
{
    private readonly ILeanRepository<LeanLanguage> _languageRepository;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储</param>
    /// <param name="languageRepository">语言仓储</param>
    /// <param name="logger">日志记录器</param>
    public LeanTranslationService(
        ILeanRepository<LeanTranslation> repository,
        ILeanRepository<LeanLanguage> languageRepository,
        ILogger<LeanTranslationService> logger)
        : base(repository, logger)
    {
        _languageRepository = languageRepository;
    }

    /// <inheritdoc/>
    public async Task<List<LeanTranslationDto>> GetTranslationListByLanguageCodeAsync(string languageCode, LeanTranslationType type)
    {
        // 获取语言
        var language = await _languageRepository.AsQueryable()
            .FirstAsync(l => l.Code == languageCode);

        if (language == null)
        {
            throw new System.Exception($"语言不存在：{languageCode}");
        }

        // 获取翻译
        var list = await Repository.AsQueryable()
            .Where(t => t.LanguageId == language.Id && t.Type == type)
            .ToListAsync();

        return list.Select(MapToDto).ToList();
    }

    /// <inheritdoc/>
    public async Task<List<LeanTranslationDto>> ExportTranslationsAsync(LeanTranslationQueryDto queryDto)
    {
        var query = Repository.AsQueryable()
            .LeftJoin<LeanLanguage>((t, l) => t.LanguageId == l.Id)
            .Select((t, l) => new LeanTranslation
            {
                Id = t.Id,
                LanguageId = t.LanguageId,
                Key = t.Key,
                Value = t.Value,
                Type = t.Type,
                Remark = t.Remark,
                Language = l
            });

        // 构建查询条件
        if (queryDto.LanguageId.HasValue)
        {
            query = query.Where(t => t.LanguageId == queryDto.LanguageId.Value);
        }

        if (!string.IsNullOrEmpty(queryDto.Key))
        {
            query = query.Where(t => t.Key.Contains(queryDto.Key));
        }

        if (!string.IsNullOrEmpty(queryDto.Value))
        {
            query = query.Where(t => t.Value.Contains(queryDto.Value));
        }

        if (queryDto.Type.HasValue)
        {
            query = query.Where(t => t.Type == queryDto.Type.Value);
        }

        // 获取数据
        var list = await query.ToListAsync();

        // 转换为DTO
        return list.Select(MapToDto).ToList();
    }

    /// <inheritdoc/>
    protected override long GetId(LeanTranslationUpdateDto dto)
    {
        return dto.Id;
    }

    /// <inheritdoc/>
    protected override LeanTranslationDto MapToDto(LeanTranslation entity)
    {
        return new LeanTranslationDto
        {
            Id = entity.Id,
            LanguageId = entity.LanguageId,
            Key = entity.Key,
            Value = entity.Value,
            Type = entity.Type,
            Remark = entity.Remark,
            LanguageName = entity.Language?.Name
        };
    }

    /// <inheritdoc/>
    protected override LeanTranslation MapToEntity(LeanTranslationCreateDto dto)
    {
        return new LeanTranslation
        {
            LanguageId = dto.LanguageId,
            Key = dto.Key,
            Value = dto.Value,
            Type = dto.Type,
            Remark = dto.Remark
        };
    }

    /// <inheritdoc/>
    protected override void MapToEntity(LeanTranslationUpdateDto dto, LeanTranslation entity)
    {
        entity.LanguageId = dto.LanguageId;
        entity.Key = dto.Key;
        entity.Value = dto.Value;
        entity.Type = dto.Type;
        entity.Remark = dto.Remark;
    }
} 
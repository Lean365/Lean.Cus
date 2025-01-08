using System;
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
/// 配置服务实现
/// </summary>
public class LeanConfigService : LeanService<LeanConfig, LeanConfigDto, LeanConfigQueryDto, LeanConfigCreateDto, LeanConfigUpdateDto>, ILeanConfigService
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="repository">仓储</param>
    /// <param name="logger">日志记录器</param>
    public LeanConfigService(
        ILeanRepository<LeanConfig> repository,
        ILogger<LeanConfigService> logger)
        : base(repository, logger)
    {
    }

    /// <inheritdoc/>
    public async Task<bool> CheckConfigKeyUniqueAsync(string key, long? id = null)
    {
        var query = Repository.AsQueryable().Where(c => c.Key == key);
        if (id.HasValue)
        {
            query = query.Where(c => c.Id != id.Value);
        }

        return await query.AnyAsync();
    }

    /// <inheritdoc/>
    public async Task<List<LeanConfigDto>> ExportConfigsAsync(LeanConfigQueryDto queryDto)
    {
        var query = Repository.AsQueryable();

        // 构建查询条件
        if (!string.IsNullOrEmpty(queryDto.Name))
        {
            query = query.Where(c => c.Name.Contains(queryDto.Name));
        }

        if (!string.IsNullOrEmpty(queryDto.Key))
        {
            query = query.Where(c => c.Key.Contains(queryDto.Key));
        }

        if (queryDto.Type.HasValue)
        {
            query = query.Where(c => c.Type == queryDto.Type.Value);
        }

        if (queryDto.Status.HasValue)
        {
            query = query.Where(c => c.Status == queryDto.Status.Value);
        }

        // 获取数据
        var list = await query.ToListAsync();

        // 转换为DTO
        return list.Select(MapToDto).ToList();
    }

    /// <inheritdoc/>
    public async Task<(int Success, int Fail)> ImportConfigsAsync(List<LeanConfigCreateDto> dtos)
    {
        var success = 0;
        var fail = 0;

        foreach (var dto in dtos)
        {
            try
            {
                // 检查配置键是否唯一
                if (await CheckConfigKeyUniqueAsync(dto.Key))
                {
                    fail++;
                    Logger.LogWarning($"导入配置失败,配置键已存在: {dto.Key}");
                    continue;
                }

                // 验证配置值格式
                if (!await ValidateConfigValueAsync(dto.Type, dto.Value))
                {
                    fail++;
                    Logger.LogWarning($"导入配置失败,配置值格式不正确: {dto.Value}");
                    continue;
                }

                // 创建配置
                var entity = MapToEntity(dto);
                await Repository.InsertAsync(entity);
                success++;
            }
            catch (Exception ex)
            {
                fail++;
                Logger.LogError(ex, $"导入配置失败: {dto.Key}");
            }
        }

        return (success, fail);
    }

    /// <inheritdoc/>
    public Task<List<LeanConfigDto>> GetImportTemplateAsync()
    {
        // 返回示例数据
        var templates = new List<LeanConfigDto>
        {
            new LeanConfigDto
            {
                Name = "系统名称",
                Key = "sys.name",
                Value = "示例系统",
                Type = LeanConfigType.String,
                Status = LeanEnabledStatus.Enabled,
                Remark = "系统显示名称"
            },
            new LeanConfigDto
            {
                Name = "系统Logo",
                Key = "sys.logo",
                Value = "http://example.com/logo.png",
                Type = LeanConfigType.Image,
                Status = LeanEnabledStatus.Enabled,
                Remark = "系统Logo图片地址"
            }
        };

        return Task.FromResult(templates);
    }

    /// <inheritdoc/>
    public async Task<bool> BatchDeleteAsync(List<long> ids)
    {
        if (ids == null || !ids.Any())
        {
            return false;
        }

        var result = await Repository.DeleteAsync(c => ids.Contains(c.Id));
        return result > 0;
    }

    /// <inheritdoc/>
    public Task<bool> ValidateConfigValueAsync(LeanConfigType type, string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return Task.FromResult(false);
        }

        var result = type switch
        {
            LeanConfigType.String => true,
            LeanConfigType.Number => decimal.TryParse(value, out _),
            LeanConfigType.Boolean => bool.TryParse(value, out _),
            LeanConfigType.Json => ValidateJson(value),
            LeanConfigType.Date => DateTime.TryParse(value, out _),
            LeanConfigType.Image => Uri.TryCreate(value, UriKind.Absolute, out _),
            _ => false
        };

        return Task.FromResult(result);
    }

    private static bool ValidateJson(string value)
    {
        try
        {
            System.Text.Json.JsonDocument.Parse(value);
            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <inheritdoc/>
    protected override long GetId(LeanConfigUpdateDto dto)
    {
        return dto.Id;
    }

    /// <inheritdoc/>
    protected override LeanConfigDto MapToDto(LeanConfig entity)
    {
        return new LeanConfigDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Key = entity.Key,
            Value = entity.Value,
            Type = entity.Type,
            Status = entity.Status,
            Remark = entity.Remark
        };
    }

    /// <inheritdoc/>
    protected override LeanConfig MapToEntity(LeanConfigCreateDto dto)
    {
        return new LeanConfig
        {
            Name = dto.Name,
            Key = dto.Key,
            Value = dto.Value,
            Type = dto.Type,
            Status = dto.Status,
            Remark = dto.Remark
        };
    }

    /// <inheritdoc/>
    protected override void MapToEntity(LeanConfigUpdateDto dto, LeanConfig entity)
    {
        entity.Name = dto.Name;
        entity.Key = dto.Key;
        entity.Value = dto.Value;
        entity.Type = dto.Type;
        entity.Status = dto.Status;
        entity.Remark = dto.Remark;
    }
} 
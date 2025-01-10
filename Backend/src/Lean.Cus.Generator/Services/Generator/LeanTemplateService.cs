using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Generator.Dtos.Generator;
using Lean.Cus.Generator.Entities.Generator;
using Lean.Cus.Generator.IServices.Generator;
using Mapster;
using SqlSugar;

namespace Lean.Cus.Generator.Services.Generator;

/// <summary>
/// 模板服务实现
/// </summary>
public class LeanTemplateService : ILeanTemplateService
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="db">数据库实例</param>
    public LeanTemplateService(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 创建模板
    /// </summary>
    /// <param name="dto">模板DTO</param>
    /// <returns>创建结果</returns>
    public async Task<bool> CreateTemplateAsync(LeanTemplateDto dto)
    {
        var entity = dto.Adapt<LeanTemplate>();
        return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 更新模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <param name="dto">模板DTO</param>
    /// <returns>更新结果</returns>
    public async Task<bool> UpdateTemplateAsync(long id, LeanTemplateDto dto)
    {
        var entity = await _db.Queryable<LeanTemplate>().FirstAsync(x => x.Id == id);
        if (entity == null)
        {
            throw new Exception("模板不存在");
        }

        dto.Adapt(entity);
        return await _db.Updateable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 删除模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <returns>删除结果</returns>
    public async Task<bool> DeleteTemplateAsync(long id)
    {
        return await _db.Deleteable<LeanTemplate>().Where(x => x.Id == id).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 获取模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <returns>模板DTO</returns>
    public async Task<LeanTemplateDto> GetTemplateAsync(long id)
    {
        var entity = await _db.Queryable<LeanTemplate>().FirstAsync(x => x.Id == id);
        return entity?.Adapt<LeanTemplateDto>();
    }

    /// <summary>
    /// 获取模板列表
    /// </summary>
    /// <param name="templateType">模板类型</param>
    /// <returns>模板DTO列表</returns>
    public async Task<List<LeanTemplateDto>> GetTemplateListAsync(string templateType)
    {
        var query = _db.Queryable<LeanTemplate>();
        if (!string.IsNullOrEmpty(templateType))
        {
            query = query.Where(x => x.TemplateType == templateType);
        }
        var list = await query.OrderBy(x => x.Id).ToListAsync();
        return list.Adapt<List<LeanTemplateDto>>();
    }

    /// <summary>
    /// 预览模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <param name="tableId">表编号</param>
    /// <returns>预览结果</returns>
    public async Task<string> PreviewTemplateAsync(long id, long tableId)
    {
        var template = await _db.Queryable<LeanTemplate>().FirstAsync(x => x.Id == id);
        if (template == null)
        {
            throw new Exception("模板不存在");
        }

        // TODO: 实现模板预览逻辑
        return template.TemplateContent;
    }
} 
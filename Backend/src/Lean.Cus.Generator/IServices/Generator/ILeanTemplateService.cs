using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Generator.Dtos.Generator;

namespace Lean.Cus.Generator.IServices.Generator;

/// <summary>
/// 模板服务接口
/// </summary>
public interface ILeanTemplateService
{
    /// <summary>
    /// 创建模板
    /// </summary>
    /// <param name="dto">模板DTO</param>
    /// <returns>创建结果</returns>
    Task<bool> CreateTemplateAsync(LeanTemplateDto dto);

    /// <summary>
    /// 更新模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <param name="dto">模板DTO</param>
    /// <returns>更新结果</returns>
    Task<bool> UpdateTemplateAsync(long id, LeanTemplateDto dto);

    /// <summary>
    /// 删除模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <returns>删除结果</returns>
    Task<bool> DeleteTemplateAsync(long id);

    /// <summary>
    /// 获取模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <returns>模板DTO</returns>
    Task<LeanTemplateDto> GetTemplateAsync(long id);

    /// <summary>
    /// 获取模板列表
    /// </summary>
    /// <param name="templateType">模板类型</param>
    /// <returns>模板DTO列表</returns>
    Task<List<LeanTemplateDto>> GetTemplateListAsync(string templateType);

    /// <summary>
    /// 预览模板
    /// </summary>
    /// <param name="id">模板编号</param>
    /// <param name="tableId">表编号</param>
    /// <returns>预览结果</returns>
    Task<string> PreviewTemplateAsync(long id, long tableId);
} 
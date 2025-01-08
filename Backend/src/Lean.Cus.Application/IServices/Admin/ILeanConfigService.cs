using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Enums;
using Lean.Cus.Domain.Entities.Admin;

namespace Lean.Cus.Application.IServices.Admin;

/// <summary>
/// 配置服务接口
/// </summary>
public interface ILeanConfigService : ILeanService<LeanConfig, LeanConfigDto, LeanConfigQueryDto, LeanConfigCreateDto, LeanConfigUpdateDto>
{
    /// <summary>
    /// 检查配置键是否唯一
    /// </summary>
    /// <param name="key">配置键</param>
    /// <param name="id">配置ID</param>
    /// <returns>是否唯一</returns>
    Task<bool> CheckConfigKeyUniqueAsync(string key, long? id = null);

    /// <summary>
    /// 导出配置数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>配置数据</returns>
    Task<List<LeanConfigDto>> ExportConfigsAsync(LeanConfigQueryDto queryDto);

    /// <summary>
    /// 导入配置数据
    /// </summary>
    /// <param name="dtos">配置数据</param>
    /// <returns>导入结果</returns>
    Task<(int Success, int Fail)> ImportConfigsAsync(List<LeanConfigCreateDto> dtos);

    /// <summary>
    /// 获取导入模板数据
    /// </summary>
    /// <returns>模板数据</returns>
    Task<List<LeanConfigDto>> GetImportTemplateAsync();

    /// <summary>
    /// 批量删除配置
    /// </summary>
    /// <param name="ids">配置ID集合</param>
    /// <returns>操作结果</returns>
    Task<bool> BatchDeleteAsync(List<long> ids);

    /// <summary>
    /// 验证配置值格式
    /// </summary>
    /// <param name="type">配置类型</param>
    /// <param name="value">配置值</param>
    /// <returns>验证结果</returns>
    Task<bool> ValidateConfigValueAsync(LeanConfigType type, string value);
} 
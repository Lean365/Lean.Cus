using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Domain.Entities.Admin;

namespace Lean.Cus.Application.IServices.Admin;

/// <summary>
/// 语言服务接口
/// </summary>
public interface ILeanLanguageService : ILeanService<LeanLanguage, LeanLanguageDto, LeanLanguageQueryDto, LeanLanguageCreateDto, LeanLanguageUpdateDto>
{
    /// <summary>
    /// 检查语言编码是否唯一
    /// </summary>
    /// <param name="code">语言编码</param>
    /// <param name="id">语言ID</param>
    /// <returns>是否唯一</returns>
    Task<bool> CheckLanguageCodeUniqueAsync(string code, long? id = null);

    /// <summary>
    /// 导出语言数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>语言数据</returns>
    Task<List<LeanLanguageDto>> ExportLanguagesAsync(LeanLanguageQueryDto queryDto);
} 
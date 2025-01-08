using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Enums;
using Lean.Cus.Domain.Entities.Admin;

namespace Lean.Cus.Application.IServices.Admin;

/// <summary>
/// 翻译服务接口
/// </summary>
public interface ILeanTranslationService : ILeanService<LeanTranslation, LeanTranslationDto, LeanTranslationQueryDto, LeanTranslationCreateDto, LeanTranslationUpdateDto>
{
    /// <summary>
    /// 根据语言编码和翻译类型获取翻译列表
    /// </summary>
    /// <param name="languageCode">语言编码</param>
    /// <param name="type">翻译类型</param>
    /// <returns>翻译列表</returns>
    Task<List<LeanTranslationDto>> GetTranslationListByLanguageCodeAsync(string languageCode, LeanTranslationType type);

    /// <summary>
    /// 导出翻译数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>翻译数据</returns>
    Task<List<LeanTranslationDto>> ExportTranslationsAsync(LeanTranslationQueryDto queryDto);
} 
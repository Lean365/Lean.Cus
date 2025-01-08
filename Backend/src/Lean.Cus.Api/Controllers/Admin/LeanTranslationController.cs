using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Lean.Cus.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 翻译管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanTranslationController : ControllerBase
{
    private readonly ILeanTranslationService _translationService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="translationService">翻译服务</param>
    public LeanTranslationController(ILeanTranslationService translationService)
    {
        _translationService = translationService;
    }

    /// <summary>
    /// 获取翻译列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>翻译列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanTranslationDto>>> GetListAsync([FromQuery] LeanTranslationQueryDto queryDto)
    {
        var list = await _translationService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 根据语言编码和翻译类型获取翻译列表
    /// </summary>
    /// <param name="languageCode">语言编码</param>
    /// <param name="type">翻译类型</param>
    /// <returns>翻译列表</returns>
    [HttpGet("language/{languageCode}/type/{type}")]
    public async Task<ActionResult<List<LeanTranslationDto>>> GetTranslationListByLanguageCodeAsync(string languageCode, LeanTranslationType type)
    {
        var list = await _translationService.GetTranslationListByLanguageCodeAsync(languageCode, type);
        return Ok(list);
    }

    /// <summary>
    /// 获取翻译信息
    /// </summary>
    /// <param name="id">翻译ID</param>
    /// <returns>翻译信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanTranslationDto>> GetAsync(long id)
    {
        var translation = await _translationService.GetAsync(id);
        return Ok(translation);
    }

    /// <summary>
    /// 新增翻译
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanTranslationCreateDto createDto)
    {
        var result = await _translationService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 更新翻译
    /// </summary>
    /// <param name="id">翻译ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanTranslationUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不匹配");
        }

        var result = await _translationService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除翻译
    /// </summary>
    /// <param name="id">翻译ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        var result = await _translationService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 导出翻译数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>翻译数据</returns>
    [HttpGet("export")]
    public async Task<ActionResult<List<LeanTranslationDto>>> ExportAsync([FromQuery] LeanTranslationQueryDto queryDto)
    {
        var translations = await _translationService.ExportTranslationsAsync(queryDto);
        return Ok(translations);
    }
} 
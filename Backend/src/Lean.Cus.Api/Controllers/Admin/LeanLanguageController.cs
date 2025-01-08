using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 语言管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanLanguageController : ControllerBase
{
    private readonly ILeanLanguageService _languageService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="languageService">语言服务</param>
    public LeanLanguageController(ILeanLanguageService languageService)
    {
        _languageService = languageService;
    }

    /// <summary>
    /// 获取语言列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>语言列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanLanguageDto>>> GetListAsync([FromQuery] LeanLanguageQueryDto queryDto)
    {
        var list = await _languageService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 获取语言信息
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <returns>语言信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanLanguageDto>> GetAsync(long id)
    {
        var language = await _languageService.GetAsync(id);
        return Ok(language);
    }

    /// <summary>
    /// 新增语言
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanLanguageCreateDto createDto)
    {
        // 检查语言编码是否唯一
        if (await _languageService.CheckLanguageCodeUniqueAsync(createDto.Code))
        {
            return BadRequest("语言编码已存在");
        }

        var result = await _languageService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 更新语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanLanguageUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不匹配");
        }

        // 检查语言编码是否唯一
        if (await _languageService.CheckLanguageCodeUniqueAsync(updateDto.Code, id))
        {
            return BadRequest("语言编码已存在");
        }

        var result = await _languageService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除语言
    /// </summary>
    /// <param name="id">语言ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        var result = await _languageService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 导出语言数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>语言数据</returns>
    [HttpGet("export")]
    public async Task<ActionResult<List<LeanLanguageDto>>> ExportAsync([FromQuery] LeanLanguageQueryDto queryDto)
    {
        var languages = await _languageService.ExportLanguagesAsync(queryDto);
        return Ok(languages);
    }
} 
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Lean.Cus.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 配置管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanConfigController : ControllerBase
{
    private readonly ILeanConfigService _configService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="configService">配置服务</param>
    public LeanConfigController(ILeanConfigService configService)
    {
        _configService = configService;
    }

    /// <summary>
    /// 获取配置列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>配置列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanConfigDto>>> GetListAsync([FromQuery] LeanConfigQueryDto queryDto)
    {
        var list = await _configService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 获取配置信息
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>配置信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanConfigDto>> GetAsync(long id)
    {
        var config = await _configService.GetAsync(id);
        return Ok(config);
    }

    /// <summary>
    /// 新增配置
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanConfigCreateDto createDto)
    {
        // 检查配置键是否唯一
        if (await _configService.CheckConfigKeyUniqueAsync(createDto.Key))
        {
            return BadRequest("配置键已存在");
        }

        // 验证配置值格式
        if (!await _configService.ValidateConfigValueAsync(createDto.Type, createDto.Value))
        {
            return BadRequest("配置值格式不正确");
        }

        var result = await _configService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 更新配置
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanConfigUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不匹配");
        }

        // 检查配置键是否唯一
        if (await _configService.CheckConfigKeyUniqueAsync(updateDto.Key, id))
        {
            return BadRequest("配置键已存在");
        }

        // 验证配置值格式
        if (!await _configService.ValidateConfigValueAsync(updateDto.Type, updateDto.Value))
        {
            return BadRequest("配置值格式不正确");
        }

        var result = await _configService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除配置
    /// </summary>
    /// <param name="id">配置ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        var result = await _configService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 批量删除配置
    /// </summary>
    /// <param name="ids">配置ID集合</param>
    /// <returns>操作结果</returns>
    [HttpDelete("batch")]
    public async Task<ActionResult<bool>> BatchDeleteAsync([FromBody] List<long> ids)
    {
        var result = await _configService.BatchDeleteAsync(ids);
        return Ok(result);
    }

    /// <summary>
    /// 导出配置数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>配置数据</returns>
    [HttpGet("export")]
    public async Task<ActionResult<List<LeanConfigDto>>> ExportAsync([FromQuery] LeanConfigQueryDto queryDto)
    {
        var configs = await _configService.ExportConfigsAsync(queryDto);
        return Ok(configs);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <returns>导入模板</returns>
    [HttpGet("import/template")]
    public async Task<ActionResult<List<LeanConfigDto>>> GetImportTemplateAsync()
    {
        var template = await _configService.GetImportTemplateAsync();
        return Ok(template);
    }

    /// <summary>
    /// 导入配置数据
    /// </summary>
    /// <param name="dtos">配置数据</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    public async Task<ActionResult<(int Success, int Fail)>> ImportAsync([FromBody] List<LeanConfigCreateDto> dtos)
    {
        var result = await _configService.ImportConfigsAsync(dtos);
        return Ok(result);
    }

    /// <summary>
    /// 验证配置值格式
    /// </summary>
    /// <param name="type">配置类型</param>
    /// <param name="value">配置值</param>
    /// <returns>验证结果</returns>
    [HttpGet("validate")]
    public async Task<ActionResult<bool>> ValidateConfigValueAsync([FromQuery] LeanConfigType type, [FromQuery] string value)
    {
        var result = await _configService.ValidateConfigValueAsync(type, value);
        return Ok(result);
    }
} 
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 字典类型管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanDictTypeController : ControllerBase
{
    private readonly ILeanDictTypeService _dictTypeService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictTypeService">字典类型服务</param>
    public LeanDictTypeController(ILeanDictTypeService dictTypeService)
    {
        _dictTypeService = dictTypeService;
    }

    /// <summary>
    /// 获取字典类型列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>字典类型列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanDictTypeDto>>> GetListAsync([FromQuery] LeanDictTypeQueryDto queryDto)
    {
        var list = await _dictTypeService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 获取字典类型信息
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <returns>字典类型信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanDictTypeDto>> GetAsync(long id)
    {
        var dictType = await _dictTypeService.GetAsync(id);
        return Ok(dictType);
    }

    /// <summary>
    /// 新增字典类型
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanDictTypeCreateDto createDto)
    {
        // 检查字典类型编码是否唯一
        if (await _dictTypeService.CheckDictTypeCodeUniqueAsync(createDto.DictCode))
        {
            return BadRequest("字典类型编码已存在");
        }

        var result = await _dictTypeService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 更新字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanDictTypeUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不匹配");
        }

        // 检查字典类型编码是否唯一
        if (await _dictTypeService.CheckDictTypeCodeUniqueAsync(updateDto.DictCode, id))
        {
            return BadRequest("字典类型编码已存在");
        }

        var result = await _dictTypeService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除字典类型
    /// </summary>
    /// <param name="id">字典类型ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        var result = await _dictTypeService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 导出字典类型数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>字典类型数据</returns>
    [HttpGet("export")]
    public async Task<ActionResult<List<LeanDictTypeDto>>> ExportAsync([FromQuery] LeanDictTypeQueryDto queryDto)
    {
        var dictTypes = await _dictTypeService.ExportDictTypesAsync(queryDto);
        return Ok(dictTypes);
    }
} 
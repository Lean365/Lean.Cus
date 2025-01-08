using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 字典数据管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanDictDataController : ControllerBase
{
    private readonly ILeanDictDataService _dictDataService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="dictDataService">字典数据服务</param>
    public LeanDictDataController(ILeanDictDataService dictDataService)
    {
        _dictDataService = dictDataService;
    }

    /// <summary>
    /// 获取字典数据列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>字典数据列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanDictDataDto>>> GetListAsync([FromQuery] LeanDictDataQueryDto queryDto)
    {
        var list = await _dictDataService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 根据字典类型编码获取字典数据列表
    /// </summary>
    /// <param name="dictTypeCode">字典类型编码</param>
    /// <returns>字典数据列表</returns>
    [HttpGet("type/{dictTypeCode}")]
    public async Task<ActionResult<List<LeanDictDataDto>>> GetDictDataListByTypeCodeAsync(string dictTypeCode)
    {
        var list = await _dictDataService.GetDictDataListByTypeCodeAsync(dictTypeCode);
        return Ok(list);
    }

    /// <summary>
    /// 获取字典数据信息
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <returns>字典数据信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanDictDataDto>> GetAsync(long id)
    {
        var dictData = await _dictDataService.GetAsync(id);
        return Ok(dictData);
    }

    /// <summary>
    /// 新增字典数据
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanDictDataCreateDto createDto)
    {
        var result = await _dictDataService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 更新字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanDictDataUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不匹配");
        }

        var result = await _dictDataService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除字典数据
    /// </summary>
    /// <param name="id">字典数据ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        var result = await _dictDataService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 导出字典数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>字典数据</returns>
    [HttpGet("export")]
    public async Task<ActionResult<List<LeanDictDataDto>>> ExportAsync([FromQuery] LeanDictDataQueryDto queryDto)
    {
        var dictData = await _dictDataService.ExportDictDataAsync(queryDto);
        return Ok(dictData);
    }
} 
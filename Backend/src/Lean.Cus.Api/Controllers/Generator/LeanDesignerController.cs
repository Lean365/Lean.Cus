using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Cus.Generator.Dtos.Designer;
using Lean.Cus.Generator.IServices.Designer;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Generator;

/// <summary>
/// 设计控制器
/// </summary>
[ApiController]
[Route("api/generator/[controller]")]
public class LeanDesignerController : ControllerBase
{
    private readonly ILeanDesignerService _designerService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="designerService">设计服务</param>
    public LeanDesignerController(ILeanDesignerService designerService)
    {
        _designerService = designerService;
    }

    /// <summary>
    /// 创建表设计
    /// </summary>
    /// <param name="dto">表设计DTO</param>
    /// <returns>创建结果</returns>
    [HttpPost("table")]
    public async Task<bool> CreateTable([FromBody] LeanTableDesignerDto dto)
    {
        return await _designerService.CreateTableAsync(dto);
    }

    /// <summary>
    /// 更新表设计
    /// </summary>
    /// <param name="id">表编号</param>
    /// <param name="dto">表设计DTO</param>
    /// <returns>更新结果</returns>
    [HttpPut("table/{id}")]
    public async Task<bool> UpdateTable([FromRoute] long id, [FromBody] LeanTableDesignerDto dto)
    {
        return await _designerService.UpdateTableAsync(id, dto);
    }

    /// <summary>
    /// 删除表设计
    /// </summary>
    /// <param name="id">表编号</param>
    /// <returns>删除结果</returns>
    [HttpDelete("table/{id}")]
    public async Task<bool> DeleteTable([FromRoute] long id)
    {
        return await _designerService.DeleteTableAsync(id);
    }

    /// <summary>
    /// 获取表设计
    /// </summary>
    /// <param name="id">表编号</param>
    /// <returns>表设计DTO</returns>
    [HttpGet("table/{id}")]
    public async Task<LeanTableDesignerDto> GetTable([FromRoute] long id)
    {
        return await _designerService.GetTableAsync(id);
    }

    /// <summary>
    /// 获取表设计列表
    /// </summary>
    /// <returns>表设计列表</returns>
    [HttpGet("table/list")]
    public async Task<List<LeanTableDesignerDto>> GetTableList()
    {
        return await _designerService.GetTableListAsync();
    }

    /// <summary>
    /// 创建字段设计
    /// </summary>
    /// <param name="tableId">表编号</param>
    /// <param name="dto">字段设计DTO</param>
    /// <returns>创建结果</returns>
    [HttpPost("column/{tableId}")]
    public async Task<bool> CreateColumn([FromRoute] long tableId, [FromBody] LeanColumnDesignerDto dto)
    {
        return await _designerService.CreateColumnAsync(tableId, dto);
    }

    /// <summary>
    /// 更新字段设计
    /// </summary>
    /// <param name="id">字段编号</param>
    /// <param name="dto">字段设计DTO</param>
    /// <returns>更新结果</returns>
    [HttpPut("column/{id}")]
    public async Task<bool> UpdateColumn([FromRoute] long id, [FromBody] LeanColumnDesignerDto dto)
    {
        return await _designerService.UpdateColumnAsync(id, dto);
    }

    /// <summary>
    /// 删除字段设计
    /// </summary>
    /// <param name="id">字段编号</param>
    /// <returns>删除结果</returns>
    [HttpDelete("column/{id}")]
    public async Task<bool> DeleteColumn([FromRoute] long id)
    {
        return await _designerService.DeleteColumnAsync(id);
    }

    /// <summary>
    /// 获取字段设计
    /// </summary>
    /// <param name="id">字段编号</param>
    /// <returns>字段设计DTO</returns>
    [HttpGet("column/{id}")]
    public async Task<LeanColumnDesignerDto> GetColumn([FromRoute] long id)
    {
        return await _designerService.GetColumnAsync(id);
    }

    /// <summary>
    /// 获取字段设计列表
    /// </summary>
    /// <param name="tableId">表编号</param>
    /// <returns>字段设计列表</returns>
    [HttpGet("column/list/{tableId}")]
    public async Task<List<LeanColumnDesignerDto>> GetColumnList([FromRoute] long tableId)
    {
        return await _designerService.GetColumnListAsync(tableId);
    }
} 
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 岗位管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanPostController : ControllerBase
{
    private readonly ILeanPostService _postService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="postService">岗位服务</param>
    public LeanPostController(ILeanPostService postService)
    {
        _postService = postService;
    }

    /// <summary>
    /// 获取岗位列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>岗位列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanPostDto>>> GetListAsync([FromQuery] LeanPostQueryDto queryDto)
    {
        var list = await _postService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 获取岗位信息
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>岗位信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanPostDto>> GetAsync(long id)
    {
        var post = await _postService.GetAsync(id);
        return Ok(post);
    }

    /// <summary>
    /// 新增岗位
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanPostCreateDto createDto)
    {
        var result = await _postService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 修改岗位
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanPostUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不一致");
        }

        var result = await _postService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除岗位
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        var result = await _postService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 导入岗位
    /// </summary>
    /// <param name="importDtos">导入数据</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    public async Task<ActionResult<(int success, int fail)>> ImportAsync([FromBody] List<LeanPostImportDto> importDtos)
    {
        var result = await _postService.ImportAsync(importDtos);
        return Ok(result);
    }

    /// <summary>
    /// 导出岗位
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>导出数据</returns>
    [HttpGet("export")]
    public async Task<ActionResult<List<LeanPostExportDto>>> ExportAsync([FromQuery] LeanPostQueryDto queryDto)
    {
        var data = await _postService.ExportAsync(queryDto);
        return Ok(data);
    }

    /// <summary>
    /// 下载岗位导入模板
    /// </summary>
    /// <returns>模板数据</returns>
    [HttpGet("import-template")]
    public ActionResult<LeanPostImportTemplateDto> GetImportTemplateAsync()
    {
        return Ok(new LeanPostImportTemplateDto());
    }
} 
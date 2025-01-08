using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 用户岗位关联管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanUserPostController : ControllerBase
{
    private readonly ILeanUserPostService _userPostService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userPostService">用户岗位关联服务</param>
    public LeanUserPostController(ILeanUserPostService userPostService)
    {
        _userPostService = userPostService;
    }

    /// <summary>
    /// 获取用户岗位关联列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>用户岗位关联列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanUserPostDto>>> GetListAsync([FromQuery] LeanUserPostQueryDto queryDto)
    {
        var list = await _userPostService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 获取用户岗位关联信息
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>用户岗位关联信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanUserPostDto>> GetAsync(long id)
    {
        var userPost = await _userPostService.GetAsync(id);
        return Ok(userPost);
    }

    /// <summary>
    /// 新增用户岗位关联
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanUserPostCreateDto createDto)
    {
        var result = await _userPostService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 修改用户岗位关联
    /// </summary>
    /// <param name="id">ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanUserPostUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不一致");
        }

        var result = await _userPostService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除用户岗位关联
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        var result = await _userPostService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 批量创建用户岗位关联
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="postIds">岗位ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("batch")]
    public async Task<ActionResult<bool>> BatchCreateAsync([FromQuery] long userId, [FromBody] List<long> postIds)
    {
        var result = await _userPostService.BatchCreateAsync(userId, postIds);
        return Ok(result);
    }

    /// <summary>
    /// 批量删除用户岗位关联
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("batch")]
    public async Task<ActionResult<bool>> BatchDeleteAsync([FromQuery] long userId)
    {
        var result = await _userPostService.BatchDeleteByUserIdAsync(userId);
        return Ok(result);
    }

    /// <summary>
    /// 获取用户的岗位ID列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>岗位ID列表</returns>
    [HttpGet("posts")]
    public async Task<ActionResult<List<long>>> GetPostIdsAsync([FromQuery] long userId)
    {
        var postIds = await _userPostService.GetPostIdsByUserIdAsync(userId);
        return Ok(postIds);
    }

    /// <summary>
    /// 获取岗位的用户ID列表
    /// </summary>
    /// <param name="postId">岗位ID</param>
    /// <returns>用户ID列表</returns>
    [HttpGet("users")]
    public async Task<ActionResult<List<long>>> GetUserIdsAsync([FromQuery] long postId)
    {
        var userIds = await _userPostService.GetUserIdsByPostIdAsync(postId);
        return Ok(userIds);
    }

    /// <summary>
    /// 导入用户岗位关联
    /// </summary>
    /// <param name="importDtos">导入数据</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    public async Task<ActionResult<(int success, int fail)>> ImportAsync([FromBody] List<LeanUserPostImportDto> importDtos)
    {
        var result = await _userPostService.ImportAsync(importDtos);
        return Ok(result);
    }

    /// <summary>
    /// 导出用户岗位关联
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>导出数据</returns>
    [HttpGet("export")]
    public async Task<ActionResult<List<LeanUserPostExportDto>>> ExportAsync([FromQuery] LeanUserPostQueryDto queryDto)
    {
        var data = await _userPostService.ExportAsync(queryDto);
        return Ok(data);
    }

    /// <summary>
    /// 下载用户岗位关联导入模板
    /// </summary>
    /// <returns>模板数据</returns>
    [HttpGet("import-template")]
    public ActionResult<LeanUserPostImportTemplateDto> GetImportTemplateAsync()
    {
        return Ok(new LeanUserPostImportTemplateDto());
    }
} 
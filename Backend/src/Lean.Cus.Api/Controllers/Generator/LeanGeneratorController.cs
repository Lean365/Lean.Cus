using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Generator.IServices.Generator;
using Microsoft.AspNetCore.Mvc;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Api.Controllers.Generator;

/// <summary>
/// 代码生成控制器
/// </summary>
[Route("api/generator")]
[ApiController]
public class LeanGeneratorController : ControllerBase
{
    private readonly ILeanGeneratorService _generatorService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="generatorService">代码生成服务</param>
    public LeanGeneratorController(ILeanGeneratorService generatorService)
    {
        _generatorService = generatorService;
    }

    /// <summary>
    /// 预览代码
    /// </summary>
    /// <param name="tableId">表编号</param>
    /// <returns>预览结果</returns>
    [HttpGet("preview/{tableId}")]
    public async Task<ActionResult<LeanApiResult<Dictionary<string, string>>>> PreviewCode([FromRoute] long tableId)
    {
        var result = await _generatorService.PreviewCodeAsync(tableId);
        return Ok(LeanApiResult<Dictionary<string, string>>.Ok(result));
    }

    /// <summary>
    /// 生成代码
    /// </summary>
    /// <param name="tableId">表编号</param>
    /// <returns>生成结果</returns>
    [HttpPost("generate/{tableId}")]
    public async Task<ActionResult<LeanApiResult<string>>> GenerateCode([FromRoute] long tableId)
    {
        var result = await _generatorService.GenerateCodeAsync(tableId);
        return Ok(LeanApiResult<string>.Ok(result));
    }

    /// <summary>
    /// 获取模板列表
    /// </summary>
    /// <returns>模板列表</returns>
    [HttpGet("templates")]
    public async Task<ActionResult<LeanApiResult<List<string>>>> GetTemplates()
    {
        var result = await _generatorService.GetTemplatesAsync();
        return Ok(LeanApiResult<List<string>>.Ok(result));
    }

    /// <summary>
    /// 获取前端模板列表
    /// </summary>
    /// <returns>前端模板列表</returns>
    [HttpGet("frontend-templates")]
    public async Task<ActionResult<LeanApiResult<List<string>>>> GetFrontendTemplates()
    {
        var result = await _generatorService.GetFrontendTemplatesAsync();
        return Ok(LeanApiResult<List<string>>.Ok(result));
    }

    /// <summary>
    /// 同步数据库
    /// </summary>
    /// <param name="tableId">表编号</param>
    /// <returns>是否成功</returns>
    [HttpPost("sync/{tableId}")]
    public async Task<ActionResult<LeanApiResult>> SyncDatabase([FromRoute] long tableId)
    {
        var result = await _generatorService.SyncDatabaseAsync(tableId);
        return Ok(result ? LeanApiResult.Ok("同步成功") : LeanApiResult.Fail("同步失败"));
    }
} 
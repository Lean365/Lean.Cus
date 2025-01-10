using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Generator.IServices.Generator;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<Dictionary<string, string>> PreviewCode([FromRoute] long tableId)
    {
        return await _generatorService.PreviewCodeAsync(tableId);
    }

    /// <summary>
    /// 生成代码
    /// </summary>
    /// <param name="tableId">表编号</param>
    /// <returns>生成结果</returns>
    [HttpPost("generate/{tableId}")]
    public async Task<string> GenerateCode([FromRoute] long tableId)
    {
        return await _generatorService.GenerateCodeAsync(tableId);
    }

    /// <summary>
    /// 获取模板列表
    /// </summary>
    /// <returns>模板列表</returns>
    [HttpGet("templates")]
    public async Task<List<string>> GetTemplates()
    {
        return await _generatorService.GetTemplatesAsync();
    }

    /// <summary>
    /// 获取前端模板列表
    /// </summary>
    /// <returns>前端模板列表</returns>
    [HttpGet("frontend-templates")]
    public async Task<List<string>> GetFrontendTemplates()
    {
        return await _generatorService.GetFrontendTemplatesAsync();
    }

    /// <summary>
    /// 同步数据库
    /// </summary>
    /// <param name="tableId">表编号</param>
    /// <returns>是否成功</returns>
    [HttpPost("sync/{tableId}")]
    public async Task<bool> SyncDatabase([FromRoute] long tableId)
    {
        return await _generatorService.SyncDatabaseAsync(tableId);
    }
} 
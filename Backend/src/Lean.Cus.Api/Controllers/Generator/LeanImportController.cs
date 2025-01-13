using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Generator.Dtos.Import;
using Lean.Cus.Generator.IServices.Import;
using Microsoft.AspNetCore.Mvc;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Api.Controllers.Generator;

/// <summary>
/// 导入控制器
/// </summary>
[ApiController]
[Route("api/generator/[controller]")]
public class LeanImportController : ControllerBase
{
    private readonly ILeanImportService _importService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="importService">导入服务</param>
    public LeanImportController(ILeanImportService importService)
    {
        _importService = importService;
    }

    /// <summary>
    /// 获取数据库表列表
    /// </summary>
    /// <returns>表名列表</returns>
    [HttpGet("tables")]
    public async Task<ActionResult<LeanApiResult<List<LeanTableImportDto>>>> GetTables()
    {
        var result = await _importService.GetTablesAsync();
        return Ok(LeanApiResult<List<LeanTableImportDto>>.Ok(result));
    }

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>字段列表</returns>
    [HttpGet("columns/{tableName}")]
    public async Task<ActionResult<LeanApiResult<List<LeanColumnImportDto>>>> GetColumns([FromRoute] string tableName)
    {
        var result = await _importService.GetColumnsAsync(tableName);
        return Ok(LeanApiResult<List<LeanColumnImportDto>>.Ok(result));
    }

    /// <summary>
    /// 导入到代码生成器
    /// </summary>
    /// <param name="tableNames">表名列表</param>
    /// <returns>是否成功</returns>
    [HttpPost("import-to-generator")]
    public async Task<ActionResult<LeanApiResult>> ImportToGenerator([FromBody] List<string> tableNames)
    {
        var result = await _importService.ImportToGeneratorAsync(tableNames);
        return Ok(result ? LeanApiResult.Ok("导入成功") : LeanApiResult.Fail("导入失败"));
    }
} 
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Generator.Dtos.Import;
using Lean.Cus.Generator.IServices.Import;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<List<string>> GetTables()
    {
        return await _importService.GetTablesAsync();
    }

    /// <summary>
    /// 获取表字段列表
    /// </summary>
    /// <param name="tableName">表名</param>
    /// <returns>字段列表</returns>
    [HttpGet("columns/{tableName}")]
    public async Task<List<LeanColumnImportDto>> GetColumns([FromRoute] string tableName)
    {
        return await _importService.GetColumnsAsync(tableName);
    }

    /// <summary>
    /// 导入到代码生成器
    /// </summary>
    /// <param name="tableNames">表名列表</param>
    /// <returns>是否成功</returns>
    [HttpPost("import-to-generator")]
    public async Task<bool> ImportToGenerator([FromBody] List<string> tableNames)
    {
        return await _importService.ImportToGeneratorAsync(tableNames);
    }
} 
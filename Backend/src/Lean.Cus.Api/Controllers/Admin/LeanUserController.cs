using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Application.Dtos.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Common.Paging;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 用户管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
public class LeanUserController : ControllerBase
{
    private readonly ILeanUserService _userService;

    /// <summary>
    /// 初始化用户控制器
    /// </summary>
    /// <param name="userService">用户服务</param>
    public LeanUserController(ILeanUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LeanUserDto>> Add([FromBody] LeanUserDto user)
    {
        var result = await _userService.AddAsync(user);
        return Ok(result);
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<bool>> Update([FromBody] LeanUserDto user)
    {
        var result = await _userService.UpdateAsync(user);
        return Ok(result);
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(long id)
    {
        var result = await _userService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 获取用户
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanUserDto>> Get(long id)
    {
        var result = await _userService.GetAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<LeanUserDto>>> GetList([FromQuery] LeanUserQueryDto query)
    {
        var result = await _userService.GetListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 分页查询用户
    /// </summary>
    [HttpGet("page")]
    public async Task<ActionResult<PagedResults<LeanUserDto>>> GetPageList([FromQuery] LeanUserQueryDto query)
    {
        var result = await _userService.GetPagedListAsync(query);
        return Ok(result);
    }

    /// <summary>
    /// 导入用户数据
    /// </summary>
    [HttpPost("import")]
    public async Task<ActionResult<(List<LeanUserDto> SuccessItems, List<string> ErrorMessages)>> Import([FromForm] IFormFile file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var result = await _userService.ImportAsync(stream.ToArray());
        return Ok(result);
    }

    /// <summary>
    /// 导出用户数据
    /// </summary>
    [HttpGet("export")]
    public async Task<FileResult> Export([FromQuery] LeanUserQueryDto query)
    {
        var result = await _userService.ExportAsync(query);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("import/template")]
    public async Task<FileResult> GetImportTemplate()
    {
        var result = await _userService.GetImportTemplateAsync();
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "user_import_template.xlsx");
    }
} 
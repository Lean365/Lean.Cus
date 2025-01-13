using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Application.Dtos.Admin;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Common.Paging;
using System.IO;
using Microsoft.AspNetCore.Http;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 菜单管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
public class LeanMenuController : ControllerBase
{
    private readonly ILeanMenuService _menuService;

    /// <summary>
    /// 初始化菜单控制器
    /// </summary>
    /// <param name="menuService">菜单服务</param>
    public LeanMenuController(ILeanMenuService menuService)
    {
        _menuService = menuService;
    }

    /// <summary>
    /// 新增菜单
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<LeanMenuDto>>> Add([FromBody] LeanMenuDto menu)
    {
        var result = await _menuService.CreateAsync(menu);
        return Ok(LeanApiResult<LeanMenuDto>.Ok(result));
    }

    /// <summary>
    /// 更新菜单
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<LeanApiResult>> Update([FromBody] LeanMenuDto menu)
    {
        var result = await _menuService.UpdateAsync(menu);
        return Ok(result ? LeanApiResult.Ok("更新成功") : LeanApiResult.Fail("更新失败"));
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> Delete(long id)
    {
        var result = await _menuService.DeleteAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取菜单
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanMenuDto>>> Get(long id)
    {
        var result = await _menuService.GetAsync(id);
        return Ok(LeanApiResult<LeanMenuDto>.Ok(result));
    }

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanMenuDto>>>> GetList([FromQuery] LeanMenuQueryDto query)
    {
        var result = await _menuService.GetListAsync(query);
        return Ok(LeanApiResult<List<LeanMenuDto>>.Ok(result));
    }

    /// <summary>
    /// 分页查询菜单
    /// </summary>
    [HttpGet("page")]
    public async Task<ActionResult<LeanApiResult<PagedResults<LeanMenuDto>>>> GetPageList([FromQuery] LeanMenuQueryDto query)
    {
        var result = await _menuService.GetPagedListAsync(query);
        return Ok(LeanApiResult<PagedResults<LeanMenuDto>>.Ok(result));
    }

    /// <summary>
    /// 导入菜单数据
    /// </summary>
    [HttpPost("import")]
    public async Task<ActionResult<LeanApiResult<(List<LeanMenuDto> SuccessItems, List<string> ErrorMessages)>>> Import(IFormFile file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var result = await _menuService.ImportAsync(stream.ToArray());
        return Ok(LeanApiResult<(List<LeanMenuDto> SuccessItems, List<string> ErrorMessages)>.Ok(result));
    }

    /// <summary>
    /// 导出菜单数据
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] LeanMenuQueryDto query)
    {
        var result = await _menuService.ExportAsync(query);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "menus.xlsx");
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("import/template")]
    public async Task<IActionResult> GetImportTemplate()
    {
        var result = await _menuService.GetImportTemplateAsync();
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "menu_import_template.xlsx");
    }

    /// <summary>
    /// 获取用户菜单树
    /// </summary>
    [HttpGet("user/{userId}/tree")]
    public async Task<ActionResult<LeanApiResult<List<LeanMenuDto>>>> GetUserMenuTree(long userId)
    {
        var result = await _menuService.GetUserMenuTreeAsync(userId);
        return Ok(LeanApiResult<List<LeanMenuDto>>.Ok(result));
    }

    /// <summary>
    /// 获取角色菜单树
    /// </summary>
    [HttpGet("role/{roleId}/tree")]
    public async Task<ActionResult<LeanApiResult<List<LeanMenuDto>>>> GetRoleMenuTree(long roleId)
    {
        var result = await _menuService.GetRoleMenuTreeAsync(roleId);
        return Ok(LeanApiResult<List<LeanMenuDto>>.Ok(result));
    }

    /// <summary>
    /// 获取菜单树
    /// </summary>
    [HttpGet("tree")]
    public async Task<ActionResult<LeanApiResult<List<LeanMenuDto>>>> GetMenuTree()
    {
        var result = await _menuService.GetMenuTreeAsync();
        return Ok(LeanApiResult<List<LeanMenuDto>>.Ok(result));
    }

    /// <summary>
    /// 更新菜单状态
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<LeanApiResult>> UpdateStatus(long id, [FromBody] LeanMenuStatusUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(LeanApiResult.Fail("菜单ID不匹配"));
        }
        var result = await _menuService.UpdateStatusAsync(dto);
        return Ok(result ? LeanApiResult.Ok("更新状态成功") : LeanApiResult.Fail("更新状态失败"));
    }

    /// <summary>
    /// 更新菜单排序
    /// </summary>
    [HttpPut("{id}/sort")]
    public async Task<ActionResult<LeanApiResult>> UpdateSort(long id, [FromBody] LeanMenuSortDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(LeanApiResult.Fail("菜单ID不匹配"));
        }
        var result = await _menuService.UpdateSortAsync(dto);
        return Ok(result ? LeanApiResult.Ok("更新排序成功") : LeanApiResult.Fail("更新排序失败"));
    }
} 
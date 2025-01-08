using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 菜单管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanMenuController : ControllerBase
{
    private readonly ILeanMenuService _menuService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="menuService">菜单服务</param>
    public LeanMenuController(ILeanMenuService menuService)
    {
        _menuService = menuService;
    }

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>菜单列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanMenuDto>>> GetListAsync([FromQuery] LeanMenuQueryDto queryDto)
    {
        var list = await _menuService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 获取菜单树形结构
    /// </summary>
    /// <returns>菜单树形结构</returns>
    [HttpGet("tree")]
    public async Task<ActionResult<List<LeanMenuDto>>> GetTreeAsync()
    {
        var tree = await _menuService.GetMenuTreeAsync();
        return Ok(tree);
    }

    /// <summary>
    /// 获取当前用户菜单树
    /// </summary>
    /// <returns>菜单树形结构</returns>
    [HttpGet("user-menu")]
    public async Task<ActionResult<List<LeanMenuDto>>> GetUserMenuTreeAsync()
    {
        var userId = User.GetUserId();
        var tree = await _menuService.GetUserMenuTreeAsync(userId);
        return Ok(tree);
    }

    /// <summary>
    /// 获取菜单信息
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>菜单信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanMenuDto>> GetAsync(long id)
    {
        var menu = await _menuService.GetAsync(id);
        return Ok(menu);
    }

    /// <summary>
    /// 新增菜单
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanMenuCreateDto createDto)
    {
        var result = await _menuService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanMenuUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不匹配");
        }

        var result = await _menuService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        // 检查是否存在子菜单
        var children = await _menuService.GetChildrenAsync(id);
        if (children.Count > 0)
        {
            return BadRequest("存在子菜单，无法删除");
        }

        var result = await _menuService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 修改菜单状态
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <param name="status">状态</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<bool>> UpdateStatusAsync(long id, [FromBody] LeanEnabledStatus status)
    {
        var result = await _menuService.UpdateStatusAsync(id, status);
        return Ok(result);
    }

    /// <summary>
    /// 获取角色菜单树
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>菜单树形结构</returns>
    [HttpGet("role/{roleId}")]
    public async Task<ActionResult<List<LeanMenuDto>>> GetRoleMenuTreeAsync(long roleId)
    {
        var tree = await _menuService.GetRoleMenuTreeAsync(roleId);
        return Ok(tree);
    }
} 
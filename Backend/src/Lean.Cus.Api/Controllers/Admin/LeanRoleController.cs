using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 角色管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanRoleController : ControllerBase
{
    private readonly ILeanRoleService _roleService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="roleService">角色服务</param>
    public LeanRoleController(ILeanRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>角色列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanRoleDto>>> GetListAsync([FromQuery] LeanRoleQueryDto queryDto)
    {
        var list = await _roleService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 获取角色信息
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <returns>角色信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanRoleDto>> GetAsync(long id)
    {
        var role = await _roleService.GetAsync(id);
        return Ok(role);
    }

    /// <summary>
    /// 新增角色
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanRoleCreateDto createDto)
    {
        // 检查角色编码是否唯一
        if (await _roleService.CheckRoleCodeExistsAsync(createDto.RoleKey))
        {
            return BadRequest("角色编码已存在");
        }

        var result = await _roleService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanRoleUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不匹配");
        }

        // 检查角色编码是否唯一
        if (await _roleService.CheckRoleCodeExistsAsync(updateDto.RoleKey, id))
        {
            return BadRequest("角色编码已存在");
        }

        var result = await _roleService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        // 检查是否为系统角色
        var role = await _roleService.GetAsync(id);
        if (role.IsSystem)
        {
            return BadRequest("系统角色不能删除");
        }

        var result = await _roleService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 修改角色状态
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <param name="status">状态</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<bool>> UpdateStatusAsync(long id, [FromBody] LeanEnabledStatus status)
    {
        var result = await _roleService.UpdateStatusAsync(id, status);
        return Ok(result);
    }

    /// <summary>
    /// 分配角色菜单权限
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <param name="menuIds">菜单ID列表</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}/menus")]
    public async Task<ActionResult<bool>> AssignMenusAsync(long id, [FromBody] List<long> menuIds)
    {
        var result = await _roleService.AssignMenusAsync(id, menuIds);
        return Ok(result);
    }

    /// <summary>
    /// 获取角色菜单ID列表
    /// </summary>
    /// <param name="id">角色ID</param>
    /// <returns>菜单ID列表</returns>
    [HttpGet("{id}/menus")]
    public async Task<ActionResult<List<long>>> GetRoleMenuIdsAsync(long id)
    {
        var menuIds = await _roleService.GetRoleMenuIdsAsync(id);
        return Ok(menuIds);
    }

    /// <summary>
    /// 导出角色数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>角色数据</returns>
    [HttpGet("export")]
    public async Task<ActionResult<List<LeanRoleDto>>> ExportAsync([FromQuery] LeanRoleQueryDto queryDto)
    {
        var roles = await _roleService.ExportRolesAsync(queryDto);
        return Ok(roles);
    }
} 
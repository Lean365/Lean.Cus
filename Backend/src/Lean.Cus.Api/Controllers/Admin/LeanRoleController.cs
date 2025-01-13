using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Application.Dtos.Admin;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Lean.Cus.Common.Paging;
using Lean.Cus.Common.Models;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 角色管理控制器
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class LeanRoleController : ControllerBase
{
    private readonly ILeanRoleService _roleService;

    /// <summary>
    /// 初始化角色控制器
    /// </summary>
    public LeanRoleController(ILeanRoleService roleService)
    {
        _roleService = roleService;
    }

    /// <summary>
    /// 新增角色
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<LeanRoleDto>>> Add([FromBody] LeanRoleDto dto)
    {
        var result = await _roleService.CreateAsync(dto);
        return Ok(LeanApiResult<LeanRoleDto>.Ok(result));
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<LeanApiResult>> Update([FromBody] LeanRoleDto dto)
    {
        var result = await _roleService.UpdateAsync(dto);
        return Ok(result ? LeanApiResult.Ok("更新成功") : LeanApiResult.Fail("更新失败"));
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> Delete(long id)
    {
        var result = await _roleService.DeleteAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取角色
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanRoleDto>>> Get(long id)
    {
        var result = await _roleService.GetAsync(id);
        return Ok(LeanApiResult<LeanRoleDto>.Ok(result));
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanRoleDto>>>> GetList([FromQuery] LeanRoleQueryDto queryDto)
    {
        var result = await _roleService.GetListAsync(queryDto);
        return Ok(LeanApiResult<List<LeanRoleDto>>.Ok(result));
    }

    /// <summary>
    /// 分页查询角色
    /// </summary>
    [HttpGet("paged")]
    public async Task<ActionResult<LeanApiResult<PagedResults<LeanRoleDto>>>> GetPagedList([FromQuery] LeanRoleQueryDto queryDto)
    {
        var result = await _roleService.GetPagedListAsync(queryDto);
        return Ok(LeanApiResult<PagedResults<LeanRoleDto>>.Ok(result));
    }

    /// <summary>
    /// 导入角色数据
    /// </summary>
    [HttpPost("import")]
    public async Task<ActionResult<LeanApiResult<(List<LeanRoleDto> SuccessItems, List<string> ErrorMessages)>>> Import([FromForm] IFormFile file)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        var result = await _roleService.ImportAsync(ms.ToArray());
        return Ok(LeanApiResult<(List<LeanRoleDto> SuccessItems, List<string> ErrorMessages)>.Ok(result));
    }

    /// <summary>
    /// 导出角色数据
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] LeanRoleQueryDto queryDto)
    {
        var result = await _roleService.ExportAsync(queryDto);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "roles.xlsx");
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("import-template")]
    public async Task<IActionResult> GetImportTemplate()
    {
        var result = await _roleService.GetImportTemplateAsync();
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "role_import_template.xlsx");
    }

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<LeanApiResult<List<LeanRoleDto>>>> GetUserRoles(long userId)
    {
        var result = await _roleService.GetUserRolesAsync(userId);
        return Ok(LeanApiResult<List<LeanRoleDto>>.Ok(result));
    }

    /// <summary>
    /// 分配角色权限
    /// </summary>
    [HttpPost("{roleId}/permissions")]
    public async Task<ActionResult<LeanApiResult>> AssignPermissions(long roleId, [FromBody] List<long> permissionIds)
    {
        var result = await _roleService.AssignPermissionsAsync(roleId, permissionIds);
        return Ok(result ? LeanApiResult.Ok("分配权限成功") : LeanApiResult.Fail("分配权限失败"));
    }

    /// <summary>
    /// 分配角色部门
    /// </summary>
    [HttpPost("{roleId}/departments")]
    public async Task<ActionResult<LeanApiResult>> AssignDepartments(long roleId, [FromBody] List<long> departmentIds)
    {
        var result = await _roleService.AssignDepartmentsAsync(roleId, departmentIds);
        return Ok(result ? LeanApiResult.Ok("分配部门成功") : LeanApiResult.Fail("分配部门失败"));
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<LeanApiResult>> UpdateStatus(long id, [FromBody] LeanRoleStatusUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(LeanApiResult.Fail("角色ID不匹配"));
        }
        var result = await _roleService.UpdateStatusAsync(dto);
        return Ok(result ? LeanApiResult.Ok("更新状态成功") : LeanApiResult.Fail("更新状态失败"));
    }

    /// <summary>
    /// 分配角色菜单
    /// </summary>
    [HttpPut("{id}/menus")]
    public async Task<ActionResult<LeanApiResult>> AssignMenus(long id, [FromBody] List<long> menuIds)
    {
        var result = await _roleService.AssignMenusAsync(id, menuIds);
        return Ok(result ? LeanApiResult.Ok("分配菜单成功") : LeanApiResult.Fail("分配菜单失败"));
    }
} 
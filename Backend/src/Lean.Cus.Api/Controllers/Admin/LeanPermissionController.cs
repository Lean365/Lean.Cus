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
/// 权限管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
public class LeanPermissionController : ControllerBase
{
    private readonly ILeanPermissionService _permissionService;

    /// <summary>
    /// 初始化权限控制器
    /// </summary>
    /// <param name="permissionService">权限服务</param>
    public LeanPermissionController(ILeanPermissionService permissionService)
    {
        _permissionService = permissionService;
    }

    /// <summary>
    /// 新增权限
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<LeanPermissionDto>>> Add([FromBody] LeanPermissionDto permission)
    {
        var result = await _permissionService.CreateAsync(permission);
        return Ok(LeanApiResult<LeanPermissionDto>.Ok(result));
    }

    /// <summary>
    /// 更新权限
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<LeanApiResult>> Update([FromBody] LeanPermissionDto permission)
    {
        var result = await _permissionService.UpdateAsync(permission);
        return Ok(result ? LeanApiResult.Ok("更新成功") : LeanApiResult.Fail("更新失败"));
    }

    /// <summary>
    /// 删除权限
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> Delete(long id)
    {
        var result = await _permissionService.DeleteAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取权限
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanPermissionDto>>> Get(long id)
    {
        var result = await _permissionService.GetAsync(id);
        return Ok(LeanApiResult<LeanPermissionDto>.Ok(result));
    }

    /// <summary>
    /// 获取权限列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanPermissionDto>>>> GetList([FromQuery] LeanPermissionQueryDto query)
    {
        var result = await _permissionService.GetListAsync(query);
        return Ok(LeanApiResult<List<LeanPermissionDto>>.Ok(result));
    }

    /// <summary>
    /// 分页查询权限
    /// </summary>
    [HttpGet("page")]
    public async Task<ActionResult<LeanApiResult<PagedResults<LeanPermissionDto>>>> GetPageList([FromQuery] LeanPermissionQueryDto query)
    {
        var result = await _permissionService.GetPagedListAsync(query);
        return Ok(LeanApiResult<PagedResults<LeanPermissionDto>>.Ok(result));
    }

    /// <summary>
    /// 导入权限数据
    /// </summary>
    [HttpPost("import")]
    public async Task<ActionResult<LeanApiResult<(List<LeanPermissionDto> SuccessItems, List<string> ErrorMessages)>>> Import(IFormFile file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var result = await _permissionService.ImportAsync(stream.ToArray());
        return Ok(LeanApiResult<(List<LeanPermissionDto> SuccessItems, List<string> ErrorMessages)>.Ok(result));
    }

    /// <summary>
    /// 导出权限数据
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] LeanPermissionQueryDto query)
    {
        var result = await _permissionService.ExportAsync(query);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "permissions.xlsx");
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("import/template")]
    public async Task<IActionResult> GetImportTemplate()
    {
        var result = await _permissionService.GetImportTemplateAsync();
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "permission_import_template.xlsx");
    }

    /// <summary>
    /// 获取用户权限列表
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<LeanApiResult<List<string>>>> GetUserPermissions(long userId)
    {
        var result = await _permissionService.GetUserPermissionsAsync(userId);
        return Ok(LeanApiResult<List<string>>.Ok(result));
    }

    /// <summary>
    /// 获取角色权限列表
    /// </summary>
    [HttpGet("role/{roleId}")]
    public async Task<ActionResult<LeanApiResult<List<string>>>> GetRolePermissions(long roleId)
    {
        var result = await _permissionService.GetRolePermissionsAsync(roleId);
        return Ok(LeanApiResult<List<string>>.Ok(result));
    }
} 
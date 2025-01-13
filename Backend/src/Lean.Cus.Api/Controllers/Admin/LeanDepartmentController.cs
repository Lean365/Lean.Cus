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
/// 部门管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
public class LeanDepartmentController : ControllerBase
{
    private readonly ILeanDepartmentService _departmentService;

    /// <summary>
    /// 初始化部门控制器
    /// </summary>
    /// <param name="departmentService">部门服务</param>
    public LeanDepartmentController(ILeanDepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    /// <summary>
    /// 新增部门
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<LeanApiResult<LeanDepartmentDto>>> Add([FromBody] LeanDepartmentDto department)
    {
        var result = await _departmentService.CreateAsync(department);
        return Ok(LeanApiResult<LeanDepartmentDto>.Ok(result));
    }

    /// <summary>
    /// 更新部门
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<LeanApiResult>> Update([FromBody] LeanDepartmentDto department)
    {
        var result = await _departmentService.UpdateAsync(department);
        return Ok(result ? LeanApiResult.Ok("更新成功") : LeanApiResult.Fail("更新失败"));
    }

    /// <summary>
    /// 删除部门
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> Delete(long id)
    {
        var result = await _departmentService.DeleteAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取部门
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanDepartmentDto>>> Get(long id)
    {
        var result = await _departmentService.GetAsync(id);
        return Ok(LeanApiResult<LeanDepartmentDto>.Ok(result));
    }

    /// <summary>
    /// 获取部门列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanDepartmentDto>>>> GetList([FromQuery] LeanDepartmentQueryDto query)
    {
        var result = await _departmentService.GetListAsync(query);
        return Ok(LeanApiResult<List<LeanDepartmentDto>>.Ok(result));
    }

    /// <summary>
    /// 分页查询部门
    /// </summary>
    [HttpGet("page")]
    public async Task<ActionResult<LeanApiResult<PagedResults<LeanDepartmentDto>>>> GetPageList([FromQuery] LeanDepartmentQueryDto query)
    {
        var result = await _departmentService.GetPagedListAsync(query);
        return Ok(LeanApiResult<PagedResults<LeanDepartmentDto>>.Ok(result));
    }

    /// <summary>
    /// 导入部门数据
    /// </summary>
    [HttpPost("import")]
    public async Task<ActionResult<LeanApiResult<(List<LeanDepartmentDto> SuccessItems, List<string> ErrorMessages)>>> Import(IFormFile file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var result = await _departmentService.ImportAsync(stream.ToArray());
        return Ok(LeanApiResult<(List<LeanDepartmentDto> SuccessItems, List<string> ErrorMessages)>.Ok(result));
    }

    /// <summary>
    /// 导出部门数据
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] LeanDepartmentQueryDto query)
    {
        var result = await _departmentService.ExportAsync(query);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "departments.xlsx");
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("import/template")]
    public async Task<IActionResult> GetImportTemplate()
    {
        var result = await _departmentService.GetImportTemplateAsync();
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "department_import_template.xlsx");
    }

    /// <summary>
    /// 获取用户部门列表
    /// </summary>
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<LeanApiResult<List<LeanDepartmentDto>>>> GetUserDepartments(long userId)
    {
        var result = await _departmentService.GetUserDepartmentsAsync(userId);
        return Ok(LeanApiResult<List<LeanDepartmentDto>>.Ok(result));
    }

    /// <summary>
    /// 获取角色部门列表
    /// </summary>
    [HttpGet("role/{roleId}")]
    public async Task<ActionResult<LeanApiResult<List<LeanDepartmentDto>>>> GetRoleDepartments(long roleId)
    {
        var result = await _departmentService.GetRoleDepartmentsAsync(roleId);
        return Ok(LeanApiResult<List<LeanDepartmentDto>>.Ok(result));
    }

    /// <summary>
    /// 获取部门树
    /// </summary>
    [HttpGet("tree")]
    public async Task<ActionResult<LeanApiResult<List<LeanDepartmentDto>>>> GetDepartmentTree()
    {
        var result = await _departmentService.GetDepartmentTreeAsync();
        return Ok(LeanApiResult<List<LeanDepartmentDto>>.Ok(result));
    }

    /// <summary>
    /// 更新部门状态
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<LeanApiResult>> UpdateStatus(long id, [FromBody] LeanDepartmentStatusUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(LeanApiResult.Fail("部门ID不匹配"));
        }
        var result = await _departmentService.UpdateStatusAsync(dto);
        return Ok(result ? LeanApiResult.Ok("更新状态成功") : LeanApiResult.Fail("更新状态失败"));
    }
} 
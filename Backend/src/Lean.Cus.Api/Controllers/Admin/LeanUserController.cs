using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Paging;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<LeanApiResult<LeanUserDto>>> Add([FromBody] LeanUserDto user)
    {
        var result = await _userService.CreateAsync(user);
        return Ok(LeanApiResult<LeanUserDto>.Ok(result));
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    [HttpPut]
    public async Task<ActionResult<LeanApiResult>> Update([FromBody] LeanUserDto user)
    {
        var result = await _userService.UpdateAsync(user);
        return Ok(result ? LeanApiResult.Ok("更新成功") : LeanApiResult.Fail("更新失败"));
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult<LeanApiResult>> Delete(long id)
    {
        var result = await _userService.DeleteAsync(id);
        return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
    }

    /// <summary>
    /// 获取用户
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanApiResult<LeanUserDto>>> Get(long id)
    {
        var result = await _userService.GetAsync(id);
        return Ok(LeanApiResult<LeanUserDto>.Ok(result));
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<LeanApiResult<List<LeanUserDto>>>> GetList([FromQuery] LeanUserQueryDto query)
    {
        var result = await _userService.GetListAsync(query);
        return Ok(LeanApiResult<List<LeanUserDto>>.Ok(result));
    }

    /// <summary>
    /// 分页查询用户
    /// </summary>
    [HttpGet("page")]
    public async Task<ActionResult<LeanApiResult<PagedResults<LeanUserDto>>>> GetPageList([FromQuery] LeanUserQueryDto query)
    {
        var result = await _userService.GetPagedListAsync(query);
        return Ok(LeanApiResult<PagedResults<LeanUserDto>>.Ok(result));
    }

    /// <summary>
    /// 导入用户数据
    /// </summary>
    [HttpPost("import")]
    public async Task<ActionResult<LeanApiResult<(List<LeanUserDto> SuccessItems, List<string> ErrorMessages)>>> Import(IFormFile file)
    {
        using var stream = new MemoryStream();
        await file.CopyToAsync(stream);
        var result = await _userService.ImportAsync(stream.ToArray());
        return Ok(LeanApiResult<(List<LeanUserDto> SuccessItems, List<string> ErrorMessages)>.Ok(result));
    }

    /// <summary>
    /// 导出用户数据
    /// </summary>
    [HttpGet("export")]
    public async Task<IActionResult> Export([FromQuery] LeanUserQueryDto query)
    {
        var result = await _userService.ExportAsync(query);
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "users.xlsx");
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    [HttpGet("import/template")]
    public async Task<IActionResult> GetImportTemplate()
    {
        var result = await _userService.GetImportTemplateAsync();
        return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "user_import_template.xlsx");
    }

    /// <summary>
    /// 分配用户角色
    /// </summary>
    [HttpPost("{userId}/roles")]
    public async Task<ActionResult<LeanApiResult>> AssignRoles(long userId, [FromBody] List<long> roleIds)
    {
        var result = await _userService.AssignRolesAsync(userId, roleIds);
        return Ok(result ? LeanApiResult.Ok("分配角色成功") : LeanApiResult.Fail("分配角色失败"));
    }

    /// <summary>
    /// 分配用户部门
    /// </summary>
    [HttpPost("{userId}/departments")]
    public async Task<ActionResult<LeanApiResult>> AssignDepartments(long userId, [FromBody] List<long> departmentIds)
    {
        var result = await _userService.AssignDepartmentsAsync(userId, departmentIds);
        return Ok(result ? LeanApiResult.Ok("分配部门成功") : LeanApiResult.Fail("分配部门失败"));
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    [HttpPost("{userId}/reset-password")]
    public async Task<ActionResult<LeanApiResult<string>>> ResetPassword([FromBody] LeanUserResetPasswordDto input)
    {
        var result = await _userService.ResetPasswordAsync(input);
        return Ok(LeanApiResult<string>.Ok(result));
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    [HttpPost("change-password")]
    public async Task<ActionResult<LeanApiResult>> ChangePassword([FromBody] LeanUserChangePasswordDto dto)
    {
        await _userService.ChangePasswordAsync(dto);
        return Ok(LeanApiResult.Ok("修改密码成功"));
    }

    /// <summary>
    /// 更新用户状态
    /// </summary>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<LeanApiResult>> UpdateStatus(long id, [FromBody] LeanUserStatusUpdateDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest(LeanApiResult.Fail("用户ID不匹配"));
        }
        var result = await _userService.UpdateStatusAsync(dto);
        return Ok(result ? LeanApiResult.Ok("更新状态成功") : LeanApiResult.Fail("更新状态失败"));
    }
}
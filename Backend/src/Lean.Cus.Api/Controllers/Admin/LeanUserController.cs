using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 用户管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanUserController : ControllerBase
{
    private readonly ILeanUserService _userService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="userService">用户服务</param>
    public LeanUserController(ILeanUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>用户列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanUserDto>>> GetListAsync([FromQuery] LeanUserQueryDto queryDto)
    {
        var list = await _userService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>用户信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanUserDto>> GetAsync(long id)
    {
        var user = await _userService.GetAsync(id);
        return Ok(user);
    }

    /// <summary>
    /// 获取当前用户信息
    /// </summary>
    /// <returns>用户信息</returns>
    [HttpGet("info")]
    public async Task<ActionResult<LeanUserDto>> GetCurrentUserInfoAsync()
    {
        // 从Token中获取用户ID
        var userId = User.GetUserId();
        var user = await _userService.GetUserInfoAsync(userId);
        return Ok(user);
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanUserCreateDto createDto)
    {
        // 检查用户名是否唯一
        if (await _userService.CheckUserNameExistsAsync(createDto.UserName))
        {
            return BadRequest("用户名已存在");
        }

        // 检查手机号是否唯一
        if (!string.IsNullOrEmpty(createDto.Phone) && await _userService.CheckPhoneExistsAsync(createDto.Phone))
        {
            return BadRequest("手机号已存在");
        }

        // 检查邮箱是否唯一
        if (!string.IsNullOrEmpty(createDto.Email) && await _userService.CheckEmailExistsAsync(createDto.Email))
        {
            return BadRequest("邮箱已存在");
        }

        var result = await _userService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanUserUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不匹配");
        }

        // 检查手机号是否唯一
        if (!string.IsNullOrEmpty(updateDto.Phone) && await _userService.CheckPhoneExistsAsync(updateDto.Phone, id))
        {
            return BadRequest("手机号已存在");
        }

        // 检查邮箱是否唯一
        if (!string.IsNullOrEmpty(updateDto.Email) && await _userService.CheckEmailExistsAsync(updateDto.Email, id))
        {
            return BadRequest("邮箱已存在");
        }

        var result = await _userService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        // 检查是否为当前用户
        var currentUserId = User.GetUserId();
        if (id == currentUserId)
        {
            return BadRequest("不能删除当前用户");
        }

        var result = await _userService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}/reset-password")]
    public async Task<ActionResult<bool>> ResetPasswordAsync(long id)
    {
        var result = await _userService.ResetPasswordAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="oldPassword">旧密码</param>
    /// <param name="newPassword">新密码</param>
    /// <returns>操作结果</returns>
    [HttpPut("change-password")]
    public async Task<ActionResult<bool>> ChangePasswordAsync([FromBody] ChangePasswordDto dto)
    {
        var userId = User.GetUserId();
        var result = await _userService.ChangePasswordAsync(userId, dto.OldPassword, dto.NewPassword);
        return Ok(result);
    }

    /// <summary>
    /// 修改用户状态
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="status">状态</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<bool>> UpdateStatusAsync(long id, [FromBody] LeanEnabledStatus status)
    {
        var result = await _userService.UpdateStatusAsync(id, status);
        return Ok(result);
    }

    /// <summary>
    /// 导入用户数据
    /// </summary>
    /// <param name="users">用户数据列表</param>
    /// <returns>操作结果</returns>
    [HttpPost("import")]
    public async Task<ActionResult<bool>> ImportAsync([FromBody] List<LeanUserDto> users)
    {
        var result = await _userService.ImportUsersAsync(users);
        return Ok(result);
    }

    /// <summary>
    /// 导出用户数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>用户数据</returns>
    [HttpGet("export")]
    public async Task<ActionResult<List<LeanUserDto>>> ExportAsync([FromQuery] LeanUserQueryDto queryDto)
    {
        var users = await _userService.ExportUsersAsync(queryDto);
        return Ok(users);
    }

    /// <summary>
    /// 下载用户导入模板
    /// </summary>
    /// <returns>模板数据</returns>
    [HttpGet("import-template")]
    public ActionResult<LeanUserImportTemplateDto> GetImportTemplateAsync()
    {
        return Ok(new LeanUserImportTemplateDto());
    }
} 
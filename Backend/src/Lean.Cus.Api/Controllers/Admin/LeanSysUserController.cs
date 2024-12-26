//===================================================
// 项目名称：Lean.Cus.Api.Controllers.Admin
// 文件名称：LeanSysUserController
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：系统用户控制器
//===================================================

using Microsoft.AspNetCore.Mvc;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.Services.Auth;
using Lean.Cus.Api.Authorization;
using Lean.Cus.Api.Attributes;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Security;
using Lean.Cus.Domain.Entities.Admin;

using Lean.Cus.Common.Enums;
using SqlSugar;
using Lean.Cus.Common.Exceptions;
using Lean.Cus.Common.Results;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 系统用户管理控制器
/// </summary>
[ApiController]
[Route("api/[controller]")]
[LeanAuthorize("Admin")]
public class LeanSysUserController : ControllerBase
{
    private readonly ISqlSugarClient _db;

    public LeanSysUserController(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 获取用户分���列表
    /// </summary>
    [HttpGet("page")]
    [LeanPermission("admin:sysuser:query")]
    [LeanOperLog("查询用户列表", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<LeanPageResult<LeanSysUserBaseDto>>> GetPage([FromQuery] LeanPage page, [FromQuery] LeanSysUserQueryDto query)
    {
        if (page == null || page.Current < 1 || page.Size < 1)
        {
            throw new LeanException("分页参数错误", LeanErrorCodeEnum.ValidationError);
        }

        if (query == null)
        {
            query = new LeanSysUserQueryDto();
        }

        RefAsync<int> total = 0;
        var records = await _db.Queryable<LeanSysUser>()
            .WhereIF(!string.IsNullOrEmpty(query.Keyword), u =>
                (!string.IsNullOrEmpty(u.UserCode) && u.UserCode.Contains(query.Keyword)) ||
                (!string.IsNullOrEmpty(u.UserName) && u.UserName.Contains(query.Keyword)) ||
                (!string.IsNullOrEmpty(u.NickName) && u.NickName.Contains(query.Keyword)) ||
                (!string.IsNullOrEmpty(u.PhoneNumber) && u.PhoneNumber.Contains(query.Keyword)))
            .WhereIF(query.UserType.HasValue, u => u.UserType == query.UserType.Value)
            .WhereIF(query.Status.HasValue, u => u.Status == query.Status.Value)
            .WhereIF(query.StartTime.HasValue, u => u.CreateTime >= query.StartTime.Value)
            .WhereIF(query.EndTime.HasValue, u => u.CreateTime <= query.EndTime.Value)
            .OrderByDescending(u => u.CreateTime)
            .Select<LeanSysUserBaseDto>()
            .ToPageListAsync(page.Current, page.Size, total);

        return LeanApiResult<LeanPageResult<LeanSysUserBaseDto>>.Ok(new LeanPageResult<LeanSysUserBaseDto>
        {
            Total = total,
            Records = records
        });
    }

    /// <summary>
    /// 获取用户详情
    /// </summary>
    [HttpGet("{id}")]
    [LeanPermission("admin:sysuser:query")]
    [LeanOperLog("查询用户详情", LeanBusinessTypeEnum.Query)]
    public async Task<LeanApiResult<LeanSysUserBaseDto>> Get(long id)
    {
        if (id < 1)
        {
            throw new LeanException("ID不能小于1", LeanErrorCodeEnum.ValidationError);
        }

        var user = await _db.Queryable<LeanSysUser>()
            .Where(u => u.Id == id)
            .Select<LeanSysUserBaseDto>()
            .FirstAsync();
            
        if (user == null)
        {
            throw new LeanException("用户不存在", LeanErrorCodeEnum.NotFound);
        }

        return LeanApiResult<LeanSysUserBaseDto>.Ok(user);
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    [HttpPost]
    [LeanPermission("admin:sysuser:insert")]
    [LeanOperLog("创建用户", LeanBusinessTypeEnum.Insert)]
    public async Task<LeanApiResult<LeanSysUserBaseDto>> Create([FromBody] LeanSysUserCreateDto dto)
    {
        var exists = await _db.Queryable<LeanSysUser>().AnyAsync(u => u.UserName == dto.UserName);
        if (exists)
        {
            throw new LeanException("用户名已存在", LeanErrorCodeEnum.ValidationError);
        }

        var (hash, salt) = LeanSecurityHelper.CreatePasswordHash(dto.Password);

        var user = new LeanSysUser
        {
            Id = await _db.Queryable<LeanSysUser>().MaxAsync(u => u.Id) + 1,
            UserCode = dto.UserCode,
            UserName = dto.UserName,
            NickName = dto.NickName,
            EnglishName = dto.EnglishName,
            UserType = dto.UserType,
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            Gender = dto.Gender,
            Avatar = dto.Avatar,
            Status = dto.Status,
            CreateTime = DateTime.Now,
            Password = hash,
            Salt = salt
        };

        var success = await _db.Insertable(user).ExecuteCommandAsync() > 0;
        if (!success)
        {
            throw new LeanException("创建用户失败", LeanErrorCodeEnum.DbError);
        }

        var result = await _db.Queryable<LeanSysUser>()
            .Where(u => u.Id == user.Id)
            .Select<LeanSysUserBaseDto>()
            .FirstAsync();

        return LeanApiResult<LeanSysUserBaseDto>.Ok(result);
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    [HttpPut]
    [LeanPermission("admin:sysuser:update")]
    [LeanOperLog("更新用户", LeanBusinessTypeEnum.Update)]
    public async Task<LeanApiResult<bool>> Update([FromBody] LeanSysUserUpdateDto dto)
    {
        if (dto.Id < 1)
        {
            throw new LeanException("ID不能小于1", LeanErrorCodeEnum.ValidationError);
        }

        var exists = await _db.Queryable<LeanSysUser>().AnyAsync(u => u.Id == dto.Id);
        if (!exists)
        {
            throw new LeanException("用户不存在", LeanErrorCodeEnum.NotFound);
        }

        var user = await _db.Queryable<LeanSysUser>().FirstAsync(u => u.Id == dto.Id);
        user.UserCode = dto.UserCode;
        user.UserName = dto.UserName;
        user.NickName = dto.NickName;
        user.EnglishName = dto.EnglishName;
        user.UserType = dto.UserType;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;
        user.Gender = dto.Gender;
        user.Avatar = dto.Avatar;
        user.Status = dto.Status;
        user.UpdateTime = DateTime.Now;

        var success = await _db.Updateable(user)
            .IgnoreColumns(u => new { u.Password, u.Salt, u.CreateTime })
            .ExecuteCommandAsync() > 0;

        if (!success)
        {
            throw new LeanException("更新用户失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    [HttpDelete("{id}")]
    [LeanPermission("admin:sysuser:delete")]
    [LeanOperLog("删除用户", LeanBusinessTypeEnum.Delete)]
    public async Task<LeanApiResult<bool>> Delete(long id)
    {
        if (id < 1)
        {
            throw new LeanException("ID不能小于1", LeanErrorCodeEnum.ValidationError);
        }

        var user = await _db.Queryable<LeanSysUser>().FirstAsync(u => u.Id == id);
        if (user == null)
        {
            throw new LeanException("用户不存在", LeanErrorCodeEnum.NotFound);
        }

        if (user.UserType == 0)
        {
            throw new LeanException("超级管理员不能删除", LeanErrorCodeEnum.BusinessError);
        }

        var success = await _db.Deleteable<LeanSysUser>().Where(u => u.Id == id).ExecuteCommandAsync() > 0;
        if (!success)
        {
            throw new LeanException("删除用户失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    [HttpPut("password")]
    [LeanPermission("admin:sysuser:update")]
    [LeanOperLog("修改密码", LeanBusinessTypeEnum.Update)]
    public async Task<LeanApiResult<bool>> UpdatePassword([FromBody] LeanSysUserPasswordDto dto)
    {
        if (dto.Id < 1)
        {
            throw new LeanException("ID不能小于1", LeanErrorCodeEnum.ValidationError);
        }

        var user = await _db.Queryable<LeanSysUser>().FirstAsync(u => u.Id == dto.Id);
        if (user == null)
        {
            throw new LeanException("用户不存在", LeanErrorCodeEnum.NotFound);
        }

        if (!LeanSecurityHelper.VerifyPassword(dto.OldPassword, user.Salt, user.Password))
        {
            throw new LeanException("旧密码不正确", LeanErrorCodeEnum.ValidationError);
        }

        var (hash, salt) = LeanSecurityHelper.CreatePasswordHash(dto.NewPassword);
        user.Password = hash;
        user.Salt = salt;
        user.UpdateTime = DateTime.Now;

        var success = await _db.Updateable(user)
            .UpdateColumns(u => new { u.Password, u.Salt, u.UpdateTime })
            .ExecuteCommandAsync() > 0;

        if (!success)
        {
            throw new LeanException("修改密码失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    [HttpPut("reset-password")]
    [LeanPermission("admin:sysuser:update")]
    [LeanOperLog("重置密码", LeanBusinessTypeEnum.Update)]
    public async Task<LeanApiResult<bool>> ResetPassword([FromBody] LeanSysUserResetPasswordDto dto)
    {
        if (dto.Id < 1)
        {
            throw new LeanException("ID不能小于1", LeanErrorCodeEnum.ValidationError);
        }

        var user = await _db.Queryable<LeanSysUser>().FirstAsync(u => u.Id == dto.Id);
        if (user == null)
        {
            throw new LeanException("用户不存在", LeanErrorCodeEnum.NotFound);
        }

        var (hash, salt) = LeanSecurityHelper.CreatePasswordHash(dto.NewPassword);
        user.Password = hash;
        user.Salt = salt;
        user.UpdateTime = DateTime.Now;

        var success = await _db.Updateable(user)
            .UpdateColumns(u => new { u.Password, u.Salt, u.UpdateTime })
            .ExecuteCommandAsync() > 0;

        if (!success)
        {
            throw new LeanException("重置密码失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }

    /// <summary>
    /// 修改状态
    /// </summary>
    [HttpPut("status")]
    [LeanPermission("admin:sysuser:update")]
    [LeanOperLog("修改用户状态", LeanBusinessTypeEnum.Update)]
    public async Task<LeanApiResult<bool>> UpdateStatus([FromBody] LeanSysUserStatusDto dto)
    {
        if (dto.Id < 1)
        {
            throw new LeanException("ID不能小于1", LeanErrorCodeEnum.ValidationError);
        }

        var user = await _db.Queryable<LeanSysUser>().FirstAsync(u => u.Id == dto.Id);
        if (user == null)
        {
            throw new LeanException("用户不存在", LeanErrorCodeEnum.NotFound);
        }

        if (user.UserType == 0 && dto.Status == 0)
        {
            throw new LeanException("超级管理员不能禁用", LeanErrorCodeEnum.BusinessError);
        }

        user.Status = dto.Status;
        user.UpdateTime = DateTime.Now;

        var success = await _db.Updateable(user)
            .UpdateColumns(u => new { u.Status, u.UpdateTime })
            .ExecuteCommandAsync() > 0;

        if (!success)
        {
            throw new LeanException("修改状态失败", LeanErrorCodeEnum.DbError);
        }

        return LeanApiResult<bool>.Ok(true);
    }
} 
//===================================================
// 项目名称：Lean.Cus.Application
// 文件名称：LeanSysUserService
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.14
// 更新时间：2024.01.14
// 备注说明：系统用户服务实现
//===================================================

using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Results;
using Lean.Cus.Domain.IRepositories.Admin;
using SqlSugar;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 系统用户服务实现
/// </summary>
public class LeanSysUserService : ILeanSysUserService
{
    private readonly ILeanSysUserRepository _userRepository;
    private readonly ISqlSugarClient _db;

    public LeanSysUserService(ILeanSysUserRepository userRepository, ISqlSugarClient db)
    {
        _userRepository = userRepository;
        _db = db;
    }

    /// <summary>
    /// 获取用户分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页列表</returns>
    public async Task<LeanApiResult<LeanPageResult<LeanSysUserBaseDto>>> GetPageAsync(LeanSysUserQueryDto query)
    {
        if (query == null)
        {
            return LeanApiResult<LeanPageResult<LeanSysUserBaseDto>>.Fail("查询条件不能为空");
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
            .ToPageListAsync(query.Current, query.Size, total);

        return LeanApiResult<LeanPageResult<LeanSysUserBaseDto>>.Ok(new LeanPageResult<LeanSysUserBaseDto>
        {
            Total = total,
            Records = records
        });
    }

    /// <summary>
    /// 获取用户详情
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>用户详情</returns>
    public async Task<LeanApiResult<LeanSysUserBaseDto>> GetAsync(long id)
    {
        var user = await _db.Queryable<LeanSysUser>()
            .Where(u => u.Id == id)
            .Select<LeanSysUserBaseDto>()
            .FirstAsync();

        return user == null ? LeanApiResult<LeanSysUserBaseDto>.NotFound("用户不存在") : LeanApiResult<LeanSysUserBaseDto>.Ok(user);
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="dto">用户信息</param>
    /// <returns>创建结果</returns>
    public async Task<LeanApiResult<LeanSysUserBaseDto>> CreateAsync(LeanSysUserCreateDto dto)
    {
        // 检查用户名是否存在
        if (await _userRepository.ExistsUserNameAsync(dto.UserName))
        {
            return LeanApiResult<LeanSysUserBaseDto>.Fail("用户名已存在");
        }

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
            Salt = Guid.NewGuid().ToString("N")[..8]
        };
        user.Password = (dto.Password + user.Salt).ToLower();

        if (!await _userRepository.CreateAsync(user))
        {
            return LeanApiResult<LeanSysUserBaseDto>.Fail("创建用户失败");
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
    /// <param name="dto">用户信息</param>
    /// <returns>更新结果</returns>
    public async Task<LeanApiResult<bool>> UpdateAsync(LeanSysUserUpdateDto dto)
    {
        var user = await _userRepository.GetAsync(dto.Id);
        if (user == null)
        {
            return LeanApiResult<bool>.NotFound("用户不存在");
        }

        // 检查用户名是否存在
        if (await _userRepository.ExistsUserNameAsync(dto.UserName, dto.Id))
        {
            return LeanApiResult<bool>.Fail("用户名已存在");
        }

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

        return await _userRepository.UpdateAsync(user)
            ? LeanApiResult<bool>.Ok(true)
            : LeanApiResult<bool>.Fail("更新用户失败");
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>删除结果</returns>
    public async Task<LeanApiResult<bool>> DeleteAsync(long id)
    {
        return await _userRepository.DeleteAsync(id)
            ? LeanApiResult<bool>.Ok(true)
            : LeanApiResult<bool>.NotFound("用户不存在");
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="dto">密码信息</param>
    /// <returns>修改结果</returns>
    public async Task<LeanApiResult<bool>> UpdatePasswordAsync(LeanSysUserPasswordDto dto)
    {
        var user = await _userRepository.GetAsync(dto.Id);
        if (user == null)
        {
            return LeanApiResult<bool>.NotFound("用户不存在");
        }

        // 验证旧密码
        if ((dto.OldPassword + user.Salt).ToLower() != user.Password)
        {
            return LeanApiResult<bool>.Fail("旧密码不正确");
        }

        // 更新密码
        var salt = Guid.NewGuid().ToString("N")[..8];
        var password = (dto.NewPassword + salt).ToLower();

        return await _userRepository.UpdatePasswordAsync(dto.Id, password, salt)
            ? LeanApiResult<bool>.Ok(true)
            : LeanApiResult<bool>.Fail("修改密码失败");
    }

    /// <summary>
    /// 重��密码
    /// </summary>
    /// <param name="dto">密码信息</param>
    /// <returns>重置结果</returns>
    public async Task<LeanApiResult<bool>> ResetPasswordAsync(LeanSysUserResetPasswordDto dto)
    {
        var user = await _userRepository.GetAsync(dto.Id);
        if (user == null)
        {
            return LeanApiResult<bool>.NotFound("用户不存在");
        }

        // 重置密码
        var salt = Guid.NewGuid().ToString("N")[..8];
        var password = (dto.NewPassword + salt).ToLower();

        return await _userRepository.UpdatePasswordAsync(dto.Id, password, salt)
            ? LeanApiResult<bool>.Ok(true)
            : LeanApiResult<bool>.Fail("重置密码失败");
    }

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="dto">状态信息</param>
    /// <returns>修改结果</returns>
    public async Task<LeanApiResult<bool>> UpdateStatusAsync(LeanSysUserStatusDto dto)
    {
        var user = await _userRepository.GetAsync(dto.Id);
        if (user == null)
        {
            return LeanApiResult<bool>.NotFound("用户不存在");
        }

        return await _userRepository.UpdateStatusAsync(dto.Id, dto.Status)
            ? LeanApiResult<bool>.Ok(true)
            : LeanApiResult<bool>.Fail("修改状态失败");
    }
}
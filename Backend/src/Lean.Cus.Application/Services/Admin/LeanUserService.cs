using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Common.Excel;
using Lean.Cus.Common.Paging;
using System.IO;
using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Application.Dtos.Admin;
using Mapster;
using SqlSugar;
using System;
using Lean.Cus.Common.Security;
using Lean.Cus.Common.Exceptions;
using System.Linq.Expressions;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 用户服务实现
/// </summary>
public class LeanUserService : ILeanUserService
{
    private readonly ILeanRepository<LeanUser> _userRepository;

    public LeanUserService(ILeanRepository<LeanUser> userRepository)
    {
        _userRepository = userRepository;
    }

    /// <summary>
    /// 新增用户
    /// </summary>
    public async Task<LeanUserDto> AddAsync(LeanUserDto userDto)
    {
        var createDto = userDto.Adapt<LeanUserCreateDto>();
        
        // 验证密码强度
        var (isValid, message) = LeanPasswordHelper.ValidatePasswordStrength(createDto.Password);
        if (!isValid)
            throw new LeanUserFriendlyException(message);

        // 生成盐值和加密密码
        var salt = LeanPasswordHelper.GenerateSalt();
        var encryptedPassword = LeanPasswordHelper.EncryptPassword(createDto.Password, salt);

        var user = createDto.Adapt<LeanUser>();
        user.Password = encryptedPassword;
        user.Salt = salt;

        await _userRepository.InsertAsync(user);
        return user.Adapt<LeanUserDto>();
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    public async Task<bool> UpdateAsync(LeanUserDto userDto)
    {
        var user = userDto.Adapt<LeanUser>();
        return await _userRepository.UpdateAsync(user) > 0;
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _userRepository.DeleteAsync(u => u.Id == id) > 0;
    }

    /// <summary>
    /// 获取用户
    /// </summary>
    public async Task<LeanUserDto> GetAsync(long id)
    {
        var user = await _userRepository.GetAsync(x => x.Id == id);
        return user.Adapt<LeanUserDto>();
    }

    /// <summary>
    /// 获取用户列表
    /// </summary>
    public async Task<List<LeanUserDto>> GetListAsync(LeanUserQueryDto queryDto)
    {
        var users = await _userRepository.GetListAsync(u =>
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (string.IsNullOrEmpty(queryDto.Name) || u.RealName.Contains(queryDto.Name)) &&
            (string.IsNullOrEmpty(queryDto.Mobile) || u.Mobile.Contains(queryDto.Mobile)) &&
            (string.IsNullOrEmpty(queryDto.Email) || u.Email.Contains(queryDto.Email)) &&
            (!queryDto.Status.HasValue || u.Status == queryDto.Status.Value));
        return users.Adapt<List<LeanUserDto>>();
    }

    /// <summary>
    /// 分页查询用户
    /// </summary>
    public async Task<PagedResults<LeanUserDto>> GetPagedListAsync(LeanUserQueryDto queryDto)
    {
        RefAsync<int> total = 0;
        var list = await _userRepository.GetPageListAsync(u =>
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (string.IsNullOrEmpty(queryDto.Name) || u.RealName.Contains(queryDto.Name)) &&
            (string.IsNullOrEmpty(queryDto.Mobile) || u.Mobile.Contains(queryDto.Mobile)) &&
            (string.IsNullOrEmpty(queryDto.Email) || u.Email.Contains(queryDto.Email)) &&
            (!queryDto.Status.HasValue || u.Status == queryDto.Status.Value) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value),
            queryDto.PageIndex,
            queryDto.PageSize,
            total);

        var userDtos = list.Adapt<List<LeanUserDto>>();
        return new PagedResults<LeanUserDto>
        {
            Items = userDtos,
            Total = total,
            PageIndex = queryDto.PageIndex,
            PageSize = queryDto.PageSize
        };
    }

    /// <summary>
    /// 导入用户数据
    /// </summary>
    public async Task<(List<LeanUserDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes)
    {
        using var stream = new MemoryStream(fileBytes);
        var result = await LeanExcelHelper.ImportAsync<LeanUser>(stream);
        if (result.SuccessItems.Count > 0)
        {
            await _userRepository.InsertRangeAsync(result.SuccessItems);
        }
        return (result.SuccessItems.Adapt<List<LeanUserDto>>(), result.ErrorMessages);
    }

    /// <summary>
    /// 导出用户数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanUserQueryDto queryDto)
    {
        var list = await _userRepository.GetListAsync(u =>
            (string.IsNullOrEmpty(queryDto.UserName) || u.UserName.Contains(queryDto.UserName)) &&
            (string.IsNullOrEmpty(queryDto.Name) || u.RealName.Contains(queryDto.Name)) &&
            (string.IsNullOrEmpty(queryDto.Mobile) || u.Mobile.Contains(queryDto.Mobile)) &&
            (string.IsNullOrEmpty(queryDto.Email) || u.Email.Contains(queryDto.Email)) &&
            (!queryDto.Status.HasValue || u.Status == queryDto.Status.Value));
        return await LeanExcelHelper.ExportAsync(list);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    public async Task<byte[]> GetImportTemplateAsync()
    {
        return await LeanExcelHelper.GetImportTemplateAsync<LeanUser>();
    }

    /// <summary>
    /// 重置密码
    /// </summary>
    public async Task<string> ResetPasswordAsync(LeanUserResetPasswordDto input)
    {
        var user = await _userRepository.GetByIdAsync(input.Id);
        if (user == null)
            throw new LeanUserFriendlyException("用户不存在");

        // 使用密码助手类重置密码
        var (password, salt, encryptedPassword) = LeanPasswordHelper.ResetPassword();
        
        user.Password = encryptedPassword;
        user.Salt = salt;
        
        await _userRepository.UpdateAsync(user);
        
        return password; // 返回明文密码给用户
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    public async Task ChangePasswordAsync(LeanUserChangePasswordDto input)
    {
        var user = await _userRepository.GetByIdAsync(input.Id);
        if (user == null)
            throw new LeanUserFriendlyException("用户不存在");

        // 验证旧密码
        if (!LeanPasswordHelper.ValidatePassword(input.OldPassword, user.Salt, user.Password))
            throw new LeanUserFriendlyException("旧密码错误");

        // 验证新密码强度
        var (isValid, message) = LeanPasswordHelper.ValidatePasswordStrength(input.NewPassword);
        if (!isValid)
            throw new LeanUserFriendlyException(message);

        // 生成新的盐值和加密密码
        var salt = LeanPasswordHelper.GenerateSalt();
        var encryptedPassword = LeanPasswordHelper.EncryptPassword(input.NewPassword, salt);

        user.Password = encryptedPassword;
        user.Salt = salt;
        
        await _userRepository.UpdateAsync(user);
    }

    /// <summary>
    /// 验证用户凭证
    /// </summary>
    public async Task<bool> ValidateCredentialsAsync(string username, string password)
    {
        var user = await _userRepository.GetAsync(u => u.UserName == username);
        if (user == null)
            return false;

        return LeanPasswordHelper.ValidatePassword(password, user.Salt, user.Password);
    }
} 
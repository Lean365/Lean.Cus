using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Auth;
using Lean.Cus.Application.Interfaces.Auth;
using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Common.Security;
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Configs;
using Microsoft.Extensions.Options;

namespace Lean.Cus.Application.Services.Auth;

/// <summary>
/// 认证服务实现
/// </summary>
public class LeanAuthService : ILeanAuthService
{
    private readonly ILeanRepository<LeanUser> _userRepository;
    private readonly ILeanUserExtendService _userExtendService;
    private readonly ILeanDevicesExtendService _devicesExtendService;
    private readonly LeanJwtConfig _jwtConfig;

    /// <summary>
    /// 初始化认证服务
    /// </summary>
    public LeanAuthService(
        ILeanRepository<LeanUser> userRepository,
        ILeanUserExtendService userExtendService,
        ILeanDevicesExtendService devicesExtendService,
        IOptions<LeanJwtConfig> jwtConfig)
    {
        _userRepository = userRepository;
        _userExtendService = userExtendService;
        _devicesExtendService = devicesExtendService;
        _jwtConfig = jwtConfig.Value;
    }

    /// <summary>
    /// 登录
    /// </summary>
    public async Task<LeanLoginResultDto> LoginAsync(LeanLoginDto input)
    {
        // 验证用户名密码
        var user = await _userRepository.GetFirstAsync(x => x.UserName == input.UserName);
        if (user == null)
        {
            throw new Exception("用户名或密码错误");
        }

        // 验证密码
        var password = LeanPasswordHelper.EncryptPassword(input.Password, user.Salt);
        if (password != user.Password)
        {
            // 更新密码错误信息
            await _userExtendService.UpdatePasswordErrorInfoAsync(new Dtos.Admin.LeanUserExtendPasswordErrorUpdateDto
            {
                UserId = user.Id,
                LockoutEndTime = DateTime.Now.AddMinutes(30) // 锁定30分钟
            });
            throw new Exception("用户名或密码错误");
        }

        // 检查用户状态
        if (user.Status == LeanUserStatus.Disabled)
        {
            throw new Exception("账号已被禁用");
        }
        else if (user.Status == LeanUserStatus.Locked)
        {
            throw new Exception("账号已被锁定");
        }
        else if (user.Status == LeanUserStatus.Expired)
        {
            throw new Exception("账号已过期");
        }
        else if (user.Status == LeanUserStatus.PendingReview)
        {
            throw new Exception("账号待审核");
        }
        else if (user.Status == LeanUserStatus.ReviewRejected)
        {
            throw new Exception("账号审核被拒绝");
        }
        else if (user.Status == LeanUserStatus.Cancelled)
        {
            throw new Exception("账号已注销");
        }

        // TODO: 获取用户角色
        var roles = new[] { "user" }; // 这里需要实现获取用户实际角色的逻辑

        // 生成访问令牌和刷新令牌
        var accessToken = LeanJwtHelper.GenerateToken(_jwtConfig, user.Id, user.UserName, roles);
        var refreshToken = LeanJwtHelper.GenerateRefreshToken();

        // 更新登录信息
        await _userExtendService.UpdateLoginInfoAsync(new Dtos.Admin.LeanUserExtendLoginUpdateDto
        {
            UserId = user.Id,
            IpAddress = input.IpAddress,
            Device = input.DeviceName,
            Location = input.Location,
            Browser = input.Browser,
            Os = input.Os
        });

        // 更新设备信息
        if (!string.IsNullOrEmpty(input.DeviceId))
        {
            var deviceType = LeanDeviceType.Other;
            if (Enum.TryParse<LeanDeviceType>(input.DeviceType, true, out var parsedDeviceType))
            {
                deviceType = parsedDeviceType;
            }

            await _devicesExtendService.UpdateLoginInfoAsync(new Dtos.Admin.LeanDevicesExtendLoginUpdateDto
            {
                DeviceId = input.DeviceId,
                DeviceName = input.DeviceName,
                DeviceType = deviceType,
                OperatingSystem = input.Os,
                Browser = input.Browser,
                IpAddress = input.IpAddress,
                Location = input.Location,
                UserId = user.Id
            });
        }

        // 返回登录结果
        return new LeanLoginResultDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresIn = _jwtConfig.ExpireMinutes,
            UserId = user.Id,
            UserName = user.UserName,
            RealName = user.RealName,
            Avatar = string.Empty, // 头像暂时返回空字符串，后续可以从用户扩展信息中获取
            Permissions = new List<string>(), // TODO: 获取用户权限
            Roles = new List<string>(roles)
        };
    }

    /// <summary>
    /// 登出
    /// </summary>
    public async Task<bool> LogoutAsync()
    {
        // TODO: 实现登出逻辑，如清除刷新令牌等
        return await Task.FromResult(true);
    }

    /// <summary>
    /// 刷新令牌
    /// </summary>
    public async Task<LeanLoginResultDto> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            // 验证刷新令牌
            var principal = LeanJwtHelper.ValidateToken(refreshToken, _jwtConfig);
            var userId = LeanJwtHelper.GetUserId(principal);
            var userName = LeanJwtHelper.GetUserName(principal);
            var roles = LeanJwtHelper.GetRoles(principal);

            // 验证用户是否存在
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("用户不存在");
            }

            // 检查用户状态
            if (user.Status != LeanUserStatus.Enabled)
            {
                throw new Exception("用户状态异常");
            }

            // 生成新的访问令牌和刷新令牌
            var newAccessToken = LeanJwtHelper.GenerateToken(_jwtConfig, userId, userName, roles);
            var newRefreshToken = LeanJwtHelper.GenerateRefreshToken();

            return new LeanLoginResultDto
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
                ExpiresIn = _jwtConfig.ExpireMinutes,
                UserId = userId,
                UserName = userName,
                RealName = user.RealName,
                Avatar = string.Empty, // 头像暂时返回空字符串，后续可以从用户扩展信息中获取
                Permissions = new List<string>(), // TODO: 获取用户权限
                Roles = new List<string>(roles)
            };
        }
        catch (Exception ex)
        {
            throw new Exception("刷新令牌无效或已过期", ex);
        }
    }
} 
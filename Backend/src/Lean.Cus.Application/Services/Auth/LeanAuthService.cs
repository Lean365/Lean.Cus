//===================================================
// 项目名称：Lean.Cus.Application.Services.Auth
// 文件名称：LeanAuthService
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：认证服务实现
//===================================================

using Lean.Cus.Application.Dtos.Auth;
using Lean.Cus.Common.Results;
using Lean.Cus.Common.Security;
using Lean.Cus.Domain.Entities.Monitor;
using Lean.Cus.Domain.IRepositories.Monitor;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using SqlSugar;

namespace Lean.Cus.Application.Services.Auth;

/// <summary>
/// 认证服务实现
/// </summary>
public class LeanAuthService : ILeanAuthService
{
    private readonly ISqlSugarClient _db;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ILeanLoginLogRepository _loginLogRepository;
    private readonly ILogger<LeanAuthService> _logger;

    public LeanAuthService(
        ISqlSugarClient db,
        IHttpContextAccessor httpContextAccessor,
        ILeanLoginLogRepository loginLogRepository,
        ILogger<LeanAuthService> logger)
    {
        _db = db;
        _httpContextAccessor = httpContextAccessor;
        _loginLogRepository = loginLogRepository;
        _logger = logger;
    }

    /// <summary>
    /// 记录登录日志
    /// </summary>
    private async Task AddLoginLog(string userName, bool isSuccess, string msg)
    {
        try
        {
            var context = _httpContextAccessor.HttpContext;
            var userAgent = context?.Request.Headers["User-Agent"].ToString();
            var ipAddress = context?.Connection.RemoteIpAddress?.ToString();

            var log = new LeanLoginLog
            {
                UserName = userName,
                IpAddr = ipAddress,
                Browser = GetBrowser(userAgent),
                Os = GetOS(userAgent),
                Status = isSuccess ? 1 : 0,
                Msg = msg,
                LoginTime = DateTime.Now
            };

            await _loginLogRepository.AddAsync(log);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "记录登录日志失败");
        }
    }

    /// <summary>
    /// 获取浏览器类型
    /// </summary>
    private string? GetBrowser(string? userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            return null;

        if (userAgent.Contains("Chrome"))
            return "Chrome";
        if (userAgent.Contains("Firefox"))
            return "Firefox";
        if (userAgent.Contains("Safari"))
            return "Safari";
        if (userAgent.Contains("Edge"))
            return "Edge";
        if (userAgent.Contains("MSIE") || userAgent.Contains("Trident"))
            return "IE";

        return "Other";
    }

    /// <summary>
    /// 获取操作系统
    /// </summary>
    private string? GetOS(string? userAgent)
    {
        if (string.IsNullOrEmpty(userAgent))
            return null;

        if (userAgent.Contains("Windows"))
            return "Windows";
        if (userAgent.Contains("Mac"))
            return "MacOS";
        if (userAgent.Contains("Linux"))
            return "Linux";
        if (userAgent.Contains("Android"))
            return "Android";
        if (userAgent.Contains("iPhone") || userAgent.Contains("iPad"))
            return "iOS";

        return "Other";
    }

    /// <summary>
    /// 用户登录
    /// </summary>
    /// <param name="dto">登录信息</param>
    /// <returns>登录结果</returns>
    public async Task<LeanApiResult<LeanLoginResultDto>> LoginAsync(LeanLoginDto dto)
    {
        try
        {
            // 获取用户信息
            var user = await _db.Queryable<LeanSysUser>()
                .FirstAsync(u => u.UserName == dto.UserName);

            if (user == null)
            {
                await AddLoginLog(dto.UserName, false, "用户名或密码错误");
                return LeanApiResult<LeanLoginResultDto>.Fail("用户名或密码错误");
            }

            // 验证密码
            if (!LeanSecurityHelper.VerifyPassword(dto.Password, user.Salt, user.Password))
            {
                await AddLoginLog(dto.UserName, false, "用户名或密码错误");
                return LeanApiResult<LeanLoginResultDto>.Fail("用户名或密码错误");
            }

            // 检查用户状态
            if (user.Status != 1)
            {
                await AddLoginLog(dto.UserName, false, "用户已被禁用");
                return LeanApiResult<LeanLoginResultDto>.Fail("用户已被禁用");
            }

            // 更新登录信息
            user.LoginCount++;
            user.LastLoginTime = DateTime.Now;
            user.LastLoginIp = _httpContextAccessor.HttpContext?.Connection.RemoteIpAddress?.ToString();

            if (user.FirstLoginTime == null)
            {
                user.FirstLoginTime = DateTime.Now;
                user.FirstLoginIp = user.LastLoginIp;
            }

            await _db.Updateable(user)
                .UpdateColumns(u => new
                {
                    u.LoginCount,
                    u.LastLoginTime,
                    u.LastLoginIp,
                    u.FirstLoginTime,
                    u.FirstLoginIp
                })
                .ExecuteCommandAsync();

            // 记录登录日志
            await AddLoginLog(dto.UserName, true, "登录成功");

            // 返回登录结果
            var result = new LeanLoginResultDto
            {
                Id = user.Id,
                UserName = user.UserName,
                NickName = user.NickName,
                UserType = user.UserType,
                Avatar = user.Avatar
            };

            return LeanApiResult<LeanLoginResultDto>.Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "用户登录失败");
            await AddLoginLog(dto.UserName, false, "系统错误");
            return LeanApiResult<LeanLoginResultDto>.Fail("系统错误");
        }
    }

    /// <summary>
    /// 强制退出
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>退出结果</returns>
    public async Task<LeanApiResult<bool>> ForceLogoutAsync(long userId)
    {
        var user = await _db.Queryable<LeanSysUser>()
            .FirstAsync(u => u.Id == userId);

        if (user == null)
        {
            return LeanApiResult<bool>.NotFound("用户不存在");
        }

        // 更新用户最后登出时间
        user.LastLogoutTime = DateTime.Now;

        var success = await _db.Updateable(user)
            .UpdateColumns(u => new { u.LastLogoutTime })
            .ExecuteCommandAsync() > 0;

        if (success)
        {
            await AddLoginLog(user.UserName, true, "强制退出成功");
        }

        return success
            ? LeanApiResult<bool>.Ok(true)
            : LeanApiResult<bool>.Fail("强制退出失败");
    }
}
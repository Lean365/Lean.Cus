using System.ComponentModel.DataAnnotations;

namespace Lean.Cus.Application.Dtos.Auth;

/// <summary>
/// 登录请求DTO
/// </summary>
public class LeanLoginDto
{
    /// <summary>
    /// 用户名
    /// </summary>
    [Required(ErrorMessage = "用户名不能为空")]
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [Required(ErrorMessage = "密码不能为空")]
    public string Password { get; set; }

    /// <summary>
    /// 验证码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 验证码唯一标识
    /// </summary>
    public string Uuid { get; set; }

    /// <summary>
    /// 设备ID
    /// </summary>
    public string DeviceId { get; set; }

    /// <summary>
    /// 设备名称
    /// </summary>
    public string DeviceName { get; set; }

    /// <summary>
    /// 设备类型
    /// </summary>
    public string DeviceType { get; set; }

    /// <summary>
    /// 操作系统
    /// </summary>
    public string Os { get; set; }

    /// <summary>
    /// 浏览器
    /// </summary>
    public string Browser { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    public string IpAddress { get; set; }

    /// <summary>
    /// 登录位置
    /// </summary>
    public string Location { get; set; }
} 
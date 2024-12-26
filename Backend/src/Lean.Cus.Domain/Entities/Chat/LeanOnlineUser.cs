using SqlSugar;

namespace Lean.Cus.Domain.Entities.Chat;

/// <summary>
/// 在线用户实体
/// </summary>
[SugarTable("Lean_online_user", "在线用户表")]
public class LeanOnlineUser : LeanLongEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnDescription = "用户名")]
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    /// 连接ID
    /// </summary>
    [SugarColumn(ColumnDescription = "连接ID")]
    public string ConnectionId { get; set; } = string.Empty;

    /// <summary>
    /// 最后活动时间
    /// </summary>
    [SugarColumn(ColumnDescription = "最后活动时间")]
    public DateTime LastActiveTime { get; set; }

    /// <summary>
    /// 在线状态（0：离线，1：在线）
    /// </summary>
    [SugarColumn(ColumnDescription = "在线状态（0：离线，1：在线）")]
    public int Status { get; set; }

    /// <summary>
    /// IP地址
    /// </summary>
    [SugarColumn(ColumnDescription = "IP地址")]
    public string IpAddress { get; set; } = string.Empty;

    /// <summary>
    /// 客户端类型（Windows/Web/Mobile等）
    /// </summary>
    [SugarColumn(ColumnDescription = "客户端类型")]
    public string ClientType { get; set; } = string.Empty;

    /// <summary>
    /// 客户端版本
    /// </summary>
    [SugarColumn(ColumnDescription = "客户端版本")]
    public string ClientVersion { get; set; } = string.Empty;

    /// <summary>
    /// 操作系统信息
    /// </summary>
    [SugarColumn(ColumnDescription = "操作系统信息")]
    public string OsInfo { get; set; } = string.Empty;

    /// <summary>
    /// 浏览器信息（如果是Web客户端）
    /// </summary>
    [SugarColumn(ColumnDescription = "浏览器信息")]
    public string BrowserInfo { get; set; } = string.Empty;

    /// <summary>
    /// 设备标识
    /// </summary>
    [SugarColumn(ColumnDescription = "设备标识")]
    public string DeviceId { get; set; } = string.Empty;

    /// <summary>
    /// 登录时间
    /// </summary>
    [SugarColumn(ColumnDescription = "登录时间")]
    public DateTime LoginTime { get; set; }

    /// <summary>
    /// 客户端分辨率
    /// </summary>
    [SugarColumn(ColumnDescription = "客户端分辨率")]
    public string Resolution { get; set; } = string.Empty;

    /// <summary>
    /// 网络类型（WiFi/4G/5G等）
    /// </summary>
    [SugarColumn(ColumnDescription = "网络类型")]
    public string NetworkType { get; set; } = string.Empty;

    /// <summary>
    /// 地理位置
    /// </summary>
    [SugarColumn(ColumnDescription = "地理位置")]
    public string Location { get; set; } = string.Empty;

    /// <summary>
    /// 客户端语言
    /// </summary>
    [SugarColumn(ColumnDescription = "客户端语言")]
    public string Language { get; set; } = string.Empty;

    /// <summary>
    /// 客户端时区
    /// </summary>
    [SugarColumn(ColumnDescription = "客户端时区")]
    public string TimeZone { get; set; } = string.Empty;
} 
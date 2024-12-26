//===================================================
// 项目名称：Lean.Cus.Common.Constants
// 文件名称：CommonConstants
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：常量类
//===================================================

namespace Lean.Cus.Common.Constants;

/// <summary>
/// 常量类
/// </summary>
public static class CommonConstants
{
    /// <summary>
    /// 系统名称
    /// </summary>
    public const string SystemName = "Lean.Cus";

    /// <summary>
    /// 系统版本
    /// </summary>
    public const string SystemVersion = "1.0.0";

    /// <summary>
    /// 系统作者
    /// </summary>
    public const string SystemAuthor = "Lean";

    /// <summary>
    /// 系统描述
    /// </summary>
    public const string SystemDescription = "Lean.Cus 企业级快速开发框架";

    /// <summary>
    /// 系统默认密码
    /// </summary>
    public const string DefaultPassword = "123456";

    /// <summary>
    /// 超级管理员角色编码
    /// </summary>
    public const string AdminRoleCode = "admin";

    /// <summary>
    /// 超级管理员用户编码
    /// </summary>
    public const string AdminUserCode = "admin";

    /// <summary>
    /// 默认分页大小
    /// </summary>
    public const int DefaultPageSize = 10;

    /// <summary>
    /// 最大分页大小
    /// </summary>
    public const int MaxPageSize = 100;

    /// <summary>
    /// 默认缓存过期时间（分钟）
    /// </summary>
    public const int DefaultCacheExpireMinutes = 30;

    /// <summary>
    /// 默认JWT过期时间（分钟）
    /// </summary>
    public const int DefaultJwtExpireMinutes = 120;

    /// <summary>
    /// 默认刷新令牌过期时间（天）
    /// </summary>
    public const int DefaultRefreshTokenExpireDays = 7;

    /// <summary>
    /// 默认文件上传大小限制（MB）
    /// </summary>
    public const int DefaultFileUploadLimitMB = 10;

    /// <summary>
    /// 默认图片上传大小限制（MB）
    /// </summary>
    public const int DefaultImageUploadLimitMB = 5;

    /// <summary>
    /// 默认视频上传大小限制（MB）
    /// </summary>
    public const int DefaultVideoUploadLimitMB = 50;

    /// <summary>
    /// 默认音频上传大小限制（MB）
    /// </summary>
    public const int DefaultAudioUploadLimitMB = 20;

    /// <summary>
    /// 默认文档上传大小限制（MB）
    /// </summary>
    public const int DefaultDocumentUploadLimitMB = 10;

    /// <summary>
    /// 允许上传的图片类型
    /// </summary>
    public static readonly string[] AllowedImageTypes = { ".jpg", ".jpeg", ".png", ".gif", ".bmp" };

    /// <summary>
    /// 允许上传的视频类型
    /// </summary>
    public static readonly string[] AllowedVideoTypes = { ".mp4", ".avi", ".wmv", ".mov", ".flv" };

    /// <summary>
    /// 允许上传的音频类型
    /// </summary>
    public static readonly string[] AllowedAudioTypes = { ".mp3", ".wav", ".wma", ".aac", ".flac" };

    /// <summary>
    /// 允许上传的文档类型
    /// </summary>
    public static readonly string[] AllowedDocumentTypes = { ".doc", ".docx", ".xls", ".xlsx", ".ppt", ".pptx", ".pdf", ".txt" };

    /// <summary>
    /// 默认日期时间格式
    /// </summary>
    public const string DefaultDateTimeFormat = "yyyy-MM-dd HH:mm:ss";

    /// <summary>
    /// 默认日期格式
    /// </summary>
    public const string DefaultDateFormat = "yyyy-MM-dd";

    /// <summary>
    /// 默认时间格式
    /// </summary>
    public const string DefaultTimeFormat = "HH:mm:ss";

    /// <summary>
    /// 默认数字格式
    /// </summary>
    public const string DefaultNumberFormat = "#,##0.00";

    /// <summary>
    /// 默认货币格式
    /// </summary>
    public const string DefaultCurrencyFormat = "¥#,##0.00";

    /// <summary>
    /// 默认百分比格式
    /// </summary>
    public const string DefaultPercentFormat = "#,##0.00%";

    /// <summary>
    /// 默认编码格式
    /// </summary>
    public const string DefaultEncoding = "UTF-8";

    /// <summary>
    /// 默认时区
    /// </summary>
    public const string DefaultTimeZone = "Asia/Shanghai";

    /// <summary>
    /// 默认语言
    /// </summary>
    public const string DefaultLanguage = "zh-CN";

    /// <summary>
    /// 默认主题
    /// </summary>
    public const string DefaultTheme = "default";

    /// <summary>
    /// 默认布局
    /// </summary>
    public const string DefaultLayout = "default";

    /// <summary>
    /// 默认字体
    /// </summary>
    public const string DefaultFont = "Microsoft YaHei";

    /// <summary>
    /// 默认字体大小
    /// </summary>
    public const int DefaultFontSize = 14;

    /// <summary>
    /// 默认主色调
    /// </summary>
    public const string DefaultPrimaryColor = "#409EFF";

    /// <summary>
    /// 默认成功色
    /// </summary>
    public const string DefaultSuccessColor = "#67C23A";

    /// <summary>
    /// 默认警告色
    /// </summary>
    public const string DefaultWarningColor = "#E6A23C";

    /// <summary>
    /// 默认危险色
    /// </summary>
    public const string DefaultDangerColor = "#F56C6C";

    /// <summary>
    /// 默认信息色
    /// </summary>
    public const string DefaultInfoColor = "#909399";
} 
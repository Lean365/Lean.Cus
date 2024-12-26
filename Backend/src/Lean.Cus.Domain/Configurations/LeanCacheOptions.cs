//===================================================
// 项目名称：Lean.Cus.Domain.Configurations
// 文件名称：LeanCacheOptions
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：缓存选项配置
//===================================================

namespace Lean.Cus.Domain.Configurations;

/// <summary>
/// Redis缓存选项
/// </summary>
public class LeanRedisOptions
{
    /// <summary>
    /// 连接超时时间（毫秒）
    /// </summary>
    public int ConnectTimeout { get; set; } = 5000;

    /// <summary>
    /// 操作超时时间（毫秒）
    /// </summary>
    public int OperationTimeout { get; set; } = 1000;

    /// <summary>
    /// 密码
    /// </summary>
    public string? Password { get; set; }

    /// <summary>
    /// 是否启用SSL
    /// </summary>
    public bool UseSsl { get; set; } = false;

    /// <summary>
    /// 是否允许管理员命令
    /// </summary>
    public bool AllowAdmin { get; set; } = false;

    /// <summary>
    /// 是否启用压缩
    /// </summary>
    public bool EnableCompression { get; set; } = false;

    /// <summary>
    /// 重试次数
    /// </summary>
    public int RetryCount { get; set; } = 3;

    /// <summary>
    /// 重试间隔（毫秒）
    /// </summary>
    public int RetryInterval { get; set; } = 1000;

    /// <summary>
    /// 连接池大小
    /// </summary>
    public int PoolSize { get; set; } = 50;

    /// <summary>
    /// 保持连接活动的时间（秒）
    /// </summary>
    public int KeepAlive { get; set; } = -1;

    /// <summary>
    /// 连接失败时是否使用备用服务器
    /// </summary>
    public bool AbortOnConnectFail { get; set; } = false;
}

/// <summary>
/// 内存缓存选项
/// </summary>
public class LeanMemoryOptions
{
    /// <summary>
    /// 默认缓存时间（分钟）
    /// </summary>
    public int DefaultExpirationMinutes { get; set; } = 30;

    /// <summary>
    /// 是否启用滑动过期
    /// </summary>
    public bool EnableSlidingExpiration { get; set; } = true;

    /// <summary>
    /// 缓存大小限制（项）
    /// </summary>
    public int SizeLimit { get; set; } = 1000;

    /// <summary>
    /// 是否在内存压力下压缩
    /// </summary>
    public bool CompactOnMemoryPressure { get; set; } = true;
} 
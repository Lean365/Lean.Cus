//===================================================
// 项目名称：Lean.Cus.Domain.Configurations
// 文件名称：LeanCacheConfig
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：缓存配置类
//===================================================

namespace Lean.Cus.Domain.Configurations;

/// <summary>
/// 缓存配置类
/// </summary>
public class LeanCacheConfig
{
    /// <summary>
    /// 缓存提供程序类型（Memory/Redis）
    /// </summary>
    public string Provider { get; set; } = "Memory";

    /// <summary>
    /// 默认缓存时间（分钟）
    /// </summary>
    public int DefaultExpirationMinutes { get; set; } = 30;

    /// <summary>
    /// Redis配置（可选）
    /// </summary>
    public RedisConfig? Redis { get; set; }
}

/// <summary>
/// Redis配置
/// </summary>
public class RedisConfig
{
    /// <summary>
    /// Redis连接字符串
    /// </summary>
    public string ConnectionString { get; set; } = "localhost:6379";

    /// <summary>
    /// 实例名称前缀
    /// </summary>
    public string InstanceName { get; set; } = "lean_cus_";

    /// <summary>
    /// 默认数据库索引
    /// </summary>
    public int DefaultDb { get; set; } = 0;

    /// <summary>
    /// 是否启用SSL
    /// </summary>
    public bool UseSsl { get; set; } = false;

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
    /// 重试次数
    /// </summary>
    public int RetryCount { get; set; } = 3;

    /// <summary>
    /// 重试间隔（毫秒）
    /// </summary>
    public int RetryInterval { get; set; } = 1000;

    /// <summary>
    /// 是否允许管理员命令
    /// </summary>
    public bool AllowAdmin { get; set; } = false;

    /// <summary>
    /// 是否启用压缩
    /// </summary>
    public bool EnableCompression { get; set; } = false;

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
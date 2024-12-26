//===================================================
// 项目名称：Lean.Cus.Domain.Configurations
// 文件名称：LeanRateLimitConfig
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：限流配置
//===================================================

namespace Lean.Cus.Domain.Configurations;

/// <summary>
/// 限流配置
/// </summary>
public class LeanRateLimitConfig
{
    /// <summary>
    /// 是否启用端点限流
    /// </summary>
    public bool EnableEndpointRateLimiting { get; set; }

    /// <summary>
    /// 是否堆叠被阻止的请求
    /// </summary>
    public bool StackBlockedRequests { get; set; }

    /// <summary>
    /// 真实IP头
    /// </summary>
    public string RealIpHeader { get; set; } = "X-Real-IP";

    /// <summary>
    /// 客户端ID头
    /// </summary>
    public string ClientIdHeader { get; set; } = "X-ClientId";

    /// <summary>
    /// HTTP状态码
    /// </summary>
    public int HttpStatusCode { get; set; } = 429;

    /// <summary>
    /// 通用规则
    /// </summary>
    public List<LeanRateLimitRule> GeneralRules { get; set; } = new();

    /// <summary>
    /// IP规则
    /// </summary>
    public List<LeanIpRateLimitRule> IpRules { get; set; } = new();
}

/// <summary>
/// 限流规则
/// </summary>
public class LeanRateLimitRule
{
    /// <summary>
    /// 端点
    /// </summary>
    public string Endpoint { get; set; } = "*";

    /// <summary>
    /// 时间段
    /// </summary>
    public string Period { get; set; } = "1s";

    /// <summary>
    /// 限制次数
    /// </summary>
    public int Limit { get; set; } = 3;
}

/// <summary>
/// IP限流规则
/// </summary>
public class LeanIpRateLimitRule
{
    /// <summary>
    /// IP地址
    /// </summary>
    public string Ip { get; set; } = string.Empty;

    /// <summary>
    /// 规则列表
    /// </summary>
    public List<LeanRateLimitRule> Rules { get; set; } = new();
} 
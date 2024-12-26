//===================================================
// 项目名称：Lean.Cus.Domain.Configurations
// 文件名称：LeanSnowflakeConfig
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：雪花算法配置类
//===================================================

namespace Lean.Cus.Domain.Configurations;

/// <summary>
/// 雪花算法配置类
/// </summary>
public class LeanSnowflakeConfig
{
    /// <summary>
    /// 工作机器ID
    /// </summary>
    public int WorkId { get; set; }

    /// <summary>
    /// 过期年份
    /// <para>默认为2099年</para>
    /// </summary>
    public int ExpirationYear { get; set; } = 2099;

    /// <summary>
    /// 起始年份
    /// <para>默认为2024年</para>
    /// </summary>
    public int StartYear { get; set; } = 2024;

    /// <summary>
    /// 工作机器ID占用的位数
    /// <para>默认为20位，最多支持约104万个工作节点</para>
    /// </summary>
    public int WorkerIdBits { get; set; } = 20;

    /// <summary>
    /// 序列号占用的位数
    /// <para>默认为12位，每秒最多支持4096个序列号</para>
    /// </summary>
    public int SequenceBits { get; set; } = 12;
} 
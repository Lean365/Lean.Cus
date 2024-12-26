//===================================================
// 项目名称：Lean.Cus.Common.Utils
// 文件名称：LeanSnowflake
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：雪花算法工具类（基于百度UidGenerator算法实现）
//===================================================

namespace Lean.Cus.Common.Utils;

/// <summary>
/// 雪花算法工具类
/// <para>基于百度UidGenerator算法实现</para>
/// <para>整体结构：64位 = 1位符号位 + 时间戳位 + WorkerId位 + 序列号位</para>
/// </summary>
public class LeanSnowflake
{
    private static readonly object SyncRoot = new();
    private static LeanSnowflake _instance;
    private static int _workId;
    private static int _startYear = 2024;
    private static int _expirationYear = 2099;
    private static int _workerIdBits = 20;
    private static int _sequenceBits = 12;

    #region 每部分占用的位数
    private const int SignBits = 1;         // 符号位，始终为0
    private int TimestampBits;              // 时间戳占用的位数
    private int WorkerIdBits;               // 工作机器ID占用的位数
    private int SequenceBits;               // 序列号占用的位数
    #endregion

    #region 每部分的最大值
    private long MaxWorkerId;               // 工作机器ID的最大值
    private long SequenceMask;              // 序列号的最大值
    #endregion

    #region 每部分向左的位移
    private int WorkerIdShift;              // 工作机器ID向左移动的位数
    private int TimestampShift;             // 时间戳向左移动的位数
    #endregion

    private readonly DateTime _epoch;        // 起始时间戳
    private long _lastTimestamp = -1L;      // 上次生成ID的时间戳
    private long _sequence;                 // 序列号

    /// <summary>
    /// 获取单例实例
    /// </summary>
    public static LeanSnowflake Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (SyncRoot)
                {
                    _instance ??= new LeanSnowflake(_workId, _startYear, _expirationYear, _workerIdBits, _sequenceBits);
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// 设置雪花算法配置
    /// </summary>
    /// <param name="workId">工作机器ID</param>
    /// <param name="startYear">起始年份</param>
    /// <param name="expirationYear">过期年份</param>
    /// <param name="workerIdBits">工作机器ID占用的位数</param>
    /// <param name="sequenceBits">序列号占用的位数</param>
    public static void Configure(int workId, int startYear = 2024, int expirationYear = 2099, int workerIdBits = 20, int sequenceBits = 12)
    {
        _startYear = startYear;
        _expirationYear = expirationYear;
        _workerIdBits = workerIdBits;
        _sequenceBits = sequenceBits;
        _workId = workId;
        _instance = null; // 重置实例，使用新的配置
    }

    private LeanSnowflake(int workId, int startYear, int expirationYear, int workerIdBits, int sequenceBits)
    {
        // 验证位数配置
        var totalBits = workerIdBits + sequenceBits;
        if (totalBits >= 63) // 63 = 64(总位数) - 1(符号位)
        {
            throw new ArgumentException($"WorkerId位数({workerIdBits})和序列号位数({sequenceBits})之和不能大于62");
        }

        // 设置位数
        WorkerIdBits = workerIdBits;
        SequenceBits = sequenceBits;
        TimestampBits = 63 - totalBits;

        // 计算最大值
        MaxWorkerId = -1L ^ (-1L << WorkerIdBits);
        SequenceMask = -1L ^ (-1L << SequenceBits);

        // 计算位移
        WorkerIdShift = SequenceBits;
        TimestampShift = SequenceBits + WorkerIdBits;

        // 验证工作机器ID
        if (workId > MaxWorkerId || workId < 0)
        {
            throw new ArgumentException($"工作机器ID不能大于{MaxWorkerId}或小于0");
        }

        _workId = workId;
        _sequence = 0L;
        _epoch = new DateTime(startYear, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        // 验证时间戳支持范围
        var maxTimestamp = (1L << TimestampBits) - 1;
        var maxDate = _epoch.AddSeconds(maxTimestamp);
        if (maxDate.Year < expirationYear)
        {
            throw new ArgumentException(
                $"当前位数配置无法支持到{expirationYear}年，最大支持到{maxDate.Year}年。\n" +
                $"时间戳位数：{TimestampBits}位\n" +
                $"工作机器位数：{WorkerIdBits}位（最大支持{MaxWorkerId}个节点）\n" +
                $"序列号位数：{SequenceBits}位（每秒最大支持{SequenceMask}个序列号）");
        }
    }

    /// <summary>
    /// 生成下一个ID
    /// </summary>
    /// <returns>ID</returns>
    public long NextId()
    {
        lock (SyncRoot)
        {
            var timestamp = TimeGen();

            // 如果当前时间小于上一次ID生成的时间戳，说明系统时钟回���过，抛出异常
            if (timestamp < _lastTimestamp)
            {
                throw new Exception($"时钟回拨，拒绝生成ID，回拨时间为{_lastTimestamp - timestamp}秒");
            }

            // 如果是同一时间生成的，则进行序列号递增
            if (_lastTimestamp == timestamp)
            {
                _sequence = (_sequence + 1) & SequenceMask;
                // 序列号已经达到最大值，等待下一秒
                if (_sequence == 0)
                {
                    timestamp = TilNextSecond(_lastTimestamp);
                }
            }
            // 不是同一时间生成的，序列号重置为0
            else
            {
                _sequence = 0L;
            }

            _lastTimestamp = timestamp;

            // 生成ID
            // 格式：符号位(0) + 时间戳 + 工作机器ID + 序列号
            return ((timestamp) << TimestampShift) |
                   (_workId << WorkerIdShift) |
                   _sequence;
        }
    }

    /// <summary>
    /// 解析ID
    /// </summary>
    /// <param name="id">ID</param>
    /// <returns>ID的组成部分</returns>
    public (DateTime Timestamp, int WorkerId, int Sequence) ParseId(long id)
    {
        var timestamp = (id >> TimestampShift) & (-1L ^ (-1L << TimestampBits));
        var workerId = (id >> WorkerIdShift) & (-1L ^ (-1L << WorkerIdBits));
        var sequence = id & SequenceMask;

        var datetime = _epoch.AddSeconds(timestamp);

        return (datetime, (int)workerId, (int)sequence);
    }

    /// <summary>
    /// 阻塞到下一秒，直到获得新的时间戳
    /// </summary>
    /// <param name="lastTimestamp">上次生成ID的时间戳</param>
    /// <returns>新的时间戳</returns>
    private long TilNextSecond(long lastTimestamp)
    {
        var timestamp = TimeGen();
        while (timestamp <= lastTimestamp)
        {
            timestamp = TimeGen();
        }
        return timestamp;
    }

    /// <summary>
    /// 获取以秒为单位的当前时间戳
    /// </summary>
    /// <returns>时间戳（秒）</returns>
    private long TimeGen()
    {
        return (long)(DateTime.UtcNow - _epoch).TotalSeconds;
    }
} 
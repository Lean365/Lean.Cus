//===================================================
// 项目名称：Lean.Cus.Common.Extensions
// 文件名称：DateTimeExtensions
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：日期时间扩展方法
//===================================================

namespace Lean.Cus.Common.Extensions;

/// <summary>
/// 日期时间扩展方法
/// </summary>
public static class DateTimeExtensions
{
    /// <summary>
    /// 获取时间戳（秒）
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>时间戳（秒）</returns>
    public static long ToTimestamp(this DateTime dateTime)
    {
        var timeSpan = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (long)timeSpan.TotalSeconds;
    }

    /// <summary>
    /// 获取时间戳（毫秒）
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>时间戳（毫秒）</returns>
    public static long ToMilliseconds(this DateTime dateTime)
    {
        var timeSpan = dateTime.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return (long)timeSpan.TotalMilliseconds;
    }

    /// <summary>
    /// 从时间戳转换（秒）
    /// </summary>
    /// <param name="timestamp">时间戳（秒）</param>
    /// <returns>日期时间</returns>
    public static DateTime FromTimestamp(this long timestamp)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return dateTime.AddSeconds(timestamp).ToLocalTime();
    }

    /// <summary>
    /// 从时间戳转换（毫秒）
    /// </summary>
    /// <param name="milliseconds">时间戳（毫秒）</param>
    /// <returns>日期时间</returns>
    public static DateTime FromMilliseconds(this long milliseconds)
    {
        var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        return dateTime.AddMilliseconds(milliseconds).ToLocalTime();
    }

    /// <summary>
    /// 获取当天开始时间
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>当天开始时间</returns>
    public static DateTime StartOfDay(this DateTime dateTime)
    {
        return dateTime.Date;
    }

    /// <summary>
    /// 获取当天结束时间
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>当天结束时间</returns>
    public static DateTime EndOfDay(this DateTime dateTime)
    {
        return dateTime.Date.AddDays(1).AddTicks(-1);
    }

    /// <summary>
    /// 获取当月开始时间
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>当月开始时间</returns>
    public static DateTime StartOfMonth(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, dateTime.Month, 1);
    }

    /// <summary>
    /// 获取当月结束时间
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>当月结束时间</returns>
    public static DateTime EndOfMonth(this DateTime dateTime)
    {
        return dateTime.StartOfMonth().AddMonths(1).AddTicks(-1);
    }

    /// <summary>
    /// 获取当年开始时间
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>当年开始时间</returns>
    public static DateTime StartOfYear(this DateTime dateTime)
    {
        return new DateTime(dateTime.Year, 1, 1);
    }

    /// <summary>
    /// 获取当年结束时间
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>当年结束时间</returns>
    public static DateTime EndOfYear(this DateTime dateTime)
    {
        return dateTime.StartOfYear().AddYears(1).AddTicks(-1);
    }

    /// <summary>
    /// 获取下一天
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>下一天</returns>
    public static DateTime NextDay(this DateTime dateTime)
    {
        return dateTime.AddDays(1);
    }

    /// <summary>
    /// 获取上一天
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>上一天</returns>
    public static DateTime PreviousDay(this DateTime dateTime)
    {
        return dateTime.AddDays(-1);
    }

    /// <summary>
    /// 获取下一月
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>下一月</returns>
    public static DateTime NextMonth(this DateTime dateTime)
    {
        return dateTime.AddMonths(1);
    }

    /// <summary>
    /// 获取上一月
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>上一月</returns>
    public static DateTime PreviousMonth(this DateTime dateTime)
    {
        return dateTime.AddMonths(-1);
    }

    /// <summary>
    /// 获取下一年
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>下一年</returns>
    public static DateTime NextYear(this DateTime dateTime)
    {
        return dateTime.AddYears(1);
    }

    /// <summary>
    /// 获取上一年
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>上一年</returns>
    public static DateTime PreviousYear(this DateTime dateTime)
    {
        return dateTime.AddYears(-1);
    }

    /// <summary>
    /// 是否为工作日
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>是否为工作日</returns>
    public static bool IsWeekday(this DateTime dateTime)
    {
        return dateTime.DayOfWeek != DayOfWeek.Saturday && dateTime.DayOfWeek != DayOfWeek.Sunday;
    }

    /// <summary>
    /// 是否为周末
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>是否为周末</returns>
    public static bool IsWeekend(this DateTime dateTime)
    {
        return !dateTime.IsWeekday();
    }

    /// <summary>
    /// 获取年龄
    /// </summary>
    /// <param name="dateTime">日期时间</param>
    /// <returns>年龄</returns>
    public static int GetAge(this DateTime dateTime)
    {
        var today = DateTime.Today;
        var age = today.Year - dateTime.Year;
        if (dateTime.Date > today.AddYears(-age))
            age--;
        return age;
    }
} 
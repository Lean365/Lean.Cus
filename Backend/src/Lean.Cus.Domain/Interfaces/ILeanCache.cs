//===================================================
// 项目名称：Lean.Cus.Domain.Interfaces
// 文件名称：ILeanCache
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：缓存接口
//===================================================

namespace Lean.Cus.Domain.Interfaces;

/// <summary>
/// 缓存接口
/// </summary>
public interface ILeanCache
{
    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns>缓存值</returns>
    T? Get<T>(string key);

    /// <summary>
    /// 获取缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <returns>缓存值</returns>
    Task<T?> GetAsync<T>(string key);

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存值</param>
    /// <param name="expireMinutes">过期时间（分钟）</param>
    void Set<T>(string key, T value, int expireMinutes = 30);

    /// <summary>
    /// 设置缓存
    /// </summary>
    /// <typeparam name="T">缓存类型</typeparam>
    /// <param name="key">缓存键</param>
    /// <param name="value">缓存值</param>
    /// <param name="expireMinutes">过期时间（分钟）</param>
    /// <returns>任务</returns>
    Task SetAsync<T>(string key, T value, int expireMinutes = 30);

    /// <summary>
    /// 移除缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    void Remove(string key);

    /// <summary>
    /// 移除缓存
    /// </summary>
    /// <param name="key">缓存键</param>
    /// <returns>任务</returns>
    Task RemoveAsync(string key);

    /// <summary>
    /// 清空缓存
    /// </summary>
    void Clear();

    /// <summary>
    /// 清空缓存
    /// </summary>
    /// <returns>任务</returns>
    Task ClearAsync();
} 
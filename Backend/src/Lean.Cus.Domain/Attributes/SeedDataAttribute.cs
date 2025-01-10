using System;

namespace Lean.Cus.Domain.Attributes;

/// <summary>
/// 种子数据特性
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class SeedDataAttribute : Attribute
{
    private readonly object[] _seedData;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="seedData">种子数据</param>
    public SeedDataAttribute(params object[] seedData)
    {
        _seedData = seedData;
    }

    /// <summary>
    /// 获取种子数据
    /// </summary>
    public object[] GetSeedData() => _seedData;
} 
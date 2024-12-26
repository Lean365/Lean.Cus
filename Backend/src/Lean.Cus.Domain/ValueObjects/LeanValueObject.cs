//===================================================
// 项目名称：Lean.Cus.Domain.ValueObjects
// 文件名称：LeanValueObject
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：值对象基类
//===================================================

namespace Lean.Cus.Domain.ValueObjects;

/// <summary>
/// 值对象基类
/// <para>作为系统中所有值对象的基类</para>
/// <para>提供值对象的基本行为和比较方法</para>
/// </summary>
/// <typeparam name="T">值对象类型</typeparam>
public abstract class LeanValueObject<T> where T : LeanValueObject<T>
{
    /// <summary>
    /// 判断两个值对象是否相等
    /// </summary>
    /// <param name="obj">要比较的对象</param>
    /// <returns>如果相等返回true，否则返回false</returns>
    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (T)obj;
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    /// <summary>
    /// 获取对象的哈希码
    /// </summary>
    /// <returns>哈希码</returns>
    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }

    /// <summary>
    /// 获取用于相等性比较的组件
    /// </summary>
    /// <returns>用于比较的组件集合</returns>
    protected abstract IEnumerable<object> GetEqualityComponents();

    /// <summary>
    /// 判断两个值对象是否相等
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    /// <returns>如果相等返回true，否则返回false</returns>
    public static bool operator ==(LeanValueObject<T> left, LeanValueObject<T> right)
    {
        if (ReferenceEquals(left, null) && ReferenceEquals(right, null))
            return true;

        if (ReferenceEquals(left, null) || ReferenceEquals(right, null))
            return false;

        return left.Equals(right);
    }

    /// <summary>
    /// 判断两个值对象是否不相等
    /// </summary>
    /// <param name="left">左操作数</param>
    /// <param name="right">右操作数</param>
    /// <returns>如果不相等返回true，否则返回false</returns>
    public static bool operator !=(LeanValueObject<T> left, LeanValueObject<T> right)
    {
        return !(left == right);
    }
} 
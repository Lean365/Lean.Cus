//===================================================
// 项目名称：Lean.Cus.Domain.Exceptions
// 文件名称：LeanValidationException
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：验证异常类
//===================================================

using Lean.Cus.Common.Constants;

namespace Lean.Cus.Domain.Exceptions;

/// <summary>
/// 验证异常类
/// <para>用于处理数据验证相关的异常情况</para>
/// <para>继承自LeanException基类</para>
/// </summary>
public class LeanValidationException : LeanException
{
    /// <summary>
    /// 验证错误信息集合
    /// <para>包含所有验证失败的字段及其错误信息</para>
    /// </summary>
    public IDictionary<string, string[]> Errors { get; }

    /// <summary>
    /// 默认构造函数
    /// </summary>
    public LeanValidationException() : base("数据验证失败")
    {
        Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// 使用指定错误消息初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    public LeanValidationException(string message) : base(message)
    {
        Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// 使用指定错误消息和验证错误信息集合初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="errors">验证错误信息集合</param>
    public LeanValidationException(string message, IDictionary<string, string[]> errors) : base(message)
    {
        Errors = errors;
    }

    /// <summary>
    /// 使用指定错误消��和内部异常初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="innerException">内部异常</param>
    public LeanValidationException(string message, Exception innerException) : base(message, innerException)
    {
        Errors = new Dictionary<string, string[]>();
    }

    /// <summary>
    /// 使用指定错误消息、验证错误信息集合和内部异常初始化异常
    /// </summary>
    /// <param name="message">错误消息</param>
    /// <param name="errors">验证错误信息集合</param>
    /// <param name="innerException">内部异常</param>
    public LeanValidationException(string message, IDictionary<string, string[]> errors, Exception innerException) : base(message, innerException)
    {
        Errors = errors;
    }

    /// <summary>
    /// 添加验证错误信息
    /// </summary>
    /// <param name="propertyName">属性名称</param>
    /// <param name="errorMessage">错误消息</param>
    public void AddError(string propertyName, string errorMessage)
    {
        if (Errors.ContainsKey(propertyName))
        {
            var errors = Errors[propertyName].ToList();
            errors.Add(errorMessage);
            Errors[propertyName] = errors.ToArray();
        }
        else
        {
            Errors.Add(propertyName, new[] { errorMessage });
        }
    }

    /// <summary>
    /// 添加验证错误信息
    /// </summary>
    /// <param name="propertyName">属性名称</param>
    /// <param name="errorMessages">错误消息数组</param>
    public void AddError(string propertyName, string[] errorMessages)
    {
        if (Errors.ContainsKey(propertyName))
        {
            var errors = Errors[propertyName].ToList();
            errors.AddRange(errorMessages);
            Errors[propertyName] = errors.ToArray();
        }
        else
        {
            Errors.Add(propertyName, errorMessages);
        }
    }

    /// <summary>
    /// 添加验证错误信息
    /// </summary>
    /// <param name="errors">验证错误信息典</param>
    public void AddErrors(IDictionary<string, string[]> errors)
    {
        foreach (var error in errors)
        {
            AddError(error.Key, error.Value);
        }
    }
} 
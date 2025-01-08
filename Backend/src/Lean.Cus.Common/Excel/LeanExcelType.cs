namespace Lean.Cus.Common.Excel;

/// <summary>
/// Excel数据类型枚举
/// </summary>
public enum LeanExcelType
{
    /// <summary>
    /// 字符串
    /// </summary>
    String = 0,

    /// <summary>
    /// 整数
    /// </summary>
    Int = 1,

    /// <summary>
    /// 长整数
    /// </summary>
    Long = 2,

    /// <summary>
    /// 小数
    /// </summary>
    Decimal = 3,

    /// <summary>
    /// 日期时间
    /// </summary>
    DateTime = 4,

    /// <summary>
    /// 布尔值
    /// </summary>
    Bool = 5,

    /// <summary>
    /// 图片
    /// </summary>
    Image = 6,

    /// <summary>
    /// 超链接
    /// </summary>
    Hyperlink = 7,

    /// <summary>
    /// 下拉框
    /// </summary>
    ComboBox = 8
} 
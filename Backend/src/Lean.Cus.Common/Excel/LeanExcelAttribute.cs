namespace Lean.Cus.Common.Excel;

/// <summary>
/// Excel导出特性
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class LeanExcelAttribute : Attribute
{
    /// <summary>
    /// 列名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 列宽
    /// </summary>
    public int Width { get; set; } = 20;

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 格式
    /// </summary>
    public string? Format { get; set; }

    /// <summary>
    /// 是否必填
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    /// 是否导出
    /// </summary>
    public bool IsExport { get; set; } = true;

    /// <summary>
    /// 是否导入
    /// </summary>
    public bool IsImport { get; set; } = true;

    /// <summary>
    /// 数据字典类型
    /// </summary>
    public string? DictType { get; set; }

    /// <summary>
    /// 正则表达式
    /// </summary>
    public string? Regex { get; set; }

    /// <summary>
    /// 正则错误消息
    /// </summary>
    public string? RegexMessage { get; set; }

    /// <summary>
    /// 最大长度
    /// </summary>
    public int MaxLength { get; set; }

    /// <summary>
    /// 最小长度
    /// </summary>
    public int MinLength { get; set; }

    /// <summary>
    /// 最大值
    /// </summary>
    public decimal? MaxValue { get; set; }

    /// <summary>
    /// 最小值
    /// </summary>
    public decimal? MinValue { get; set; }

    /// <summary>
    /// 提示信息
    /// </summary>
    public string? Prompt { get; set; }

    /// <summary>
    /// 对齐方式
    /// </summary>
    public LeanExcelAlign Align { get; set; } = LeanExcelAlign.Left;

    /// <summary>
    /// 数据类型
    /// </summary>
    public LeanExcelType Type { get; set; } = LeanExcelType.String;

    /// <summary>
    /// 下拉列表数据源
    /// </summary>
    public string[]? ComboBoxItems { get; set; }

    /// <summary>
    /// 图片宽度
    /// </summary>
    public int ImageWidth { get; set; } = 100;

    /// <summary>
    /// 图片高度
    /// </summary>
    public int ImageHeight { get; set; } = 100;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="name">列名</param>
    /// <param name="width">列宽</param>
    /// <param name="sort">排序</param>
    /// <param name="format">格式</param>
    /// <param name="required">是否必填</param>
    /// <param name="isExport">是否导出</param>
    /// <param name="isImport">是否导入</param>
    /// <param name="dictType">数据字典类型</param>
    /// <param name="align">对齐方式</param>
    /// <param name="type">数据类型</param>
    public LeanExcelAttribute(
        string name = "", 
        int width = 20, 
        int sort = 0, 
        string? format = null, 
        bool required = false,
        bool isExport = true,
        bool isImport = true,
        string? dictType = null,
        LeanExcelAlign align = LeanExcelAlign.Left,
        LeanExcelType type = LeanExcelType.String)
    {
        Name = name;
        Width = width;
        Sort = sort;
        Format = format;
        Required = required;
        IsExport = isExport;
        IsImport = isImport;
        DictType = dictType;
        Align = align;
        Type = type;
    }
} 
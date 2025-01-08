using OfficeOpenXml;
using OfficeOpenXml.Drawing;
using OfficeOpenXml.Style;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Reflection;
using System.IO;

namespace Lean.Cus.Common.Excel;

/// <summary>
/// Excel帮助类
/// </summary>
public static class LeanExcelHelper
{
    static LeanExcelHelper()
    {
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
    }

    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="data">数据</param>
    /// <param name="sheetName">工作表名称</param>
    /// <param name="excelProperties">Excel属性</param>
    /// <returns>Excel字节数组</returns>
    public static byte[] ExportExcel<T>(IEnumerable<T> data, string sheetName = "Sheet1", LeanExcelProperties excelProperties = null)
    {
        using var package = new ExcelPackage();

        // 设置Excel属性
        if (excelProperties != null)
        {
            package.Workbook.Properties.Title = excelProperties.Title;
            package.Workbook.Properties.Subject = excelProperties.Subject;
            package.Workbook.Properties.Author = excelProperties.Author;
            package.Workbook.Properties.Company = excelProperties.Company;
            package.Workbook.Properties.Category = excelProperties.Category;
            package.Workbook.Properties.Keywords = excelProperties.Keywords;
            package.Workbook.Properties.Comments = excelProperties.Comments;
            package.Workbook.Properties.Manager = excelProperties.Manager;
            package.Workbook.Properties.LastModifiedBy = excelProperties.LastModifiedBy;
            package.Workbook.Properties.Created = excelProperties.Created ?? DateTime.Now;
            package.Workbook.Properties.Modified = excelProperties.Modified ?? DateTime.Now;
        }

        var worksheet = package.Workbook.Worksheets.Add(sheetName);

        // 获取属性信息
        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<LeanExcelAttribute>() != null)
            .OrderBy(p => p.GetCustomAttribute<LeanExcelAttribute>()?.Sort ?? 0)
            .ToList();

        // 创建表头
        for (int i = 0; i < properties.Count; i++)
        {
            var attr = properties[i].GetCustomAttribute<LeanExcelAttribute>();
            worksheet.Cells[1, i + 1].Value = attr?.Name ?? properties[i].Name;
            worksheet.Column(i + 1).Width = attr?.Width ?? 20;

            // 设置对齐方式
            switch (attr?.Align)
            {
                case LeanExcelAlign.Center:
                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    break;
                case LeanExcelAlign.Right:
                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                    break;
                default:
                    worksheet.Cells[1, i + 1].Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                    break;
            }

            // 设置表头样式
            worksheet.Cells[1, i + 1].Style.Font.Bold = true;
            if (attr?.Required == true)
            {
                worksheet.Cells[1, i + 1].Style.Font.Color.SetColor(Color.Red);
                worksheet.Cells[1, i + 1].Value = $"{worksheet.Cells[1, i + 1].Value}*";
            }
        }

        // 填充数据
        int row = 2;
        foreach (var item in data)
        {
            for (int i = 0; i < properties.Count; i++)
            {
                var property = properties[i];
                var attr = property.GetCustomAttribute<LeanExcelAttribute>();
                var value = property.GetValue(item);
                
                if (value != null)
                {
                    var cell = worksheet.Cells[row, i + 1];

                    // 根据数据类型设置值和格式
                    switch (attr?.Type)
                    {
                        case LeanExcelType.DateTime:
                            if (value is DateTime dateTime)
                            {
                                cell.Value = dateTime;
                                cell.Style.Numberformat.Format = attr.Format ?? "yyyy-MM-dd HH:mm:ss";
                            }
                            break;
                        case LeanExcelType.Decimal:
                            if (value is decimal decimalValue)
                            {
                                cell.Value = decimalValue;
                                cell.Style.Numberformat.Format = attr.Format ?? "#,##0.00";
                            }
                            break;
                        case LeanExcelType.Bool:
                            cell.Value = value;
                            cell.Style.Numberformat.Format = "是;否;";
                            break;
                        case LeanExcelType.Image:
                            if (value is byte[] imageData)
                            {
                                using var ms = new MemoryStream(imageData);
                                var tempPath = Path.GetTempFileName();
                                File.WriteAllBytes(tempPath, imageData);
                                var excelImage = worksheet.Drawings.AddPicture($"Image_{row}_{i}", new FileInfo(tempPath));
                                excelImage.SetPosition(row - 1, 0, i, 0);
                                excelImage.SetSize(attr.ImageWidth, attr.ImageHeight);
                                try { File.Delete(tempPath); } catch { }
                            }
                            break;
                        case LeanExcelType.Hyperlink:
                            if (value is string link)
                            {
                                cell.Hyperlink = new Uri(link);
                                cell.Value = link;
                                cell.Style.Font.UnderLine = true;
                                cell.Style.Font.Color.SetColor(Color.Blue);
                            }
                            break;
                        default:
                            cell.Value = value;
                            if (!string.IsNullOrEmpty(attr?.Format))
                            {
                                cell.Style.Numberformat.Format = attr.Format;
                            }
                            break;
                    }

                    // 设置对齐方式
                    switch (attr?.Align)
                    {
                        case LeanExcelAlign.Center:
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                            break;
                        case LeanExcelAlign.Right:
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Right;
                            break;
                        default:
                            cell.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;
                            break;
                    }
                }
            }
            row++;
        }

        // 设置数据验证
        for (int i = 0; i < properties.Count; i++)
        {
            var attr = properties[i].GetCustomAttribute<LeanExcelAttribute>();
            if (attr?.Type == LeanExcelType.ComboBox && attr.ComboBoxItems?.Length > 0)
            {
                var validation = worksheet.DataValidations.AddListValidation($"{(char)('A' + i)}2:{(char)('A' + i)}{row}");
                foreach (var item in attr.ComboBoxItems)
                {
                    validation.Formula.Values.Add(item);
                }
            }
        }

        return package.GetAsByteArray();
    }

    /// <summary>
    /// 导入Excel
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>数据列表</returns>
    public static List<T> ImportExcel<T>(Stream fileStream, string sheetName = "Sheet1") where T : new()
    {
        using var package = new ExcelPackage(fileStream);
        var worksheet = package.Workbook.Worksheets[sheetName];

        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<LeanExcelAttribute>() != null)
            .ToList();

        var results = new List<T>();
        int rows = worksheet.Dimension.Rows;
        
        for (int row = 2; row <= rows; row++)
        {
            var item = new T();
            bool hasValue = false;

            for (int i = 0; i < properties.Count; i++)
            {
                var property = properties[i];
                var attr = property.GetCustomAttribute<LeanExcelAttribute>();
                var cell = worksheet.Cells[row, i + 1];
                
                if (cell.Value != null)
                {
                    hasValue = true;
                    var value = cell.Value.ToString();
                    
                    if (attr.Required && string.IsNullOrEmpty(value))
                        throw new Exception($"第{row}行{attr.Name}不能为空");

                    try
                    {
                        object convertedValue = null;
                        switch (attr.Type)
                        {
                            case LeanExcelType.DateTime:
                                convertedValue = cell.GetValue<DateTime>();
                                break;
                            case LeanExcelType.Decimal:
                                convertedValue = cell.GetValue<decimal>();
                                break;
                            case LeanExcelType.Int:
                                convertedValue = cell.GetValue<int>();
                                break;
                            case LeanExcelType.Long:
                                convertedValue = cell.GetValue<long>();
                                break;
                            case LeanExcelType.Bool:
                                convertedValue = cell.GetValue<bool>();
                                break;
                            default:
                                convertedValue = value;
                                break;
                        }

                        // 验证数据
                        if (convertedValue != null)
                        {
                            // 长度验证
                            if (convertedValue is string str)
                            {
                                if (attr.MaxLength > 0 && str.Length > attr.MaxLength)
                                    throw new Exception($"第{row}行{attr.Name}超过最大长度{attr.MaxLength}");
                                if (attr.MinLength > 0 && str.Length < attr.MinLength)
                                    throw new Exception($"第{row}行{attr.Name}小于最小长度{attr.MinLength}");
                            }

                            // 数值范围验证
                            if (convertedValue is decimal decimalValue)
                            {
                                if (attr.MaxValue.HasValue && decimalValue > attr.MaxValue.Value)
                                    throw new Exception($"第{row}行{attr.Name}超过最大值{attr.MaxValue}");
                                if (attr.MinValue.HasValue && decimalValue < attr.MinValue.Value)
                                    throw new Exception($"第{row}行{attr.Name}小于最小值{attr.MinValue}");
                            }

                            // 正则验证
                            if (!string.IsNullOrEmpty(attr.Regex) && !System.Text.RegularExpressions.Regex.IsMatch(value, attr.Regex))
                                throw new Exception($"第{row}行{attr.Name}{attr.RegexMessage ?? "格式不正确"}");

                            property.SetValue(item, convertedValue);
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception($"第{row}行{attr.Name}格式不正确: {ex.Message}");
                    }
                }
                else if (attr.Required)
                {
                    throw new Exception($"第{row}行{attr.Name}不能为空");
                }
            }

            if (hasValue)
                results.Add(item);
        }

        return results;
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="excelProperties">Excel属性</param>
    /// <returns>模板字节数组</returns>
    public static byte[] GetImportTemplate<T>(LeanExcelProperties excelProperties = null) where T : new()
    {
        var data = new List<T>();
        return ExportExcel(data, "Sheet1", excelProperties);
    }
} 
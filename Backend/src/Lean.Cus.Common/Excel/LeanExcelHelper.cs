using System.ComponentModel;
using System.Data;
using System.Reflection;
using OfficeOpenXml;
using OfficeOpenXml.Style;

namespace Lean.Cus.Common.Excel;

/// <summary>
/// Excel导入导出帮助类
/// </summary>
public static class LeanExcelHelper
{
    /// <summary>
    /// 导出Excel
    /// </summary>
    /// <typeparam name="T">导出对象类型</typeparam>
    /// <param name="list">导出数据集合</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel文件字节数组</returns>
    public static byte[] ExportExcel<T>(List<T> list, string sheetName = "Sheet1")
    {
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        
        using var package = new ExcelPackage();
        var worksheet = package.Workbook.Worksheets.Add(sheetName);

        // 获取属性信息
        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<DescriptionAttribute>() != null)
            .ToList();

        // 写入表头
        for (int i = 0; i < properties.Count; i++)
        {
            var description = properties[i].GetCustomAttribute<DescriptionAttribute>()?.Description;
            worksheet.Cells[1, i + 1].Value = description ?? properties[i].Name;
            
            // 设置表头样式
            var headerCell = worksheet.Cells[1, i + 1];
            headerCell.Style.Font.Bold = true;
            headerCell.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerCell.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            headerCell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
        }

        // 写入数据
        for (int row = 0; row < list.Count; row++)
        {
            for (int col = 0; col < properties.Count; col++)
            {
                var value = properties[col].GetValue(list[row]);
                worksheet.Cells[row + 2, col + 1].Value = value;
                
                // 设置数据单元格样式
                var cell = worksheet.Cells[row + 2, col + 1];
                cell.Style.Border.BorderAround(ExcelBorderStyle.Thin);
            }
        }

        // 自动调整列宽
        worksheet.Cells.AutoFitColumns();

        return package.GetAsByteArray();
    }

    /// <summary>
    /// 导入Excel
    /// </summary>
    /// <typeparam name="T">导入对象类型</typeparam>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>导入的数据集合</returns>
    public static List<T> ImportExcel<T>(Stream fileStream, string sheetName = "Sheet1") where T : new()
    {
        ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
        
        using var package = new ExcelPackage(fileStream);
        var worksheet = package.Workbook.Worksheets[sheetName];
        var result = new List<T>();

        // 获取属性信息
        var properties = typeof(T).GetProperties()
            .Where(p => p.GetCustomAttribute<DescriptionAttribute>() != null)
            .ToList();

        // 获取表头
        var headers = new Dictionary<string, int>();
        for (int col = 1; col <= worksheet.Dimension.End.Column; col++)
        {
            var headerValue = worksheet.Cells[1, col].Value?.ToString();
            if (!string.IsNullOrEmpty(headerValue))
            {
                headers[headerValue] = col;
            }
        }

        // 读取数据
        for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
        {
            var item = new T();
            foreach (var property in properties)
            {
                var description = property.GetCustomAttribute<DescriptionAttribute>()?.Description;
                if (description != null && headers.ContainsKey(description))
                {
                    var col = headers[description];
                    var cellValue = worksheet.Cells[row, col].Value;
                    if (cellValue != null)
                    {
                        var convertedValue = Convert.ChangeType(cellValue, property.PropertyType);
                        property.SetValue(item, convertedValue);
                    }
                }
            }
            result.Add(item);
        }

        return result;
    }
} 
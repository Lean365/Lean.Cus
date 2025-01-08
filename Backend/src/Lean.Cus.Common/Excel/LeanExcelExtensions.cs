using OfficeOpenXml;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Lean.Cus.Common.Excel;

/// <summary>
/// Excel扩展方法
/// </summary>
public static class LeanExcelExtensions
{
    /// <summary>
    /// 导出到Excel
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="data">数据</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>Excel字节数组</returns>
    public static byte[] ToExcel<T>(this IEnumerable<T> data, string sheetName = "Sheet1")
    {
        return LeanExcelHelper.ExportExcel(data, sheetName);
    }

    /// <summary>
    /// 从Excel导入
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <param name="fileStream">Excel文件流</param>
    /// <param name="sheetName">工作表名称</param>
    /// <returns>数据列表</returns>
    public static List<T> FromExcel<T>(this Stream fileStream, string sheetName = "Sheet1") where T : new()
    {
        return LeanExcelHelper.ImportExcel<T>(fileStream, sheetName);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <typeparam name="T">数据类型</typeparam>
    /// <returns>模板字节数组</returns>
    public static byte[] GetExcelTemplate<T>() where T : new()
    {
        return LeanExcelHelper.GetImportTemplate<T>();
    }
} 
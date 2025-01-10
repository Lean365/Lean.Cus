using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.DataValidation;

namespace Lean.Cus.Common.Excel
{
    /// <summary>
    /// Excel处理帮助类
    /// </summary>
    public static class LeanExcelHelper
    {
        static LeanExcelHelper()
        {
            // 设置EPPlus商业许可
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
        }

        #region 导出

        /// <summary>
        /// 导出单个Sheet的Excel
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据列表</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>Excel文件字节数组</returns>
        public static async Task<byte[]> ExportAsync<T>(IEnumerable<T> data, string sheetName = "Sheet1") where T : class
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null)
                .ToDictionary(p => p.Name, p => GetDisplayName(p));
            return await ExportAsync(data, properties, sheetName);
        }

        /// <summary>
        /// 导出多个Sheet的Excel
        /// </summary>
        /// <param name="sheets">Sheet数据（Key: Sheet名称, Value: 数据和列定义）</param>
        /// <returns>Excel文件字节数组</returns>
        public static async Task<byte[]> ExportMultiSheetAsync(Dictionary<string, (IEnumerable<object> Data, Dictionary<string, string> Columns)> sheets)
        {
            using var package = new ExcelPackage();
            foreach (var sheet in sheets)
            {
                var worksheet = package.Workbook.Worksheets.Add(sheet.Key);
                await WriteSheetDataAsync(worksheet, sheet.Value.Data, sheet.Value.Columns);
                FormatWorksheet(worksheet);
            }
            return await package.GetAsByteArrayAsync();
        }

        /// <summary>
        /// 导出数据到Excel（指定列）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="data">数据列表</param>
        /// <param name="columns">要导出的列（Key: 属性名, Value: 显示名）</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>Excel文件字节数组</returns>
        public static async Task<byte[]> ExportAsync<T>(IEnumerable<T> data, Dictionary<string, string> columns, string sheetName = "Sheet1") where T : class
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(sheetName);
            await WriteSheetDataAsync(worksheet, data, columns);
            FormatWorksheet(worksheet);
            return await package.GetAsByteArrayAsync();
        }

        #endregion

        #region 导入

        /// <summary>
        /// 获取导入模板
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>Excel模板文件字节数组</returns>
        public static async Task<byte[]> GetImportTemplateAsync<T>(string sheetName = "Sheet1") where T : class
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null
                    && p.GetCustomAttribute<DatabaseGeneratedAttribute>()?.DatabaseGeneratedOption != DatabaseGeneratedOption.Identity)
                .ToDictionary(p => p.Name, p => GetDisplayName(p));

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add(sheetName);

            // 写入表头
            var col = 1;
            foreach (var property in properties)
            {
                worksheet.Cells[1, col].Value = property.Value;

                // 添加数据验证和注释
                var validationAttribute = typeof(T).GetProperty(property.Key)?.GetCustomAttributes<ValidationAttribute>();
                if (validationAttribute?.Any() == true)
                {
                    var comment = string.Join(Environment.NewLine, validationAttribute.Select(x => x.ErrorMessage));
                    worksheet.Cells[2, col].AddComment(comment ?? "请输入有效值", "系统");

                    // 添加数据验证
                    var rangeAttribute = validationAttribute.OfType<RangeAttribute>().FirstOrDefault();
                    if (rangeAttribute != null)
                    {
                        var validation = worksheet.Cells[$"B{col}:ZZ{col}"].DataValidation.AddIntegerDataValidation();
                        validation.Operator = OfficeOpenXml.DataValidation.ExcelDataValidationOperator.between;
                        validation.Formula.Value = Convert.ToInt32(rangeAttribute.Minimum);
                        validation.Formula2.Value = Convert.ToInt32(rangeAttribute.Maximum);
                        validation.ShowErrorMessage = true;
                        validation.Error = rangeAttribute.ErrorMessage;
                        validation.ErrorTitle = "输入错误";
                        validation.ShowInputMessage = true;
                        validation.PromptTitle = "有效范围";
                        validation.Prompt = $"请输入 {rangeAttribute.Minimum} 到 {rangeAttribute.Maximum} 之间的数字";
                    }

                    var regexAttribute = validationAttribute.OfType<RegularExpressionAttribute>().FirstOrDefault();
                    if (regexAttribute != null)
                    {
                        var validation = worksheet.Cells[$"B{col}:ZZ{col}"].DataValidation.AddCustomDataValidation();
                        validation.Formula.ExcelFormula = $"=MATCH(TRUE,{regexAttribute.Pattern},0)";
                        validation.ShowErrorMessage = true;
                        validation.Error = regexAttribute.ErrorMessage;
                        validation.ErrorTitle = "输入错误";
                        validation.ShowInputMessage = true;
                        validation.PromptTitle = "输入规则";
                        validation.Prompt = regexAttribute.ErrorMessage;
                    }
                }
                col++;
            }

            FormatWorksheet(worksheet);
            return await package.GetAsByteArrayAsync();
        }

        /// <summary>
        /// 导入单个Sheet的数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>导入结果</returns>
        public static async Task<(List<T> SuccessItems, List<string> ErrorMessages)> ImportAsync<T>(Stream fileStream, string sheetName = null) where T : class, new()
        {
            var properties = typeof(T).GetProperties()
                .Where(p => p.GetCustomAttribute<NotMappedAttribute>() == null
                    && p.GetCustomAttribute<DatabaseGeneratedAttribute>()?.DatabaseGeneratedOption != DatabaseGeneratedOption.Identity)
                .ToDictionary(p => p.Name, p => GetDisplayName(p));

            return await ImportAsync<T>(fileStream, properties, sheetName);
        }

        /// <summary>
        /// 导入多个Sheet的数据
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="sheetConfigs">Sheet配置（Key: Sheet名称, Value: 列映射）</param>
        /// <returns>导入结果</returns>
        public static async Task<Dictionary<string, (List<T> SuccessItems, List<string> ErrorMessages)>> ImportMultiSheetAsync<T>(
            Stream fileStream,
            Dictionary<string, Dictionary<string, string>> sheetConfigs) where T : class, new()
        {
            var result = new Dictionary<string, (List<T> SuccessItems, List<string> ErrorMessages)>();
            using var package = new ExcelPackage(fileStream);

            foreach (var config in sheetConfigs)
            {
                var worksheet = package.Workbook.Worksheets[config.Key];
                if (worksheet != null)
                {
                    var sheetResult = await ImportSheetAsync<T>(worksheet, config.Value);
                    result.Add(config.Key, sheetResult);
                }
            }

            return result;
        }

        /// <summary>
        /// 导入数据（指定列）
        /// </summary>
        /// <typeparam name="T">数据类型</typeparam>
        /// <param name="fileStream">Excel文件流</param>
        /// <param name="columns">列映射关系（Key: 属性名, Value: Excel列名）</param>
        /// <param name="sheetName">Sheet名称</param>
        /// <returns>导入结果</returns>
        public static async Task<(List<T> SuccessItems, List<string> ErrorMessages)> ImportAsync<T>(
            Stream fileStream,
            Dictionary<string, string> columns,
            string sheetName = null) where T : class, new()
        {
            using var package = new ExcelPackage(fileStream);
            var worksheet = string.IsNullOrEmpty(sheetName) 
                ? package.Workbook.Worksheets[0] 
                : package.Workbook.Worksheets[sheetName];

            if (worksheet == null)
            {
                return (new List<T>(), new List<string> { "未找到指定的Sheet" });
            }

            return await ImportSheetAsync<T>(worksheet, columns);
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 写入Sheet数据
        /// </summary>
        private static async Task WriteSheetDataAsync<T>(ExcelWorksheet worksheet, IEnumerable<T> data, Dictionary<string, string> columns)
        {
            // 写入表头
            var col = 1;
            foreach (var column in columns)
            {
                worksheet.Cells[1, col].Value = column.Value;
                col++;
            }

            // 写入数据
            var row = 2;
            foreach (var item in data)
            {
                col = 1;
                foreach (var column in columns)
                {
                    var value = typeof(T).GetProperty(column.Key)?.GetValue(item);
                    worksheet.Cells[row, col].Value = value;
                    col++;
                }
                row++;
            }

            await Task.CompletedTask;
        }

        /// <summary>
        /// 格式化工作表
        /// </summary>
        private static void FormatWorksheet(ExcelWorksheet worksheet)
        {
            // 设置表头样式
            var headerRange = worksheet.Cells[1, 1, 1, worksheet.Dimension?.Columns ?? 1];
            headerRange.Style.Font.Bold = true;
            headerRange.Style.Fill.PatternType = ExcelFillStyle.Solid;
            headerRange.Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.LightGray);
            headerRange.Style.Border.BorderAround(ExcelBorderStyle.Thin);

            // 自动调整列宽
            worksheet.Cells.AutoFitColumns();
        }

        /// <summary>
        /// 导入Sheet数据
        /// </summary>
        private static async Task<(List<T> SuccessItems, List<string> ErrorMessages)> ImportSheetAsync<T>(
            ExcelWorksheet worksheet,
            Dictionary<string, string> columns) where T : class, new()
        {
            var successItems = new List<T>();
            var errorMessages = new List<string>();

            var rowCount = worksheet.Dimension?.Rows ?? 0;
            var colCount = worksheet.Dimension?.Columns ?? 0;

            if (rowCount <= 1) // 只有表头或空文件
            {
                errorMessages.Add("文件为空或只有表头");
                return (successItems, errorMessages);
            }

            // 验证表头
            var headerRow = 1;
            var columnIndexes = new Dictionary<string, int>();
            for (int col = 1; col <= colCount; col++)
            {
                var headerValue = worksheet.Cells[headerRow, col].Value?.ToString();
                if (string.IsNullOrEmpty(headerValue)) continue;

                var propertyName = columns.FirstOrDefault(x => x.Value == headerValue).Key;
                if (!string.IsNullOrEmpty(propertyName))
                {
                    columnIndexes.Add(propertyName, col);
                }
            }

            // 开始导入数据
            for (int row = 2; row <= rowCount; row++)
            {
                try
                {
                    var item = new T();
                    foreach (var column in columnIndexes)
                    {
                        var property = typeof(T).GetProperty(column.Key);
                        if (property == null) continue;

                        var cellValue = worksheet.Cells[row, column.Value].Value;
                        if (cellValue != null)
                        {
                            try
                            {
                                var propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
                                object convertedValue;

                                if (propertyType.IsEnum)
                                {
                                    convertedValue = Enum.Parse(propertyType, cellValue.ToString());
                                }
                                else if (propertyType == typeof(DateTime))
                                {
                                    convertedValue = DateTime.Parse(cellValue.ToString());
                                }
                                else if (propertyType == typeof(bool))
                                {
                                    convertedValue = Convert.ToBoolean(cellValue);
                                }
                                else
                                {
                                    convertedValue = Convert.ChangeType(cellValue, propertyType);
                                }

                            property.SetValue(item, convertedValue);
                            }
                            catch (Exception ex)
                            {
                                errorMessages.Add($"第{row}行{columns[column.Key]}列数据转换失败：{ex.Message}");
                                continue;
                            }
                        }
                    }

                    // 验证实体
                    var validationContext = new ValidationContext(item);
                    var validationResults = new List<ValidationResult>();
                    if (Validator.TryValidateObject(item, validationContext, validationResults, true))
                    {
                        successItems.Add(item);
                    }
                    else
                    {
                        errorMessages.Add($"第{row}行数据验证失败：{string.Join(", ", validationResults.Select(x => x.ErrorMessage))}");
                    }
                }
                catch (Exception ex)
                {
                    errorMessages.Add($"第{row}行数据处理失败：{ex.Message}");
                }
            }

            await Task.CompletedTask;
            return (successItems, errorMessages);
        }

        /// <summary>
        /// 获取属性显示名称
        /// </summary>
        private static string GetDisplayName(PropertyInfo property)
        {
            var displayAttribute = property.GetCustomAttribute<DisplayAttribute>();
            return displayAttribute?.Name ?? property.Name;
        }

        #endregion
    }
} 
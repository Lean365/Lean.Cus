using Lean.Cus.Generator.Dtos.Designer;
using Lean.Cus.Generator.Entities.Designer;
using Lean.Cus.Generator.IServices.Generator;
using Mapster;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System.Text;

namespace Lean.Cus.Generator.Services.Generator
{
    /// <summary>
    /// 代码生成器服务实现
    /// </summary>
    public class LeanGeneratorService : ILeanGeneratorService
    {
        private readonly ISqlSugarClient _db;
        private readonly string _templatePath;
        private readonly string _outputPath;

        public LeanGeneratorService(ISqlSugarClient db, IConfiguration configuration)
        {
            _db = db;
            _templatePath = configuration["Generator:TemplatePath"] ?? "Templates";
            _outputPath = configuration["Generator:OutputPath"] ?? "Output";
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="id">表设计ID</param>
        /// <returns>生成的代码文件路径</returns>
        public async Task<string> GenerateCodeAsync(long id)
        {
            // 获取表设计信息
            var table = await GetTableDesignerAsync(id);
            if (table == null) return string.Empty;

            // 生成代码
            var codeFiles = await GenerateCodeFilesAsync(table);
            if (!codeFiles.Any()) return string.Empty;

            // 保存代码文件
            var outputDir = Path.Combine(_outputPath, table.TableName);
            if (Directory.Exists(outputDir))
                Directory.Delete(outputDir, true);
            Directory.CreateDirectory(outputDir);

            foreach (var file in codeFiles)
            {
                var filePath = Path.Combine(outputDir, file.Key);
                var fileDir = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(fileDir))
                    Directory.CreateDirectory(fileDir);
                await File.WriteAllTextAsync(filePath, file.Value, Encoding.UTF8);
            }

            // 如果是自定义路径生成
            if (table.GenerateType == 1)
            {
                foreach (var file in codeFiles)
                {
                    var filePath = file.Key;
                    var fileDir = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(fileDir))
                        Directory.CreateDirectory(fileDir);
                    await File.WriteAllTextAsync(filePath, file.Value, Encoding.UTF8);
                }
            }

            return outputDir;
        }

        /// <summary>
        /// 预览代码
        /// </summary>
        /// <param name="id">表设计ID</param>
        /// <returns>预览代码内容</returns>
        public async Task<Dictionary<string, string>> PreviewCodeAsync(long id)
        {
            // 获取表设计信息
            var table = await GetTableDesignerAsync(id);
            if (table == null) return new Dictionary<string, string>();

            // 生成代码
            return await GenerateCodeFilesAsync(table);
        }

        /// <summary>
        /// 同步数据库
        /// </summary>
        /// <param name="id">表设计ID</param>
        /// <returns>是否成功</returns>
        public async Task<bool> SyncDatabaseAsync(long id)
        {
            // 获取表设计信息
            var table = await GetTableDesignerAsync(id);
            if (table == null) return false;

            try
            {
                // 创建表
                var tableSql = GenerateTableSql(table);
                await _db.Ado.ExecuteCommandAsync(tableSql);

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <returns>模板列表</returns>
        public Task<List<string>> GetTemplatesAsync()
        {
            var templates = new List<string> { "Default", "Tree", "Master" };
            return Task.FromResult(templates);
        }

        /// <summary>
        /// 获取前端模板列表
        /// </summary>
        /// <returns>前端模板列表</returns>
        public Task<List<string>> GetFrontendTemplatesAsync()
        {
            var templates = new List<string> { "Default", "Tree", "Master" };
            return Task.FromResult(templates);
        }

        #region 私有方法
        /// <summary>
        /// 获取表设计信息
        /// </summary>
        /// <param name="id">表设计ID</param>
        /// <returns>表设计信息</returns>
        private async Task<LeanTableDesignerDto> GetTableDesignerAsync(long id)
        {
            var entity = await _db.Queryable<LeanTableDesigner>()
                .Includes(x => x.Columns)
                .FirstAsync(x => x.Id == id);
            return entity?.Adapt<LeanTableDesignerDto>();
        }

        /// <summary>
        /// 生成代码文件
        /// </summary>
        /// <param name="table">表设计信息</param>
        /// <returns>代码文件字典</returns>
        private async Task<Dictionary<string, string>> GenerateCodeFilesAsync(LeanTableDesignerDto table)
        {
            var files = new Dictionary<string, string>();

            // 获取模板文件
            var templateDir = Path.Combine(_templatePath, table.Template);
            if (!Directory.Exists(templateDir)) return files;

            // 读取模板文件
            var templateFiles = Directory.GetFiles(templateDir, "*.template", SearchOption.AllDirectories);
            foreach (var templateFile in templateFiles)
            {
                var template = await File.ReadAllTextAsync(templateFile);
                var fileName = Path.GetFileNameWithoutExtension(templateFile);

                // 替换模板变量
                var code = ReplaceTemplateVariables(template, table);
                files.Add(fileName, code);
            }

            return files;
        }

        /// <summary>
        /// 替换模板变量
        /// </summary>
        /// <param name="template">模板内容</param>
        /// <param name="table">表设计信息</param>
        /// <returns>生成的代码</returns>
        private string ReplaceTemplateVariables(string template, LeanTableDesignerDto table)
        {
            var code = template;

            // 替换基本变量
            code = code.Replace("${namespace}", table.NamespacePrefix)
                      .Replace("${module}", table.ModuleName)
                      .Replace("${business}", table.BusinessName)
                      .Replace("${function}", table.FunctionName)
                      .Replace("${author}", table.Author)
                      .Replace("${date}", DateTime.Now.ToString("yyyy-MM-dd"));

            // 替换表信息
            code = code.Replace("${table.name}", table.TableName)
                      .Replace("${table.comment}", table.TableDesc)
                      .Replace("${table.class}", table.EntityName)
                      .Replace("${table.permission}", table.PermissionPrefix);

            // 替换字段信息
            var fieldList = new StringBuilder();
            var importList = new StringBuilder();
            var propertyList = new StringBuilder();
            var columnList = new StringBuilder();

            foreach (var column in table.Columns)
            {
                // 字段定义
                fieldList.AppendLine($"    private {column.CSharpType} {column.CSharpProperty};");

                // 导入语句
                if (column.CSharpType.Contains("."))
                    importList.AppendLine($"using {column.CSharpType.Substring(0, column.CSharpType.LastIndexOf("."))}");

                // 属性定义
                propertyList.AppendLine($@"    /// <summary>
    /// {column.ColumnDesc}
    /// </summary>
    public {column.CSharpType} {column.CSharpProperty} {{ get; set; }}");

                // 列定义
                columnList.AppendLine($@"    [SugarColumn(ColumnName = ""{column.ColumnName}"", ColumnDescription = ""{column.ColumnDesc}"",
        IsNullable = {(column.IsRequired == 0 ? "true" : "false")}, ColumnDataType = ""{column.PhysicalType}"")]");
            }

            code = code.Replace("${field.list}", fieldList.ToString().TrimEnd())
                      .Replace("${import.list}", importList.ToString().TrimEnd())
                      .Replace("${property.list}", propertyList.ToString().TrimEnd())
                      .Replace("${column.list}", columnList.ToString().TrimEnd());

            return code;
        }

        /// <summary>
        /// 生成建表SQL
        /// </summary>
        /// <param name="table">表设计信息</param>
        /// <returns>SQL语句</returns>
        private string GenerateTableSql(LeanTableDesignerDto table)
        {
            var sql = new StringBuilder();
            sql.AppendLine($"CREATE TABLE {table.TableName} (");

            // 添加字段
            foreach (var column in table.Columns)
            {
                sql.AppendLine($"    {column.ColumnName} {column.PhysicalType} {(column.IsRequired == 1 ? "NOT NULL" : "NULL")},");
            }

            // 添加主键
            sql.AppendLine("    PRIMARY KEY (Id)");
            sql.AppendLine(");");

            // 添加注释
            sql.AppendLine($"COMMENT ON TABLE {table.TableName} IS '{table.TableDesc}';");
            foreach (var column in table.Columns)
            {
                sql.AppendLine($"COMMENT ON COLUMN {table.TableName}.{column.ColumnName} IS '{column.ColumnDesc}';");
            }

            return sql.ToString();
        }
        #endregion
    }
} 
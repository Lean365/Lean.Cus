using SqlSugar;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Domain.Entities;

namespace Lean.Cus.Generator.Entities.Designer
{
    /// <summary>
    /// 表设计器
    /// </summary>
    [SugarTable("lean_gen_table_designer", "代码生成器表设计")]
    [SugarIndex("idx_table_name", nameof(TableName), OrderByType.Asc, true)]
    public class LeanTableDesigner : LeanBaseEntity
    {
        #region 基本信息
        /// <summary>
        /// 表名称
        /// </summary>
        [Required(ErrorMessage = "表名称不能为空")]
        [SugarColumn(ColumnName = "table_name", ColumnDescription = "表名称", 
            Length = 200, IsNullable = false, ColumnDataType = "varchar")]
        public string TableName { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        [Required(ErrorMessage = "表描述不能为空")]
        [SugarColumn(ColumnName = "table_desc", ColumnDescription = "表描述", 
            Length = 500, IsNullable = false, ColumnDataType = "nvarchar")]
        public string TableDesc { get; set; }

        /// <summary>
        /// 实体类名
        /// </summary>
        [Required(ErrorMessage = "实体类名不能为空")]
        [SugarColumn(ColumnName = "entity_name", ColumnDescription = "实体类名", 
            Length = 200, IsNullable = false, ColumnDataType = "varchar")]
        public string EntityName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Required(ErrorMessage = "作者不能为空")]
        [SugarColumn(ColumnName = "author", ColumnDescription = "作者", 
            Length = 50, IsNullable = false, ColumnDataType = "varchar")]
        public string Author { get; set; }
        #endregion

        #region 生成信息
        /// <summary>
        /// 生成模板
        /// </summary>
        [Required(ErrorMessage = "生成模板不能为空")]
        [SugarColumn(ColumnName = "template", ColumnDescription = "生成模板", 
            Length = 100, IsNullable = false, ColumnDataType = "varchar")]
        public string Template { get; set; }

        /// <summary>
        /// 前端模板
        /// </summary>
        [Required(ErrorMessage = "前端模板不能为空")]
        [SugarColumn(ColumnName = "frontend_template", ColumnDescription = "前端模板", 
            Length = 100, IsNullable = false, ColumnDataType = "varchar")]
        public string FrontendTemplate { get; set; }

        /// <summary>
        /// 生成命名空间前缀
        /// </summary>
        [Required(ErrorMessage = "生成命名空间前缀不能为空")]
        [SugarColumn(ColumnName = "namespace_prefix", ColumnDescription = "生成命名空间前缀", 
            Length = 100, IsNullable = false, ColumnDataType = "varchar")]
        public string NamespacePrefix { get; set; }

        /// <summary>
        /// 生成模块名称
        /// </summary>
        [Required(ErrorMessage = "生成模块名称不能为空")]
        [SugarColumn(ColumnName = "module_name", ColumnDescription = "生成模块名称", 
            Length = 100, IsNullable = false, ColumnDataType = "varchar")]
        public string ModuleName { get; set; }

        /// <summary>
        /// 生成业务名
        /// </summary>
        [Required(ErrorMessage = "生成业务名不能为空")]
        [SugarColumn(ColumnName = "business_name", ColumnDescription = "生成业务名", 
            Length = 100, IsNullable = false, ColumnDataType = "varchar")]
        public string BusinessName { get; set; }

        /// <summary>
        /// 生成功能名称
        /// </summary>
        [Required(ErrorMessage = "生成功能名称不能为空")]
        [SugarColumn(ColumnName = "function_name", ColumnDescription = "生成功能名称", 
            Length = 100, IsNullable = false, ColumnDataType = "nvarchar")]
        public string FunctionName { get; set; }

        /// <summary>
        /// 上级菜单ID
        /// </summary>
        [SugarColumn(ColumnName = "parent_menu_id", ColumnDescription = "上级菜单ID", 
            IsNullable = true, ColumnDataType = "bigint")]
        public long? ParentMenuId { get; set; }

        /// <summary>
        /// 默认查询排序字段
        /// </summary>
        [SugarColumn(ColumnName = "order_field", ColumnDescription = "默认查询排序字段", 
            Length = 100, IsNullable = true, ColumnDataType = "varchar")]
        public string OrderField { get; set; }

        /// <summary>
        /// 排序方式(0:正序 1:倒序)
        /// </summary>
        [SugarColumn(ColumnName = "order_type", ColumnDescription = "排序方式(0:正序 1:倒序)", 
            IsNullable = false, ColumnDataType = "int")]
        public int OrderType { get; set; }

        /// <summary>
        /// 是否使用雪花ID(0:否 1:是)
        /// </summary>
        [SugarColumn(ColumnName = "use_snowflake_id", ColumnDescription = "是否使用雪花ID(0:否 1:是)", 
            IsNullable = false, ColumnDataType = "int")]
        public int UseSnowflakeId { get; set; }

        /// <summary>
        /// 生成代码方式(0:zip压缩包 1:自定义路径)
        /// </summary>
        [SugarColumn(ColumnName = "generate_type", ColumnDescription = "生成代码方式(0:zip压缩包 1:自定义路径)", 
            IsNullable = false, ColumnDataType = "int")]
        public int GenerateType { get; set; }

        /// <summary>
        /// 是否生成多层级(0:否 1:是)
        /// </summary>
        [SugarColumn(ColumnName = "is_multi_level", ColumnDescription = "是否生成多层级(0:否 1:是)", 
            IsNullable = false, ColumnDataType = "int")]
        public int IsMultiLevel { get; set; }

        /// <summary>
        /// 是否生成仓储层(0:否 1:是)
        /// </summary>
        [SugarColumn(ColumnName = "is_generate_repository", ColumnDescription = "是否生成仓储层(0:否 1:是)", 
            IsNullable = false, ColumnDataType = "int")]
        public int IsGenerateRepository { get; set; }

        /// <summary>
        /// 是否生成SQL差异日志(0:否 1:是)
        /// </summary>
        [SugarColumn(ColumnName = "is_generate_sql_log", ColumnDescription = "是否生成SQL差异日志(0:否 1:是)", 
            IsNullable = false, ColumnDataType = "int")]
        public int IsGenerateSqlLog { get; set; }

        /// <summary>
        /// 权限前缀
        /// </summary>
        [Required(ErrorMessage = "权限前缀不能为空")]
        [SugarColumn(ColumnName = "permission_prefix", ColumnDescription = "权限前缀", 
            Length = 100, IsNullable = false, ColumnDataType = "varchar")]
        public string PermissionPrefix { get; set; }

        /// <summary>
        /// 操作按钮样式(0:button 1:text button)
        /// </summary>
        [SugarColumn(ColumnName = "button_style", ColumnDescription = "操作按钮样式(0:button 1:text button)", 
            IsNullable = false, ColumnDataType = "int")]
        public int ButtonStyle { get; set; }

        /// <summary>
        /// 生成功能选项(新增,修改,删除,导出,查看,清空,批量删除,批量导入)
        /// </summary>
        [Required(ErrorMessage = "生成功能选项不能为空")]
        [SugarColumn(ColumnName = "functions", ColumnDescription = "生成功能选项", 
            Length = 500, IsNullable = false, ColumnDataType = "varchar")]
        public string Functions { get; set; }
        #endregion

        /// <summary>
        /// 字段设计列表
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public List<LeanColumnDesigner> Columns { get; set; }
    }
} 
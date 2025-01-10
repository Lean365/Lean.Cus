using SqlSugar;
using System.ComponentModel.DataAnnotations;
using Lean.Cus.Domain.Entities;

namespace Lean.Cus.Generator.Entities.Import
{
    /// <summary>
    /// 字段导入配置
    /// </summary>
    [SugarTable("gen_column_import", "代码生成器字段导入配置")]
    [SugarIndex("idx_column_table_id", nameof(TableId), OrderByType.Asc)]
    [SugarIndex("idx_column_name", nameof(ColumnName), OrderByType.Asc)]
    public class LeanColumnImport : LeanBaseEntity
    {
        /// <summary>
        /// 表导入配置ID
        /// </summary>
        [Required]
        [SugarColumn(ColumnName = "table_id", ColumnDescription = "表导入配置ID",
            IsNullable = false, ColumnDataType = "bigint")]
        public long TableId { get; set; }

        /// <summary>
        /// 字段列名
        /// </summary>
        [Required]
        [SugarColumn(ColumnName = "column_name", ColumnDescription = "字段列名",
            Length = 200, IsNullable = false, ColumnDataType = "varchar")]
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        [Required]
        [SugarColumn(ColumnName = "column_desc", ColumnDescription = "字段描述",
            Length = 500, IsNullable = false, ColumnDataType = "nvarchar")]
        public string ColumnDesc { get; set; }

        /// <summary>
        /// 物理类型
        /// </summary>
        [Required]
        [SugarColumn(ColumnName = "physical_type", ColumnDescription = "物理类型",
            Length = 50, IsNullable = false, ColumnDataType = "varchar")]
        public string PhysicalType { get; set; }

        /// <summary>
        /// C#类型
        /// </summary>
        [Required]
        [SugarColumn(ColumnName = "csharp_type", ColumnDescription = "C#类型",
            Length = 50, IsNullable = false, ColumnDataType = "varchar")]
        public string CSharpType { get; set; }

        /// <summary>
        /// C#属性
        /// </summary>
        [Required]
        [SugarColumn(ColumnName = "csharp_property", ColumnDescription = "C#属性",
            Length = 50, IsNullable = false, ColumnDataType = "varchar")]
        public string CSharpProperty { get; set; }

        /// <summary>
        /// 是否必填(0:否 1:是)
        /// </summary>
        [SugarColumn(ColumnName = "is_required", ColumnDescription = "是否必填(0:否 1:是)",
            IsNullable = false, ColumnDataType = "int")]
        public int IsRequired { get; set; }

        /// <summary>
        /// 是否前端列表显示(0:否 1:是)
        /// </summary>
        [SugarColumn(ColumnName = "is_list", ColumnDescription = "是否前端列表显示(0:否 1:是)",
            IsNullable = false, ColumnDataType = "int")]
        public int IsList { get; set; }

        /// <summary>
        /// 是否排序(0:否 1:是)
        /// </summary>
        [SugarColumn(ColumnName = "is_sort", ColumnDescription = "是否排序(0:否 1:是)",
            IsNullable = false, ColumnDataType = "int")]
        public int IsSort { get; set; }

        /// <summary>
        /// 是否编辑(0:否 1:是)
        /// </summary>
        [SugarColumn(ColumnName = "is_edit", ColumnDescription = "是否编辑(0:否 1:是)",
            IsNullable = false, ColumnDataType = "int")]
        public int IsEdit { get; set; }

        /// <summary>
        /// 是否导出(0:否 1:是)
        /// </summary>
        [SugarColumn(ColumnName = "is_export", ColumnDescription = "是否导出(0:否 1:是)",
            IsNullable = false, ColumnDataType = "int")]
        public int IsExport { get; set; }

        /// <summary>
        /// 是否查询(0:否 1:是)
        /// </summary>
        [SugarColumn(ColumnName = "is_query", ColumnDescription = "是否查询(0:否 1:是)",
            IsNullable = false, ColumnDataType = "int")]
        public int IsQuery { get; set; }

        /// <summary>
        /// 查询方式(eq:等于 like:模糊 gt:大于 lt:小于 between:范围)
        /// </summary>
        [SugarColumn(ColumnName = "query_type", ColumnDescription = "查询方式",
            Length = 50, IsNullable = true, ColumnDataType = "varchar")]
        public string QueryType { get; set; }

        /// <summary>
        /// 显示类型(文本框、文本域、下拉框、单选框、复选框、日期控件等)
        /// </summary>
        [SugarColumn(ColumnName = "display_type", ColumnDescription = "显示类型",
            Length = 50, IsNullable = true, ColumnDataType = "varchar")]
        public string DisplayType { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        [SugarColumn(ColumnName = "dict_type", ColumnDescription = "字典类型",
            Length = 200, IsNullable = true, ColumnDataType = "varchar")]
        public string DictType { get; set; }

        /// <summary>
        /// 自动填充
        /// </summary>
        [SugarColumn(ColumnName = "auto_fill", ColumnDescription = "自动填充",
            Length = 50, IsNullable = true, ColumnDataType = "varchar")]
        public string AutoFill { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [SugarColumn(ColumnName = "sort", ColumnDescription = "排序",
            IsNullable = false, ColumnDataType = "int")]
        public int Sort { get; set; }
    }
} 
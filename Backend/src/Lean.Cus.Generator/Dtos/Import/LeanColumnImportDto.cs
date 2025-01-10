using System.ComponentModel.DataAnnotations;

namespace Lean.Cus.Generator.Dtos.Import
{
    /// <summary>
    /// 字段导入配置DTO
    /// </summary>
    public class LeanColumnImportDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 表导入配置ID
        /// </summary>
        [Required]
        public long TableId { get; set; }

        /// <summary>
        /// 字段列名
        /// </summary>
        [Required]
        public string ColumnName { get; set; }

        /// <summary>
        /// 字段描述
        /// </summary>
        [Required]
        public string ColumnDesc { get; set; }

        /// <summary>
        /// 物理类型
        /// </summary>
        [Required]
        public string PhysicalType { get; set; }

        /// <summary>
        /// C#类型
        /// </summary>
        [Required]
        public string CSharpType { get; set; }

        /// <summary>
        /// C#属性
        /// </summary>
        [Required]
        public string CSharpProperty { get; set; }

        /// <summary>
        /// 是否必填(0:否 1:是)
        /// </summary>
        public int IsRequired { get; set; }

        /// <summary>
        /// 是否前端列表显示(0:否 1:是)
        /// </summary>
        public int IsList { get; set; }

        /// <summary>
        /// 是否排序(0:否 1:是)
        /// </summary>
        public int IsSort { get; set; }

        /// <summary>
        /// 是否编辑(0:否 1:是)
        /// </summary>
        public int IsEdit { get; set; }

        /// <summary>
        /// 是否导出(0:否 1:是)
        /// </summary>
        public int IsExport { get; set; }

        /// <summary>
        /// 是否查询(0:否 1:是)
        /// </summary>
        public int IsQuery { get; set; }

        /// <summary>
        /// 查询方式(eq:等于 like:模糊 gt:大于 lt:小于 between:范围)
        /// </summary>
        public string QueryType { get; set; }

        /// <summary>
        /// 显示类型(文本框、文本域、下拉框、单选框、复选框、日期控件等)
        /// </summary>
        public string DisplayType { get; set; }

        /// <summary>
        /// 字典类型
        /// </summary>
        public string DictType { get; set; }

        /// <summary>
        /// 自动填充
        /// </summary>
        public string AutoFill { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
} 
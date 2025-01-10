using System.ComponentModel.DataAnnotations;

namespace Lean.Cus.Generator.Dtos.Import
{
    /// <summary>
    /// 表导入配置DTO
    /// </summary>
    public class LeanTableImportDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 表名称
        /// </summary>
        [Required(ErrorMessage = "表名称不能为空")]
        public string TableName { get; set; }

        /// <summary>
        /// 表描述
        /// </summary>
        [Required(ErrorMessage = "表描述不能为空")]
        public string TableDesc { get; set; }

        /// <summary>
        /// 实体类名
        /// </summary>
        [Required(ErrorMessage = "实体类名不能为空")]
        public string EntityName { get; set; }

        /// <summary>
        /// 作者
        /// </summary>
        [Required(ErrorMessage = "作者不能为空")]
        public string Author { get; set; }

        /// <summary>
        /// 生成模板
        /// </summary>
        [Required(ErrorMessage = "生成模板不能为空")]
        public string Template { get; set; }

        /// <summary>
        /// 前端模板
        /// </summary>
        [Required(ErrorMessage = "前端模板不能为空")]
        public string FrontendTemplate { get; set; }

        /// <summary>
        /// 生成命名空间前缀
        /// </summary>
        [Required(ErrorMessage = "生成命名空间前缀不能为空")]
        public string NamespacePrefix { get; set; }

        /// <summary>
        /// 生成模块名称
        /// </summary>
        [Required(ErrorMessage = "生成模块名称不能为空")]
        public string ModuleName { get; set; }

        /// <summary>
        /// 生成业务名
        /// </summary>
        [Required(ErrorMessage = "生成业务名不能为空")]
        public string BusinessName { get; set; }

        /// <summary>
        /// 生成功能名称
        /// </summary>
        [Required(ErrorMessage = "生成功能名称不能为空")]
        public string FunctionName { get; set; }

        /// <summary>
        /// 上级菜单ID
        /// </summary>
        public long? ParentMenuId { get; set; }

        /// <summary>
        /// 默认查询排序字段
        /// </summary>
        public string OrderField { get; set; }

        /// <summary>
        /// 排序方式(0:正序 1:倒序)
        /// </summary>
        public int OrderType { get; set; }

        /// <summary>
        /// 是否使用雪花ID(0:否 1:是)
        /// </summary>
        public int UseSnowflakeId { get; set; }

        /// <summary>
        /// 生成代码方式(0:zip压缩包 1:自定义路径)
        /// </summary>
        public int GenerateType { get; set; }

        /// <summary>
        /// 是否生成多层级(0:否 1:是)
        /// </summary>
        public int IsMultiLevel { get; set; }

        /// <summary>
        /// 是否生成仓储层(0:否 1:是)
        /// </summary>
        public int IsGenerateRepository { get; set; }

        /// <summary>
        /// 是否生成SQL差异日志(0:否 1:是)
        /// </summary>
        public int IsGenerateSqlLog { get; set; }

        /// <summary>
        /// 权限前缀
        /// </summary>
        [Required(ErrorMessage = "权限前缀不能为空")]
        public string PermissionPrefix { get; set; }

        /// <summary>
        /// 操作按钮样式(0:button 1:text button)
        /// </summary>
        public int ButtonStyle { get; set; }

        /// <summary>
        /// 生成功能选项(新增,修改,删除,导出,查看,清空,批量删除,批量导入)
        /// </summary>
        [Required(ErrorMessage = "生成功能选项不能为空")]
        public string Functions { get; set; }

        /// <summary>
        /// 字段导入列表
        /// </summary>
        public List<LeanColumnImportDto> Columns { get; set; }
    }
} 
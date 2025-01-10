using Lean.Cus.Generator.Dtos.Import;

namespace Lean.Cus.Generator.IServices.Import
{
    /// <summary>
    /// 导入服务接口
    /// </summary>
    public interface ILeanImportService
    {
        #region 表导入
        /// <summary>
        /// 获取表导入信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>表导入信息</returns>
        Task<LeanTableImportDto> GetTableAsync(long id);

        /// <summary>
        /// 获取表导入列表
        /// </summary>
        /// <returns>表导入列表</returns>
        Task<List<LeanTableImportDto>> GetTableListAsync();

        /// <summary>
        /// 新增表导入
        /// </summary>
        /// <param name="input">表导入信息</param>
        /// <returns>是否成功</returns>
        Task<bool> CreateTableAsync(LeanTableImportDto input);

        /// <summary>
        /// 更新表导入
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="input">表导入信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateTableAsync(long id, LeanTableImportDto input);

        /// <summary>
        /// 删除表导入
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteTableAsync(long id);
        #endregion

        #region 字段导入
        /// <summary>
        /// 获取字段导入信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>字段导入信息</returns>
        Task<LeanColumnImportDto> GetColumnAsync(long id);

        /// <summary>
        /// 获取字段导入列表
        /// </summary>
        /// <param name="tableId">表导入ID</param>
        /// <returns>字段导入列表</returns>
        Task<List<LeanColumnImportDto>> GetColumnListAsync(long tableId);

        /// <summary>
        /// 新增字段导入
        /// </summary>
        /// <param name="tableId">表导入ID</param>
        /// <param name="input">字段导入信息</param>
        /// <returns>是否成功</returns>
        Task<bool> CreateColumnAsync(long tableId, LeanColumnImportDto input);

        /// <summary>
        /// 更新字段导入
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="input">字段导入信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateColumnAsync(long id, LeanColumnImportDto input);

        /// <summary>
        /// 删除字段导入
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteColumnAsync(long id);
        #endregion

        #region 数据库导入
        /// <summary>
        /// 获取数据库表列表
        /// </summary>
        /// <returns>数据库表列表</returns>
        Task<List<LeanTableImportDto>> GetTablesAsync();

        /// <summary>
        /// 获取数据库表字段列表
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <returns>数据库表字段列表</returns>
        Task<List<LeanColumnImportDto>> GetColumnsAsync(string tableName);

        /// <summary>
        /// 导入到代码生成器
        /// </summary>
        /// <param name="tableNames">表名列表</param>
        /// <returns>是否成功</returns>
        Task<bool> ImportToGeneratorAsync(List<string> tableNames);
        #endregion
    }
} 
using Lean.Cus.Generator.Dtos.Designer;

namespace Lean.Cus.Generator.IServices.Designer
{
    /// <summary>
    /// 设计器服务接口
    /// </summary>
    public interface ILeanDesignerService
    {
        #region 表设计
        /// <summary>
        /// 获取表设计信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>表设计信息</returns>
        Task<LeanTableDesignerDto> GetTableAsync(long id);

        /// <summary>
        /// 获取表设计列表
        /// </summary>
        /// <returns>表设计列表</returns>
        Task<List<LeanTableDesignerDto>> GetTableListAsync();

        /// <summary>
        /// 新增表设计
        /// </summary>
        /// <param name="input">表设计信息</param>
        /// <returns>是否成功</returns>
        Task<bool> CreateTableAsync(LeanTableDesignerDto input);

        /// <summary>
        /// 更新表设计
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="input">表设计信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateTableAsync(long id, LeanTableDesignerDto input);

        /// <summary>
        /// 删除表设计
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteTableAsync(long id);
        #endregion

        #region 字段设计
        /// <summary>
        /// 获取字段设计信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>字段设计信息</returns>
        Task<LeanColumnDesignerDto> GetColumnAsync(long id);

        /// <summary>
        /// 获取字段设计列表
        /// </summary>
        /// <param name="tableId">表设计ID</param>
        /// <returns>字段设计列表</returns>
        Task<List<LeanColumnDesignerDto>> GetColumnListAsync(long tableId);

        /// <summary>
        /// 新增字段设计
        /// </summary>
        /// <param name="tableId">表设计ID</param>
        /// <param name="input">字段设计信息</param>
        /// <returns>是否成功</returns>
        Task<bool> CreateColumnAsync(long tableId, LeanColumnDesignerDto input);

        /// <summary>
        /// 更新字段设计
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="input">字段设计信息</param>
        /// <returns>是否成功</returns>
        Task<bool> UpdateColumnAsync(long id, LeanColumnDesignerDto input);

        /// <summary>
        /// 删除字段设计
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>是否成功</returns>
        Task<bool> DeleteColumnAsync(long id);
        #endregion
    }
} 
using Lean.Cus.Generator.Dtos.Designer;
using Lean.Cus.Generator.Entities.Designer;
using Lean.Cus.Generator.IServices.Designer;
using Mapster;
using SqlSugar;

namespace Lean.Cus.Generator.Services.Designer
{
    /// <summary>
    /// 设计器服务实现
    /// </summary>
    public class LeanDesignerService : ILeanDesignerService
    {
        private readonly ISqlSugarClient _db;

        public LeanDesignerService(ISqlSugarClient db)
        {
            _db = db;
        }

        #region 表设计
        /// <summary>
        /// 获取表设计信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>表设计信息</returns>
        public async Task<LeanTableDesignerDto> GetTableAsync(long id)
        {
            var entity = await _db.Queryable<LeanTableDesigner>()
                .Includes(x => x.Columns)
                .FirstAsync(x => x.Id == id);
            return entity?.Adapt<LeanTableDesignerDto>();
        }

        /// <summary>
        /// 获取表设计列表
        /// </summary>
        /// <returns>表设计列表</returns>
        public async Task<List<LeanTableDesignerDto>> GetTableListAsync()
        {
            var list = await _db.Queryable<LeanTableDesigner>()
                .Includes(x => x.Columns)
                .ToListAsync();
            return list?.Adapt<List<LeanTableDesignerDto>>();
        }

        /// <summary>
        /// 新增表设计
        /// </summary>
        /// <param name="input">表设计信息</param>
        /// <returns>是否成功</returns>
        public async Task<bool> CreateTableAsync(LeanTableDesignerDto input)
        {
            var entity = input.Adapt<LeanTableDesigner>();
            return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新表设计
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="input">表设计信息</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateTableAsync(long id, LeanTableDesignerDto input)
        {
            var entity = input.Adapt<LeanTableDesigner>();
            entity.Id = id;
            return await _db.Updateable(entity).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 删除表设计
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteTableAsync(long id)
        {
            return await _db.Deleteable<LeanTableDesigner>()
                .Where(x => x.Id == id)
                .ExecuteCommandAsync() > 0;
        }
        #endregion

        #region 字段设计
        /// <summary>
        /// 获取字段设计信息
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>字段设计信息</returns>
        public async Task<LeanColumnDesignerDto> GetColumnAsync(long id)
        {
            var entity = await _db.Queryable<LeanColumnDesigner>()
                .FirstAsync(x => x.Id == id);
            return entity?.Adapt<LeanColumnDesignerDto>();
        }

        /// <summary>
        /// 获取字段设计列表
        /// </summary>
        /// <param name="tableId">表设计ID</param>
        /// <returns>字段设计列表</returns>
        public async Task<List<LeanColumnDesignerDto>> GetColumnListAsync(long tableId)
        {
            var list = await _db.Queryable<LeanColumnDesigner>()
                .Where(x => x.TableId == tableId)
                .OrderBy(x => x.Sort)
                .ToListAsync();
            return list?.Adapt<List<LeanColumnDesignerDto>>();
        }

        /// <summary>
        /// 新增字段设计
        /// </summary>
        /// <param name="tableId">表设计ID</param>
        /// <param name="input">字段设计信息</param>
        /// <returns>是否成功</returns>
        public async Task<bool> CreateColumnAsync(long tableId, LeanColumnDesignerDto input)
        {
            var entity = input.Adapt<LeanColumnDesigner>();
            entity.TableId = tableId;
            return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 更新字段设计
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="input">字段设计信息</param>
        /// <returns>是否成功</returns>
        public async Task<bool> UpdateColumnAsync(long id, LeanColumnDesignerDto input)
        {
            var entity = input.Adapt<LeanColumnDesigner>();
            entity.Id = id;
            return await _db.Updateable(entity).ExecuteCommandAsync() > 0;
        }

        /// <summary>
        /// 删除字段设计
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>是否成功</returns>
        public async Task<bool> DeleteColumnAsync(long id)
        {
            return await _db.Deleteable<LeanColumnDesigner>()
                .Where(x => x.Id == id)
                .ExecuteCommandAsync() > 0;
        }
        #endregion
    }
} 
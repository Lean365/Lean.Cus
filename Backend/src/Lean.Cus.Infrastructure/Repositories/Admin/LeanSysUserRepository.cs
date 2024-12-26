//===================================================
// 项目名称：Lean.Cus.Infrastructure
// 文件名称：LeanSysUserRepository
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.14
// 更新时间：2024.01.14
// 备注说明：系统用户仓储实现
//===================================================

using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.IRepositories.Admin;
using SqlSugar;

namespace Lean.Cus.Infrastructure.Repositories.Admin;

/// <summary>
/// 系统用户仓储实现
/// </summary>
public class LeanSysUserRepository : ILeanSysUserRepository
{
    private readonly ISqlSugarClient _db;

    public LeanSysUserRepository(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 检查用户名是否存在
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="id">用户ID（排除自身）</param>
    /// <returns>是否存在</returns>
    public async Task<bool> ExistsUserNameAsync(string userName, long? id = null)
    {
        return await _db.Queryable<LeanSysUser>()
            .WhereIF(id.HasValue, u => u.Id != id.Value)
            .AnyAsync(u => u.UserName == userName);
    }

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>用户信息</returns>
    public async Task<LeanSysUser?> GetAsync(long id)
    {
        return await _db.Queryable<LeanSysUser>()
            .FirstAsync(u => u.Id == id);
    }

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="entity">用户信息</param>
    /// <returns>是否成功</returns>
    public async Task<bool> CreateAsync(LeanSysUser entity)
    {
        return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="entity">用户信息</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateAsync(LeanSysUser entity)
    {
        return await _db.Updateable(entity)
            .IgnoreColumns(u => new { u.Password, u.Salt, u.CreateTime })
            .ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _db.Deleteable<LeanSysUser>()
            .Where(u => u.Id == id)
            .ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="password">新密码</param>
    /// <param name="salt">盐值</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdatePasswordAsync(long id, string password, string salt)
    {
        return await _db.Updateable<LeanSysUser>()
            .SetColumns(u => new LeanSysUser
            {
                Password = password,
                Salt = salt,
                UpdateTime = DateTime.Now
            })
            .Where(u => u.Id == id)
            .ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="status">状态</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateStatusAsync(long id, int status)
    {
        return await _db.Updateable<LeanSysUser>()
            .SetColumns(u => new LeanSysUser
            {
                Status = status,
                UpdateTime = DateTime.Now
            })
            .Where(u => u.Id == id)
            .ExecuteCommandAsync() > 0;
    }
} 
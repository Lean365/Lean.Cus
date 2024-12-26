//===================================================
// 项目名称：Lean.Cus.Domain
// 文件名称：ILeanSysUserRepository
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.14
// 更新时间：2024.01.14
// 备注说明：系统用户仓储接口
//===================================================

using Lean.Cus.Domain.Entities.Admin;

namespace Lean.Cus.Domain.IRepositories.Admin;

/// <summary>
/// 系统用户仓储接口
/// </summary>
public interface ILeanSysUserRepository
{
    /// <summary>
    /// 检查用户名是否存在
    /// </summary>
    /// <param name="userName">用户名</param>
    /// <param name="id">用户ID（排除自身）</param>
    /// <returns>是否存在</returns>
    Task<bool> ExistsUserNameAsync(string userName, long? id = null);

    /// <summary>
    /// 获取用户信息
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>用户信息</returns>
    Task<LeanSysUser?> GetAsync(long id);

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="entity">用户信息</param>
    /// <returns>是否成功</returns>
    Task<bool> CreateAsync(LeanSysUser entity);

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="entity">用户信息</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateAsync(LeanSysUser entity);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="password">新密码</param>
    /// <param name="salt">盐值</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdatePasswordAsync(long id, string password, string salt);

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <param name="status">状态</param>
    /// <returns>是否成功</returns>
    Task<bool> UpdateStatusAsync(long id, int status);
} 
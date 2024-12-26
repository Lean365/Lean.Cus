//===================================================
// 项目名称：Lean.Cus.Application
// 文件名称：ILeanSysUserService
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.14
// 更新时间：2024.01.14
// 备注说明：系统用户服务接口
//===================================================

using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Results;

namespace Lean.Cus.Application.IServices.Admin;

/// <summary>
/// 系统用户服务接口
/// </summary>
public interface ILeanSysUserService
{
    /// <summary>
    /// 获取用户分页列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页列表</returns>
    Task<LeanApiResult<LeanPageResult<LeanSysUserBaseDto>>> GetPageAsync(LeanSysUserQueryDto query);

    /// <summary>
    /// 获取用户详情
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>用户详情</returns>
    Task<LeanApiResult<LeanSysUserBaseDto>> GetAsync(long id);

    /// <summary>
    /// 创建用户
    /// </summary>
    /// <param name="dto">用户信息</param>
    /// <returns>创建结果</returns>
    Task<LeanApiResult<LeanSysUserBaseDto>> CreateAsync(LeanSysUserCreateDto dto);

    /// <summary>
    /// 更新用户
    /// </summary>
    /// <param name="dto">用户信息</param>
    /// <returns>更新结果</returns>
    Task<LeanApiResult<bool>> UpdateAsync(LeanSysUserUpdateDto dto);

    /// <summary>
    /// 删除用户
    /// </summary>
    /// <param name="id">用户ID</param>
    /// <returns>删除结果</returns>
    Task<LeanApiResult<bool>> DeleteAsync(long id);

    /// <summary>
    /// 修改密码
    /// </summary>
    /// <param name="dto">密码信息</param>
    /// <returns>修改结果</returns>
    Task<LeanApiResult<bool>> UpdatePasswordAsync(LeanSysUserPasswordDto dto);

    /// <summary>
    /// 重置密码
    /// </summary>
    /// <param name="dto">密码信息</param>
    /// <returns>重置结果</returns>
    Task<LeanApiResult<bool>> ResetPasswordAsync(LeanSysUserResetPasswordDto dto);

    /// <summary>
    /// 修改状态
    /// </summary>
    /// <param name="dto">状态信息</param>
    /// <returns>修改结果</returns>
    Task<LeanApiResult<bool>> UpdateStatusAsync(LeanSysUserStatusDto dto);
}
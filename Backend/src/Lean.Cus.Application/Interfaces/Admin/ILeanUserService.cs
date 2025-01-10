using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Application.Interfaces.Admin;

/// <summary>
/// 用户服务接口
/// </summary>
public interface ILeanUserService
{
    /// <summary>
    /// 新增用户
    /// </summary>
    Task<LeanUserDto> AddAsync(LeanUserDto dto);

    /// <summary>
    /// 更新用户
    /// </summary>
    Task<bool> UpdateAsync(LeanUserDto dto);

    /// <summary>
    /// 删除用户
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 获取用户
    /// </summary>
    Task<LeanUserDto> GetAsync(long id);

    /// <summary>
    /// 获取用户列表
    /// </summary>
    Task<List<LeanUserDto>> GetListAsync(LeanUserQueryDto query);

    /// <summary>
    /// 分页查询用户
    /// </summary>
    Task<PagedResults<LeanUserDto>> GetPagedListAsync(LeanUserQueryDto query);

    /// <summary>
    /// 导入用户数据
    /// </summary>
    Task<(List<LeanUserDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes);

    /// <summary>
    /// 导出用户数据
    /// </summary>
    Task<byte[]> ExportAsync(LeanUserQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<byte[]> GetImportTemplateAsync();

    /// <summary>
    /// 重置密码
    /// </summary>
    Task<string> ResetPasswordAsync(LeanUserResetPasswordDto input);

    /// <summary>
    /// 修改密码
    /// </summary>
    Task ChangePasswordAsync(LeanUserChangePasswordDto input);

    /// <summary>
    /// 验证用户凭证
    /// </summary>
    Task<bool> ValidateCredentialsAsync(string username, string password);
} 
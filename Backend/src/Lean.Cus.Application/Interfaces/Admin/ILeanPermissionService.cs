using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Application.Interfaces.Admin;

/// <summary>
/// 权限服务接口
/// </summary>
public interface ILeanPermissionService
{
    /// <summary>
    /// 新增权限
    /// </summary>
    Task<LeanPermissionDto> AddAsync(LeanPermissionDto dto);

    /// <summary>
    /// 更新权限
    /// </summary>
    Task<bool> UpdateAsync(LeanPermissionDto dto);

    /// <summary>
    /// 删除权限
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 获取权限
    /// </summary>
    Task<LeanPermissionDto> GetAsync(long id);

    /// <summary>
    /// 获取权限列表
    /// </summary>
    Task<List<LeanPermissionDto>> GetListAsync(LeanPermissionQueryDto query);

    /// <summary>
    /// 分页查询权限
    /// </summary>
    Task<PagedResults<LeanPermissionDto>> GetPagedListAsync(LeanPermissionQueryDto query);

    /// <summary>
    /// 导入权限数据
    /// </summary>
    Task<(List<LeanPermissionDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes);

    /// <summary>
    /// 导出权限数据
    /// </summary>
    Task<byte[]> ExportAsync(LeanPermissionQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<byte[]> GetImportTemplateAsync();

    /// <summary>
    /// 获取用户权限列表
    /// </summary>
    Task<List<string>> GetUserPermissionsAsync(long userId);

    /// <summary>
    /// 获取角色权限列表
    /// </summary>
    Task<List<string>> GetRolePermissionsAsync(long roleId);
} 
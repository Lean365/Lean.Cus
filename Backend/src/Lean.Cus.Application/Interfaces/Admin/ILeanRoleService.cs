using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Application.Interfaces.Admin;

/// <summary>
/// 角色服务接口
/// </summary>
public interface ILeanRoleService
{
    /// <summary>
    /// 新增角色
    /// </summary>
    Task<LeanRoleDto> AddAsync(LeanRoleDto dto);

    /// <summary>
    /// 更新角色
    /// </summary>
    Task<bool> UpdateAsync(LeanRoleDto dto);

    /// <summary>
    /// 删除角色
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 获取角色
    /// </summary>
    Task<LeanRoleDto> GetAsync(long id);

    /// <summary>
    /// 获取角色列表
    /// </summary>
    Task<List<LeanRoleDto>> GetListAsync(LeanRoleQueryDto query);

    /// <summary>
    /// 分页查询角色
    /// </summary>
    Task<PagedResults<LeanRoleDto>> GetPagedListAsync(LeanRoleQueryDto query);

    /// <summary>
    /// 导入角色数据
    /// </summary>
    Task<(List<LeanRoleDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes);

    /// <summary>
    /// 导出角色数据
    /// </summary>
    Task<byte[]> ExportAsync(LeanRoleQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<byte[]> GetImportTemplateAsync();

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    Task<List<LeanRoleDto>> GetUserRolesAsync(long userId);

    /// <summary>
    /// 分配角色权限
    /// </summary>
    Task<bool> AssignPermissionsAsync(long roleId, List<long> permissionIds);

    /// <summary>
    /// 分配角色部门
    /// </summary>
    Task<bool> AssignDepartmentsAsync(long roleId, List<long> departmentIds);
} 
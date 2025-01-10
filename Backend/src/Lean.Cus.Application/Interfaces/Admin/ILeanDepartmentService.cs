using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Application.Interfaces.Admin;

/// <summary>
/// 部门服务接口
/// </summary>
public interface ILeanDepartmentService
{
    /// <summary>
    /// 新增部门
    /// </summary>
    Task<LeanDepartmentDto> AddAsync(LeanDepartmentDto dto);

    /// <summary>
    /// 更新部门
    /// </summary>
    Task<bool> UpdateAsync(LeanDepartmentDto dto);

    /// <summary>
    /// 删除部门
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 获取部门
    /// </summary>
    Task<LeanDepartmentDto> GetAsync(long id);

    /// <summary>
    /// 获取部门列表
    /// </summary>
    Task<List<LeanDepartmentDto>> GetListAsync(LeanDepartmentQueryDto query);

    /// <summary>
    /// 分页查询部门
    /// </summary>
    Task<PagedResults<LeanDepartmentDto>> GetPagedListAsync(LeanDepartmentQueryDto query);

    /// <summary>
    /// 导入部门数据
    /// </summary>
    Task<(List<LeanDepartmentDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes);

    /// <summary>
    /// 导出部门数据
    /// </summary>
    Task<byte[]> ExportAsync(LeanDepartmentQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<byte[]> GetImportTemplateAsync();

    /// <summary>
    /// 获取用户部门列表
    /// </summary>
    Task<List<LeanDepartmentDto>> GetUserDepartmentsAsync(long userId);

    /// <summary>
    /// 获取角色部门列表
    /// </summary>
    Task<List<LeanDepartmentDto>> GetRoleDepartmentsAsync(long roleId);

    /// <summary>
    /// 获取部门树
    /// </summary>
    Task<List<LeanDepartmentDto>> GetDepartmentTreeAsync();
} 
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Common.Excel;
using Lean.Cus.Common.Paging;
using System.IO;
using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Application.Dtos.Admin;
using Mapster;
using SqlSugar;
using System;
using System.Linq;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 角色服务实现
/// </summary>
public class LeanRoleService : ILeanRoleService
{
    private readonly ILeanRepository<LeanRole> _roleRepository;
    private readonly ILeanRepository<LeanRolePermission> _rolePermissionRepository;
    private readonly ILeanRepository<LeanRoleDepartment> _roleDepartmentRepository;
    private readonly ILeanRepository<LeanRoleMenu> _roleMenuRepository;

    /// <summary>
    /// 初始化角色服务
    /// </summary>
    public LeanRoleService(
        ILeanRepository<LeanRole> roleRepository,
        ILeanRepository<LeanRolePermission> rolePermissionRepository,
        ILeanRepository<LeanRoleDepartment> roleDepartmentRepository,
        ILeanRepository<LeanRoleMenu> roleMenuRepository)
    {
        _roleRepository = roleRepository;
        _rolePermissionRepository = rolePermissionRepository;
        _roleDepartmentRepository = roleDepartmentRepository;
        _roleMenuRepository = roleMenuRepository;
    }

    /// <summary>
    /// 新增角色
    /// </summary>
    public async Task<LeanRoleDto> CreateAsync(LeanRoleDto dto)
    {
        var role = dto.Adapt<LeanRole>();
        await _roleRepository.InsertAsync(role);
        return role.Adapt<LeanRoleDto>();
    }

    /// <summary>
    /// 更新角色
    /// </summary>
    public async Task<bool> UpdateAsync(LeanRoleDto dto)
    {
        var role = await _roleRepository.GetByIdAsync(dto.Id);
        if (role == null)
            return false;

        dto.Adapt(role);
        var result = await _roleRepository.UpdateAsync(role);
        return result > 0;
    }

    /// <summary>
    /// 删除角色
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _roleRepository.DeleteAsync(u => u.Id == id) > 0;
    }

    /// <summary>
    /// 获取角色
    /// </summary>
    public async Task<LeanRoleDto> GetAsync(long id)
    {
        var role = await _roleRepository.GetAsync(x => x.Id == id);
        return role.Adapt<LeanRoleDto>();
    }

    /// <summary>
    /// 获取角色列表
    /// </summary>
    public async Task<List<LeanRoleDto>> GetListAsync(LeanRoleQueryDto query)
    {
        var roles = await _roleRepository.GetListAsync(u =>
            (string.IsNullOrEmpty(query.RoleName) || u.Name.Contains(query.RoleName)) &&
            (string.IsNullOrEmpty(query.RoleCode) || u.Code.Contains(query.RoleCode)) &&
            (!query.Status.HasValue || u.Status == query.Status.Value) &&
            (!query.CreatedTimeStart.HasValue || u.CreateTime >= query.CreatedTimeStart.Value) &&
            (!query.CreatedTimeEnd.HasValue || u.CreateTime <= query.CreatedTimeEnd.Value));

        return roles.Adapt<List<LeanRoleDto>>();
    }

    /// <summary>
    /// 分页查询角色
    /// </summary>
    public async Task<PagedResults<LeanRoleDto>> GetPagedListAsync(LeanRoleQueryDto query)
    {
        RefAsync<int> total = 0;
        var list = await _roleRepository.GetPageListAsync(u =>
            (string.IsNullOrEmpty(query.RoleName) || u.Name.Contains(query.RoleName)) &&
            (string.IsNullOrEmpty(query.RoleCode) || u.Code.Contains(query.RoleCode)) &&
            (!query.Status.HasValue || u.Status == query.Status.Value) &&
            (!query.CreatedTimeStart.HasValue || u.CreateTime >= query.CreatedTimeStart.Value) &&
            (!query.CreatedTimeEnd.HasValue || u.CreateTime <= query.CreatedTimeEnd.Value),
            query.PageIndex,
            query.PageSize,
            total);

        var roleDtos = list.Adapt<List<LeanRoleDto>>();
        return new PagedResults<LeanRoleDto>
        {
            Items = roleDtos,
            Total = total,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 导入角色数据
    /// </summary>
    public async Task<(List<LeanRoleDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes)
    {
        using var stream = new MemoryStream(fileBytes);
        var result = await LeanExcelHelper.ImportAsync<LeanRole>(stream);
        if (result.SuccessItems.Count > 0)
        {
            await _roleRepository.InsertRangeAsync(result.SuccessItems);
        }
        return (result.SuccessItems.Adapt<List<LeanRoleDto>>(), result.ErrorMessages);
    }

    /// <summary>
    /// 导出角色数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanRoleQueryDto query)
    {
        var roles = await GetListAsync(query);
        var exportItems = roles.Adapt<List<LeanRoleExportDto>>();
        return await LeanExcelHelper.ExportAsync(exportItems);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    public async Task<byte[]> GetImportTemplateAsync()
    {
        return await LeanExcelHelper.GetImportTemplateAsync<LeanRole>();
    }

    /// <summary>
    /// 获取用户角色列表
    /// </summary>
    public async Task<List<LeanRoleDto>> GetUserRolesAsync(long userId)
    {
        var roles = await _roleRepository.GetListAsync(u => 
            u.Status == Common.Enums.LeanStatus.Enabled &&
            SqlFunc.Subqueryable<LeanUserRole>()
                .Where(r => r.UserId == userId && r.RoleId == u.Id)
                .Any());

        return roles.Adapt<List<LeanRoleDto>>();
    }

    /// <summary>
    /// 分配角色权限
    /// </summary>
    public async Task<bool> AssignPermissionsAsync(long roleId, List<long> permissionIds)
    {
        await _rolePermissionRepository.DeleteAsync(x => x.RoleId == roleId);

        var rolePermissions = permissionIds.Select(permissionId => new LeanRolePermission
        {
            RoleId = roleId,
            PermissionId = permissionId
        }).ToList();

        var result = await _rolePermissionRepository.InsertRangeAsync(rolePermissions);
        return result > 0;
    }

    /// <summary>
    /// 分配角色部门
    /// </summary>
    public async Task<bool> AssignDepartmentsAsync(long roleId, List<long> departmentIds)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
            return false;

        // 删除原有的部门关联
        await _roleDepartmentRepository.DeleteAsync(rd => rd.RoleId == roleId);

        // 添加新的部门关联
        var roleDepartments = departmentIds.Select(departmentId => new LeanRoleDepartment
        {
            RoleId = roleId,
            DepartmentId = departmentId
        }).ToList();

        await _roleDepartmentRepository.InsertRangeAsync(roleDepartments);
        return true;
    }

    /// <summary>
    /// 更新角色状态
    /// </summary>
    public async Task<bool> UpdateStatusAsync(LeanRoleStatusUpdateDto input)
    {
        var role = await _roleRepository.GetByIdAsync(input.Id);
        if (role == null)
            return false;

        role.Status = input.Status;
        return await _roleRepository.UpdateAsync(role) > 0;
    }

    /// <summary>
    /// 分配角色菜单
    /// </summary>
    public async Task<bool> AssignMenusAsync(long roleId, List<long> menuIds)
    {
        var role = await _roleRepository.GetByIdAsync(roleId);
        if (role == null)
            return false;

        // 删除现有菜单关联
        await _roleMenuRepository.DeleteAsync(rm => rm.RoleId == roleId);

        // 添加新的菜单关联
        if (menuIds?.Count > 0)
        {
            var roleMenus = menuIds.Select(menuId => new LeanRoleMenu
            {
                RoleId = roleId,
                MenuId = menuId
            }).ToList();

            await _roleMenuRepository.InsertRangeAsync(roleMenus);
        }

        return true;
    }
} 
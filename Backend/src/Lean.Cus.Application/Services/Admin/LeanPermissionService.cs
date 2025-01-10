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
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 权限服务实现
/// </summary>
public class LeanPermissionService : ILeanPermissionService
{
    private readonly ILeanRepository<LeanPermission> _permissionRepository;

    /// <summary>
    /// 初始化权限服务
    /// </summary>
    public LeanPermissionService(ILeanRepository<LeanPermission> permissionRepository)
    {
        _permissionRepository = permissionRepository;
    }

    /// <summary>
    /// 新增权限
    /// </summary>
    public async Task<LeanPermissionDto> AddAsync(LeanPermissionDto dto)
    {
        var permission = dto.Adapt<LeanPermission>();
        await _permissionRepository.InsertAsync(permission);
        return permission.Adapt<LeanPermissionDto>();
    }

    /// <summary>
    /// 更新权限
    /// </summary>
    public async Task<bool> UpdateAsync(LeanPermissionDto dto)
    {
        var permission = dto.Adapt<LeanPermission>();
        return await _permissionRepository.UpdateAsync(permission) > 0;
    }

    /// <summary>
    /// 删除权限
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _permissionRepository.DeleteAsync(p => p.Id == id) > 0;
    }

    /// <summary>
    /// 获取权限
    /// </summary>
    public async Task<LeanPermissionDto> GetAsync(long id)
    {
        var permission = await _permissionRepository.GetAsync(p => p.Id == id);
        return permission.Adapt<LeanPermissionDto>();
    }

    /// <summary>
    /// 获取权限列表
    /// </summary>
    public async Task<List<LeanPermissionDto>> GetListAsync(LeanPermissionQueryDto query)
    {
        var permissions = await _permissionRepository.GetListAsync(p =>
            (string.IsNullOrEmpty(query.PermissionName) || p.Name.Contains(query.PermissionName)) &&
            (string.IsNullOrEmpty(query.PermissionCode) || p.Code.Contains(query.PermissionCode)) &&
            (!query.Status.HasValue || p.IsEnabled == (query.Status.Value == LeanStatus.Enabled)));
        return permissions.Adapt<List<LeanPermissionDto>>();
    }

    /// <summary>
    /// 分页查询权限
    /// </summary>
    public async Task<PagedResults<LeanPermissionDto>> GetPagedListAsync(LeanPermissionQueryDto query)
    {
        RefAsync<int> total = 0;
        var list = await _permissionRepository.GetPageListAsync(p =>
            (string.IsNullOrEmpty(query.PermissionName) || p.Name.Contains(query.PermissionName)) &&
            (string.IsNullOrEmpty(query.PermissionCode) || p.Code.Contains(query.PermissionCode)) &&
            (!query.Status.HasValue || p.IsEnabled == (query.Status.Value == LeanStatus.Enabled)) &&
            (!query.CreatedTimeStart.HasValue || p.CreatedTime >= query.CreatedTimeStart.Value) &&
            (!query.CreatedTimeEnd.HasValue || p.CreatedTime <= query.CreatedTimeEnd.Value),
            query.PageIndex,
            query.PageSize,
            total);

        var dtos = list.Adapt<List<LeanPermissionDto>>();
        return new PagedResults<LeanPermissionDto>
        {
            Items = dtos,
            Total = total,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 导入权限数据
    /// </summary>
    public async Task<(List<LeanPermissionDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes)
    {
        using var stream = new MemoryStream(fileBytes);
        var result = await LeanExcelHelper.ImportAsync<LeanPermission>(stream);
        if (result.SuccessItems.Count > 0)
        {
            await _permissionRepository.InsertRangeAsync(result.SuccessItems);
        }
        return (result.SuccessItems.Adapt<List<LeanPermissionDto>>(), result.ErrorMessages);
    }

    /// <summary>
    /// 导出权限数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanPermissionQueryDto query)
    {
        var list = await _permissionRepository.GetListAsync(p =>
            (string.IsNullOrEmpty(query.PermissionName) || p.Name.Contains(query.PermissionName)) &&
            (string.IsNullOrEmpty(query.PermissionCode) || p.Code.Contains(query.PermissionCode)) &&
            (!query.Status.HasValue || p.IsEnabled == (query.Status.Value == LeanStatus.Enabled)));
        return await LeanExcelHelper.ExportAsync(list);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    public async Task<byte[]> GetImportTemplateAsync()
    {
        return await LeanExcelHelper.GetImportTemplateAsync<LeanPermission>();
    }

    /// <summary>
    /// 获取用户权限列表
    /// </summary>
    public async Task<List<string>> GetUserPermissionsAsync(long userId)
    {
        // 需要实现用户-角色-权限关联查询
        var permissions = await _permissionRepository.GetListAsync(p => p.Id > 0);
        return permissions.Select(p => p.Code).ToList();
    }

    /// <summary>
    /// 获取角色权限列表
    /// </summary>
    public async Task<List<string>> GetRolePermissionsAsync(long roleId)
    {
        // 需要实现角色-权限关联查询
        var permissions = await _permissionRepository.GetListAsync(p => p.Id > 0);
        return permissions.Select(p => p.Code).ToList();
    }
} 
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件的所有修改。
// </auto-generated>
//
// <copyright file="LeanMenuService.cs" company="Lean365">
//     Copyright (c) Lean365. All rights reserved.
// </copyright>
// <author>Lean365</author>
// <date>2024-01-09</date>
// <summary>
// 菜单服务实现
// </summary>
//------------------------------------------------------------------------------

using System.Linq.Expressions;
using Mapster;
using SqlSugar;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Common.Excel;
using Lean.Cus.Common.Exceptions;
using Lean.Cus.Common.Enums;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Paging;
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.IRepositories;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 菜单服务实现
/// </summary>
public class LeanMenuService : ILeanMenuService
{
    private readonly ILeanRepository<LeanMenu> _menuRepository;

    public LeanMenuService(ILeanRepository<LeanMenu> menuRepository)
    {
        _menuRepository = menuRepository;
    }

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>菜单列表</returns>
    public async Task<List<LeanMenuDto>> GetListAsync(LeanMenuQueryDto query)
    {
        var list = await _menuRepository.GetListAsync(m =>
            (string.IsNullOrEmpty(query.MenuName) || m.MenuName.Contains(query.MenuName)) &&
            (string.IsNullOrEmpty(query.MenuCode) || m.MenuCode.Contains(query.MenuCode)) &&
            (!query.MenuType.HasValue || m.MenuType == query.MenuType.Value) &&
            (string.IsNullOrEmpty(query.PermissionCode) || m.PermissionCode == query.PermissionCode) &&
            (!query.Status.HasValue || m.Status == query.Status.Value));

        return list.Adapt<List<LeanMenuDto>>();
    }

    /// <summary>
    /// 获取分页菜单列表
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>分页菜单列表</returns>
    public async Task<PagedResults<LeanMenuDto>> GetPagedListAsync(LeanMenuQueryDto query)
    {
        RefAsync<int> total = 0;
        var list = await _menuRepository.GetPageListAsync(m =>
            (string.IsNullOrEmpty(query.MenuName) || m.MenuName.Contains(query.MenuName)) &&
            (string.IsNullOrEmpty(query.MenuCode) || m.MenuCode.Contains(query.MenuCode)) &&
            (!query.MenuType.HasValue || m.MenuType == query.MenuType.Value) &&
            (string.IsNullOrEmpty(query.PermissionCode) || m.PermissionCode == query.PermissionCode) &&
            (!query.Status.HasValue || m.Status == query.Status.Value) &&
            (!query.CreatedTimeStart.HasValue || m.CreateTime >= query.CreatedTimeStart.Value) &&
            (!query.CreatedTimeEnd.HasValue || m.CreateTime <= query.CreatedTimeEnd.Value),
            query.PageIndex,
            query.PageSize,
            total);

        var menuDtos = list.Adapt<List<LeanMenuDto>>();
        return new PagedResults<LeanMenuDto>
        {
            Items = menuDtos,
            Total = total,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 获取菜单树
    /// </summary>
    /// <returns>菜单树</returns>
    public async Task<List<LeanMenuDto>> GetMenuTreeAsync()
    {
        var list = await _menuRepository.GetListAsync();
        var dtoList = list.Adapt<List<LeanMenuDto>>();
        return BuildTree(dtoList);
    }

    /// <summary>
    /// 获取用户菜单树
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>用户菜单树</returns>
    public async Task<List<LeanMenuDto>> GetUserMenuTreeAsync(long userId)
    {
        // TODO: 根据用户ID获取菜单权限
        var list = await _menuRepository.GetListAsync();
        var dtoList = list.Adapt<List<LeanMenuDto>>();
        return BuildTree(dtoList);
    }

    /// <summary>
    /// 获取角色菜单树
    /// </summary>
    /// <param name="roleId">角色ID</param>
    /// <returns>角色菜单树</returns>
    public async Task<List<LeanMenuDto>> GetRoleMenuTreeAsync(long roleId)
    {
        // TODO: 根据角色ID获取菜单权限
        var list = await _menuRepository.GetListAsync();
        var dtoList = list.Adapt<List<LeanMenuDto>>();
        return BuildTree(dtoList);
    }

    /// <summary>
    /// 获取菜单详情
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>菜单详情</returns>
    public async Task<LeanMenuDto> GetAsync(long id)
    {
        var menu = await _menuRepository.GetByIdAsync(id);
        if (menu == null)
        {
            throw new LeanUserFriendlyException($"菜单[{id}]不存在");
        }
        return menu.Adapt<LeanMenuDto>();
    }

    /// <summary>
    /// 创建菜单
    /// </summary>
    /// <param name="dto">创建对象</param>
    /// <returns>菜单ID</returns>
    public async Task<LeanMenuDto> CreateAsync(LeanMenuDto dto)
    {
        // 检查菜单编码是否已存在
        var exists = await _menuRepository.ExistsAsync(m => m.MenuCode == dto.MenuCode);
        if (exists)
        {
            throw new LeanUserFriendlyException($"菜单编码[{dto.MenuCode}]已存在");
        }

        var menu = dto.Adapt<LeanMenu>();
        await _menuRepository.InsertAsync(menu);
        return menu.Adapt<LeanMenuDto>();
    }

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="dto">更新对象</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateAsync(LeanMenuDto dto)
    {
        var menu = await _menuRepository.GetByIdAsync(dto.Id);
        if (menu == null)
        {
            throw new LeanUserFriendlyException($"菜单[{dto.Id}]不存在");
        }

        // 检查菜单编码是否已存在
        var exists = await _menuRepository.ExistsAsync(m => m.MenuCode == dto.MenuCode && m.Id != dto.Id);
        if (exists)
        {
            throw new LeanUserFriendlyException($"菜单编码[{dto.MenuCode}]已存在");
        }

        dto.Adapt(menu);
        return await _menuRepository.UpdateAsync(menu) > 0;
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>是否成功</returns>
    public async Task<bool> DeleteAsync(long id)
    {
        // 检查是否存在子菜单
        var hasChildren = await _menuRepository.ExistsAsync(m => m.ParentId == id);
        if (hasChildren)
        {
            throw new LeanUserFriendlyException("存在子菜单，无法删除");
        }

        return await _menuRepository.DeleteAsync(m => m.Id == id) > 0;
    }

    /// <summary>
    /// 更新菜单状态
    /// </summary>
    /// <param name="input">状态更新对象</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateStatusAsync(LeanMenuStatusUpdateDto input)
    {
        var menu = await _menuRepository.GetByIdAsync(input.Id);
        if (menu == null)
        {
            throw new LeanUserFriendlyException($"菜单[{input.Id}]不存在");
        }

        menu.Status = input.Status;
        return await _menuRepository.UpdateAsync(menu) > 0;
    }

    /// <summary>
    /// 更新菜单排序
    /// </summary>
    /// <param name="input">排序对象</param>
    /// <returns>是否成功</returns>
    public async Task<bool> UpdateSortAsync(LeanMenuSortDto input)
    {
        var menu = await _menuRepository.GetByIdAsync(input.Id);
        if (menu == null)
        {
            throw new LeanUserFriendlyException($"菜单[{input.Id}]不存在");
        }

        menu.OrderNum = input.OrderNum;
        menu.ParentId = input.ParentId;
        return await _menuRepository.UpdateAsync(menu) > 0;
    }

    /// <summary>
    /// 导出菜单数据
    /// </summary>
    /// <param name="query">查询条件</param>
    /// <returns>导出数据</returns>
    public async Task<byte[]> ExportAsync(LeanMenuQueryDto query)
    {
        var list = await _menuRepository.GetListAsync(m =>
            (string.IsNullOrEmpty(query.MenuName) || m.MenuName.Contains(query.MenuName)) &&
            (string.IsNullOrEmpty(query.MenuCode) || m.MenuCode.Contains(query.MenuCode)) &&
            (!query.MenuType.HasValue || m.MenuType == query.MenuType.Value) &&
            (string.IsNullOrEmpty(query.PermissionCode) || m.PermissionCode == query.PermissionCode) &&
            (!query.Status.HasValue || m.Status == query.Status.Value));

        var exportDtos = list.Select(m => new LeanMenuExportDto
        {
            MenuName = m.MenuName,
            MenuCode = m.MenuCode,
            ParentName = m.ParentId == 0 ? "无" : list.FirstOrDefault(p => p.Id == m.ParentId)?.MenuName ?? "无",
            Path = m.Path,
            Component = m.Component,
            Redirect = m.Redirect,
            MenuTypeName = m.MenuType.ToString(),
            Icon = m.Icon,
            PermissionCode = m.PermissionCode,
            OrderNum = m.OrderNum,
            VisibleName = m.Visible == 1 ? "显示" : "隐藏",
            IsCacheName = m.IsCache == 1 ? "是" : "否",
            IsFrameName = m.IsFrame == 1 ? "是" : "否",
            StatusName = m.Status.ToString(),
            Remark = m.Remark,
            TransKey = m.TransKey,
            CreateTime = m.CreateTime
        }).ToList();

        return await LeanExcelHelper.ExportAsync(exportDtos);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <returns>导入模板</returns>
    public async Task<byte[]> GetImportTemplateAsync()
    {
        return await LeanExcelHelper.GetImportTemplateAsync<LeanMenuImportTemplateDto>();
    }

    /// <summary>
    /// 导入菜单数据
    /// </summary>
    /// <param name="fileBytes">导入文件</param>
    /// <returns>导入结果</returns>
    public async Task<(List<LeanMenuDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes)
    {
        using var stream = new MemoryStream(fileBytes);
        var result = await LeanExcelHelper.ImportAsync<LeanMenuImportDto>(stream);
        if (result.SuccessItems.Count > 0)
        {
            var menus = result.SuccessItems.Adapt<List<LeanMenu>>();
            await _menuRepository.InsertRangeAsync(menus);
        }
        return (result.SuccessItems.Adapt<List<LeanMenuDto>>(), result.ErrorMessages);
    }

    /// <summary>
    /// 构建树形结构
    /// </summary>
    /// <param name="list">菜单列表</param>
    /// <param name="parentId">父级ID</param>
    /// <returns>树形结构</returns>
    private List<LeanMenuDto> BuildTree(List<LeanMenuDto> list, long parentId = 0)
    {
        return list.Where(m => m.ParentId == parentId)
            .Select(m =>
            {
                m.Children = BuildTree(list, m.Id);
                return m;
            })
            .OrderBy(m => m.OrderNum)
            .ToList();
    }

    /// <summary>
    /// 获取父级菜单名称
    /// </summary>
    /// <param name="parentId">父级ID</param>
    /// <param name="list">菜单列表</param>
    /// <returns>父级菜单名称</returns>
    private string GetParentMenuName(long parentId, List<LeanMenu> list)
    {
        return list.FirstOrDefault(m => m.Id == parentId)?.MenuName ?? string.Empty;
    }
} 
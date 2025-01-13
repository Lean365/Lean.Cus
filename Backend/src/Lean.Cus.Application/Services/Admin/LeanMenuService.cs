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
/// 菜单服务实现
/// </summary>
public class LeanMenuService : ILeanMenuService
{
    private readonly ILeanRepository<LeanMenu> _menuRepository;

    /// <summary>
    /// 初始化菜单服务
    /// </summary>
    public LeanMenuService(ILeanRepository<LeanMenu> menuRepository)
    {
        _menuRepository = menuRepository;
    }

    /// <summary>
    /// 新增菜单
    /// </summary>
    public async Task<LeanMenuDto> CreateAsync(LeanMenuDto dto)
    {
        var menu = dto.Adapt<LeanMenu>();
        await _menuRepository.InsertAsync(menu);
        return menu.Adapt<LeanMenuDto>();
    }

    /// <summary>
    /// 更新菜单
    /// </summary>
    public async Task<bool> UpdateAsync(LeanMenuDto dto)
    {
        var menu = dto.Adapt<LeanMenu>();
        return await _menuRepository.UpdateAsync(menu) > 0;
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _menuRepository.DeleteAsync(m => m.Id == id) > 0;
    }

    /// <summary>
    /// 获取菜单
    /// </summary>
    public async Task<LeanMenuDto> GetAsync(long id)
    {
        var menu = await _menuRepository.GetAsync(m => m.Id == id);
        return menu.Adapt<LeanMenuDto>();
    }

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    public async Task<List<LeanMenuDto>> GetListAsync(LeanMenuQueryDto query)
    {
        var menus = await _menuRepository.GetListAsync(m =>
            (string.IsNullOrEmpty(query.MenuName) || m.MenuName.Contains(query.MenuName)) &&
            (string.IsNullOrEmpty(query.MenuCode) || m.MenuCode.Contains(query.MenuCode)) &&
            (!query.MenuType.HasValue || m.MenuType == query.MenuType.Value) &&
            (string.IsNullOrEmpty(query.PermissionCode) || m.PermissionCode.Contains(query.PermissionCode)) &&
            (!query.Status.HasValue || m.Status == query.Status.Value));
        return menus.Adapt<List<LeanMenuDto>>();
    }

    /// <summary>
    /// 分页查询菜单
    /// </summary>
    public async Task<PagedResults<LeanMenuDto>> GetPagedListAsync(LeanMenuQueryDto query)
    {
        RefAsync<int> total = 0;
        var list = await _menuRepository.GetPageListAsync(m =>
            (string.IsNullOrEmpty(query.MenuName) || m.MenuName.Contains(query.MenuName)) &&
            (string.IsNullOrEmpty(query.MenuCode) || m.MenuCode.Contains(query.MenuCode)) &&
            (!query.MenuType.HasValue || m.MenuType == query.MenuType.Value) &&
            (string.IsNullOrEmpty(query.PermissionCode) || m.PermissionCode.Contains(query.PermissionCode)) &&
            (!query.Status.HasValue || m.Status == query.Status.Value) &&
            (!query.CreatedTimeStart.HasValue || m.CreateTime >= query.CreatedTimeStart.Value) &&
            (!query.CreatedTimeEnd.HasValue || m.CreateTime <= query.CreatedTimeEnd.Value),
            query.PageIndex,
            query.PageSize,
            total);

        var dtos = list.Adapt<List<LeanMenuDto>>();
        return new PagedResults<LeanMenuDto>
        {
            Items = dtos,
            Total = total,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 导入菜单数据
    /// </summary>
    public async Task<(List<LeanMenuDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes)
    {
        using var stream = new MemoryStream(fileBytes);
        var result = await LeanExcelHelper.ImportAsync<LeanMenu>(stream);
        if (result.SuccessItems.Count > 0)
        {
            await _menuRepository.InsertRangeAsync(result.SuccessItems);
        }
        return (result.SuccessItems.Adapt<List<LeanMenuDto>>(), result.ErrorMessages);
    }

    /// <summary>
    /// 导出菜单数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanMenuQueryDto query)
    {
        var list = await _menuRepository.GetListAsync(m =>
            (string.IsNullOrEmpty(query.MenuName) || m.MenuName.Contains(query.MenuName)) &&
            (string.IsNullOrEmpty(query.MenuCode) || m.MenuCode.Contains(query.MenuCode)) &&
            (!query.MenuType.HasValue || m.MenuType == query.MenuType.Value) &&
            (string.IsNullOrEmpty(query.PermissionCode) || m.PermissionCode.Contains(query.PermissionCode)) &&
            (!query.Status.HasValue || m.Status == query.Status.Value));
        return await LeanExcelHelper.ExportAsync(list);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    public async Task<byte[]> GetImportTemplateAsync()
    {
        return await LeanExcelHelper.GetImportTemplateAsync<LeanMenu>();
    }

    /// <summary>
    /// 获取用户菜单树
    /// </summary>
    public async Task<List<LeanMenuDto>> GetUserMenuTreeAsync(long userId)
    {
        // 需要实现用户-角色-菜单关联查询
        var menus = await _menuRepository.GetListAsync(m => m.Id > 0);
        var dtos = menus.Adapt<List<LeanMenuDto>>();
        return BuildMenuTree(dtos);
    }

    /// <summary>
    /// 获取角色菜单树
    /// </summary>
    public async Task<List<LeanMenuDto>> GetRoleMenuTreeAsync(long roleId)
    {
        // 需要实现角色-菜单关联查询
        var menus = await _menuRepository.GetListAsync(m => m.Id > 0);
        var dtos = menus.Adapt<List<LeanMenuDto>>();
        return BuildMenuTree(dtos);
    }

    /// <summary>
    /// 获取菜单树
    /// </summary>
    public async Task<List<LeanMenuDto>> GetMenuTreeAsync()
    {
        var menus = await _menuRepository.GetListAsync();
        var dtos = menus.Adapt<List<LeanMenuDto>>();
        return BuildMenuTree(dtos);
    }

    private List<LeanMenuDto> BuildMenuTree(List<LeanMenuDto> menus)
    {
        var rootMenus = menus.Where(m => !m.ParentId.HasValue || m.ParentId == 0).ToList();
        foreach (var menu in rootMenus)
        {
            BuildMenuTreeRecursive(menu, menus);
        }
        return rootMenus;
    }

    private void BuildMenuTreeRecursive(LeanMenuDto parent, List<LeanMenuDto> allMenus)
    {
        parent.Children = allMenus.Where(m => m.ParentId == parent.Id).ToList();
        foreach (var child in parent.Children)
        {
            BuildMenuTreeRecursive(child, allMenus);
        }
    }

    /// <summary>
    /// 更新菜单状态
    /// </summary>
    public async Task<bool> UpdateStatusAsync(LeanMenuStatusUpdateDto input)
    {
        var menu = await _menuRepository.GetByIdAsync(input.Id);
        if (menu == null)
            return false;

        menu.Status = input.Status;
        return await _menuRepository.UpdateAsync(menu) > 0;
    }

    /// <summary>
    /// 更新菜单排序
    /// </summary>
    public async Task<bool> UpdateSortAsync(LeanMenuSortDto input)
    {
        var menu = await _menuRepository.GetByIdAsync(input.Id);
        if (menu == null)
            return false;

        menu.Sort = input.Sort;
        if (menu.ParentId != input.ParentId)
        {
            menu.ParentId = input.ParentId;
        }
        return await _menuRepository.UpdateAsync(menu) > 0;
    }
} 
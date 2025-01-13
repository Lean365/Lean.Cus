using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.Application.Interfaces.Admin;

/// <summary>
/// 菜单服务接口
/// </summary>
public interface ILeanMenuService
{
    /// <summary>
    /// 新增菜单
    /// </summary>
    Task<LeanMenuDto> CreateAsync(LeanMenuDto dto);

    /// <summary>
    /// 更新菜单
    /// </summary>
    Task<bool> UpdateAsync(LeanMenuDto dto);

    /// <summary>
    /// 删除菜单
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 获取菜单
    /// </summary>
    Task<LeanMenuDto> GetAsync(long id);

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    Task<List<LeanMenuDto>> GetListAsync(LeanMenuQueryDto query);

    /// <summary>
    /// 分页查询菜单
    /// </summary>
    Task<PagedResults<LeanMenuDto>> GetPagedListAsync(LeanMenuQueryDto query);

    /// <summary>
    /// 导入菜单数据
    /// </summary>
    Task<(List<LeanMenuDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes);

    /// <summary>
    /// 导出菜单数据
    /// </summary>
    Task<byte[]> ExportAsync(LeanMenuQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<byte[]> GetImportTemplateAsync();

    /// <summary>
    /// 获取用户菜单树
    /// </summary>
    Task<List<LeanMenuDto>> GetUserMenuTreeAsync(long userId);

    /// <summary>
    /// 获取角色菜单树
    /// </summary>
    Task<List<LeanMenuDto>> GetRoleMenuTreeAsync(long roleId);

    /// <summary>
    /// 获取菜单树
    /// </summary>
    Task<List<LeanMenuDto>> GetMenuTreeAsync();

    /// <summary>
    /// 更新菜单状态
    /// </summary>
    Task<bool> UpdateStatusAsync(LeanMenuStatusUpdateDto input);

    /// <summary>
    /// 更新菜单排序
    /// </summary>
    Task<bool> UpdateSortAsync(LeanMenuSortDto input);
}
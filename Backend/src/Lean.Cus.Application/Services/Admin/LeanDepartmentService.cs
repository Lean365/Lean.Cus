using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Common.Excel;
using Lean.Cus.Common.Paging;
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.IRepositories;
using Mapster;
using SqlSugar;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 部门服务实现
/// </summary>
public class LeanDepartmentService : ILeanDepartmentService
{
    private readonly ILeanRepository<LeanDepartment> _departmentRepository;

    /// <summary>
    /// 初始化部门服务
    /// </summary>
    public LeanDepartmentService(ILeanRepository<LeanDepartment> departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    /// <summary>
    /// 新增部门
    /// </summary>
    public async Task<LeanDepartmentDto> CreateAsync(LeanDepartmentDto dto)
    {
        var department = dto.Adapt<LeanDepartment>();
        await _departmentRepository.InsertAsync(department);
        return department.Adapt<LeanDepartmentDto>();
    }

    /// <summary>
    /// 更新部门
    /// </summary>
    public async Task<bool> UpdateAsync(LeanDepartmentDto dto)
    {
        var department = dto.Adapt<LeanDepartment>();
        return await _departmentRepository.UpdateAsync(department) > 0;
    }

    /// <summary>
    /// 删除部门
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _departmentRepository.DeleteAsync(d => d.Id == id) > 0;
    }

    /// <summary>
    /// 获取部门
    /// </summary>
    public async Task<LeanDepartmentDto> GetAsync(long id)
    {
        var department = await _departmentRepository.GetAsync(d => d.Id == id);
        return department.Adapt<LeanDepartmentDto>();
    }

    /// <summary>
    /// 获取部门列表
    /// </summary>
    public async Task<List<LeanDepartmentDto>> GetListAsync(LeanDepartmentQueryDto query)
    {
        var departments = await _departmentRepository.GetListAsync(d =>
            (string.IsNullOrEmpty(query.DepartmentName) || d.DepartmentName.Contains(query.DepartmentName)) &&
            (string.IsNullOrEmpty(query.DepartmentCode) || d.DepartmentCode.Contains(query.DepartmentCode)) &&
            (!query.ParentId.HasValue || d.ParentId == query.ParentId.Value) &&
            (!query.Status.HasValue || d.Status == query.Status.Value));
        return departments.Adapt<List<LeanDepartmentDto>>();
    }

    /// <summary>
    /// 分页查询部门
    /// </summary>
    public async Task<PagedResults<LeanDepartmentDto>> GetPagedListAsync(LeanDepartmentQueryDto query)
    {
        RefAsync<int> total = 0;
        var list = await _departmentRepository.GetPageListAsync(d =>
            (string.IsNullOrEmpty(query.DepartmentName) || d.DepartmentName.Contains(query.DepartmentName)) &&
            (string.IsNullOrEmpty(query.DepartmentCode) || d.DepartmentCode.Contains(query.DepartmentCode)) &&
            (!query.ParentId.HasValue || d.ParentId == query.ParentId.Value) &&
            (!query.Status.HasValue || d.Status == query.Status.Value) &&
            (!query.CreatedTimeStart.HasValue || d.CreateTime >= query.CreatedTimeStart.Value) &&
            (!query.CreatedTimeEnd.HasValue || d.CreateTime <= query.CreatedTimeEnd.Value),
            query.PageIndex,
            query.PageSize,
            total);

        var dtos = list.Adapt<List<LeanDepartmentDto>>();
        return new PagedResults<LeanDepartmentDto>
        {
            Items = dtos,
            Total = total,
            PageIndex = query.PageIndex,
            PageSize = query.PageSize
        };
    }

    /// <summary>
    /// 导入部门数据
    /// </summary>
    public async Task<(List<LeanDepartmentDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes)
    {
        using var stream = new MemoryStream(fileBytes);
        var result = await LeanExcelHelper.ImportAsync<LeanDepartment>(stream);
        if (result.SuccessItems.Count > 0)
        {
            await _departmentRepository.InsertRangeAsync(result.SuccessItems);
        }
        return (result.SuccessItems.Adapt<List<LeanDepartmentDto>>(), result.ErrorMessages);
    }

    /// <summary>
    /// 导出部门数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanDepartmentQueryDto query)
    {
        var list = await _departmentRepository.GetListAsync(d =>
            (string.IsNullOrEmpty(query.DepartmentName) || d.DepartmentName.Contains(query.DepartmentName)) &&
            (string.IsNullOrEmpty(query.DepartmentCode) || d.DepartmentCode.Contains(query.DepartmentCode)) &&
            (!query.ParentId.HasValue || d.ParentId == query.ParentId.Value) &&
            (!query.Status.HasValue || d.Status == query.Status.Value));
        return await LeanExcelHelper.ExportAsync(list);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    public async Task<byte[]> GetImportTemplateAsync()
    {
        return await LeanExcelHelper.GetImportTemplateAsync<LeanDepartment>();
    }

    /// <summary>
    /// 获取用户部门列表
    /// </summary>
    public async Task<List<LeanDepartmentDto>> GetUserDepartmentsAsync(long userId)
    {
        // 需要实现用户-部门关联查询
        var departments = await _departmentRepository.GetListAsync(d => d.Id > 0);
        return departments.Adapt<List<LeanDepartmentDto>>();
    }

    /// <summary>
    /// 获取角色部门列表
    /// </summary>
    public async Task<List<LeanDepartmentDto>> GetRoleDepartmentsAsync(long roleId)
    {
        // 需要实现角色-部门关联查询
        var departments = await _departmentRepository.GetListAsync(d => d.Id > 0);
        return departments.Adapt<List<LeanDepartmentDto>>();
    }

    /// <summary>
    /// 获取部门树
    /// </summary>
    public async Task<List<LeanDepartmentDto>> GetDepartmentTreeAsync()
    {
        var departments = await _departmentRepository.GetListAsync();
        var dtos = departments.Adapt<List<LeanDepartmentDto>>();
        return BuildDepartmentTree(dtos);
    }

    /// <summary>
    /// 更新部门状态
    /// </summary>
    public async Task<bool> UpdateStatusAsync(LeanDepartmentStatusUpdateDto input)
    {
        var department = await _departmentRepository.GetByIdAsync(input.Id);
        if (department == null)
            return false;

        department.Status = input.Status;
        return await _departmentRepository.UpdateAsync(department) > 0;
    }

    private List<LeanDepartmentDto> BuildDepartmentTree(List<LeanDepartmentDto> departments)
    {
        var rootDepartments = departments.Where(d => !d.ParentId.HasValue || d.ParentId == 0).ToList();
        foreach (var department in rootDepartments)
        {
            BuildDepartmentTree(department, departments);
        }
        return rootDepartments;
    }

    private void BuildDepartmentTree(LeanDepartmentDto parent, List<LeanDepartmentDto> allDepartments)
    {
        parent.Children = allDepartments.Where(d => d.ParentId == parent.Id).ToList();
        foreach (var child in parent.Children)
        {
            BuildDepartmentTree(child, allDepartments);
        }
    }
}
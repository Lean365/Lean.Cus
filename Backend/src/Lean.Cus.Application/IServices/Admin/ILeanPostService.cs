using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Services;

namespace Lean.Cus.Application.IServices.Admin;

/// <summary>
/// 岗位服务接口
/// </summary>
public interface ILeanPostService : ILeanService<LeanPostDto, LeanPostQueryDto, LeanPostCreateDto, LeanPostUpdateDto>
{
    /// <summary>
    /// 导入岗位数据
    /// </summary>
    /// <param name="importDtos">导入数据</param>
    /// <returns>导入结果</returns>
    Task<(int success, int fail)> ImportAsync(List<LeanPostImportDto> importDtos);

    /// <summary>
    /// 导出岗位数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>导出数据</returns>
    Task<List<LeanPostExportDto>> ExportAsync(LeanPostQueryDto queryDto);
} 
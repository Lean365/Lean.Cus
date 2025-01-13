using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Application.Interfaces.Admin;

/// <summary>
/// 职位服务接口
/// </summary>
public interface ILeanPositionService
{
    /// <summary>
    /// 新增职位
    /// </summary>
    Task<LeanPositionDto> CreateAsync(LeanPositionDto dto);

    /// <summary>
    /// 更新职位
    /// </summary>
    Task<bool> UpdateAsync(LeanPositionDto dto);

    /// <summary>
    /// 删除职位
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 获取职位
    /// </summary>
    Task<LeanPositionDto> GetAsync(long id);

    /// <summary>
    /// 获取职位列表
    /// </summary>
    Task<List<LeanPositionDto>> GetListAsync(LeanPositionQueryDto query);

    /// <summary>
    /// 分页查询职位
    /// </summary>
    Task<PagedResults<LeanPositionDto>> GetPagedListAsync(LeanPositionQueryDto query);

    /// <summary>
    /// 导入职位数据
    /// </summary>
    Task<(List<LeanPositionDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes);

    /// <summary>
    /// 导出职位数据
    /// </summary>
    Task<byte[]> ExportAsync(LeanPositionQueryDto query);

    /// <summary>
    /// 获取导入模板
    /// </summary>
    Task<byte[]> GetImportTemplateAsync();

    /// <summary>
    /// 获取用户职位列表
    /// </summary>
    Task<List<LeanPositionDto>> GetUserPositionsAsync(long userId);

    /// <summary>
    /// 更新职位状态
    /// </summary>
    Task<bool> UpdateStatusAsync(LeanPositionStatusUpdateDto input);
} 
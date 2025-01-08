using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Common.Services;

namespace Lean.Cus.Application.IServices.Admin;

/// <summary>
/// 用户岗位关联服务接口
/// </summary>
public interface ILeanUserPostService : ILeanService<LeanUserPostDto, LeanUserPostQueryDto, LeanUserPostCreateDto, LeanUserPostUpdateDto>
{
    /// <summary>
    /// 批量创建用户岗位关联
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <param name="postIds">岗位ID列表</param>
    /// <returns>是否成功</returns>
    Task<bool> BatchCreateAsync(long userId, List<long> postIds);

    /// <summary>
    /// 批量删除用户岗位关联
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>是否成功</returns>
    Task<bool> BatchDeleteByUserIdAsync(long userId);

    /// <summary>
    /// 获取用户的岗位ID列表
    /// </summary>
    /// <param name="userId">用户ID</param>
    /// <returns>岗位ID列表</returns>
    Task<List<long>> GetPostIdsByUserIdAsync(long userId);

    /// <summary>
    /// 获取岗位的用户ID列表
    /// </summary>
    /// <param name="postId">岗位ID</param>
    /// <returns>用户ID列表</returns>
    Task<List<long>> GetUserIdsByPostIdAsync(long postId);

    /// <summary>
    /// 导入用户岗位关联
    /// </summary>
    /// <param name="importDtos">导入数据</param>
    /// <returns>导入结果</returns>
    Task<(int success, int fail)> ImportAsync(List<LeanUserPostImportDto> importDtos);

    /// <summary>
    /// 导出用户岗位关联
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>导出数据</returns>
    Task<List<LeanUserPostExportDto>> ExportAsync(LeanUserPostQueryDto queryDto);
} 
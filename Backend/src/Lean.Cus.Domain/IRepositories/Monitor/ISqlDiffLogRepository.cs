using Lean.Cus.Domain.Entities.Monitor;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Domain.IRepositories.Monitor;

/// <summary>
/// SQL差异日志仓储接口
/// </summary>
public interface ISqlDiffLogRepository
{
    /// <summary>
    /// 获取分页列表
    /// </summary>
    Task<(List<LeanSqlDiffLog> list, int total)> GetPageListAsync(
        string databaseName,
        string tableName,
        string operationType,
        string operatorName,
        string ipAddress,
        DateTime? startTime,
        DateTime? endTime,
        int current,
        int size);

    /// <summary>
    /// 获取详情
    /// </summary>
    Task<LeanSqlDiffLog> GetByIdAsync(long id);

    /// <summary>
    /// 新增
    /// </summary>
    Task<bool> AddAsync(LeanSqlDiffLog entity);

    /// <summary>
    /// 删除
    /// </summary>
    Task<bool> DeleteAsync(long id);

    /// <summary>
    /// 批量删除
    /// </summary>
    Task<bool> BatchDeleteAsync(long[] ids);

    /// <summary>
    /// 清空日志
    /// </summary>
    Task<bool> ClearAllAsync();
} 
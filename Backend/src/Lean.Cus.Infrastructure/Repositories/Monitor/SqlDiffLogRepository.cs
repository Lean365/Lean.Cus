using Lean.Cus.Domain.Entities.Monitor;
using Lean.Cus.Domain.IRepositories.Monitor;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lean.Cus.Infrastructure.Repositories.Monitor;

/// <summary>
/// SQL差异日志仓储实现
/// </summary>
public class SqlDiffLogRepository : ISqlDiffLogRepository
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 构造函数
    /// </summary>
    public SqlDiffLogRepository(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 获取分页列表
    /// </summary>
    public async Task<(List<LeanSqlDiffLog> list, int total)> GetPageListAsync(
        string databaseName,
        string tableName,
        string operationType,
        string operatorName,
        string ipAddress,
        DateTime? startTime,
        DateTime? endTime,
        int current,
        int size)
    {
        var query = _db.Queryable<LeanSqlDiffLog>()
            .WhereIF(!string.IsNullOrEmpty(databaseName), x => x.DatabaseName.Contains(databaseName))
            .WhereIF(!string.IsNullOrEmpty(tableName), x => x.TableName.Contains(tableName))
            .WhereIF(!string.IsNullOrEmpty(operationType), x => x.OperationType == operationType)
            .WhereIF(!string.IsNullOrEmpty(operatorName), x => x.OperatorName.Contains(operatorName))
            .WhereIF(!string.IsNullOrEmpty(ipAddress), x => x.IpAddress.Contains(ipAddress))
            .WhereIF(startTime.HasValue, x => x.CreateTime >= startTime.Value)
            .WhereIF(endTime.HasValue, x => x.CreateTime <= endTime.Value)
            .OrderByDescending(x => x.CreateTime);

        RefAsync<int> total = 0;
        var list = await query.ToPageListAsync(current, size, total);
        return (list, total);
    }

    /// <summary>
    /// 获取详情
    /// </summary>
    public async Task<LeanSqlDiffLog> GetByIdAsync(long id)
    {
        return await _db.Queryable<LeanSqlDiffLog>().FirstAsync(x => x.Id == id);
    }

    /// <summary>
    /// 新增
    /// </summary>
    public async Task<bool> AddAsync(LeanSqlDiffLog entity)
    {
        entity.CreateTime = DateTime.Now;
        return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 删除
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _db.Deleteable<LeanSqlDiffLog>().Where(x => x.Id == id).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 批量删除
    /// </summary>
    public async Task<bool> BatchDeleteAsync(long[] ids)
    {
        return await _db.Deleteable<LeanSqlDiffLog>().Where(x => ids.Contains(x.Id)).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 清空日志
    /// </summary>
    public async Task<bool> ClearAllAsync()
    {
        return await _db.Deleteable<LeanSqlDiffLog>().ExecuteCommandAsync() > 0;
    }
} 
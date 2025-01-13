using Lean.Cus.Generator.Dtos.Import;
using Lean.Cus.Generator.Entities.Import;
using Lean.Cus.Generator.IServices.Import;
using Mapster;
using SqlSugar;

namespace Lean.Cus.Generator.Services.Import;

/// <summary>
/// 导入服务实现
/// </summary>
public class LeanImportService : ILeanImportService
{
    private readonly ISqlSugarClient _db;

    public LeanImportService(ISqlSugarClient db)
    {
        _db = db;
    }

    #region 表导入

    /// <summary>
    /// 获取表导入信息
    /// </summary>
    public async Task<LeanTableImportDto> GetTableAsync(long id)
    {
        var entity = await _db.Queryable<LeanTableImport>()
            .Where(x => x.Id == id)
            .FirstAsync();
        return entity?.Adapt<LeanTableImportDto>();
    }

    /// <summary>
    /// 获取表导入列表
    /// </summary>
    public async Task<List<LeanTableImportDto>> GetTableListAsync()
    {
        var list = await _db.Queryable<LeanTableImport>()
            .OrderBy(x => x.Id)
            .ToListAsync();
        return list?.Adapt<List<LeanTableImportDto>>();
    }

    /// <summary>
    /// 创建表导入
    /// </summary>
    public async Task<bool> CreateTableAsync(LeanTableImportDto dto)
    {
        var entity = dto.Adapt<LeanTableImport>();
        return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 更新表导入
    /// </summary>
    public async Task<bool> UpdateTableAsync(long id, LeanTableImportDto dto)
    {
        var entity = dto.Adapt<LeanTableImport>();
        entity.Id = id;
        return await _db.Updateable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 删除表导入
    /// </summary>
    public async Task<bool> DeleteTableAsync(long id)
    {
        return await _db.Deleteable<LeanTableImport>()
            .Where(x => x.Id == id)
            .ExecuteCommandAsync() > 0;
    }

    #endregion 表导入

    #region 字段导入

    /// <summary>
    /// 获取字段导入信息
    /// </summary>
    public async Task<LeanColumnImportDto> GetColumnAsync(long id)
    {
        var entity = await _db.Queryable<LeanColumnImport>()
            .Where(x => x.Id == id)
            .FirstAsync();
        return entity?.Adapt<LeanColumnImportDto>();
    }

    /// <summary>
    /// 获取字段导入列表
    /// </summary>
    public async Task<List<LeanColumnImportDto>> GetColumnListAsync(long tableId)
    {
        var list = await _db.Queryable<LeanColumnImport>()
            .Where(x => x.TableId == tableId)
            .OrderBy(x => x.OrderNum)
            .ToListAsync();
        return list?.Adapt<List<LeanColumnImportDto>>();
    }

    /// <summary>
    /// 创建字段导入
    /// </summary>
    public async Task<bool> CreateColumnAsync(long tableId, LeanColumnImportDto dto)
    {
        var entity = dto.Adapt<LeanColumnImport>();
        entity.TableId = tableId;
        return await _db.Insertable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 更新字段导入
    /// </summary>
    public async Task<bool> UpdateColumnAsync(long id, LeanColumnImportDto dto)
    {
        var entity = dto.Adapt<LeanColumnImport>();
        entity.Id = id;
        return await _db.Updateable(entity).ExecuteCommandAsync() > 0;
    }

    /// <summary>
    /// 删除字段导入
    /// </summary>
    public async Task<bool> DeleteColumnAsync(long id)
    {
        return await _db.Deleteable<LeanColumnImport>()
            .Where(x => x.Id == id)
            .ExecuteCommandAsync() > 0;
    }

    #endregion 字段导入

    #region 数据库导入

    /// <summary>
    /// 获取数据库表列表
    /// </summary>
    public async Task<List<LeanTableImportDto>> GetTablesAsync()
    {
        var tables = _db.DbMaintenance.GetTableInfoList();
        var result = tables.Select(x => new LeanTableImportDto
        {
            TableName = x.Name,
            TableDesc = x.Description ?? x.Name,
            EntityName = x.Name,
            Author = "System",
            Template = "default",
            FrontendTemplate = "default",
            NamespacePrefix = "Lean.Cus",
            ModuleName = "Generator",
            BusinessName = x.Name.ToLower(),
            FunctionName = x.Description ?? x.Name,
            ParentMenuId = 0,
            OrderField = "Id",
            OrderType = 0,
            UseSnowflakeId = 1,
            GenerateType = 0,
            IsMultiLevel = 0,
            IsGenerateRepository = 1,
            IsGenerateSqlLog = 1,
            PermissionPrefix = $"generator:{x.Name.ToLower()}",
            ButtonStyle = 0,
            Functions = "query,add,edit,delete,import,export",
            Columns = new List<LeanColumnImportDto>()
        }).ToList();
        return result;
    }

    /// <summary>
    /// 获取数据库表字段列表
    /// </summary>
    public async Task<List<LeanColumnImportDto>> GetColumnsAsync(string tableName)
    {
        var columns = _db.DbMaintenance.GetColumnInfosByTableName(tableName);
        var result = columns.Select(x => new LeanColumnImportDto
        {
            ColumnName = x.DbColumnName,
            ColumnDesc = x.ColumnDescription ?? x.DbColumnName,
            PhysicalType = x.DataType,
            CSharpType = x.DataType,
            CSharpProperty = x.DbColumnName,
            IsRequired = !x.IsNullable ? 1 : 0,
            IsList = 1,
            IsSort = 0,
            IsEdit = 1,
            IsExport = 1,
            IsQuery = 0,
            QueryType = "eq",
            DisplayType = "text",
            DictType = "",
            AutoFill = "",
            OrderNum = 0
        }).ToList();
        return result;
    }

    /// <summary>
    /// 导入到代码生成器
    /// </summary>
    public async Task<bool> ImportToGeneratorAsync(List<string> tableNames)
    {
        foreach (var tableName in tableNames)
        {
            // 获取表信息
            var allTables = _db.DbMaintenance.GetTableInfoList();
            var tableInfo = allTables.FirstOrDefault(x => x.Name == tableName);
            if (tableInfo == null) continue;

            var table = tableInfo;
            var tableImport = new LeanTableImportDto
            {
                TableName = table.Name,
                TableDesc = table.Description ?? table.Name,
                EntityName = table.Name,
                Author = "System",
                Template = "default",
                FrontendTemplate = "default",
                NamespacePrefix = "Lean.Cus",
                ModuleName = "Generator",
                BusinessName = table.Name.ToLower(),
                FunctionName = table.Description ?? table.Name,
                ParentMenuId = 0,
                OrderField = "Id",
                OrderType = 0,
                UseSnowflakeId = 1,
                GenerateType = 0,
                IsMultiLevel = 0,
                IsGenerateRepository = 1,
                IsGenerateSqlLog = 1,
                PermissionPrefix = $"generator:{table.Name.ToLower()}",
                ButtonStyle = 0,
                Functions = "query,add,edit,delete,import,export",
                Columns = new List<LeanColumnImportDto>()
            };

            // 保存表信息
            var success = await CreateTableAsync(tableImport);
            if (!success) continue;

            // 获取新创建的表记录
            var savedTable = await _db.Queryable<LeanTableImport>()
                .Where(x => x.TableName == tableImport.TableName)
                .FirstAsync();
            if (savedTable == null) continue;

            // 获取字段信息
            var columns = await GetColumnsAsync(tableName);
            foreach (var column in columns)
            {
                await CreateColumnAsync(savedTable.Id, column);
            }
        }

        return true;
    }

    #endregion 数据库导入
}
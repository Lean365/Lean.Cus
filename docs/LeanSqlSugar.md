# SqlSugar ORM 开发规范

本文档规定了使用 SqlSugar ORM 进行数据库开发时应遵循的规范和最佳实践。

## 数据库连接配置说明

### 支持的数据库类型及连接字符串示例

#### 1. MySQL (DbType.MySql)
```csharp
// MySQL 连接字符串示例
string connectionString = "Server=localhost;Database=lean_db;Uid=root;Pwd=123456;CharSet=utf8mb4;";
```
说明：
- Server：MySQL服务器地址
- Database：数据库名称
- Uid：用户名
- Pwd：密码
- CharSet：字符集，建议使用utf8mb4以支持完整的Unicode字符

#### 2. SQL Server (DbType.SqlServer)
```csharp
// SQL Server 连接字符串示例
string connectionString = "Server=.;Database=lean_db;Uid=sa;Pwd=123456;TrustServerCertificate=true;";
```
说明：
- Server：SQL Server实例名，"."表示本地默认实例
- Database：数据库名称
- Uid：用户名
- Pwd：密码
- TrustServerCertificate：是否信任服务器证书

#### 3. SQLite (DbType.Sqlite)
```csharp
// SQLite 连接字符串示例
string connectionString = "DataSource=lean.db";
```
说明：
- DataSource：数据库文件路径，可以是相对路径或绝对路径

#### 4. Oracle (DbType.Oracle)
```csharp
// Oracle 连接字符串示例
string connectionString = "Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521)))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=system;Password=123456;";
```
说明：
- HOST：Oracle服务器地址
- PORT：监听端口
- SERVICE_NAME：服务名
- User Id：用户名
- Password：密码

#### 5. PostgreSQL (DbType.PostgreSQL)
```csharp
// PostgreSQL 连接字符串示例
string connectionString = "Host=localhost;Port=5432;Database=lean_db;Username=postgres;Password=123456;";
```
说明：
- Host：PostgreSQL服务器地址
- Port：端口号
- Database：数据库名称
- Username：用户名
- Password：密码

#### 6. 达梦数据库 (DbType.Dm)
```csharp
// 达梦数据库连接字符串示例
string connectionString = "Server=localhost:5236;User Id=SYSDBA;PWD=SYSDBA;";
```
说明：
- Server：服务器地址和端口
- User Id：用户名
- PWD：密码

#### 7. 人大金仓数据库 (DbType.Kdbndp)
```csharp
// 人大金仓数据库连接字符串示例
string connectionString = "Server=localhost;Port=54321;Database=lean_db;User Id=system;Password=123456;";
```
说明：
- Server：服务器地址
- Port：端口号
- Database：数据库名称
- User Id：用户名
- Password：密码

#### 8. 神通数据库 (DbType.Oscar)
```csharp
// 神通数据库连接字符串示例
string connectionString = "Server=localhost;Port=2003;Database=lean_db;User Id=system;Password=123456;";
```
说明：
- Server：服务器地址
- Port：端口号
- Database：数据库名称
- User Id：用户名
- Password：密码

#### 9. MySqlConnector (DbType.MySqlConnector)
```csharp
// MySqlConnector 连接字符串示例
string connectionString = "Server=localhost;Database=lean_db;User=root;Password=123456;CharSet=utf8mb4;";
```
说明：
- Server：服务器地址
- Database：数据库名称
- User：用户名
- Password：密码
- CharSet：字符集

#### 10. Access (DbType.Access)
```csharp
// Access 连接字符串示例
string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=lean.accdb;";
```
说明：
- Provider：Access数据库提供程序
- Data Source：数据库文件路径

#### 11. OpenGauss (DbType.OpenGauss)
```csharp
// OpenGauss 连接字符串示例
string connectionString = "Host=localhost;Port=5432;Database=lean_db;Username=gaussdb;Password=123456;";
```
说明：
- Host：服务器地址
- Port：端口号
- Database：数据库名称
- Username：用户名
- Password：密码

#### 12. QuestDB (DbType.QuestDB)
```csharp
// QuestDB 连接字符串示例
string connectionString = "Host=localhost;Port=8812;Database=lean_db;Username=admin;Password=quest;";
```
说明：
- Host：服务器地址
- Port：端口号
- Database：数据库名称
- Username：用户名
- Password：密码

#### 13. 瀚高数据库 (DbType.HG)
```csharp
// 瀚高数据库连接字符串示例
string connectionString = "Server=localhost;Port=5866;Database=lean_db;User Id=system;Password=123456;";
```
说明：
- Server：服务器地址
- Port：端口号
- Database：数据库名称
- User Id：用户名
- Password：密码

#### 14. ClickHouse (DbType.ClickHouse)
```csharp
// ClickHouse 连接字符串示例
string connectionString = "Host=localhost;Port=8123;Database=lean_db;Username=default;Password=;";
```
说明：
- Host：服务器地址
- Port：HTTP端口
- Database：数据库名称
- Username：用户名
- Password：密码

#### 15. 南大通用数据库 (DbType.GBase)
```csharp
// 南大通用数据库连接字符串示例
string connectionString = "Server=localhost;Port=5258;Database=lean_db;User Id=gbase;Password=123456;";
```
说明：
- Server：服务器地址
- Port：端口号
- Database：数据库名称
- User Id：用户名
- Password：密码

#### 16. ODBC通用连接 (DbType.Odbc)
```csharp
// ODBC通用连接字符串示例
string connectionString = "Driver={SQL Server};Server=localhost;Database=lean_db;Uid=sa;Pwd=123456;";
```
说明：
- Driver：ODBC驱动名称
- Server：服务器地址
- Database：数据库名称
- Uid：用户名
- Pwd：密码

#### 17. OceanBase Oracle模式 (DbType.OceanBaseForOracle)
```csharp
// OceanBase Oracle模式连接字符串示例
string connectionString = "Data Source=localhost:2883/lean_db;User Id=root;Password=123456;";
```
说明：
- Data Source：服务器地址:端口/数据库名
- User Id：用户名
- Password：密码

#### 18. TDengine (DbType.TDengine)
```csharp
// TDengine 连接字符串示例
string connectionString = "Server=localhost;Port=6041;Database=lean_db;Username=root;Password=taosdata;";
```
说明：
- Server：服务器地址
- Port：REST端口
- Database：数据库名称
- Username：用户名
- Password：密码

#### 19. GaussDB (DbType.GaussDB)
```csharp
// GaussDB 连接字符串示例
string connectionString = "Server=localhost;Port=25308;Database=lean_db;User Id=gaussdb;Password=123456;";
```
说明：
- Server：服务器地址
- Port：端口号
- Database：数据库名称
- User Id：用户名
- Password：密码

#### 20. OceanBase (DbType.OceanBase)
```csharp
// OceanBase 连接字符串示例
string connectionString = "Server=localhost;Port=2881;Database=lean_db;User Id=root;Password=123456;";
```
说明：
- Server：服务器地址
- Port：端口号
- Database：数据库名称
- User Id：用户名
- Password：密码

#### 21. TiDB (DbType.Tidb)
```csharp
// TiDB 连接字符串示例
string connectionString = "Server=localhost;Port=4000;Database=lean_db;User=root;Password=123456;";
```
说明：
- Server：服务器地址
- Port：端口号
- Database：数据库名称
- User：用户名
- Password：密码

#### 22. Vastbase (DbType.Vastbase)
```csharp
// Vastbase 连接字符串示例
string connectionString = "Server=localhost;Port=5432;Database=lean_db;User Id=vastbase;Password=123456;";
```
说明：
- Server：服务器地址
- Port：端口号
- Database：数据库名称
- User Id：用户名
- Password：密码

#### 23. PolarDB (DbType.PolarDB)
```csharp
// PolarDB 连接字符串示例
string connectionString = "Server=localhost;Port=1521;Database=lean_db;User Id=root;Password=123456;";
```
说明：
- Server：服务器地址
- Port：端口号
- Database：数据库名称
- User Id：用户名
- Password：密码

#### 24. Doris (DbType.Doris)
```csharp
// Doris 连接字符串示例
string connectionString = "Server=localhost;Port=9030;Database=lean_db;User Id=root;Password=123456;";
```
说明：
- Server：服务器地址
- Port：端口号
- Database：数据库名称
- User Id：用户名
- Password：密码

### 连接配置最佳实践
1. 安全性考虑
   - 不要在代码中硬编码连接字符串
   - 使用配置文件或环境变量存储连接信息
   - 对敏感信息进行加密处理

2. 性能优化
   - 合理设置连接池大小
   - 及时释放数据库连接
   - 使用异步操作提高性能

3. 可维护性
   - 统一管理连接字符串
   - 使用配置转换器处理不同环境
   - 做好连接字符串版本控制

4. 错误处理
   - 实现连接重试机制
   - 记录连接错误日志
   - 提供友好的错误提示

## 基础配置

### 数据库连接配置
```csharp
/// <summary>
/// SqlSugar 配置类
/// </summary>
/// <remarks>
/// 用于配置数据库连接、实体映射等基础设置
/// </remarks>
public static class SqlSugarConfig 
{
    /// <summary>
    /// 获取 SqlSugar 实例
    /// </summary>
    /// <returns>返回配置好的 SqlSugarScope 实例</returns>
    public static SqlSugarScope GetInstance()
    {
        return new SqlSugarScope(new ConnectionConfig()
        {
            // 数据库类型
            DbType = DbType.SqlServer,
            // 连接字符串
            ConnectionString = "Server=.;Database=Lean.Cus;Uid=sa;Pwd=123456;",
            // 自动关闭连接
            IsAutoCloseConnection = true,
            // 实体特性初始化类型
            InitKeyType = InitKeyType.Attribute,
            // 外部服务配置
            ConfigureExternalServices = new ConfigureExternalServices
            {
                // 实体服务配置
                EntityService = (property, column) =>
                {
                    // 自动配置主键
                    if (column.IsPrimarykey && property.Name.ToLower() == "id")
                        column.IsPrimarykey = true;
                    // 配置可空性
                    column.IsNullable = property.GetCustomAttribute<SugarColumn>()?.IsNullable ?? true;
                }
            }
        });
    }
}
```

### 仓储基类实现
```csharp
/// <summary>
/// 通用仓储基类
/// </summary>
/// <typeparam name="T">实体类型</typeparam>
/// <remarks>
/// 提供基础的 CRUD 操作和分页查询功能
/// </remarks>
public class BaseRepository<T> : SimpleClient<T> where T : class, new()
{
    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="context">数据库上下文</param>
    public BaseRepository(ISqlSugarClient context) : base(context)
    {
        base.Context = context;
    }

    /// <summary>
    /// 获取符合条件的记录总数
    /// </summary>
    /// <param name="whereExpression">查询条件表达式</param>
    /// <returns>记录总数</returns>
    public virtual async Task<int> CountAsync(Expression<Func<T, bool>> whereExpression)
    {
        return await base.Context.Queryable<T>().CountAsync(whereExpression);
    }

    /// <summary>
    /// 分页查询
    /// </summary>
    /// <param name="whereExpression">查询条件表达式</param>
    /// <param name="pageIndex">页码，从1开始</param>
    /// <param name="pageSize">每页记录数</param>
    /// <param name="orderByFields">排序字段</param>
    /// <returns>分页结果</returns>
    public virtual async Task<PagedList<T>> GetPagedListAsync(Expression<Func<T, bool>> whereExpression, 
        int pageIndex = 1, int pageSize = 20, string orderByFields = null)
    {
        RefAsync<int> totalCount = 0;
        var list = await base.Context.Queryable<T>()
            .WhereIF(whereExpression != null, whereExpression)
            .OrderByIF(!string.IsNullOrEmpty(orderByFields), orderByFields)
            .ToPageListAsync(pageIndex, pageSize, totalCount);

        return new PagedList<T>(list, pageIndex, pageSize, totalCount);
    }
}
```

## 实体映射规范

### 实体特性配置
```csharp
/// <summary>
/// 用户实体类
/// </summary>
/// <remarks>
/// 用于存储用户基本信息
/// </remarks>
[SugarTable("lean_user", "用户表")]
[SugarIndex("idx_user_username", nameof(UserName), OrderByType.Asc, true)]
public class LeanUser : LeanEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    /// <remarks>
    /// 数据类型：nvarchar(50)
    /// 允许为空：否
    /// 说明：用户登录账号，不可重复
    /// </remarks>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", 
        Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    /// <remarks>
    /// 数据类型：varchar(100)
    /// 允许为空：否
    /// 说明：用户登录密码，存储加密后的值
    /// </remarks>
    [SugarColumn(ColumnName = "password", ColumnDescription = "密码", 
        Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string Password { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：int
    /// 允许为空：否
    /// 说明：用户状态，1-启用，0-禁用
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", 
        IsNullable = false, ColumnDataType = "int")]
    public int Status { get; set; }
}
```

### 导航属性配置
```csharp
/// <summary>
/// 用户实体类
/// </summary>
public class LeanUser : LeanEntity
{
    /// <summary>
    /// 用户角色关联
    /// </summary>
    /// <remarks>
    /// 一对多关系：一个用户可以拥有多个角色
    /// </remarks>
    [Navigate(NavigateType.OneToMany, nameof(LeanUserRole.UserId))]
    public List<LeanUserRole> UserRoles { get; set; }

    /// <summary>
    /// 用户信息关联
    /// </summary>
    /// <remarks>
    /// 一对一关系：一个用户对应一条用户信息
    /// </remarks>
    [Navigate(NavigateType.OneToOne, nameof(LeanUserInfo.UserId))]
    public LeanUserInfo UserInfo { get; set; }
}
```

## 查询规范

### 查询规范总则
1. 所有查询必须使用异步方法
2. 查询必须指定具体字段，避免使用 `Select *`
3. 查询条件统一使用 Lambda 表达式
4. 分页查询必须指定排序字段
5. 查询结果应使用强类型实体或 DTO
6. 合理使用导航属性和延迟加载
7. 需要考虑查询性能优化
8. 查询应遵循最小权限原则

### 基础查询规范
```csharp
/// <summary>
/// 基础查询规范示例
/// </summary>
public class BasicQuerySpecification
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 单条记录查询
    /// </summary>
    /// <remarks>
    /// 1. 优先使用 FirstAsync 而不是 SingleAsync
    /// 2. 查询单条记录时应使用主键或唯一索引
    /// 3. 指定具体的查询字段
    /// </remarks>
    public async Task<LeanUser> GetUserAsync(long id)
    {
        return await _db.Queryable<LeanUser>()
            .Select(u => new LeanUser 
            { 
                Id = u.Id,
                UserName = u.UserName,
                Status = u.Status,
                CreateTime = u.CreateTime
            })
            .Where(u => u.Id == id && !u.IsDeleted)
            .FirstAsync();
    }

    /// <summary>
    /// 列表查询
    /// </summary>
    /// <remarks>
    /// 1. 必须添加合理的查询条件
    /// 2. 必须指定排序规则
    /// 3. 建议限制返回数量
    /// </remarks>
    public async Task<List<LeanUser>> GetUserListAsync(string keyword)
    {
        return await _db.Queryable<LeanUser>()
            .WhereIF(!string.IsNullOrEmpty(keyword), 
                u => u.UserName.Contains(keyword))
            .Where(u => u.Status == 1 && !u.IsDeleted)
            .OrderBy(u => u.CreateTime, OrderByType.Desc)
            .Take(100)
            .ToListAsync();
    }

    /// <summary>
    /// 条件查询
    /// </summary>
    /// <remarks>
    /// 1. 使用 WhereIF 进行动态条件查询
    /// 2. 使用表达式目录树组合查询条件
    /// 3. 查询条件应考虑索引使用
    /// </remarks>
    public async Task<List<LeanUser>> GetUsersByConditionAsync(
        string keyword, int? status, DateTime? startTime, DateTime? endTime)
    {
        var query = _db.Queryable<LeanUser>()
            .Where(u => !u.IsDeleted);
            
        // 动态条件
        query = query.WhereIF(!string.IsNullOrEmpty(keyword), 
            u => u.UserName.Contains(keyword));
        query = query.WhereIF(status.HasValue, 
            u => u.Status == status.Value);
        query = query.WhereIF(startTime.HasValue, 
            u => u.CreateTime >= startTime.Value);
        query = query.WhereIF(endTime.HasValue, 
            u => u.CreateTime <= endTime.Value);

        // 排序和结果
        return await query
            .OrderBy(u => u.OrderNum)
            .ToListAsync();
    }
}
```

### 分页查询规范
```csharp
/// <summary>
/// 分页查询规范示例
/// </summary>
public class PagedQuerySpecification
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 标准分页查询
    /// </summary>
    /// <remarks>
    /// 1. 必须指定排序字段
    /// 2. 使用 ToPageListAsync 进行分页
    /// 3. 返回总记录数
    /// 4. 分页参数合法性验证
    /// </remarks>
    public async Task<PagedList<LeanUserDto>> GetPagedListAsync(
        string keyword, int pageIndex = 1, int pageSize = 20)
    {
        // 参数验证
        pageIndex = Math.Max(1, pageIndex);
        pageSize = Math.Max(1, Math.Min(100, pageSize));

        // 分页查询
        RefAsync<int> total = 0;
        var list = await _db.Queryable<LeanUser>()
            .WhereIF(!string.IsNullOrEmpty(keyword), 
                u => u.UserName.Contains(keyword))
            .Where(u => !u.IsDeleted)
            .OrderBy(u => u.OrderNum)
            .OrderBy(u => u.CreateTime, OrderByType.Desc)
            .Select(u => new LeanUserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Status = u.Status,
                CreateTime = u.CreateTime
            })
            .ToPageListAsync(pageIndex, pageSize, total);

        // 返回分页结果
        return new PagedList<LeanUserDto>(list, pageIndex, pageSize, total);
    }

    /// <summary>
    /// 分页查询带导航属性
    /// </summary>
    /// <remarks>
    /// 1. 使用 Includes 加载导航属性
    /// 2. 注意导航属性的性能影响
    /// 3. 合理使用 Select 优化查询字段
    /// </remarks>
    public async Task<PagedList<LeanUserDto>> GetPagedListWithNavigationAsync(
        string keyword, int pageIndex = 1, int pageSize = 20)
    {
        RefAsync<int> total = 0;
        var list = await _db.Queryable<LeanUser>()
            .Includes(u => u.UserRoles.Where(r => r.Status == 1))
            .Includes(u => u.UserInfo)
            .Where(u => !u.IsDeleted)
            .WhereIF(!string.IsNullOrEmpty(keyword), 
                u => u.UserName.Contains(keyword))
            .OrderBy(u => u.OrderNum)
            .Select((u) => new LeanUserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Status = u.Status,
                RoleNames = u.UserRoles.Select(r => r.RoleName).ToList(),
                RealName = u.UserInfo.RealName
            })
            .ToPageListAsync(pageIndex, pageSize, total);

        return new PagedList<LeanUserDto>(list, pageIndex, pageSize, total);
    }
}
```

### 联表查询规范
```csharp
/// <summary>
/// 联表查询规范示例
/// </summary>
public class JoinQuerySpecification
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 左连接查询
    /// </summary>
    /// <remarks>
    /// 1. 使用类型安全的 Lambda 表达式
    /// 2. 指定连接条件和查询条件
    /// 3. 使用强类型 DTO 返回结果
    /// 4. 考虑索引使用效率
    /// </remarks>
    public async Task<List<UserDetailDto>> GetUserDetailsAsync(string keyword)
    {
        return await _db.Queryable<LeanUser>()
            .LeftJoin<LeanUserInfo>((u, i) => u.Id == i.UserId)
            .LeftJoin<LeanDepartment>((u, i, d) => i.DeptId == d.Id)
            .Where((u, i, d) => !u.IsDeleted)
            .WhereIF(!string.IsNullOrEmpty(keyword), 
                (u, i, d) => u.UserName.Contains(keyword) || 
                            i.RealName.Contains(keyword))
            .OrderBy((u, i, d) => u.CreateTime, OrderByType.Desc)
            .Select((u, i, d) => new UserDetailDto
            {
                Id = u.Id,
                UserName = u.UserName,
                RealName = i.RealName,
                DeptName = d.DeptName,
                CreateTime = u.CreateTime
            })
            .ToListAsync();
    }

    /// <summary>
    /// 多表关联分页查询
    /// </summary>
    /// <remarks>
    /// 1. 合理使用索引提高查询效率
    /// 2. 控制连接表数量，一般不超过3个
    /// 3. 使用别名避免字段名冲突
    /// 4. 必须指定排序字段
    /// </remarks>
    public async Task<PagedList<UserRoleDto>> GetUserRolesPagedListAsync(
        string keyword, int pageIndex = 1, int pageSize = 20)
    {
        RefAsync<int> total = 0;
        var list = await _db.Queryable<LeanUser>()
            .LeftJoin<LeanUserRole>((u, r) => u.Id == r.UserId)
            .LeftJoin<LeanRole>((u, r, ro) => r.RoleId == ro.Id)
            .Where((u, r, ro) => !u.IsDeleted && !ro.IsDeleted)
            .WhereIF(!string.IsNullOrEmpty(keyword), 
                (u, r, ro) => u.UserName.Contains(keyword) || 
                             ro.RoleName.Contains(keyword))
            .OrderBy((u, r, ro) => u.OrderNum)
            .OrderBy((u, r, ro) => u.CreateTime, OrderByType.Desc)
            .Select((u, r, ro) => new UserRoleDto
            {
                UserId = u.Id,
                UserName = u.UserName,
                RoleId = ro.Id,
                RoleName = ro.RoleName,
                CreateTime = r.CreateTime
            })
            .ToPageListAsync(pageIndex, pageSize, total);

        return new PagedList<UserRoleDto>(list, pageIndex, pageSize, total);
    }
}
```

### 导航属性查询规范
```csharp
/// <summary>
/// 导航属性查询规范示例
/// </summary>
public class NavigationQuerySpecification
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 一对一导航查询
    /// </summary>
    /// <remarks>
    /// 1. 使用 Includes 加载一对一关系
    /// 2. 指定导航属性的查询字段
    /// 3. 注意延迟加载的性能影响
    /// </remarks>
    public async Task<LeanUser> GetUserWithInfoAsync(long userId)
    {
        return await _db.Queryable<LeanUser>()
            .Includes(u => u.UserInfo.Where(i => !i.IsDeleted))
            .Where(u => u.Id == userId && !u.IsDeleted)
            .FirstAsync();
    }

    /// <summary>
    /// 一对多导航查询
    /// </summary>
    /// <remarks>
    /// 1. 使用 Includes 加载一对多关系
    /// 2. 为导航属性添加过滤条件
    /// 3. 使用 Select 优化返回字段
    /// </remarks>
    public async Task<List<LeanUser>> GetUsersWithRolesAsync(string keyword)
    {
        return await _db.Queryable<LeanUser>()
            .Includes(u => u.UserRoles
                .Where(r => r.Status == 1)
                .OrderBy(r => r.CreateTime))
            .Where(u => !u.IsDeleted)
            .WhereIF(!string.IsNullOrEmpty(keyword), 
                u => u.UserName.Contains(keyword))
            .OrderBy(u => u.CreateTime, OrderByType.Desc)
            .ToListAsync();
    }

    /// <summary>
    /// 多级导航查询
    /// </summary>
    /// <remarks>
    /// 1. 使用多个 Includes 加载多级关系
    /// 2. 注意查询性能，避免过多级联
    /// 3. 合理使用 Select 优化查询
    /// </remarks>
    public async Task<List<LeanUser>> GetUsersWithNestedAsync(string keyword)
    {
        return await _db.Queryable<LeanUser>()
            .Includes(u => u.UserInfo)
            .Includes(u => u.UserRoles
                .Where(r => r.Status == 1))
            .Includes(u => u.UserRoles
                .Select(r => r.Role)
                .Where(r => !r.IsDeleted))
            .Where(u => !u.IsDeleted)
            .WhereIF(!string.IsNullOrEmpty(keyword), 
                u => u.UserName.Contains(keyword))
            .OrderBy(u => u.CreateTime, OrderByType.Desc)
            .ToListAsync();
    }
}
```

### 高级查询规范
```csharp
/// <summary>
/// 高级查询规范示例
/// </summary>
public class AdvancedQuerySpecification
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 子查询示例
    /// </summary>
    /// <remarks>
    /// 1. 使用 SubQuery 实现子查询
    /// 2. 注意子查询的性能影响
    /// 3. 可能的情况下优先使用 Join
    /// </remarks>
    public async Task<List<LeanUser>> GetUsersWithSubQueryAsync()
    {
        return await _db.Queryable<LeanUser>()
            .Where(u => !u.IsDeleted)
            .Where(u => SqlFunc.Subqueryable<LeanUserRole>()
                .Where(r => r.UserId == u.Id && r.Status == 1)
                .Count() > 0)
            .OrderBy(u => u.CreateTime, OrderByType.Desc)
            .ToListAsync();
    }

    /// <summary>
    /// 动态排序示例
    /// </summary>
    /// <remarks>
    /// 1. 使用 OrderByIF 实现动态排序
    /// 2. 验证排序字段的合法性
    /// 3. 提供默认排序规则
    /// </remarks>
    public async Task<List<LeanUser>> GetUsersWithDynamicOrderAsync(
        string orderField, OrderByType? orderType)
    {
        var query = _db.Queryable<LeanUser>()
            .Where(u => !u.IsDeleted);

        // 动态排序
        query = query
            .OrderByIF(!string.IsNullOrEmpty(orderField), orderField, 
                orderType ?? OrderByType.Asc)
            .OrderBy(u => u.CreateTime, OrderByType.Desc);

        return await query.ToListAsync();
    }

    /// <summary>
    /// 分组查询示例
    /// </summary>
    /// <remarks>
    /// 1. 使用 GroupBy 进行分组统计
    /// 2. 使用 Having 过滤分组结果
    /// 3. 返回聚合结果使用 DTO
    /// </remarks>
    public async Task<List<UserStatDto>> GetUserStatisticsAsync()
    {
        return await _db.Queryable<LeanUser>()
            .LeftJoin<LeanDepartment>((u, d) => u.DeptId == d.Id)
            .Where(u => !u.IsDeleted)
            .GroupBy((u, d) => new 
            { 
                u.DeptId, 
                DeptName = d.DeptName 
            })
            .Select((u, d) => new UserStatDto
            {
                DeptId = u.DeptId,
                DeptName = d.DeptName,
                UserCount = SqlFunc.AggregateCount(u.Id),
                LastUpdateTime = SqlFunc.AggregateMax(u.UpdateTime)
            })
            .Having(g => SqlFunc.AggregateCount(g.UserCount) > 0)
            .OrderBy(g => g.UserCount, OrderByType.Desc)
            .ToListAsync();
    }
}
```

### 查询性能优化规范
```csharp
/// <summary>
/// 查询性能优化规范示例
/// </summary>
public class QueryOptimizationSpecification
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// With(NoLock) 查询
    /// </summary>
    /// <remarks>
    /// 1. 使用 With(NoLock) 提高查询性能
    /// 2. 注意数据一致性要求
    /// 3. 适用于读多写少的场景
    /// </remarks>
    public async Task<List<LeanUser>> GetUsersWithNoLockAsync()
    {
        return await _db.Queryable<LeanUser>()
            .With(SqlWith.NoLock)
            .Where(u => !u.IsDeleted)
            .OrderBy(u => u.CreateTime, OrderByType.Desc)
            .ToListAsync();
    }

    /// <summary>
    /// 流式查询
    /// </summary>
    /// <remarks>
    /// 1. 使用流式查询处理大数据量
    /// 2. 避免一次性加载大量数据
    /// 3. 控制内存使用
    /// </remarks>
    public async Task ProcessLargeDataAsync(Action<List<LeanUser>> process)
    {
        await _db.Queryable<LeanUser>()
            .Where(u => !u.IsDeleted)
            .OrderBy(u => u.Id)
            .ForEachAsync(async (items) =>
            {
                process(items);
                await Task.CompletedTask;
            }, 1000);
    }

    /// <summary>
    /// 查询缓存
    /// </summary>
    /// <remarks>
    /// 1. 使用 WithCache 缓存查询结果
    /// 2. 设置合理的缓存时间
    /// 3. 注意缓存更新策略
    /// </remarks>
    public async Task<List<LeanUser>> GetUsersWithCacheAsync()
    {
        return await _db.Queryable<LeanUser>()
            .Where(u => !u.IsDeleted)
            .WithCache(60)  // 缓存60秒
            .OrderBy(u => u.CreateTime, OrderByType.Desc)
            .ToListAsync();
    }
}
```

## 新增规范

### 单条新增
```csharp
/// <summary>
/// 新增示例
/// </summary>
public class InsertExample
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 实体新增
    /// </summary>
    /// <remarks>
    /// 新增用户并返回包含自增ID的实体
    /// </remarks>
    public async Task<LeanUser> InsertUserAsync(LeanUser user)
    {
        return await _db.Insertable(user).ExecuteReturnEntityAsync();
    }

    /// <summary>
    /// 指定字段新增
    /// </summary>
    /// <remarks>
    /// 只新增用户名和密码字段
    /// </remarks>
    public async Task<int> InsertUserFieldsAsync(LeanUser user)
    {
        return await _db.Insertable(user)
            .InsertColumns(u => new { u.UserName, u.Password })
            .ExecuteCommandAsync();
    }
}
```

### 批量新增
```csharp
/// <summary>
/// 批量新增示例
/// </summary>
public class BatchInsertExample
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 批量新增
    /// </summary>
    /// <remarks>
    /// 批量新增用户列表
    /// </remarks>
    public async Task<int> BatchInsertUsersAsync(List<LeanUser> users)
    {
        return await _db.Insertable(users).ExecuteCommandAsync();
    }

    /// <summary>
    /// 大数据批量新增
    /// </summary>
    /// <remarks>
    /// 使用 Fastest 方法批量新增大量数据
    /// </remarks>
    public async Task BatchInsertLargeDataAsync(List<LeanUser> users)
    {
        await _db.Fastest<LeanUser>()
            .BulkCopyAsync(users);
    }
}
```

## 更新规范

### 单条更新
```csharp
/// <summary>
/// 更新示例
/// </summary>
public class UpdateExample
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 实体更新
    /// </summary>
    /// <remarks>
    /// 更新整个实体
    /// </remarks>
    public async Task<bool> UpdateUserAsync(LeanUser user)
    {
        return await _db.Updateable(user).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 指定字段更新
    /// </summary>
    /// <remarks>
    /// 只更新状态字段
    /// </remarks>
    public async Task<bool> UpdateUserStatusAsync(long id, int status)
    {
        return await _db.Updateable<LeanUser>()
            .SetColumns(u => new LeanUser { Status = status })
            .Where(u => u.Id == id)
            .ExecuteCommandHasChangeAsync();
    }
}
```

### 批量更新
```csharp
/// <summary>
/// 批量更新示例
/// </summary>
public class BatchUpdateExample
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 批量更新
    /// </summary>
    /// <remarks>
    /// 批量更新用户列表
    /// </remarks>
    public async Task<int> BatchUpdateUsersAsync(List<LeanUser> users)
    {
        return await _db.Updateable(users).ExecuteCommandAsync();
    }

    /// <summary>
    /// 大数据批量更新
    /// </summary>
    /// <remarks>
    /// 使用 Fastest 方法批量更新大量数据
    /// </remarks>
    public async Task BatchUpdateLargeDataAsync(List<LeanUser> users)
    {
        await _db.Fastest<LeanUser>()
            .BulkUpdateAsync(users);
    }
}
```

## 删除规范

### 单条删除
```csharp
/// <summary>
/// 删除示例
/// </summary>
public class DeleteExample
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 实体删除
    /// </summary>
    /// <remarks>
    /// 根据实体删除记录
    /// </remarks>
    public async Task<bool> DeleteUserAsync(LeanUser user)
    {
        return await _db.Deleteable(user).ExecuteCommandHasChangeAsync();
    }

    /// <summary>
    /// 条件删除
    /// </summary>
    /// <remarks>
    /// 根据ID删除记录
    /// </remarks>
    public async Task<bool> DeleteUserByIdAsync(long id)
    {
        return await _db.Deleteable<LeanUser>()
            .Where(u => u.Id == id)
            .ExecuteCommandHasChangeAsync();
    }
}
```

### 批量删除
```csharp
/// <summary>
/// 批量删除示例
/// </summary>
public class BatchDeleteExample
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 批量删除
    /// </summary>
    /// <remarks>
    /// 根据ID列表批量删除记录
    /// </remarks>
    public async Task<bool> BatchDeleteUsersAsync(List<long> ids)
    {
        return await _db.Deleteable<LeanUser>()
            .Where(u => ids.Contains(u.Id))
            .ExecuteCommandHasChangeAsync();
    }
}
```

### 软删除
```csharp
/// <summary>
/// 软删除示例
/// </summary>
public class SoftDeleteExample
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 软删除
    /// </summary>
    /// <remarks>
    /// 更新删除标记而不是物理删除记录
    /// </remarks>
    public async Task<bool> SoftDeleteUserAsync(long id, long userId)
    {
        return await _db.Updateable<LeanUser>()
            .SetColumns(u => new LeanUser 
            { 
                IsDeleted = 1,
                DeletedTime = DateTime.Now,
                DeletedBy = userId
            })
            .Where(u => u.Id == id)
            .ExecuteCommandHasChangeAsync();
    }
}
```

## 事务规范

### 普通事务
```csharp
/// <summary>
/// 事务示例
/// </summary>
public class TransactionExample
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 普通事务
    /// </summary>
    /// <remarks>
    /// 使用传统方式管理事务
    /// </remarks>
    public async Task<bool> TransactionDemo(LeanUser user, LeanUserInfo userInfo)
    {
        try
        {
            _db.BeginTran();

            await _db.Insertable(user).ExecuteCommandAsync();
            await _db.Insertable(userInfo).ExecuteCommandAsync();

            _db.CommitTran();
            return true;
        }
        catch (Exception)
        {
            _db.RollbackTran();
            throw;
        }
    }

    /// <summary>
    /// 异步事务
    /// </summary>
    /// <remarks>
    /// 使用异步方式管理事务
    /// </remarks>
    public async Task<bool> AsyncTransactionDemo(LeanUser user, LeanUserInfo userInfo)
    {
        return await _db.Ado.UseTranAsync(async () =>
        {
            await _db.Insertable(user).ExecuteCommandAsync();
            await _db.Insertable(userInfo).ExecuteCommandAsync();
        });
    }
}
```

## 性能优化

### 查询优化
```csharp
/// <summary>
/// 查询优化示例
/// </summary>
public class QueryOptimizationExample
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 优化查询
    /// </summary>
    /// <remarks>
    /// 使用多种方式优化查询性能
    /// </remarks>
    public async Task<List<LeanUser>> OptimizedQueryAsync()
    {
        // 禁用实体跟踪
        var query1 = await _db.Queryable<LeanUser>()
            .AsTracking(false)
            .ToListAsync();

        // 只查询需要的字段
        var query2 = await _db.Queryable<LeanUser>()
            .Select(u => new { u.Id, u.UserName })
            .ToListAsync();

        // 使用 NoLock
        var query3 = await _db.Queryable<LeanUser>()
            .With(SqlWith.NoLock)
            .ToListAsync();

        return query1;
    }
}
```

### 批量操作优化
```csharp
/// <summary>
/// 批量操作优化示例
/// </summary>
public class BatchOperationExample
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 批量操作优化
    /// </summary>
    /// <remarks>
    /// 使用 Fastest 方法优化批量操作性能
    /// </remarks>
    public async Task BatchOperationOptimizationAsync(List<LeanUser> users)
    {
        // 批量新增优化
        await _db.Fastest<LeanUser>()
            .BulkCopyAsync(users);

        // 批量更新优化
        await _db.Fastest<LeanUser>()
            .BulkUpdateAsync(users);
    }
}
```

### SQL 审计
```csharp
/// <summary>
/// SQL 审计示例
/// </summary>
public class SqlAuditExample
{
    private readonly ISqlSugarClient _db;
    private readonly ILogger _logger;

    /// <summary>
    /// 配置 SQL 审计
    /// </summary>
    /// <remarks>
    /// 记录 SQL 语句和参数用于性能分析和调试
    /// </remarks>
    public void ConfigureSqlAudit()
    {
        _db.Aop.OnLogExecuting = (sql, pars) =>
        {
            _logger.LogInformation($"SQL: {sql}");
            _logger.LogInformation($"Parameters: {string.Join(",", pars)}");
        };
    }
}
```

## 代码生成

### DbFirst 规范

### DbFirst 配置
```csharp
/// <summary>
/// DbFirst 配置示例
/// </summary>
public class DbFirstConfig
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 生成实体代码
    /// </summary>
    /// <remarks>
    /// 1. 指定实体命名空间
    /// 2. 配置生成规则
    /// 3. 指定输出路径
    /// 4. 添加完整注释
    /// </remarks>
    public void GenerateEntities()
    {
        // 基础配置
        var config = new DbFirstTemplate
        {
            // 指定命名空间
            ClassNamespace = "Lean.Cus.Domain.Entities",
            // 指定实体前缀
            ClassPrefix = "Lean",
            // 是否生成特性
            IsCreateAttribute = true,
            // 是否生成默认值
            IsCreateDefaultValue = true,
            // 移除表前缀
            TablePrefixList = new List<string> { "lean_" }
        };

        // 生成实体
        _db.DbFirst
            // 设置生成规则
            .SettingClassTemplate(old =>
            {
                return $@"
using System;
using SqlSugar;
using System.ComponentModel;

namespace {config.ClassNamespace}
{{
/// <summary>
    /// {old.TableDescription}
/// </summary>
    [SugarTable(""{old.TableName}"", ""{old.TableDescription}"")]
    public class {old.ClassName} : LeanEntity
    {{
        {old.Fields}
    }}
}}";
            })
            // 设置属性模板
            .SettingPropertyTemplate(old =>
            {
                var columnType = old.DataType;
                var isNullable = old.IsNullable;
                var defaultValue = old.DefaultValue;

                return $@"
    /// <summary>
        /// {old.ColumnDescription}
    /// </summary>
    /// <remarks>
        /// 数据类型：{columnType}
        /// 长度：{old.Length}
        /// 允许为空：{(isNullable ? "是" : "否")}
        /// 默认值：{defaultValue}
    /// </remarks>
        [SugarColumn(ColumnName = ""{old.DbColumnName}"", ColumnDescription = ""{old.ColumnDescription}"",
            IsNullable = {isNullable.ToString().ToLower()}, Length = {old.Length}{(string.IsNullOrEmpty(defaultValue) ? "" : $", DefaultValue = \"{defaultValue}\"")})]
        public {columnType}{(isNullable ? "?" : "")} {old.PropertyName} {{ get; set; }}";
            })
            // 指定数据表
            .Where(it => it.StartsWith("lean_"))
            // 生成文件
            .CreateClassFile("Entities");
    }

    /// <summary>
    /// 生成仓储代码
    /// </summary>
    /// <remarks>
    /// 1. 生成仓储接口
    /// 2. 生成仓储实现
    /// 3. 添加完整注释
    /// </remarks>
    public void GenerateRepositories()
    {
        _db.DbFirst
            .SettingClassTemplate(old =>
            {
                return $@"
using Lean.Cus.Domain.Entities;
using Lean.Cus.Domain.Repositories;

namespace Lean.Cus.Infrastructure.Repositories
{{
    /// <summary>
    /// {old.TableDescription}仓储实现
    /// </summary>
    public class {old.ClassName}Repository : BaseRepository<{old.ClassName}>, I{old.ClassName}Repository
    {{
        public {old.ClassName}Repository(ISqlSugarClient context) : base(context)
        {{
        }}
    }}
}}";
            })
            .Where(it => it.StartsWith("lean_"))
            .CreateClassFile("Repositories", "Repository");
    }
}
```

### DbFirst 生成规则
1. 命名规范
   - 实体类名必须以 `Lean` 前缀开头
   - 属性名使用 PascalCase 命名
   - 移除表前缀 `lean_`
   - 仓储类名以 `Repository` 结尾

2. 注释规范
   - 必须生成完整的类注释
   - 必须生成完整的属性注释
   - 注释必须包含中文说明
   - 属性注释必须包含数据类型、长度、是否可空等信息

3. 特性规范
   - 必须生成 `[SugarTable]` 特性
   - 必须生成 `[SugarColumn]` 特性
   - 必须包含列名和说明
   - 必须设置是否可空和长度

4. 生成内容规范
   - 生成实体类
   - 生成仓储接口
   - 生成仓储实现
   - 生成数据传输对象（DTO）

### CodeFirst 规范

### CodeFirst 配置
```csharp
/// <summary>
/// CodeFirst 配置示例
/// </summary>
public class CodeFirstConfig
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <remarks>
    /// 1. 创建数据库
    /// 2. 创建数据表
    /// 3. 创建索引
    /// 4. 添加初始数据
    /// </remarks>
    public void Initialize()
    {
        // 创建数据库
        _db.DbMaintenance.CreateDatabase();

        // 创建表
        _db.CodeFirst
            .SetStringDefaultLength(50) // 设置字符串默认长度
            .InitTables(typeof(LeanEntity).Assembly); // 初始化所有实体表

        // 添加种子数据
        if (!_db.Queryable<LeanUser>().Any())
        {
            _db.Insertable(new LeanUser
            {
                UserName = "admin",
                Password = "123456",
                Status = 1,
                CreateTime = DateTime.Now,
                CreateBy = 0
            }).ExecuteCommand();
        }
    }

    /// <summary>
    /// 更新数据库
    /// </summary>
    /// <remarks>
    /// 1. 比较实体和数据表的差异
    /// 2. 添加新字段
    /// 3. 修改字段属性
    /// 4. 添加新索引
    /// </remarks>
    public void UpdateDatabase()
    {
        // 开发环境下执行
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            _db.CodeFirst
                .SetStringDefaultLength(50)
                .BackupTable() // 备份表
                .InitTables(typeof(LeanEntity).Assembly);
        }
    }
}
```

### CodeFirst 实体规范
```csharp
/// <summary>
/// 用户实体示例
/// </summary>
[SugarTable("lean_user", "用户表")]
[SugarIndex("idx_user_username", nameof(UserName), OrderByType.Asc, true)]
public class LeanUser : LeanEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    /// <remarks>
    /// 数据类型：nvarchar(50)
    /// 允许为空：否
    /// 说明：登录账号，不可重复
    /// </remarks>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", 
        Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    /// <remarks>
    /// 数据类型：varchar(100)
    /// 允许为空：否
    /// 说明：登录密码，存储加密后的值
    /// </remarks>
    [SugarColumn(ColumnName = "password", ColumnDescription = "密码", 
        Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string Password { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：int
    /// 允许为空：否
    /// 说明：用户状态，1-启用，0-禁用
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", 
        IsNullable = false, ColumnDataType = "int")]
    public int Status { get; set; }
}
```

### CodeFirst 规范要点

1. 数据库设计规范
   - 表名必须以 `lean_` 前缀开头
   - 字段名使用小写，单词间用下划线分隔
   - 主键统一使用 `id`，类型为 `bigint`
   - 必须包含基础字段（创建时间、创建人等）
   - 所有表必须有注释说明

2. 字段设计规范
   - 字符串类型默认长度 50
   - 金额类型使用 decimal(18,5)
   - 状态字段使用 int 类型
   - 时间字段使用 datetime 类型
   - 必须指定是否可空

3. 索引设计规范
   - 主键必须建立聚集索引
   - 外键字段必须建立索引
   - 经常查询的字段建议建立索引
   - 唯一字段必须建立唯一索引

4. 种子数据规范
   - 必须提供基础的字典数据
   - 必须提供管理员账号
   - 必须提供基础的权限数据
   - 必须提供系统必需的配置数据

5. 更新策略规范
   - 开发环境可以删除重建表
   - 测试环境必须备份数据
   - 生产环境只能新增字段
   - 必须保留表结构变更记录

### 实体生成模板
```csharp
    /// <summary>
/// 实体生成模板配置
    /// </summary>
public static class EntityTemplate
{
    /// <summary>
    /// 获取实体类模板
    /// </summary>
    public static string GetClassTemplate(string tableName, string tableDesc)
    {
        return $@"
using System;
using SqlSugar;
using System.ComponentModel;

namespace Lean.Cus.Domain.Entities
{{
    /// <summary>
    /// {tableDesc}
    /// </summary>
    [SugarTable(""{tableName}"", ""{tableDesc}"")]
    public class {GetClassName(tableName)} : LeanEntity
    {{
        {GetPropertyTemplate()}
    }}
}}";
    }

    /// <summary>
    /// 获取属性模板
    /// </summary>
    public static string GetPropertyTemplate()
    {
        return $@"
        /// <summary>
        /// 属性说明
        /// </summary>
        /// <remarks>
        /// 数据类型：
        /// 长度：
        /// 允许为空：
        /// 说明：
        /// </remarks>
        [SugarColumn(ColumnName = """", ColumnDescription = """",
            IsNullable = false, Length = 50)]
        public string PropertyName {{ get; set; }}";
    }

    /// <summary>
    /// 获取类名
    /// </summary>
    private static string GetClassName(string tableName)
    {
        // 移除前缀，转换为帕斯卡命名
        return "Lean" + tableName.Replace("lean_", "")
            .Split('_')
            .Select(s => char.ToUpper(s[0]) + s.Substring(1))
            .Aggregate((a, b) => a + b);
    }
}
```

### 数据库更新记录
```csharp
/// <summary>
/// 数据库更新记录
/// </summary>
public class DatabaseUpdateLog
{
    /// <summary>
    /// 记录数据库更新
    /// </summary>
    public static void LogUpdate(string description)
    {
        var log = new
        {
            Version = GetVersion(),
            Description = description,
            UpdateTime = DateTime.Now,
            UpdateBy = "System"
        };

        // 记录到数据库或日志文件
        File.AppendAllText("db_update.log", 
            JsonSerializer.Serialize(log) + Environment.NewLine);
    }

    /// <summary>
    /// 获取版本号
    /// </summary>
    private static string GetVersion()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss");
    }
}
```

## DbFirst 规范

### DbFirst 配置
```csharp
/// <summary>
/// DbFirst 配置示例
/// </summary>
public class DbFirstConfig
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 生成实体代码
    /// </summary>
    /// <remarks>
    /// 1. 指定实体命名空间
    /// 2. 配置生成规则
    /// 3. 指定输出路径
    /// 4. 添加完整注释
    /// </remarks>
    public void GenerateEntities()
    {
        // 基础配置
        var config = new DbFirstTemplate
        {
            // 指定命名空间
            ClassNamespace = "Lean.Cus.Domain.Entities",
            // 指定实体前缀
            ClassPrefix = "Lean",
            // 是否生成特性
            IsCreateAttribute = true,
            // 是否生成默认值
            IsCreateDefaultValue = true,
            // 移除表前缀
            TablePrefixList = new List<string> { "lean_" }
        };

        // 生成实体
        _db.DbFirst
            // 设置生成规则
            .SettingClassTemplate(old =>
            {
                return $@"
using System;
using SqlSugar;
using System.ComponentModel;

namespace {config.ClassNamespace}
{{
    /// <summary>
    /// {old.TableDescription}
    /// </summary>
    [SugarTable(""{old.TableName}"", ""{old.TableDescription}"")]
    public class {old.ClassName} : LeanEntity
    {{
        {old.Fields}
    }}
}}";
            })
            // 设置属性模板
            .SettingPropertyTemplate(old =>
            {
                var columnType = old.DataType;
                var isNullable = old.IsNullable;
                var defaultValue = old.DefaultValue;

                return $@"
    /// <summary>
        /// {old.ColumnDescription}
    /// </summary>
        /// <remarks>
        /// 数据类型：{columnType}
        /// 长度：{old.Length}
        /// 允许为空：{(isNullable ? "是" : "否")}
        /// 默认值：{defaultValue}
        /// </remarks>
        [SugarColumn(ColumnName = ""{old.DbColumnName}"", ColumnDescription = ""{old.ColumnDescription}"",
            IsNullable = {isNullable.ToString().ToLower()}, Length = {old.Length}{(string.IsNullOrEmpty(defaultValue) ? "" : $", DefaultValue = \"{defaultValue}\"")})]
        public {columnType}{(isNullable ? "?" : "")} {old.PropertyName} {{ get; set; }}";
            })
            // 指定数据表
            .Where(it => it.StartsWith("lean_"))
            // 生成文件
            .CreateClassFile("Entities");
    }

    /// <summary>
    /// 生成仓储代码
    /// </summary>
    /// <remarks>
    /// 1. 生成仓储接口
    /// 2. 生成仓储实现
    /// 3. 添加完整注释
    /// </remarks>
    public void GenerateRepositories()
    {
        _db.DbFirst
            .SettingClassTemplate(old =>
            {
                return $@"
using Lean.Cus.Domain.Entities;
using Lean.Cus.Domain.Repositories;

namespace Lean.Cus.Infrastructure.Repositories
{{
    /// <summary>
    /// {old.TableDescription}仓储实现
    /// </summary>
    public class {old.ClassName}Repository : BaseRepository<{old.ClassName}>, I{old.ClassName}Repository
    {{
        public {old.ClassName}Repository(ISqlSugarClient context) : base(context)
        {{
        }}
    }}
}}";
            })
            .Where(it => it.StartsWith("lean_"))
            .CreateClassFile("Repositories", "Repository");
    }
}
```

### DbFirst 生成规则
1. 命名规范
   - 实体类名必须以 `Lean` 前缀开头
   - 属性名使用 PascalCase 命名
   - 移除表前缀 `lean_`
   - 仓储类名以 `Repository` 结尾

2. 注释规范
   - 必须生成完整的类注释
   - 必须生成完整的属性注释
   - 注释必须包含中文说明
   - 属性注释必须包含数据类型、长度、是否可空等信息

3. 特性规范
   - 必须生成 `[SugarTable]` 特性
   - 必须生成 `[SugarColumn]` 特性
   - 必须包含列名和说明
   - 必须设置是否可空和长度

4. 生成内容规范
   - 生成实体类
   - 生成仓储接口
   - 生成仓储实现
   - 生成数据传输对象（DTO）

## CodeFirst 规范

### CodeFirst 配置
```csharp
/// <summary>
/// CodeFirst 配置示例
/// </summary>
public class CodeFirstConfig
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <remarks>
    /// 1. 创建数据库
    /// 2. 创建数据表
    /// 3. 创建索引
    /// 4. 添加初始数据
    /// </remarks>
    public void Initialize()
    {
        // 创建数据库
        _db.DbMaintenance.CreateDatabase();

        // 创建表
        _db.CodeFirst
            .SetStringDefaultLength(50) // 设置字符串默认长度
            .InitTables(typeof(LeanEntity).Assembly); // 初始化所有实体表

        // 添加种子数据
        if (!_db.Queryable<LeanUser>().Any())
        {
            _db.Insertable(new LeanUser
            {
                UserName = "admin",
                Password = "123456",
                Status = 1,
                CreateTime = DateTime.Now,
                CreateBy = 0
            }).ExecuteCommand();
        }
    }

    /// <summary>
    /// 更新数据库
    /// </summary>
    /// <remarks>
    /// 1. 比较实体和数据表的差异
    /// 2. 添加新字段
    /// 3. 修改字段属性
    /// 4. 添加新索引
    /// </remarks>
    public void UpdateDatabase()
    {
        // 开发环境下执行
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            _db.CodeFirst
                .SetStringDefaultLength(50)
                .BackupTable() // 备份表
                .InitTables(typeof(LeanEntity).Assembly);
        }
    }
}
```

### CodeFirst 实体规范
```csharp
/// <summary>
/// 用户实体示例
/// </summary>
[SugarTable("lean_user", "用户表")]
[SugarIndex("idx_user_username", nameof(UserName), OrderByType.Asc, true)]
public class LeanUser : LeanEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    /// <remarks>
    /// 数据类型：nvarchar(50)
    /// 允许为空：否
    /// 说明：登录账号，不可重复
    /// </remarks>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", 
        Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string UserName { get; set; }

/// <summary>
    /// 密码
/// </summary>
    /// <remarks>
    /// 数据类型：varchar(100)
    /// 允许为空：否
    /// 说明：登录密码，存储加密后的值
    /// </remarks>
    [SugarColumn(ColumnName = "password", ColumnDescription = "密码", 
        Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string Password { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：int
    /// 允许为空：否
    /// 说明：用户状态，1-启用，0-禁用
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", 
        IsNullable = false, ColumnDataType = "int")]
    public int Status { get; set; }
}
```

### CodeFirst 规范要点

1. 数据库设计规范
   - 表名必须以 `lean_` 前缀开头
   - 字段名使用小写，单词间用下划线分隔
   - 主键统一使用 `id`，类型为 `bigint`
   - 必须包含基础字段（创建时间、创建人等）
   - 所有表必须有注释说明

2. 字段设计规范
   - 字符串类型默认长度 50
   - 金额类型使用 decimal(18,5)
   - 状态字段使用 int 类型
   - 时间字段使用 datetime 类型
   - 必须指定是否可空

3. 索引设计规范
   - 主键必须建立聚集索引
   - 外键字段必须建立索引
   - 经常查询的字段建议建立索引
   - 唯一字段必须建立唯一索引

4. 种子数据规范
   - 必须提供基础的字典数据
   - 必须提供管理员账号
   - 必须提供基础的权限数据
   - 必须提供系统必需的配置数据

5. 更新策略规范
   - 开发环境可以删除重建表
   - 测试环境必须备份数据
   - 生产环境只能新增字段
   - 必须保留表结构变更记录

### 实体生成模板
```csharp
/// <summary>
/// 实体生成模板配置
/// </summary>
public static class EntityTemplate
{
    /// <summary>
    /// 获取实体类模板
    /// </summary>
    public static string GetClassTemplate(string tableName, string tableDesc)
    {
        return $@"
using System;
using SqlSugar;
using System.ComponentModel;

namespace Lean.Cus.Domain.Entities
{{
    /// <summary>
    /// {tableDesc}
    /// </summary>
    [SugarTable(""{tableName}"", ""{tableDesc}"")]
    public class {GetClassName(tableName)} : LeanEntity
    {{
        {GetPropertyTemplate()}
    }}
}}";
    }

    /// <summary>
    /// 获取属性模板
    /// </summary>
    public static string GetPropertyTemplate()
    {
        return $@"
        /// <summary>
        /// 属性说明
        /// </summary>
        /// <remarks>
        /// 数据类型：
        /// 长度：
        /// 允许为空：
        /// 说明：
        /// </remarks>
        [SugarColumn(ColumnName = """", ColumnDescription = """",
            IsNullable = false, Length = 50)]
        public string PropertyName {{ get; set; }}";
    }

    /// <summary>
    /// 获取类名
    /// </summary>
    private static string GetClassName(string tableName)
    {
        // 移除前缀，转换为帕斯卡命名
        return "Lean" + tableName.Replace("lean_", "")
            .Split('_')
            .Select(s => char.ToUpper(s[0]) + s.Substring(1))
            .Aggregate((a, b) => a + b);
    }
}
```

### 数据库更新记录
```csharp
/// <summary>
/// 数据库更新记录
/// </summary>
public class DatabaseUpdateLog
{
    /// <summary>
    /// 记录数据库更新
    /// </summary>
    public static void LogUpdate(string description)
    {
        var log = new
        {
            Version = GetVersion(),
            Description = description,
            UpdateTime = DateTime.Now,
            UpdateBy = "System"
        };

        // 记录到数据库或日志文件
        File.AppendAllText("db_update.log", 
            JsonSerializer.Serialize(log) + Environment.NewLine);
    }

    /// <summary>
    /// 获取版本号
    /// </summary>
    private static string GetVersion()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss");
    }
}
```

## DbFirst 规范

### DbFirst 配置
```csharp
/// <summary>
/// DbFirst 配置示例
/// </summary>
public class DbFirstConfig
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 生成实体代码
    /// </summary>
    /// <remarks>
    /// 1. 指定实体命名空间
    /// 2. 配置生成规则
    /// 3. 指定输出路径
    /// 4. 添加完整注释
    /// </remarks>
    public void GenerateEntities()
    {
        // 基础配置
        var config = new DbFirstTemplate
        {
            // 指定命名空间
            ClassNamespace = "Lean.Cus.Domain.Entities",
            // 指定实体前缀
            ClassPrefix = "Lean",
            // 是否生成特性
            IsCreateAttribute = true,
            // 是否生成默认值
            IsCreateDefaultValue = true,
            // 移除表前缀
            TablePrefixList = new List<string> { "lean_" }
        };

        // 生成实体
        _db.DbFirst
            // 设置生成规则
            .SettingClassTemplate(old =>
            {
                return $@"
using System;
using SqlSugar;
using System.ComponentModel;

namespace {config.ClassNamespace}
{{
    /// <summary>
    /// {old.TableDescription}
    /// </summary>
    [SugarTable(""{old.TableName}"", ""{old.TableDescription}"")]
    public class {old.ClassName} : LeanEntity
    {{
        {old.Fields}
    }}
}}";
            })
            // 设置属性模板
            .SettingPropertyTemplate(old =>
            {
                var columnType = old.DataType;
                var isNullable = old.IsNullable;
                var defaultValue = old.DefaultValue;

                return $@"
        /// <summary>
        /// {old.ColumnDescription}
        /// </summary>
        /// <remarks>
        /// 数据类型：{columnType}
        /// 长度：{old.Length}
        /// 允许为空：{(isNullable ? "是" : "否")}
        /// 默认值：{defaultValue}
        /// </remarks>
        [SugarColumn(ColumnName = ""{old.DbColumnName}"", ColumnDescription = ""{old.ColumnDescription}"",
            IsNullable = {isNullable.ToString().ToLower()}, Length = {old.Length}{(string.IsNullOrEmpty(defaultValue) ? "" : $", DefaultValue = \"{defaultValue}\"")})]
        public {columnType}{(isNullable ? "?" : "")} {old.PropertyName} {{ get; set; }}";
            })
            // 指定数据表
            .Where(it => it.StartsWith("lean_"))
            // 生成文件
            .CreateClassFile("Entities");
    }

    /// <summary>
    /// 生成仓储代码
    /// </summary>
    /// <remarks>
    /// 1. 生成仓储接口
    /// 2. 生成仓储实现
    /// 3. 添加完整注释
    /// </remarks>
    public void GenerateRepositories()
    {
        _db.DbFirst
            .SettingClassTemplate(old =>
            {
                return $@"
using Lean.Cus.Domain.Entities;
using Lean.Cus.Domain.Repositories;

namespace Lean.Cus.Infrastructure.Repositories
{{
    /// <summary>
    /// {old.TableDescription}仓储实现
    /// </summary>
    public class {old.ClassName}Repository : BaseRepository<{old.ClassName}>, I{old.ClassName}Repository
    {{
        public {old.ClassName}Repository(ISqlSugarClient context) : base(context)
        {{
        }}
    }}
}}";
            })
            .Where(it => it.StartsWith("lean_"))
            .CreateClassFile("Repositories", "Repository");
    }
}
```

### DbFirst 生成规则
1. 命名规范
   - 实体类名必须以 `Lean` 前缀开头
   - 属性名使用 PascalCase 命名
   - 移除表前缀 `lean_`
   - 仓储类名以 `Repository` 结尾

2. 注释规范
   - 必须生成完整的类注释
   - 必须生成完整的属性注释
   - 注释必须包含中文说明
   - 属性注释必须包含数据类型、长度、是否可空等信息

3. 特性规范
   - 必须生成 `[SugarTable]` 特性
   - 必须生成 `[SugarColumn]` 特性
   - 必须包含列名和说明
   - 必须设置是否可空和长度

4. 生成内容规范
   - 生成实体类
   - 生成仓储接口
   - 生成仓储实现
   - 生成数据传输对象（DTO）

## CodeFirst 规范

### CodeFirst 配置
```csharp
/// <summary>
/// CodeFirst 配置示例
/// </summary>
public class CodeFirstConfig
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <remarks>
    /// 1. 创建数据库
    /// 2. 创建数据表
    /// 3. 创建索引
    /// 4. 添加初始数据
    /// </remarks>
    public void Initialize()
    {
        // 创建数据库
        _db.DbMaintenance.CreateDatabase();

        // 创建表
        _db.CodeFirst
            .SetStringDefaultLength(50) // 设置字符串默认长度
            .InitTables(typeof(LeanEntity).Assembly); // 初始化所有实体表

        // 添加种子数据
        if (!_db.Queryable<LeanUser>().Any())
        {
            _db.Insertable(new LeanUser
            {
                UserName = "admin",
                Password = "123456",
                Status = 1,
                CreateTime = DateTime.Now,
                CreateBy = 0
            }).ExecuteCommand();
        }
    }

    /// <summary>
    /// 更新数据库
    /// </summary>
    /// <remarks>
    /// 1. 比较实体和数据表的差异
    /// 2. 添加新字段
    /// 3. 修改字段属性
    /// 4. 添加新索引
    /// </remarks>
    public void UpdateDatabase()
    {
        // 开发环境下执行
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            _db.CodeFirst
                .SetStringDefaultLength(50)
                .BackupTable() // 备份表
                .InitTables(typeof(LeanEntity).Assembly);
        }
    }
}
```

### CodeFirst 实体规范
```csharp
/// <summary>
/// 用户实体示例
/// </summary>
[SugarTable("lean_user", "用户表")]
[SugarIndex("idx_user_username", nameof(UserName), OrderByType.Asc, true)]
public class LeanUser : LeanEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    /// <remarks>
    /// 数据类型：nvarchar(50)
    /// 允许为空：否
    /// 说明：登录账号，不可重复
    /// </remarks>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", 
        Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    /// <remarks>
    /// 数据类型：varchar(100)
    /// 允许为空：否
    /// 说明：登录密码，存储加密后的值
    /// </remarks>
    [SugarColumn(ColumnName = "password", ColumnDescription = "密码", 
        Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string Password { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：int
    /// 允许为空：否
    /// 说明：用户状态，1-启用，0-禁用
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", 
        IsNullable = false, ColumnDataType = "int")]
    public int Status { get; set; }
}
```

### CodeFirst 规范要点

1. 数据库设计规范
   - 表名必须以 `lean_` 前缀开头
   - 字段名使用小写，单词间用下划线分隔
   - 主键统一使用 `id`，类型为 `bigint`
   - 必须包含基础字段（创建时间、创建人等）
   - 所有表必须有注释说明

2. 字段设计规范
   - 字符串类型默认长度 50
   - 金额类型使用 decimal(18,5)
   - 状态字段使用 int 类型
   - 时间字段使用 datetime 类型
   - 必须指定是否可空

3. 索引设计规范
   - 主键必须建立聚集索引
   - 外键字段必须建立索引
   - 经常查询的字段建议建立索引
   - 唯一字段必须建立唯一索引

4. 种子数据规范
   - 必须提供基础的字典数据
   - 必须提供管理员账号
   - 必须提供基础的权限数据
   - 必须提供系统必需的配置数据

5. 更新策略规范
   - 开发环境可以删除重建表
   - 测试环境必须备份数据
   - 生产环境只能新增字段
   - 必须保留表结构变更记录

### 实体生成模板
```csharp
    /// <summary>
/// 实体生成模板配置
    /// </summary>
public static class EntityTemplate
{
    /// <summary>
    /// 获取实体类模板
    /// </summary>
    public static string GetClassTemplate(string tableName, string tableDesc)
    {
        return $@"
using System;
using SqlSugar;
using System.ComponentModel;

namespace Lean.Cus.Domain.Entities
{{
    /// <summary>
    /// {tableDesc}
    /// </summary>
    [SugarTable(""{tableName}"", ""{tableDesc}"")]
    public class {GetClassName(tableName)} : LeanEntity
    {{
        {GetPropertyTemplate()}
    }}
}}";
}

/// <summary>
    /// 获取属性模板
/// </summary>
    public static string GetPropertyTemplate()
{
        return $@"
    /// <summary>
        /// 属性说明
    /// </summary>
        /// <remarks>
        /// 数据类型：
        /// 长度：
        /// 允许为空：
        /// 说明：
        /// </remarks>
        [SugarColumn(ColumnName = """", ColumnDescription = """",
            IsNullable = false, Length = 50)]
        public string PropertyName {{ get; set; }}";
    }

    /// <summary>
    /// 获取类名
    /// </summary>
    private static string GetClassName(string tableName)
    {
        // 移除前缀，转换为帕斯卡命名
        return "Lean" + tableName.Replace("lean_", "")
            .Split('_')
            .Select(s => char.ToUpper(s[0]) + s.Substring(1))
            .Aggregate((a, b) => a + b);
    }
}
```

### 数据库更新记录
```csharp
    /// <summary>
/// 数据库更新记录
    /// </summary>
public class DatabaseUpdateLog
{
    /// <summary>
    /// 记录数据库更新
    /// </summary>
    public static void LogUpdate(string description)
    {
        var log = new
        {
            Version = GetVersion(),
            Description = description,
            UpdateTime = DateTime.Now,
            UpdateBy = "System"
        };

        // 记录到数据库或日志文件
        File.AppendAllText("db_update.log", 
            JsonSerializer.Serialize(log) + Environment.NewLine);
    }

    /// <summary>
    /// 获取版本号
    /// </summary>
    private static string GetVersion()
    {
        return DateTime.Now.ToString("yyyyMMddHHmmss");
    }
}
```

## DbFirst 规范

### DbFirst 配置
```csharp
/// <summary>
/// DbFirst 配置示例
/// </summary>
public class DbFirstConfig
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 生成实体代码
    /// </summary>
    /// <remarks>
    /// 1. 指定实体命名空间
    /// 2. 配置生成规则
    /// 3. 指定输出路径
    /// 4. 添加完整注释
    /// </remarks>
    public void GenerateEntities()
    {
        // 基础配置
        var config = new DbFirstTemplate
        {
            // 指定命名空间
            ClassNamespace = "Lean.Cus.Domain.Entities",
            // 指定实体前缀
            ClassPrefix = "Lean",
            // 是否生成特性
            IsCreateAttribute = true,
            // 是否生成默认值
            IsCreateDefaultValue = true,
            // 移除表前缀
            TablePrefixList = new List<string> { "lean_" }
        };

        // 生成实体
        _db.DbFirst
            // 设置生成规则
            .SettingClassTemplate(old =>
            {
                return $@"
using System;
using SqlSugar;
using System.ComponentModel;

namespace {config.ClassNamespace}
{{
    /// <summary>
    /// {old.TableDescription}
    /// </summary>
    [SugarTable(""{old.TableName}"", ""{old.TableDescription}"")]
    public class {old.ClassName} : LeanEntity
    {{
        {old.Fields}
    }}
}}";
            })
            // 设置属性模板
            .SettingPropertyTemplate(old =>
            {
                var columnType = old.DataType;
                var isNullable = old.IsNullable;
                var defaultValue = old.DefaultValue;

                return $@"
        /// <summary>
        /// {old.ColumnDescription}
    /// </summary>
    /// <remarks>
        /// 数据类型：{columnType}
        /// 长度：{old.Length}
        /// 允许为空：{(isNullable ? "是" : "否")}
        /// 默认值：{defaultValue}
    /// </remarks>
        [SugarColumn(ColumnName = ""{old.DbColumnName}"", ColumnDescription = ""{old.ColumnDescription}"",
            IsNullable = {isNullable.ToString().ToLower()}, Length = {old.Length}{(string.IsNullOrEmpty(defaultValue) ? "" : $", DefaultValue = \"{defaultValue}\"")})]
        public {columnType}{(isNullable ? "?" : "")} {old.PropertyName} {{ get; set; }}";
            })
            // 指定数据表
            .Where(it => it.StartsWith("lean_"))
            // 生成文件
            .CreateClassFile("Entities");
    }

    /// <summary>
    /// 生成仓储代码
    /// </summary>
    /// <remarks>
    /// 1. 生成仓储接口
    /// 2. 生成仓储实现
    /// 3. 添加完整注释
    /// </remarks>
    public void GenerateRepositories()
    {
        _db.DbFirst
            .SettingClassTemplate(old =>
            {
                return $@"
using Lean.Cus.Domain.Entities;
using Lean.Cus.Domain.Repositories;

namespace Lean.Cus.Infrastructure.Repositories
{{
    /// <summary>
    /// {old.TableDescription}仓储实现
    /// </summary>
    public class {old.ClassName}Repository : BaseRepository<{old.ClassName}>, I{old.ClassName}Repository
    {{
        public {old.ClassName}Repository(ISqlSugarClient context) : base(context)
        {{
        }}
    }}
}}";
            })
            .Where(it => it.StartsWith("lean_"))
            .CreateClassFile("Repositories", "Repository");
    }
}
```

### DbFirst 生成规则
1. 命名规范
   - 实体类名必须以 `Lean` 前缀开头
   - 属性名使用 PascalCase 命名
   - 移除表前缀 `lean_`
   - 仓储类名以 `Repository` 结尾

2. 注释规范
   - 必须生成完整的类注释
   - 必须生成完整的属性注释
   - 注释必须包含中文说明
   - 属性注释必须包含数据类型、长度、是否可空等信息

3. 特性规范
   - 必须生成 `[SugarTable]` 特性
   - 必须生成 `[SugarColumn]` 特性
   - 必须包含列名和说明
   - 必须设置是否可空和长度

4. 生成内容规范
   - 生成实体类
   - 生成仓储接口
   - 生成仓储实现
   - 生成数据传输对象（DTO）

## CodeFirst 规范

### CodeFirst 配置
```csharp
    /// <summary>
/// CodeFirst 配置示例
/// </summary>
public class CodeFirstConfig
{
    private readonly ISqlSugarClient _db;

    /// <summary>
    /// 初始化数据库
    /// </summary>
    /// <remarks>
    /// 1. 创建数据库
    /// 2. 创建数据表
    /// 3. 创建索引
    /// 4. 添加初始数据
    /// </remarks>
    public void Initialize()
    {
        // 创建数据库
        _db.DbMaintenance.CreateDatabase();

        // 创建表
        _db.CodeFirst
            .SetStringDefaultLength(50) // 设置字符串默认长度
            .InitTables(typeof(LeanEntity).Assembly); // 初始化所有实体表

        // 添加种子数据
        if (!_db.Queryable<LeanUser>().Any())
        {
            _db.Insertable(new LeanUser
            {
                UserName = "admin",
                Password = "123456",
                Status = 1,
                CreateTime = DateTime.Now,
                CreateBy = 0
            }).ExecuteCommand();
        }
    }

    /// <summary>
    /// 更新数据库
    /// </summary>
    /// <remarks>
    /// 1. 比较实体和数据表的差异
    /// 2. 添加新字段
    /// 3. 修改字段属性
    /// 4. 添加新索引
    /// </remarks>
    public void UpdateDatabase()
    {
        // 开发环境下执行
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development")
        {
            _db.CodeFirst
                .SetStringDefaultLength(50)
                .BackupTable() // 备份表
                .InitTables(typeof(LeanEntity).Assembly);
        }
    }
}
```

### CodeFirst 实体规范
```csharp
/// <summary>
/// 用户实体示例
/// </summary>
[SugarTable("lean_user", "用户表")]
[SugarIndex("idx_user_username", nameof(UserName), OrderByType.Asc, true)]
public class LeanUser : LeanEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    /// <remarks>
    /// 数据类型：nvarchar(50)
    /// 允许为空：否
    /// 说明：登录账号，不可重复
    /// </remarks>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", 
        Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    /// <remarks>
    /// 数据类型：varchar(100)
    /// 允许为空：否
    /// 说明：登录密码，存储加密后的值
    /// </remarks>
    [SugarColumn(ColumnName = "password", ColumnDescription = "密码", 
        Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string Password { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    /// <remarks>
    /// 数据类型：int
    /// 允许为空：否
    /// 说明：用户状态，1-启用，0-禁用
    /// </remarks>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", 
        IsNullable = false, ColumnDataType = "int")]
    public int Status { get; set; }
}
```

### CodeFirst 规范要点

1. 数据库设计规范
   - 表名必须以 `lean_` 前缀开头
   - 字段名使用小写，单词间用下划线分隔
   - 主键统一使用 `id`，类型为 `bigint`
   - 必须包含基础字段（创建时间、创建人等）
   - 所有表必须有注释说明

2. 字段设计规范
   - 字符串类型默认长度 50
   - 金额类型使用 decimal(18,5)
   - 状态字段使用 int 类型
   - 时间字段使用 datetime 类型
   - 必须指定是否可空

3. 索引设计规范
   - 主键必须建立聚集索引
   - 外键字段必须建立索引
   - 经常查询的字段建议建立索引
   - 唯一字段必须建立
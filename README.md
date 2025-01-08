# Lean.Cus - 企业级快速开发框架

## 📖 目录

- [项目介绍](#-项目介绍)
- [特色功能](#-特色功能)
- [快速开始](#-快速开始)
- [版本日志](#-版本日志)
- [最佳实践](#-最佳实践)
- [常见问题](#-常见问题)
- [贡献者](#-贡献者)
- [技术栈](#-技术栈)
- [联系我们](#-联系我们)
- [许可证](#-许可证)

## 📖 项目介绍

### 项目背景
Lean.Cus 是一个基于 .NET 8 + Vue 3 的现代化企业级快速开发框架。它采用最新的技术栈,遵循 DDD (领域驱动设计)架构,旨在帮助开发者快速构建高质量的企业应用。

### 项目目标
- 提供完整的企业应用开发解决方案
- 实现快速开发和轻松维护
- 确保系统安全性和可扩展性
- 支持多语言和多终端

### 核心特色
- 完整的 DDD 分层架构
- 强大的代码生成能力
- 灵活的权限控制系统
- 丰富的开发组件
- 完善的开发文档

### 应用场景
- 企业管理系统
- 业务处理平台
- 数据分析系统
- 工作流系统

## 🌟 特色功能

### 技术创新
- 基于 SqlSugar ORM 的高性能数据访问
- 基于 SignalR 的实时通信
- 基于 Vue 3 + TypeScript 的现代化前端
- 基于 Ant Design Vue 的专业 UI

### 功能亮点
- 多租户支持
- 多语言支持
- 数据权限控制
- 工作流引擎
- 报表设计器
- 代码生成器

### 性能优势
- 高性能 ORM
- 分布式缓存
- 异步处理
- 按需加载

## 📚 快速开始

### 环境要求
- .NET 8.0 SDK
- Node.js 18+
- SQL Server/MySQL/PostgreSQL
- Redis (可选)
- Visual Studio 2022/VS Code

### 开发环境设置
1. 克隆仓库
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. 后端配置
```bash
cd Backend/src/Lean.Cus.Api
dotnet restore
```

3. 前端配置
```bash
cd Frontend/src/Lean.Cus.Antd
pnpm install
```

### 启动运行
1. 启动后端
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

2. 启动前端
```bash
cd Frontend/src/Lean.Cus.Antd
pnpm dev
```

## 📋 版本日志

### v1.0.0 (2024-01)
- 初始版本发布
- 完整的 DDD 架构实现
- 基础功能模块完成
- 多语言支持

### v1.1.0 (计划中)
- 工作流引擎
- 报表设计器
- 移动端适配
- 性能优化

## 💡 最佳实践

### 开发建议
- 遵循 DDD 设计原则
- 使用代码生成器
- 编写单元测试
- 规范代码风格

### 性能优化
- 使用缓存
- 异步处理
- 代码压缩
- 懒加载

### 安全加固
- 参数验证
- SQL注入防护
- XSS防护
- CSRF防护

### 部署指南
- 环境配置
- 数据库部署
- 服务器设置
- 负载均衡

## ❓ 常见问题

### 安装问题
Q: 如何解决依赖安装失败？
A: 检查网络连接,使用国内镜像源

### 配置问题
Q: 如何配置数据库连接？
A: 修改 appsettings.json 中的连接字符串

### 运行问题
Q: 如何处理跨域问题？
A: 配置 CORS 中间件

### 性能问题
Q: 如何提高查询性能？
A: 使用缓存,优化索引,编写高效查询

## 👥 贡献者

### 核心团队
- [Lean365](https://github.com/Lean365) - 项目负责人
- 欢迎加入我们的开发团队！

### 贡献方式
1. Fork 项目
2. 创建新分支
3. 提交代码
4. 发起 Pull Request

## 🔧 技术栈

### 后端技术
- **.NET 8.0**: 最新的 .NET 开发平台
- **SqlSugar ORM**: 高性能 ORM，支持多种数据库
- **Redis**: 分布式缓存
- **SignalR**: 实时通信
- **Quartz.NET**: 任务调度

### 前端技术
- **Vue 3**: 渐进式 JavaScript 框架
- **TypeScript**: 类型安全的 JavaScript
- **Ant Design Vue**: UI 组件库
- **Vite**: 现代构建工具

## 📞 联系我们

- 官网：[https://lean365.com](https://lean365.com)
- 邮箱：support@lean365.com
- QQ群：123456789

## 📄 许可证

本项目采用 [MIT 许可证](LICENSE)

## 📚 相关文档

- [SqlSugar ORM 开发规范](./docs/LeanSqlSugar.md)
- [Windows Forms 客户端开发规范](./docs/LeanWinForms.md)
- [前端开发规范](./docs/LeanFrontend.md)
- [API 接口文档](./docs/LeanAPI.md)
- [部署指南](./docs/LeanDeploy.md)

## 📚 项目截图

### 后台管理
![后台管理](./docs/images/admin.png)

### 数据分析
![数据分析](./docs/images/analysis.png)

### 工作流
![工作流](./docs/images/workflow.png)

## 📝 开发规范

### 目录
- [通用规范](#通用规范)
  - [文件头规范](#文件头规范)
  - [注释规范](#注释规范)
  - [代码提交规范](#代码提交规范)
  - [分支管理规范](#分支管理规范)
  - [文档编写规范](#文档编写规范)

- [架构规范](#架构规范)
  - [DDD分层规范](#DDD分层规范)
  - [项目结构规范](#项目结构规范)
  - [依赖注入规范](#依赖注入规范)

- [后端开发规范](#后端开发规范)
  - [代码规范](#后端代码规范)
  - [接口设计规范](#接口设计规范)
  - [数据库设计规范](#数据库设计规范)
  - [缓存使用规范](#缓存使用规范)
  - [异常处理规范](#异常处理规范)
  - [日志规范](#日志规范)
  - [单元测试规范](#单元测试规范)

- [前端开发规范](#前端开发规范)
  - [代码规范](#前端代码规范)
  - [组件开发规范](#组件开发规范)
  - [状态管理规范](#状态管理规范)
  - [路由管理规范](#路由管理规范)
  - [UI/UX设计规范](#UI/UX设计规范)
  - [性能优化规范](#前端性能优化规范)
  - [安全规范](#前端安全规范)

- [工程化规范](#工程化规范)
  - [CI/CD规范](#CI/CD规范)
  - [环境管理规范](#环境管理规范)
  - [版本发布规范](#版本发布规范)
  - [监控告警规范](#监控告警规范)

### 通用规范

#### 文件头规范
- 所有代码文件（包括前端和后端）必须包含统一的文件头注释
- 文件头注释必须包含以下信息：
  - 版权声明
  - 文件名称
  - 功能描述
  - 创建时间
  - 创建人
  - 修改记录

示例文件头：
```csharp
//------------------------------------------------------------------------------
// Copyright (c) Lean365 团队.
// All rights reserved.
//
// 文件名：xxx.cs/xxx.vue/xxx.ts
// 功能描述：简要描述本文件的功能
//
// 创建时间：2024-01-01
// 创建人：Lean365
// 
// 修改记录：
// 时间：2024-01-01 修改人：Lean365 修改说明：创建文件
//------------------------------------------------------------------------------
```

#### 注释规范
1. 类注释
```csharp
/// <summary>
/// 类的功能描述
/// </summary>
/// <remarks>
/// 详细说明，包括使用场景、注意事项等
/// </remarks>
```

2. 方法注释
```csharp
/// <summary>
/// 方法的功能描述
/// </summary>
/// <param name="参数名">参数说明</param>
/// <returns>返回值说明</returns>
/// <exception cref="异常类型">可能抛出的异常说明</exception>
```

3. 属性注释
```csharp
/// <summary>
/// 属性说明
/// </summary>
/// <remarks>
/// 补充说明（如果需要）
/// </remarks>
```

4. 代码逻辑注释
```typescript
// 重要的业务逻辑必须添加注释
// 复杂的算法必须说明其思路
// 特殊处理必须注明原因
```

5. TODO注释
```typescript
// TODO: 待完成的功能
// FIXME: 待修复的问题
// NOTE: 需要注意的事项
```

#### 代码提交规范
- 提交信息必须符合以下格式：
```bash
<type>(<scope>): <subject>

<body>

<footer>
```

- type类型：
  - feat: 新功能
  - fix: 修复bug
  - docs: 文档修改
  - style: 代码格式修改
  - refactor: 重构代码
  - test: 测试用例修改
  - chore: 其他修改

- scope：影响范围
  - 后端：api, service, entity等
  - 前端：component, page, store等

- subject：简短描述，不超过50个字符
- body：详细描述
- footer：关联issue等信息

示例：
```bash
feat(user): 添加用户管理功能

1. 实现用户CRUD接口
2. 添加用户权限验证
3. 完善用户管理页面

Closes #123
```

#### 分支管理规范
- 分支命名规范：
  - 主分支：master/main
  - 开发分支：develop
  - 功能分支：feature/xxx
  - 修复分支：hotfix/xxx
  - 发布分支：release/xxx

- 分支工作流：
  1. 从develop分支创建feature分支
  2. 在feature分支开发新功能
  3. 完成后合并回develop分支
  4. 发布时从develop创建release分支
  5. 测试通过后合并到master分支

#### 文档编写规范
1. API文档规范
```yaml
接口名称：用户登录
接口地址：/api/auth/login
请求方式：POST
接口描述：用户登录接口，返回token

请求参数：
  - 名称：username
    类型：string
    必填：是
    描述：用户名
    示例：admin
    
  - 名称：password
    类型：string
    必填：是
    描述：密码
    示例：123456

响应参数：
  - 名称：token
    类型：string
    描述：访问令牌
    示例：eyJ0eXAiOiJKV1QiLCJhbGc...

错误码：
  - 401：用户名或密码错误
  - 423：账号已被锁定
```

2. 技术文档规范
- 文档结构：
  - 概述
  - 架构设计
  - 核心功能
  - 部署说明
  - 开发指南
  - 常见问题

- 文档要求：
  - 使用Markdown格式
  - 包含必要的图表
  - 代码示例完整
  - 步骤清晰明确
  - 定期更新维护

### 架构规范

#### DDD分层规范
1. 领域层（Domain）
- 职责：
  - 定义领域模型
  - 实现领域逻辑
  - 定义领域事件
  - 定义仓储接口

- 规范：
  - 不依赖其他层
  - 实体类必须继承LeanEntity
  - 充血模型，包含业务逻辑
  - 使用值对象表示属性组合

2. 应用层（Application）
- 职责：
  - 实现应用服务
  - 处理用例流程
  - 事务管理
  - 权限验证

- 规范：
  - 依赖领域层
  - 不包含业务逻辑
  - 使用DTO进行数据传输
  - 实现CQRS模式

3. 基础设施层（Infrastructure）
- 职责：
  - 实现仓储接口
  - 实现技术细节
  - 提供基础服务
  - 集成第三方

- 规范：
  - 依赖领域层
  - 实现持久化
  - 提供技术实现
  - 处理外部集成

4. 接口层（API）
- 职责：
  - 处理HTTP请求
  - 参数验证
  - 返回结果
  - 处理异常

- 规范：
  - 依赖应用层
  - 统一返回格式
  - 统一异常处理
  - 接口版本控制

5. 公共层（Common）
- 职责：
  - 提供通用工具类
  - 定义公共常量
  - 提供扩展方法
  - 封装第三方组件
  - 提供公共中间件
  - 定义通用注解

- 规范：
  - 不依赖其他层
  - 只包含与业务无关的通用功能
  - 遵循SOLID原则
  - 类名必须带有Lean前缀
  - 完整的XML注释
  - 单元测试覆盖率>80%

#### 项目结构规范
```
Backend/
├── src/
│   ├── Lean.Cus.Domain/             # 领域层
│   │   ├── Entities/                # 实体
│   │   ├── Events/                  # 领域事件
│   │   ├── Services/                # 领域服务
│   │   └── Repositories/            # 仓储接口
│   │
│   ├── Lean.Cus.Application/        # 应用层
│   │   ├── Services/                # 应用服务
│   │   ├── DTOs/                    # 数据传输对象
│   │   ├── Commands/                # 命令
│   │   └── Queries/                 # 查询
│   │
│   ├── Lean.Cus.Infrastructure/     # 基础设施层
│   │   ├── Repositories/            # 仓储实现
│   │   ├── Database/               # 数据库配置
│   │   ├── Cache/                   # 缓存实现
│   │   ├── Message/                 # 消息实现
│   │   └── Services/               # 第三方服务集成
│   │
│   ├── Lean.Cus.Common/            # 公共层
│   │   ├── Attributes/             # 自定义特性
│   │   ├── Caching/               # 缓存相关
│   │   ├── Configuration/         # 配置相关
│   │   ├── Constants/             # 常量定义
│   │   ├── Converters/           # 类型转换
│   │   ├── Encryption/           # 加密解密
│   │   ├── Enums/                # 枚举定义
│   │   ├── Extensions/           # 扩展方法
│   │   ├── Filters/              # 过滤器
│   │   ├── Helpers/              # 帮助类
│   │   ├── Json/                 # JSON处理
│   │   ├── Logging/              # 日志相关
│   │   ├── Middleware/           # 中间件
│   │   ├── Models/               # 通用模型
│   │   ├── Security/             # 安全相关
│   │   ├── Threading/            # 线程相关
│   │   └── Utils/                # 工具类
│   │
│   └── Lean.Cus.Api/               # 接口层
│       ├── Controllers/             # 控制器
│       ├── Filters/                 # 过滤器
│       ├── Middlewares/            # 中间件
│       └── Extensions/             # 扩展方法

Frontend/
├── src/
│   ├── api/                        # API接口
│   ├── assets/                     # 静态资源
│   ├── components/                 # 组件
│   ├── hooks/                      # 钩子函数
│   ├── layouts/                    # 布局
│   ├── router/                     # 路由
│   ├── store/                      # 状态管理
│   ├── styles/                     # 样式
│   ├── utils/                      # 工具函数
│   └── views/                      # 页面
```

### 后端开发规范

#### 接口设计规范
1. RESTful API设计
```csharp
// 资源命名：使用名词复数形式
GET    /api/users           // 获取用户列表
GET    /api/users/{id}      // 获取单个用户
POST   /api/users           // 创建用户
PUT    /api/users/{id}      // 更新用户
DELETE /api/users/{id}      // 删除用户

// 查询参数
GET /api/users?pageSize=10&pageIndex=1&keyword=admin

// 关联资源
GET /api/users/{id}/roles   // 获取用户角色
```

2. 返回结果规范
```csharp
// 统一返回格式
public class ApiResult<T>
{
    public int Code { get; set; }         // 状态码
    public string Message { get; set; }    // 消息
    public T Data { get; set; }           // 数据
    public bool Success { get; set; }      // 是否成功
}

// 分页返回格式
public class PageResult<T>
{
    public int Total { get; set; }        // 总记录数
    public int PageSize { get; set; }     // 每页大小
    public int PageIndex { get; set; }    // 当前页码
    public List<T> Items { get; set; }    // 数据列表
}
```

3. 状态码规范
```csharp
// HTTP状态码
200 OK                // 成功
201 Created          // 创建成功
204 No Content       // 删除成功
400 Bad Request      // 请求错误
401 Unauthorized     // 未授权
403 Forbidden        // 禁止访问
404 Not Found        // 资源不存在
500 Server Error     // 服务器错误

// 业务状态码
public enum ApiCode
{
    Success = 200,            // 成功
    Unauthorized = 401,       // 未授权
    ParameterError = 400,     // 参数错误
    BusinessError = 500,      // 业务错误
    SystemError = 599         // 系统错误
}
```

#### 异常处理规范
1. 异常分类
```csharp
// 基础异常类
public class BusinessException : Exception
{
    public int Code { get; }
    public BusinessException(string message, int code = 500) 
        : base(message)
    {
        Code = code;
    }
}

// 业务异常
public class UserNotFoundException : BusinessException
{
    public UserNotFoundException(string username) 
        : base($"用户 {username} 不存在", 404)
    {
    }
}

// 权限异常
public class UnauthorizedException : BusinessException
{
    public UnauthorizedException(string message = "未授权") 
        : base(message, 401)
    {
    }
}
```

2. 异常处理中间件
```csharp
public class GlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionMiddleware> _logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (BusinessException ex)
        {
            _logger.LogWarning(ex, "业务异常");
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "系统异常");
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        var response = new ApiResult<object>
        {
            Success = false,
            Code = ex is BusinessException bex ? bex.Code : 500,
            Message = ex.Message
        };
        await context.Response.WriteAsJsonAsync(response);
    }
}
```

3. 异常处理最佳实践
```csharp
public class UserService
{
    // 1. 参数验证
    public async Task<UserDto> CreateUser(CreateUserDto dto)
    {
        if (string.IsNullOrEmpty(dto.Username))
            throw new BusinessException("用户名不能为空");

        if (await _userRepository.ExistsAsync(u => u.Username == dto.Username))
            throw new BusinessException("用户名已存在");

        // 处理业务逻辑
    }

    // 2. 业务异常
    public async Task DeleteUser(long id)
    {
        var user = await _userRepository.GetByIdAsync(id)
            ?? throw new UserNotFoundException(id.ToString());

        if (user.IsSystem)
            throw new BusinessException("系统用户不能删除");

        // 处理删除逻辑
    }

    // 3. 权限验证
    public async Task<UserDto> UpdateUser(UpdateUserDto dto)
    {
        if (!await _permissionChecker.HasPermissionAsync("user.update"))
            throw new UnauthorizedException();

        // 处理更新逻辑
    }
}
```

4. 日志记录规范
```csharp
public class UserService
{
    private readonly ILogger<UserService> _logger;

    public async Task<UserDto> CreateUser(CreateUserDto dto)
    {
        try
        {
            _logger.LogInformation("开始创建用户: {@Dto}", dto);
            
            var user = await _userRepository.CreateAsync(dto);
            
            _logger.LogInformation("用户创建成功: {@User}", user);
            return user;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "创建用户失败: {@Dto}", dto);
            throw;
        }
    }
}
```

#### 数据库设计规范

1. 表设计规范
```sql
-- 基础字段
id              BIGINT        PRIMARY KEY IDENTITY(1,1), -- 主键
tenant_id       BIGINT        NOT NULL,                  -- 租户ID
create_time     DATETIME      NOT NULL,                  -- 创建时间
create_by       BIGINT        NOT NULL,                  -- 创建人
update_time     DATETIME      NULL,                      -- 更新时间
update_by       BIGINT        NULL,                      -- 更新人
delete_time     DATETIME      NULL,                      -- 删除时间
delete_by       BIGINT        NULL,                      -- 删除人
is_deleted      INT           NOT NULL DEFAULT 0         -- 删除标记
```

2. 命名规范
```sql
-- 表命名：lean_模块名_表名，全部小写，下划线分隔
lean_sys_user           -- 系统用户表
lean_sys_role          -- 系统角色表
lean_sys_menu          -- 系统菜单表

-- 字段命名：小写，下划线分隔
user_name              -- 用户名
create_time            -- 创建时间
is_deleted            -- 删除标记

-- 索引命名
idx_表名_字段名        -- 普通索引
udx_表名_字段名        -- 唯一索引
```

3. 字段类型规范
```sql
-- 字符串类型
name            VARCHAR(50)     -- 一般文本
description     NVARCHAR(500)   -- 包含中文的文本
content         TEXT           -- 长文本

-- 数值类型
id              BIGINT         -- 主键、外键
age             INT            -- 一般数值
status          TINYINT        -- 状态标记
amount          DECIMAL(18,5)  -- 金额
rate            DECIMAL(18,5)  -- 比例

-- 日期时间类型
create_time     DATETIME       -- 创建时间
expire_date     DATE           -- 日期
```

4. 索引规范
```sql
-- 主键索引
PRIMARY KEY (id)

-- 唯一索引
CREATE UNIQUE INDEX udx_user_username ON lean_sys_user (username)

-- 普通索引
CREATE INDEX idx_user_status ON lean_sys_user (status)

-- 组合索引
CREATE INDEX idx_user_name_status ON lean_sys_user (name, status)
```

5. SqlSugar实体映射
```csharp
[SugarTable("lean_sys_user", "系统用户表")]
public class LeanUser : LeanEntity
{
    /// <summary>
    /// 用户名
    /// </summary>
    [SugarColumn(ColumnName = "user_name", ColumnDescription = "用户名", 
        Length = 50, IsNullable = false, ColumnDataType = "nvarchar")]
    public string UserName { get; set; }

    /// <summary>
    /// 密码
    /// </summary>
    [SugarColumn(ColumnName = "password", ColumnDescription = "密码", 
        Length = 100, IsNullable = false, ColumnDataType = "varchar")]
    public string Password { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [SugarColumn(ColumnName = "status", ColumnDescription = "状态", 
        IsNullable = false, DefaultValue = "1")]
    public int Status { get; set; }
}
```

#### 缓存使用规范

1. 缓存键命名规范
```csharp
// 缓存键前缀
public static class CacheKeys
{
    // 模块名:实体名:操作:参数
    public const string UserInfo = "sys:user:info:{0}";        // 用户信息
    public const string UserToken = "sys:user:token:{0}";      // 用户令牌
    public const string RolePermissions = "sys:role:perms:{0}"; // 角色权限
}

// 使用示例
string cacheKey = string.Format(CacheKeys.UserInfo, userId);
```

2. 缓存接口定义
```csharp
public interface ICacheService
{
    // 获取缓存
    Task<T> GetAsync<T>(string key);
    
    // 设置缓存
    Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
    
    // 删除缓存
    Task RemoveAsync(string key);
    
    // 批量删除缓存
    Task RemoveByPrefixAsync(string prefix);
    
    // 设置缓存过期时间
    Task SetExpiryAsync(string key, TimeSpan expiry);
}
```

3. Redis缓存实现
```csharp
public class RedisCacheService : ICacheService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<RedisCacheService> _logger;

    public async Task<T> GetAsync<T>(string key)
    {
        try
        {
            var db = _redis.GetDatabase();
            var value = await db.StringGetAsync(key);
            return value.HasValue 
                ? JsonSerializer.Deserialize<T>(value) 
                : default;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Redis获取缓存失败: {Key}", key);
            return default;
        }
    }

    public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
    {
        try
        {
            var db = _redis.GetDatabase();
            var json = JsonSerializer.Serialize(value);
            await db.StringSetAsync(key, json, expiry);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Redis设置缓存失败: {Key}", key);
            throw;
        }
    }
}
```

4. 缓存最佳实践
```csharp
public class UserService
{
    private readonly ICacheService _cache;
    private readonly IUserRepository _userRepository;

    // 1. 缓存读取
    public async Task<UserDto> GetUserAsync(long id)
    {
        var cacheKey = string.Format(CacheKeys.UserInfo, id);
        
        // 先从缓存读取
        var user = await _cache.GetAsync<UserDto>(cacheKey);
        if (user != null)
            return user;

        // 缓存不存在，从数据库读取
        user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            return null;

        // 写入缓存
        await _cache.SetAsync(cacheKey, user, TimeSpan.FromHours(1));
        return user;
    }

    // 2. 缓存更新
    public async Task UpdateUserAsync(UserDto dto)
    {
        // 更新数据库
        await _userRepository.UpdateAsync(dto);

        // 更新缓存
        var cacheKey = string.Format(CacheKeys.UserInfo, dto.Id);
        await _cache.SetAsync(cacheKey, dto, TimeSpan.FromHours(1));
    }

    // 3. 缓存删除
    public async Task DeleteUserAsync(long id)
    {
        // 删除数据库
        await _userRepository.DeleteAsync(id);

        // 删除缓存
        var cacheKey = string.Format(CacheKeys.UserInfo, id);
        await _cache.RemoveAsync(cacheKey);
    }
}
```

5. 缓存注意事项
```csharp
public class CacheService
{
    // 1. 防止缓存穿透
    public async Task<UserDto> GetUser(long id)
    {
        var cacheKey = string.Format(CacheKeys.UserInfo, id);
        
        // 使用空对象缓存
        var user = await _cache.GetAsync<UserDto>(cacheKey);
        if (user != null)
            return user.Id == 0 ? null : user;

        user = await _userRepository.GetByIdAsync(id);
        
        // 即使为null也缓存，但过期时间较短
        await _cache.SetAsync(cacheKey, user ?? new UserDto(), 
            user == null ? TimeSpan.FromMinutes(5) : TimeSpan.FromHours(1));
            
        return user;
    }

    // 2. 防止缓存雪崩
    public async Task<UserDto> GetUserWithSlidingExpiry(long id)
    {
        var cacheKey = string.Format(CacheKeys.UserInfo, id);
        
        var user = await _cache.GetAsync<UserDto>(cacheKey);
        if (user != null)
        {
            // 滑动过期
            await _cache.SetExpiryAsync(cacheKey, TimeSpan.FromHours(1));
            return user;
        }

        // 随机过期时间，避免同时过期
        var random = new Random();
        var expiry = TimeSpan.FromMinutes(55 + random.Next(10));
        
        user = await _userRepository.GetByIdAsync(id);
        await _cache.SetAsync(cacheKey, user, expiry);
        
        return user;
    }

    // 3. 防止缓存击穿
    private static readonly SemaphoreSlim _lock = new(1, 1);
    
    public async Task<UserDto> GetUserWithLock(long id)
    {
        var cacheKey = string.Format(CacheKeys.UserInfo, id);
        
        var user = await _cache.GetAsync<UserDto>(cacheKey);
        if (user != null)
            return user;

        try
        {
            await _lock.WaitAsync();
            
            // 双重检查
            user = await _cache.GetAsync<UserDto>(cacheKey);
            if (user != null)
                return user;

            user = await _userRepository.GetByIdAsync(id);
            await _cache.SetAsync(cacheKey, user, TimeSpan.FromHours(1));
            
            return user;
        }
        finally
        {
            _lock.Release();
        }
    }
}
```

### 前端开发规范

#### 组件开发规范

1. 组件命名
```typescript
// 组件名使用 PascalCase
export default defineComponent({
  name: 'UserList'  // 必须与文件名匹配
})

// 基础组件使用 Base 前缀
export default defineComponent({
  name: 'BaseButton'
})

// 单例组件使用 The 前缀
export default defineComponent({
  name: 'TheHeader'
})
```

2. 组件文件结构
```vue
<script setup lang="ts">
// 类型导入
import type { UserInfo } from '@/types'

// 组件导入
import { BaseTable, BaseForm } from '@/components'

// 属性定义
interface Props {
  userId: number
  mode?: 'view' | 'edit'
}
const props = withDefaults(defineProps<Props>(), {
  mode: 'view'
})

// 事件定义
const emit = defineEmits<{
  (e: 'save', user: UserInfo): void
  (e: 'cancel'): void
}>()

// 响应式数据
const loading = ref(false)
const userInfo = ref<UserInfo>()

// 生命周期
onMounted(async () => {
  await loadData()
})

// 方法定义
async function loadData() {
  loading.value = true
  try {
    userInfo.value = await getUserById(props.userId)
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <div class="user-detail">
    <a-card :loading="loading">
      <BaseForm
        v-if="userInfo"
        v-model:value="userInfo"
        :readonly="props.mode === 'view'"
        @save="$emit('save', $event)"
        @cancel="$emit('cancel')"
      />
    </a-card>
  </div>
</template>

<style lang="less" scoped>
.user-detail {
  @apply p-6 bg-white rounded-lg;
}
</style>

3. 组件通信规范
```typescript
// Props 定义
interface Props {
  // 必填属性
  userId: number
  // 可选属性，带默认值
  mode?: 'view' | 'edit'
  // 回调函数
  onSave?: (user: UserInfo) => void
}

// Emits 定义
const emit = defineEmits<{
  // 带参数的事件
  (e: 'save', user: UserInfo): void
  // 无参数的事件
  (e: 'cancel'): void
}>()

// 事件处理
const handleSave = (user: UserInfo) => {
  emit('save', user)
}
```

#### 状态管理规范

1. Store 定义
```typescript
// store/modules/user.ts
import { defineStore } from 'pinia'
import type { UserInfo } from '@/types'

export const useUserStore = defineStore('user', {
  // 状态定义
  state: () => ({
    userInfo: null as UserInfo | null,
    permissions: [] as string[],
    token: '',
  }),

  // 计算属性
  getters: {
    isAdmin(): boolean {
      return this.permissions.includes('admin')
    },
    hasPermission: (state) => {
      return (permission: string) => state.permissions.includes(permission)
    }
  },

  // 异步操作
  actions: {
    async login(username: string, password: string) {
      try {
        const { token, user } = await userApi.login({ username, password })
        this.setToken(token)
        this.setUserInfo(user)
        return true
      } catch (error) {
        return false
      }
    },

    async logout() {
      this.clearUserInfo()
      this.clearToken()
    },
  },

  // 持久化配置
  persist: {
    key: 'user-store',
    paths: ['token']
  }
})
```

2. Store 使用规范
```typescript
// 组件中使用
const userStore = useUserStore()

// 读取状态
const userInfo = computed(() => userStore.userInfo)

// 修改状态
userStore.$patch({
  userInfo: {
    ...userStore.userInfo,
    name: 'New Name'
  }
})

// 调用 action
await userStore.login(username, password)

// 批量修改
userStore.$patch((state) => {
  state.userInfo = null
  state.permissions = []
})
```

#### 路由管理规范

1. 路由配置
```typescript
// router/index.ts
import type { RouteRecordRaw } from 'vue-router'

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [
      {
        path: 'dashboard',
        name: 'Dashboard',
        component: () => import('@/views/dashboard/index.vue'),
        meta: {
          title: '仪表盘',
          icon: 'dashboard',
          affix: true
        }
      }
    ]
  },
  {
    path: '/user',
    component: Layout,
    meta: {
      title: '用户管理',
      icon: 'user',
      permissions: ['user.view']
    },
    children: [
      {
        path: 'list',
        name: 'UserList',
        component: () => import('@/views/user/list.vue'),
        meta: {
          title: '用户列表',
          keepAlive: true
        }
      }
    ]
  }
]
```

2. 路由守卫
```typescript
// router/guards.ts
import type { Router } from 'vue-router'

export function setupRouterGuards(router: Router) {
  // 权限守卫
  router.beforeEach(async (to, from, next) => {
    const userStore = useUserStore()
    
    // 检查token
    if (!userStore.token && to.name !== 'Login') {
      next({ name: 'Login' })
      return
    }

    // 检查权限
    if (to.meta?.permissions) {
      const hasPermission = userStore.hasPermissions(to.meta.permissions)
      if (!hasPermission) {
        next({ name: '403' })
        return
      }
    }

    next()
  })

  // 进度条
  router.beforeEach(() => {
    NProgress.start()
  })

  router.afterEach(() => {
    NProgress.done()
  })
}
```

3. 路由跳转
```typescript
// 声明式导航
<router-link :to="{ name: 'UserDetail', params: { id: 1 } }">
  用户详情
</router-link>

// 编程式导航
const router = useRouter()
const route = useRoute()

// 带参数跳转
router.push({
  name: 'UserDetail',
  params: { id: 1 },
  query: { tab: 'info' }
})

// 返回上一页
router.back()

// 替换当前页面
router.replace({
  name: 'UserList'
})
```

#### UI/UX设计规范

1. 布局规范
```vue
<!-- 页面布局 -->
<template>
  <div class="page-container">
    <!-- 页头 -->
    <div class="page-header">
      <h1 class="page-title">{{ title }}</h1>
      <div class="page-actions">
        <a-button type="primary" @click="handleAdd">新增</a-button>
      </div>
    </div>

    <!-- 搜索区域 -->
    <div class="search-bar">
      <a-form layout="inline">
        <!-- 搜索表单项 -->
      </a-form>
    </div>

    <!-- 内容区域 -->
    <div class="page-content">
      <a-table :columns="columns" :data-source="dataSource">
        <!-- 表格内容 -->
      </a-table>
    </div>

    <!-- 页脚 -->
    <div class="page-footer">
      <!-- 分页等 -->
    </div>
  </div>
</template>

<style lang="less" scoped>
.page-container {
  @apply flex flex-col min-h-full p-6 bg-white;

  .page-header {
    @apply flex justify-between items-center mb-6;
  }

  .page-title {
    @apply text-xl font-bold;
  }

  .search-bar {
    @apply mb-6;
  }

  .page-content {
    @apply flex-1;
  }

  .page-footer {
    @apply mt-6;
  }
}
</style>
```

2. 响应式设计
```vue
<template>
  <a-row :gutter="{ xs: 8, sm: 16, md: 24 }">
    <a-col :xs="24" :sm="12" :md="8" :lg="6">
      <a-card>内容1</a-card>
    </a-col>
  </a-row>
</template>

<style lang="less" scoped>
.responsive-container {
  @apply w-full;

  @screen sm {
    @apply max-w-screen-sm;
  }

  @screen md {
    @apply max-w-screen-md;
  }

  @screen lg {
    @apply max-w-screen-lg;
  }
}
</style>
```

3. 主题定制
```typescript
// 定制 Ant Design Vue 主题
export default {
  theme: {
    token: {
      colorPrimary: '#1890ff',
      borderRadius: 4,
      colorSuccess: '#52c41a',
      colorWarning: '#faad14',
      colorError: '#f5222d',
    }
  }
}

// 使用CSS变量
:root {
  --primary-color: #1890ff;
  --success-color: #52c41a;
  --warning-color: #faad14;
  --error-color: #f5222d;
}

.custom-component {
  color: var(--primary-color);
}
```

#### 性能优化规范

1. 代码分割
```typescript
// 路由懒加载
const routes = [
  {
    path: '/user',
    component: () => import('@/views/user/index.vue')
  }
]

// 组件懒加载
const MyComponent = defineAsyncComponent(() =>
  import('@/components/MyComponent.vue')
)
```

2. 虚拟列表
```vue
<template>
  <VirtualList
    :data-key="'id'"
    :data-source="list"
    :item-height="54"
    :container-height="400"
  >
    <template #item="{ item }">
      <div class="list-item">
        {{ item.name }}
      </div>
    </template>
  </VirtualList>
</template>
```

3. 图片优化
```vue
<template>
  <!-- 懒加载图片 -->
  <img
    v-lazy="imageUrl"
    :alt="description"
    class="lazy-image"
  >

  <!-- 使用webp格式 -->
  <picture>
    <source :srcset="imageWebp" type="image/webp">
    <img :src="imageJpg" :alt="description">
  </picture>
</template>
```

4. 缓存优化
```typescript
// 页面缓存
<keep-alive :include="['UserList', 'UserDetail']">
  <router-view />
</keep-alive>

// API缓存
const useUserList = createFetch({
  baseUrl: '/api',
  options: {
    immediate: true,
    refetch: false,
    cache: true,
    cacheTime: 1000 * 60 * 5 // 5分钟
  }
})
```

#### 安全规范

1. XSS防护
```typescript
// 使用 v-html 时注意转义
import { escapeHtml } from '@/utils/security'

const html = ref(escapeHtml(rawHtml))

<div v-html="html"></div>
```

2. CSRF防护
```typescript
// 请求拦截器添加token
axios.interceptors.request.use(config => {
  const token = useUserStore().token
  if (token) {
    config.headers['X-CSRF-TOKEN'] = token
  }
  return config
})
```

3. 敏感信息处理
```typescript
// 密码加密
import { encrypt } from '@/utils/crypto'

const handleLogin = async () => {
  const encryptedPassword = encrypt(password.value)
  await login(username.value, encryptedPassword)
}

// 本地存储
const storage = {
  set(key: string, value: any) {
    if (typeof value === 'object') {
      value = JSON.stringify(value)
    }
    localStorage.setItem(key, value)
  },
  get(key: string) {
    const value = localStorage.getItem(key)
    try {
      return JSON.parse(value!)
    } catch {
      return value
    }
  },
  remove(key: string) {
    localStorage.removeItem(key)
  }
}
```

### 工程化规范

#### CI/CD规范

1. 持续集成流程
```yaml
# .github/workflows/ci.yml
name: CI

on:
  push:
    branches: [ main, develop ]
  pull_request:
    branches: [ main, develop ]

jobs:
  build:
    runs-on: ubuntu-latest
    
    steps:
    - uses: actions/checkout@v3
    
    # 后端构建
    - name: Setup .NET
      uses: actions/setup-dotnet@v3
      with:
        dotnet-version: 8.0.x
        
    - name: Restore dependencies
      run: dotnet restore
      
    - name: Build
      run: dotnet build --no-restore
      
    - name: Test
      run: dotnet test --no-build --verbosity normal
      
    # 前端构建
    - name: Setup Node.js
      uses: actions/setup-node@v3
      with:
        node-version: '18'
        
    - name: Install dependencies
      run: pnpm install
      
    - name: Lint
      run: pnpm lint
      
    - name: Build
      run: pnpm build
```

2. 持续部署流程
```yaml
# .github/workflows/cd.yml
name: CD

on:
  push:
    tags:
      - 'v*'

jobs:
  deploy:
    runs-on: ubuntu-latest
    
    steps:
    # 构建Docker镜像
    - name: Build and push Docker images
      uses: docker/build-push-action@v4
      with:
        context: .
        push: true
        tags: |
          lean365/lean.cus:latest
          lean365/lean.cus:${{ github.ref_name }}
          
    # 部署到服务器
    - name: Deploy to server
      uses: appleboy/ssh-action@master
      with:
        host: ${{ secrets.SERVER_HOST }}
        username: ${{ secrets.SERVER_USERNAME }}
        key: ${{ secrets.SERVER_KEY }}
        script: |
          cd /app/lean.cus
          docker-compose pull
          docker-compose up -d
```

#### 环境管理规范

1. 环境分类
```bash
# 环境类型
development  # 开发环境
test         # 测试环境
staging      # 预发布环境
production   # 生产环境
```

2. 环境配置
```json
// appsettings.json
{
  "Env": "development",
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=lean_cus;Uid=root;Pwd=123456;"
  },
  "Redis": {
    "ConnectionString": "localhost:6379",
    "DatabaseId": 0
  },
  "Jwt": {
    "SecretKey": "your-secret-key",
    "Issuer": "lean.cus",
    "Audience": "lean.cus",
    "ExpireMinutes": 120
  }
}

// .env
VITE_ENV=development
VITE_API_URL=http://localhost:5000
VITE_UPLOAD_URL=http://localhost:5000/upload
```

3. 环境隔离
```typescript
// 前端环境隔离
const config = {
  development: {
    apiUrl: 'http://localhost:5000',
    uploadUrl: 'http://localhost:5000/upload'
  },
  test: {
    apiUrl: 'http://test-api.lean365.com',
    uploadUrl: 'http://test-api.lean365.com/upload'
  },
  production: {
    apiUrl: 'https://api.lean365.com',
    uploadUrl: 'https://api.lean365.com/upload'
  }
}

export default config[import.meta.env.VITE_ENV]
```

#### 版本发布规范

1. 版本号规范
```bash
# 版本号格式：主版本号.次版本号.修订号
1.0.0   # 初始版本
1.1.0   # 新功能版本
1.1.1   # 问题修复版本
2.0.0   # 重大更新版本
```

2. 发布流程
```bash
# 1. 创建发布分支
git checkout -b release/v1.1.0 develop

# 2. 更新版本号
修改 package.json 和 Directory.Build.props 中的版本号

# 3. 更新更新日志
更新 CHANGELOG.md

# 4. 提交更改
git add .
git commit -m "chore(release): v1.1.0"

# 5. 合并到主分支
git checkout main
git merge --no-ff release/v1.1.0

# 6. 打标签
git tag -a v1.1.0 -m "version 1.1.0"

# 7. 推送到远程
git push origin main
git push origin v1.1.0

# 8. 合并回开发分支
git checkout develop
git merge --no-ff release/v1.1.0

# 9. 删除发布分支
git branch -d release/v1.1.0
```

3. 更新日志规范
```markdown
# 更新日志

## [1.1.0] - 2024-01-15

### 新增
- 用户管理模块
- 角色权限管理
- 数据字典功能

### 优化
- 提升查询性能
- 优化页面加载速度
- 改进用户体验

### 修复
- 修复登录失败问题
- 修复数据导出错误
- 修复IE浏览器兼容性问题
```

#### 监控告警规范

1. 日志监控
```csharp
// 日志配置
{
  "Serilog": {
    "MinimumLevel": {
      "Default": "Information",
      "Override": {
        "Microsoft": "Warning",
        "System": "Warning"
      }
    },
    "WriteTo": [
      {
        "Name": "File",
        "Args": {
          "path": "logs/log-.txt",
          "rollingInterval": "Day"
        }
      },
      {
        "Name": "Elasticsearch",
        "Args": {
          "nodeUris": "http://localhost:9200",
          "indexFormat": "lean-cus-logs-{0:yyyy.MM}"
        }
      }
    ]
  }
}

// 日志记录
public class LogService
{
    private readonly ILogger<LogService> _logger;

    public void LogOperation(string operation, string userId, string details)
    {
        _logger.LogInformation(
            "操作日志 - 操作:{Operation} 用户:{UserId} 详情:{Details}",
            operation, userId, details);
    }

    public void LogError(Exception ex, string context)
    {
        _logger.LogError(
            ex,
            "错误日志 - 上下文:{Context} 消息:{Message}",
            context, ex.Message);
    }
}
```

2. 性能监控
```csharp
// 性能计数器
public class PerformanceMonitor
{
    private readonly ILogger<PerformanceMonitor> _logger;
    private readonly Metrics _metrics;

    public void RecordDuration(string operation, long milliseconds)
    {
        _metrics.Measure.Histogram.Update(
            "operation_duration",
            milliseconds,
            new MetricTags("operation", operation)
        );
    }

    public void RecordError(string operation)
    {
        _metrics.Measure.Counter.Increment(
            "operation_errors",
            new MetricTags("operation", operation)
        );
    }
}

// 使用示例
public async Task<UserDto> GetUserAsync(long id)
{
    var sw = Stopwatch.StartNew();
    try
    {
        var user = await _userRepository.GetByIdAsync(id);
        _monitor.RecordDuration("get_user", sw.ElapsedMilliseconds);
        return user;
    }
    catch (Exception)
    {
        _monitor.RecordError("get_user");
        throw;
    }
}
```

3. 告警配置
```yaml
# prometheus告警规则
groups:
- name: lean-cus-alerts
  rules:
  - alert: HighErrorRate
    expr: rate(operation_errors_total[5m]) > 0.1
    for: 5m
    labels:
      severity: critical
    annotations:
      summary: "错误率过高"
      description: "最近5分钟错误率超过10%"

  - alert: SlowResponses
    expr: histogram_quantile(0.95, rate(operation_duration_bucket[5m])) > 1000
    for: 5m
    labels:
      severity: warning
    annotations:
      summary: "响应时间过长"
      description: "95%请求响应时间超过1秒"

  - alert: HighCPUUsage
    expr: rate(process_cpu_seconds_total[5m]) * 100 > 80
    for: 5m
    labels:
      severity: warning
    annotations:
      summary: "CPU使用率过高"
      description: "CPU使用率超过80%"
```

4. 监控面板
```typescript
// 前端监控面板
const MonitorDashboard = defineComponent({
  setup() {
    const metrics = ref({
      errorRate: 0,
      responseTime: 0,
      cpuUsage: 0,
      memoryUsage: 0
    })

    // 获取监控数据
    const fetchMetrics = async () => {
      const data = await monitorApi.getMetrics()
      metrics.value = data
    }

    // 定时刷新
    onMounted(() => {
      fetchMetrics()
      setInterval(fetchMetrics, 60000)
    })

    return () => (
      <div class="monitor-dashboard">
        <a-row gutter={16}>
          <a-col span={6}>
            <a-card>
              <statistic
                title="错误率"
                value={metrics.value.errorRate}
                suffix="%"
                valueStyle={metrics.value.errorRate > 10 ? { color: '#cf1322' } : {}}
              />
            </a-card>
          </a-col>
          <a-col span={6}>
            <a-card>
              <statistic
                title="响应时间"
                value={metrics.value.responseTime}
                suffix="ms"
                valueStyle={metrics.value.responseTime > 1000 ? { color: '#cf1322' } : {}}
              />
            </a-card>
          </a-col>
          <a-col span={6}>
            <a-card>
              <statistic
                title="CPU使用率"
                value={metrics.value.cpuUsage}
                suffix="%"
                valueStyle={metrics.value.cpuUsage > 80 ? { color: '#cf1322' } : {}}
              />
            </a-card>
          </a-col>
          <a-col span={6}>
            <a-card>
              <statistic
                title="内存使用率"
                value={metrics.value.memoryUsage}
                suffix="%"
                valueStyle={metrics.value.memoryUsage > 80 ? { color: '#cf1322' } : {}}
              />
            </a-card>
          </a-col>
        </a-row>
      </div>
    )
  }
})
```

// ... 继续添加其他规范 ... 
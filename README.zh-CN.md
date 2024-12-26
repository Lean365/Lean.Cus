# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

一个使用 SqlSugar ORM 进行高效数据库操作的现代 .NET Core 项目。

## 🚀 特性

- **.NET Core 后端**
  - 清晰的架构设计
  - SqlSugar ORM 集成
  - Redis 缓存
  - JWT 身份认证
  - 基于角色的授权
  - 日志系统
  - API 文档

- **前端选项**
  - Ant Design Vue UI
  - Windows Forms 客户端

## 📋 环境要求

- .NET 6.0 SDK 或更高版本
- SQL Server/MySQL/PostgreSQL
- Redis（可选，用于缓存）
- Node.js（用于 Ant Design Vue 前端）

## 🛠️ 安装步骤

1. 克隆仓库
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. 在 `Backend/src/Lean.Cus.Api/appsettings.json` 中配置数据库连接

3. 运行后端
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. 运行 Ant Design Vue 前端（可选）
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ 项目结构

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # Web API 层
│       ├── Lean.Cus.Application   # 应用层
│       ├── Lean.Cus.Domain        # 领域层
│       ├── Lean.Cus.Infrastructure# 基础设施层
│       └── Lean.Cus.Common        # 公共组件
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # Vue.js 前端
        └── Lean.Cus.WinForm      # Windows Forms 客户端
```

## 🔧 核心组件

- **SqlSugar ORM**: 支持多数据库的高性能 ORM
- **Redis 缓存**: 分布式缓存实现
- **JWT 认证**: 安全的 API 访问
- **日志系统**: 全面的日志记录
- **API 文档**: Swagger/OpenAPI 集成

## 🔐 安全特性

- JWT 令牌认证
- 基于角色的访问控制
- 数据权限过滤
- SQL 注入防护
- XSS 防护
- CSRF 防护

## 📝 API 文档

在开发模式下，API 文档可在 `/swagger` 路径访问。

## 🤝 贡献

欢迎提交 Pull Request 来贡献代码！

## 📄 许可证

本项目采用 MIT 许可证 - 详见 [LICENSE](LICENSE) 文件。

## 📞 支持

如需支持，请在 GitHub 仓库中创建 Issue。 
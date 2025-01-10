# 数据库迁移

本目录用于存放数据库迁移相关的SQL脚本文件。

## 目录结构

```
Migrations/
├── MySQL/                 # MySQL数据库迁移脚本
├── SQLServer/            # SQL Server数据库迁移脚本
├── PostgreSQL/           # PostgreSQL数据库迁移脚本
└── SQLite/              # SQLite数据库迁移脚本
```

## 命名规范

迁移脚本文件命名规范：`V{版本号}_{描述}.sql`

例如：
- V1.0.0_InitDatabase.sql
- V1.0.1_AddUserTable.sql
- V1.0.2_AddUserIndexes.sql

## 注意事项

1. 每个迁移脚本都应该是幂等的，即可以重复执行而不会产生错误
2. 所有迁移脚本都应该有回滚脚本
3. 迁移脚本应该包含完整的注释
4. 建议在测试环境验证迁移脚本后再应用到生产环境 
# SqlSugar 数据库连接字符串示例

本文档提供了 SqlSugar ORM 支持的所有数据库类型及其连接字符串示例。

## 基础数据库

### MySql (DbType = 0)
```
Server=localhost;Port=3306;Database=lean_cus;Uid=root;Pwd=123456;CharSet=utf8mb4;
```

### SqlServer (DbType = 1)
```
Server=.;Database=Lean.Cus;User ID=sa;Password=123456;TrustServerCertificate=true;
```

### SQLite (DbType = 2)
```
Data Source=./lean_cus.db
```

### Oracle (DbType = 3)
```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));User Id=system;Password=123456;
```

### PostgreSQL (DbType = 4)
```
Host=localhost;Port=5432;Database=lean_cus;Username=postgres;Password=123456;
```

## 国��数据库

### 达梦数据库 (DbType = 5)
```
Server=localhost;Port=5236;User Id=SYSDBA;Password=123456;Database=lean_cus;
```

### 人大金仓数据库 (DbType = 6)
```
Server=localhost;Port=54321;Database=lean_cus;User Id=SYSTEM;Password=123456;
```

### 神通数据库 (DbType = 7)
```
Server=localhost;Port=2003;Database=lean_cus;User Id=SYSDBA;Password=123456;
```

### MySql官方驱动 (DbType = 8)
```
Server=localhost;Port=3306;Database=lean_cus;Uid=root;Pwd=123456;CharSet=utf8mb4;
```

### Microsoft Access (DbType = 9)
```
Provider=Microsoft.ACE.OLEDB.12.0;Data Source=lean_cus.accdb;Persist Security Info=False;
```

## 云数据库

### 华为OpenGauss (DbType = 10)
```
Server=localhost;Port=5432;Database=lean_cus;Username=gaussdb;Password=123456;
```

### QuestDB时序数据库 (DbType = 11)
```
Host=localhost;Port=8812;Database=lean_cus;Username=admin;Password=quest;
```

### 瀚高数据库 (DbType = 12)
```
Server=localhost;Port=5866;Database=lean_cus;Username=SYSDBA;Password=123456;
```

### ClickHouse列式数据库 (DbType = 13)
```
Host=localhost;Port=8123;Database=lean_cus;Username=default;Password=;
```

### 南大通用数据库 (DbType = 14)
```
Server=localhost;Port=5258;Database=lean_cus;User Id=SYSDBA;Password=123456;
```

### ODBC数据源连接 (DbType = 15)
```
Driver={SQL Server};Server=localhost;Database=lean_cus;Uid=sa;Pwd=123456;
```

### OceanBase Oracle模式 (DbType = 16)
```
Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=localhost)(PORT=2883))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=lean_cus)));User Id=root;Password=123456;
```

### 涛思时序数据库 (DbType = 17)
```
url=http://localhost:6041/rest/sql;token=123456;
```

### 华为GaussDB (DbType = 18)
```
Server=localhost;Port=25308;Database=lean_cus;Username=gaussdb;Password=123456;
```

### OceanBase MySQL模式 (DbType = 19)
```
Server=localhost;Port=2881;User Id=root@sys;Password=123456;Database=lean_cus;
```

### TiDB分布式数据库 (DbType = 20)
```
Server=localhost;Port=4000;Database=lean_cus;User=root;Password=123456;
```

### 海量数据库 (DbType = 21)
```
Server=localhost;Port=5432;Database=lean_cus;Username=vastbase;Password=123456;
```

### 阿里云PolarDB (DbType = 22)
```
Server=localhost;Port=3306;Database=lean_cus;User Id=root;Password=123456;
```

### Apache Doris (DbType = 23)
```
Host=localhost;Port=9030;Database=lean_cus;Username=root;Password=123456;
```

### 虚谷数据库 (DbType = 24)
```
Server=localhost;Port=5138;Database=lean_cus;User Id=SYSDBA;Password=123456;
```

### 金仓数据库 (DbType = 25)
```
Server=localhost;Port=5433;Database=lean_cus;Username=SYSDBA;Password=123456;
```

### 腾讯云TDSQL PostgreSQL版 (DbType = 26)
```
Server=localhost;Port=5432;Database=lean_cus;Username=postgres;Password=123456;
```

### 腾讯云TDSQL (DbType = 27)
```
Server=localhost;Port=3306;Database=lean_cus;User Id=root;Password=123456;
```

### SAP HANA (DbType = 28)
```
Server=localhost:30015;UID=SYSTEM;PWD=123456;Current Schema=lean_cus;
```

### 自定义数据库 (DbType = 900)
```
// 根据实际数据库类型配置连接字符串
```

## 注意事项

1. 以上连接字符串仅为示例，实际使用时需要根据您的环境配置正确的：
   - 服务器地址（Server/Host）
   - 端口号（Port）
   - 数据库名（Database）
   - 用户名（User ID/Username/Uid）
   - 密码（Password/Pwd）
   - 其他特定参数

2. 某些数据库可能需要额外的连接参数，请参考具体数据库的官方文档。

3. 在生产环境中，建议：
   - 使用环境变量或配置文件管理连接字符串
   - 不要在代码中硬编码连接字符串
   - 妥善保管数据库凭据
   - 使用最小权限原则配置数据库用户
   - 启用 SSL/TLS 加密（如果数据库支持） 
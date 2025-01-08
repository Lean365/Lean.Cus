# 部署指南

## 目录
- [环境要求](#环境要求)
- [安装步骤](#安装步骤)
- [配置说明](#配置说明)
- [部署方案](#部署方案)
- [运维管理](#运维管理)

## 环境要求
### 服务器要求
- CPU: 4核+
- 内存: 8GB+
- 硬盘: 100GB+
- 操作系统: Windows Server 2019/Ubuntu 20.04+

### 软件要求
- .NET 8.0 Runtime
- Node.js 18+
- Nginx 1.20+
- Redis 6.0+
- SQL Server/MySQL/PostgreSQL

## 安装步骤
### 1. 安装运行环境
```bash
# Ubuntu 环境
# 安装 .NET Runtime
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt update
sudo apt install -y dotnet-runtime-8.0

# 安装 Node.js
curl -fsSL https://deb.nodesource.com/setup_18.x | sudo -E bash -
sudo apt install -y nodejs

# 安装 Nginx
sudo apt install -y nginx

# 安装 Redis
sudo apt install -y redis-server
```

### 2. 部署后端
```bash
# 发布后端应用
dotnet publish -c Release -o ./publish

# 配置系统服务
sudo nano /etc/systemd/system/lean-cus-api.service

[Unit]
Description=Lean.Cus API Service
After=network.target

[Service]
WorkingDirectory=/var/www/lean-cus/api
ExecStart=/usr/bin/dotnet Lean.Cus.Api.dll
Restart=always
RestartSec=10
User=www-data
Environment=ASPNETCORE_ENVIRONMENT=Production

[Install]
WantedBy=multi-user.target

# 启动服务
sudo systemctl enable lean-cus-api
sudo systemctl start lean-cus-api
```

### 3. 部署前端
```bash
# 构建前端应用
pnpm build

# 配置 Nginx
sudo nano /etc/nginx/sites-available/lean-cus

server {
    listen 80;
    server_name example.com;

    # 前端静态文件
    location / {
        root /var/www/lean-cus/web;
        try_files $uri $uri/ /index.html;
    }

    # API 反向代理
    location /api {
        proxy_pass http://localhost:5000;
        proxy_http_version 1.1;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection keep-alive;
        proxy_set_header Host $host;
        proxy_cache_bypass $http_upgrade;
    }
}

# 启用站点
sudo ln -s /etc/nginx/sites-available/lean-cus /etc/nginx/sites-enabled/
sudo nginx -t
sudo systemctl restart nginx
```

## 配置说明
### 数据库配置
```json
{
  "ConnectionStrings": {
    "Default": "Server=localhost;Database=LeanCus;User Id=sa;Password=yourpassword;"
  }
}
```

### Redis配置
```json
{
  "Redis": {
    "ConnectionString": "localhost:6379",
    "InstanceName": "LeanCus:"
  }
}
```

### JWT配置
```json
{
  "JWT": {
    "Secret": "your-secret-key",
    "Issuer": "lean-cus",
    "Audience": "lean-cus",
    "ExpireMinutes": 1440
  }
}
```

## 部署方案
### 单机部署
![单机部署](../images/deploy-single.png)

### 集群部署
![集群部署](../images/deploy-cluster.png)

## 运维管理
### 日志管理
- 使用 NLog 记录日志
- 日志文件按日期分割
- 定期清理过期日志

### 监控告警
- 使用 Prometheus 监控
- 配置 Grafana 仪表板
- 设置关键指标告警

### 备份策略
- 数据库每日备份
- 定期备份配置文件
- 异地备份重要数据

### 安全加固
- 启用 HTTPS
- 配置防火墙
- 定期更新补丁
- 加密敏感信息 
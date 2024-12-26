# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

一個使用 SqlSugar ORM 進行高效資料庫操作的現代 .NET Core 專案。

## 🚀 特性

- **.NET Core 後端**
  - 清晰的架構設計
  - SqlSugar ORM 整合
  - Redis 快取
  - JWT 身份驗證
  - 基於角色的授權
  - 日誌系統
  - API 文件

- **前端選項**
  - Ant Design Vue UI
  - Windows Forms 用戶端

## 📋 環境要求

- .NET 6.0 SDK 或更高版本
- SQL Server/MySQL/PostgreSQL
- Redis（可選，用於快取）
- Node.js（用於 Ant Design Vue 前端）

## 🛠️ 安裝步驟

1. 克隆儲存庫
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. 在 `Backend/src/Lean.Cus.Api/appsettings.json` 中配置資料庫連接

3. 執行後端
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. 執行 Ant Design Vue 前端（可選）
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ 專案結構

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # Web API 層
│       ├── Lean.Cus.Application   # 應用層
│       ├── Lean.Cus.Domain        # 領域層
│       ├── Lean.Cus.Infrastructure# 基礎設施層
│       └── Lean.Cus.Common        # 公共組件
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # Vue.js 前端
        └── Lean.Cus.WinForm      # Windows Forms 用戶端
```

## 🔧 核心組件

- **SqlSugar ORM**: 支援多資料庫的高效能 ORM
- **Redis 快取**: 分散式快取實現
- **JWT 驗證**: 安全的 API 存取
- **日誌系統**: 全面的日誌記錄
- **API 文件**: Swagger/OpenAPI 整合

## 🔐 安全特性

- JWT 令牌驗證
- 基於角色的存取控制
- 資料權限過濾
- SQL 注入防護
- XSS 防護
- CSRF 防護

## 📝 API 文件

在開發模式下，API 文件可在 `/swagger` 路徑存取。

## 🤝 貢獻

歡迎提交 Pull Request 來貢獻程式碼！

## 📄 授權條款

本專案採用 MIT 授權條款 - 詳見 [LICENSE](LICENSE) 檔案。

## 📞 支援

如需支援，請在 GitHub 儲存庫中建立 Issue。

## ⚠️ 警告說明

這是一個由 Cursor 生成的系統。使用者需要具備 .NET 和 Vue.js 相關技術知識才能有效地使用此專案。程式碼庫要求理解：
- .NET Core 開發和架構
- Vue.js 框架和 Ant Design
- SqlSugar ORM 資料庫操作
- 前後端整合 
# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

SqlSugar ORMを使用した効率的なデータベース操作を実現する最新の.NET Coreプロジェクト。

## 🚀 特徴

- **.NET Core バックエンド**
  - クリーンアーキテクチャ
  - SqlSugar ORM 統合
  - Redis キャッシング
  - JWT 認証
  - ロールベースの認可
  - ロギングシステム
  - API ドキュメント

- **フロントエンドオプション**
  - Ant Design Vue UI
  - Windows Forms クライアント

## 📋 必要条件

- .NET 6.0 SDK 以降
- SQL Server/MySQL/PostgreSQL
- Redis（オプション、キャッシング用）
- Node.js（Ant Design Vue フロントエンド用）

## 🛠️ インストール

1. リポジトリのクローン
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. `Backend/src/Lean.Cus.Api/appsettings.json` でデータベース接続を設定

3. バックエンドの実行
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. Ant Design Vue フロントエンドの実行（オプション）
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ プロジェクト構造

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # Web API レイヤー
│       ├── Lean.Cus.Application   # アプリケーションレイヤー
│       ├── Lean.Cus.Domain        # ドメインレイヤー
│       ├── Lean.Cus.Infrastructure# インフラストラクチャレイヤー
│       └── Lean.Cus.Common        # 共通コンポーネント
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # Vue.js フロントエンド
        └── Lean.Cus.WinForm      # Windows Forms クライアント
```

## 🔧 主要コンポーネント

- **SqlSugar ORM**: 複数のデータベースをサポートする高性能ORM
- **Redis キャッシュ**: 分散キャッシュの実装
- **JWT 認証**: セキュアなAPI アクセス
- **ロギング**: 包括的なログシステム
- **API ドキュメント**: Swagger/OpenAPI 統合

## 🔐 セキュリティ機能

- JWT トークン認証
- ロールベースのアクセス制御
- データ権限フィルタリング
- SQLインジェクション防止
- XSS 防止
- CSRF 防止

## 📝 API ドキュメント

開発モードでは、API ドキュメントは `/swagger` で利用可能です。

## 🤝 貢献

プルリクエストは大歓迎です！

## 📄 ライセンス

このプロジェクトは MIT ライセンスの下で公開されています - 詳細は [LICENSE](LICENSE) ファイルを参照してください。

## 📞 サポート

サポートが必要な場合は、GitHub リポジトリでイシューを作成してください。 
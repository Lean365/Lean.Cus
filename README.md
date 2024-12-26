# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

A modern .NET Core project using SqlSugar ORM for efficient database operations.

## 🚀 Features

- **.NET Core Backend**
  - Clean Architecture
  - SqlSugar ORM Integration
  - Redis Caching
  - JWT Authentication
  - Role-based Authorization
  - Logging System
  - API Documentation

- **Frontend Options**
  - Ant Design Vue UI
  - Windows Forms Client

## 📋 Prerequisites

- .NET 6.0 SDK or later
- SQL Server/MySQL/PostgreSQL
- Redis (optional, for caching)
- Node.js (for Ant Design Vue frontend)

## 🛠️ Installation

1. Clone the repository
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. Configure the database connection in `Backend/src/Lean.Cus.Api/appsettings.json`

3. Run the backend
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. Run the Ant Design Vue frontend (optional)
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ Project Structure

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # Web API Layer
│       ├── Lean.Cus.Application   # Application Layer
│       ├── Lean.Cus.Domain        # Domain Layer
│       ├── Lean.Cus.Infrastructure# Infrastructure Layer
│       └── Lean.Cus.Common        # Shared Components
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # Vue.js Frontend
        └── Lean.Cus.WinForm      # Windows Forms Client
```

## 🔧 Key Components

- **SqlSugar ORM**: High-performance ORM with multiple database support
- **Redis Cache**: Distributed caching implementation
- **JWT Authentication**: Secure API access
- **Logging**: Comprehensive logging system
- **API Documentation**: Swagger/OpenAPI integration

## 🔐 Security Features

- JWT Token Authentication
- Role-Based Access Control
- Data Permission Filtering
- SQL Injection Prevention
- XSS Protection
- CSRF Protection

## 📝 API Documentation

API documentation is available at `/swagger` when running in development mode.

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

For support, please open an issue in the GitHub repository. 
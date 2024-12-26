# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

Un proyecto moderno de .NET Core que utiliza SqlSugar ORM para operaciones eficientes de base de datos.

## 🚀 Características

- **Backend .NET Core**
  - Arquitectura limpia
  - Integración con SqlSugar ORM
  - Caché Redis
  - Autenticación JWT
  - Autorización basada en roles
  - Sistema de registro
  - Documentación API

- **Opciones de Frontend**
  - Interfaz de usuario Ant Design Vue
  - Cliente Windows Forms

## 📋 Requisitos Previos

- SDK .NET 6.0 o superior
- SQL Server/MySQL/PostgreSQL
- Redis (opcional, para caché)
- Node.js (para frontend Ant Design Vue)

## 🛠️ Instalación

1. Clonar el repositorio
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. Configurar la conexión a la base de datos en `Backend/src/Lean.Cus.Api/appsettings.json`

3. Ejecutar el backend
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. Ejecutar el frontend Ant Design Vue (opcional)
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ Estructura del Proyecto

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # Capa API Web
│       ├── Lean.Cus.Application   # Capa de Aplicación
│       ├── Lean.Cus.Domain        # Capa de Dominio
│       ├── Lean.Cus.Infrastructure# Capa de Infraestructura
│       └── Lean.Cus.Common        # Componentes Comunes
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # Frontend Vue.js
        └── Lean.Cus.WinForm      # Cliente Windows Forms
```

## 🔧 Componentes Principales

- **SqlSugar ORM**: ORM de alto rendimiento con soporte para múltiples bases de datos
- **Caché Redis**: Implementación de caché distribuida
- **Autenticación JWT**: Acceso seguro a API
- **Sistema de Registro**: Sistema completo de registro
- **Documentación API**: Integración Swagger/OpenAPI

## 🔐 Características de Seguridad

- Autenticación por token JWT
- Control de acceso basado en roles
- Filtrado de permisos de datos
- Prevención de inyección SQL
- Protección XSS
- Protección CSRF

## 📝 Documentaci��n API

La documentación API está disponible en `/swagger` en modo desarrollo.

## 🤝 Contribución

¡Las contribuciones son bienvenidas! No dude en enviar un Pull Request.

## 📄 Licencia

Este proyecto está bajo la licencia MIT - ver el archivo [LICENSE](LICENSE) para más detalles.

## 📞 Soporte

Para soporte, por favor cree un issue en el repositorio de GitHub.

## ⚠️ Advertencia

Este es un sistema generado por Cursor. Los usuarios deben tener conocimientos de tecnologías .NET y Vue.js para trabajar eficazmente con este proyecto. La base de código requiere comprensión de:
- Desarrollo y arquitectura de .NET Core
- Framework Vue.js y Ant Design
- Operaciones de base de datos con SqlSugar ORM
- Integración frontend-backend 
# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

Современный проект на .NET Core, использующий SqlSugar ORM для эффективной работы с базами данных.

## 🚀 Возможности

- **Бэкенд на .NET Core**
  - Чистая архитектура
  - Интеграция с SqlSugar ORM
  - Кэширование Redis
  - JWT аутентификация
  - Ролевая авторизация
  - Система логирования
  - Документация API

- **Варианты фронтенда**
  - Пользовательский интерфейс Ant Design Vue
  - Клиент Windows Forms

## 📋 Требования

- SDK .NET 6.0 или выше
- SQL Server/MySQL/PostgreSQL
- Redis (опционально, для кэширования)
- Node.js (для фронтенда Ant Design Vue)

## 🛠️ Установка

1. Клонировать репозиторий
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. Настроить подключение к базе данных в `Backend/src/Lean.Cus.Api/appsettings.json`

3. Запустить бэкенд
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. Запустить фронтенд Ant Design Vue (опционально)
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ Структура проекта

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # Web API слой
│       ├── Lean.Cus.Application   # Слой приложения
│       ├── Lean.Cus.Domain        # Доменный слой
│       ├── Lean.Cus.Infrastructure# Инфраструктурный слой
│       └── Lean.Cus.Common        # Общие компоненты
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # Фронтенд Vue.js
        └── Lean.Cus.WinForm      # Клиент Windows Forms
```

## 🔧 Основные компоненты

- **SqlSugar ORM**: Высокопроизводительный ORM с поддержкой множества баз данных
- **Redis кэш**: Реализация распределенного кэширования
- **JWT аутентификация**: Безопасный доступ к API
- **Система логирования**: Комплексная система логирования
- **Документация API**: Интеграция Swagger/OpenAPI

## 🔐 Функции безопасности

- JWT токен аутентификация
- Ролевой контроль доступа
- Фильтрация прав доступа к данным
- Защита от SQL-инъекций
- Защита от XSS
- Защита от CSRF

## 📝 Документация API

Документация API доступна по пути `/swagger` в режиме разработки.

## 🤝 Участие в разработке

Мы приветствуем вклад в развитие проекта! Не стесняйтесь отправлять Pull Request.

## 📄 Лицензия

Этот проект распространяется под лицензией MIT - подробности см. в файле [LICENSE](LICENSE).

## 📞 Поддержка

Для получения поддержки создайте issue в репозитории GitHub.

## ⚠️ Предупреждение

Эта система сгенерирована Cursor. Пользователям необходимо знание технологий .NET и Vue.js для эффективной работы с этим проектом. Кодовая база требует понимания:
- Разработка и архитектура .NET Core
- Фреймворк Vue.js и Ant Design
- Операции с базой данных через SqlSugar ORM
- Интеграция фронтенда и бэкенда 
# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

Ein modernes .NET Core-Projekt, das SqlSugar ORM für effiziente Datenbankoperationen verwendet.

## 🚀 Funktionen

- **.NET Core Backend**
  - Saubere Architektur
  - SqlSugar ORM Integration
  - Redis Caching
  - JWT Authentifizierung
  - Rollenbasierte Autorisierung
  - Logging-System
  - API-Dokumentation

- **Frontend-Optionen**
  - Ant Design Vue Benutzeroberfläche
  - Windows Forms Client

## 📋 Voraussetzungen

- .NET 6.0 SDK oder höher
- SQL Server/MySQL/PostgreSQL
- Redis (optional, für Caching)
- Node.js (für Ant Design Vue Frontend)

## 🛠️ Installation

1. Repository klonen
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. Datenbank-Verbindung in `Backend/src/Lean.Cus.Api/appsettings.json` konfigurieren

3. Backend starten
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. Ant Design Vue Frontend starten (optional)
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ Projektstruktur

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # Web API Schicht
│       ├── Lean.Cus.Application   # Anwendungsschicht
│       ├── Lean.Cus.Domain        # Domänenschicht
│       ├── Lean.Cus.Infrastructure# Infrastrukturschicht
│       └── Lean.Cus.Common        # Gemeinsame Komponenten
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # Vue.js Frontend
        └── Lean.Cus.WinForm      # Windows Forms Client
```

## 🔧 Hauptkomponenten

- **SqlSugar ORM**: Hochleistungs-ORM mit Multi-Datenbank-Unterstützung
- **Redis Cache**: Implementierung von verteiltem Caching
- **JWT Authentifizierung**: Sicherer API-Zugriff
- **Logging-System**: Umfassendes Logging-System
- **API-Dokumentation**: Swagger/OpenAPI Integration

## 🔐 Sicherheitsfunktionen

- JWT Token Authentifizierung
- Rollenbasierte Zugriffssteuerung
- Datenberechtigungsfilterung
- SQL-Injection-Schutz
- XSS-Schutz
- CSRF-Schutz

## 📝 API-Dokumentation

Die API-Dokumentation ist im Entwicklungsmodus unter `/swagger` verfügbar.

## 🤝 Mitwirken

Beiträge sind willkommen! Fühlen Sie sich frei, einen Pull Request einzureichen.

## 📄 Lizenz

Dieses Projekt steht unter der MIT-Lizenz - siehe [LICENSE](LICENSE) Datei für Details.

## 📞 Support

Für Support erstellen Sie bitte ein Issue im GitHub-Repository. 
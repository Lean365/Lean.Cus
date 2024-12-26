# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

Un projet moderne .NET Core utilisant SqlSugar ORM pour des opérations efficaces sur les bases de données.

## 🚀 Fonctionnalités

- **Backend .NET Core**
  - Architecture propre
  - Intégration SqlSugar ORM
  - Cache Redis
  - Authentification JWT
  - Autorisation basée sur les rôles
  - Système de journalisation
  - Documentation API

- **Options Frontend**
  - Interface utilisateur Ant Design Vue
  - Client Windows Forms

## 📋 Prérequis

- SDK .NET 6.0 ou supérieur
- SQL Server/MySQL/PostgreSQL
- Redis (optionnel, pour le cache)
- Node.js (pour le frontend Ant Design Vue)

## 🛠️ Installation

1. Cloner le dépôt
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. Configurer la connexion à la base de données dans `Backend/src/Lean.Cus.Api/appsettings.json`

3. Lancer le backend
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. Lancer le frontend Ant Design Vue (optionnel)
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ Structure du Projet

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # Couche API Web
│       ├── Lean.Cus.Application   # Couche Application
│       ├── Lean.Cus.Domain        # Couche Domaine
│       ├── Lean.Cus.Infrastructure# Couche Infrastructure
│       └── Lean.Cus.Common        # Composants Communs
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # Frontend Vue.js
        └── Lean.Cus.WinForm      # Client Windows Forms
```

## 🔧 Composants Principaux

- **SqlSugar ORM**: ORM haute performance avec support multi-bases de données
- **Cache Redis**: Implémentation de cache distribué
- **Authentification JWT**: Accès API sécurisé
- **Système de Journalisation**: Système complet de logs
- **Documentation API**: Intégration Swagger/OpenAPI

## 🔐 Fonctionnalités de Sécurité

- Authentification par token JWT
- Contrôle d'accès basé sur les rôles
- Filtrage des permissions de données
- Protection contre l'injection SQL
- Protection XSS
- Protection CSRF

## 📝 Documentation API

La documentation API est disponible sur `/swagger` en mode développement.

## 🤝 Contribution

Les contributions sont les bienvenues ! N'hésitez pas à soumettre une Pull Request.

## 📄 Licence

Ce projet est sous licence MIT - voir le fichier [LICENSE](LICENSE) pour plus de détails.

## 📞 Support

Pour le support, veuillez créer une issue dans le dépôt GitHub.

## ⚠️ Avertissement

Ceci est un système généré par Cursor. Les utilisateurs doivent avoir des connaissances des technologies .NET et Vue.js pour travailler efficacement avec ce projet. La base de code nécessite une compréhension de :
- Développement et architecture .NET Core
- Framework Vue.js et Ant Design
- Opérations de base de données avec SqlSugar ORM
- Intégration frontend-backend 
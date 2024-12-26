# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

Um projeto moderno em .NET Core que utiliza SqlSugar ORM para operações eficientes de banco de dados.

## 🚀 Recursos

- **Backend .NET Core**
  - Arquitetura limpa
  - Integração com SqlSugar ORM
  - Cache Redis
  - Autenticação JWT
  - Autorização baseada em funções
  - Sistema de registro
  - Documentação da API

- **Opções de Frontend**
  - Interface de usuário Ant Design Vue
  - Cliente Windows Forms

## 📋 Pré-requisitos

- SDK .NET 6.0 ou superior
- SQL Server/MySQL/PostgreSQL
- Redis (opcional, para cache)
- Node.js (para frontend Ant Design Vue)

## 🛠️ Instalação

1. Clonar o repositório
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. Configurar a conexão com o banco de dados em `Backend/src/Lean.Cus.Api/appsettings.json`

3. Executar o backend
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. Executar o frontend Ant Design Vue (opcional)
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ Estrutura do Projeto

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # Camada API Web
│       ├── Lean.Cus.Application   # Camada de Aplicação
│       ├── Lean.Cus.Domain        # Camada de Domínio
│       ├── Lean.Cus.Infrastructure# Camada de Infraestrutura
│       └── Lean.Cus.Common        # Componentes Comuns
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # Frontend Vue.js
        └── Lean.Cus.WinForm      # Cliente Windows Forms
```

## 🔧 Componentes Principais

- **SqlSugar ORM**: ORM de alto desempenho com suporte a múltiplos bancos de dados
- **Cache Redis**: Implementação de cache distribuído
- **Autenticação JWT**: Acesso seguro à API
- **Sistema de Registro**: Sistema completo de registro
- **Documentação API**: Integração Swagger/OpenAPI

## 🔐 Recursos de Segurança

- Autenticação por token JWT
- Controle de acesso baseado em funções
- Filtragem de permissões de dados
- Prevenção contra injeção SQL
- Proteção XSS
- Proteção CSRF

## 📝 Documentação da API

A documentação da API está disponível em `/swagger` no modo de desenvolvimento.

## 🤝 Contribuição

Contribuições são bem-vindas! Sinta-se à vontade para enviar um Pull Request.

## 📄 Licença

Este projeto está sob a licença MIT - veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 📞 Suporte

Para suporte, por favor crie uma issue no repositório GitHub.

## ⚠️ Aviso

Este é um sistema gerado pelo Cursor. Os usuários devem ter conhecimento das tecnologias .NET e Vue.js para trabalhar efetivamente com este projeto. A base de código requer compreensão de:
- Desenvolvimento e arquitetura .NET Core
- Framework Vue.js e Ant Design
- Operações de banco de dados com SqlSugar ORM
- Integração frontend-backend 
# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

효율적인 데이터베이스 작업을 위해 SqlSugar ORM을 사용하는 현대적인 .NET Core 프로젝트입니다.

## 🚀 특징

- **.NET Core 백엔드**
  - 클린 아키텍처
  - SqlSugar ORM 통합
  - Redis 캐싱
  - JWT 인증
  - 역할 기반 권한 부여
  - 로깅 시스템
  - API 문서화

- **프론트엔드 옵션**
  - Ant Design Vue UI
  - Windows Forms 클라이언트

## 📋 필수 조건

- .NET 6.0 SDK 이상
- SQL Server/MySQL/PostgreSQL
- Redis (선택사항, 캐싱용)
- Node.js (Ant Design Vue 프론트엔드용)

## 🛠️ 설치

1. 저장소 복제
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. `Backend/src/Lean.Cus.Api/appsettings.json`에서 데이터베이스 연결 구성

3. 백엔드 실행
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. Ant Design Vue 프론트엔드 실행 (선택사항)
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ 프로젝트 구조

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # Web API 계층
│       ├── Lean.Cus.Application   # 애플리케이션 계층
│       ├── Lean.Cus.Domain        # 도메인 계층
│       ├── Lean.Cus.Infrastructure# 인프라 계층
│       └── Lean.Cus.Common        # 공통 구성요소
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # Vue.js 프론트엔드
        └── Lean.Cus.WinForm      # Windows Forms 클라이언트
```

## 🔧 주요 구성요소

- **SqlSugar ORM**: 다중 데이터베이스를 지원하는 고성능 ORM
- **Redis 캐시**: 분산 캐싱 구현
- **JWT 인증**: 안전한 API 접근
- **로깅 시스템**: 포괄적인 로깅 시스템
- **API 문서화**: Swagger/OpenAPI 통합

## 🔐 보안 기능

- JWT 토큰 인증
- 역할 기반 접근 제어
- 데이터 권한 필터링
- SQL 인젝션 방지
- XSS 보호
- CSRF 보호

## 📝 API 문서

개발 모드에서 API 문서는 `/swagger`에서 확인할 수 있습니다.

## 🤝 기여

기여는 언제나 환영합니다! 자유롭게 Pull Request를 제출해 주세요.

## 📄 라이선스

이 프로젝트는 MIT 라이선스를 따릅니다 - 자세한 내용은 [LICENSE](LICENSE) 파일을 참조하세요.

## 📞 지원

지원이 필요한 경우 GitHub 저장소에서 이슈를 생성해 주세요. 

## ⚠️ 경고

이 시스템은 Cursor에 의해 생성되었습니다. 이 프로젝트를 효과적으로 사용하기 위해서는 .NET과 Vue.js 관련 기술 지식이 필요합니다. 코드베이스는 다음에 대한 이해가 필요합니다:
- .NET Core 개발 및 아키텍처
- Vue.js 프레임워크 및 Ant Design
- SqlSugar ORM 데이터베이스 작업
- 프론트엔드-백엔드 통합 
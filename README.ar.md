# Lean.Cus

[English](README.md) | [简体中文](README.zh-CN.md) | [繁體中文](README.zh-TW.md) | [日本語](README.ja.md) | [한국어](README.ko.md) | [Español](README.es.md) | [Português](README.pt.md) | [Русский](README.ru.md) | [العربية](README.ar.md) | [Français](README.fr.md) | [Deutsch](README.de.md)

<div dir="rtl">

مشروع حديث بإطار .NET Core يستخدم SqlSugar ORM لعمليات قواعد البيانات الفعالة.

## 🚀 المميزات

- **الواجهة الخلفية .NET Core**
  - هندسة نظيفة
  - تكامل مع SqlSugar ORM
  - تخزين مؤقت Redis
  - مصادقة JWT
  - تفويض قائم على الأدوار
  - نظام التسجيل
  - توثيق API

- **خيارات الواجهة الأمامية**
  - واجهة مستخدم Ant Design Vue
  - تطبيق Windows Forms

## 📋 المتطلبات الأساسية

- SDK .NET 6.0 أو أحدث
- SQL Server/MySQL/PostgreSQL
- Redis (اختياري، للتخزين المؤقت)
- Node.js (لواجهة Ant Design Vue)

## 🛠️ التثبيت

1. استنساخ المستودع
```bash
git clone https://github.com/Lean365/Lean.Cus.git
cd Lean.Cus
```

2. تكوين اتصال قاعدة البيانات في `Backend/src/Lean.Cus.Api/appsettings.json`

3. تشغيل الواجهة الخلفية
```bash
cd Backend/src/Lean.Cus.Api
dotnet run
```

4. تشغيل واجهة Ant Design Vue (اختياري)
```bash
cd Frontend/src/Lean.Cus.Antd
npm install
npm run dev
```

## 🏗️ هيكل المشروع

```
Lean.Cus/
├── Backend/
│   └── src/
│       ├── Lean.Cus.Api           # طبقة API
│       ├── Lean.Cus.Application   # طبقة التطبيق
│       ├── Lean.Cus.Domain        # طبقة المجال
│       ├── Lean.Cus.Infrastructure# طبقة البنية التحتية
│       └── Lean.Cus.Common        # المكونات المشتركة
└── Frontend/
    └── src/
        ├── Lean.Cus.Antd         # واجهة Vue.js
        └── Lean.Cus.WinForm      # تطبيق Windows Forms
```

## 🔧 المكونات الرئيسية

- **SqlSugar ORM**: ORM عالي الأداء مع دعم قواعد بيانات متعددة
- **تخزين Redis**: تنفيذ التخزين المؤقت الموزع
- **مصادقة JWT**: وصول آمن إلى API
- **نظام التسجيل**: نظام تسجيل شامل
- **توثيق API**: توثيق Swagger/OpenAPI

## 🔐 ميزات الأمان

- مصادقة رمز JWT
- التحكم في الوصول القائم على الأدوار
- تصفية أذونات البيانات
- الح��اية من حقن SQL
- حماية XSS
- حماية CSRF

## 📝 توثيق API

وثائق API متاحة على `/swagger` في وضع التطوير.

## 🤝 المساهمة

المساهمات مرحب بها! لا تتردد في إرسال طلب سحب.

## 📄 الترخيص

هذا المشروع مرخص تحت رخصة MIT - راجع ملف [LICENSE](LICENSE) للتفاصيل.

## 📞 الدعم

للدعم، يرجى إنشاء مشكلة في مستودع GitHub.

## ⚠️ تحذير

هذا النظام تم إنشاؤه بواسطة Cursor. يجب أن يكون لدى المستخدمين معرفة بتقنيات .NET و Vue.js للعمل بشكل فعال مع هذا المشروع. تتطلب قاعدة الكود فهم:
- تطوير وهندسة .NET Core
- إطار عمل Vue.js و Ant Design
- عمليات قواعد البيانات مع SqlSugar ORM
- تكامل الواجهة الأمامية والخلفية

</div> 
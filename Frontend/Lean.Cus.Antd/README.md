# Lean.Cus 前端项目

基于 Vue 3 + TypeScript + Vite + Ant Design Vue 的前端项目。

## 技术栈

- Vue 3 - 渐进式 JavaScript 框架
- TypeScript - JavaScript 的超集
- Vite - 下一代前端构建工具
- Ant Design Vue - 企业级 UI 组件库
- Vue Router - 官方路由管理器
- Pinia - Vue 的状态管理库
- Vue I18n - 国际化解决方案
- Axios - HTTP 客户端
- ECharts - 数据可视化图表库
- SignalR - 实时通信
- CryptoJS - 加密库
- VueUse - Vue Composition API 工具集
- Less - CSS 预处理器

## 特性

- 🚀 基于 Vue 3 和 TypeScript 开发
- 📦 Vite 构建，快速的冷启动和热重载
- 🎨 使用 Ant Design Vue 组件库，支持主题定制
- 🌐 内置国际化支持
- 📊 集成 ECharts 图表库
- 🔐 使用 CryptoJS 进行数据加密
- 🔄 实时通信支持（SignalR）
- 📱 响应式设计，支持移动端
- 🎯 TypeScript 类型检查
- 🔍 ESLint + Prettier 代码规范
- 📝 详细的文档和注释

## 开发环境要求

- Node.js >= 16
- npm >= 7 或 yarn >= 1.22

## 安装和运行

1. 安装依赖：

```bash
npm install
# 或
yarn install
```

2. 启动开发服务器：

```bash
npm run dev
# 或
yarn dev
```

3. 构建生产版本：

```bash
npm run build
# 或
yarn build
```

## 项目结构

```
├── public/              # 静态资源
├── src/                 # 源代码
│   ├── api/            # API 接口
│   ├── assets/         # 资源文件
│   ├── components/     # 公共组件
│   ├── composables/    # 组合式函数
│   ├── layouts/        # 布局组件
│   ├── locales/        # 国际化文件
│   ├── router/         # 路由配置
│   ├── stores/         # 状态管理
│   ├── styles/         # 样式文件
│   ├── utils/          # 工具函数
│   ├── views/          # 页面组件
│   ├── App.vue         # 根组件
│   └── main.ts         # 入口文件
├── .env                # 环境变量
├── .env.development    # 开发环境变量
├── .env.production     # 生产环境变量
├── .eslintrc.json      # ESLint 配置
├── .prettierrc         # Prettier 配置
├── index.html          # HTML 模板
├── package.json        # 项目配置
├── tsconfig.json       # TypeScript 配置
└── vite.config.ts      # Vite 配置
```

## 开发规范

- 遵循 Vue 3 组合式 API 风格
- 使用 TypeScript 编写代码
- 遵循 ESLint 和 Prettier 代码规范
- 组件和文件使用 PascalCase 命名
- 使用 Composition API 和 `<script setup>` 语法
- 组件属性和事件使用 kebab-case 命名
- 使用 Ant Design Vue 组件和设计规范

## 国际化

项目使用 Vue I18n 实现国际化，支持中文和英文。语言文件位于 `src/locales` 目录。

## 主题定制

可以通过修改 `vite.config.ts` 中的 `less` 配置来自定义 Ant Design Vue 主题。

## 状态管理

使用 Pinia 进行状态管理，store 文件位于 `src/stores` 目录。

## API 请求

使用 Axios 进行 API 请求，支持请求拦截和响应拦截，可以进行统一的错误处理和认证处理。

## 实时通信

使用 SignalR 实现实时通信功能，支持服务器推送和客户端订阅。

## 安全性

- 使用 CryptoJS 进行数据加密
- 实现 JWT 认证
- XSS 防护
- CSRF 防护

## 部署

1. 构建生产版本：
```bash
npm run build
```

2. 将 `dist` 目录下的文件部署到 Web 服务器

## 贡献指南

1. Fork 项目
2. 创建特性分支
3. 提交代码
4. 创建 Pull Request

## 许可证

[MIT](LICENSE) 
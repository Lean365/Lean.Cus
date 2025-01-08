# 前端开发规范 (Lean.Cus.Antd)

## 目录
- [开发环境](#开发环境)
- [项目结构](#项目结构)
- [编码规范](#编码规范)
- [组件开发](#组件开发)
- [状态管理](#状态管理)
- [最佳实践](#最佳实践)

## 开发环境
- Node.js 18+
- pnpm
- Visual Studio Code
- Vite
- TypeScript

## 项目结构
```
Lean.Cus.Antd/
├── src/
│   ├── api/                # API 接口定义
│   │   ├── model/         # 接口数据模型
│   │   └── modules/       # 按模块划分的接口
│   │
│   ├── assets/            # 静态资源
│   │   ├── icons/        # 图标文件
│   │   ├── images/       # 图片资源
│   │   └── styles/       # 全局样式
│   │
│   ├── components/        # 公共组件
│   │   ├── Basic/        # 基础组件
│   │   ├── Form/         # 表单组件
│   │   ├── Table/        # 表格组件
│   │   └── Layout/       # 布局组件
│   │
│   ├── hooks/            # 自定义 Hooks
│   ├── router/           # 路由配置
│   ├── store/            # 状态管理
│   ├── utils/            # 工具函数
│   └── views/            # 页面组件
```

## 编码规范
### TypeScript 规范
```typescript
// 使用接口定义数据类型
interface UserInfo {
  id: number;
  name: string;
  role: UserRole;
}

// 使用枚举定义常量
enum UserRole {
  Admin = 'admin',
  User = 'user'
}

// 使用类型注解
function getUserInfo(id: number): Promise<UserInfo> {
  return http.get(`/api/users/${id}`);
}
```

### Vue 组件规范
```vue
<script setup lang="ts">
// 导入声明
import { ref, onMounted } from 'vue'
import type { UserInfo } from '@/types'

// Props 定义
const props = defineProps<{
  userId: number
}>()

// Emits 定义
const emit = defineEmits<{
  (e: 'update', value: UserInfo): void
}>()

// 响应式数据
const loading = ref(false)
const userInfo = ref<UserInfo>()

// 生命周期钩子
onMounted(async () => {
  await loadData()
})

// 方法定义
async function loadData() {
  loading.value = true
  try {
    userInfo.value = await getUserInfo(props.userId)
  } finally {
    loading.value = false
  }
}
</script>

<template>
  <div class="user-info">
    <a-spin :spinning="loading">
      <a-descriptions v-if="userInfo">
        <a-descriptions-item label="用户名">
          {{ userInfo.name }}
        </a-descriptions-item>
      </a-descriptions>
    </a-spin>
  </div>
</template>

<style lang="less" scoped>
.user-info {
  padding: 24px;
}
</style>
```

## 组件开发
### 组件设计原则
- 单一职责
- 可复用性
- 可测试性
- 松耦合

### Ant Design Vue 使用
- 遵循官方命名规范
- 使用官方提供的属性和事件
- 合理使用插槽
- 自定义主题

## 状态管理
### Pinia 使用规范
```typescript
// store/modules/user.ts
import { defineStore } from 'pinia'
import type { UserInfo } from '@/types'

export const useUserStore = defineStore('user', {
  state: () => ({
    userInfo: null as UserInfo | null,
    token: '',
  }),
  
  getters: {
    isLoggedIn(): boolean {
      return !!this.token
    },
  },
  
  actions: {
    async login(username: string, password: string) {
      // 登录逻辑
    },
    
    async logout() {
      // 登出逻辑
    },
  },
})
```

## 最佳实践
### 性能优化
- 组件懒加载
- 路由懒加载
- 图片优化
- 缓存策略

### 代码质量
- ESLint 检查
- TypeScript 类型检查
- 单元测试
- 代码评审 
import { createApp } from 'vue'
import { createPinia } from 'pinia'
import piniaPluginPersistedstate from 'pinia-plugin-persistedstate'
import { createI18n } from 'vue-i18n'
import Antd from 'ant-design-vue'

import App from './App.vue'
import router from './router'
import messages from './locales'

// 创建应用实例
const app = createApp(App)

// 使用 Antd
app.use(Antd)

// 使用 Pinia 状态管理
const pinia = createPinia()
pinia.use(piniaPluginPersistedstate)
app.use(pinia)

// 使用路由
app.use(router)

// 使用国际化
const i18n = createI18n({
  legacy: false,
  locale: localStorage.getItem('locale') || 'zh-CN',
  fallbackLocale: 'en-US',
  messages
})
app.use(i18n)

// 挂载应用
app.mount('#app') 
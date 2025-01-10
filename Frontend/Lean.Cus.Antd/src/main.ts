import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createI18n } from 'vue-i18n'

import App from './App.vue'
import router from './router'
import messages from './locales'

// 导入全局样式
import './assets/styles/index.less'

// 创建应用实例
const app = createApp(App)

// 使用 Pinia 状态管理
app.use(createPinia())

// 使用路由
app.use(router)

// 使用国际化
const i18n = createI18n({
  legacy: false,
  locale: 'zh-CN', // 默认语言
  fallbackLocale: 'en-US', // 备用语言
  messages: {
    'zh-CN': messages['zh-CN'],
    'zh-TW': messages['zh-TW'],
    'en-US': messages['en-US'],
    'ar-SA': messages['ar-SA'],
    'fr-FR': messages['fr-FR'],
    'ru-RU': messages['ru-RU'],
    'es-ES': messages['es-ES']
  }
})
app.use(i18n)

// 挂载应用
app.mount('#app') 
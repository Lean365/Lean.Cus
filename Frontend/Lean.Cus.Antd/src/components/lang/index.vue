<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.vue
 * 文件功能描述：语言切换组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：简化组件结构
 ------------------------------------------------------------------>

<template>
  <a-dropdown>
    <span class="lang-trigger">
      <span class="flag-icon" :class="currentLangFlag"></span>
      <span>{{ currentLangLabel }}</span>
    </span>
    <template #overlay>
      <a-menu @click="handleMenuClick">
        <a-menu-item v-for="lang in languages" :key="lang.value">
          <span class="flag-icon" :class="lang.flag"></span>
          <span class="lang-label">{{ lang.label }}</span>
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script lang="ts" setup>
import { computed } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import 'flag-icons/css/flag-icons.min.css'

// 支持的语言列表
const languages = [
  { label: '简体中文', value: 'zh-CN', flag: 'fi fi-cn' },
  { label: '繁體中文', value: 'zh-TW', flag: 'fi fi-tw' },
  { label: 'English', value: 'en-US', flag: 'fi fi-us' },
  { label: 'العربية', value: 'ar-SA', flag: 'fi fi-sa' },
  { label: 'Français', value: 'fr-FR', flag: 'fi fi-fr' },
  { label: 'Русский', value: 'ru-RU', flag: 'fi fi-ru' },
  { label: 'Español', value: 'es-ES', flag: 'fi fi-es' },
  { label: '日本語', value: 'ja-JP', flag: 'fi fi-jp' },
  { label: '한국어', value: 'ko-KR', flag: 'fi fi-kr' }
]

const { locale } = useI18n()

// 计算当前语言显示名称
const currentLangLabel = computed(() => {
  const lang = languages.find(item => item.value === locale.value)
  return lang ? lang.label : 'English'
})

// 计算当前语言的国旗图标
const currentLangFlag = computed(() => {
  const lang = languages.find(item => item.value === locale.value)
  return lang ? lang.flag : 'fi fi-us'
})

// 处理语言切换
const handleMenuClick = ({ key }: { key: string }) => {
  if (key === locale.value) return
  
  locale.value = key
  // 使用选中语言的提示文本
  const messageText = {
    'zh-CN': '切换语言成功',
    'zh-TW': '切換語言成功',
    'en-US': 'Language switched successfully',
    'ar-SA': 'تم تغيير اللغة بنجاح',
    'fr-FR': 'Langue changée avec succès',
    'ru-RU': 'Язык успешно изменен',
    'es-ES': 'Idioma cambiado con éxito',
    'ja-JP': '言語が正常に切り替わりました',
    'ko-KR': '언어가 성공적으로 변경되었습니다'
  }[key]
  
  message.success(messageText)
}
</script>

<style lang="less" scoped>
.lang-trigger {
  display: inline-flex;
  align-items: center;
  cursor: pointer;
  padding: 0 12px;
}

.flag-icon {
  margin-right: 8px;
  font-size: 1.2em;
}

.lang-label {
  margin-left: 8px;
}

/* 阿拉伯语时调整图标位置 */
:dir(rtl) {
  .flag-icon {
    margin-right: 0;
    margin-left: 8px;
  }

  .lang-label {
    margin-left: 0;
    margin-right: 8px;
  }
}
</style> 
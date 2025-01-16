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
    <div class="lang-switch">
      <span class="flag-icon" :class="currentLangFlag" />
    </div>
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

<script setup lang="ts">
import { computed } from 'vue'
import { TranslationOutlined } from '@ant-design/icons-vue'
import { useI18n } from 'vue-i18n'
import { message, theme } from 'ant-design-vue'
import type { MenuProps } from 'ant-design-vue'
import 'flag-icons/css/flag-icons.min.css'

const { token } = theme.useToken()
const { locale } = useI18n()

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

// 处理语言切换
const handleMenuClick: MenuProps['onClick'] = ({ key }) => {
  const langKey = key.toString()
  if (langKey === locale.value) return
  
  locale.value = langKey
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
  }[langKey]
  
  message.success(messageText)
}

const currentLangFlag = computed(() => {
  return languages.find(lang => lang.value === locale.value)?.flag
})
</script>

<style lang="less" scoped>
.lang-switch {
  display: flex;
  align-items: center;
  justify-content: center;
  width: 32px;
  height: 32px;
  border-radius: 6px;
  cursor: pointer;
  transition: all 0.3s ease;
  color: v-bind('token.colorText');
  
  &:hover {
    background: v-bind('token.colorBgTextHover');
  }
  
  :deep(svg) {
    font-size: 18px;
  }
}

.flag-icon {
  margin-right: 8px;
  font-size: 1.2em;
}

.lang-label {
  margin-left: 8px;
}
</style> 
<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.vue
 * 文件功能描述：主题切换组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：遵循Ant Design Vue主题规范
 ------------------------------------------------------------------>

<template>
  <a-dropdown>
    <span class="theme-trigger">
      <theme-outlined />
      <span>{{ currentThemeLabel }}</span>
    </span>
    <template #overlay>
      <a-menu @click="handleMenuClick">
        <a-menu-item v-for="theme in themes" :key="theme.value">
          <span class="theme-icon">
            <component :is="theme.icon" />
          </span>
          <span class="theme-label">{{ theme.label }}</span>
        </a-menu-item>
      </a-menu>
    </template>
  </a-dropdown>
</template>

<script lang="ts" setup>
import { computed, watch } from 'vue'
import { useStorage } from '@vueuse/core'
import { message, theme } from 'ant-design-vue'
import { ThemeOutlined, BulbOutlined, BulbFilled } from '@ant-design/icons-vue'
import type { ThemeConfig } from 'ant-design-vue/es/config-provider/context'

// 主题配置
const themes = [
  { 
    label: '亮色主题', 
    value: 'light',
    icon: BulbOutlined,
    config: {
      token: {
        colorPrimary: '#1677ff',
        borderRadius: 6,
      },
      algorithm: theme.defaultAlgorithm
    }
  },
  { 
    label: '暗色主题', 
    value: 'dark',
    icon: BulbFilled,
    config: {
      token: {
        colorPrimary: '#1677ff',
        borderRadius: 6,
      },
      algorithm: theme.darkAlgorithm
    }
  },
  { 
    label: '紧凑主题', 
    value: 'compact',
    icon: ThemeOutlined,
    config: {
      token: {
        colorPrimary: '#1677ff',
        borderRadius: 4,
        sizeStep: 2,
        controlHeight: 28
      },
      algorithm: theme.compactAlgorithm
    }
  }
]

// 使用 localStorage 存储主题设置
const themeMode = useStorage('theme-mode', 'light')

// 当前主题配置
const currentTheme = computed(() => {
  return themes.find(item => item.value === themeMode.value) || themes[0]
})

// 当前主题显示名称
const currentThemeLabel = computed(() => currentTheme.value.label)

// 主题配置
const themeConfig = computed<ThemeConfig>(() => currentTheme.value.config)

// 监听主题变化
watch(
  () => themeMode.value,
  () => {
    // 触发主题变更事件
    window.dispatchEvent(new CustomEvent('theme-change', { 
      detail: { theme: themeMode.value, config: themeConfig.value }
    }))
  },
  { immediate: true }
)

// 处理主题切换
const handleMenuClick = ({ key }: { key: string }) => {
  if (key === themeMode.value) return
  
  themeMode.value = key
  // 使用选中主题的提示文本
  const messageText = {
    'light': '已切换为亮色主题',
    'dark': '已切换为暗色主题',
    'compact': '已切换为紧凑主题'
  }[key]
  
  message.success(messageText)
}

// 导出主题配置
defineExpose({
  themeConfig
})
</script>

<style lang="less" scoped>
.theme-trigger {
  display: inline-flex;
  align-items: center;
  cursor: pointer;
  padding: 0 12px;

  .anticon {
    margin-right: 8px;
    font-size: 16px;
  }
}

.theme-icon {
  margin-right: 8px;
  
  .anticon {
    font-size: 14px;
  }
}

.theme-label {
  margin-left: 8px;
}
</style> 
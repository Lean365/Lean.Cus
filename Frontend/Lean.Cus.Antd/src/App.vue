<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：App.vue
 * 文件功能描述：应用程序根组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ------------------------------------------------------------------>

<template>
  <a-config-provider :locale="locale">
    <router-view v-slot="{ Component }">
      <component :is="Component" />
    </router-view>
  </a-config-provider>
</template>

<script lang="ts" setup>
import { ref, watch } from 'vue'
import { useI18n } from 'vue-i18n'
import zhCN from 'ant-design-vue/es/locale/zh_CN'
import enUS from 'ant-design-vue/es/locale/en_US'

const { locale: i18nLocale } = useI18n()
const locale = ref(zhCN)

// 监听语言变化
watch(
  () => i18nLocale.value,
  (val) => {
    locale.value = val === 'zh-CN' ? zhCN : enUS
  },
  { immediate: true }
)
</script>

<style>
#app {
  width: 100%;
  height: 100%;
}
</style> 
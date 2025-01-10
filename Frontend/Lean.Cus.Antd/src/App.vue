<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：App.vue
 * 文件功能描述：应用程序根组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：添加主题切换支持
 ------------------------------------------------------------------>

<template>
  <a-config-provider :locale="antLocale" :theme="themeConfig">
    <router-view v-slot="{ Component }">
      <component :is="Component" />
    </router-view>
  </a-config-provider>
</template>

<script lang="ts" setup>
import { ref, watch, onMounted } from 'vue'
import { useI18n } from 'vue-i18n'
import zhCN from 'ant-design-vue/es/locale/zh_CN'
import zhTW from 'ant-design-vue/es/locale/zh_TW'
import enUS from 'ant-design-vue/es/locale/en_US'
import arEG from 'ant-design-vue/es/locale/ar_EG'
import frFR from 'ant-design-vue/es/locale/fr_FR'
import ruRU from 'ant-design-vue/es/locale/ru_RU'
import esES from 'ant-design-vue/es/locale/es_ES'
import jaJP from 'ant-design-vue/es/locale/ja_JP'
import koKR from 'ant-design-vue/es/locale/ko_KR'
import type { ThemeConfig } from 'ant-design-vue/es/config-provider/context'

const { locale: i18nLocale } = useI18n()
const antLocale = ref(zhCN)
const themeConfig = ref<ThemeConfig>({})

// 监听语言变化
watch(
  () => i18nLocale.value,
  (val) => {
    switch (val) {
      case 'zh-CN':
        antLocale.value = zhCN
        break
      case 'zh-TW':
        antLocale.value = zhTW
        break
      case 'en-US':
        antLocale.value = enUS
        break
      case 'ar-SA':
        antLocale.value = arEG // 使用阿拉伯语（埃及）作为阿拉伯语（沙特）的替代
        break
      case 'fr-FR':
        antLocale.value = frFR
        break
      case 'ru-RU':
        antLocale.value = ruRU
        break
      case 'es-ES':
        antLocale.value = esES
        break
      case 'ja-JP':
        antLocale.value = jaJP
        break
      case 'ko-KR':
        antLocale.value = koKR
        break
      default:
        antLocale.value = enUS // 默认使用英语
    }
  },
  { immediate: true }
)

// 监听主题变化
onMounted(() => {
  window.addEventListener('theme-change', ((event: CustomEvent) => {
    themeConfig.value = event.detail.config
  }) as EventListener)
})
</script>

<style>
#app {
  width: 100%;
  height: 100%;
}
</style> 
<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：App.vue
 * 文件功能描述：应用程序根组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：使用 Ant Design Vue 官方暗黑主题
 ------------------------------------------------------------------>

<template>
  <a-config-provider
    :theme="{
      algorithm: isDark ? theme.darkAlgorithm : theme.defaultAlgorithm
    }"
    :locale="antLocale"
    :direction="locale.startsWith('ar') ? 'rtl' : 'ltr'"
  >
    <router-view></router-view>
  </a-config-provider>
</template>

<script setup lang="ts">
import { ref, watch, provide } from 'vue';
import { useI18n } from 'vue-i18n';
import { theme } from 'ant-design-vue';
import zhCN from 'ant-design-vue/es/locale/zh_CN';
import zhTW from 'ant-design-vue/es/locale/zh_TW';
import enUS from 'ant-design-vue/es/locale/en_US';
import arEG from 'ant-design-vue/es/locale/ar_EG';
import frFR from 'ant-design-vue/es/locale/fr_FR';
import ruRU from 'ant-design-vue/es/locale/ru_RU';
import esES from 'ant-design-vue/es/locale/es_ES';
import jaJP from 'ant-design-vue/es/locale/ja_JP';
import koKR from 'ant-design-vue/es/locale/ko_KR';

const { locale } = useI18n();
const antLocale = ref(zhCN);

// 从 localStorage 读取主题设置，默认为 false（明亮主题）
const isDark = ref(localStorage.getItem('theme') === 'dark');

// 监听主题变化并保存到 localStorage
watch(isDark, (val) => {
  localStorage.setItem('theme', val ? 'dark' : 'light');
});

// 从 localStorage 读取语言设置
const savedLocale = localStorage.getItem('locale');
if (savedLocale) {
  locale.value = savedLocale;
}

// 监听语言变化
watch(
  () => locale.value,
  (val) => {
    // 保存语言设置到 localStorage
    localStorage.setItem('locale', val);
    
    switch (val) {
      case 'zh-CN':
        antLocale.value = zhCN;
        break;
      case 'zh-TW':
        antLocale.value = zhTW;
        break;
      case 'en-US':
        antLocale.value = enUS;
        break;
      case 'ar-SA':
        antLocale.value = arEG;
        break;
      case 'fr-FR':
        antLocale.value = frFR;
        break;
      case 'ru-RU':
        antLocale.value = ruRU;
        break;
      case 'es-ES':
        antLocale.value = esES;
        break;
      case 'ja-JP':
        antLocale.value = jaJP;
        break;
      case 'ko-KR':
        antLocale.value = koKR;
        break;
      default:
        antLocale.value = zhCN;
    }
  },
  { immediate: true }
);

// 提供给子组件使用
provide('isDark', isDark);
</script>

<style>
#app {
  width: 100%;
  height: 100%;
}

body {
  margin: 0;
  min-height: 100vh;
}
</style> 
<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.vue
 * 文件功能描述：主题切换组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：使用 Vue inject/provide 管理主题状态
 ------------------------------------------------------------------>

<template>
  <a-tooltip :title="isDark ? '切换到亮色主题' : '切换到暗色主题'">
    <div class="theme-switch" @click="toggleTheme">
      <BulbFilled v-if="isDark" />
      <BulbOutlined v-else />
    </div>
  </a-tooltip>
</template>

<script setup lang="ts">
import { ref, inject, Ref } from 'vue'
import { BulbOutlined, BulbFilled } from '@ant-design/icons-vue'
import { theme } from 'ant-design-vue'

const isDark = inject<Ref<boolean>>('isDark')!
const { token } = theme.useToken()

const toggleTheme = () => {
  isDark.value = !isDark.value
}
</script>

<style lang="less" scoped>
.theme-switch {
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
</style> 
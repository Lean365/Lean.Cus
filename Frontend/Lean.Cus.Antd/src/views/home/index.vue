<template>
  <div class="home-container">
    <a-spin :spinning="loading">
      <div class="content-wrapper">
        <statistic-cards ref="statisticCardsRef" />
        <shortcut-entries ref="shortcutEntriesRef" />
        <chart-statistics ref="chartStatisticsRef" />
      </div>
    </a-spin>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onErrorCaptured } from 'vue'
import { message } from 'ant-design-vue'
import StatisticCards from '@/components/home/StatisticCards.vue'
import ShortcutEntries from '@/components/home/ShortcutEntries.vue'
import ChartStatistics from '@/components/home/ChartStatistics.vue'

const loading = ref(true)
const statisticCardsRef = ref()
const shortcutEntriesRef = ref()
const chartStatisticsRef = ref()

// 暴露方法给父组件
defineExpose({
  statisticCardsRef,
  shortcutEntriesRef,
  chartStatisticsRef
})

// 错误处理
onErrorCaptured((error) => {
  console.error('首页组件错误:', error)
  message.error('加载组件时发生错误，请刷新页面重试')
  return false // 阻止错误继续传播
})

onMounted(() => {
  try {
    // 这里可以添加首页需要的初始化逻辑
    loading.value = false
  } catch (error) {
    console.error('首页初始化失败:', error)
    message.error('页面初始化失败，请刷新重试')
  }
})
</script>

<style lang="less" scoped>
.home-container {
  height: calc(100vh - 64px - 69px - 32px);  // 减去头部64px、底部69px和padding 32px
  padding: 16px;
  box-sizing: border-box;

  :deep(.ant-spin-nested-loading) {
    height: 100%;
    
    .ant-spin-container {
      height: 100%;
      display: flex;
      flex-direction: column;
      gap: 16px;
    }
  }

  .content-wrapper {
    height: 100%;
    display: flex;
    flex-direction: column;
    gap: 16px;
  }
}
</style> 
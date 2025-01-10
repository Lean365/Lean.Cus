<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.vue
 * 文件功能描述：工作流监控页面
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ------------------------------------------------------------------>

<template>
  <div class="app-container">
    <!-- 统计卡片 -->
    <a-row :gutter="16" class="statistics">
      <a-col :span="6">
        <a-card>
          <template #title>
            <a-space>
              <a-icon type="dashboard" />
              总实例数
            </a-space>
          </template>
          <div class="card-content">
            <div class="number">{{ statistics.totalInstances }}</div>
            <div class="trend">
              <span>较上周</span>
              <span :class="{ up: weekTrend > 0, down: weekTrend < 0 }">
                {{ Math.abs(weekTrend) }}%
                <a-icon :type="weekTrend > 0 ? 'rise' : 'fall'" />
              </span>
            </div>
          </div>
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card>
          <template #title>
            <a-space>
              <a-icon type="sync" />
              运行中实例
            </a-space>
          </template>
          <div class="card-content">
            <div class="number">{{ statistics.runningInstances }}</div>
            <div class="trend">
              <span>较昨日</span>
              <span :class="{ up: dayTrend > 0, down: dayTrend < 0 }">
                {{ Math.abs(dayTrend) }}%
                <a-icon :type="dayTrend > 0 ? 'rise' : 'fall'" />
              </span>
            </div>
          </div>
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card>
          <template #title>
            <a-space>
              <a-icon type="check-circle" />
              已完成实例
            </a-space>
          </template>
          <div class="card-content">
            <div class="number">{{ statistics.completedInstances }}</div>
            <div class="trend">
              <span>完成率</span>
              <span class="rate">{{ completionRate }}%</span>
            </div>
          </div>
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card>
          <template #title>
            <a-space>
              <a-icon type="warning" />
              告警数量
            </a-space>
          </template>
          <div class="card-content">
            <div class="number warning">{{ alarmCount }}</div>
            <div class="trend">
              <a-button type="link" @click="handleViewAlarms">查看告警</a-button>
            </div>
          </div>
        </a-card>
      </a-col>
    </a-row>

    <!-- 图表区域 -->
    <a-row :gutter="16" class="charts">
      <a-col :span="12">
        <a-card title="工作流趋势">
          <div ref="trendChartRef" class="chart"></div>
        </a-card>
      </a-col>
      <a-col :span="12">
        <a-card title="任务分布">
          <div ref="taskChartRef" class="chart"></div>
        </a-card>
      </a-col>
    </a-row>

    <a-row :gutter="16" class="charts">
      <a-col :span="12">
        <a-card title="节点耗时">
          <div ref="nodeChartRef" class="chart"></div>
        </a-card>
      </a-col>
      <a-col :span="12">
        <a-card title="处理人排名">
          <div ref="handlerChartRef" class="chart"></div>
        </a-card>
      </a-col>
    </a-row>

    <!-- 告警列表对话框 -->
    <a-modal
      v-model:visible="alarmVisible"
      title="告警列表"
      width="800px"
      :footer="null"
    >
      <a-table
        :columns="alarmColumns"
        :data-source="alarmList"
        :loading="alarmLoading"
      >
        <template #level="{ text }">
          <a-tag :color="getAlarmLevelColor(text)">
            {{ getAlarmLevelText(text) }}
          </a-tag>
        </template>
        <template #status="{ text }">
          <a-tag :color="getAlarmStatusColor(text)">
            {{ getAlarmStatusText(text) }}
          </a-tag>
        </template>
        <template #action="{ record }">
          <a-space>
            <a @click="handleProcessAlarm(record)">处理</a>
            <a-divider type="vertical" />
            <a @click="handleIgnoreAlarm(record)">忽略</a>
          </a-space>
        </template>
      </a-table>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, onUnmounted } from 'vue'
import { message } from 'ant-design-vue'
import * as echarts from 'echarts'
import type { EChartsOption } from 'echarts'
import { getWorkflowMetrics, getNodeMetrics } from '@/api/workflow'
import type { WorkflowMetrics, NodeMetrics } from '@/api/workflow/types'

// 统计数据
const statistics = reactive<WorkflowMetrics>({
  totalInstances: 0,
  runningInstances: 0,
  completedInstances: 0,
  totalTasks: 0,
  pendingTasks: 0,
  completedTasks: 0
})

// 趋势数据
const weekTrend = ref(5.2)
const dayTrend = ref(-2.1)
const completionRate = ref(85.6)

// 告警相关
const alarmCount = ref(3)
const alarmVisible = ref(false)
const alarmLoading = ref(false)
const alarmList = ref([])
const alarmColumns = [
  {
    title: '告警时间',
    dataIndex: 'createTime',
    key: 'createTime'
  },
  {
    title: '告警级别',
    dataIndex: 'level',
    key: 'level',
    slots: { customRender: 'level' }
  },
  {
    title: '告警内容',
    dataIndex: 'content',
    key: 'content'
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    slots: { customRender: 'status' }
  },
  {
    title: '操作',
    key: 'action',
    slots: { customRender: 'action' }
  }
]

// 图表引用
const trendChartRef = ref<HTMLDivElement>()
const taskChartRef = ref<HTMLDivElement>()
const nodeChartRef = ref<HTMLDivElement>()
const handlerChartRef = ref<HTMLDivElement>()

// 图表实例
let trendChart: echarts.ECharts
let taskChart: echarts.ECharts
let nodeChart: echarts.ECharts
let handlerChart: echarts.ECharts

// 初始化趋势图表
const initTrendChart = () => {
  if (!trendChartRef.value) return
  trendChart = echarts.init(trendChartRef.value)
  const option: EChartsOption = {
    tooltip: {
      trigger: 'axis'
    },
    legend: {
      data: ['总实例数', '运行中', '已完成']
    },
    xAxis: {
      type: 'category',
      data: ['周一', '周二', '周三', '周四', '周五', '周六', '周日']
    },
    yAxis: {
      type: 'value'
    },
    series: [
      {
        name: '总实例数',
        type: 'line',
        data: [120, 132, 101, 134, 90, 230, 210]
      },
      {
        name: '运行中',
        type: 'line',
        data: [20, 32, 11, 34, 30, 50, 40]
      },
      {
        name: '已完成',
        type: 'line',
        data: [100, 100, 90, 100, 60, 180, 170]
      }
    ]
  }
  trendChart.setOption(option)
}

// 初始化任务图表
const initTaskChart = () => {
  if (!taskChartRef.value) return
  taskChart = echarts.init(taskChartRef.value)
  const option: EChartsOption = {
    tooltip: {
      trigger: 'item'
    },
    legend: {
      orient: 'vertical',
      left: 'left'
    },
    series: [
      {
        name: '任务分布',
        type: 'pie',
        radius: '50%',
        data: [
          { value: 1048, name: '审批任务' },
          { value: 735, name: '会签任务' },
          { value: 580, name: '传阅任务' },
          { value: 484, name: '抄送任务' }
        ],
        emphasis: {
          itemStyle: {
            shadowBlur: 10,
            shadowOffsetX: 0,
            shadowColor: 'rgba(0, 0, 0, 0.5)'
          }
        }
      }
    ]
  }
  taskChart.setOption(option)
}

// 初始化节点图表
const initNodeChart = () => {
  if (!nodeChartRef.value) return
  nodeChart = echarts.init(nodeChartRef.value)
  const option: EChartsOption = {
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'shadow'
      }
    },
    legend: {},
    grid: {
      left: '3%',
      right: '4%',
      bottom: '3%',
      containLabel: true
    },
    xAxis: [
      {
        type: 'category',
        data: ['节点1', '节点2', '节点3', '节点4', '节点5']
      }
    ],
    yAxis: [
      {
        type: 'value',
        name: '耗时(分钟)'
      }
    ],
    series: [
      {
        name: '平均耗时',
        type: 'bar',
        data: [30, 45, 25, 60, 20]
      },
      {
        name: '最大耗时',
        type: 'bar',
        data: [60, 90, 50, 120, 40]
      }
    ]
  }
  nodeChart.setOption(option)
}

// 初始化处理人图表
const initHandlerChart = () => {
  if (!handlerChartRef.value) return
  handlerChart = echarts.init(handlerChartRef.value)
  const option: EChartsOption = {
    tooltip: {
      trigger: 'axis',
      axisPointer: {
        type: 'shadow'
      }
    },
    xAxis: {
      type: 'value'
    },
    yAxis: {
      type: 'category',
      data: ['张三', '李四', '王五', '赵六', '钱七']
    },
    series: [
      {
        name: '处理任务数',
        type: 'bar',
        data: [120, 100, 80, 70, 60]
      }
    ]
  }
  handlerChart.setOption(option)
}

// 加载统计数据
const loadStatistics = async () => {
  try {
    const { data: metrics } = await getWorkflowMetrics('all')
    Object.assign(statistics, metrics)
  } catch (error) {
    message.error('获取工作流指标失败')
  }
}

// 查看告警
const handleViewAlarms = () => {
  alarmVisible.value = true
  // TODO: 加载告警数据
}

// 处理告警
const handleProcessAlarm = (alarm: any) => {
  message.info('处理告警功能开发中')
}

// 忽略告警
const handleIgnoreAlarm = (alarm: any) => {
  message.info('忽略告警功能开发中')
}

// 获取告警级别颜色
const getAlarmLevelColor = (level: string) => {
  const colorMap: Record<string, string> = {
    high: 'red',
    medium: 'orange',
    low: 'blue'
  }
  return colorMap[level] || 'default'
}

// 获取告警级别文本
const getAlarmLevelText = (level: string) => {
  const textMap: Record<string, string> = {
    high: '高',
    medium: '中',
    low: '低'
  }
  return textMap[level] || level
}

// 获取告警状态颜色
const getAlarmStatusColor = (status: string) => {
  const colorMap: Record<string, string> = {
    unprocessed: 'red',
    processing: 'orange',
    processed: 'green',
    ignored: 'gray'
  }
  return colorMap[status] || 'default'
}

// 获取告警状态文本
const getAlarmStatusText = (status: string) => {
  const textMap: Record<string, string> = {
    unprocessed: '未处理',
    processing: '处理中',
    processed: '已处理',
    ignored: '已忽略'
  }
  return textMap[status] || status
}

// 窗口大小变化时重绘图表
const handleResize = () => {
  trendChart?.resize()
  taskChart?.resize()
  nodeChart?.resize()
  handlerChart?.resize()
}

onMounted(() => {
  loadStatistics()
  initTrendChart()
  initTaskChart()
  initNodeChart()
  initHandlerChart()
  window.addEventListener('resize', handleResize)
})

onUnmounted(() => {
  window.removeEventListener('resize', handleResize)
  trendChart?.dispose()
  taskChart?.dispose()
  nodeChart?.dispose()
  handlerChart?.dispose()
})
</script>

<style lang="less" scoped>
.statistics {
  margin-bottom: 24px;

  .card-content {
    text-align: center;

    .number {
      font-size: 24px;
      font-weight: bold;
      margin-bottom: 8px;

      &.warning {
        color: #f5222d;
      }
    }

    .trend {
      color: rgba(0, 0, 0, 0.45);

      .up {
        color: #52c41a;
      }

      .down {
        color: #f5222d;
      }

      .rate {
        color: #1890ff;
        font-weight: bold;
      }
    }
  }
}

.charts {
  margin-bottom: 24px;

  .chart {
    height: 300px;
  }
}
</style> 
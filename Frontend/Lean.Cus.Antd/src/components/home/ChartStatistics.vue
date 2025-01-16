<template>
  <div class="chart-statistics">
    <div class="section-title">
      <span class="title-text">统计图表</span>
    </div>
    <a-row :gutter="[16, 16]">
      <a-col :span="8" v-for="chart in displayedCharts" :key="chart.id">
        <a-card :bordered="true" class="chart-card" :hoverable="true">
          <template #title>{{ chart.title }}</template>
          <template #extra>
            <a-space class="card-actions">
              <a-button type="link" size="small" @click="handleEdit(chart)">
                <template #icon><EditOutlined /></template>
              </a-button>
              <a-button type="link" danger size="small" @click="handleDelete(chart)">
                <template #icon><DeleteOutlined /></template>
              </a-button>
            </a-space>
          </template>
          <div :id="'chart-' + chart.id" class="chart-container"></div>
        </a-card>
      </a-col>
    </a-row>
  </div>

  <!-- 编辑模态框 -->
  <a-modal
    v-model:visible="modalVisible"
    :title="formState.id ? '编辑图表' : '添加图表'"
    @ok="handleModalOk"
    @cancel="handleModalCancel"
  >
    <a-form
      :model="formState"
      :rules="rules"
      ref="formRef"
      :label-col="{ span: 6 }"
      :wrapper-col="{ span: 16 }"
    >
      <a-form-item label="图表标题" name="title">
        <a-input v-model:value="formState.title" placeholder="请输入图表标题" />
      </a-form-item>
      <a-form-item label="图表类型" name="type">
        <a-select v-model:value="formState.type">
          <a-select-option value="line">折线图</a-select-option>
          <a-select-option value="bar">柱状图</a-select-option>
          <a-select-option value="pie">饼图</a-select-option>
          <a-select-option value="sunburst">旭日图</a-select-option>
          <a-select-option value="radar">雷达图</a-select-option>
          <a-select-option value="scatter">散点图</a-select-option>
          <a-select-option value="heatmap">热力图</a-select-option>
          <a-select-option value="tree">树图</a-select-option>
          <a-select-option value="gauge">仪表盘</a-select-option>
        </a-select>
      </a-form-item>
      <template v-if="formState.type !== 'pie' && formState.type !== 'sunburst' && formState.type !== 'radar' && formState.type !== 'tree' && formState.type !== 'gauge'">
        <a-form-item label="X轴数据" name="xData">
          <a-input v-model:value="formState.xData" placeholder="请输入X轴数据，用逗号分隔" />
        </a-form-item>
        <a-form-item label="Y轴数据" name="yData">
          <a-input v-model:value="formState.yData" placeholder="请输入Y轴数据，用逗号分隔" />
        </a-form-item>
      </template>
      <template v-else-if="formState.type === 'pie' || formState.type === 'sunburst'">
        <a-form-item label="图表数据" name="pieData">
          <a-input v-model:value="formState.pieData" placeholder="请输入数据，格式：名称:数值，用逗号分隔" />
        </a-form-item>
      </template>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted, nextTick } from 'vue'
import * as echarts from 'echarts'
import { EditOutlined, DeleteOutlined } from '@ant-design/icons-vue'
import { message, Modal } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import type { Rule } from 'ant-design-vue/es/form'

// 图表数据接口
interface ChartData {
  id: string
  title: string
  options: any // 简化类型定义
}

// 表单状态
interface FormState {
  id?: string
  title: string
  type: 'line' | 'bar' | 'pie' | 'sunburst' | 'radar' | 'scatter' | 'heatmap' | 'tree' | 'gauge'
  xData: string
  yData: string
  pieData: string
}

// 表单引用
const formRef = ref<FormInstance>()

// 表单状态
const formState = ref<FormState>({
  title: '',
  type: 'line',
  xData: '',
  yData: '',
  pieData: ''
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  title: [{ required: true, message: '请输入图表标题' }],
  type: [{ required: true, message: '请选择图表类型' }],
  xData: [{ required: true, message: '请输入X轴数据', trigger: 'blur' }],
  yData: [{ required: true, message: '请输入Y轴数据', trigger: 'blur' }],
  pieData: [{ required: true, message: '请输入饼图数据', trigger: 'blur' }]
}

// 模态框可见性
const modalVisible = ref(false)

// 编辑图表
const handleEdit = (chart: ChartData) => {
  formState.value = {
    id: chart.id,
    title: chart.title,
    type: chart.options.series[0].type,
    xData: chart.options.xAxis?.data?.join(',') || '',
    yData: chart.options.series[0].data?.map((item: any) => item.value || item).join(',') || '',
    pieData: chart.options.series[0].data?.map((item: any) => `${item.name}:${item.value}`).join(',') || ''
  }
  modalVisible.value = true
}

// 删除图表
const handleDelete = async (chart: ChartData) => {
  try {
    // TODO: 调用API删除图表
    displayedCharts.value = displayedCharts.value.filter(item => item.id !== chart.id)
    const instance = chartInstances.get(chart.id)
    if (instance) {
      instance.dispose()
      chartInstances.delete(chart.id)
    }
    message.success('删除成功')
  } catch (error) {
    message.error('删除失败')
  }
}

// 处理模态框确认
const handleModalOk = async () => {
  try {
    await formRef.value?.validate()
    const formData = formState.value
    
    // 构建图表选项
    const options: any = {
      tooltip: { trigger: ['line', 'bar', 'scatter', 'heatmap'].includes(formData.type) ? 'axis' : 'item' }
    }
    
    switch (formData.type) {
      case 'line':
      case 'bar':
      case 'scatter':
        const xData = formData.xData.split(',').map(item => item.trim())
        const yData = formData.yData.split(',').map(item => Number(item.trim()))
        
        options.xAxis = {
          type: 'category',
          data: xData
        }
        options.yAxis = { type: 'value' }
        options.series = [{
          type: formData.type,
          data: yData,
          ...(formData.type === 'line' ? { smooth: true, areaStyle: { opacity: 0.1 } } : {})
        }]
        break
        
      case 'pie':
      case 'sunburst':
        const pieData = formData.pieData.split(',').map(item => {
          const [name, value] = item.split(':').map(s => s.trim())
          return { name, value: Number(value) }
        })
        
        options.series = [{
          type: formData.type,
          radius: '50%',
          data: pieData
        }]
        break
        
      case 'radar':
        // 添加雷达图配置
        const indicators = formData.xData.split(',').map(item => ({
          name: item.trim(),
          max: 100
        }))
        const radarData = formData.yData.split(',').map(item => Number(item.trim()))
        
        options.radar = {
          indicator: indicators
        }
        options.series = [{
          type: 'radar',
          data: [{
            value: radarData
          }]
        }]
        break
        
      case 'gauge':
        // 添加仪表盘配置
        const gaugeValue = Number(formData.yData.split(',')[0])
        options.series = [{
          type: 'gauge',
          progress: {
            show: true
          },
          detail: {
            valueAnimation: true,
            formatter: '{value}'
          },
          data: [{
            value: gaugeValue
          }]
        }]
        break
        
      // 可以继续添加其他图表类型的配置...
    }
    
    if (formData.id) {
      // 更新现有图表
      const index = displayedCharts.value.findIndex(item => item.id === formData.id)
      if (index !== -1) {
        displayedCharts.value[index] = {
          id: formData.id,
          title: formData.title,
          options
        }
        const instance = chartInstances.get(formData.id)
        if (instance) {
          instance.setOption(options)
        }
      }
      message.success('更新成功')
    } else {
      // 添加新图表
      const newChart: ChartData = {
        id: `chart-${Date.now()}`,
        title: formData.title,
        options
      }
      displayedCharts.value.push(newChart)
      setTimeout(() => {
        initChart(newChart)
      }, 0)
      message.success('添加成功')
    }
    
    modalVisible.value = false
  } catch (error) {
    console.error('表单验证失败:', error)
  }
}

// 处理模态框取消
const handleModalCancel = () => {
  formRef.value?.resetFields()
  modalVisible.value = false
}

// 默认图表数据
const defaultCharts: ChartData[] = [
  {
    id: 'sales-trend',
    title: '销售趋势',
    options: {
      tooltip: { trigger: 'axis' },
      xAxis: {
        type: 'category',
        data: ['1月', '2月', '3月', '4月', '5月', '6月']
      },
      yAxis: { type: 'value' },
      series: [{
        type: 'line',
        data: [150, 230, 224, 218, 135, 147],
        smooth: true,
        areaStyle: { opacity: 0.1 }
      }]
    }
  },
  {
    id: 'category-distribution',
    title: '品类分布',
    options: {
      tooltip: { trigger: 'item' },
      series: [{
        type: 'pie',
        radius: '50%',
        data: [
          { name: '电子产品', value: 335 },
          { name: '服装', value: 310 },
          { name: '食品', value: 234 },
          { name: '家具', value: 135 },
          { name: '其他', value: 148 }
        ]
      }]
    }
  },
  {
    id: 'monthly-orders',
    title: '月度订单',
    options: {
      tooltip: { trigger: 'axis' },
      xAxis: {
        type: 'category',
        data: ['1月', '2月', '3月', '4月', '5月', '6月']
      },
      yAxis: { type: 'value' },
      series: [{
        type: 'bar',
        data: [320, 332, 301, 334, 390, 330]
      }]
    }
  }
]

// 显示的图表数据
const displayedCharts = ref<ChartData[]>(defaultCharts)

// 图表实例Map
const chartInstances = new Map<string, echarts.ECharts>()

// 初始化图表
const initChart = (chartData: ChartData) => {
  const chartDom = document.getElementById('chart-' + chartData.id)
  if (!chartDom) return
  
  const chart = echarts.init(chartDom)
  chart.setOption(chartData.options)
  chartInstances.set(chartData.id, chart)
}

// 监听窗口大小变化
const handleResize = () => {
  chartInstances.forEach(chart => {
    chart.resize()
  })
}

// 暴露给父组件的添加图表方法
const showAddModal = () => {
  // 清除图表实例
  chartInstances.forEach(chart => {
    chart.dispose()
  })
  chartInstances.clear()
  
  // 重置表单状态
  formState.value.title = ''
  formState.value.type = 'line'
  formState.value.xData = ''
  formState.value.yData = ''
  formState.value.pieData = ''
  
  // 显示模态框
  modalVisible.value = true
}

// 暴露方法给父组件
defineExpose({
  showAddModal
})

onMounted(() => {
  nextTick(() => {
    displayedCharts.value.forEach(chart => {
      initChart(chart)
    })
  })
  window.addEventListener('resize', handleResize)
})

onUnmounted(() => {
  // 销毁图表实例
  chartInstances.forEach(chart => {
    chart.dispose()
  })
  chartInstances.clear()
  window.removeEventListener('resize', handleResize)
})
</script>

<style scoped lang="less">
.section-title {
  margin-bottom: 16px;
  .title-text {
    font-size: 16px;
    font-weight: 500;
    color: var(--ant-color-text);
  }
}

.chart-card {
  :deep(.ant-card-head) {
    min-height: 48px;
    padding: 0 16px;
    border-bottom: 1px solid var(--ant-border-color-split);
    
    .ant-card-head-title {
      padding: 12px 0;
      font-size: 14px;
      color: var(--ant-color-text-secondary);
    }

    .ant-card-extra {
      padding: 12px 0;
      .card-actions {
        opacity: 0;
        transition: opacity 0.3s;
        visibility: hidden;
      }
    }
  }

  &:hover {
    :deep(.ant-card-head) {
      .ant-card-extra {
        .card-actions {
          opacity: 1;
          visibility: visible;
        }
      }
    }
  }

  .chart-container {
    width: 100%;
    height: 280px;
    background-color: var(--ant-background-color);
  }
}

.chart-statistics {
  .chart-card {
    .chart-container {
      height: 280px;
    }
  }
}
</style> 
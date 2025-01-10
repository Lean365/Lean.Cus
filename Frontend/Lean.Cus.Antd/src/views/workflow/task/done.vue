<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：done.vue
 * 文件功能描述：工作流已办任务列表页面
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ------------------------------------------------------------------>

<template>
  <div class="app-container">
    <a-card :bordered="false">
      <!-- 查询表单 -->
      <a-form ref="queryForm" :model="queryParams" layout="inline">
        <a-form-item label="工作流名称" name="workflowName">
          <a-input
            v-model:value="queryParams.workflowName"
            placeholder="请输入工作流名称"
            allow-clear
            style="width: 200px"
          />
        </a-form-item>
        <a-form-item label="节点名称" name="nodeName">
          <a-input
            v-model:value="queryParams.nodeName"
            placeholder="请输入节点名称"
            allow-clear
            style="width: 200px"
          />
        </a-form-item>
        <a-form-item label="处理时间" name="timeRange">
          <a-range-picker
            v-model:value="queryParams.timeRange"
            style="width: 240px"
          />
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" @click="handleQuery">查询</a-button>
            <a-button @click="handleReset">重置</a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 操作按钮 -->
      <div class="table-operations">
        <a-button @click="handleExport">导出</a-button>
      </div>

      <!-- 数据表格 -->
      <a-table
        :columns="columns"
        :data-source="taskList"
        :loading="loading"
        :pagination="pagination"
        @change="handleTableChange"
      >
        <template #status="{ text }">
          <a-tag :color="getStatusColor(text)">{{ getStatusText(text) }}</a-tag>
        </template>
        <template #action="{ record }">
          <a-space>
            <a @click="handleViewDetail(record)">详情</a>
            <a-divider type="vertical" />
            <a @click="handleViewHistory(record)">历史</a>
          </a-space>
        </template>
      </a-table>

      <!-- 详情对话框 -->
      <a-modal
        v-model:visible="detailVisible"
        title="任务详情"
        :footer="null"
        width="800px"
      >
        <a-descriptions bordered>
          <a-descriptions-item label="工作流名称">
            {{ currentTask?.workflowName }}
          </a-descriptions-item>
          <a-descriptions-item label="节点名称">
            {{ currentTask?.nodeName }}
          </a-descriptions-item>
          <a-descriptions-item label="创建时间">
            {{ currentTask?.createTime }}
          </a-descriptions-item>
          <a-descriptions-item label="完成时间">
            {{ currentTask?.completeTime }}
          </a-descriptions-item>
          <a-descriptions-item label="状态">
            <a-tag :color="getStatusColor(currentTask?.status)">
              {{ getStatusText(currentTask?.status) }}
            </a-tag>
          </a-descriptions-item>
          <a-descriptions-item label="处理人">
            {{ currentTask?.assignee }}
          </a-descriptions-item>
          <a-descriptions-item label="处理意见" :span="3">
            {{ currentTask?.comment }}
          </a-descriptions-item>
          <a-descriptions-item label="表单数据" :span="3">
            <pre>{{ JSON.stringify(currentTask?.formData, null, 2) }}</pre>
          </a-descriptions-item>
        </a-descriptions>
      </a-modal>

      <!-- 历史对话框 -->
      <a-modal
        v-model:visible="historyVisible"
        title="任务历史"
        :footer="null"
        width="800px"
      >
        <a-timeline>
          <a-timeline-item
            v-for="item in taskHistory"
            :key="item.id"
            :color="getHistoryColor(item.type)"
          >
            <template #dot>
              <a-icon :type="getHistoryIcon(item.type)" />
            </template>
            <div class="history-item">
              <div class="history-header">
                <span class="history-type">{{ getHistoryText(item.type) }}</span>
                <span class="history-time">{{ item.operateTime }}</span>
              </div>
              <div class="history-operator">
                操作人：{{ item.operator }}
              </div>
              <div class="history-comment" v-if="item.comment">
                处理意见：{{ item.comment }}
              </div>
              <div class="history-data" v-if="item.data">
                <pre>{{ JSON.stringify(item.data, null, 2) }}</pre>
              </div>
            </div>
          </a-timeline-item>
        </a-timeline>
      </a-modal>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { getDoneTasks, getTaskHistory } from '@/api/workflow'
import type { WorkflowTask, WorkflowTaskHistory } from '@/api/workflow/types'

// 查询参数
const queryParams = reactive({
  workflowName: '',
  nodeName: '',
  timeRange: [],
  pageNum: 1,
  pageSize: 10
})

// 表格列定义
const columns = [
  {
    title: '工作流名称',
    dataIndex: 'workflowName',
    key: 'workflowName'
  },
  {
    title: '节点名称',
    dataIndex: 'nodeName',
    key: 'nodeName'
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    slots: { customRender: 'status' }
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime'
  },
  {
    title: '完成时间',
    dataIndex: 'completeTime',
    key: 'completeTime'
  },
  {
    title: '处理人',
    dataIndex: 'assignee',
    key: 'assignee'
  },
  {
    title: '操作',
    key: 'action',
    slots: { customRender: 'action' }
  }
]

// 表格数据
const loading = ref(false)
const taskList = ref<WorkflowTask[]>([])
const pagination = reactive<TablePaginationConfig>({
  total: 0,
  current: 1,
  pageSize: 10,
  showSizeChanger: true,
  showTotal: (total) => `共 ${total} 条`
})

// 当前任务
const currentTask = ref<WorkflowTask>()

// 详情相关
const detailVisible = ref(false)

// 历史相关
const historyVisible = ref(false)
const taskHistory = ref<WorkflowTaskHistory[]>([])

// 查询列表
const handleQuery = async () => {
  loading.value = true
  try {
    const { data: tasks } = await getDoneTasks({
      ...queryParams,
      userId: 'currentUser' // TODO: 从用户上下文获取
    })
    taskList.value = tasks
    pagination.total = tasks.length // TODO: 从后端获取总数
  } catch (error) {
    message.error('获取已办任务失败')
  } finally {
    loading.value = false
  }
}

// 重置查询
const handleReset = () => {
  queryParams.workflowName = ''
  queryParams.nodeName = ''
  queryParams.timeRange = []
  handleQuery()
}

// 表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  pagination.current = pag.current
  pagination.pageSize = pag.pageSize
  handleQuery()
}

// 导出
const handleExport = () => {
  message.info('导出功能开发中')
}

// 查看详情
const handleViewDetail = (task: WorkflowTask) => {
  currentTask.value = task
  detailVisible.value = true
}

// 查看历史
const handleViewHistory = async (task: WorkflowTask) => {
  try {
    const { data: history } = await getTaskHistory(task.instanceId)
    taskHistory.value = history
    historyVisible.value = true
  } catch (error) {
    message.error('获取任务历史失败')
  }
}

// 获取状态颜色
const getStatusColor = (status: string) => {
  const colorMap: Record<string, string> = {
    completed: 'green',
    rejected: 'red'
  }
  return colorMap[status] || 'default'
}

// 获取状态文本
const getStatusText = (status: string) => {
  const textMap: Record<string, string> = {
    completed: '已完成',
    rejected: '已拒绝'
  }
  return textMap[status] || status
}

// 获取历史颜色
const getHistoryColor = (type: string) => {
  const colorMap: Record<string, string> = {
    create: 'blue',
    approve: 'green',
    reject: 'red',
    transfer: 'orange'
  }
  return colorMap[type] || 'default'
}

// 获取历史图标
const getHistoryIcon = (type: string) => {
  const iconMap: Record<string, string> = {
    create: 'plus-circle',
    approve: 'check-circle',
    reject: 'close-circle',
    transfer: 'swap'
  }
  return iconMap[type] || 'clock-circle'
}

// 获取历史文本
const getHistoryText = (type: string) => {
  const textMap: Record<string, string> = {
    create: '创建任务',
    approve: '审批通过',
    reject: '审批拒绝',
    transfer: '任务转办'
  }
  return textMap[type] || type
}

onMounted(() => {
  handleQuery()
})
</script>

<style lang="less" scoped>
.table-operations {
  margin-bottom: 16px;
}

.ant-descriptions {
  margin-bottom: 24px;
}

pre {
  margin: 0;
  padding: 8px;
  background-color: #f5f5f5;
  border-radius: 2px;
}

.history-item {
  margin-bottom: 16px;

  .history-header {
    display: flex;
    justify-content: space-between;
    margin-bottom: 8px;

    .history-type {
      font-weight: bold;
    }

    .history-time {
      color: rgba(0, 0, 0, 0.45);
    }
  }

  .history-operator {
    margin-bottom: 4px;
  }

  .history-comment {
    margin-bottom: 8px;
  }

  .history-data {
    background-color: #f5f5f5;
    padding: 8px;
    border-radius: 2px;
  }
}
</style> 
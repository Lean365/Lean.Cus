<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.vue
 * 文件功能描述：工作流实例列表页面
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ------------------------------------------------------------------>

<template>
  <div class="app-container">
    <a-card :bordered="false">
      <!-- 搜索表单 -->
      <a-form layout="inline" :model="queryParams" @finish="handleQuery">
        <a-form-item label="流程名称">
          <a-input
            v-model:value="queryParams.workflowName"
            placeholder="请输入流程名称"
            allow-clear
          />
        </a-form-item>
        <a-form-item label="状态">
          <a-select
            v-model:value="queryParams.status"
            placeholder="请选择状态"
            allow-clear
          >
            <a-select-option value="running">运行中</a-select-option>
            <a-select-option value="completed">已完成</a-select-option>
            <a-select-option value="terminated">已终止</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="创建时间">
          <a-range-picker
            v-model:value="queryParams.timeRange"
            show-time
            format="YYYY-MM-DD HH:mm:ss"
          />
        </a-form-item>
        <a-form-item>
          <a-space>
            <a-button type="primary" html-type="submit">查询</a-button>
            <a-button @click="handleReset">重置</a-button>
          </a-space>
        </a-form-item>
      </a-form>

      <!-- 工具栏 -->
      <div class="toolbar">
        <a-space>
          <a-button type="primary" @click="handleStart">发起流程</a-button>
          <a-button @click="handleExport">导出</a-button>
        </a-space>
      </div>

      <!-- 数据表格 -->
      <a-table
        :columns="columns"
        :data-source="instanceList"
        :loading="loading"
        :pagination="pagination"
        @change="handleTableChange"
      >
        <!-- 状态列 -->
        <template #status="{ text }">
          <a-tag :color="getStatusColor(text)">{{ getStatusText(text) }}</a-tag>
        </template>

        <!-- 操作列 -->
        <template #action="{ record }">
          <a-space>
            <a @click="handleView(record)">查看</a>
            <a-divider type="vertical" />
            <a @click="handleHistory(record)">历史</a>
            <a-divider type="vertical" />
            <a-dropdown>
              <a class="ant-dropdown-link" @click.prevent>
                更多
                <down-outlined />
              </a>
              <template #overlay>
                <a-menu>
                  <a-menu-item key="suspend" v-if="record.status === 'running'">
                    <a @click="handleSuspend(record)">挂起</a>
                  </a-menu-item>
                  <a-menu-item key="resume" v-if="record.status === 'suspended'">
                    <a @click="handleResume(record)">恢复</a>
                  </a-menu-item>
                  <a-menu-item key="terminate" v-if="record.status === 'running'">
                    <a @click="handleTerminate(record)">终止</a>
                  </a-menu-item>
                  <a-menu-item key="delete">
                    <a @click="handleDelete(record)">删除</a>
                  </a-menu-item>
                </a-menu>
              </template>
            </a-dropdown>
          </a-space>
        </template>
      </a-table>

      <!-- 发起流程对话框 -->
      <a-modal
        v-model:visible="startVisible"
        title="发起流程"
        width="600px"
        @ok="handleStartConfirm"
      >
        <a-form :model="startForm" layout="vertical">
          <a-form-item
            label="流程定义"
            name="workflowId"
            :rules="[{ required: true, message: '请选择流程定义' }]"
          >
            <a-select
              v-model:value="startForm.workflowId"
              placeholder="请选择流程定义"
              :options="workflowOptions"
            />
          </a-form-item>
          <a-form-item
            label="流程标题"
            name="title"
            :rules="[{ required: true, message: '请输入流程标题' }]"
          >
            <a-input v-model:value="startForm.title" placeholder="请输入流程标题" />
          </a-form-item>
          <a-form-item label="备注" name="remark">
            <a-textarea
              v-model:value="startForm.remark"
              placeholder="请输入备注"
              :rows="4"
            />
          </a-form-item>
        </a-form>
      </a-modal>

      <!-- 查看流程对话框 -->
      <a-modal
        v-model:visible="viewVisible"
        title="查看流程"
        width="800px"
        :footer="null"
      >
        <a-descriptions bordered>
          <a-descriptions-item label="流程名称">
            {{ currentInstance?.workflowName }}
          </a-descriptions-item>
          <a-descriptions-item label="流程状态">
            <a-tag :color="getStatusColor(currentInstance?.status)">
              {{ getStatusText(currentInstance?.status) }}
            </a-tag>
          </a-descriptions-item>
          <a-descriptions-item label="开始时间">
            {{ currentInstance?.startTime }}
          </a-descriptions-item>
          <a-descriptions-item label="结束时间">
            {{ currentInstance?.endTime || '-' }}
          </a-descriptions-item>
          <a-descriptions-item label="当前节点">
            {{ currentInstance?.currentNode || '-' }}
          </a-descriptions-item>
        </a-descriptions>

        <!-- 流程图 -->
        <div class="workflow-graph" ref="graphRef"></div>
      </a-modal>

      <!-- 历史记录对话框 -->
      <a-modal
        v-model:visible="historyVisible"
        title="历史记录"
        width="800px"
        :footer="null"
      >
        <a-timeline>
          <a-timeline-item
            v-for="item in historyList"
            :key="item.id"
            :color="getHistoryColor(item.type)"
          >
            <template #dot>
              <a-icon :type="getHistoryIcon(item.type)" />
            </template>
            <div class="history-item">
              <div class="history-header">
                <span class="history-type">{{ getHistoryTypeText(item.type) }}</span>
                <span class="history-time">{{ item.operateTime }}</span>
              </div>
              <div class="history-content">
                <div class="history-node">节点：{{ item.nodeName }}</div>
                <div class="history-operator">操作人：{{ item.operator }}</div>
                <div class="history-comment" v-if="item.comment">
                  备注：{{ item.comment }}
                </div>
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
import { Graph } from '@antv/x6'
import type { WorkflowInstance, WorkflowHistory } from '@/api/workflow/types'

// 查询参数
const queryParams = reactive({
  workflowName: '',
  status: undefined,
  timeRange: [],
  pageNum: 1,
  pageSize: 10
})

// 表格列定义
const columns = [
  {
    title: '流程名称',
    dataIndex: 'workflowName',
    key: 'workflowName'
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    slots: { customRender: 'status' }
  },
  {
    title: '开始时间',
    dataIndex: 'startTime',
    key: 'startTime',
    sorter: true
  },
  {
    title: '结束时间',
    dataIndex: 'endTime',
    key: 'endTime'
  },
  {
    title: '当前节点',
    dataIndex: 'currentNode',
    key: 'currentNode'
  },
  {
    title: '操作',
    key: 'action',
    slots: { customRender: 'action' }
  }
]

// 数据列表
const instanceList = ref<WorkflowInstance[]>([])
const loading = ref(false)
const pagination = reactive<TablePaginationConfig>({
  total: 0,
  current: 1,
  pageSize: 10,
  showSizeChanger: true,
  showQuickJumper: true
})

// 发起流程相关
const startVisible = ref(false)
const startForm = reactive({
  workflowId: undefined,
  title: '',
  remark: ''
})
const workflowOptions = ref([])

// 查看流程相关
const viewVisible = ref(false)
const currentInstance = ref<WorkflowInstance>()
const graphRef = ref<HTMLDivElement>()
const graph = ref<Graph>()

// 历史记录相关
const historyVisible = ref(false)
const historyList = ref<WorkflowHistory[]>([])

// 查询列表
const handleQuery = async () => {
  loading.value = true
  try {
    // TODO: 调用查询API
    loading.value = false
  } catch (error) {
    message.error('查询失败')
    loading.value = false
  }
}

// 重置查询
const handleReset = () => {
  queryParams.workflowName = ''
  queryParams.status = undefined
  queryParams.timeRange = []
  handleQuery()
}

// 表格变化
const handleTableChange = (pag: TablePaginationConfig) => {
  pagination.current = pag.current
  pagination.pageSize = pag.pageSize
  handleQuery()
}

// 发起流程
const handleStart = () => {
  startVisible.value = true
}

// 确认发起
const handleStartConfirm = async () => {
  try {
    // TODO: 调用发起API
    startVisible.value = false
    message.success('发起成功')
    handleQuery()
  } catch (error) {
    message.error('发起失败')
  }
}

// 查看流程
const handleView = (record: WorkflowInstance) => {
  currentInstance.value = record
  viewVisible.value = true
  // 初始化流程图
  if (graphRef.value) {
    graph.value = new Graph({
      container: graphRef.value,
      grid: true,
      interacting: false
    })
    // TODO: 渲染流程图
  }
}

// 查看历史
const handleHistory = async (record: WorkflowInstance) => {
  try {
    // TODO: 调用查询历史API
    historyVisible.value = true
  } catch (error) {
    message.error('查询历史失败')
  }
}

// 挂起流程
const handleSuspend = async (record: WorkflowInstance) => {
  try {
    // TODO: 调用挂起API
    message.success('操作成功')
    handleQuery()
  } catch (error) {
    message.error('操作失败')
  }
}

// 恢复流程
const handleResume = async (record: WorkflowInstance) => {
  try {
    // TODO: 调用恢复API
    message.success('操作成功')
    handleQuery()
  } catch (error) {
    message.error('操作失败')
  }
}

// 终止流程
const handleTerminate = async (record: WorkflowInstance) => {
  try {
    // TODO: 调用终止API
    message.success('操作成功')
    handleQuery()
  } catch (error) {
    message.error('操作失败')
  }
}

// 删除流程
const handleDelete = async (record: WorkflowInstance) => {
  try {
    // TODO: 调用删除API
    message.success('删除成功')
    handleQuery()
  } catch (error) {
    message.error('删除失败')
  }
}

// 导出列表
const handleExport = () => {
  message.info('导出功能开发中')
}

// 获取状态颜色
const getStatusColor = (status?: string) => {
  const colorMap: Record<string, string> = {
    running: 'processing',
    completed: 'success',
    terminated: 'error',
    suspended: 'warning'
  }
  return colorMap[status || ''] || 'default'
}

// 获取状态文本
const getStatusText = (status?: string) => {
  const textMap: Record<string, string> = {
    running: '运行中',
    completed: '已完成',
    terminated: '已终止',
    suspended: '已挂起'
  }
  return textMap[status || ''] || status || '-'
}

// 获取历史记录颜色
const getHistoryColor = (type: string) => {
  const colorMap: Record<string, string> = {
    start: 'green',
    approve: 'blue',
    reject: 'red',
    complete: 'green'
  }
  return colorMap[type] || 'gray'
}

// 获取历史记录图标
const getHistoryIcon = (type: string) => {
  const iconMap: Record<string, string> = {
    start: 'play-circle',
    approve: 'check-circle',
    reject: 'close-circle',
    complete: 'check-circle'
  }
  return iconMap[type] || 'clock-circle'
}

// 获取历史记录类型文本
const getHistoryTypeText = (type: string) => {
  const textMap: Record<string, string> = {
    start: '发起流程',
    approve: '审批通过',
    reject: '审批拒绝',
    complete: '流程完成'
  }
  return textMap[type] || type
}

// 初始化
onMounted(() => {
  handleQuery()
})
</script>

<style lang="less" scoped>
.app-container {
  padding: 24px;
}

.toolbar {
  margin: 16px 0;
}

.workflow-graph {
  height: 400px;
  margin-top: 16px;
  border: 1px solid #d9d9d9;
  border-radius: 2px;
}

.history-item {
  .history-header {
    display: flex;
    justify-content: space-between;
    margin-bottom: 8px;

    .history-type {
      font-weight: bold;
    }

    .history-time {
      color: #999;
    }
  }

  .history-content {
    color: #666;
    line-height: 1.5;

    .history-comment {
      margin-top: 8px;
      color: #999;
    }
  }
}
</style> 
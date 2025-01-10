<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：todo.vue
 * 文件功能描述：工作流待办任务列表页面
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
        <a-form-item label="创建时间" name="timeRange">
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
        <a-space>
          <a-button type="primary" @click="handleBatchApprove" :disabled="!selectedRowKeys.length">
            批量审批
          </a-button>
          <a-button @click="handleExport">导出</a-button>
        </a-space>
      </div>

      <!-- 数据表格 -->
      <a-table
        :columns="columns"
        :data-source="taskList"
        :loading="loading"
        :pagination="pagination"
        :row-selection="{ selectedRowKeys, onChange: onSelectChange }"
        @change="handleTableChange"
      >
        <template #status="{ text }">
          <a-tag :color="getStatusColor(text)">{{ getStatusText(text) }}</a-tag>
        </template>
        <template #action="{ record }">
          <a-space>
            <a @click="handleApprove(record)">审批</a>
            <a-divider type="vertical" />
            <a @click="handleTransfer(record)">转办</a>
            <a-divider type="vertical" />
            <a @click="handleViewDetail(record)">详情</a>
          </a-space>
        </template>
      </a-table>

      <!-- 审批对话框 -->
      <a-modal
        v-model:visible="approveVisible"
        title="任务审批"
        @ok="handleApproveConfirm"
      >
        <a-form :model="approveForm" layout="vertical">
          <a-form-item
            label="审批意见"
            name="comment"
            :rules="[{ required: true, message: '请输入审批意见' }]"
          >
            <a-textarea
              v-model:value="approveForm.comment"
              placeholder="请输入审批意见"
              :rows="4"
            />
          </a-form-item>
          <a-form-item name="formData">
            <!-- 根据任务类型显示不同的表单 -->
            <component
              :is="getFormComponent(currentTask?.formType)"
              v-model:value="approveForm.formData"
            />
          </a-form-item>
        </a-form>
      </a-modal>

      <!-- 转办对话框 -->
      <a-modal
        v-model:visible="transferVisible"
        title="任务转办"
        @ok="handleTransferConfirm"
      >
        <a-form :model="transferForm" layout="vertical">
          <a-form-item
            label="转办人"
            name="targetUserId"
            :rules="[{ required: true, message: '请选择转办人' }]"
          >
            <a-select
              v-model:value="transferForm.targetUserId"
              placeholder="请选择转办人"
              :options="userOptions"
            />
          </a-form-item>
          <a-form-item
            label="转办意见"
            name="comment"
            :rules="[{ required: true, message: '请输入转办意见' }]"
          >
            <a-textarea
              v-model:value="transferForm.comment"
              placeholder="请输入转办意见"
              :rows="4"
            />
          </a-form-item>
        </a-form>
      </a-modal>

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
          <a-descriptions-item label="状态">
            <a-tag :color="getStatusColor(currentTask?.status)">
              {{ getStatusText(currentTask?.status) }}
            </a-tag>
          </a-descriptions-item>
          <a-descriptions-item label="处理人">
            {{ currentTask?.assignee }}
          </a-descriptions-item>
          <a-descriptions-item label="表单数据" :span="3">
            <pre>{{ JSON.stringify(currentTask?.formData, null, 2) }}</pre>
          </a-descriptions-item>
        </a-descriptions>
      </a-modal>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import { getTodoTasks, completeTask, transferTask } from '@/api/workflow'
import type { WorkflowTask } from '@/api/workflow/types'

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
    key: 'createTime',
    sorter: true
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

// 选择行
const selectedRowKeys = ref<string[]>([])
const onSelectChange = (keys: string[]) => {
  selectedRowKeys.value = keys
}

// 当前任务
const currentTask = ref<WorkflowTask>()

// 审批相关
const approveVisible = ref(false)
const approveForm = reactive({
  comment: '',
  formData: {}
})

// 转办相关
const transferVisible = ref(false)
const transferForm = reactive({
  targetUserId: '',
  comment: ''
})
const userOptions = ref([
  { label: '用户1', value: 'user1' },
  { label: '用户2', value: 'user2' }
])

// 详情相关
const detailVisible = ref(false)

// 查询列表
const handleQuery = async () => {
  loading.value = true
  try {
    const { data: tasks } = await getTodoTasks({
      ...queryParams,
      userId: 'currentUser' // TODO: 从用户上下文获取
    })
    taskList.value = tasks
    pagination.total = tasks.length // TODO: 从后端获取总数
  } catch (error) {
    message.error('获取待办任务失败')
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

// 批量审批
const handleBatchApprove = () => {
  message.info('批量审批功能开发中')
}

// 导出
const handleExport = () => {
  message.info('导出功能开发中')
}

// 审批任务
const handleApprove = (task: WorkflowTask) => {
  currentTask.value = task
  approveVisible.value = true
}

// 确认审批
const handleApproveConfirm = async () => {
  if (!currentTask.value) return
  try {
    await completeTask(currentTask.value.id, {
      userId: 'currentUser', // TODO: 从用户上下文获取
      comment: approveForm.comment,
      formData: approveForm.formData
    })
    message.success('审批成功')
    approveVisible.value = false
    handleQuery()
  } catch (error) {
    message.error('审批失败')
  }
}

// 转办任务
const handleTransfer = (task: WorkflowTask) => {
  currentTask.value = task
  transferVisible.value = true
}

// 确认转办
const handleTransferConfirm = async () => {
  if (!currentTask.value) return
  try {
    await transferTask(currentTask.value.id, {
      userId: 'currentUser', // TODO: 从用户上下文获取
      targetUserId: transferForm.targetUserId,
      comment: transferForm.comment
    })
    message.success('转办成功')
    transferVisible.value = false
    handleQuery()
  } catch (error) {
    message.error('转办失败')
  }
}

// 查看详情
const handleViewDetail = (task: WorkflowTask) => {
  currentTask.value = task
  detailVisible.value = true
}

// 获取表单组件
const getFormComponent = (type: string) => {
  // 根据任务类型返回不同的表单组件
  return 'div'
}

// 获取状态颜色
const getStatusColor = (status: string) => {
  const colorMap: Record<string, string> = {
    pending: 'blue',
    processing: 'orange',
    completed: 'green',
    rejected: 'red'
  }
  return colorMap[status] || 'default'
}

// 获取状态文本
const getStatusText = (status: string) => {
  const textMap: Record<string, string> = {
    pending: '待处理',
    processing: '处理中',
    completed: '已完成',
    rejected: '已拒绝'
  }
  return textMap[status] || status
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
</style> 
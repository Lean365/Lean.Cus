<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.vue
 * 文件功能描述：工作流任务列表页面
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
        <a-form-item label="节点名称">
          <a-input
            v-model:value="queryParams.nodeName"
            placeholder="请输入节点名称"
            allow-clear
          />
        </a-form-item>
        <a-form-item label="状态">
          <a-select
            v-model:value="queryParams.status"
            placeholder="请选择状态"
            allow-clear
          >
            <a-select-option value="pending">待处理</a-select-option>
            <a-select-option value="processing">处理中</a-select-option>
            <a-select-option value="completed">已完成</a-select-option>
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
          <a-button type="primary" @click="handleBatchApprove">批量通过</a-button>
          <a-button @click="handleExport">导出</a-button>
        </a-space>
      </div>

      <!-- 数据表格 -->
      <a-table
        :columns="columns"
        :data-source="taskList"
        :loading="loading"
        :pagination="pagination"
        :row-selection="rowSelection"
        @change="handleTableChange"
      >
        <!-- 状态列 -->
        <template #status="{ text }">
          <a-tag :color="getStatusColor(text)">{{ getStatusText(text) }}</a-tag>
        </template>

        <!-- 操作列 -->
        <template #action="{ record }">
          <a-space>
            <a @click="handleApprove(record)">审批</a>
            <a-divider type="vertical" />
            <a @click="handleView(record)">查看</a>
            <a-divider type="vertical" />
            <a @click="handleTransfer(record)">转办</a>
          </a-space>
        </template>
      </a-table>

      <!-- 审批对话框 -->
      <a-modal
        v-model:visible="approveVisible"
        title="审批任务"
        width="600px"
        @ok="handleApproveConfirm"
      >
        <a-form :model="approveForm" layout="vertical">
          <a-form-item label="审批结果" name="result" :rules="[{ required: true, message: '请选择审批结果' }]">
            <a-radio-group v-model:value="approveForm.result">
              <a-radio value="approve">通过</a-radio>
              <a-radio value="reject">拒绝</a-radio>
            </a-radio-group>
          </a-form-item>
          <a-form-item label="审批意见" name="comment">
            <a-textarea
              v-model:value="approveForm.comment"
              placeholder="请输入审批意见"
              :rows="4"
            />
          </a-form-item>
        </a-form>
      </a-modal>

      <!-- 转办对话框 -->
      <a-modal
        v-model:visible="transferVisible"
        title="转办任务"
        width="600px"
        @ok="handleTransferConfirm"
      >
        <a-form :model="transferForm" layout="vertical">
          <a-form-item
            label="转办人"
            name="assignee"
            :rules="[{ required: true, message: '请选择转办人' }]"
          >
            <a-select
              v-model:value="transferForm.assignee"
              placeholder="请选择转办人"
              :options="userOptions"
            />
          </a-form-item>
          <a-form-item label="转办说明" name="comment">
            <a-textarea
              v-model:value="transferForm.comment"
              placeholder="请输入转办说明"
              :rows="4"
            />
          </a-form-item>
        </a-form>
      </a-modal>

      <!-- 查看任务对话框 -->
      <a-modal
        v-model:visible="viewVisible"
        title="查看任务"
        width="800px"
        :footer="null"
      >
        <a-descriptions bordered>
          <a-descriptions-item label="流程名称">
            {{ currentTask?.workflowName }}
          </a-descriptions-item>
          <a-descriptions-item label="节点名称">
            {{ currentTask?.nodeName }}
          </a-descriptions-item>
          <a-descriptions-item label="任务状态">
            <a-tag :color="getStatusColor(currentTask?.status)">
              {{ getStatusText(currentTask?.status) }}
            </a-tag>
          </a-descriptions-item>
          <a-descriptions-item label="创建时间">
            {{ currentTask?.createTime }}
          </a-descriptions-item>
          <a-descriptions-item label="完成时间">
            {{ currentTask?.completeTime || '-' }}
          </a-descriptions-item>
          <a-descriptions-item label="处理人">
            {{ currentTask?.assignee || '-' }}
          </a-descriptions-item>
        </a-descriptions>

        <!-- 表单数据 -->
        <div class="form-data" v-if="currentTask?.formData">
          <h3>表单数据</h3>
          <a-form layout="vertical">
            <template v-for="(value, key) in currentTask.formData" :key="key">
              <a-form-item :label="key">
                <a-input :value="value" disabled />
              </a-form-item>
            </template>
          </a-form>
        </div>
      </a-modal>
    </a-card>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { TablePaginationConfig } from 'ant-design-vue'
import type { WorkflowTask } from '@/api/workflow/types'

// 查询参数
const queryParams = reactive({
  workflowName: '',
  nodeName: '',
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

// 数据列表
const taskList = ref<WorkflowTask[]>([])
const loading = ref(false)
const pagination = reactive<TablePaginationConfig>({
  total: 0,
  current: 1,
  pageSize: 10,
  showSizeChanger: true,
  showQuickJumper: true
})

// 表格选择
const selectedRowKeys = ref<string[]>([])
const rowSelection = {
  selectedRowKeys: selectedRowKeys.value,
  onChange: (keys: string[]) => {
    selectedRowKeys.value = keys
  }
}

// 审批相关
const approveVisible = ref(false)
const approveForm = reactive({
  result: 'approve',
  comment: ''
})
const currentTask = ref<WorkflowTask>()

// 转办相关
const transferVisible = ref(false)
const transferForm = reactive({
  assignee: undefined,
  comment: ''
})
const userOptions = ref([])

// 查看相关
const viewVisible = ref(false)

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
  queryParams.nodeName = ''
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

// 批量通过
const handleBatchApprove = async () => {
  if (selectedRowKeys.value.length === 0) {
    message.warning('请选择要审批的任务')
    return
  }
  try {
    // TODO: 调用批量通过API
    message.success('操作成功')
    handleQuery()
  } catch (error) {
    message.error('操作失败')
  }
}

// 审批任务
const handleApprove = (record: WorkflowTask) => {
  currentTask.value = record
  approveVisible.value = true
}

// 确认审批
const handleApproveConfirm = async () => {
  try {
    // TODO: 调用审批API
    approveVisible.value = false
    message.success('操作成功')
    handleQuery()
  } catch (error) {
    message.error('操作失败')
  }
}

// 转办任务
const handleTransfer = (record: WorkflowTask) => {
  currentTask.value = record
  transferVisible.value = true
}

// 确认转办
const handleTransferConfirm = async () => {
  try {
    // TODO: 调用转办API
    transferVisible.value = false
    message.success('操作成功')
    handleQuery()
  } catch (error) {
    message.error('操作失败')
  }
}

// 查看任务
const handleView = (record: WorkflowTask) => {
  currentTask.value = record
  viewVisible.value = true
}

// 导出列表
const handleExport = () => {
  message.info('导出功能开发中')
}

// 获取状态颜色
const getStatusColor = (status?: string) => {
  const colorMap: Record<string, string> = {
    pending: 'warning',
    processing: 'processing',
    completed: 'success'
  }
  return colorMap[status || ''] || 'default'
}

// 获取状态文本
const getStatusText = (status?: string) => {
  const textMap: Record<string, string> = {
    pending: '待处理',
    processing: '处理中',
    completed: '已完成'
  }
  return textMap[status || ''] || status || '-'
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

.form-data {
  margin-top: 24px;
  padding-top: 24px;
  border-top: 1px solid #d9d9d9;

  h3 {
    margin-bottom: 16px;
  }
}
</style> 
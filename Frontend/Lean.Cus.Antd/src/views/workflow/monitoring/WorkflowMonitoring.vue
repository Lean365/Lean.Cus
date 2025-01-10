<!-- 工作流监控 -->
<template>
  <div class="workflow-monitoring">
    <a-card class="monitoring-card">
      <template #title>
        <div class="monitoring-header">
          <span>工作流监控</span>
          <div class="monitoring-actions">
            <a-button @click="refreshData">刷新</a-button>
          </div>
        </div>
      </template>
      <div class="monitoring-content">
        <!-- 统计卡片 -->
        <div class="statistics-cards">
          <a-row :gutter="16">
            <a-col :span="6">
              <a-card>
                <statistic
                  title="运行中的工作流"
                  :value="statistics.running"
                  :value-style="{ color: '#1890ff' }"
                >
                  <template #prefix>
                    <a-icon type="sync" spin />
                  </template>
                </statistic>
              </a-card>
            </a-col>
            <a-col :span="6">
              <a-card>
                <statistic
                  title="已完成的工作流"
                  :value="statistics.completed"
                  :value-style="{ color: '#52c41a' }"
                >
                  <template #prefix>
                    <a-icon type="check-circle" />
                  </template>
                </statistic>
              </a-card>
            </a-col>
            <a-col :span="6">
              <a-card>
                <statistic
                  title="暂停的工作流"
                  :value="statistics.suspended"
                  :value-style="{ color: '#faad14' }"
                >
                  <template #prefix>
                    <a-icon type="pause-circle" />
                  </template>
                </statistic>
              </a-card>
            </a-col>
            <a-col :span="6">
              <a-card>
                <statistic
                  title="终止的工作流"
                  :value="statistics.terminated"
                  :value-style="{ color: '#ff4d4f' }"
                >
                  <template #prefix>
                    <a-icon type="stop" />
                  </template>
                </statistic>
              </a-card>
            </a-col>
          </a-row>
        </div>

        <!-- 工作流列表 -->
        <a-table
          :columns="columns"
          :data-source="workflows"
          :loading="loading"
          :pagination="pagination"
          @change="handleTableChange"
        >
          <!-- 工作流名称 -->
          <template #name="{ text, record }">
            <a @click="showWorkflowDetails(record)">{{ text }}</a>
          </template>

          <!-- 工作流状态 -->
          <template #status="{ text }">
            <a-tag :color="getStatusColor(text)">
              {{ getStatusText(text) }}
            </a-tag>
          </template>

          <!-- 进度 -->
          <template #progress="{ text }">
            <a-progress :percent="text" size="small" />
          </template>

          <!-- 操作 -->
          <template #action="{ record }">
            <a-space>
              <a-button
                type="link"
                size="small"
                @click="showWorkflowDetails(record)"
              >
                详情
              </a-button>
              <a-button
                v-if="record.status === 'InProgress'"
                type="link"
                size="small"
                @click="suspendWorkflow(record)"
              >
                暂停
              </a-button>
              <a-button
                v-if="record.status === 'Draft'"
                type="link"
                size="small"
                @click="resumeWorkflow(record)"
              >
                恢复
              </a-button>
              <a-button
                type="link"
                size="small"
                danger
                @click="terminateWorkflow(record)"
              >
                终止
              </a-button>
            </a-space>
          </template>
        </a-table>
      </div>
    </a-card>

    <!-- 工作流详情抽屉 -->
    <a-drawer
      v-model:visible="drawerVisible"
      title="工作流详情"
      width="800"
      :footer-style="{ textAlign: 'right' }"
      @close="closeDrawer"
    >
      <template v-if="selectedWorkflow">
        <!-- 基本信息 -->
        <a-descriptions title="基本信息" bordered>
          <a-descriptions-item label="工作流ID">
            {{ selectedWorkflow.id }}
          </a-descriptions-item>
          <a-descriptions-item label="工作流名称">
            {{ selectedWorkflow.name }}
          </a-descriptions-item>
          <a-descriptions-item label="创建时间">
            {{ formatDateTime(selectedWorkflow.createTime) }}
          </a-descriptions-item>
          <a-descriptions-item label="状态">
            <a-tag :color="getStatusColor(selectedWorkflow.status)">
              {{ getStatusText(selectedWorkflow.status) }}
            </a-tag>
          </a-descriptions-item>
          <a-descriptions-item label="开始时间">
            {{ formatDateTime(selectedWorkflow.startTime) }}
          </a-descriptions-item>
          <a-descriptions-item label="结束时间">
            {{ formatDateTime(selectedWorkflow.endTime) }}
          </a-descriptions-item>
        </a-descriptions>

        <!-- 节点执行记录 -->
        <a-divider orientation="left">节点执行记录</a-divider>
        <a-timeline>
          <a-timeline-item
            v-for="node in selectedWorkflow.nodes"
            :key="node.id"
            :color="getNodeStatusColor(node.status)"
          >
            <template #dot>
              <a-icon :type="getNodeStatusIcon(node.status)" />
            </template>
            <div class="node-info">
              <div class="node-header">
                <span class="node-name">{{ node.name }}</span>
                <span class="node-time">
                  {{ formatDateTime(node.startTime) }}
                </span>
              </div>
              <div class="node-content">
                <div class="node-status">
                  <a-tag :color="getNodeStatusColor(node.status)">
                    {{ getNodeStatusText(node.status) }}
                  </a-tag>
                </div>
                <div v-if="node.assignee" class="node-assignee">
                  处理人：{{ node.assignee }}
                </div>
                <div v-if="node.comment" class="node-comment">
                  处理意见：{{ node.comment }}
                </div>
              </div>
            </div>
          </a-timeline-item>
        </a-timeline>

        <!-- 表单数据 -->
        <template v-if="selectedWorkflow.formData">
          <a-divider orientation="left">表单数据</a-divider>
          <a-descriptions bordered>
            <a-descriptions-item
              v-for="(value, key) in selectedWorkflow.formData"
              :key="key"
              :label="key"
            >
              {{ value }}
            </a-descriptions-item>
          </a-descriptions>
        </template>
      </template>
    </a-drawer>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue';
import { message } from 'ant-design-vue';
import type { TablePaginationConfig } from 'ant-design-vue';
import dayjs from 'dayjs';
import {
  getWorkflowList,
  getWorkflowDetails,
  suspendWorkflow as suspendWorkflowApi,
  resumeWorkflow as resumeWorkflowApi,
  terminateWorkflow as terminateWorkflowApi,
} from '@/api/workflow';

// 表格列定义
const columns = [
  {
    title: '工作流名称',
    dataIndex: 'name',
    slots: { customRender: 'name' },
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    sorter: true,
  },
  {
    title: '状态',
    dataIndex: 'status',
    slots: { customRender: 'status' },
  },
  {
    title: '进度',
    dataIndex: 'progress',
    slots: { customRender: 'progress' },
  },
  {
    title: '操作',
    key: 'action',
    slots: { customRender: 'action' },
  },
];

// 状态数据
const loading = ref(false);
const drawerVisible = ref(false);
const selectedWorkflow = ref(null);
const workflows = ref([]);
const statistics = reactive({
  running: 0,
  completed: 0,
  suspended: 0,
  terminated: 0,
});

// 分页配置
const pagination = reactive<TablePaginationConfig>({
  current: 1,
  pageSize: 10,
  total: 0,
  showSizeChanger: true,
  showQuickJumper: true,
});

// 加载数据
const loadData = async () => {
  loading.value = true;
  try {
    const { data, total } = await getWorkflowList({
      page: pagination.current,
      pageSize: pagination.pageSize,
    });
    workflows.value = data;
    pagination.total = total;

    // 更新统计数据
    statistics.running = data.filter(w => w.status === 'InProgress').length;
    statistics.completed = data.filter(w => w.status === 'Completed').length;
    statistics.suspended = data.filter(w => w.status === 'Draft').length;
    statistics.terminated = data.filter(w => w.status === 'Cancelled').length;
  } catch (error) {
    message.error('加载数据失败');
  } finally {
    loading.value = false;
  }
};

// 表格变化处理
const handleTableChange = (pag: TablePaginationConfig) => {
  pagination.current = pag.current || 1;
  pagination.pageSize = pag.pageSize || 10;
  loadData();
};

// 刷新数据
const refreshData = () => {
  loadData();
};

// 显示工作流详情
const showWorkflowDetails = async (workflow: any) => {
  try {
    const details = await getWorkflowDetails(workflow.id);
    selectedWorkflow.value = details;
    drawerVisible.value = true;
  } catch (error) {
    message.error('获取工作流详情失败');
  }
};

// 关闭抽屉
const closeDrawer = () => {
  drawerVisible.value = false;
  selectedWorkflow.value = null;
};

// 暂停工作流
const suspendWorkflow = async (workflow: any) => {
  try {
    await suspendWorkflowApi(workflow.id);
    message.success('工作流已暂停');
    loadData();
  } catch (error) {
    message.error('暂停工作流失败');
  }
};

// 恢复工作流
const resumeWorkflow = async (workflow: any) => {
  try {
    await resumeWorkflowApi(workflow.id);
    message.success('工作流已恢复');
    loadData();
  } catch (error) {
    message.error('恢复工作流失败');
  }
};

// 终止工作流
const terminateWorkflow = async (workflow: any) => {
  try {
    await terminateWorkflowApi(workflow.id);
    message.success('工作流已终止');
    loadData();
  } catch (error) {
    message.error('终止工作流失败');
  }
};

// 工具方法
const getStatusColor = (status: string) => {
  switch (status) {
    case 'InProgress':
      return 'blue';
    case 'Completed':
      return 'green';
    case 'Draft':
      return 'orange';
    case 'Cancelled':
      return 'red';
    default:
      return 'default';
  }
};

const getStatusText = (status: string) => {
  switch (status) {
    case 'InProgress':
      return '进行中';
    case 'Completed':
      return '已完成';
    case 'Draft':
      return '已暂停';
    case 'Cancelled':
      return '已终止';
    default:
      return '未知';
  }
};

const getNodeStatusColor = (status: string) => {
  switch (status) {
    case 'Completed':
      return 'green';
    case 'InProgress':
      return 'blue';
    case 'Pending':
      return 'orange';
    case 'Failed':
      return 'red';
    default:
      return 'gray';
  }
};

const getNodeStatusIcon = (status: string) => {
  switch (status) {
    case 'Completed':
      return 'check-circle';
    case 'InProgress':
      return 'sync';
    case 'Pending':
      return 'clock-circle';
    case 'Failed':
      return 'close-circle';
    default:
      return 'question-circle';
  }
};

const getNodeStatusText = (status: string) => {
  switch (status) {
    case 'Completed':
      return '已完成';
    case 'InProgress':
      return '进行中';
    case 'Pending':
      return '待处理';
    case 'Failed':
      return '失败';
    default:
      return '未知';
  }
};

const formatDateTime = (date: string | Date) => {
  if (!date) return '';
  return dayjs(date).format('YYYY-MM-DD HH:mm:ss');
};

// 初始化
onMounted(() => {
  loadData();
});
</script>

<style lang="less" scoped>
.workflow-monitoring {
  height: 100%;
  padding: 16px;

  .monitoring-card {
    height: 100%;
  }

  .monitoring-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
  }

  .monitoring-content {
    .statistics-cards {
      margin-bottom: 24px;
    }

    .node-info {
      .node-header {
        display: flex;
        justify-content: space-between;
        margin-bottom: 8px;

        .node-name {
          font-weight: bold;
        }

        .node-time {
          color: rgba(0, 0, 0, 0.45);
        }
      }

      .node-content {
        .node-status {
          margin-bottom: 4px;
        }

        .node-assignee,
        .node-comment {
          color: rgba(0, 0, 0, 0.65);
          margin-bottom: 4px;
        }
      }
    }
  }
}
</style> 
<script setup lang="ts">
import { ref, onMounted } from 'vue'
import type { TableColumnsType } from 'ant-design-vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'

const { t } = useI18n()

// 搜索表单
const searchForm = ref({
  instanceId: '',
  operator: ''
})

// 表格列定义
const columns = ref<TableColumnsType>([
  {
    title: t('workflow.history.instance'),
    dataIndex: 'instanceId',
    key: 'instanceId',
    width: 180
  },
  {
    title: t('workflow.history.node'),
    dataIndex: 'nodeName',
    key: 'nodeName',
    width: 150
  },
  {
    title: t('workflow.history.operator'),
    dataIndex: 'operator',
    key: 'operator',
    width: 120
  },
  {
    title: t('workflow.history.action'),
    dataIndex: 'action',
    key: 'action',
    width: 120
  },
  {
    title: t('workflow.history.comment'),
    dataIndex: 'comment',
    key: 'comment',
    width: 200
  },
  {
    title: t('workflow.history.time'),
    dataIndex: 'operateTime',
    key: 'operateTime',
    width: 180
  }
])

// 表格数据
const dataSource = ref([])

// 加载数据
const loading = ref(false)
const fetchData = async () => {
  loading.value = true
  try {
    // TODO: 调用API获取数据
    // const response = await getWorkflowHistory()
    // dataSource.value = response.data
  } catch (error) {
    message.error(t('app.common.error'))
  } finally {
    loading.value = false
  }
}

// 搜索
const handleSearch = () => {
  fetchData()
}

// 重置
const handleReset = () => {
  searchForm.value = {
    instanceId: '',
    operator: ''
  }
  fetchData()
}

// 详情抽屉
const drawerVisible = ref(false)
const currentRecord = ref<any>({})

// 查看详情
const handleView = (record: any) => {
  currentRecord.value = record
  drawerVisible.value = true
}

// 页面加载时获取数据
onMounted(() => {
  fetchData()
})
</script>

<template>
  <div class="workflow-history">
    <a-card :bordered="false">
      <!-- 搜索表单 -->
      <a-form layout="inline" class="search-form">
        <a-form-item :label="t('workflow.history.instance')">
          <a-input v-model:value="searchForm.instanceId" allow-clear />
        </a-form-item>
        <a-form-item :label="t('workflow.history.operator')">
          <a-input v-model:value="searchForm.operator" allow-clear />
        </a-form-item>
        <a-form-item>
          <a-button type="primary" @click="handleSearch">
            <template #icon>
              <SearchOutlined />
            </template>
            {{ t('app.common.search') }}
          </a-button>
          <a-button style="margin-left: 8px" @click="handleReset">
            {{ t('app.common.reset') }}
          </a-button>
        </a-form-item>
      </a-form>

      <!-- 数据表格 -->
      <a-table
        :columns="columns"
        :data-source="dataSource"
        :loading="loading"
        :pagination="{
          showSizeChanger: true,
          showQuickJumper: true,
          showTotal: (total: number) => t('app.common.total', { total })
        }"
        bordered
        size="middle"
        row-key="id"
      >
        <!-- 操作列 -->
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'action'">
            <a-button type="link" size="small" @click="handleView(record)">
              {{ t('app.common.view') }}
            </a-button>
          </template>
        </template>
      </a-table>
    </a-card>

    <!-- 详情抽屉 -->
    <a-drawer
      v-model:visible="drawerVisible"
      :title="t('workflow.history.title')"
      placement="right"
      width="600"
    >
      <a-descriptions :column="1" bordered>
        <a-descriptions-item :label="t('workflow.history.instance')">
          {{ currentRecord.instanceId }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.history.node')">
          {{ currentRecord.nodeName }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.history.operator')">
          {{ currentRecord.operator }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.history.action')">
          {{ currentRecord.action }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.history.comment')">
          {{ currentRecord.comment }}
        </a-descriptions-item>
        <a-descriptions-item :label="t('workflow.history.time')">
          {{ currentRecord.operateTime }}
        </a-descriptions-item>
      </a-descriptions>
    </a-drawer>
  </div>
</template>

<style lang="less" scoped>
.workflow-history {
  .search-form {
    margin-bottom: 24px;
  }
}
</style> 
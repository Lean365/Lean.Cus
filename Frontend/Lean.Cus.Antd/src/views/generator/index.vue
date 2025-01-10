<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.vue
 * 文件功能描述：代码生成页面
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ------------------------------------------------------------------>

<template>
  <div>
    <!-- 搜索表单 -->
    <a-form layout="inline" :model="searchForm" @submit="handleSearch">
      <a-form-item label="表名">
        <a-input v-model:value="searchForm.tableName" placeholder="请输入表名" />
      </a-form-item>
      <a-form-item>
        <a-button type="primary" html-type="submit">搜索</a-button>
        <a-button style="margin-left: 8px" @click="handleReset">重置</a-button>
      </a-form-item>
    </a-form>

    <!-- 工具栏 -->
    <div style="margin-bottom: 16px">
      <a-button type="primary" @click="handleImport">导入</a-button>
      <a-button style="margin-left: 8px" @click="handleDesign">设计</a-button>
    </div>

    <!-- 数据表格 -->
    <a-table :columns="columns" :data-source="dataSource" :loading="loading" rowKey="id">
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <a-space>
            <a @click="handlePreview(record)">预览</a>
            <a-divider type="vertical" />
            <a @click="handleGenerate(record)">生成</a>
            <a-divider type="vertical" />
            <a @click="handleDownload(record)">下载</a>
          </a-space>
        </template>
      </template>
    </a-table>

    <!-- 预览对话框 -->
    <a-modal
      v-model:visible="previewVisible"
      title="预览代码"
      width="800px"
      :footer="null"
      @cancel="handlePreviewCancel"
    >
      <a-tabs v-model:activeKey="previewActiveKey">
        <a-tab-pane v-for="(code, fileName) in previewCodes" :key="fileName" :tab="fileName">
          <pre>{{ code }}</pre>
        </a-tab-pane>
      </a-tabs>
    </a-modal>

    <!-- 生成对话框 -->
    <a-modal
      v-model:visible="generateVisible"
      title="生成代码"
      :confirm-loading="generateLoading"
      @ok="handleGenerateOk"
      @cancel="handleGenerateCancel"
    >
      <a-form :model="generateForm" :rules="generateRules" ref="generateFormRef">
        <a-form-item label="生成路径" name="generatePath">
          <a-input v-model:value="generateForm.generatePath" placeholder="请输入生成路径" />
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { previewCode, generateCode, downloadCode } from '@/api/generator/generator'
import { useRouter } from 'vue-router'

// 路由
const router = useRouter()

// 搜索表单
const searchForm = reactive({
  tableName: ''
})

// 数据表格
const columns = [
  {
    title: '表名',
    dataIndex: 'tableName',
    key: 'tableName'
  },
  {
    title: '表注释',
    dataIndex: 'tableComment',
    key: 'tableComment'
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime'
  },
  {
    title: '更新时间',
    dataIndex: 'updateTime',
    key: 'updateTime'
  },
  {
    title: '操作',
    key: 'action',
    width: 200
  }
]
const dataSource = ref<any[]>([])
const loading = ref(false)

// 预览对话框
const previewVisible = ref(false)
const previewActiveKey = ref('')
const previewCodes = ref<Record<string, string>>({})

// 生成对话框
const generateVisible = ref(false)
const generateLoading = ref(false)
const generateForm = reactive({
  generatePath: ''
})
const generateRules = {
  generatePath: [{ required: true, message: '请输入生成路径' }]
}
const generateFormRef = ref<FormInstance>()
const generateTableId = ref<number>()

// 加载数据
const loadData = async () => {
  loading.value = true
  try {
    // TODO: 加载数据
  } catch (error) {
    console.error(error)
  } finally {
    loading.value = false
  }
}

// 搜索
const handleSearch = () => {
  loadData()
}

// 重置
const handleReset = () => {
  searchForm.tableName = ''
  loadData()
}

// 导入
const handleImport = () => {
  router.push('/generator/import')
}

// 设计
const handleDesign = () => {
  router.push('/generator/design')
}

// 预览
const handlePreview = async (record: any) => {
  try {
    const data = await previewCode(record.id)
    previewCodes.value = data
    previewActiveKey.value = Object.keys(data)[0]
    previewVisible.value = true
  } catch (error) {
    console.error(error)
  }
}

// 取消预览
const handlePreviewCancel = () => {
  previewVisible.value = false
}

// 生成
const handleGenerate = (record: any) => {
  generateTableId.value = record.id
  generateForm.generatePath = ''
  generateVisible.value = true
}

// 确定生成
const handleGenerateOk = async () => {
  try {
    await generateFormRef.value?.validate()
    generateLoading.value = true
    await generateCode(generateTableId.value!, generateForm.generatePath)
    message.success('生成成功')
    generateVisible.value = false
  } catch (error) {
    console.error(error)
  } finally {
    generateLoading.value = false
  }
}

// 取消生成
const handleGenerateCancel = () => {
  generateVisible.value = false
}

// 下载
const handleDownload = async (record: any) => {
  try {
    const data = await downloadCode(record.id)
    const blob = new Blob([data], { type: 'application/zip' })
    const url = window.URL.createObjectURL(blob)
    const link = document.createElement('a')
    link.href = url
    link.download = 'code.zip'
    link.click()
    window.URL.revokeObjectURL(url)
  } catch (error) {
    console.error(error)
  }
}

// 初始化
onMounted(() => {
  loadData()
})
</script>

<style lang="less" scoped>
.app-container {
  padding: 24px;
}
</style> 
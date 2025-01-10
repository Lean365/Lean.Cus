<!-- 模板管理页面 -->
<template>
  <div>
    <!-- 搜索表单 -->
    <a-form layout="inline" :model="searchForm" @submit="handleSearch">
      <a-form-item label="模板类型">
        <a-input v-model:value="searchForm.templateType" placeholder="请输入模板类型" />
      </a-form-item>
      <a-form-item>
        <a-button type="primary" html-type="submit">搜索</a-button>
        <a-button style="margin-left: 8px" @click="handleReset">重置</a-button>
      </a-form-item>
    </a-form>

    <!-- 工具栏 -->
    <div style="margin-bottom: 16px">
      <a-button type="primary" @click="handleAdd">新增</a-button>
    </div>

    <!-- 数据表格 -->
    <a-table :columns="columns" :data-source="dataSource" :loading="loading" rowKey="id">
      <template #bodyCell="{ column, record }">
        <template v-if="column.key === 'action'">
          <a-space>
            <a @click="handleEdit(record)">编辑</a>
            <a-divider type="vertical" />
            <a-popconfirm title="确定要删除吗？" @confirm="handleDelete(record)">
              <a>删除</a>
            </a-popconfirm>
          </a-space>
        </template>
      </template>
    </a-table>

    <!-- 编辑对话框 -->
    <a-modal
      v-model:visible="modalVisible"
      :title="modalTitle"
      :confirm-loading="modalLoading"
      @ok="handleModalOk"
      @cancel="handleModalCancel"
    >
      <a-form :model="modalForm" :rules="modalRules" ref="modalFormRef">
        <a-form-item label="模板类型" name="templateType">
          <a-input v-model:value="modalForm.templateType" placeholder="请输入模板类型" />
        </a-form-item>
        <a-form-item label="文件名" name="fileName">
          <a-input v-model:value="modalForm.fileName" placeholder="请输入文件名" />
        </a-form-item>
        <a-form-item label="模板内容" name="templateContent">
          <a-textarea v-model:value="modalForm.templateContent" placeholder="请输入模板内容" :rows="10" />
        </a-form-item>
        <a-form-item label="备注" name="remark">
          <a-textarea v-model:value="modalForm.remark" placeholder="请输入备注" :rows="4" />
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import {
  LeanTemplateDto,
  createTemplate,
  updateTemplate,
  deleteTemplate,
  getTemplateList
} from '@/api/generator/template'

// 搜索表单
const searchForm = reactive({
  templateType: ''
})

// 数据表格
const columns = [
  {
    title: '模板类型',
    dataIndex: 'templateType',
    key: 'templateType'
  },
  {
    title: '文件名',
    dataIndex: 'fileName',
    key: 'fileName'
  },
  {
    title: '备注',
    dataIndex: 'remark',
    key: 'remark'
  },
  {
    title: '操作',
    key: 'action',
    width: 150
  }
]
const dataSource = ref<LeanTemplateDto[]>([])
const loading = ref(false)

// 编辑对话框
const modalVisible = ref(false)
const modalTitle = ref('')
const modalLoading = ref(false)
const modalForm = reactive<LeanTemplateDto>({
  templateType: '',
  fileName: '',
  templateContent: '',
  remark: ''
})
const modalRules = {
  templateType: [{ required: true, message: '请输入模板类型' }],
  fileName: [{ required: true, message: '请输入文件名' }],
  templateContent: [{ required: true, message: '请输入模板内容' }]
}
const modalFormRef = ref<FormInstance>()
const modalId = ref<number>()

// 加载数据
const loadData = async () => {
  loading.value = true
  try {
    const data = await getTemplateList(searchForm.templateType)
    dataSource.value = data
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
  searchForm.templateType = ''
  loadData()
}

// 新增
const handleAdd = () => {
  modalTitle.value = '新增模板'
  modalId.value = undefined
  modalForm.templateType = ''
  modalForm.fileName = ''
  modalForm.templateContent = ''
  modalForm.remark = ''
  modalVisible.value = true
}

// 编辑
const handleEdit = (record: LeanTemplateDto) => {
  modalTitle.value = '编辑模板'
  modalId.value = record.id
  modalForm.templateType = record.templateType
  modalForm.fileName = record.fileName
  modalForm.templateContent = record.templateContent
  modalForm.remark = record.remark
  modalVisible.value = true
}

// 删除
const handleDelete = async (record: LeanTemplateDto) => {
  try {
    await deleteTemplate(record.id!)
    message.success('删除成功')
    loadData()
  } catch (error) {
    console.error(error)
  }
}

// 确定
const handleModalOk = async () => {
  try {
    await modalFormRef.value?.validate()
    modalLoading.value = true
    if (modalId.value) {
      await updateTemplate(modalId.value, modalForm)
      message.success('更新成功')
    } else {
      await createTemplate(modalForm)
      message.success('创建成功')
    }
    modalVisible.value = false
    loadData()
  } catch (error) {
    console.error(error)
  } finally {
    modalLoading.value = false
  }
}

// 取消
const handleModalCancel = () => {
  modalVisible.value = false
}

// 初始化
onMounted(() => {
  loadData()
})
</script> 
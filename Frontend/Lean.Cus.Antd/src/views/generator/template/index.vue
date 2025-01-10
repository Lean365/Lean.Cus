<!-- 模板管理页面 -->
<template>
  <div class="template-container">
    <a-card class="template-card">
      <template #title>
        <div class="card-title">
          <span>模板管理</span>
          <a-button type="primary" @click="handleAdd">
            新增模板
          </a-button>
        </div>
      </template>

      <a-table
        :columns="columns"
        :data-source="templateList"
        :loading="loading"
        :pagination="false"
        row-key="id"
      >
        <template #bodyCell="{ column, record }">
          <template v-if="column.key === 'action'">
            <a-space>
              <a-button type="link" @click="handleEdit(record)">编辑</a-button>
              <a-popconfirm
                title="确定要删除这个模板吗？"
                @confirm="handleDelete(record.id)"
              >
                <a-button type="link" danger>删除</a-button>
              </a-popconfirm>
            </a-space>
          </template>
        </template>
      </a-table>
    </a-card>

    <a-modal
      v-model:visible="modalVisible"
      :title="modalTitle"
      @ok="handleModalOk"
      @cancel="handleModalCancel"
      width="800px"
    >
      <a-form
        ref="formRef"
        :model="formState"
        :rules="rules"
        layout="vertical"
      >
        <a-form-item label="模板名称" name="name">
          <a-input v-model:value="formState.name" placeholder="请输入模板名称" />
        </a-form-item>
        <a-form-item label="模板描述" name="description">
          <a-textarea
            v-model:value="formState.description"
            placeholder="请输入模板描述"
            :rows="2"
          />
        </a-form-item>
        <a-form-item label="模板类型" name="type">
          <a-select v-model:value="formState.type" placeholder="请选择模板类型">
            <a-select-option :value="1">实体模板</a-select-option>
            <a-select-option :value="2">服务模板</a-select-option>
            <a-select-option :value="3">控制器模板</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="模板内容" name="content">
          <a-textarea
            v-model:value="formState.content"
            placeholder="请输入模板内容"
            :rows="10"
          />
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted } from 'vue'
import { message } from 'ant-design-vue'
import type { Template } from '@/api/generator/template'
import type { Rule } from 'ant-design-vue/es/form'
import {
  getTemplateList,
  createTemplate,
  updateTemplate,
  deleteTemplate
} from '@/api/generator/template'

// 表格列定义
const columns = [
  {
    title: '模板名称',
    dataIndex: 'name',
    key: 'name'
  },
  {
    title: '模板描述',
    dataIndex: 'description',
    key: 'description'
  },
  {
    title: '模板类型',
    dataIndex: 'type',
    key: 'type',
    customRender: ({ text }: { text: number }) => {
      const typeMap = {
        1: '实体模板',
        2: '服务模板',
        3: '控制器模板'
      }
      return typeMap[text as keyof typeof typeMap] || text
    }
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

// 数据状态
const loading = ref(false)
const templateList = ref<Template[]>([])
const modalVisible = ref(false)
const modalTitle = ref('')
const formRef = ref()
const formState = ref<Partial<Template>>({
  name: '',
  description: '',
  type: undefined,
  content: ''
})

// 表单验证规则
const rules: Record<string, Rule[]> = {
  name: [{ required: true, type: 'string', message: '请输入模板名称', trigger: 'blur' }],
  type: [{ required: true, type: 'number', message: '请选择模板类型', trigger: 'change' }],
  content: [{ required: true, type: 'string', message: '请输入模板内容', trigger: 'blur' }]
}

// 获取模板列表
const fetchTemplateList = async () => {
  loading.value = true
  try {
    const data = await getTemplateList()
    templateList.value = data
  } catch (error) {
    message.error('获取模板列表失败')
  } finally {
    loading.value = false
  }
}

// 新增模板
const handleAdd = () => {
  modalTitle.value = '新增模板'
  formState.value = {
    name: '',
    description: '',
    type: undefined,
    content: ''
  }
  modalVisible.value = true
}

// 编辑模板
const handleEdit = (record: Template) => {
  modalTitle.value = '编辑模板'
  formState.value = { ...record }
  modalVisible.value = true
}

// 删除模板
const handleDelete = async (id: string) => {
  try {
    await deleteTemplate(id)
    message.success('删除成功')
    fetchTemplateList()
  } catch (error) {
    message.error('删除失败')
  }
}

// 模态框确认
const handleModalOk = () => {
  formRef.value?.validate().then(async () => {
    try {
      if (formState.value.id) {
        await updateTemplate(formState.value)
        message.success('更新成功')
      } else {
        await createTemplate(formState.value)
        message.success('创建成功')
      }
      modalVisible.value = false
      fetchTemplateList()
    } catch (error) {
      message.error(formState.value.id ? '更新失败' : '创建失败')
    }
  })
}

// 模态框取消
const handleModalCancel = () => {
  modalVisible.value = false
  formRef.value?.resetFields()
}

// 页面加载时获取模板列表
onMounted(() => {
  fetchTemplateList()
})
</script>

<style scoped>
.template-container {
  padding: 24px;
}

.template-card {
  background: #fff;
}

.card-title {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style> 
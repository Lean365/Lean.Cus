<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.vue
 * 文件功能描述：工作流表单设计器页面
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ------------------------------------------------------------------>

<template>
  <div class="app-container">
    <a-card :bordered="false">
      <!-- 工具栏 -->
      <div class="toolbar">
        <a-space>
          <a-button type="primary" @click="handleSave">保存</a-button>
          <a-button @click="handlePreview">预览</a-button>
          <a-button @click="handleImport">导入</a-button>
          <a-button @click="handleExport">导出</a-button>
        </a-space>
      </div>

      <!-- 设计器区域 -->
      <div class="designer">
        <!-- 左侧组件面板 -->
        <div class="component-panel">
          <h3>表单组件</h3>
          <div class="component-list">
            <div
              v-for="component in formComponents"
              :key="component.type"
              class="component-item"
              draggable="true"
              @dragstart="handleDragStart($event, component)"
            >
              <a-icon :type="component.icon" />
              <span>{{ component.name }}</span>
            </div>
          </div>
        </div>

        <!-- 中间画布 -->
        <div class="canvas">
          <template v-if="formFields.length > 0">
            <draggable
              v-model="formFields"
              group="form-fields"
              item-key="id"
              handle=".drag-handle"
              ghost-class="ghost"
              @start="handleDragStart"
              @end="handleDragEnd"
            >
              <template #item="{ element }">
                <div
                  class="field-item"
                  :class="{ selected: selectedField?.id === element.id }"
                  @click="handleFieldSelect(element)"
                >
                  <div class="drag-handle">
                    <a-icon type="drag" />
                  </div>
                  <div class="field-content">
                    <component
                      :is="getFieldComponent(element.type)"
                      v-model:value="element.value"
                      v-bind="element.properties"
                      :disabled="true"
                    />
                  </div>
                  <div class="field-actions">
                    <a-icon type="copy" @click.stop="handleFieldCopy(element)" />
                    <a-icon type="delete" @click.stop="handleFieldDelete(element)" />
                  </div>
                </div>
              </template>
            </draggable>
          </template>
          <div v-else class="empty-canvas">
            <p>拖拽组件到此处开始设计表单</p>
          </div>
        </div>

        <!-- 右侧属性面板 -->
        <div class="property-panel">
          <template v-if="selectedField">
            <h3>字段属性</h3>
            <a-form :model="selectedField" layout="vertical">
              <a-form-item label="字段名">
                <a-input v-model:value="selectedField.name" />
              </a-form-item>
              <a-form-item label="标签">
                <a-input v-model:value="selectedField.label" />
              </a-form-item>
              <a-form-item label="必填">
                <a-switch v-model:checked="selectedField.required" />
              </a-form-item>
              <!-- 根据字段类型显示不同的属性配置 -->
              <component
                :is="getPropertyComponent(selectedField.type)"
                v-model:value="selectedField.properties"
              />
            </a-form>
          </template>
          <template v-else>
            <div class="empty">请选择一个字段</div>
          </template>
        </div>
      </div>
    </a-card>

    <!-- 预览对话框 -->
    <a-modal
      v-model:visible="previewVisible"
      title="表单预览"
      width="800px"
      :footer="null"
    >
      <a-form :model="previewForm" layout="vertical">
        <template v-for="field in formFields" :key="field.id">
          <a-form-item
            :label="field.label"
            :name="field.name"
            :rules="[{ required: field.required, message: `请输入${field.label}` }]"
          >
            <component
              :is="getFieldComponent(field.type)"
              v-model:value="previewForm[field.name]"
              v-bind="field.properties"
            />
          </a-form-item>
        </template>
      </a-form>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue'
import { message } from 'ant-design-vue'
import { v4 as uuidv4 } from 'uuid'
import draggable from 'vuedraggable'
import type { WorkflowForm, WorkflowFormField } from '@/api/workflow/types'

// 表单组件定义
const formComponents = [
  { type: 'input', name: '单行文本', icon: 'edit' },
  { type: 'textarea', name: '多行文本', icon: 'form' },
  { type: 'number', name: '数字输入', icon: 'number' },
  { type: 'select', name: '下拉选择', icon: 'down' },
  { type: 'radio', name: '单选框组', icon: 'check-circle' },
  { type: 'checkbox', name: '复选框组', icon: 'check-square' },
  { type: 'date', name: '日期选择', icon: 'calendar' },
  { type: 'time', name: '时间选择', icon: 'clock-circle' },
  { type: 'switch', name: '开关', icon: 'poweroff' },
  { type: 'slider', name: '滑块', icon: 'sliders' },
  { type: 'upload', name: '文件上传', icon: 'upload' }
]

// 表单字段列表
const formFields = ref<WorkflowFormField[]>([])

// 选中的字段
const selectedField = ref<WorkflowFormField>()

// 预览相关
const previewVisible = ref(false)
const previewForm = reactive<Record<string, any>>({})

// 拖拽开始
const handleDragStart = (event: DragEvent, component?: typeof formComponents[0]) => {
  if (component) {
    event.dataTransfer?.setData('componentType', component.type)
  }
}

// 拖拽结束
const handleDragEnd = () => {
  // 处理拖拽结束逻辑
}

// 选择字段
const handleFieldSelect = (field: WorkflowFormField) => {
  selectedField.value = field
}

// 复制字段
const handleFieldCopy = (field: WorkflowFormField) => {
  const newField = { ...field, id: uuidv4() }
  const index = formFields.value.findIndex(f => f.id === field.id)
  formFields.value.splice(index + 1, 0, newField)
}

// 删除字段
const handleFieldDelete = (field: WorkflowFormField) => {
  const index = formFields.value.findIndex(f => f.id === field.id)
  formFields.value.splice(index, 1)
  if (selectedField.value?.id === field.id) {
    selectedField.value = undefined
  }
}

// 保存表单
const handleSave = async () => {
  try {
    const form: WorkflowForm = {
      id: uuidv4(),
      name: 'New Form',
      fields: formFields.value
    }
    // TODO: 调用保存API
    message.success('保存成功')
  } catch (error) {
    message.error('保存失败')
  }
}

// 预览表单
const handlePreview = () => {
  previewVisible.value = true
  previewForm.value = {}
}

// 导入表单
const handleImport = () => {
  message.info('导入功能开发中')
}

// 导出表单
const handleExport = () => {
  message.info('导出功能开发中')
}

// 获取字段组件
const getFieldComponent = (type: string) => {
  const componentMap: Record<string, string> = {
    input: 'a-input',
    textarea: 'a-textarea',
    number: 'a-input-number',
    select: 'a-select',
    radio: 'a-radio-group',
    checkbox: 'a-checkbox-group',
    date: 'a-date-picker',
    time: 'a-time-picker',
    switch: 'a-switch',
    slider: 'a-slider',
    upload: 'a-upload'
  }
  return componentMap[type] || 'a-input'
}

// 获取属性组件
const getPropertyComponent = (type: string) => {
  // 根据字段类型返回不同的属性编辑组件
  return 'div'
}
</script>

<style lang="less" scoped>
.app-container {
  padding: 24px;
  height: 100%;
}

.toolbar {
  margin-bottom: 16px;
}

.designer {
  display: flex;
  height: calc(100vh - 200px);
  border: 1px solid #d9d9d9;
  border-radius: 2px;

  .component-panel {
    width: 200px;
    padding: 16px;
    border-right: 1px solid #d9d9d9;
    overflow-y: auto;

    h3 {
      margin-bottom: 16px;
    }

    .component-list {
      .component-item {
        display: flex;
        align-items: center;
        padding: 8px;
        margin-bottom: 8px;
        border: 1px solid #d9d9d9;
        border-radius: 2px;
        cursor: move;

        &:hover {
          background-color: #f5f5f5;
        }

        .anticon {
          margin-right: 8px;
        }
      }
    }
  }

  .canvas {
    flex: 1;
    padding: 16px;
    background-color: #fafafa;
    overflow-y: auto;

    .empty-canvas {
      display: flex;
      align-items: center;
      justify-content: center;
      height: 100%;
      color: #999;
    }

    .field-item {
      display: flex;
      align-items: center;
      padding: 8px;
      margin-bottom: 8px;
      background-color: #fff;
      border: 1px solid #d9d9d9;
      border-radius: 2px;

      &.selected {
        border-color: #1890ff;
      }

      &.ghost {
        opacity: 0.5;
        background-color: #f5f5f5;
      }

      .drag-handle {
        padding: 0 8px;
        cursor: move;
      }

      .field-content {
        flex: 1;
        padding: 0 8px;
      }

      .field-actions {
        padding: 0 8px;

        .anticon {
          margin-left: 8px;
          cursor: pointer;

          &:hover {
            color: #1890ff;
          }
        }
      }
    }
  }

  .property-panel {
    width: 300px;
    padding: 16px;
    border-left: 1px solid #d9d9d9;
    overflow-y: auto;

    h3 {
      margin-bottom: 16px;
    }

    .empty {
      text-align: center;
      color: #999;
      padding: 32px 0;
    }
  }
}
</style> 
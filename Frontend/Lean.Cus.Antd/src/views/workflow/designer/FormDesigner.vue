<!-- 工作流表单设计器 -->
<template>
  <div class="form-designer">
    <a-card class="designer-card">
      <template #title>
        <div class="designer-header">
          <span>表单设计器</span>
          <div class="designer-actions">
            <a-button type="primary" @click="saveForm">保存</a-button>
            <a-button @click="previewForm">预览</a-button>
          </div>
        </div>
      </template>
      <div class="designer-content">
        <!-- 工具栏 -->
        <div class="designer-toolbar">
          <a-card title="字段类型">
            <div
              v-for="fieldType in fieldTypes"
              :key="fieldType.type"
              class="field-type-item"
              draggable="true"
              @dragstart="onDragStart($event, fieldType)"
            >
              <a-icon :type="fieldType.icon" />
              <span>{{ fieldType.name }}</span>
            </div>
          </a-card>
        </div>

        <!-- 设计画布 -->
        <div
          class="designer-canvas"
          @dragover="onDragOver"
          @drop="onDrop"
          @click="onCanvasClick"
        >
          <a-form
            layout="vertical"
            class="form-preview"
          >
            <div
              v-for="field in form.fields"
              :key="field.id"
              class="form-field"
              :class="{ selected: selectedField?.id === field.id }"
              @click.stop="selectField(field)"
            >
              <a-form-item
                :label="field.name"
                :required="field.required"
              >
                <!-- 文本输入 -->
                <a-input
                  v-if="field.type === 'Text'"
                  :placeholder="`请输入${field.name}`"
                  disabled
                />

                <!-- 数字输入 -->
                <a-input-number
                  v-else-if="field.type === 'Number'"
                  :placeholder="`请输入${field.name}`"
                  disabled
                  style="width: 100%"
                />

                <!-- 日期选择 -->
                <a-date-picker
                  v-else-if="field.type === 'Date'"
                  :placeholder="`请选择${field.name}`"
                  disabled
                  style="width: 100%"
                />

                <!-- 时间选择 -->
                <a-time-picker
                  v-else-if="field.type === 'Time'"
                  :placeholder="`请选择${field.name}`"
                  disabled
                  style="width: 100%"
                />

                <!-- 日期时间选择 -->
                <a-date-picker
                  v-else-if="field.type === 'DateTime'"
                  :placeholder="`请选择${field.name}`"
                  show-time
                  disabled
                  style="width: 100%"
                />

                <!-- 单选框 -->
                <a-radio-group
                  v-else-if="field.type === 'Radio'"
                  disabled
                >
                  <a-radio
                    v-for="option in field.options"
                    :key="option.value"
                    :value="option.value"
                  >
                    {{ option.label }}
                  </a-radio>
                </a-radio-group>

                <!-- 多选框 -->
                <a-checkbox-group
                  v-else-if="field.type === 'Checkbox'"
                  disabled
                >
                  <a-checkbox
                    v-for="option in field.options"
                    :key="option.value"
                    :value="option.value"
                  >
                    {{ option.label }}
                  </a-checkbox>
                </a-checkbox-group>

                <!-- 下拉选择 -->
                <a-select
                  v-else-if="field.type === 'Select'"
                  :placeholder="`请选择${field.name}`"
                  disabled
                  style="width: 100%"
                >
                  <a-select-option
                    v-for="option in field.options"
                    :key="option.value"
                    :value="option.value"
                  >
                    {{ option.label }}
                  </a-select-option>
                </a-select>

                <!-- 文件上传 -->
                <a-upload
                  v-else-if="field.type === 'File'"
                  disabled
                >
                  <a-button>
                    <a-icon type="upload" /> 点击上传
                  </a-button>
                </a-upload>

                <!-- 图片上传 -->
                <a-upload
                  v-else-if="field.type === 'Image'"
                  list-type="picture-card"
                  disabled
                >
                  <div>
                    <a-icon type="plus" />
                    <div class="ant-upload-text">上传图片</div>
                  </div>
                </a-upload>

                <!-- 富文本编辑器 -->
                <div
                  v-else-if="field.type === 'RichText'"
                  class="rich-text-placeholder"
                >
                  富文本编辑器
                </div>
              </a-form-item>

              <!-- 字段操作按钮 -->
              <div class="field-actions">
                <a-button
                  type="link"
                  size="small"
                  @click.stop="moveField(field, -1)"
                >
                  <a-icon type="arrow-up" />
                </a-button>
                <a-button
                  type="link"
                  size="small"
                  @click.stop="moveField(field, 1)"
                >
                  <a-icon type="arrow-down" />
                </a-button>
                <a-button
                  type="link"
                  size="small"
                  @click.stop="deleteField(field)"
                >
                  <a-icon type="delete" />
                </a-button>
              </div>
            </div>
          </a-form>
        </div>

        <!-- 属性面板 -->
        <div class="designer-properties">
          <a-card title="字段属性">
            <template v-if="selectedField">
              <a-form layout="vertical">
                <a-form-item label="字段名称">
                  <a-input v-model:value="selectedField.name" />
                </a-form-item>
                <a-form-item label="字段类型">
                  <a-select v-model:value="selectedField.type">
                    <a-select-option
                      v-for="type in fieldTypes"
                      :key="type.type"
                      :value="type.type"
                    >
                      {{ type.name }}
                    </a-select-option>
                  </a-select>
                </a-form-item>
                <a-form-item>
                  <a-checkbox v-model:checked="selectedField.required">必填字段</a-checkbox>
                </a-form-item>
                <a-form-item label="默认值">
                  <a-input v-model:value="selectedField.defaultValue" />
                </a-form-item>
                <a-form-item label="验证规则">
                  <a-input v-model:value="selectedField.validationRule" />
                </a-form-item>

                <!-- 选项配置（单选、多选、下拉） -->
                <template v-if="['Radio', 'Checkbox', 'Select'].includes(selectedField.type)">
                  <a-divider>选项配置</a-divider>
                  <div
                    v-for="(option, index) in selectedField.options"
                    :key="index"
                    class="option-item"
                  >
                    <a-input
                      v-model:value="option.label"
                      placeholder="选项名称"
                      style="width: 120px"
                    />
                    <a-input
                      v-model:value="option.value"
                      placeholder="选项值"
                      style="width: 120px"
                    />
                    <a-button
                      type="link"
                      @click="removeOption(selectedField, index)"
                    >
                      <a-icon type="minus-circle" />
                    </a-button>
                  </div>
                  <a-button
                    type="dashed"
                    block
                    @click="addOption(selectedField)"
                  >
                    <a-icon type="plus" /> 添加选项
                  </a-button>
                </template>
              </a-form>
            </template>
            <template v-else>
              <div class="empty-properties">
                <a-empty description="请选择字段以编辑属性" />
              </div>
            </template>
          </a-card>
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
      <div class="form-preview">
        <a-form layout="vertical">
          <template v-for="field in form.fields" :key="field.id">
            <a-form-item
              :label="field.name"
              :required="field.required"
            >
              <!-- 字段预览内容与设计画布中的相同 -->
            </a-form-item>
          </template>
        </a-form>
      </div>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive } from 'vue';
import { message } from 'ant-design-vue';
import type { LeanWorkflowForm, LeanWorkflowFormField } from '@/api/workflow/types';
import { saveWorkflowForm } from '@/api/workflow';

// 字段类型定义
const fieldTypes = [
  { type: 'Text', name: '文本', icon: 'font-size' },
  { type: 'Number', name: '数字', icon: 'number' },
  { type: 'Date', name: '日期', icon: 'calendar' },
  { type: 'Time', name: '时间', icon: 'clock-circle' },
  { type: 'DateTime', name: '日期时间', icon: 'calendar' },
  { type: 'Radio', name: '单选框', icon: 'check-circle' },
  { type: 'Checkbox', name: '多选框', icon: 'check-square' },
  { type: 'Select', name: '下拉选择', icon: 'down-square' },
  { type: 'File', name: '文件上传', icon: 'file' },
  { type: 'Image', name: '图片上传', icon: 'picture' },
  { type: 'RichText', name: '富文本', icon: 'edit' },
];

// 表单数据
const form = reactive<LeanWorkflowForm>({
  id: '',
  name: '新建表单',
  description: '',
  fields: [],
});

// 选中的字段
const selectedField = ref<LeanWorkflowFormField | null>(null);

// 预览对话框
const previewVisible = ref(false);

// 方法
const onDragStart = (event: DragEvent, fieldType: any) => {
  event.dataTransfer?.setData('fieldType', fieldType.type);
};

const onDragOver = (event: DragEvent) => {
  event.preventDefault();
};

const onDrop = (event: DragEvent) => {
  event.preventDefault();
  const fieldType = event.dataTransfer?.getData('fieldType');
  if (fieldType) {
    const field: LeanWorkflowFormField = {
      id: `field_${Date.now()}`,
      name: `${fieldType}字段`,
      type: fieldType as any,
      required: false,
      defaultValue: null,
      validationRule: '',
      properties: {},
      options: ['Radio', 'Checkbox', 'Select'].includes(fieldType)
        ? [
            { label: '选项1', value: '1' },
            { label: '选项2', value: '2' },
          ]
        : [],
    };
    form.fields.push(field);
    selectField(field);
  }
};

const onCanvasClick = () => {
  selectedField.value = null;
};

const selectField = (field: LeanWorkflowFormField) => {
  selectedField.value = field;
};

const moveField = (field: LeanWorkflowFormField, direction: number) => {
  const index = form.fields.indexOf(field);
  const newIndex = index + direction;
  if (newIndex >= 0 && newIndex < form.fields.length) {
    form.fields.splice(index, 1);
    form.fields.splice(newIndex, 0, field);
  }
};

const deleteField = (field: LeanWorkflowFormField) => {
  const index = form.fields.indexOf(field);
  if (index > -1) {
    form.fields.splice(index, 1);
    if (selectedField.value === field) {
      selectedField.value = null;
    }
  }
};

const addOption = (field: LeanWorkflowFormField) => {
  if (!field.options) {
    field.options = [];
  }
  field.options.push({
    label: `选项${field.options.length + 1}`,
    value: `${field.options.length + 1}`,
  });
};

const removeOption = (field: LeanWorkflowFormField, index: number) => {
  field.options?.splice(index, 1);
};

const saveForm = async () => {
  try {
    await saveWorkflowForm(form);
    message.success('保存成功');
  } catch (error) {
    message.error('保存失败');
  }
};

const previewForm = () => {
  previewVisible.value = true;
};
</script>

<style lang="less" scoped>
.form-designer {
  height: 100%;
  padding: 16px;

  .designer-card {
    height: 100%;
  }

  .designer-header {
    display: flex;
    justify-content: space-between;
    align-items: center;

    .designer-actions {
      .ant-btn {
        margin-left: 8px;
      }
    }
  }

  .designer-content {
    display: flex;
    height: calc(100vh - 180px);
    margin-top: 16px;

    .designer-toolbar {
      width: 200px;
      margin-right: 16px;
      overflow-y: auto;

      .field-type-item {
        display: flex;
        align-items: center;
        padding: 8px;
        margin-bottom: 8px;
        border: 1px solid #d9d9d9;
        border-radius: 4px;
        cursor: move;

        .anticon {
          margin-right: 8px;
        }

        &:hover {
          background-color: #f5f5f5;
        }
      }
    }

    .designer-canvas {
      flex: 1;
      padding: 16px;
      border: 1px solid #d9d9d9;
      border-radius: 4px;
      overflow-y: auto;
      background-color: #fafafa;

      .form-preview {
        padding: 24px;
        background-color: #fff;
        border-radius: 4px;

        .form-field {
          position: relative;
          padding: 8px;
          border: 1px dashed transparent;
          border-radius: 4px;

          &:hover {
            border-color: #1890ff;

            .field-actions {
              display: flex;
            }
          }

          &.selected {
            border-color: #1890ff;
            background-color: #e6f7ff;

            .field-actions {
              display: flex;
            }
          }

          .field-actions {
            display: none;
            position: absolute;
            right: 8px;
            top: 8px;
          }

          .rich-text-placeholder {
            height: 200px;
            padding: 16px;
            background-color: #fafafa;
            border: 1px solid #d9d9d9;
            border-radius: 4px;
          }
        }
      }
    }

    .designer-properties {
      width: 300px;
      margin-left: 16px;
      overflow-y: auto;

      .empty-properties {
        padding: 32px 0;
      }

      .option-item {
        display: flex;
        align-items: center;
        margin-bottom: 8px;

        .ant-input {
          margin-right: 8px;
        }
      }
    }
  }
}
</style> 
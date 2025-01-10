<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：GatewayProperties.vue
 * 文件功能描述：网关节点属性编辑组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ------------------------------------------------------------------>

<template>
  <div class="gateway-properties">
    <a-form layout="vertical">
      <!-- 网关类型 -->
      <a-form-item label="网关类型">
        <a-select v-model:value="properties.gatewayType">
          <a-select-option value="exclusive">排他网关</a-select-option>
          <a-select-option value="parallel">并行网关</a-select-option>
          <a-select-option value="inclusive">包容网关</a-select-option>
        </a-select>
      </a-form-item>

      <!-- 条件配置 -->
      <template v-if="properties.gatewayType === 'exclusive' || properties.gatewayType === 'inclusive'">
        <a-form-item label="条件配置">
          <a-button @click="handleAddCondition">添加条件</a-button>
          <a-table
            :columns="conditionColumns"
            :data-source="properties.conditions"
            :pagination="false"
            size="small"
          >
            <template #action="{ record, index }">
              <a-space>
                <a @click="handleEditCondition(record, index)">编辑</a>
                <a @click="handleDeleteCondition(index)">删除</a>
              </a-space>
            </template>
          </a-table>
        </a-form-item>
      </template>

      <!-- 默认分支 -->
      <template v-if="properties.gatewayType === 'exclusive'">
        <a-form-item label="默认分支">
          <a-select
            v-model:value="properties.defaultBranch"
            placeholder="请选择默认分支"
          >
            <a-select-option
              v-for="branch in outgoingBranches"
              :key="branch.id"
              :value="branch.id"
            >
              {{ branch.name }}
            </a-select-option>
          </a-select>
        </a-form-item>
      </template>

      <!-- 并行配置 -->
      <template v-if="properties.gatewayType === 'parallel'">
        <a-form-item label="分支配置">
          <a-radio-group v-model:value="properties.parallelType">
            <a-radio value="all">全部完成</a-radio>
            <a-radio value="any">任一完成</a-radio>
            <a-radio value="ratio">比例完成</a-radio>
          </a-radio-group>
          <template v-if="properties.parallelType === 'ratio'">
            <a-input-number
              v-model:value="properties.completionRatio"
              :min="1"
              :max="100"
              addon-after="%"
            />
          </template>
        </a-form-item>
      </template>

      <!-- 高级配置 -->
      <a-collapse>
        <a-collapse-panel key="advanced" header="高级配置">
          <!-- 回调配置 -->
          <a-form-item label="回调配置">
            <a-input
              v-model:value="properties.beforeCallback"
              placeholder="分支前回调"
            />
            <a-input
              v-model:value="properties.afterCallback"
              placeholder="分支后回调"
            />
          </a-form-item>
        </a-collapse-panel>
      </a-collapse>
    </a-form>

    <!-- 条件编辑对话框 -->
    <a-modal
      v-model:visible="conditionVisible"
      :title="conditionIndex === -1 ? '添加条件' : '编辑条件'"
      @ok="handleConditionConfirm"
    >
      <a-form :model="conditionForm" layout="vertical">
        <a-form-item
          label="条件名称"
          name="name"
          :rules="[{ required: true, message: '请输入条件名称' }]"
        >
          <a-input v-model:value="conditionForm.name" />
        </a-form-item>
        <a-form-item
          label="目标分支"
          name="targetBranch"
          :rules="[{ required: true, message: '请选择目标分支' }]"
        >
          <a-select v-model:value="conditionForm.targetBranch">
            <a-select-option
              v-for="branch in outgoingBranches"
              :key="branch.id"
              :value="branch.id"
            >
              {{ branch.name }}
            </a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item
          label="条件表达式"
          name="expression"
          :rules="[{ required: true, message: '请输入条件表达式' }]"
        >
          <a-textarea
            v-model:value="conditionForm.expression"
            :rows="4"
            placeholder="请输入条件表达式"
          />
          <div class="expr-help">
            支持的变量：
            <ul>
              <li>${form.field} - 表单字段值</li>
              <li>${user.dept} - 用户部门</li>
              <li>${user.role} - 用户角色</li>
              <li>${workflow.initiator} - 流程发起人</li>
            </ul>
          </div>
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, watch } from 'vue'
import { message } from 'ant-design-vue'

// 类型定义
interface Branch {
  id: string
  name: string
}

interface Props {
  value: Record<string, any>
  outgoingBranches?: Branch[]
}

interface Emits {
  (e: 'update:value', value: Record<string, any>): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

// 属性数据
const properties = reactive<Record<string, any>>({
  gatewayType: 'exclusive',
  conditions: [],
  defaultBranch: undefined,
  parallelType: 'all',
  completionRatio: 100,
  beforeCallback: '',
  afterCallback: ''
})

// 分支列表
const outgoingBranches = ref<Branch[]>([])

// 条件列表
const conditionColumns = [
  {
    title: '名称',
    dataIndex: 'name',
    key: 'name'
  },
  {
    title: '目标分支',
    dataIndex: 'targetBranch',
    key: 'targetBranch',
    customRender: ({ text }: { text: string }) => {
      const branch = outgoingBranches.value.find(b => b.id === text)
      return branch?.name || text
    }
  },
  {
    title: '操作',
    key: 'action',
    slots: { customRender: 'action' }
  }
]

// 条件编辑
const conditionVisible = ref(false)
const conditionIndex = ref(-1)
const conditionForm = reactive({
  name: '',
  targetBranch: '',
  expression: ''
})

// 监听属性变化
watch(
  () => props.value,
  (newValue) => {
    Object.assign(properties, newValue)
  },
  { immediate: true, deep: true }
)

// 监听数据变化
watch(
  properties,
  (newValue) => {
    emit('update:value', { ...newValue })
  },
  { deep: true }
)

// 监听分支变化
watch(
  () => props.outgoingBranches,
  (newValue) => {
    outgoingBranches.value = newValue || []
  },
  { immediate: true }
)

// 添加条件
const handleAddCondition = () => {
  conditionIndex.value = -1
  conditionForm.name = ''
  conditionForm.targetBranch = ''
  conditionForm.expression = ''
  conditionVisible.value = true
}

// 编辑条件
const handleEditCondition = (condition: Record<string, any>, index: number) => {
  conditionIndex.value = index
  Object.assign(conditionForm, condition)
  conditionVisible.value = true
}

// 删除条件
const handleDeleteCondition = (index: number) => {
  properties.conditions.splice(index, 1)
}

// 确认条件编辑
const handleConditionConfirm = () => {
  const condition = {
    name: conditionForm.name,
    targetBranch: conditionForm.targetBranch,
    expression: conditionForm.expression
  }

  if (conditionIndex.value === -1) {
    properties.conditions.push(condition)
  } else {
    properties.conditions[conditionIndex.value] = condition
  }

  conditionVisible.value = false
}
</script>

<style lang="less" scoped>
.gateway-properties {
  .expr-help {
    margin-top: 8px;
    color: #666;
    font-size: 12px;

    ul {
      margin: 4px 0 0 20px;
      padding: 0;
    }
  }

  :deep(.ant-form-item) {
    margin-bottom: 16px;
  }

  :deep(.ant-radio-group) {
    margin-bottom: 8px;
  }
}
</style> 
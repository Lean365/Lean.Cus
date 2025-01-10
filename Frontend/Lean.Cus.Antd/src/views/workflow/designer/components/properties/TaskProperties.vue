<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：TaskProperties.vue
 * 文件功能描述：任务节点属性编辑组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ------------------------------------------------------------------>

<template>
  <div class="task-properties">
    <a-form layout="vertical">
      <!-- 任务类型 -->
      <a-form-item label="任务类型">
        <a-select v-model:value="properties.taskType">
          <a-select-option value="approval">审批任务</a-select-option>
          <a-select-option value="handle">处理任务</a-select-option>
          <a-select-option value="notice">通知任务</a-select-option>
        </a-select>
      </a-form-item>

      <!-- 表单配置 -->
      <a-form-item label="关联表单">
        <a-select
          v-model:value="properties.formId"
          placeholder="请选择表单"
          :loading="formLoading"
        >
          <a-select-option v-for="form in formList" :key="form.id" :value="form.id">
            {{ form.name }}
          </a-select-option>
        </a-select>
      </a-form-item>

      <!-- 处理人配置 -->
      <a-form-item label="处理人配置">
        <a-radio-group v-model:value="properties.assigneeType">
          <a-radio value="specific">指定人员</a-radio>
          <a-radio value="role">指定角色</a-radio>
          <a-radio value="dept">指定部门</a-radio>
          <a-radio value="expr">表达式</a-radio>
        </a-radio-group>

        <!-- 指定人员 -->
        <template v-if="properties.assigneeType === 'specific'">
          <a-select
            v-model:value="properties.assigneeList"
            mode="multiple"
            placeholder="请选择处理人"
            :loading="userLoading"
          >
            <a-select-option v-for="user in userList" :key="user.id" :value="user.id">
              {{ user.name }}
            </a-select-option>
          </a-select>
        </template>

        <!-- 指定角色 -->
        <template v-if="properties.assigneeType === 'role'">
          <a-select
            v-model:value="properties.roleList"
            mode="multiple"
            placeholder="请选择角色"
            :loading="roleLoading"
          >
            <a-select-option v-for="role in roleList" :key="role.id" :value="role.id">
              {{ role.name }}
            </a-select-option>
          </a-select>
        </template>

        <!-- 指定部门 -->
        <template v-if="properties.assigneeType === 'dept'">
          <a-tree-select
            v-model:value="properties.deptList"
            :tree-data="deptTree"
            placeholder="请选择部门"
            :loading="deptLoading"
            multiple
            tree-checkable
          />
        </template>

        <!-- 表达式 -->
        <template v-if="properties.assigneeType === 'expr'">
          <a-input
            v-model:value="properties.assigneeExpr"
            placeholder="请输入表达式"
          />
          <div class="expr-help">
            支持的变量：
            <ul>
              <li>${initiator} - 发起人</li>
              <li>${previousAssignee} - 上一节点处理人</li>
              <li>${deptLeader} - 部门负责人</li>
            </ul>
          </div>
        </template>
      </a-form-item>

      <!-- 超时配置 -->
      <a-form-item label="超时配置">
        <a-checkbox v-model:checked="properties.enableTimeout">启用超时提醒</a-checkbox>
        <template v-if="properties.enableTimeout">
          <a-input-number
            v-model:value="properties.timeoutHours"
            :min="1"
            :max="720"
            addon-after="小时"
          />
          <a-select v-model:value="properties.timeoutAction">
            <a-select-option value="notice">发送提醒</a-select-option>
            <a-select-option value="transfer">自动转办</a-select-option>
            <a-select-option value="approve">自动通过</a-select-option>
            <a-select-option value="reject">自动拒绝</a-select-option>
          </a-select>
        </template>
      </a-form-item>

      <!-- 高级配置 -->
      <a-collapse>
        <a-collapse-panel key="advanced" header="高级配置">
          <!-- 会签配置 -->
          <a-form-item label="会签设置">
            <a-checkbox v-model:checked="properties.enableCountersign">
              启用会签
            </a-checkbox>
            <template v-if="properties.enableCountersign">
              <a-radio-group v-model:value="properties.countersignRule">
                <a-radio value="all">全部同意</a-radio>
                <a-radio value="any">任意同意</a-radio>
                <a-radio value="ratio">比例同意</a-radio>
              </a-radio-group>
              <template v-if="properties.countersignRule === 'ratio'">
                <a-input-number
                  v-model:value="properties.countersignRatio"
                  :min="1"
                  :max="100"
                  addon-after="%"
                />
              </template>
            </template>
          </a-form-item>

          <!-- 自定义按钮 -->
          <a-form-item label="自定义按钮">
            <a-button @click="handleAddButton">添加按钮</a-button>
            <a-table
              :columns="buttonColumns"
              :data-source="properties.buttons"
              :pagination="false"
              size="small"
            >
              <template #action="{ record, index }">
                <a-space>
                  <a @click="handleEditButton(record, index)">编辑</a>
                  <a @click="handleDeleteButton(index)">删除</a>
                </a-space>
              </template>
            </a-table>
          </a-form-item>

          <!-- 回调配置 -->
          <a-form-item label="回调配置">
            <a-input
              v-model:value="properties.beforeCallback"
              placeholder="处理前回调"
            />
            <a-input
              v-model:value="properties.afterCallback"
              placeholder="处理后回调"
            />
          </a-form-item>
        </a-collapse-panel>
      </a-collapse>
    </a-form>

    <!-- 按钮编辑对话框 -->
    <a-modal
      v-model:visible="buttonVisible"
      :title="buttonIndex === -1 ? '添加按钮' : '编辑按钮'"
      @ok="handleButtonConfirm"
    >
      <a-form :model="buttonForm" layout="vertical">
        <a-form-item
          label="按钮名称"
          name="name"
          :rules="[{ required: true, message: '请输入按钮名称' }]"
        >
          <a-input v-model:value="buttonForm.name" />
        </a-form-item>
        <a-form-item
          label="按钮类型"
          name="type"
          :rules="[{ required: true, message: '请选择按钮类型' }]"
        >
          <a-select v-model:value="buttonForm.type">
            <a-select-option value="approve">同意</a-select-option>
            <a-select-option value="reject">拒绝</a-select-option>
            <a-select-option value="transfer">转办</a-select-option>
            <a-select-option value="custom">自定义</a-select-option>
          </a-select>
        </a-form-item>
        <template v-if="buttonForm.type === 'custom'">
          <a-form-item
            label="处理动作"
            name="action"
            :rules="[{ required: true, message: '请输入处理动作' }]"
          >
            <a-input v-model:value="buttonForm.action" />
          </a-form-item>
        </template>
      </a-form>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, watch } from 'vue'
import { message } from 'ant-design-vue'
import type { TreeDataItem } from 'ant-design-vue/es/tree/Tree'

// 类型定义
interface Form {
  id: string
  name: string
}

interface User {
  id: string
  name: string
}

interface Role {
  id: string
  name: string
}

interface Props {
  value: Record<string, any>
}

interface Emits {
  (e: 'update:value', value: Record<string, any>): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

// 属性数据
const properties = reactive<Record<string, any>>({
  taskType: 'approval',
  formId: undefined,
  assigneeType: 'specific',
  assigneeList: [],
  roleList: [],
  deptList: [],
  assigneeExpr: '',
  enableTimeout: false,
  timeoutHours: 24,
  timeoutAction: 'notice',
  enableCountersign: false,
  countersignRule: 'all',
  countersignRatio: 100,
  buttons: [],
  beforeCallback: '',
  afterCallback: ''
})

// 表单列表
const formList = ref<Form[]>([])
const formLoading = ref(false)

// 用户列表
const userList = ref<User[]>([])
const userLoading = ref(false)

// 角色列表
const roleList = ref<Role[]>([])
const roleLoading = ref(false)

// 部门树
const deptTree = ref<TreeDataItem[]>([])
const deptLoading = ref(false)

// 按钮列表
const buttonColumns = [
  {
    title: '名称',
    dataIndex: 'name',
    key: 'name'
  },
  {
    title: '类型',
    dataIndex: 'type',
    key: 'type'
  },
  {
    title: '操作',
    key: 'action',
    slots: { customRender: 'action' }
  }
]

// 按钮编辑
const buttonVisible = ref(false)
const buttonIndex = ref(-1)
const buttonForm = reactive({
  name: '',
  type: 'approve',
  action: ''
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

// 加载表单列表
const loadFormList = async () => {
  formLoading.value = true
  try {
    // TODO: 调用获取表单列表API
    formLoading.value = false
  } catch (error) {
    message.error('加载表单列表失败')
    formLoading.value = false
  }
}

// 加载用户列表
const loadUserList = async () => {
  userLoading.value = true
  try {
    // TODO: 调用获取用户列表API
    userLoading.value = false
  } catch (error) {
    message.error('加载用户列表失败')
    userLoading.value = false
  }
}

// 加载角色列表
const loadRoleList = async () => {
  roleLoading.value = true
  try {
    // TODO: 调用获取角色列表API
    roleLoading.value = false
  } catch (error) {
    message.error('加载角色列表失败')
    roleLoading.value = false
  }
}

// 加载部门树
const loadDeptTree = async () => {
  deptLoading.value = true
  try {
    // TODO: 调用获取部门树API
    deptLoading.value = false
  } catch (error) {
    message.error('加载部门树失败')
    deptLoading.value = false
  }
}

// 添加按钮
const handleAddButton = () => {
  buttonIndex.value = -1
  buttonForm.name = ''
  buttonForm.type = 'approve'
  buttonForm.action = ''
  buttonVisible.value = true
}

// 编辑按钮
const handleEditButton = (button: Record<string, any>, index: number) => {
  buttonIndex.value = index
  Object.assign(buttonForm, button)
  buttonVisible.value = true
}

// 删除按钮
const handleDeleteButton = (index: number) => {
  properties.buttons.splice(index, 1)
}

// 确认按钮编辑
const handleButtonConfirm = () => {
  const button = {
    name: buttonForm.name,
    type: buttonForm.type,
    action: buttonForm.action
  }

  if (buttonIndex.value === -1) {
    properties.buttons.push(button)
  } else {
    properties.buttons[buttonIndex.value] = button
  }

  buttonVisible.value = false
}

// 初始化
onMounted(() => {
  loadFormList()
  loadUserList()
  loadRoleList()
  loadDeptTree()
})
</script>

<style lang="less" scoped>
.task-properties {
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
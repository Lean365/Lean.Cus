<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：StartProperties.vue
 * 文件功能描述：开始节点属性编辑组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ------------------------------------------------------------------>

<template>
  <div class="start-properties">
    <a-form layout="vertical">
      <!-- 发起人配置 -->
      <a-form-item label="发起人配置">
        <a-radio-group v-model:value="properties.initiatorType">
          <a-radio value="all">所有人</a-radio>
          <a-radio value="specific">指定人员</a-radio>
          <a-radio value="role">指定角色</a-radio>
          <a-radio value="dept">指定部门</a-radio>
        </a-radio-group>

        <!-- 指定人员 -->
        <template v-if="properties.initiatorType === 'specific'">
          <a-select
            v-model:value="properties.initiatorList"
            mode="multiple"
            placeholder="请选择发起人"
            :loading="userLoading"
          >
            <a-select-option v-for="user in userList" :key="user.id" :value="user.id">
              {{ user.name }}
            </a-select-option>
          </a-select>
        </template>

        <!-- 指定角色 -->
        <template v-if="properties.initiatorType === 'role'">
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
        <template v-if="properties.initiatorType === 'dept'">
          <a-tree-select
            v-model:value="properties.deptList"
            :tree-data="deptTree"
            placeholder="请选择部门"
            :loading="deptLoading"
            multiple
            tree-checkable
          />
        </template>
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

      <!-- 高级配置 -->
      <a-collapse>
        <a-collapse-panel key="advanced" header="高级配置">
          <!-- 流程标题 -->
          <a-form-item label="流程标题">
            <a-input
              v-model:value="properties.titleTemplate"
              placeholder="请输入流程标题模板"
            />
            <div class="expr-help">
              支持的变量：
              <ul>
                <li>${form.field} - 表单字段值</li>
                <li>${user.name} - 发起人姓名</li>
                <li>${user.dept} - 发起人部门</li>
                <li>${date} - 发起日期</li>
              </ul>
            </div>
          </a-form-item>

          <!-- 回调配置 -->
          <a-form-item label="回调配置">
            <a-input
              v-model:value="properties.beforeCallback"
              placeholder="发起前回调"
            />
            <a-input
              v-model:value="properties.afterCallback"
              placeholder="发起后回调"
            />
          </a-form-item>
        </a-collapse-panel>
      </a-collapse>
    </a-form>
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
  initiatorType: 'all',
  initiatorList: [],
  roleList: [],
  deptList: [],
  formId: undefined,
  titleTemplate: '',
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

// 初始化
onMounted(() => {
  loadFormList()
  loadUserList()
  loadRoleList()
  loadDeptTree()
})
</script>

<style lang="less" scoped>
.start-properties {
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
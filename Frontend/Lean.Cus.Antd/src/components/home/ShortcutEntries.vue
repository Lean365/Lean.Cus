<template>
  <div class="shortcut-entries">
    <div class="section-title">
      <span class="title-text">快捷入口</span>
    </div>
    <a-row :gutter="[16, 16]">
      <a-col :span="2" :xs="8" :sm="6" :md="4" :lg="2" v-for="item in displayedEntries" :key="item.id">
        <a-card hoverable class="shortcut-item">
          <div class="card-actions" v-if="!item.isDefault">
            <div class="edit-action">
              <a-button type="text" @click.stop="handleEdit(item)">
                <template #icon><edit-outlined /></template>
              </a-button>
            </div>
            <div class="delete-action">
              <a-button type="text" danger @click.stop="handleDelete(item)">
                <template #icon><delete-outlined /></template>
              </a-button>
            </div>
          </div>
          <div class="shortcut-content" @click="handleClick(item)">
            <component :is="item.icon" :style="{ color: item.iconColor, fontSize: '32px' }" />
            <div class="shortcut-title">{{ item.title }}</div>
          </div>
        </a-card>
      </a-col>
    </a-row>

    <a-modal
      v-model:visible="modalVisible"
      :title="editingEntry ? '编辑快捷入口' : '添加快捷入口'"
      @ok="handleModalOk"
      @cancel="handleModalCancel"
      :maskClosable="false"
      :destroyOnClose="true"
    >
      <a-form
        ref="formRef"
        :model="formState"
        :rules="rules"
        :label-col="{ span: 6 }"
        :wrapper-col="{ span: 16 }"
      >
        <a-form-item label="标题" name="title">
          <a-input v-model:value="formState.title" placeholder="请输入标题" />
        </a-form-item>
        <a-form-item label="描述" name="description">
          <a-input v-model:value="formState.description" placeholder="请输入描述" />
        </a-form-item>
        <a-form-item label="路由地址" name="route">
          <a-input v-model:value="formState.route" placeholder="请输入路由地址" />
        </a-form-item>
        <a-form-item label="图标" name="icon">
          <a-select v-model:value="formState.icon" show-search placeholder="请选择图标">
            <a-select-option value="LinkOutlined">链接</a-select-option>
            <a-select-option value="FileTextOutlined">文件</a-select-option>
            <a-select-option value="ProjectOutlined">项目</a-select-option>
            <a-select-option value="CalendarOutlined">日历</a-select-option>
            <a-select-option value="TeamOutlined">团队</a-select-option>
            <a-select-option value="UserOutlined">用户</a-select-option>
            <a-select-option value="SettingOutlined">设置</a-select-option>
            <a-select-option value="BulbOutlined">想法</a-select-option>
            <a-select-option value="GlobalOutlined">全球</a-select-option>
          </a-select>
        </a-form-item>
        <a-form-item label="图标颜色" name="iconColor">
          <a-input type="color" v-model:value="formState.iconColor" />
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, defineExpose, computed, watch } from 'vue'
import { message } from 'ant-design-vue'
import {
  EditOutlined,
  DeleteOutlined,
  LinkOutlined,
  FileTextOutlined,
  ProjectOutlined,
  CalendarOutlined,
  TeamOutlined,
  UserOutlined,
  SettingOutlined,
  BulbOutlined,
  GlobalOutlined,
  BarChartOutlined,
  BellOutlined
} from '@ant-design/icons-vue'

// 图标映射
const iconMap: Record<string, any> = {
  LinkOutlined,
  FileTextOutlined,
  ProjectOutlined,
  CalendarOutlined,
  TeamOutlined,
  UserOutlined,
  SettingOutlined,
  BulbOutlined,
  GlobalOutlined,
  BarChartOutlined,
  BellOutlined
}

interface ShortcutEntry {
  id: string
  title: string
  description: string
  route: string
  icon: any
  iconColor: string
  isDefault?: boolean
}

// 从localStorage获取已保存的快捷入口数据，如果没有则使用默认数据
const savedEntries = localStorage.getItem('shortcut-entries')
const defaultEntries = [
  {
    id: '1',
    title: '项目管理',
    description: '管理所有项目',
    route: '/projects',
    icon: ProjectOutlined,
    iconColor: '#1890ff',
    isDefault: true
  },
  {
    id: '2',
    title: '团队协作',
    description: '团队任务协作',
    route: '/team',
    icon: TeamOutlined,
    iconColor: '#52c41a',
    isDefault: true
  },
  {
    id: '3',
    title: '文档中心',
    description: '查看系统文档',
    route: '/docs',
    icon: FileTextOutlined,
    iconColor: '#722ed1',
    isDefault: true
  },
  {
    id: '4',
    title: '系统设置',
    description: '系统配置管理',
    route: '/settings',
    icon: SettingOutlined,
    iconColor: '#fa8c16',
    isDefault: true
  },
  {
    id: '5',
    title: '用户管理',
    description: '用户信息管理',
    route: '/users',
    icon: UserOutlined,
    iconColor: '#eb2f96',
    isDefault: true
  },
  {
    id: '6',
    title: '日程安排',
    description: '日程管理',
    route: '/calendar',
    icon: CalendarOutlined,
    iconColor: '#13c2c2',
    isDefault: true
  },
  {
    id: '7',
    title: '全局设置',
    description: '全局配置',
    route: '/global',
    icon: GlobalOutlined,
    iconColor: '#2f54eb',
    isDefault: true
  },
  {
    id: '8',
    title: '数据分析',
    description: '数据统计分析',
    route: '/analysis',
    icon: BarChartOutlined,
    iconColor: '#faad14',
    isDefault: true
  },
  {
    id: '9',
    title: '消息中心',
    description: '系统消息通知',
    route: '/messages',
    icon: BellOutlined,
    iconColor: '#f5222d',
    isDefault: true
  }
]

const entries = ref<ShortcutEntry[]>(
  savedEntries ? JSON.parse(savedEntries).map((entry: any) => {
    // 确保从localStorage恢复时使用正确的图标组件
    const iconComponent = iconMap[entry.icon]
    if (!iconComponent) {
      console.warn(`找不到图标组件: ${entry.icon}`)
    }
    return {
      ...entry,
      icon: iconComponent || iconMap.LinkOutlined
    }
  }) : defaultEntries
)

// 监听快捷入口数据变化，保存到localStorage
watch(entries, (newValue) => {
  const serializedEntries = newValue.map(entry => {
    // 找到图标组件对应的名称
    let iconName = 'LinkOutlined'
    for (const [name, component] of Object.entries(iconMap)) {
      if (entry.icon === component || entry.icon?.type === component) {
        iconName = name
        break
      }
    }
    
    return {
      ...entry,
      icon: iconName
    }
  })
  localStorage.setItem('shortcut-entries', JSON.stringify(serializedEntries))
}, { deep: true })

// 表单状态
const modalVisible = ref(false)
const editingEntry = ref<ShortcutEntry | null>(null)
const formRef = ref()

const formState = reactive({
  title: '',
  description: '',
  route: '',
  icon: 'LinkOutlined',
  iconColor: '#1890ff'
})

// 表单验证规则
const rules = {
  title: [{ required: true, message: '请输入标题' }],
  description: [{ required: true, message: '请输入描述' }],
  route: [{ required: true, message: '请输入路由地址' }],
  icon: [{ required: true, message: '请选择图标' }]
}

// 处理编辑
const handleEdit = (entry: ShortcutEntry) => {
  editingEntry.value = entry
  formState.title = entry.title
  formState.description = entry.description
  formState.route = entry.route
  // 找到正确的图标名称
  let iconKey = 'LinkOutlined'
  for (const [name, component] of Object.entries(iconMap)) {
    if (entry.icon === component || entry.icon?.type === component) {
      iconKey = name
      break
    }
  }
  formState.icon = iconKey
  formState.iconColor = entry.iconColor
  modalVisible.value = true
}

// 处理删除
const handleDelete = (entry: ShortcutEntry) => {
  entries.value = entries.value.filter(item => item.id !== entry.id)
  message.success('删除成功')
}

// 处理点击
const handleClick = (entry: ShortcutEntry) => {
  // TODO: 处理路由跳转
  console.log('跳转到:', entry.route)
}

// 处理模态框确认
const handleModalOk = async () => {
  try {
    await formRef.value?.validate()
    const selectedIcon = iconMap[formState.icon]
    if (!selectedIcon) {
      message.error('图标选择错误')
      return
    }

    const newEntry: ShortcutEntry = {
      id: editingEntry.value?.id || Date.now().toString(),
      title: formState.title,
      description: formState.description,
      route: formState.route,
      icon: selectedIcon,
      iconColor: formState.iconColor,
      isDefault: false
    }

    if (editingEntry.value) {
      const index = entries.value.findIndex(item => item.id === editingEntry.value?.id)
      if (index !== -1) {
        entries.value[index] = { ...newEntry, isDefault: entries.value[index].isDefault }
        message.success('编辑成功')
      }
    } else {
      // 添加新入口
      entries.value.push(newEntry)
      message.success('添加成功')
    }
    modalVisible.value = false
    formRef.value?.resetFields()
  } catch (error) {
    console.error('表单验证失败:', error)
  }
}

// 处理模态框取消
const handleModalCancel = () => {
  formRef.value?.resetFields()
  modalVisible.value = false
}

// 计算属性：限制显示的快捷入口数量，最多24个（2行x12个）
const displayedEntries = computed(() => {
  return entries.value.slice(0, 24)
})

// 暴露方法
defineExpose({
  showAddModal: () => {
    editingEntry.value = null
    formRef.value?.resetFields()
    modalVisible.value = true
  }
})
</script>

<style lang="less" scoped>
.shortcut-entries {
  .section-title {
    margin-bottom: 16px;
    
    .title-text {
      font-size: 16px;
      font-weight: 500;
      color: var(--ant-color-text);
    }
  }

  :deep(.ant-card) {
    &.shortcut-item {
      position: relative;
      width: 100%;
      aspect-ratio: 1;
      padding: 0;
      cursor: pointer;
      transition: all 0.3s;

      .ant-card-body {
        height: 100%;
        padding: 16px;
      }

      &:hover {
        transform: translateY(-2px);
      }

      .card-actions {
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        opacity: 0;
        transition: opacity 0.3s;
        z-index: 1;

        .edit-action {
          position: absolute;
          top: 8px;
          left: 8px;
        }

        .delete-action {
          position: absolute;
          top: 8px;
          right: 8px;
        }
      }

      &:hover .card-actions {
        opacity: 1;
      }

      .shortcut-content {
        height: 100%;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        gap: 12px;

        .anticon {
          font-size: clamp(24px, 4vw, 32px);
        }

        .shortcut-title {
          color: var(--ant-color-text);
          font-size: clamp(12px, 2vw, 14px);
          font-weight: 500;
          text-align: center;
          white-space: nowrap;
          overflow: hidden;
          text-overflow: ellipsis;
          width: 100%;
        }
      }
    }
  }
}
</style> 
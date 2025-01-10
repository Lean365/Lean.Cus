<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.vue
 * 文件功能描述：工作流设计器页面
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
          <a-button @click="handlePublish">发布</a-button>
          <a-dropdown>
            <a-button>
              版本管理
              <down-outlined />
            </a-button>
            <template #overlay>
              <a-menu>
                <a-menu-item key="versions" @click="handleVersions">版本列表</a-menu-item>
                <a-menu-item key="switch" @click="handleSwitchVersion">切换版本</a-menu-item>
                <a-menu-item key="compare" @click="handleCompareVersion">版本对比</a-menu-item>
              </a-menu>
            </template>
          </a-dropdown>
          <a-button @click="handleImport">导入</a-button>
          <a-button @click="handleExport">导出</a-button>
        </a-space>
      </div>

      <!-- 设计器区域 -->
      <div class="designer">
        <!-- 左侧节点面板 -->
        <div class="node-panel">
          <h3>节点类型</h3>
          <div
            v-for="node in nodeTypes"
            :key="node.type"
            class="node-item"
            draggable="true"
            @dragstart="handleDragStart($event, node)"
          >
            <a-icon :type="node.icon" />
            <span>{{ node.name }}</span>
          </div>
        </div>

        <!-- 中间画布 -->
        <div class="canvas" ref="canvasRef">
          <!-- 使用X6图形库渲染工作流图 -->
        </div>

        <!-- 右侧属性面板 -->
        <div class="property-panel">
          <div v-if="selectedCell" class="property-content">
            <h3>{{ selectedCell?.isNode ? '节点属性' : '连线属性' }}</h3>
            <a-form :model="propertyForm" layout="vertical">
              <a-form-item label="名称">
                <a-input v-model:value="propertyForm.name" />
              </a-form-item>
              <a-form-item label="描述">
                <a-textarea v-model:value="propertyForm.description" />
              </a-form-item>
              <!-- 根据节点类型显示不同的属性表单 -->
              <div v-if="selectedCell?.isNode">
                <component
                  :is="getPropertyComponent(selectedCell?.data?.type)"
                  v-model:value="propertyForm.properties"
                />
              </div>
            </a-form>
          </div>
          <div v-else class="empty">
            请选择节点或连线
          </div>
        </div>
      </div>
    </a-card>

    <!-- 版本列表对话框 -->
    <a-modal
      v-model:visible="versionsVisible"
      title="版本列表"
      width="800px"
      :footer="null"
    >
      <a-table :columns="versionColumns" :data-source="versionList">
        <template #status="{ text }">
          <a-tag :color="getVersionStatusColor(text)">
            {{ getVersionStatusText(text) }}
          </a-tag>
        </template>
        <template #action="{ record }">
          <a-space>
            <a @click="handleViewVersion(record)">查看</a>
            <a-divider type="vertical" />
            <a @click="handleRestoreVersion(record)">还原</a>
            <a-divider type="vertical" />
            <a @click="handleDeprecateVersion(record)" v-if="record.status === 'published'">
              废弃
            </a>
          </a-space>
        </template>
      </a-table>
    </a-modal>

    <!-- 发布对话框 -->
    <a-modal
      v-model:visible="publishVisible"
      title="发布工作流"
      @ok="handlePublishConfirm"
    >
      <a-form :model="publishForm" layout="vertical">
        <a-form-item
          label="版本号"
          name="version"
          :rules="[{ required: true, message: '请输入版本号' }]"
        >
          <a-input-number v-model:value="publishForm.version" :min="1" />
        </a-form-item>
        <a-form-item label="描述" name="description">
          <a-textarea
            v-model:value="publishForm.description"
            placeholder="请输入版本描述"
            :rows="4"
          />
        </a-form-item>
        <a-form-item name="isForceUpdate">
          <a-checkbox v-model:checked="publishForm.isForceUpdate">
            强制更新（将使当前运行的流程实例迁移到新版本）
          </a-checkbox>
        </a-form-item>
      </a-form>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, onMounted, reactive, defineComponent } from 'vue'
import type { Ref } from 'vue'
import { message } from 'ant-design-vue'
import { Graph } from '@antv/x6'
import type { Cell, Node, Edge, Graph as GraphType, ModifierKey } from '@antv/x6'
import { saveDesign, getDesign, getVersions, publishVersion, deprecateVersion } from '@/api/workflow'
import type { WorkflowDesign, WorkflowNode, WorkflowEdge, WorkflowVersion, PublishConfig } from '@/api/workflow/types'

interface NodeType {
  type: string
  name: string
  icon: string
}

// 节点类型定义
const nodeTypes = ref<NodeType[]>([
  { type: 'start', name: '开始节点', icon: 'play-circle' },
  { type: 'end', name: '结束节点', icon: 'stop' },
  { type: 'task', name: '任务节点', icon: 'form' },
  { type: 'gateway', name: '网关节点', icon: 'fork' },
  { type: 'subprocess', name: '子流程', icon: 'branches' }
])

// 画布引用
const canvasRef = ref<HTMLDivElement>()
// 图形实例
const graph = ref<Graph>()
// 选中的节点或连线
const selectedCell = ref<Cell>()

// 属性表单
const propertyForm = reactive({
  name: '',
  description: '',
  properties: {}
})

// 版本管理相关
const versionsVisible = ref(false)
const versionList = ref<WorkflowVersion[]>([])
const versionColumns = [
  {
    title: '版本号',
    dataIndex: 'version',
    key: 'version'
  },
  {
    title: '状态',
    dataIndex: 'status',
    key: 'status',
    slots: { customRender: 'status' }
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    key: 'createTime'
  },
  {
    title: '发布时间',
    dataIndex: 'publishTime',
    key: 'publishTime'
  },
  {
    title: '描述',
    dataIndex: 'description',
    key: 'description'
  },
  {
    title: '操作',
    key: 'action',
    slots: { customRender: 'action' }
  }
]

// 发布相关
const publishVisible = ref(false)
const publishForm = reactive<PublishConfig>({
  workflowId: '',
  version: 1,
  description: '',
  isForceUpdate: false
})

// 初始化图形
onMounted(() => {
  if (canvasRef.value) {
    const options = {
      container: canvasRef.value,
      grid: true,
      mousewheel: {
        enabled: true,
        zoomAtMousePosition: true,
        modifiers: ['ctrl', 'meta'] as ModifierKey[]
      },
      connecting: {
        snap: true,
        allowBlank: false,
        allowLoop: false,
        highlight: true,
        connector: 'rounded',
        connectionPoint: 'boundary'
      },
      interacting: {
        nodeMovable: true,
        edgeMovable: true,
        edgeLabelMovable: true,
        arrowheadMovable: true,
        vertexMovable: true,
        vertexAddable: true,
        vertexDeletable: true
      }
    }
    graph.value = new Graph(options)

    // 监听选中事件
    graph.value.on('cell:selected', ({ cell }: { cell: Cell }) => {
      selectedCell.value = cell
      propertyForm.name = cell.prop('name') || ''
      propertyForm.description = cell.prop('description') || ''
      propertyForm.properties = cell.prop('properties') || {}
    })

    // 监听取消选中事件
    graph.value.on('cell:unselected', () => {
      selectedCell.value = undefined
    })
  }
})

// 拖拽开始
const handleDragStart = (event: DragEvent, node: NodeType) => {
  event.dataTransfer?.setData('nodeType', node.type)
}

// 保存设计
const handleSave = async () => {
  if (!graph.value) return

  const nodes: WorkflowNode[] = []
  const edges: WorkflowEdge[] = []

  // 收集节点数据
  graph.value.getNodes().forEach((node: Node) => {
    nodes.push({
      id: node.id,
      type: node.prop('type'),
      name: node.prop('name'),
      properties: node.prop('properties') || {},
      position: node.position()
    })
  })

  // 收集边数据
  graph.value.getEdges().forEach((edge: Edge) => {
    edges.push({
      id: edge.id,
      source: edge.getSourceCellId(),
      target: edge.getTargetCellId(),
      type: edge.prop('type'),
      properties: edge.prop('properties') || {}
    })
  })

  const design: WorkflowDesign = {
    name: 'New Workflow',
    version: 1,
    nodes,
    edges
  }

  try {
    const { data: workflowId } = await saveDesign(design)
    publishForm.workflowId = workflowId
    message.success('保存成功')
  } catch (error) {
    message.error('保存失败')
  }
}

// 发布流程
const handlePublish = () => {
  publishVisible.value = true
}

// 确认发布
const handlePublishConfirm = async () => {
  try {
    await publishVersion(publishForm)
    publishVisible.value = false
    message.success('发布成功')
  } catch (error) {
    message.error('发布失败')
  }
}

// 查看版本列表
const handleVersions = async () => {
  try {
    const { data: versions } = await getVersions(publishForm.workflowId)
    versionList.value = versions
    versionsVisible.value = true
  } catch (error) {
    message.error('获取版本列表失败')
  }
}

// 切换版本
const handleSwitchVersion = () => {
  message.info('切换版本功能开发中')
}

// 版本对比
const handleCompareVersion = () => {
  message.info('版本对比功能开发中')
}

// 查看版本
const handleViewVersion = (version: WorkflowVersion) => {
  message.info('查看版本功能开发中')
}

// 还原版本
const handleRestoreVersion = (version: WorkflowVersion) => {
  message.info('还原版本功能开发中')
}

// 废弃版本
const handleDeprecateVersion = async (version: WorkflowVersion) => {
  try {
    await deprecateVersion(version.workflowId, version.version)
    message.success('操作成功')
    handleVersions()
  } catch (error) {
    message.error('操作失败')
  }
}

// 导入流程
const handleImport = () => {
  message.info('导入功能开发中')
}

// 导出流程
const handleExport = () => {
  message.info('导出功能开发中')
}

// 获取属性组件
const getPropertyComponent = (type: string) => {
  // 根据节点类型返回不同的属性编辑组件
  return 'div'
}

// 获取版本状态颜色
const getVersionStatusColor = (status: string) => {
  const colorMap: Record<string, string> = {
    draft: 'blue',
    published: 'green',
    deprecated: 'red'
  }
  return colorMap[status] || 'default'
}

// 获取版本状态文本
const getVersionStatusText = (status: string) => {
  const textMap: Record<string, string> = {
    draft: '草稿',
    published: '已发布',
    deprecated: '已废弃'
  }
  return textMap[status] || status
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

  .node-panel {
    width: 200px;
    padding: 16px;
    border-right: 1px solid #d9d9d9;
    overflow-y: auto;

    h3 {
      margin-bottom: 16px;
    }

    .node-item {
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

  .canvas {
    flex: 1;
    background-color: #fafafa;
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
<!-- 工作流设计器 -->
<template>
  <div class="workflow-designer">
    <a-card class="designer-card">
      <template #title>
        <div class="designer-header">
          <span>工作流设计器</span>
          <div class="designer-actions">
            <a-button type="primary" @click="saveWorkflow">保存</a-button>
            <a-button @click="previewWorkflow">预览</a-button>
          </div>
        </div>
      </template>
      <div class="designer-content">
        <!-- 工具栏 -->
        <div class="designer-toolbar">
          <a-card title="节点类型">
            <div
              v-for="nodeType in nodeTypes"
              :key="nodeType.type"
              class="node-type-item"
              draggable="true"
              @dragstart="onDragStart($event, nodeType)"
            >
              <a-icon :type="nodeType.icon" />
              <span>{{ nodeType.name }}</span>
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
          <!-- 节点 -->
          <div
            v-for="node in workflow.nodes"
            :key="node.id"
            class="workflow-node"
            :class="[`node-type-${node.type}`, { selected: selectedNode?.id === node.id }]"
            :style="{ left: `${node.x}px`, top: `${node.y}px` }"
            @click.stop="selectNode(node)"
          >
            <div class="node-content">
              <a-icon :type="getNodeIcon(node.type)" />
              <span>{{ node.name }}</span>
            </div>
            <div class="node-ports">
              <div class="port port-in" @mousedown="startConnection($event, node, 'in')"></div>
              <div class="port port-out" @mousedown="startConnection($event, node, 'out')"></div>
            </div>
          </div>

          <!-- 连线 -->
          <svg class="connections-layer">
            <path
              v-for="conn in workflow.connections"
              :key="conn.id"
              :d="getConnectionPath(conn)"
              :class="{ selected: selectedConnection?.id === conn.id }"
              @click.stop="selectConnection(conn)"
            />
          </svg>
        </div>

        <!-- 属性面板 -->
        <div class="designer-properties">
          <a-card :title="getPropertiesPanelTitle()">
            <!-- 节点属性 -->
            <template v-if="selectedNode">
              <a-form layout="vertical">
                <a-form-item label="节点名称">
                  <a-input v-model:value="selectedNode.name" />
                </a-form-item>
                <a-form-item label="节点描述">
                  <a-textarea v-model:value="selectedNode.description" />
                </a-form-item>
                <a-form-item label="参与者">
                  <a-select
                    v-model:value="selectedNode.participants"
                    mode="multiple"
                    placeholder="请选择参与者"
                  >
                    <a-select-option v-for="user in users" :key="user.id" :value="user.id">
                      {{ user.name }}
                    </a-select-option>
                  </a-select>
                </a-form-item>
                <a-form-item label="表单">
                  <a-select
                    v-model:value="selectedNode.formId"
                    placeholder="请选择表单"
                  >
                    <a-select-option v-for="form in forms" :key="form.id" :value="form.id">
                      {{ form.name }}
                    </a-select-option>
                  </a-select>
                </a-form-item>
              </a-form>
            </template>

            <!-- 连线属性 -->
            <template v-else-if="selectedConnection">
              <a-form layout="vertical">
                <a-form-item label="连线名称">
                  <a-input v-model:value="selectedConnection.name" />
                </a-form-item>
                <a-form-item label="条件表达式">
                  <a-textarea v-model:value="selectedConnection.condition" />
                </a-form-item>
              </a-form>
            </template>

            <!-- 空白提示 -->
            <template v-else>
              <div class="empty-properties">
                <a-empty description="请选择节点或连线以编辑属性" />
              </div>
            </template>
          </a-card>
        </div>
      </div>
    </a-card>

    <!-- 预览对话框 -->
    <a-modal
      v-model:visible="previewVisible"
      title="工作流预览"
      width="800px"
      :footer="null"
    >
      <div class="workflow-preview">
        <!-- 这里可以添加工作流预览的内容 -->
      </div>
    </a-modal>
  </div>
</template>

<script lang="ts" setup>
import { ref, reactive, computed } from 'vue';
import { message } from 'ant-design-vue';
import type { LeanWorkflowDesign, LeanWorkflowNode, LeanWorkflowConnection } from '@/api/workflow/types';
import { saveWorkflowDesign } from '@/api/workflow';

// 节点类型定义
const nodeTypes = [
  { type: 'Start', name: '开始节点', icon: 'play-circle' },
  { type: 'End', name: '结束节点', icon: 'stop' },
  { type: 'Task', name: '任务节点', icon: 'form' },
  { type: 'Gateway', name: '网关节点', icon: 'fork' },
  { type: 'ParallelGateway', name: '并行网关', icon: 'branches' },
  { type: 'ExclusiveGateway', name: '排他网关', icon: 'question-circle' },
  { type: 'InclusiveGateway', name: '包容网关', icon: 'plus-circle' },
  { type: 'SubProcess', name: '子流程', icon: 'apartment' },
];

// 工作流数据
const workflow = reactive<LeanWorkflowDesign>({
  id: '',
  name: '新建工作流',
  description: '',
  version: 1,
  createTime: new Date(),
  updateTime: new Date(),
  nodes: [],
  connections: [],
  form: null,
});

// 选中的节点和连线
const selectedNode = ref<LeanWorkflowNode | null>(null);
const selectedConnection = ref<LeanWorkflowConnection | null>(null);

// 预览对话框
const previewVisible = ref(false);

// 模拟数据
const users = ref([
  { id: '1', name: '用户1' },
  { id: '2', name: '用户2' },
  { id: '3', name: '用户3' },
]);

const forms = ref([
  { id: '1', name: '表单1' },
  { id: '2', name: '表单2' },
  { id: '3', name: '表单3' },
]);

// 方法
const getNodeIcon = (type: string) => {
  const nodeType = nodeTypes.find(t => t.type === type);
  return nodeType?.icon || 'question';
};

const getPropertiesPanelTitle = () => {
  if (selectedNode.value) {
    return '节点属性';
  }
  if (selectedConnection.value) {
    return '连线属性';
  }
  return '属性';
};

const onDragStart = (event: DragEvent, nodeType: any) => {
  event.dataTransfer?.setData('nodeType', nodeType.type);
};

const onDragOver = (event: DragEvent) => {
  event.preventDefault();
};

const onDrop = (event: DragEvent) => {
  event.preventDefault();
  const nodeType = event.dataTransfer?.getData('nodeType');
  if (nodeType) {
    const rect = (event.target as HTMLElement).getBoundingClientRect();
    const x = event.clientX - rect.left;
    const y = event.clientY - rect.top;

    workflow.nodes.push({
      id: `node_${Date.now()}`,
      type: nodeType as any,
      name: `${nodeType}节点`,
      description: '',
      x,
      y,
      properties: {},
      participants: [],
    });
  }
};

const onCanvasClick = () => {
  selectedNode.value = null;
  selectedConnection.value = null;
};

const selectNode = (node: LeanWorkflowNode) => {
  selectedNode.value = node;
  selectedConnection.value = null;
};

const selectConnection = (connection: LeanWorkflowConnection) => {
  selectedConnection.value = connection;
  selectedNode.value = null;
};

const startConnection = (event: MouseEvent, node: LeanWorkflowNode, portType: 'in' | 'out') => {
  // 实现连线逻辑
};

const getConnectionPath = (connection: LeanWorkflowConnection) => {
  // 计算连线路径
  const sourceNode = workflow.nodes.find(n => n.id === connection.sourceNodeId);
  const targetNode = workflow.nodes.find(n => n.id === connection.targetNodeId);
  if (!sourceNode || !targetNode) return '';

  const startX = sourceNode.x + 100; // 节点宽度
  const startY = sourceNode.y + 25; // 节点高度的一半
  const endX = targetNode.x;
  const endY = targetNode.y + 25;

  const controlPoint1X = startX + (endX - startX) / 2;
  const controlPoint1Y = startY;
  const controlPoint2X = startX + (endX - startX) / 2;
  const controlPoint2Y = endY;

  return `M ${startX} ${startY} C ${controlPoint1X} ${controlPoint1Y}, ${controlPoint2X} ${controlPoint2Y}, ${endX} ${endY}`;
};

const saveWorkflow = async () => {
  try {
    await saveWorkflowDesign(workflow);
    message.success('保存成功');
  } catch (error) {
    message.error('保存失败');
  }
};

const previewWorkflow = () => {
  previewVisible.value = true;
};
</script>

<style lang="less" scoped>
.workflow-designer {
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

      .node-type-item {
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
      position: relative;
      border: 1px solid #d9d9d9;
      border-radius: 4px;
      overflow: hidden;

      .workflow-node {
        position: absolute;
        width: 100px;
        height: 50px;
        background-color: #fff;
        border: 1px solid #d9d9d9;
        border-radius: 4px;
        cursor: move;
        user-select: none;

        &.selected {
          border-color: #1890ff;
          box-shadow: 0 0 0 2px rgba(24, 144, 255, 0.2);
        }

        .node-content {
          display: flex;
          align-items: center;
          justify-content: center;
          height: 100%;
          padding: 0 8px;

          .anticon {
            margin-right: 4px;
          }
        }

        .node-ports {
          position: absolute;
          width: 100%;
          height: 100%;
          top: 0;
          left: 0;
          pointer-events: none;

          .port {
            position: absolute;
            width: 10px;
            height: 10px;
            background-color: #fff;
            border: 1px solid #d9d9d9;
            border-radius: 50%;
            pointer-events: auto;
            cursor: crosshair;

            &.port-in {
              top: 50%;
              left: -5px;
              transform: translateY(-50%);
            }

            &.port-out {
              top: 50%;
              right: -5px;
              transform: translateY(-50%);
            }
          }
        }
      }

      .connections-layer {
        position: absolute;
        width: 100%;
        height: 100%;
        pointer-events: none;

        path {
          fill: none;
          stroke: #d9d9d9;
          stroke-width: 2;
          pointer-events: auto;
          cursor: pointer;

          &.selected {
            stroke: #1890ff;
            stroke-width: 3;
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
    }
  }
}
</style> 
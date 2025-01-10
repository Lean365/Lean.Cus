/*----------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：types.ts
 * 文件功能描述：工作流相关的类型定义
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ----------------------------------------------------------------*/

// 工作流节点类型
export type WorkflowNodeType = 'start' | 'end' | 'task' | 'gateway' | 'subprocess'

// 工作流节点
export interface WorkflowNode {
  id: string
  type: WorkflowNodeType
  name: string
  properties: Record<string, any>
  position: {
    x: number
    y: number
  }
}

// 工作流边
export interface WorkflowEdge {
  id: string
  source: string
  target: string
  type: string
  properties: Record<string, any>
}

// 工作流设计
export interface WorkflowDesign {
  id?: string
  name: string
  version: number
  nodes: WorkflowNode[]
  edges: WorkflowEdge[]
}

// 工作流表单
export interface WorkflowForm {
  id: string
  name: string
  fields: WorkflowFormField[]
}

// 工作流表单字段
export interface WorkflowFormField {
  id: string
  name: string
  label: string
  type: string
  required: boolean
  defaultValue?: any
  options?: any[]
  properties: Record<string, any>
}

// 工作流实例
export interface WorkflowInstance {
  id: string
  workflowId: string
  workflowName: string
  status: string
  startTime: string
  endTime?: string
  currentNode?: string
  variables: Record<string, any>
}

// 工作流任务
export interface WorkflowTask {
  id: string
  instanceId: string
  nodeId: string
  nodeName: string
  assignee?: string
  status: string
  createTime: string
  completeTime?: string
  formData?: Record<string, any>
}

// 工作流历史记录
export interface WorkflowHistory {
  id: string
  instanceId: string
  nodeId: string
  nodeName: string
  type: string
  operator: string
  operateTime: string
  comment?: string
  data?: Record<string, any>
}

// 工作流统计数据
export interface WorkflowStatistics {
  totalInstances: number
  runningInstances: number
  completedInstances: number
  totalTasks: number
  pendingTasks: number
  completedTasks: number
}

// 工作流版本
export interface WorkflowVersion {
  id: string
  workflowId: string
  version: number
  status: 'draft' | 'published' | 'deprecated'
  createTime: string
  publishTime?: string
  description?: string
}

// 工作流发布配置
export interface PublishConfig {
  workflowId: string
  version: number
  description?: string
  isForceUpdate?: boolean
}

// 工作流状态
export interface WorkflowStatus {
  instanceId: string
  status: string
  currentNode?: string
  startTime: string
  endTime?: string
  variables: Record<string, any>
}

// 工作流任务历史
export interface WorkflowTaskHistory {
  id: string
  instanceId: string
  taskId: string
  nodeId: string
  nodeName: string
  assignee: string
  action: string
  comment?: string
  createTime: string
}

// 工作流指标
export interface WorkflowMetrics {
  instanceId: string
  startTime: string
  endTime?: string
  duration?: number
  nodeCount: number
  completedNodeCount: number
  taskCount: number
  completedTaskCount: number
  status: string
}

// 节点指标
export interface NodeMetrics {
  nodeId: string
  nodeName: string
  instanceCount: number
  averageDuration: number
  minDuration: number
  maxDuration: number
  completionRate: number
} 
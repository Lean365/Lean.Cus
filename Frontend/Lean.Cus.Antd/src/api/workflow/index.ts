/*----------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.ts
 * 文件功能描述：工作流API接口
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ----------------------------------------------------------------*/

import request from '@/utils/request'
import type { WorkflowDesign, WorkflowForm, WorkflowStatus, WorkflowTask, WorkflowTaskHistory, WorkflowMetrics, NodeMetrics, WorkflowVersion, PublishConfig } from './types'

// 工作流设计
export function saveDesign(design: WorkflowDesign) {
  return request<string>({
    url: '/api/workflow/design',
    method: 'post',
    data: design
  })
}

export function getDesign(workflowId: string) {
  return request<WorkflowDesign>({
    url: `/api/workflow/design/${workflowId}`,
    method: 'get'
  })
}

// 工作流版本管理
export function getVersions(workflowId: string) {
  return request<WorkflowVersion[]>({
    url: `/api/workflow/versions/${workflowId}`,
    method: 'get'
  })
}

export function getVersionDesign(workflowId: string, version: number) {
  return request<WorkflowDesign>({
    url: `/api/workflow/versions/${workflowId}/${version}`,
    method: 'get'
  })
}

export function publishVersion(config: PublishConfig) {
  return request<void>({
    url: '/api/workflow/versions/publish',
    method: 'post',
    data: config
  })
}

export function deprecateVersion(workflowId: string, version: number) {
  return request<void>({
    url: `/api/workflow/versions/${workflowId}/${version}/deprecate`,
    method: 'post'
  })
}

// 工作流表单
export function createForm(form: WorkflowForm) {
  return request({
    url: '/api/workflow/forms',
    method: 'post',
    data: form
  })
}

export function updateForm(formId: string, form: WorkflowForm) {
  return request({
    url: `/api/workflow/forms/${formId}`,
    method: 'put',
    data: form
  })
}

export function getForm(formId: string) {
  return request({
    url: `/api/workflow/forms/${formId}`,
    method: 'get'
  })
}

export function deleteForm(formId: string) {
  return request({
    url: `/api/workflow/forms/${formId}`,
    method: 'delete'
  })
}

// 工作流实例
export function startWorkflow(workflowId: string, data: Record<string, any>) {
  return request({
    url: '/api/workflow/instances',
    method: 'post',
    params: { workflowId },
    data
  })
}

export function suspendWorkflow(instanceId: string) {
  return request({
    url: `/api/workflow/instances/${instanceId}/suspend`,
    method: 'post'
  })
}

export function resumeWorkflow(instanceId: string) {
  return request({
    url: `/api/workflow/instances/${instanceId}/resume`,
    method: 'post'
  })
}

export function terminateWorkflow(instanceId: string) {
  return request({
    url: `/api/workflow/instances/${instanceId}/terminate`,
    method: 'post'
  })
}

export function getWorkflowStatus(instanceId: string) {
  return request<WorkflowStatus>({
    url: `/api/workflow/instances/${instanceId}/status`,
    method: 'get'
  })
}

// 工作流任务
export function getTodoTasks(userId: string) {
  return request<WorkflowTask[]>({
    url: '/api/workflow/tasks/todo',
    method: 'get',
    params: { userId }
  })
}

export function getDoneTasks(userId: string) {
  return request<WorkflowTask[]>({
    url: '/api/workflow/tasks/done',
    method: 'get',
    params: { userId }
  })
}

export function completeTask(taskId: string, userId: string, formData: Record<string, any>, comment?: string) {
  return request({
    url: `/api/workflow/tasks/${taskId}/complete`,
    method: 'post',
    params: { userId, comment },
    data: formData
  })
}

export function transferTask(taskId: string, userId: string, targetUserId: string, comment?: string) {
  return request({
    url: `/api/workflow/tasks/${taskId}/transfer`,
    method: 'post',
    params: { userId, targetUserId, comment }
  })
}

// 工作流历史
export function getTaskHistory(instanceId: string) {
  return request<WorkflowTaskHistory[]>({
    url: `/api/workflow/instances/${instanceId}/history`,
    method: 'get'
  })
}

// 工作流指标
export function getWorkflowMetrics(instanceId: string) {
  return request<WorkflowMetrics>({
    url: `/api/workflow/instances/${instanceId}/metrics`,
    method: 'get'
  })
}

export function getNodeMetrics(instanceId: string, nodeId: string) {
  return request<NodeMetrics>({
    url: `/api/workflow/instances/${instanceId}/nodes/${nodeId}/metrics`,
    method: 'get'
  })
} 
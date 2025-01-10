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

import { request } from '@/utils/request'
import type { WorkflowDesign, WorkflowForm, WorkflowStatus, WorkflowTask, WorkflowTaskHistory, WorkflowMetrics, NodeMetrics, WorkflowVersion, PublishConfig } from './types'

// 工作流设计
export function saveDesign(design: WorkflowDesign) {
  return request.post<string>('/api/workflow/design', design)
}

export function getDesign(workflowId: string) {
  return request.get<WorkflowDesign>(`/api/workflow/design/${workflowId}`)
}

// 工作流版本管理
export function getVersions(workflowId: string) {
  return request.get<WorkflowVersion[]>(`/api/workflow/versions/${workflowId}`)
}

export function getVersionDesign(workflowId: string, version: number) {
  return request.get<WorkflowDesign>(`/api/workflow/versions/${workflowId}/${version}`)
}

export function publishVersion(config: PublishConfig) {
  return request.post<void>('/api/workflow/versions/publish', config)
}

export function deprecateVersion(workflowId: string, version: number) {
  return request.post<void>(`/api/workflow/versions/${workflowId}/${version}/deprecate`)
}

// 工作流表单
export function createForm(form: WorkflowForm) {
  return request.post('/api/workflow/forms', form)
}

export function updateForm(formId: string, form: WorkflowForm) {
  return request.put(`/api/workflow/forms/${formId}`, form)
}

export function getForm(formId: string) {
  return request.get(`/api/workflow/forms/${formId}`)
}

export function deleteForm(formId: string) {
  return request.delete(`/api/workflow/forms/${formId}`)
}

// 工作流实例
export function startWorkflow(workflowId: string, data: Record<string, any>) {
  return request.post('/api/workflow/instances', data, { params: { workflowId } })
}

export function suspendWorkflow(instanceId: string) {
  return request.post(`/api/workflow/instances/${instanceId}/suspend`)
}

export function resumeWorkflow(instanceId: string) {
  return request.post(`/api/workflow/instances/${instanceId}/resume`)
}

export function terminateWorkflow(instanceId: string) {
  return request.post(`/api/workflow/instances/${instanceId}/terminate`)
}

export function getWorkflowStatus(instanceId: string) {
  return request.get<WorkflowStatus>(`/api/workflow/instances/${instanceId}/status`)
}

// 工作流任务
export function getTodoTasks(userId: string) {
  return request.get<WorkflowTask[]>('/api/workflow/tasks/todo', { params: { userId } })
}

export function getDoneTasks(userId: string) {
  return request.get<WorkflowTask[]>('/api/workflow/tasks/done', { params: { userId } })
}

export function completeTask(taskId: string, userId: string, formData: Record<string, any>, comment?: string) {
  return request.post(`/api/workflow/tasks/${taskId}/complete`, formData, { params: { userId, comment } })
}

export function transferTask(taskId: string, userId: string, targetUserId: string, comment?: string) {
  return request.post(`/api/workflow/tasks/${taskId}/transfer`, null, { params: { userId, targetUserId, comment } })
}

// 工作流历史
export function getTaskHistory(instanceId: string) {
  return request.get<WorkflowTaskHistory[]>(`/api/workflow/instances/${instanceId}/history`)
}

// 工作流指标
export function getWorkflowMetrics(instanceId: string) {
  return request.get<WorkflowMetrics>(`/api/workflow/instances/${instanceId}/metrics`)
}

export function getNodeMetrics(instanceId: string, nodeId: string) {
  return request.get<NodeMetrics>(`/api/workflow/instances/${instanceId}/nodes/${nodeId}/metrics`)
} 
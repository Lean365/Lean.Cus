/*----------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：properties.ts
 * 文件功能描述：工作流节点属性处理工具类
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ----------------------------------------------------------------*/

import type { Node } from '@antv/x6'

// 节点类型
export enum NodeType {
  Start = 'start',
  Task = 'task',
  Gateway = 'gateway',
  End = 'end'
}

// 节点属性
export interface NodeProperties {
  // 开始节点属性
  initiatorType?: 'all' | 'specific' | 'role' | 'dept'
  initiatorList?: string[]
  roleList?: string[]
  deptList?: string[]
  formId?: string
  titleTemplate?: string

  // 任务节点属性
  taskType?: 'approval' | 'handle' | 'notice'
  assigneeType?: 'specific' | 'role' | 'dept' | 'expr'
  assigneeList?: string[]
  assigneeExpr?: string
  enableTimeout?: boolean
  timeoutHours?: number
  timeoutAction?: 'notice' | 'transfer' | 'approve' | 'reject'
  enableCountersign?: boolean
  countersignRule?: 'all' | 'any' | 'ratio'
  countersignRatio?: number
  buttons?: Array<{
    name: string
    type: 'approve' | 'reject' | 'transfer' | 'custom'
    action?: string
  }>

  // 网关节点属性
  gatewayType?: 'exclusive' | 'parallel' | 'inclusive'
  conditions?: Array<{
    name: string
    targetBranch: string
    expression: string
  }>
  defaultBranch?: string
  parallelType?: 'all' | 'any' | 'ratio'
  completionRatio?: number

  // 结束节点属性
  endType?: 'normal' | 'terminate'
  notifyTypes?: Array<'email' | 'sms' | 'wechat' | 'dingtalk'>
  notifyTargets?: Array<'initiator' | 'participants' | 'supervisor'>
  notifyTemplate?: string
  enableArchive?: boolean
  archiveType?: 'database' | 'file'
  archivePath?: string

  // 通用属性
  beforeCallback?: string
  afterCallback?: string
}

// 获取节点类型
export function getNodeType(node: Node): NodeType {
  const shape = node.shape as string
  if (shape.includes('start')) {
    return NodeType.Start
  }
  if (shape.includes('task')) {
    return NodeType.Task
  }
  if (shape.includes('gateway')) {
    return NodeType.Gateway
  }
  if (shape.includes('end')) {
    return NodeType.End
  }
  throw new Error(`未知的节点类型: ${shape}`)
}

// 获取节点默认属性
export function getDefaultProperties(type: NodeType): NodeProperties {
  switch (type) {
    case NodeType.Start:
      return {
        initiatorType: 'all',
        initiatorList: [],
        roleList: [],
        deptList: [],
        formId: undefined,
        titleTemplate: '',
        beforeCallback: '',
        afterCallback: ''
      }
    case NodeType.Task:
      return {
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
      }
    case NodeType.Gateway:
      return {
        gatewayType: 'exclusive',
        conditions: [],
        defaultBranch: undefined,
        parallelType: 'all',
        completionRatio: 100,
        beforeCallback: '',
        afterCallback: ''
      }
    case NodeType.End:
      return {
        endType: 'normal',
        notifyTypes: [],
        notifyTargets: [],
        notifyTemplate: '',
        enableArchive: false,
        archiveType: 'database',
        archivePath: '',
        beforeCallback: '',
        afterCallback: ''
      }
    default:
      return {}
  }
}

// 验证节点属性
export function validateProperties(type: NodeType, properties: NodeProperties): string[] {
  const errors: string[] = []

  switch (type) {
    case NodeType.Start:
      if (properties.initiatorType === 'specific' && (!properties.initiatorList || properties.initiatorList.length === 0)) {
        errors.push('请选择发起人')
      }
      if (properties.initiatorType === 'role' && (!properties.roleList || properties.roleList.length === 0)) {
        errors.push('请选择角色')
      }
      if (properties.initiatorType === 'dept' && (!properties.deptList || properties.deptList.length === 0)) {
        errors.push('请选择部门')
      }
      if (!properties.formId) {
        errors.push('请选择关联表单')
      }
      break

    case NodeType.Task:
      if (properties.assigneeType === 'specific' && (!properties.assigneeList || properties.assigneeList.length === 0)) {
        errors.push('请选择处理人')
      }
      if (properties.assigneeType === 'role' && (!properties.roleList || properties.roleList.length === 0)) {
        errors.push('请选择角色')
      }
      if (properties.assigneeType === 'dept' && (!properties.deptList || properties.deptList.length === 0)) {
        errors.push('请选择部门')
      }
      if (properties.assigneeType === 'expr' && !properties.assigneeExpr) {
        errors.push('请输入处理人表达式')
      }
      if (properties.enableTimeout && (!properties.timeoutHours || properties.timeoutHours <= 0)) {
        errors.push('请设置超时时间')
      }
      if (properties.enableCountersign && properties.countersignRule === 'ratio' && (!properties.countersignRatio || properties.countersignRatio <= 0)) {
        errors.push('请设置会签比例')
      }
      break

    case NodeType.Gateway:
      if ((properties.gatewayType === 'exclusive' || properties.gatewayType === 'inclusive') && (!properties.conditions || properties.conditions.length === 0)) {
        errors.push('请添加分支条件')
      }
      if (properties.gatewayType === 'parallel' && properties.parallelType === 'ratio' && (!properties.completionRatio || properties.completionRatio <= 0)) {
        errors.push('请设置完成比例')
      }
      break

    case NodeType.End:
      if (properties.notifyTypes && properties.notifyTypes.length > 0 && (!properties.notifyTargets || properties.notifyTargets.length === 0)) {
        errors.push('请选择通知对象')
      }
      if (properties.notifyTypes && properties.notifyTypes.length > 0 && !properties.notifyTemplate) {
        errors.push('请输入通知模板')
      }
      if (properties.enableArchive && properties.archiveType === 'file' && !properties.archivePath) {
        errors.push('请输入归档路径')
      }
      break
  }

  return errors
}

// 序列化节点属性
export function serializeProperties(properties: NodeProperties): string {
  return JSON.stringify(properties)
}

// 反序列化节点属性
export function deserializeProperties(data: string): NodeProperties {
  try {
    return JSON.parse(data)
  } catch (error) {
    console.error('反序列化节点属性失败:', error)
    return {}
  }
}

// 克隆节点属性
export function cloneProperties(properties: NodeProperties): NodeProperties {
  return JSON.parse(JSON.stringify(properties))
}

// 合并节点属性
export function mergeProperties(target: NodeProperties, source: NodeProperties): NodeProperties {
  return {
    ...target,
    ...source
  }
}

// 清理节点属性
export function cleanProperties(properties: NodeProperties): NodeProperties {
  const cleaned: Record<string, any> = {}
  for (const [key, value] of Object.entries(properties)) {
    if (value !== undefined && value !== null && value !== '') {
      if (Array.isArray(value)) {
        if (value.length > 0) {
          cleaned[key] = value
        }
      } else if (typeof value === 'object') {
        const cleanedObj = cleanProperties(value as NodeProperties)
        if (Object.keys(cleanedObj).length > 0) {
          cleaned[key] = cleanedObj
        }
      } else {
        cleaned[key] = value
      }
    }
  }
  return cleaned
}

// 格式化表达式
export function formatExpression(expr: string, context: Record<string, any>): string {
  return expr.replace(/\${([\w.]+)}/g, (match, key) => {
    const keys = key.split('.')
    let value = context
    for (const k of keys) {
      value = value[k]
      if (value === undefined) {
        return match
      }
    }
    return String(value)
  })
}

// 解析表达式
export function parseExpression(expr: string): string[] {
  const variables: string[] = []
  const regex = /\${([\w.]+)}/g
  let match
  while ((match = regex.exec(expr)) !== null) {
    variables.push(match[1])
  }
  return variables
}

// 验证表达式
export function validateExpression(expr: string, allowedVariables: string[]): boolean {
  const variables = parseExpression(expr)
  return variables.every(variable => allowedVariables.includes(variable))
}

// 获取表达式上下文
export function getExpressionContext(type: NodeType): string[] {
  switch (type) {
    case NodeType.Start:
      return [
        'form.field',
        'user.name',
        'user.dept',
        'date'
      ]
    case NodeType.Task:
      return [
        'form.field',
        'user.dept',
        'user.role',
        'workflow.initiator'
      ]
    case NodeType.Gateway:
      return [
        'form.field',
        'user.dept',
        'user.role',
        'workflow.initiator'
      ]
    case NodeType.End:
      return [
        'workflow.title',
        'workflow.initiator',
        'workflow.startTime',
        'workflow.endTime',
        'workflow.duration',
        'workflow.result'
      ]
    default:
      return []
  }
} 
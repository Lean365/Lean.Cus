/*----------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：index.ts
 * 文件功能描述：代码生成器API接口
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ----------------------------------------------------------------*/

import request from '@/utils/request'
import type { TableDesign, GenerateConfig, GenerateResult } from './types'

// 获取表设计
export function getTableDesign(tableName: string) {
  return request<TableDesign>({
    url: `/api/generator/design/${tableName}`,
    method: 'get'
  })
}

// 保存表设计
export function saveTableDesign(design: TableDesign) {
  return request<void>({
    url: '/api/generator/design',
    method: 'post',
    data: design
  })
}

// 同步数据库
export function syncDatabase(tableName: string) {
  return request<void>({
    url: `/api/generator/sync/${tableName}`,
    method: 'post'
  })
}

// 生成代码
export function generateCode(config: GenerateConfig) {
  return request<GenerateResult>({
    url: '/api/generator/generate',
    method: 'post',
    data: config
  })
} 
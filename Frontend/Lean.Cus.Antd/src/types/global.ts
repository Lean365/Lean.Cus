/*----------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：global.ts
 * 文件功能描述：全局类型定义
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ----------------------------------------------------------------*/

// 分页查询参数
export interface PagedInput {
  pageNum?: number
  pageSize?: number
  [key: string]: any
}

// 分页查询结果
export interface PagedList<T> {
  total: number
  data: T[]
} 
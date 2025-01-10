/*----------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：types.ts
 * 文件功能描述：代码生成器相关的类型定义
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ----------------------------------------------------------------*/

// 表设计
export interface TableDesign {
  id?: string
  tableName: string
  tableComment: string
  columns: ColumnDesign[]
}

// 列设计
export interface ColumnDesign {
  id?: string
  columnName: string
  columnComment: string
  columnType: string
  isRequired: boolean
  isPrimaryKey: boolean
  isAutoIncrement: boolean
  defaultValue?: string
  length?: number
  precision?: number
  scale?: number
}

// 代码生成配置
export interface GenerateConfig {
  tableName: string
  moduleName: string
  businessName: string
  packageName: string
  functionName: string
  functionAuthor: string
  options: {
    generateApi: boolean
    generateEntity: boolean
    generateService: boolean
    generateController: boolean
    generateVue: boolean
  }
}

// 生成结果
export interface GenerateResult {
  success: boolean
  message?: string
  files?: GeneratedFile[]
}

// 生成的文件
export interface GeneratedFile {
  path: string
  content: string
} 
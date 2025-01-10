// 模板类型
export enum TemplateType {
  Entity = 1,
  Service = 2,
  Controller = 3
}

// 模板接口
export interface Template {
  id: string
  name: string
  description: string
  content: string
  type: TemplateType
  createTime: string
  updateTime: string
}

// 模板表单状态
export interface TemplateFormState {
  id?: string
  name: string
  description: string
  content: string
  type: TemplateType
}

// 代码生成配置
export interface GeneratorConfig {
  templateId: string
  tableName: string
  namespace: string
  outputPath: string
  additionalParams?: Record<string, any>
}

// 代码生成结果
export interface GeneratorResult {
  success: boolean
  message: string
  filePath?: string
} 
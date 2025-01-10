import { request } from '@/utils/request'

// 模板相关的接口
export interface Template {
  id: string
  name: string
  description: string
  content: string
  type: number
  createTime: string
  updateTime: string
}

// 获取模板列表
export const getTemplateList = () => {
  return request.get<Template[]>('/api/LeanTemplate/GetList')
}

// 获取单个模板
export const getTemplate = (id: string) => {
  return request.get<Template>(`/api/LeanTemplate/Get/${id}`)
}

// 创建模板
export const createTemplate = (data: Partial<Template>) => {
  return request.post<Template>('/api/LeanTemplate/Create', data)
}

// 更新模板
export const updateTemplate = (data: Partial<Template>) => {
  return request.put<Template>('/api/LeanTemplate/Update', data)
}

// 删除模板
export const deleteTemplate = (id: string) => {
  return request.delete<void>(`/api/LeanTemplate/Delete/${id}`)
} 
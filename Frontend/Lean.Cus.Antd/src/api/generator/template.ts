import { http } from '@/utils/http'

/**
 * 模板DTO
 */
export interface LeanTemplateDto {
  /**
   * 编号
   */
  id?: number
  /**
   * 模板类型
   */
  templateType: string
  /**
   * 文件名
   */
  fileName: string
  /**
   * 模板内容
   */
  templateContent: string
  /**
   * 备注
   */
  remark?: string
}

/**
 * 创建模板
 * @param dto 模板DTO
 * @returns 创建结果
 */
export function createTemplate(dto: LeanTemplateDto) {
  return http.post<boolean>('/api/generator/template', dto)
}

/**
 * 更新模板
 * @param id 模板编号
 * @param dto 模板DTO
 * @returns 更新结果
 */
export function updateTemplate(id: number, dto: LeanTemplateDto) {
  return http.put<boolean>(`/api/generator/template/${id}`, dto)
}

/**
 * 删除模板
 * @param id 模板编号
 * @returns 删除结果
 */
export function deleteTemplate(id: number) {
  return http.delete<boolean>(`/api/generator/template/${id}`)
}

/**
 * 获取模板
 * @param id 模板编号
 * @returns 模板DTO
 */
export function getTemplate(id: number) {
  return http.get<LeanTemplateDto>(`/api/generator/template/${id}`)
}

/**
 * 获取模板列表
 * @param templateType 模板类型
 * @returns 模板DTO列表
 */
export function getTemplateList(templateType?: string) {
  return http.get<LeanTemplateDto[]>('/api/generator/template', { params: { templateType } })
}

/**
 * 预览模板
 * @param id 模板编号
 * @param tableId 表编号
 * @returns 预览结果
 */
export function previewTemplate(id: number, tableId: number) {
  return http.get<string>(`/api/generator/template/${id}/preview`, { params: { tableId } })
} 
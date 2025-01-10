import { http } from '@/utils/http'

/**
 * 预览代码
 * @param tableId 表编号
 * @returns 预览结果
 */
export function previewCode(tableId: number) {
  return http.get<Record<string, string>>(`/api/generator/preview/${tableId}`)
}

/**
 * 生成代码
 * @param tableId 表编号
 * @param generatePath 生成路径
 * @returns 生成结果
 */
export function generateCode(tableId: number, generatePath: string) {
  return http.post<boolean>(`/api/generator/generate/${tableId}`, null, { params: { generatePath } })
}

/**
 * 下载代码
 * @param tableId 表编号
 * @returns 代码压缩包
 */
export function downloadCode(tableId: number) {
  return http.get(`/api/generator/download/${tableId}`, { responseType: 'blob' })
} 
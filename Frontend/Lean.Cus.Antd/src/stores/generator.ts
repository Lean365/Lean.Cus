import { defineStore } from 'pinia'
import { ref } from 'vue'
import type { Template, GeneratorConfig } from '@/types/generator'
import {
  getTemplateList,
  createTemplate,
  updateTemplate,
  deleteTemplate
} from '@/api/generator/template'

export const useGeneratorStore = defineStore('generator', () => {
  // 状态
  const templates = ref<Template[]>([])
  const loading = ref(false)
  const currentConfig = ref<GeneratorConfig | null>(null)

  // 获取模板列表
  const fetchTemplates = async () => {
    loading.value = true
    try {
      const data = await getTemplateList()
      templates.value = data
    } catch (error) {
      console.error('Failed to fetch templates:', error)
      throw error
    } finally {
      loading.value = false
    }
  }

  // 创建模板
  const addTemplate = async (template: Partial<Template>) => {
    try {
      const data = await createTemplate(template)
      await fetchTemplates()
      return data
    } catch (error) {
      console.error('Failed to create template:', error)
      throw error
    }
  }

  // 更新模板
  const editTemplate = async (template: Partial<Template>) => {
    try {
      const data = await updateTemplate(template)
      await fetchTemplates()
      return data
    } catch (error) {
      console.error('Failed to update template:', error)
      throw error
    }
  }

  // 删除模板
  const removeTemplate = async (id: string) => {
    try {
      await deleteTemplate(id)
      await fetchTemplates()
    } catch (error) {
      console.error('Failed to delete template:', error)
      throw error
    }
  }

  // 设置当前生成配置
  const setGeneratorConfig = (config: GeneratorConfig) => {
    currentConfig.value = config
  }

  // 清除当前生成配置
  const clearGeneratorConfig = () => {
    currentConfig.value = null
  }

  return {
    // 状态
    templates,
    loading,
    currentConfig,
    // 方法
    fetchTemplates,
    addTemplate,
    editTemplate,
    removeTemplate,
    setGeneratorConfig,
    clearGeneratorConfig
  }
}) 
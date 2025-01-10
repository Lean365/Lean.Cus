import { defineStore } from 'pinia'
import { ref } from 'vue'
import { LeanTemplateDto } from '@/api/generator/template'

export const useGeneratorStore = defineStore('generator', () => {
  // 模板列表
  const templates = ref<LeanTemplateDto[]>([])

  // 设置模板列表
  const setTemplates = (data: LeanTemplateDto[]) => {
    templates.value = data
  }

  return {
    templates,
    setTemplates
  }
}) 
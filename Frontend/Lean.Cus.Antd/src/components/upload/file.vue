<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：file.vue
 * 文件功能描述：文件上传组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：添加上传进度条显示
 ------------------------------------------------------------------>

<template>
  <div class="upload-wrapper">
    <a-upload-dragger
      v-if="draggable"
      v-model:file-list="fileList"
      :action="uploadUrl"
      :headers="headers"
      :before-upload="beforeUpload"
      :data="uploadData"
      :multiple="multiple"
      :show-upload-list="showUploadList"
      :max-count="maxCount"
      :accept="accept"
      @change="handleChange"
      @remove="handleRemove"
      @progress="handleProgress"
    >
      <p class="ant-upload-drag-icon">
        <inbox-outlined />
      </p>
      <p class="ant-upload-text">{{ dragText }}</p>
      <p class="ant-upload-hint">{{ tip }}</p>
      
      <!-- 拖拽模式的进度条 -->
      <template #itemRender="{ file }">
        <div class="upload-list-item">
          <div class="file-icon">
            <file-outlined v-if="file.status !== 'uploading'" />
            <loading-outlined v-else spin />
          </div>
          <div class="file-info">
            <div class="filename">{{ file.name }}</div>
            <div class="filesize">{{ formatFileSize(file.size) }}</div>
            <div v-if="file.status === 'uploading'" class="upload-progress">
              <a-progress
                :percent="file.percent"
                size="small"
                status="active"
              />
            </div>
            <div v-else-if="file.status === 'done'" class="upload-status success">
              <check-circle-outlined />
              <span>上传成功</span>
            </div>
            <div v-else-if="file.status === 'error'" class="upload-status error">
              <close-circle-outlined />
              <span>上传失败</span>
            </div>
          </div>
        </div>
      </template>
    </a-upload-dragger>
    
    <a-upload
      v-else
      v-model:file-list="fileList"
      :action="uploadUrl"
      :headers="headers"
      :before-upload="beforeUpload"
      :data="uploadData"
      :multiple="multiple"
      :show-upload-list="showUploadList"
      :max-count="maxCount"
      :accept="accept"
      @change="handleChange"
      @remove="handleRemove"
      @progress="handleProgress"
    >
      <a-button>
        <upload-outlined />
        {{ buttonText }}
      </a-button>
      
      <!-- 传统模式的进度条 -->
      <template #itemRender="{ file }">
        <div class="upload-list-item">
          <div class="file-icon">
            <file-outlined v-if="file.status !== 'uploading'" />
            <loading-outlined v-else spin />
          </div>
          <div class="file-info">
            <div class="filename">{{ file.name }}</div>
            <div class="filesize">{{ formatFileSize(file.size) }}</div>
            <div v-if="file.status === 'uploading'" class="upload-progress">
              <a-progress
                :percent="file.percent"
                size="small"
                status="active"
              />
            </div>
            <div v-else-if="file.status === 'done'" class="upload-status success">
              <check-circle-outlined />
              <span>上传成功</span>
            </div>
            <div v-else-if="file.status === 'error'" class="upload-status error">
              <close-circle-outlined />
              <span>上传失败</span>
            </div>
          </div>
        </div>
      </template>
    </a-upload>
    
    <div v-if="!draggable && tip" class="upload-tip">
      {{ tip }}
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { message } from 'ant-design-vue'
import {
  UploadOutlined,
  InboxOutlined,
  FileOutlined,
  LoadingOutlined,
  CheckCircleOutlined,
  CloseCircleOutlined
} from '@ant-design/icons-vue'
import type { UploadProps, UploadFile } from 'ant-design-vue'

interface Props {
  /** 上传地址 */
  action: string
  /** 上传时附带的额外参数 */
  data?: Record<string, any>
  /** 设置上传的请求头部 */
  headers?: Record<string, string>
  /** 是否支持多选 */
  multiple?: boolean
  /** 是否显示上传列表 */
  showUploadList?: boolean
  /** 限制上传数量 */
  maxCount?: number
  /** 接受上传的文件类型 */
  accept?: string
  /** 按钮文字 */
  buttonText?: string
  /** 拖拽提示文字 */
  dragText?: string
  /** 提示文字 */
  tip?: string
  /** 是否启用拖拽上传 */
  draggable?: boolean
  /** 文件大小限制（MB） */
  maxSize?: number
}

const props = withDefaults(defineProps<Props>(), {
  multiple: false,
  showUploadList: true,
  maxCount: 1,
  buttonText: '上传文件',
  dragText: '点击或拖拽文件到此区域上传',
  tip: '',
  draggable: false,
  maxSize: 10
})

const emit = defineEmits<{
  (e: 'update:fileList', files: UploadFile[]): void
  (e: 'change', info: any): void
  (e: 'success', response: any): void
  (e: 'error', error: any): void
  (e: 'progress', file: UploadFile, percent: number): void
}>()

// 文件列表
const fileList = ref<UploadFile[]>([])

// 上传地址
const uploadUrl = computed(() => props.action)

// 上传参数
const uploadData = computed(() => props.data || {})

// 上传头部
const headers = computed(() => props.headers || {})

// 上传前校验
const beforeUpload = (file: File) => {
  // 文件大小限制
  const isLtMaxSize = file.size / 1024 / 1024 < props.maxSize
  if (!isLtMaxSize) {
    message.error(`文件大小不能超过${props.maxSize}MB!`)
    return false
  }

  return true
}

// 处理上传进度
const handleProgress = (e: { percent: number }, file: UploadFile) => {
  emit('progress', file, e.percent)
}

// 处理上传状态改变
const handleChange: UploadProps['onChange'] = (info) => {
  fileList.value = info.fileList
  emit('update:fileList', info.fileList)
  emit('change', info)

  if (info.file.status === 'done') {
    message.success(`${info.file.name} 上传成功`)
    emit('success', info.file.response)
  } else if (info.file.status === 'error') {
    message.error(`${info.file.name} 上传失败`)
    emit('error', info.file.error)
  }
}

// 处理移除文件
const handleRemove = (file: UploadFile) => {
  const index = fileList.value.indexOf(file)
  const newFileList = fileList.value.slice()
  newFileList.splice(index, 1)
  fileList.value = newFileList
  emit('update:fileList', newFileList)
}

// 格式化文件大小
const formatFileSize = (size: number): string => {
  if (size === undefined) return '0 B'
  const units = ['B', 'KB', 'MB', 'GB', 'TB']
  let index = 0
  while (size >= 1024 && index < units.length - 1) {
    size /= 1024
    index++
  }
  return `${size.toFixed(2)} ${units[index]}`
}
</script>

<style lang="less" scoped>
.upload-wrapper {
  :deep(.ant-upload-select) {
    margin-bottom: 8px;
  }
  
  :deep(.ant-upload-drag) {
    .anticon {
      font-size: 48px;
      color: #1677ff;
    }
    
    .ant-upload-text {
      margin: 0 0 4px;
      color: rgba(0, 0, 0, 0.88);
      font-size: 16px;
    }
    
    .ant-upload-hint {
      color: rgba(0, 0, 0, 0.45);
      font-size: 14px;
    }
  }
  
  .upload-list-item {
    display: flex;
    align-items: flex-start;
    padding: 8px;
    border-radius: 4px;
    
    .file-icon {
      margin-right: 8px;
      font-size: 24px;
      color: rgba(0, 0, 0, 0.45);
    }
    
    .file-info {
      flex: 1;
      min-width: 0;
      
      .filename {
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
      }
      
      .filesize {
        margin-top: 4px;
        color: rgba(0, 0, 0, 0.45);
        font-size: 12px;
      }
      
      .upload-progress {
        margin-top: 8px;
      }
      
      .upload-status {
        display: flex;
        align-items: center;
        margin-top: 8px;
        font-size: 14px;
        
        .anticon {
          margin-right: 4px;
        }
        
        &.success {
          color: #52c41a;
        }
        
        &.error {
          color: #ff4d4f;
        }
      }
    }
  }
  
  .upload-tip {
    margin-top: 8px;
    color: rgba(0, 0, 0, 0.45);
    font-size: 12px;
  }
}
</style> 
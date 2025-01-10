<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：image.vue
 * 文件功能描述：图片上传组件
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
      :accept="accept || 'image/*'"
      list-type="picture"
      @preview="handlePreview"
      @change="handleChange"
      @remove="handleRemove"
      @progress="handleProgress"
    >
      <p class="ant-upload-drag-icon">
        <picture-outlined />
      </p>
      <p class="ant-upload-text">{{ dragText }}</p>
      <p class="ant-upload-hint">{{ tip }}</p>
      
      <!-- 拖拽模式的进度条 -->
      <template #itemRender="{ file }">
        <div class="upload-list-item">
          <div class="image-wrapper">
            <img
              v-if="file.url || file.preview"
              :src="file.url || file.preview"
              :alt="file.name"
              class="image-thumb"
            />
            <div v-else class="image-placeholder">
              <picture-outlined />
            </div>
            <div v-if="file.status === 'uploading'" class="upload-progress">
              <a-progress
                type="circle"
                :percent="file.percent"
                :width="46"
                :stroke-width="6"
                status="active"
              />
            </div>
            <div v-else-if="file.status === 'done'" class="upload-status success">
              <check-circle-outlined />
            </div>
            <div v-else-if="file.status === 'error'" class="upload-status error">
              <close-circle-outlined />
            </div>
          </div>
          <div class="image-info">
            <div class="filename">{{ file.name }}</div>
            <div class="filesize">{{ formatFileSize(file.size) }}</div>
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
      :accept="accept || 'image/*'"
      list-type="picture-card"
      @preview="handlePreview"
      @change="handleChange"
      @remove="handleRemove"
      @progress="handleProgress"
    >
      <div v-if="fileList.length < maxCount">
        <plus-outlined />
        <div class="ant-upload-text">{{ buttonText }}</div>
      </div>
      
      <!-- 传统模式的进度条 -->
      <template #itemRender="{ file }">
        <div class="upload-card-item">
          <img
            v-if="file.url || file.preview"
            :src="file.url || file.preview"
            :alt="file.name"
            class="image-thumb"
          />
          <div v-else class="image-placeholder">
            <picture-outlined />
          </div>
          <div v-if="file.status === 'uploading'" class="upload-progress">
            <a-progress
              type="circle"
              :percent="file.percent"
              :width="46"
              :stroke-width="6"
              status="active"
            />
          </div>
          <div v-else-if="file.status === 'done'" class="upload-status success">
            <check-circle-outlined />
          </div>
          <div v-else-if="file.status === 'error'" class="upload-status error">
            <close-circle-outlined />
          </div>
        </div>
      </template>
    </a-upload>
    
    <a-modal
      :visible="previewVisible"
      :title="previewTitle"
      :footer="null"
      @cancel="handleCancel"
    >
      <img :src="previewImage" :alt="previewTitle" style="width: 100%" />
    </a-modal>
    
    <div v-if="!draggable && tip" class="upload-tip">
      {{ tip }}
    </div>
  </div>
</template>

<script lang="ts" setup>
import { ref, computed } from 'vue'
import { message } from 'ant-design-vue'
import {
  PlusOutlined,
  PictureOutlined,
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
  /** 图片大小限制（MB） */
  maxSize?: number
  /** 图片尺寸限制 */
  dimension?: {
    width?: number
    height?: number
    minWidth?: number
    minHeight?: number
    maxWidth?: number
    maxHeight?: number
  }
}

const props = withDefaults(defineProps<Props>(), {
  multiple: false,
  showUploadList: true,
  maxCount: 1,
  buttonText: '上传图片',
  dragText: '点击或拖拽图片到此区域上传',
  tip: '',
  draggable: false,
  maxSize: 5,
  dimension: () => ({})
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

// 预览相关
const previewVisible = ref(false)
const previewImage = ref('')
const previewTitle = ref('')

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
    message.error(`图片大小不能超过${props.maxSize}MB!`)
    return false
  }

  // 文件类型限制
  const isImage = file.type.startsWith('image/')
  if (!isImage) {
    message.error('只能上传图片文件!')
    return false
  }

  // 图片尺寸限制
  if (Object.keys(props.dimension).length > 0) {
    return new Promise((resolve, reject) => {
      const reader = new FileReader()
      reader.readAsDataURL(file)
      reader.onload = () => {
        const img = new Image()
        img.src = reader.result as string
        img.onload = () => {
          const { width, height } = img
          const {
            width: exactWidth,
            height: exactHeight,
            minWidth,
            minHeight,
            maxWidth,
            maxHeight
          } = props.dimension

          if (exactWidth && width !== exactWidth) {
            message.error(`图片宽度必须为 ${exactWidth}px!`)
            reject()
            return
          }
          if (exactHeight && height !== exactHeight) {
            message.error(`图片高度必须为 ${exactHeight}px!`)
            reject()
            return
          }
          if (minWidth && width < minWidth) {
            message.error(`图片宽度不能小于 ${minWidth}px!`)
            reject()
            return
          }
          if (minHeight && height < minHeight) {
            message.error(`图片高度不能小于 ${minHeight}px!`)
            reject()
            return
          }
          if (maxWidth && width > maxWidth) {
            message.error(`图片宽度不能大于 ${maxWidth}px!`)
            reject()
            return
          }
          if (maxHeight && height > maxHeight) {
            message.error(`图片高度不能大于 ${maxHeight}px!`)
            reject()
            return
          }

          resolve(true)
        }
      }
    })
  }

  return true
}

// 处理预览
const handlePreview = async (file: UploadFile) => {
  if (!file.url && !file.preview) {
    file.preview = await getBase64(file.originFileObj as File)
  }
  previewImage.value = file.url || file.preview || ''
  previewVisible.value = true
  previewTitle.value = file.name || ''
}

// 处理取消预览
const handleCancel = () => {
  previewVisible.value = false
  previewTitle.value = ''
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

// 将文件转换为 Base64
const getBase64 = (file: File): Promise<string> => {
  return new Promise((resolve, reject) => {
    const reader = new FileReader()
    reader.readAsDataURL(file)
    reader.onload = () => resolve(reader.result as string)
    reader.onerror = error => reject(error)
  })
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
    width: 104px;
    height: 104px;
    margin-right: 8px;
    margin-bottom: 8px;
    text-align: center;
    vertical-align: top;
    background-color: #fafafa;
    border: 1px dashed #d9d9d9;
    border-radius: 2px;
    cursor: pointer;
    transition: border-color 0.3s;
    
    &:hover {
      border-color: #1677ff;
    }
    
    .anticon {
      font-size: 24px;
      color: #999;
    }
    
    .ant-upload-text {
      margin-top: 8px;
      color: #666;
    }
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
    
    .image-wrapper {
      position: relative;
      width: 80px;
      height: 80px;
      margin-right: 8px;
      border-radius: 2px;
      overflow: hidden;
      
      .image-thumb {
        width: 100%;
        height: 100%;
        object-fit: cover;
      }
      
      .image-placeholder {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 100%;
        height: 100%;
        background-color: #fafafa;
        
        .anticon {
          font-size: 24px;
          color: #999;
        }
      }
      
      .upload-progress {
        position: absolute;
        top: 0;
        left: 0;
        display: flex;
        align-items: center;
        justify-content: center;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.45);
      }
      
      .upload-status {
        position: absolute;
        top: 0;
        left: 0;
        display: flex;
        align-items: center;
        justify-content: center;
        width: 100%;
        height: 100%;
        background-color: rgba(0, 0, 0, 0.45);
        
        .anticon {
          font-size: 24px;
          color: #fff;
        }
        
        &.success {
          background-color: rgba(82, 196, 26, 0.45);
        }
        
        &.error {
          background-color: rgba(255, 77, 79, 0.45);
        }
      }
    }
    
    .image-info {
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
    }
  }
  
  .upload-card-item {
    position: relative;
    width: 100%;
    height: 100%;
    
    .image-thumb {
      width: 100%;
      height: 100%;
      object-fit: cover;
    }
    
    .image-placeholder {
      display: flex;
      align-items: center;
      justify-content: center;
      width: 100%;
      height: 100%;
      background-color: #fafafa;
      
      .anticon {
        font-size: 24px;
        color: #999;
      }
    }
    
    .upload-progress {
      position: absolute;
      top: 0;
      left: 0;
      display: flex;
      align-items: center;
      justify-content: center;
      width: 100%;
      height: 100%;
      background-color: rgba(0, 0, 0, 0.45);
    }
    
    .upload-status {
      position: absolute;
      top: 0;
      left: 0;
      display: flex;
      align-items: center;
      justify-content: center;
      width: 100%;
      height: 100%;
      background-color: rgba(0, 0, 0, 0.45);
      
      .anticon {
        font-size: 24px;
        color: #fff;
      }
      
      &.success {
        background-color: rgba(82, 196, 26, 0.45);
      }
      
      &.error {
        background-color: rgba(255, 77, 79, 0.45);
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
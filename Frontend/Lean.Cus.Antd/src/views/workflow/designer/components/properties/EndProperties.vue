<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：EndProperties.vue
 * 文件功能描述：结束节点属性编辑组件
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：
 ------------------------------------------------------------------>

<template>
  <div class="end-properties">
    <a-form layout="vertical">
      <!-- 结束类型 -->
      <a-form-item label="结束类型">
        <a-select v-model:value="properties.endType">
          <a-select-option value="normal">正常结束</a-select-option>
          <a-select-option value="terminate">终止流程</a-select-option>
        </a-select>
      </a-form-item>

      <!-- 通知配置 -->
      <a-form-item label="通知配置">
        <a-checkbox-group v-model:value="properties.notifyTypes">
          <a-checkbox value="email">邮件通知</a-checkbox>
          <a-checkbox value="sms">短信通知</a-checkbox>
          <a-checkbox value="wechat">微信通知</a-checkbox>
          <a-checkbox value="dingtalk">钉钉通知</a-checkbox>
        </a-checkbox-group>
      </a-form-item>

      <!-- 通知对象 -->
      <a-form-item label="通知对象">
        <a-checkbox-group v-model:value="properties.notifyTargets">
          <a-checkbox value="initiator">发起人</a-checkbox>
          <a-checkbox value="participants">参与人</a-checkbox>
          <a-checkbox value="supervisor">监督人</a-checkbox>
        </a-checkbox-group>
      </a-form-item>

      <!-- 通知模板 -->
      <a-form-item label="通知模板">
        <a-textarea
          v-model:value="properties.notifyTemplate"
          :rows="4"
          placeholder="请输入通知模板"
        />
        <div class="expr-help">
          支持的变量：
          <ul>
            <li>${workflow.title} - 流程标题</li>
            <li>${workflow.initiator} - 发起人</li>
            <li>${workflow.startTime} - 开始时间</li>
            <li>${workflow.endTime} - 结束时间</li>
            <li>${workflow.duration} - 流转时长</li>
            <li>${workflow.result} - 流程结果</li>
          </ul>
        </div>
      </a-form-item>

      <!-- 高级配置 -->
      <a-collapse>
        <a-collapse-panel key="advanced" header="高级配置">
          <!-- 归档配置 -->
          <a-form-item label="归档配置">
            <a-checkbox v-model:checked="properties.enableArchive">启用归档</a-checkbox>
            <template v-if="properties.enableArchive">
              <a-select v-model:value="properties.archiveType">
                <a-select-option value="database">数据库归档</a-select-option>
                <a-select-option value="file">文件归档</a-select-option>
              </a-select>
              <template v-if="properties.archiveType === 'file'">
                <a-input
                  v-model:value="properties.archivePath"
                  placeholder="请输入归档路径"
                />
              </template>
            </template>
          </a-form-item>

          <!-- 回调配置 -->
          <a-form-item label="回调配置">
            <a-input
              v-model:value="properties.beforeCallback"
              placeholder="结束前回调"
            />
            <a-input
              v-model:value="properties.afterCallback"
              placeholder="结束后回调"
            />
          </a-form-item>
        </a-collapse-panel>
      </a-collapse>
    </a-form>
  </div>
</template>

<script lang="ts" setup>
import { reactive, watch } from 'vue'

interface Props {
  value: Record<string, any>
}

interface Emits {
  (e: 'update:value', value: Record<string, any>): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

// 属性数据
const properties = reactive<Record<string, any>>({
  endType: 'normal',
  notifyTypes: [],
  notifyTargets: [],
  notifyTemplate: '',
  enableArchive: false,
  archiveType: 'database',
  archivePath: '',
  beforeCallback: '',
  afterCallback: ''
})

// 监听属性变化
watch(
  () => props.value,
  (newValue) => {
    Object.assign(properties, newValue)
  },
  { immediate: true, deep: true }
)

// 监听数据变化
watch(
  properties,
  (newValue) => {
    emit('update:value', { ...newValue })
  },
  { deep: true }
)
</script>

<style lang="less" scoped>
.end-properties {
  .expr-help {
    margin-top: 8px;
    color: #666;
    font-size: 12px;

    ul {
      margin: 4px 0 0 20px;
      padding: 0;
    }
  }

  :deep(.ant-form-item) {
    margin-bottom: 16px;
  }

  :deep(.ant-checkbox-group) {
    display: flex;
    flex-direction: column;
    gap: 8px;
  }
}
</style> 
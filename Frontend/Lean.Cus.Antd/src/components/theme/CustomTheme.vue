# 创建自定义皮肤组件
<template>
  <a-modal
    v-model:visible="visible"
    :title="$t('theme.customTitle')"
    @ok="handleOk"
    @cancel="handleCancel"
    :maskClosable="false"
  >
    <a-form :model="formState" :rules="rules" ref="formRef">
      <a-form-item name="primaryColor" :label="$t('theme.primaryColor')">
        <a-input v-model:value="formState.primaryColor" type="color" />
      </a-form-item>
      <a-form-item name="borderRadius" :label="$t('theme.borderRadius')">
        <a-input-number
          v-model:value="formState.borderRadius"
          :min="0"
          :max="20"
          :step="1"
          style="width: 100%"
        />
      </a-form-item>
      <a-form-item name="fontSize" :label="$t('theme.fontSize')">
        <a-input-number
          v-model:value="formState.fontSize"
          :min="12"
          :max="20"
          :step="1"
          style="width: 100%"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { ref, reactive } from 'vue'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'

const { t } = useI18n()
const visible = ref(false)
const formRef = ref<FormInstance>()

const formState = reactive({
  primaryColor: '#1890ff',
  borderRadius: 6,
  fontSize: 14
})

const rules = {
  primaryColor: [{ required: true, message: t('theme.primaryColorRequired') }],
  borderRadius: [{ required: true, message: t('theme.borderRadiusRequired') }],
  fontSize: [{ required: true, message: t('theme.fontSizeRequired') }]
}

const showCustomThemeModal = () => {
  visible.value = true
}

const handleOk = () => {
  formRef.value?.validate().then(() => {
    // TODO: 保存主题配置
    message.success(t('theme.saveSuccess'))
    visible.value = false
  })
}

const handleCancel = () => {
  visible.value = false
}

defineExpose({
  showCustomThemeModal
})
</script>

<style lang="less" scoped>
:deep(.ant-form-item) {
  margin-bottom: 24px;
}
</style> 
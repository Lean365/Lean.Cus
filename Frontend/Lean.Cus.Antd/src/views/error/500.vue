<script setup lang="ts">
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { CloseCircleOutlined } from '@ant-design/icons-vue'

const router = useRouter()
const { t } = useI18n()

const handleBack = () => {
  router.push('/')
}

const handleRefresh = () => {
  window.location.reload()
}
</script>

<template>
  <div class="error-page">
    <div class="error-content">
      <div class="error-icon">
        <CloseCircleOutlined />
      </div>
      <h1>500</h1>
      <h2>{{ t('error.500.title') }}</h2>
      <p>{{ t('error.500.description') }}</p>
      <div class="error-actions">
        <a-space>
          <a-button type="primary" @click="handleBack">
            {{ t('app.common.back') }}
          </a-button>
          <a-button @click="handleRefresh">
            {{ t('app.common.refresh') }}
          </a-button>
        </a-space>
      </div>
    </div>
  </div>
</template>

<style lang="less" scoped>
.error-page {
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
  background: #f0f2f5;
}

.error-content {
  text-align: center;
  padding: 48px 32px;
  animation: fadeIn 0.5s ease-in-out;

  .error-icon {
    font-size: 84px;
    color: #ff7875;
    margin-bottom: 24px;
    animation: rotate 1s ease-in-out infinite;
  }

  h1 {
    color: #ff7875;
    font-size: 72px;
    font-weight: 600;
    line-height: 1;
    margin: 0 0 16px;
    animation: slideInDown 0.5s ease-in-out;
  }

  h2 {
    color: rgba(0, 0, 0, 0.85);
    font-size: 24px;
    line-height: 1.4;
    margin: 0 0 16px;
    font-weight: 500;
    animation: slideInUp 0.5s ease-in-out;
  }

  p {
    color: rgba(0, 0, 0, 0.45);
    font-size: 14px;
    line-height: 1.6;
    margin: 0 0 24px;
    animation: fadeIn 0.5s ease-in-out;
  }

  .error-actions {
    animation: slideInUp 0.5s ease-in-out;
  }
}

@keyframes fadeIn {
  from {
    opacity: 0;
  }
  to {
    opacity: 1;
  }
}

@keyframes slideInDown {
  from {
    transform: translateY(-20px);
    opacity: 0;
  }
  to {
    transform: translateY(0);
    opacity: 1;
  }
}

@keyframes slideInUp {
  from {
    transform: translateY(20px);
    opacity: 0;
  }
  to {
    transform: translateY(0);
    opacity: 1;
  }
}

@keyframes rotate {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}
</style> 
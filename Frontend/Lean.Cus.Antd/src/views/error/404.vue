<script setup lang="ts">
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'

const router = useRouter()
const { t } = useI18n()

const handleBack = () => {
  router.push('/')
}
</script>

<template>
  <div class="error-page">
    <div class="error-content">
      <div class="error-image">
        <img src="@/assets/images/404.svg" alt="404" />
      </div>
      <h1>404</h1>
      <h2>{{ t('error.404.title') }}</h2>
      <p>{{ t('error.404.description') }}</p>
      <a-button type="primary" @click="handleBack">
        {{ t('app.common.back') }}
      </a-button>
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

  .error-image {
    margin-bottom: 24px;
    
    img {
      max-width: 400px;
      width: 100%;
      height: auto;
    }
  }

  h1 {
    color: #1890ff;
    font-size: 72px;
    font-weight: 600;
    line-height: 1;
    margin: 0 0 16px;
  }

  h2 {
    color: rgba(0, 0, 0, 0.85);
    font-size: 24px;
    line-height: 1.4;
    margin: 0 0 16px;
    font-weight: 500;
  }

  p {
    color: rgba(0, 0, 0, 0.45);
    font-size: 14px;
    line-height: 1.6;
    margin: 0 0 24px;
  }
}
</style> 
<script setup lang="ts">
import { ref, reactive } from 'vue'
import { useRouter } from 'vue-router'
import { useI18n } from 'vue-i18n'
import { message } from 'ant-design-vue'
import type { FormInstance } from 'ant-design-vue'
import { useUserStore } from '@/stores/user'

const { t } = useI18n()
const router = useRouter()
const userStore = useUserStore()

// 登录表单
const formRef = ref<FormInstance>()
const formState = reactive({
  username: '',
  password: '',
  remember: true
})

// 加载状态
const loading = ref(false)

// 表单校验规则
const rules = {
  username: [
    { required: true, message: t('app.common.required'), trigger: 'blur' },
    { min: 3, max: 20, message: t('login.username.length'), trigger: 'blur' }
  ],
  password: [
    { required: true, message: t('app.common.required'), trigger: 'blur' },
    { min: 6, max: 20, message: t('login.password.length'), trigger: 'blur' }
  ]
}

// 处理登录
const handleSubmit = async () => {
  try {
    await formRef.value?.validate()
    loading.value = true
    
    // 调用登录接口
    const loginParams = {
      username: formState.username,
      password: formState.password,
      remember: formState.remember
    }
    
    // TODO: 实现登录逻辑
    // await userStore.login(loginParams)
    
    message.success(t('login.success'))
    // 登录成功后跳转到首页
    router.push('/')
  } catch (error) {
    console.error('Login failed:', error)
  } finally {
    loading.value = false
  }
}

// 处理忘记密码
const handleForgotPassword = () => {
  message.info(t('login.forgot.tip'))
  // TODO: 实现忘记密码逻辑
  // router.push('/forgot-password')
}
</script>

<template>
  <div class="login-container">
    <div class="login-content">
      <div class="login-header">
        <h1>{{ t('app.name') }}</h1>
        <p>{{ t('app.description') }}</p>
      </div>
      
      <a-form
        ref="formRef"
        :model="formState"
        :rules="rules"
        class="login-form"
        @finish="handleSubmit"
      >
        <a-form-item name="username">
          <a-input
            v-model:value="formState.username"
            :placeholder="t('login.username.placeholder')"
            size="large"
          >
            <template #prefix>
              <UserOutlined />
            </template>
          </a-input>
        </a-form-item>
        
        <a-form-item name="password">
          <a-input-password
            v-model:value="formState.password"
            :placeholder="t('login.password.placeholder')"
            size="large"
          >
            <template #prefix>
              <LockOutlined />
            </template>
          </a-input-password>
        </a-form-item>
        
        <a-form-item>
          <a-row>
            <a-col :span="12">
              <a-checkbox
                v-model:checked="formState.remember"
                class="remember-me"
              >
                {{ t('login.remember') }}
              </a-checkbox>
            </a-col>
            <a-col :span="12" class="text-right">
              <a class="forgot-password" @click="handleForgotPassword">
                {{ t('login.forgot') }}
              </a>
            </a-col>
          </a-row>
        </a-form-item>
        
        <a-form-item>
          <a-button
            type="primary"
            html-type="submit"
            size="large"
            :loading="loading"
            block
          >
            {{ t('login.submit') }}
          </a-button>
        </a-form-item>
      </a-form>
    </div>
  </div>
</template>

<style lang="less" scoped>
.login-container {
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
  background: #f0f2f5;
  background-image: url('@/assets/images/login-bg.svg');
  background-repeat: no-repeat;
  background-position: center;
  background-size: 100%;
}

.login-content {
  width: 368px;
  margin: 0 auto;
  padding: 32px;
  background: #fff;
  border-radius: 4px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.15);
}

.login-header {
  text-align: center;
  margin-bottom: 40px;

  h1 {
    margin-bottom: 12px;
    color: rgba(0, 0, 0, 0.85);
    font-weight: 600;
    font-size: 24px;
  }

  p {
    margin-bottom: 0;
    color: rgba(0, 0, 0, 0.45);
    font-size: 14px;
  }
}

.login-form {
  .remember-me {
    color: rgba(0, 0, 0, 0.65);
  }

  .forgot-password {
    float: right;
    color: @primary-color;
  }

  .text-right {
    text-align: right;
  }
}
</style> 
<!------------------------------------------------------------------
 * Copyright (C) 2024 Lean.Cus 版权所有。
 * 
 * 文件名：login.vue
 * 文件功能描述：登录页面
 *
 * 创建标识：CodeGenerator 2024-01-09
 * 
 * 修改标识：
 * 修改描述：使用 Ant Design Vue 的变量实现暗黑主题
 ------------------------------------------------------------------>

<template>
  <div class="login-container">
    <div class="settings-bar">
      <theme-switch />
      <lang-switch />
    </div>
    <a-card :bordered="false" class="login-card">
      <template #title>
        <div class="login-title">{{ $t('login.title') }}</div>
      </template>
      <a-form
        ref="formRef"
        :model="formState"
        name="login"
        autocomplete="off"
        @finish="handleSubmit"
      >
        <a-form-item
          name="userName"
          :rules="[
            { required: true, message: $t('login.usernameRequired') },
            { whitespace: true, message: $t('login.usernameRequired') }
          ]"
        >
          <a-input
            v-model:value="formState.userName"
            :placeholder="$t('login.username')"
            size="large"
          >
            <template #prefix>
              <UserOutlined />
            </template>
          </a-input>
        </a-form-item>
  
        <a-form-item
          name="password"
          :rules="[
            { required: true, message: $t('login.passwordRequired') },
            { whitespace: true, message: $t('login.passwordRequired') }
          ]"
        >
          <a-input-password
            v-model:value="formState.password"
            :placeholder="$t('login.password')"
            size="large"
            autocomplete="current-password"
          >
            <template #prefix>
              <LockOutlined />
            </template>
          </a-input-password>
        </a-form-item>
  
        <a-form-item>
          <a-button
            type="primary"
            size="large"
            :loading="loading"
            block
            html-type="submit"
          >
            {{ $t('login.submit') }}
          </a-button>
        </a-form-item>
      </a-form>
    </a-card>
  
    <!-- 滑块验证码 -->
    <a-modal
      v-model:open="sliderVisible"
      :title="$t('login.captcha.title')"
      :footer="null"
      :maskClosable="false"
      :closable="false"
      width="360px"
      :centered="true"
      class="captcha-modal"
    >
      <slider-captcha
        v-if="sliderVisible"
        v-model:visible="sliderVisible"
        @success="handleCaptchaSuccess"
        @fail="handleCaptchaFail"
        @generate="handleCaptchaGenerate"
      />
    </a-modal>
  </div>
</template>

<script setup lang="ts">
import { ref, inject, Ref } from 'vue'
import { useRouter } from 'vue-router'
import { message, theme } from 'ant-design-vue'
import { UserOutlined, LockOutlined } from '@ant-design/icons-vue'
import { useI18n } from 'vue-i18n'
import type { FormInstance } from 'ant-design-vue'
import type { LeanLoginDto } from '@/types/auth'
import type { LeanCaptchaGenerateDto } from '@/types/auth'
import { login, generateCaptcha, verifyCaptcha } from '@/api/auth'
import { validateCredentials } from '@/api/auth/index'
import { useUserStore } from '@/stores/user'
import SliderCaptcha from '@/components/Captcha/index.vue'
import ThemeSwitch from '@/components/theme/index.vue'
import LangSwitch from '@/components/lang/index.vue'

const router = useRouter()
const userStore = useUserStore()
const { t } = useI18n()
const formRef = ref<FormInstance>()
const loading = ref(false)
const sliderVisible = ref(false)
const uuid = ref('')
const isDark = inject<Ref<boolean>>('isDark')!
const { token } = theme.useToken()

// 表单数据
const formState = ref<LeanLoginDto>({
  userName: '',
  password: '',
  code: '',
  uuid: '',
  deviceId: '',
  deviceName: '',
  deviceType: '',
  os: '',
  browser: '',
  ipAddress: '',
  location: '',
  timestamp: 0
})

const captchaInfo = ref<LeanCaptchaGenerateDto>({
  uuid: '',
  backgroundImage: '',
  sliderImage: '',
  y: 0
})

const moveX = ref(0)
const moveY = ref(0)

// 表单提交
const handleSubmit = async (values: any) => {
  try {
    loading.value = true;
    console.log('提交的表单数据:', values);
    
    // 构造登录数据
    const loginData: LeanLoginDto = {
      userName: values.userName,
      password: values.password,
      code: '',
      uuid: '',
      deviceId: 'web',
      deviceName: 'Web Browser',
      deviceType: 'web',
      os: navigator.platform.substring(0, 50),
      browser: navigator.userAgent.split(' ')[0].substring(0, 50),
      ipAddress: '',  // 由后端获取
      location: '',   // 由后端获取
      timestamp: Date.now()
    };
    
    // 等待2秒，确保CSRF token设置完成
    await new Promise(resolve => setTimeout(resolve, 2000));
    
    // 先验证用户名和密码
    const validateResult = await validateCredentials(loginData);
    
    console.log('验证结果:', validateResult);
    
    if (!validateResult.data) {
      message.error(t('login.invalidCredentials'));
      loading.value = false;
      return;
    }
    
    // 验证通过后显示验证码
    sliderVisible.value = true;
    loading.value = false;
  } catch (error: any) {
    loading.value = false;
  
    console.error('验证失败:', error);
    console.error('错误详情:', {
      message: error.message,
      response: error.response?.data,
      status: error.response?.status,
      headers: error.response?.headers,
      config: error.config
    });
    message.error(t('login.validateFailed'));
  }
};

// 处理验证码成功
const handleCaptchaSuccess = async () => {
  try {
    // 发送登录请求
    const res = await login(formState.value);
    if (res.success && res.data) {
      message.success(t('login.loginSuccess'));
      // 使用store的login方法处理登录结果
      await userStore.login(res.data);
      // 跳转到首页
      await router.push('/');
    } else {
      message.error(res.message || t('login.loginFailed'));
    }
  } catch (error: any) {
    console.log('登录失败:', error);
    console.log('错误详情:', {
      message: error.message,
      response: error.response?.data,
      status: error.response?.status,
      headers: error.response?.headers
    });
    message.error(t('login.loginFailed'));
  }
};

// 处理验证码失败
const handleCaptchaFail = () => {
  formState.value.code = ''
  formState.value.uuid = ''
}

// 处理验证码生成
const handleCaptchaGenerate = (data: any) => {
  uuid.value = data.uuid
}
</script>

<style lang="less" scoped>
.login-container {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
  position: relative;
  background-color: v-bind('token.colorBgLayout');
}

.settings-bar {
  position: absolute;
  top: 24px;
  right: 24px;
  display: flex;
  gap: 8px;
}

.login-card {
  width: 360px;
  margin: 0 auto;
  background-color: v-bind('token.colorBgContainer');
  border-radius: 16px;
  box-shadow: 0 6px 16px rgba(0, 0, 0, 0.08);
  border: 1px solid v-bind('token.colorBorderSecondary');
  transition: all 0.3s ease;
  
  &:hover {
    box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);
  }
  
  :deep(.ant-card-head) {
    background-color: v-bind('token.colorBgContainer');
    border-bottom: 1px solid v-bind('token.colorBorderSecondary');
    border-radius: 16px 16px 0 0;
  }
  
  :deep(.ant-card-head-title) {
    text-align: center;
    padding: 24px 0;
    color: v-bind('token.colorText');
    font-size: 20px;
    font-weight: 500;
  }

  :deep(.ant-card-body) {
    background-color: v-bind('token.colorBgContainer');
    border-radius: 0 0 16px 16px;
    padding: 24px;
  }

  :deep(.ant-input-affix-wrapper) {
    border-radius: 8px;
    transition: all 0.3s ease;
    
    &:hover, &:focus {
      border-color: v-bind('token.colorPrimary');
    }
  }

  :deep(.ant-btn) {
    border-radius: 8px;
    height: 40px;
    font-size: 16px;
  }
}

.captcha-modal {
  :deep(.ant-modal-content) {
    padding: 0;
    background-color: v-bind('token.colorBgContainer');
  }
}
</style> 
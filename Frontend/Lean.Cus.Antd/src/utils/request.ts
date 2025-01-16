import axios from 'axios'
import type { LeanApiResult } from '@/types/auth'
import { message } from 'ant-design-vue'

// 创建axios实例
const request = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 10000,
  withCredentials: true // 允许跨域携带cookie
})

// 请求拦截器
request.interceptors.request.use(
  (config) => {
    // 从cookie中获取CSRF token
    const cookies = document.cookie.split(';');
    console.log('所有cookies:', cookies);
    
    const csrfCookie = cookies.find(cookie => cookie.trim().startsWith('XSRF-TOKEN='));
    console.log('找到的CSRF cookie:', csrfCookie);
    
    if (csrfCookie) {
      const csrfToken = csrfCookie.split('=')[1];
      if (csrfToken) {
        const decodedToken = decodeURIComponent(csrfToken);
        config.headers['X-XSRF-TOKEN'] = decodedToken;
        console.log('设置CSRF Token:', decodedToken);
      } else {
        console.warn('CSRF Token 值为空');
      }
    } else {
      console.warn('未找到CSRF Token cookie');
    }
    
    console.log('完整请求配置:', {
      url: config.url,
      method: config.method,
      data: config.data,
      headers: config.headers
    });
    return config;
  },
  (error) => {
    console.error('请求错误:', error);
    return Promise.reject(error);
  }
);

// 响应拦截器
request.interceptors.response.use(
  (response) => {
    console.log('响应成功:', {
      url: response.config.url,
      status: response.status,
      data: response.data,
      headers: response.headers
    });
    
    const res = response.data;
    if (!res.success) {
      // 对于验证码验证失败的情况，不显示错误消息
      if (!response.config.url?.includes('/api/captcha/verify') || res.message !== '验证失败') {
        message.error(res.message || '请求失败');
      }
      return Promise.reject(new Error(res.message || '请求失败'));
    }
    return response.data;
  },
  (error) => {
    console.error('响应错误:', {
      url: error.config?.url,
      status: error.response?.status,
      data: error.response?.data,
      headers: error.response?.headers,
      message: error.message
    });
    
    message.error(error.response?.data?.message || error.message || '请求失败');
    return Promise.reject(error);
  }
);

export { request }
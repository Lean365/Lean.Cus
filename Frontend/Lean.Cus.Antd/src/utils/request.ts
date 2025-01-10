import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios';
import { message } from 'ant-design-vue';
import { CryptoService } from './crypto';

// 创建 axios 实例
const service: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || '/api',
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json',
  },
});

// 请求拦截器
service.interceptors.request.use(
  (config) => {
    // 从 localStorage 获取 token
    const token = localStorage.getItem('token');
    if (token) {
      config.headers['Authorization'] = `Bearer ${token}`;
    }

    // 加密请求数据
    if (config.data && config.headers['Content-Type'] === 'application/json') {
      config.data = CryptoService.encrypt(JSON.stringify(config.data));
    }

    return config;
  },
  (error) => {
    console.error('请求错误:', error);
    return Promise.reject(error);
  }
);

// 响应拦截器
service.interceptors.response.use(
  (response: AxiosResponse) => {
    const res = response.data;

    // 如果响应数据是加密的，先解密
    if (response.headers['content-type']?.includes('application/json+encrypted')) {
      try {
        const decryptedData = CryptoService.decrypt(res);
        return JSON.parse(decryptedData);
      } catch (error) {
        console.error('解密响应数据失败:', error);
        message.error('数据解密失败');
        return Promise.reject(error);
      }
    }

    // 处理业务状态码
    if (res.code !== 0) {
      message.error(res.message || '请求失败');
      
      // 处理特定的错误码
      if (res.code === 401) {
        // 未授权，清除用户信息并跳转到登录页
        localStorage.removeItem('token');
        window.location.href = '/login';
      }
      return Promise.reject(new Error(res.message || '请求失败'));
    }

    return res.data;
  },
  (error) => {
    console.error('响应错误:', error);
    
    // 处理网络错误
    if (!error.response) {
      message.error('网络错误，请检查您的网络连接');
      return Promise.reject(error);
    }

    // 处理 HTTP 状态码
    const status = error.response.status;
    switch (status) {
      case 400:
        message.error('请求参数错误');
        break;
      case 401:
        message.error('未授权，请重新登录');
        localStorage.removeItem('token');
        window.location.href = '/login';
        break;
      case 403:
        message.error('拒绝访问');
        break;
      case 404:
        message.error('请求的资源不存在');
        break;
      case 500:
        message.error('服务器错误');
        break;
      default:
        message.error(`请求失败: ${status}`);
    }

    return Promise.reject(error);
  }
);

// 封装 GET 请求
export function get<T = any>(url: string, params?: any, config?: AxiosRequestConfig): Promise<T> {
  return service.get(url, { params, ...config });
}

// 封装 POST 请求
export function post<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
  return service.post(url, data, config);
}

// 封装 PUT 请求
export function put<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
  return service.put(url, data, config);
}

// 封装 DELETE 请求
export function del<T = any>(url: string, config?: AxiosRequestConfig): Promise<T> {
  return service.delete(url, config);
}

// 封装上传文件请求
export function upload<T = any>(url: string, file: File, config?: AxiosRequestConfig): Promise<T> {
  const formData = new FormData();
  formData.append('file', file);
  return service.post(url, formData, {
    headers: {
      'Content-Type': 'multipart/form-data',
    },
    ...config,
  });
}

// 封装下载文件请求
export function download(url: string, params?: any, config?: AxiosRequestConfig): Promise<Blob> {
  return service.get(url, {
    params,
    responseType: 'blob',
    ...config,
  });
}

export default service; 
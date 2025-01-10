import axios from 'axios'
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, AxiosError } from 'axios'
import { message } from 'ant-design-vue'
import { useUserStore } from '@/stores/user'

interface ApiResponse<T = any> {
  code: number
  msg: string
  data: T
}

// 创建 axios 实例
const service: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_URL as string,
  timeout: 10000,
  headers: {
    'Content-Type': 'application/json;charset=utf-8'
  }
})

// 请求拦截器
service.interceptors.request.use(
  (config: AxiosRequestConfig) => {
    const userStore = useUserStore()
    const token = userStore.token
    if (token) {
      config.headers = {
        ...config.headers,
        Authorization: `Bearer ${token}`
      }
    }
    return config
  },
  (error: AxiosError) => {
    return Promise.reject(error)
  }
)

// 响应拦截器
service.interceptors.response.use(
  (response: AxiosResponse<ApiResponse>) => {
    const { code, msg, data } = response.data
    if (code === 200) {
      return data
    } else {
      message.error(msg || '操作失败')
      return Promise.reject(new Error(msg || '操作失败'))
    }
  },
  (error: AxiosError) => {
    const { response } = error
    let msg = '请求失败'
    if (response && response.data) {
      msg = (response.data as ApiResponse).msg
    }
    message.error(msg)
    return Promise.reject(error)
  }
)

// 封装 GET 请求
export function get<T>(url: string, params?: Record<string, any>): Promise<T> {
  return service.get(url, { params })
}

// 封装 POST 请求
export function post<T>(url: string, data?: Record<string, any>): Promise<T> {
  return service.post(url, data)
}

// 封装 PUT 请求
export function put<T>(url: string, data?: Record<string, any>): Promise<T> {
  return service.put(url, data)
}

// 封装 DELETE 请求
export function del<T>(url: string): Promise<T> {
  return service.delete(url)
}

// 导出 axios 实例
export default service 
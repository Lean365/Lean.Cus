import axios, { AxiosInstance, AxiosRequestConfig, AxiosResponse } from 'axios'
import { message } from 'ant-design-vue'

/**
 * HTTP客户端
 */
class HttpClient {
  private instance: AxiosInstance

  constructor() {
    this.instance = axios.create({
      baseURL: import.meta.env.VITE_API_URL,
      timeout: 10000,
      headers: {
        'Content-Type': 'application/json'
      }
    })

    // 请求拦截器
    this.instance.interceptors.request.use(
      (config) => {
        // 添加token
        const token = localStorage.getItem('token')
        if (token) {
          config.headers.Authorization = `Bearer ${token}`
        }
        return config
      },
      (error) => {
        return Promise.reject(error)
      }
    )

    // 响应拦截器
    this.instance.interceptors.response.use(
      (response) => {
        return response.data
      },
      (error) => {
        // 处理错误
        if (error.response) {
          const { status, data } = error.response
          switch (status) {
            case 400:
              message.error(data.message || '请求参数错误')
              break
            case 401:
              message.error('未授权，请重新登录')
              // 清除token
              localStorage.removeItem('token')
              // 跳转到登录页
              window.location.href = '/login'
              break
            case 403:
              message.error('拒绝访问')
              break
            case 404:
              message.error('请求的资源不存在')
              break
            case 500:
              message.error('服务器错误')
              break
            default:
              message.error('网络错误')
          }
        } else {
          message.error('网络错误')
        }
        return Promise.reject(error)
      }
    )
  }

  /**
   * GET请求
   * @param url 请求地址
   * @param config 请求配置
   * @returns 响应结果
   */
  get<T = any>(url: string, config?: AxiosRequestConfig): Promise<T> {
    return this.instance.get(url, config)
  }

  /**
   * POST请求
   * @param url 请求地址
   * @param data 请求数据
   * @param config 请求配置
   * @returns 响应结果
   */
  post<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
    return this.instance.post(url, data, config)
  }

  /**
   * PUT请求
   * @param url 请求地址
   * @param data 请求数据
   * @param config 请求配置
   * @returns 响应结果
   */
  put<T = any>(url: string, data?: any, config?: AxiosRequestConfig): Promise<T> {
    return this.instance.put(url, data, config)
  }

  /**
   * DELETE请求
   * @param url 请求地址
   * @param config 请求配置
   * @returns 响应结果
   */
  delete<T = any>(url: string, config?: AxiosRequestConfig): Promise<T> {
    return this.instance.delete(url, config)
  }
}

export const http = new HttpClient() 
import { request } from '@/utils/request'
import type { LeanLoginDto, LeanLoginResultDto } from '@/types/auth'

// 登录
export const login = (data: LeanLoginDto) => {
  return request.post<LeanLoginResultDto>('/api/auth/login', data)
}

// 生成验证码
export const generateCaptcha = () => {
  return request.get('/api/captcha/generate')
}

// 验证验证码
export const verifyCaptcha = (data: { uuid: string; x: number; y: number }) => {
  return request.post('/api/captcha/verify', data)
} 
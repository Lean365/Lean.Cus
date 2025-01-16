import type { LeanLoginDto, LeanCaptchaVerifyDto, LeanLoginResponse, LeanCaptchaGenerateResponse, LeanCaptchaVerifyResponse, LeanApiResult, LeanLoginResultDto } from '@/types/auth'
import { request } from '@/utils/request'

/**
 * 验证用户名密码
 * @param data 登录信息
 */
export function validateCredentials(data: Pick<LeanLoginDto, 'userName' | 'password'>) {
  console.log('验证凭证请求数据:', data);
  return request<LeanApiResult<boolean>>({
    url: '/api/auth/validate',
    method: 'post',
    data
  });
}

/**
 * 登录
 * @param data 登录参数
 */
export function login(data: LeanLoginDto): Promise<LeanLoginResponse> {
  return request({
    url: '/api/auth/login',
    method: 'post',
    data
  })
}

/**
 * 退出登录
 */
export function logout(): Promise<LeanApiResult<void>> {
  return request.post('/api/auth/logout')
}

/**
 * 刷新令牌
 * @param refreshToken 刷新令牌
 */
export function refreshToken(refreshToken: string): Promise<LeanLoginResponse> {
  return request.post('/api/auth/refresh-token', { refreshToken })
}

/**
 * 生成验证码
 */
export function generateCaptcha(): Promise<LeanCaptchaGenerateResponse> {
  return request.get('/api/captcha/generate')
}

/**
 * 验证验证码
 * @param data 验证参数
 */
export function verifyCaptcha(data: LeanCaptchaVerifyDto): Promise<LeanCaptchaVerifyResponse> {
  return request.post('/api/captcha/verify', data)
} 
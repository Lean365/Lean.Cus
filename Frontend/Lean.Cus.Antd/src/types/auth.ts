/**
 * API 响应结果
 */
export interface LeanApiResult<T> {
  /** 是否成功 */
  success: boolean
  /** 状态码 */
  code: number
  /** 消息 */
  message: string
  /** 数据 */
  data?: T
}

/**
 * 登录请求参数
 */
export interface LeanLoginDto {
  /** 用户名 */
  userName: string
  /** 密码 */
  password: string
  /** 验证码 */
  code: string
  /** 验证码唯一标识 */
  uuid: string
  /** 设备ID */
  deviceId: string
  /** 设备名称 */
  deviceName: string
  /** 设备类型 */
  deviceType: string
  /** 操作系统 */
  os: string
  /** 浏览器 */
  browser: string
  /** IP地址 */
  ipAddress: string
  /** 位置 */
  location: string
  /** 时间戳 */
  timestamp: number
}

/**
 * 登录响应结果
 */
export interface LeanLoginResultDto {
  /** 访问令牌 */
  accessToken: string
  /** 刷新令牌 */
  refreshToken: string
  /** 过期时间 */
  expiresIn: number
  /** 用户ID */
  userId: bigint
  /** 用户名 */
  userName: string
  /** 真实姓名 */
  realName: string
  /** 头像 */
  avatar: string
  /** 权限列表 */
  permissions: string[]
  /** 角色列表 */
  roles: string[]
}

/**
 * 验证码生成响应
 */
export interface LeanCaptchaGenerateDto {
  /** 唯一标识 */
  uuid: string
  /** 背景图片Base64 */
  backgroundImage: string
  /** 滑块图片Base64 */
  sliderImage: string
  /** y轴坐标 */
  y: number
}

/**
 * 验证码校验请求
 */
export interface LeanCaptchaVerifyDto {
  /** 唯一标识 */
  uuid: string
  /** x轴坐标 */
  x: number
  /** y轴坐标 */
  y: number
}

/**
 * 验证码校验结果
 */
export interface LeanCaptchaVerifyResultDto {
  /** 是否验证通过 */
  success: boolean
  /** 错误信息 */
  message: string
}

// API 响应类型
export type LeanLoginResponse = LeanApiResult<LeanLoginResultDto>
export type LeanCaptchaGenerateResponse = LeanApiResult<LeanCaptchaGenerateDto>
export type LeanCaptchaVerifyResponse = LeanApiResult<LeanCaptchaVerifyResultDto> 
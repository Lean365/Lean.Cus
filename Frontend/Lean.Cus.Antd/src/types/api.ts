// API响应结果类型
export interface LeanApiResult<T = any> {
  success: boolean;
  code: number;
  message: string | null;
  data?: T;
} 
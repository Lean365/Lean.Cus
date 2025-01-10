/**
 * 菜单配置
 */
export interface MenuConfig {
  /**
   * 菜单键值
   */
  key: string
  /**
   * 菜单标题
   */
  title: string
  /**
   * 菜单图标
   */
  icon?: string
  /**
   * 菜单路径
   */
  path?: string
  /**
   * 子菜单
   */
  children?: MenuConfig[]
} 
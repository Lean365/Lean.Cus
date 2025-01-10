/**
 * 模板类型
 */
export const TEMPLATE_TYPES = {
  /**
   * 实体
   */
  ENTITY: 'Entity',
  /**
   * 服务接口
   */
  SERVICE_INTERFACE: 'ServiceInterface',
  /**
   * 服务实现
   */
  SERVICE_IMPL: 'ServiceImpl',
  /**
   * 控制器
   */
  CONTROLLER: 'Controller',
  /**
   * 前端API
   */
  API: 'Api',
  /**
   * 前端页面
   */
  PAGE: 'Page'
}

/**
 * 生成类型
 */
export const GENERATE_TYPES = {
  /**
   * 单表
   */
  SINGLE: 'Single',
  /**
   * 主子表
   */
  MASTER_DETAIL: 'MasterDetail',
  /**
   * 树表
   */
  TREE: 'Tree'
}

/**
 * 生成功能
 */
export const GENERATE_FUNCTIONS = {
  /**
   * 新增
   */
  CREATE: 'Create',
  /**
   * 删除
   */
  DELETE: 'Delete',
  /**
   * 修改
   */
  UPDATE: 'Update',
  /**
   * 查询
   */
  QUERY: 'Query',
  /**
   * 导入
   */
  IMPORT: 'Import',
  /**
   * 导出
   */
  EXPORT: 'Export'
}

/**
 * 查询类型
 */
export const QUERY_TYPES = {
  /**
   * 等于
   */
  EQ: 'EQ',
  /**
   * 不等于
   */
  NE: 'NE',
  /**
   * 大于
   */
  GT: 'GT',
  /**
   * 大于等于
   */
  GE: 'GE',
  /**
   * 小于
   */
  LT: 'LT',
  /**
   * 小于等于
   */
  LE: 'LE',
  /**
   * 包含
   */
  LIKE: 'LIKE',
  /**
   * 不包含
   */
  NOT_LIKE: 'NOT_LIKE',
  /**
   * 为空
   */
  IS_NULL: 'IS_NULL',
  /**
   * 不为空
   */
  IS_NOT_NULL: 'IS_NOT_NULL',
  /**
   * 在范围内
   */
  IN: 'IN',
  /**
   * 不在范围内
   */
  NOT_IN: 'NOT_IN',
  /**
   * 在日期范围内
   */
  BETWEEN: 'BETWEEN'
}

/**
 * 显示类型
 */
export const DISPLAY_TYPES = {
  /**
   * 文本框
   */
  INPUT: 'Input',
  /**
   * 文本域
   */
  TEXTAREA: 'Textarea',
  /**
   * 下拉框
   */
  SELECT: 'Select',
  /**
   * 单选框
   */
  RADIO: 'Radio',
  /**
   * 复选框
   */
  CHECKBOX: 'Checkbox',
  /**
   * 日期选择器
   */
  DATE: 'Date',
  /**
   * 时间选择器
   */
  TIME: 'Time',
  /**
   * 日期时间选择器
   */
  DATETIME: 'DateTime',
  /**
   * 图片上传
   */
  IMAGE: 'Image',
  /**
   * 文件上传
   */
  FILE: 'File',
  /**
   * 富文本编辑器
   */
  EDITOR: 'Editor'
} 
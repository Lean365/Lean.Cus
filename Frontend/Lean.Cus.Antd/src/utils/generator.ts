/**
 * 获取C#类型对应的TypeScript类型
 * @param csharpType C#类型
 * @returns TypeScript类型
 */
export function getCSharpToTypeScriptType(csharpType: string): string {
  switch (csharpType) {
    case 'string':
      return 'string'
    case 'int':
    case 'long':
    case 'float':
    case 'double':
    case 'decimal':
      return 'number'
    case 'bool':
      return 'boolean'
    case 'DateTime':
      return 'Date'
    default:
      return 'any'
  }
}

/**
 * 获取C#类型对应的SQL类型
 * @param csharpType C#类型
 * @returns SQL类型
 */
export function getCSharpToSqlType(csharpType: string): string {
  switch (csharpType) {
    case 'string':
      return 'nvarchar'
    case 'int':
      return 'int'
    case 'long':
      return 'bigint'
    case 'float':
      return 'float'
    case 'double':
      return 'float'
    case 'decimal':
      return 'decimal'
    case 'bool':
      return 'bit'
    case 'DateTime':
      return 'datetime'
    default:
      return 'nvarchar'
  }
}

/**
 * 获取SQL类型对应的C#类型
 * @param sqlType SQL类型
 * @returns C#类型
 */
export function getSqlToCSharpType(sqlType: string): string {
  switch (sqlType.toLowerCase()) {
    case 'nvarchar':
    case 'varchar':
    case 'char':
    case 'text':
    case 'ntext':
      return 'string'
    case 'int':
      return 'int'
    case 'bigint':
      return 'long'
    case 'float':
      return 'float'
    case 'decimal':
      return 'decimal'
    case 'bit':
      return 'bool'
    case 'datetime':
    case 'date':
      return 'DateTime'
    default:
      return 'string'
  }
}

/**
 * 获取SQL类型对应的默认长度
 * @param sqlType SQL类型
 * @returns 默认长度
 */
export function getSqlTypeDefaultLength(sqlType: string): number {
  switch (sqlType.toLowerCase()) {
    case 'nvarchar':
    case 'varchar':
      return 50
    case 'char':
      return 10
    case 'decimal':
      return 18
    default:
      return 0
  }
}

/**
 * 获取SQL类型对应的默认精度
 * @param sqlType SQL类型
 * @returns 默认精度
 */
export function getSqlTypeDefaultPrecision(sqlType: string): number {
  switch (sqlType.toLowerCase()) {
    case 'decimal':
      return 2
    default:
      return 0
  }
}

/**
 * 获取C#字段名
 * @param columnName 列名
 * @returns C#字段名
 */
export function getCSharpFieldName(columnName: string): string {
  // 将下划线分隔的字段名转换为驼峰命名
  const words = columnName.split('_')
  return words
    .map((word) => word.charAt(0).toUpperCase() + word.slice(1).toLowerCase())
    .join('')
}

/**
 * 获取TypeScript字段名
 * @param columnName 列名
 * @returns TypeScript字段名
 */
export function getTypeScriptFieldName(columnName: string): string {
  // 将下划线分隔的字段名转换为驼峰命名
  const words = columnName.split('_')
  return words
    .map((word, index) => {
      if (index === 0) {
        return word.toLowerCase()
      }
      return word.charAt(0).toUpperCase() + word.slice(1).toLowerCase()
    })
    .join('')
}

/**
 * 获取数据库字段名
 * @param columnName 列名
 * @returns 数据库字段名
 */
export function getDbColumnName(columnName: string): string {
  // 将驼峰命名转换为下划线分隔的字段名
  return columnName
    .replace(/([A-Z])/g, '_$1')
    .toLowerCase()
    .replace(/^_/, '')
} 
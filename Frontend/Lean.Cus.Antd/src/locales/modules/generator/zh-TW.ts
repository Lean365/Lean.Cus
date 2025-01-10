export default {
  title: '程式碼生成',
  template: {
    title: '範本管理',
    add: '新增範本',
    edit: '編輯範本',
    delete: '刪除範本',
    name: '範本名稱',
    type: '範本類型',
    content: '範本內容',
    description: '描述',
    types: {
      entity: '實體',
      service: '服務',
      controller: '控制器'
    }
  },
  designer: {
    title: '資料表設計',
    table: {
      name: '資料表名稱',
      comment: '註解',
      columns: '欄位'
    },
    column: {
      name: '欄位名稱',
      type: '資料類型',
      length: '長度',
      nullable: '允許空值',
      primary: '主鍵',
      comment: '註解'
    }
  },
  generate: {
    title: '生成程式碼',
    preview: '預覽',
    download: '下載',
    success: '生成成功',
    failed: '生成失敗'
  }
} 
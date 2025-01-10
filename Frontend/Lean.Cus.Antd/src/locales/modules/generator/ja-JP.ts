export default {
  title: 'コード生成',
  template: {
    title: 'テンプレート管理',
    add: 'テンプレート追加',
    edit: 'テンプレート編集',
    delete: 'テンプレート削除',
    name: 'テンプレート名',
    type: 'テンプレート種類',
    content: 'テンプレート内容',
    description: '説明',
    types: {
      entity: 'エンティティ',
      service: 'サービス',
      controller: 'コントローラー'
    }
  },
  designer: {
    title: 'テーブル設計',
    table: {
      name: 'テーブル名',
      comment: 'コメント',
      columns: 'カラム'
    },
    column: {
      name: 'カラム名',
      type: 'データ型',
      length: '長さ',
      nullable: 'NULL許可',
      primary: '主キー',
      comment: 'コメント'
    }
  },
  generate: {
    title: 'コード生成',
    preview: 'プレビュー',
    download: 'ダウンロード',
    success: '生成成功',
    failed: '生成失敗'
  }
} 
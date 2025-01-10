export default {
  user: {
    title: 'ユーザー管理',
    username: 'ユーザー名',
    password: 'パスワード',
    name: '氏名',
    email: 'メールアドレス',
    phone: '電話番号',
    role: '役割',
    status: 'ステータス',
    createTime: '作成日時',
    updateTime: '更新日時',
    lastLoginTime: '最終ログイン',
    action: {
      add: 'ユーザー追加',
      edit: 'ユーザー編集',
      delete: 'ユーザー削除',
      resetPassword: 'パスワードリセット'
    }
  },
  role: {
    title: '役割管理',
    name: '役割名',
    code: '役割コード',
    description: '説明',
    permissions: '権限',
    action: {
      add: '役割追加',
      edit: '役割編集',
      delete: '役割削除'
    }
  },
  menu: {
    title: 'メニュー管理',
    name: 'メニュー名',
    path: 'パス',
    component: 'コンポーネント',
    icon: 'アイコン',
    sort: '並び順',
    parent: '親メニュー',
    action: {
      add: 'メニュー追加',
      edit: 'メニュー編集',
      delete: 'メニュー削除'
    }
  },
  permission: {
    title: '権限管理',
    name: '権限名',
    code: '権限コード',
    description: '説明',
    action: {
      add: '権限追加',
      edit: '権限編集',
      delete: '権限削除'
    }
  }
} 
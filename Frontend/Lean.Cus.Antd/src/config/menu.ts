import { MenuConfig } from '@/types/menu'

const menu: MenuConfig[] = [
  {
    key: 'generator',
    title: '代码生成',
    icon: 'code',
    children: [
      {
        key: 'generator-list',
        title: '代码生成',
        path: '/generator'
      },
      {
        key: 'generator-template',
        title: '模板管理',
        path: '/generator/template'
      }
    ]
  }
]

export default menu 
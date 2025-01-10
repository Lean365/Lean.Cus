import { RouteRecordRaw } from 'vue-router'

const routes: RouteRecordRaw[] = [
  {
    path: '/generator',
    name: 'Generator',
    component: () => import('@/views/generator/index.vue'),
    meta: {
      title: '代码生成',
      icon: 'code'
    }
  },
  {
    path: '/generator/template',
    name: 'Template',
    component: () => import('@/views/generator/template/index.vue'),
    meta: {
      title: '模板管理',
      icon: 'file-text'
    }
  }
]

export default routes 
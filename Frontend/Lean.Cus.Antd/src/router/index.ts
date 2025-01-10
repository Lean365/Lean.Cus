import { createRouter, createWebHistory, type RouteLocationNormalized, type NavigationGuardNext } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import { useUserStore } from '@/stores/user'

// 工作流相关路由
const workflowRoutes: RouteRecordRaw[] = [
  {
    path: '/workflow/designer',
    name: 'WorkflowDesigner',
    component: () => import('@/views/workflow/designer/index.vue'),
    meta: {
      title: '工作流设计器',
      requiresAuth: true,
      permissions: ['workflow.design']
    }
  },
  {
    path: '/workflow/instance',
    name: 'WorkflowInstance',
    component: () => import('@/views/workflow/instance/index.vue'),
    meta: {
      title: '流程实例',
      requiresAuth: true,
      permissions: ['workflow.instance']
    }
  },
  {
    path: '/workflow/task/todo',
    name: 'WorkflowTaskTodo',
    component: () => import('@/views/workflow/task/todo.vue'),
    meta: {
      title: '待办任务',
      requiresAuth: true,
      permissions: ['workflow.task']
    }
  },
  {
    path: '/workflow/task/done',
    name: 'WorkflowTaskDone',
    component: () => import('@/views/workflow/task/done.vue'),
    meta: {
      title: '已办任务',
      requiresAuth: true,
      permissions: ['workflow.task']
    }
  },
  {
    path: '/workflow/history',
    name: 'WorkflowHistory',
    component: () => import('@/views/workflow/history/index.vue'),
    meta: {
      title: '流程历史',
      requiresAuth: true,
      permissions: ['workflow.history']
    }
  },
  {
    path: '/workflow/monitor',
    name: 'WorkflowMonitor',
    component: () => import('@/views/workflow/monitor/index.vue'),
    meta: {
      title: '流程监控',
      requiresAuth: true,
      permissions: ['workflow.monitor']
    }
  }
]

// 代码生成相关路由
const generatorRoutes: RouteRecordRaw[] = [
  {
    path: '/generator',
    name: 'Generator',
    component: () => import('@/views/generator/index.vue'),
    meta: {
      title: '代码生成',
      requiresAuth: true,
      permissions: ['generator']
    }
  }
]

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    redirect: '/workflow/designer'
  },
  ...workflowRoutes,
  ...generatorRoutes,
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/login/index.vue'),
    meta: {
      title: '登录'
    }
  },
  {
    path: '/:pathMatch(.*)*',
    name: 'NotFound',
    component: () => import('@/views/error/404.vue'),
    meta: {
      title: '404'
    }
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

// 路由守卫
router.beforeEach(async (to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
  // 设置页面标题
  document.title = `${to.meta.title} - Lean.Cus`

  // 检查是否需要登录认证
  if (to.meta.requiresAuth) {
    const token = localStorage.getItem('token')
    if (!token) {
      next({ name: 'Login', query: { redirect: to.fullPath } })
      return
    }

    // 检查权限
    if (to.meta.permissions) {
      const userStore = useUserStore()
      const hasPermission = await userStore.checkPermissions(to.meta.permissions as string[])
      if (!hasPermission) {
        next({ name: 'NotFound' })
        return
      }
    }
  }

  next()
})

export default router 
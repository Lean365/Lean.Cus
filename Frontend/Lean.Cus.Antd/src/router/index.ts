import { createRouter, createWebHistory, type RouteLocationNormalized, type NavigationGuardNext } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import { useUserStore } from '@/stores/user'
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'

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
    component: () => import('@/layouts/BasicLayout.vue'),
    redirect: '/home',
    children: [
      {
        path: '/home',
        name: 'Home',
        component: () => import('@/views/home/index.vue'),
        meta: {
          title: '首页',
          requiresAuth: true
        }
      },
      ...workflowRoutes,
      ...generatorRoutes,
    ]
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('@/views/auth/login.vue'),
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

// 白名单路由
const whiteList = ['/login', '/404']

// 路由守卫
router.beforeEach(async (to: RouteLocationNormalized, from: RouteLocationNormalized, next: NavigationGuardNext) => {
  // 开始进度条
  NProgress.start()

  // 设置页面标题
  document.title = `${to.meta.title} - Lean.Cus`

  const userStore = useUserStore()
  const token = userStore.token

  if (token) {
    if (to.path === '/login') {
      // 已登录且要跳转的页面是登录页
      next({ path: '/' })
      NProgress.done()
    } else {
      // 判断是否有用户信息
      if (userStore.userInfo) {
        // 检查权限
        if (to.meta.permissions) {
          const hasPermission = (to.meta.permissions as string[]).some(permission => 
            userStore.hasPermission(permission)
          )
          if (!hasPermission) {
            next({ name: 'NotFound' })
          } else {
            next()
          }
        } else {
          next()
        }
      } else {
        try {
          // 获取用户信息
          const hasUserInfo = await userStore.fetchUserInfo()
          if (hasUserInfo) {
            if (to.meta.permissions) {
              const hasPermission = (to.meta.permissions as string[]).some(permission => 
                userStore.hasPermission(permission)
              )
              if (!hasPermission) {
                next({ name: 'NotFound' })
              } else {
                next()
              }
            } else {
              next()
            }
          } else {
            userStore.clearUser()
            next(`/login?redirect=${to.path}`)
            NProgress.done()
          }
        } catch (error) {
          // 获取用户信息失败，可能是token过期
          userStore.clearUser()
          next(`/login?redirect=${to.path}`)
          NProgress.done()
        }
      }
    }
  } else {
    // 未登录
    if (whiteList.includes(to.path)) {
      // 在免登录白名单，直接进入
      next()
    } else {
      // 否则全部重定向到登录页
      next(`/login?redirect=${to.path}`)
      NProgress.done()
    }
  }
})

router.afterEach(() => {
  // 结束进度条
  NProgress.done()
})

// 添加全局路由错误处理
router.onError((error) => {
  console.error('Router error:', error)
  // 如果是chunk加载失败，自动刷新页面
  if (error.message.includes('Failed to load chunk')) {
    window.location.reload()
  }
})

export default router 
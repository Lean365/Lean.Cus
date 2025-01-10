import { defineStore } from 'pinia'
import { ref } from 'vue'

export const useUserStore = defineStore('user', () => {
  const token = ref('')
  const userInfo = ref<{
    id: string
    username: string
    realName: string
    avatar: string
    roles: string[]
    permissions: string[]
  } | null>(null)

  // 设置token
  function setToken(value: string) {
    token.value = value
  }

  // 设置用户信息
  function setUserInfo(info: typeof userInfo.value) {
    userInfo.value = info
  }

  // 清除用户信息
  function clearUser() {
    token.value = ''
    userInfo.value = null
  }

  // 判断是否有权限
  function hasPermission(permission: string) {
    return userInfo.value?.permissions.includes(permission) ?? false
  }

  // 判断是否有角色
  function hasRole(role: string) {
    return userInfo.value?.roles.includes(role) ?? false
  }

  return {
    token,
    userInfo,
    setToken,
    setUserInfo,
    clearUser,
    hasPermission,
    hasRole
  }
}) 
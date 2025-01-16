import { defineStore } from 'pinia'
import type { PiniaPluginContext } from 'pinia'
import { ref } from 'vue'
import type { LeanLoginResultDto } from '@/types/auth'

export const useUserStore = defineStore('user', () => {
  const token = ref('')
  const userInfo = ref<{
    id: string
    username: string
    realName: string
    avatar: string
    roles: string[]
    permissions: string[]
  }>()

  function setToken(value: string) {
    token.value = value
  }

  function setUserInfo(info: LeanLoginResultDto) {
    userInfo.value = {
      id: info.userId.toString(),
      username: info.userName,
      realName: info.realName,
      avatar: info.avatar,
      roles: info.roles,
      permissions: info.permissions
    }
  }

  function clearUser() {
    token.value = ''
    userInfo.value = undefined
  }

  function hasPermission(permission: string) {
    return userInfo.value?.permissions.includes(permission) ?? false
  }

  function hasRole(role: string) {
    return userInfo.value?.roles.includes(role) ?? false
  }

  async function login(loginResult: LeanLoginResultDto) {
    setToken(loginResult.accessToken)
    setUserInfo(loginResult)
  }

  function logout() {
    clearUser()
  }

  return {
    token,
    userInfo,
    setToken,
    setUserInfo,
    clearUser,
    hasPermission,
    hasRole,
    login,
    logout
  }
}, {
  persist: true
}) 
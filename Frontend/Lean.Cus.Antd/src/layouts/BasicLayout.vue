<template>
  <a-layout style="min-height: 100vh">
    <a-layout-sider v-model:collapsed="collapsed" collapsible>
      <div class="logo">
        <img src="@/assets/images/logo/logo.svg" alt="logo" />
      </div>
      <a-menu
        v-model:selectedKeys="selectedKeys"
        v-model:openKeys="openKeys"
        theme="dark"
        mode="inline"
      >
        <a-menu-item key="home">
          <template #icon>
            <home-outlined />
          </template>
          <router-link to="/home">{{ $t('menu.home') }}</router-link>
        </a-menu-item>
        <a-sub-menu key="workflow">
          <template #icon>
            <apartment-outlined />
          </template>
          <template #title>{{ $t('menu.workflow.title') }}</template>
          <a-menu-item key="workflow-designer">
            <router-link to="/workflow/designer">{{ $t('menu.workflow.designer') }}</router-link>
          </a-menu-item>
          <a-menu-item key="workflow-instance">
            <router-link to="/workflow/instance">{{ $t('menu.workflow.instance') }}</router-link>
          </a-menu-item>
          <a-menu-item key="workflow-task-todo">
            <router-link to="/workflow/task/todo">{{ $t('menu.workflow.task.todo') }}</router-link>
          </a-menu-item>
          <a-menu-item key="workflow-task-done">
            <router-link to="/workflow/task/done">{{ $t('menu.workflow.task.done') }}</router-link>
          </a-menu-item>
          <a-menu-item key="workflow-monitoring">
            <router-link to="/workflow/monitoring">{{ $t('menu.workflow.monitoring') }}</router-link>
          </a-menu-item>
        </a-sub-menu>
        <a-menu-item key="generator">
          <template #icon>
            <code-outlined />
          </template>
          <router-link to="/generator">{{ $t('menu.generator') }}</router-link>
        </a-menu-item>
      </a-menu>
    </a-layout-sider>
    <a-layout>
      <a-layout-header class="header">
        <div class="header-left">
          <component
            :is="collapsed ? 'MenuUnfoldOutlined' : 'MenuFoldOutlined'"
            class="trigger"
            @click="collapsed = !collapsed"
          />
        </div>
        <div class="header-right">
          <a-tooltip placement="left" :title="'自定义主页'">
            <a-dropdown>
              <a-button type="text">
                <template #icon><AppstoreOutlined /></template>
              </a-button>
              <template #overlay>
                <a-menu>
                  <a-menu-item key="add-statistic" @click="handleAddStatisticCard">
                    <template #icon><fund-outlined /></template>
                    添加统计卡片
                  </a-menu-item>
                  <a-menu-item key="add-shortcut" @click="handleAddShortcut">
                    <template #icon><link-outlined /></template>
                    添加快捷入口
                  </a-menu-item>
                  <a-menu-item key="add-chart" @click="handleAddChart">
                    <template #icon><area-chart-outlined /></template>
                    添加图表
                  </a-menu-item>
                </a-menu>
              </template>
            </a-dropdown>
          </a-tooltip>
          <a-tooltip placement="left" :title="'自定义皮肤'">
            <a-button type="text" @click="handleCustomTheme">
              <template #icon><SkinOutlined /></template>
            </a-button>
          </a-tooltip>
          <theme-switch />
          <lang-switch />
          <custom-theme ref="customThemeRef" />
          <a-dropdown>
            <a class="user-dropdown" @click.prevent>
              <a-avatar>
                <template #icon><UserOutlined /></template>
              </a-avatar>
              <span class="username">{{ userStore.userInfo?.realName }}</span>
              <DownOutlined />
            </a>
            <template #overlay>
              <a-menu>
                <a-menu-item>
                  <template #icon>
                    <SettingOutlined />
                  </template>
                  <span>{{ $t('menu.settings.personal') }}</span>
                </a-menu-item>
                <a-menu-divider />
                <a-menu-item @click="handleLogout">
                  <template #icon>
                    <LogoutOutlined />
                  </template>
                  <span>{{ $t('menu.settings.logout') }}</span>
                </a-menu-item>
              </a-menu>
            </template>
          </a-dropdown>
        </div>
      </a-layout-header>
      <a-layout-content class="content">
        <div class="content-container">
          <router-view v-slot="{ Component }">
            <component :is="Component" ref="homeViewRef">
              <template #statisticCards>
                <StatisticCards ref="statisticCardsRef" />
              </template>
              <template #shortcutEntries>
                <ShortcutEntries ref="shortcutEntriesRef" />
              </template>
              <template #chartStatistics>
                <ChartStatistics ref="chartStatisticsRef" />
              </template>
            </component>
          </router-view>
        </div>
      </a-layout-content>
      <a-layout-footer class="footer">
        Lean.Cus ©2024 Created by Lean Team
      </a-layout-footer>
    </a-layout>
  </a-layout>
</template>

<script setup lang="ts">
import { ref, watch } from 'vue'
import { useRouter, useRoute } from 'vue-router'
import { useUserStore } from '@/stores/user'
import { useI18n } from 'vue-i18n'
import { theme } from 'ant-design-vue'
import ThemeSwitch from '@/components/theme/index.vue'
import LangSwitch from '@/components/lang/index.vue'
import CustomTheme from '@/components/theme/CustomTheme.vue'
import StatisticCards from '@/components/home/StatisticCards.vue'
import ShortcutEntries from '@/components/home/ShortcutEntries.vue'
import ChartStatistics from '@/components/home/ChartStatistics.vue'
import {
  MenuFoldOutlined,
  MenuUnfoldOutlined,
  UserOutlined,
  HomeOutlined,
  ApartmentOutlined,
  CodeOutlined,
  SettingOutlined,
  LogoutOutlined,
  DownOutlined,
  FundOutlined,
  LinkOutlined,
  AreaChartOutlined,
  SkinOutlined,
  AppstoreOutlined
} from '@ant-design/icons-vue'

const router = useRouter()
const route = useRoute()
const userStore = useUserStore()
const { t } = useI18n()
const { token } = theme.useToken()

const collapsed = ref(false)
const selectedKeys = ref<string[]>([])
const openKeys = ref<string[]>([])

// 监听路由变化，更新选中的菜单项
watch(
  () => route.path,
  (path) => {
    const paths = path.split('/')
    const lastPath = paths[paths.length - 1]
    selectedKeys.value = [lastPath]
    openKeys.value = paths.slice(1, -1)
  },
  { immediate: true }
)

const handleLogout = () => {
  userStore.logout()
  router.push('/auth/login')
}

// 组件引用
const statisticCardsRef = ref()
const shortcutEntriesRef = ref()
const chartStatisticsRef = ref()
const customThemeRef = ref()
const homeViewRef = ref()

// 处理添加统计卡片
const handleAddStatisticCard = () => {
  const homeView = homeViewRef.value
  if (homeView) {
    homeView.statisticCardsRef?.showAddModal()
  }
}

// 处理添加快捷入口
const handleAddShortcut = () => {
  const homeView = homeViewRef.value
  if (homeView) {
    homeView.shortcutEntriesRef?.showAddModal()
  }
}

// 处理添加图表
const handleAddChart = () => {
  const homeView = homeViewRef.value
  if (homeView) {
    homeView.chartStatisticsRef?.showAddModal()
  }
}

// 处理自定义皮肤
const handleCustomTheme = () => {
  customThemeRef.value?.showCustomThemeModal()
}
</script>

<style lang="less" scoped>
.logo {
  height: 32px;
  margin: 16px;
  text-align: center;

  img {
    height: 100%;
  }
}

.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 16px;
  background-color: v-bind('token.colorBgContainer');
  border-bottom: 1px solid v-bind('token.colorBorderSecondary');

  .trigger {
    font-size: 18px;
    cursor: pointer;
    transition: color 0.3s;

    &:hover {
      color: v-bind('token.colorPrimary');
    }
  }

  .header-right {
    display: flex;
    align-items: center;
    gap: 16px;
  }

  .user-dropdown {
    display: flex;
    align-items: center;
    gap: 8px;
    cursor: pointer;
    color: v-bind('token.colorText');

    .username {
      margin: 0 8px;
    }
  }
}

.content {
  padding: 0;
  height: calc(100vh - 64px - 69px); // 减去头部高度(64px)和底部高度(69px)
  overflow-y: auto;
  
  .content-container {
    height: 100%;
  }
}

.footer {
  text-align: center;
  color: v-bind('token.colorTextSecondary');
  background-color: transparent;
}
</style> 
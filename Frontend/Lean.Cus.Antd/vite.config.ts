import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import vueJsx from '@vitejs/plugin-vue-jsx';
import AutoImport from 'unplugin-auto-import/vite';
import Components from 'unplugin-vue-components/vite';
import { AntDesignVueResolver } from 'unplugin-vue-components/resolvers';
import Icons from 'unplugin-icons/vite';
import IconsResolver from 'unplugin-icons/resolver';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueJsx(),
    AutoImport({
      // 自动导入 Vue 相关函数，如：ref, reactive, toRef 等
      imports: [
        'vue',
        'vue-router',
        'vue-i18n',
        '@vueuse/core',
        'pinia',
        {
          'ant-design-vue': [
            'message',
            'notification',
            'Modal',
            'Form'
          ]
        }
      ],
      // 自动导入组合式API文件夹下的所有文件
      dirs: [
        'src/composables/**',
        'src/stores/**',
        'src/utils/**',
        'src/api/**',
        'src/directives/**'
      ],
      // 生成类型声明文件
      dts: 'src/auto-imports.d.ts',
      // 在Vue模板中自动导入
      vueTemplate: true,
      // 自动导入目录下的模块
      resolvers: [
        // Ant Design Vue 组件按需导入
        AntDesignVueResolver({
          resolveIcons: true, // 自动导入图标
          importStyle: true, // 自动导入样式
          importLess: true // 使用 Less 样式源文件以支持主题定制
        })
      ]
    }),
    Components({
      // 自动导入组件
      dirs: ['src/components'],
      // 组件的有效文件扩展名
      extensions: ['vue', 'tsx'],
      // 配置type文件生成位置
      dts: 'src/components.d.ts',
      // 自定义组件的解析器
      resolvers: [
        // Ant Design Vue 组件按需导入
        AntDesignVueResolver({
          importStyle: true, // 自动导入样式
          importLess: true, // 使用 Less 样式源文件以支持主题定制
          resolveIcons: true // 自动导入图标
        }),
        // 自动导入图标组件
        IconsResolver({
          prefix: 'Icon',
          // 启用的图标集合
          enabledCollections: [
            'ant-design', // Ant Design 图标
            'mdi', // Material Design 图标
            'carbon', // Carbon 图标
            'ic', // Google Material Icons
            'ri', // Remix Icons
            'ep', // Element Plus 图标
            'bi', // Bootstrap Icons
            'fa', // Font Awesome 图标
            'fa-solid', // Font Awesome Solid 图标
            'fa-regular', // Font Awesome Regular 图标
            'fa-brands' // Font Awesome Brands 图标
          ]
        })
      ]
    }),
    // 自动导入图标
    Icons({
      compiler: 'vue3',
      autoInstall: true,
      defaultClass: 'anticon', // 使用 anticon 类名，保持与 Ant Design Vue 一致的样式
      defaultStyle: 'vertical-align: -0.125em;', // 图标垂直对齐方式
      scale: 1
    })
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  css: {
    preprocessorOptions: {
      less: {
        javascriptEnabled: true,
        // Ant Design Vue 的主题变量
        modifyVars: {
          // 使用 Ant Design Vue 的默认主题变量
          // 如果需要覆盖，可以在这里添加
          // 例如：
          // 'primary-color': '#1890ff',
          // 'link-color': '#1890ff',
          // 'success-color': '#52c41a',
          // 'warning-color': '#faad14',
          // 'error-color': '#f5222d',
          // 'font-size-base': '14px',
          // 'heading-color': 'rgba(0, 0, 0, 0.85)',
          // 'text-color': 'rgba(0, 0, 0, 0.65)',
          // 'text-color-secondary': 'rgba(0, 0, 0, 0.45)',
          // 'disabled-color': 'rgba(0, 0, 0, 0.25)',
          // 'border-radius-base': '2px',
          // 'border-color-base': '#d9d9d9',
          // 'box-shadow-base': '0 2px 8px rgba(0, 0, 0, 0.15)'
        }
      }
    }
  },
  server: {
    port: 5173,
    proxy: {
      '/api': {
        target: 'http://localhost:5172',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/api/, '')
      },
      '/signalr': {
        target: 'http://localhost:5172',
        ws: true,
        changeOrigin: true
      }
    }
  }
}) 
<template>
  <div class="statistic-cards">
    <div class="section-title">
      <span class="title-text">统计卡片</span>
    </div>
    <a-row :gutter="[16, 16]">
      <a-col :span="4" :xs="12" :sm="8" :md="6" :lg="4" v-for="item in statistics" :key="item.id">
        <a-card hoverable class="statistic-item" :bodyStyle="{ padding: '4px', height: '120px', display: 'flex', flexDirection: 'column' }">
          <div class="card-header" style="margin-bottom: 8px; padding: 4px 8px;">
            <span class="card-title">{{ item.title }}</span>
            <div class="card-actions">
              <a-space :size="4">
                <a-button type="text" size="small" @click.stop="handleEdit(item)">
                  <template #icon><edit-outlined style="font-size: 13px" /></template>
                </a-button>
                <a-button type="text" size="small" danger @click.stop="handleDelete(item)">
                  <template #icon><delete-outlined style="font-size: 13px" /></template>
                </a-button>
              </a-space>
            </div>
          </div>
          <div class="statistic-content" style="flex: 1; display: flex; align-items: center; justify-content: center;">
            <a-statistic
              :value="item.value"
              :precision="item.unit === '%' ? 1 : 0"
              :value-style="{ color: item.iconColor, fontSize: 'clamp(20px, 3vw, 28px)', fontWeight: 600, lineHeight: 1 }"
              :suffix="item.unit"
              animation
            >
              <template #prefix>
                <component :is="item.icon" :style="{ color: item.iconColor, fontSize: 'clamp(20px, 3vw, 28px)' }" />
              </template>
            </a-statistic>
          </div>
        </a-card>
      </a-col>
    </a-row>
  </div>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { EditOutlined, DeleteOutlined, MoneyCollectOutlined, ShoppingOutlined, UserOutlined, FundOutlined } from '@ant-design/icons-vue'
import { message } from 'ant-design-vue'

// 统计卡片数据
const statistics = ref([
  {
    id: '1',
    title: '总销售额',
    value: 126500,
    unit: '元',
    icon: MoneyCollectOutlined,
    iconColor: '#1890ff'
  },
  {
    id: '2',
    title: '订单数量',
    value: 1568,
    unit: '单',
    icon: ShoppingOutlined,
    iconColor: '#52c41a'
  },
  {
    id: '3',
    title: '活跃用户',
    value: 15280,
    unit: '人',
    icon: UserOutlined,
    iconColor: '#722ed1'
  },
  {
    id: '4',
    title: '转化率',
    value: 15.8,
    unit: '%',
    icon: FundOutlined,
    iconColor: '#fa8c16'
  }
])

// 编辑统计卡片
const handleEdit = (item: any) => {
  message.info('编辑功能开发中')
}

// 删除统计卡片
const handleDelete = (item: any) => {
  message.info('删除功能开发中')
}
</script>

<style scoped lang="less">
.statistic-cards {
  .section-title {
    margin-bottom: 16px;
    .title-text {
      font-size: 16px;
      font-weight: 500;
      color: var(--ant-color-text);
    }
  }

  .statistic-item {
    .card-header {
      display: flex;
      justify-content: space-between;
      align-items: center;
      
      .card-title {
        font-size: 13px;
        color: var(--ant-color-text-secondary);
      }
      
      .card-actions {
        opacity: 0;
        transition: opacity 0.3s;
        visibility: hidden;
      }
    }
    
    .statistic-content {
      height: 100%;
      display: flex;
      align-items: center;
      justify-content: center;

      .ant-statistic {
        width: 100%;
        text-align: center;
        
        .ant-statistic-content {
          display: flex;
          align-items: center;
          justify-content: center;
          gap: 8px;

          .ant-statistic-content-value {
            color: var(--ant-color-text);
          }

          .ant-statistic-content-suffix {
            color: var(--ant-color-text-secondary);
          }
        }
      }
    }

    &:hover {
      .card-header {
        .card-actions {
          opacity: 1;
          visibility: visible;
        }
      }
    }
  }
}
</style>
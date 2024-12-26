<template>
  <a-layout class="chat-layout">
    <a-layout-sider width="300" class="chat-sider">
      <div class="online-users">
        <a-list
          :dataSource="chatStore.onlineUsers"
          :renderItem="renderUser"
          class="user-list"
        >
          <template #header>
            <div class="list-header">
              <span>在线用户 ({{ chatStore.onlineUsers.length }})</span>
              <a-button
                v-if="chatStore.isAdmin"
                type="primary"
                danger
                size="small"
                @click="handleBatchOffline"
                :disabled="selectedUsers.length === 0"
              >
                批量下线
              </a-button>
            </div>
          </template>
          <template #renderItem="{ item }">
            <a-list-item>
              <a-list-item-meta>
                <template #title>
                  <div class="user-item">
                    <span>{{ item.userName }}</span>
                    <a-space>
                      <a-checkbox
                        v-if="chatStore.isAdmin"
                        v-model:checked="selectedUsers"
                        :value="item.id"
                      />
                      <a-button
                        v-if="chatStore.isAdmin"
                        type="link"
                        danger
                        size="small"
                        @click="handleForceOffline(item)"
                      >
                        下线
                      </a-button>
                    </a-space>
                  </div>
                </template>
                <template #description>
                  <span>{{ item.clientType }} - {{ item.networkType || '未知网络' }}</span>
                </template>
              </a-list-item-meta>
            </a-list-item>
          </template>
        </a-list>
      </div>
    </a-layout-sider>

    <a-layout-content class="chat-content">
      <div class="message-area">
        <div class="messages" ref="messageContainer">
          <a-list
            :dataSource="chatStore.messages"
            :renderItem="renderMessage"
          />
        </div>
        <div class="input-area">
          <a-input-group compact>
            <a-input
              v-model:value="messageContent"
              placeholder="输入消息..."
              @keyup.enter="sendMessage"
              class="message-input"
            />
            <a-button type="primary" @click="sendMessage">发送</a-button>
          </a-input-group>
        </div>
      </div>
    </a-layout-content>

    <a-layout-sider width="300" class="user-info-sider">
      <div class="user-info" v-if="selectedUser">
        <a-descriptions title="用户信息" bordered>
          <a-descriptions-item label="用户名">
            {{ selectedUser.userName }}
          </a-descriptions-item>
          <a-descriptions-item label="客户端类型">
            {{ selectedUser.clientType }}
          </a-descriptions-item>
          <a-descriptions-item label="操作系统">
            {{ selectedUser.osInfo }}
          </a-descriptions-item>
          <a-descriptions-item label="分辨率">
            {{ selectedUser.resolution }}
          </a-descriptions-item>
          <a-descriptions-item label="网络类型">
            {{ selectedUser.networkType }}
          </a-descriptions-item>
          <a-descriptions-item label="地理位置">
            {{ selectedUser.location || '未知' }}
          </a-descriptions-item>
          <a-descriptions-item label="最后活动">
            {{ formatTime(selectedUser.lastActiveTime) }}
          </a-descriptions-item>
        </a-descriptions>
      </div>
    </a-layout-sider>
  </a-layout>
</template>

<script setup lang="ts">
import { ref, onMounted, computed } from 'vue';
import { useChatStore } from '../stores/chat';
import type { OnlineUser, ChatMessage } from '../types/chat';
import { storeToRefs } from 'pinia';
import dayjs from 'dayjs';
import { Modal } from 'ant-design-vue';

const chatStore = useChatStore();
const messageContent = ref('');
const messageContainer = ref<HTMLElement | null>(null);
const selectedUsers = ref<number[]>([]);

const selectedUser = computed(() => chatStore.selectedUser);

onMounted(async () => {
  try {
    await chatStore.initializeConnection();
    // TODO: 这里应该根据实际登录用户的角色来设置
    chatStore.setAdmin(true);
  } catch (error) {
    console.error('初始化连接失败:', error);
  }
});

const handleForceOffline = (user: OnlineUser) => {
  Modal.confirm({
    title: '强制下线确认',
    content: `确定要将用户 ${user.userName} 强制下线吗？`,
    okText: '确定',
    cancelText: '取消',
    okType: 'danger',
    async onOk() {
      await chatStore.forceOffline(user.id);
    },
  });
};

const handleBatchOffline = () => {
  if (selectedUsers.value.length === 0) return;

  Modal.confirm({
    title: '批量强制下线确认',
    content: `确定要将选中的 ${selectedUsers.value.length} 个用户强制下线吗？`,
    okText: '确定',
    cancelText: '取消',
    okType: 'danger',
    async onOk() {
      await chatStore.forceOfflineBatch(selectedUsers.value);
      selectedUsers.value = [];
    },
  });
};

const renderUser = (user: OnlineUser) => {
  return {
    title: user.userName,
    description: `${user.clientType} - ${user.networkType || '未知网络'}`,
  };
};

const renderMessage = (message: ChatMessage) => {
  const isCurrentUser = message.senderId === chatStore.currentUser?.id;
  return {
    content: message.content,
    datetime: formatTime(message.createTime),
    author: message.senderName,
    class: isCurrentUser ? 'message-right' : 'message-left',
  };
};

const sendMessage = async () => {
  if (!messageContent.value.trim()) return;
  
  try {
    await chatStore.sendMessage(messageContent.value);
    messageContent.value = '';
    scrollToBottom();
  } catch (error) {
    console.error('发送消息失败:', error);
  }
};

const scrollToBottom = () => {
  if (messageContainer.value) {
    messageContainer.value.scrollTop = messageContainer.value.scrollHeight;
  }
};

const formatTime = (time: string) => {
  return dayjs(time).format('YYYY-MM-DD HH:mm:ss');
};
</script>

<style scoped>
.chat-layout {
  height: 100vh;
}

.chat-sider {
  background: #fff;
  border-right: 1px solid #f0f0f0;
}

.chat-content {
  padding: 24px;
  background: #fff;
}

.user-info-sider {
  background: #fff;
  border-left: 1px solid #f0f0f0;
  padding: 24px;
}

.online-users {
  height: 100%;
  overflow-y: auto;
}

.user-list {
  padding: 0 16px;
}

.list-header {
  padding: 16px 0;
  border-bottom: 1px solid #f0f0f0;
}

.message-area {
  display: flex;
  flex-direction: column;
  height: 100%;
}

.messages {
  flex: 1;
  overflow-y: auto;
  padding: 16px;
}

.input-area {
  padding: 16px;
  border-top: 1px solid #f0f0f0;
}

.message-input {
  width: calc(100% - 88px);
}

.message-left {
  text-align: left;
}

.message-right {
  text-align: right;
}

.user-info {
  padding: 16px;
}

.user-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  width: 100%;
}

.list-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}
</style> 
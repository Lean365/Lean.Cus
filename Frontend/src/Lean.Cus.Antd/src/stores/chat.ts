import { defineStore } from 'pinia';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import type { ChatState, OnlineUser, ChatMessage } from '../types/chat';
import { message } from 'ant-design-vue';

const SIGNALR_HUB_URL = import.meta.env.VITE_SIGNALR_HUB_URL;

export const useChatStore = defineStore('chat', {
    state: (): ChatState => ({
        onlineUsers: [],
        messages: [],
        currentUser: null,
        selectedUser: null,
        unreadMessages: [],
        connected: false,
        isAdmin: false
    }),

    actions: {
        async initializeConnection() {
            const connection = new HubConnectionBuilder()
                .withUrl(SIGNALR_HUB_URL)
                .withAutomaticReconnect()
                .build();

            // 保存连接实例
            (this as any).connection = connection;

            connection.on('UserConnected', (user: OnlineUser) => {
                this.onlineUsers.push(user);
            });

            connection.on('UserDisconnected', (connectionId: string) => {
                const index = this.onlineUsers.findIndex(u => u.connectionId === connectionId);
                if (index !== -1) {
                    this.onlineUsers.splice(index, 1);
                }
            });

            connection.on('ReceiveMessage', (message: ChatMessage) => {
                this.messages.push(message);
                if (message.status === 0) {
                    this.unreadMessages.push(message);
                }
            });

            // 添加强制下线处理
            connection.on('ForceOffline', (reason: string) => {
                message.warning(reason);
                this.connected = false;
                connection.stop().catch(console.error);
            });

            // 添加断开连接处理
            connection.on('Disconnect', () => {
                connection.stop().catch(console.error);
            });

            try {
                await connection.start();
                this.connected = true;
                await this.loadOnlineUsers();
                await this.loadUnreadMessages();
                await this.updateClientInfo();
            } catch (error) {
                console.error('连接失败:', error);
                throw error;
            }

            return connection;
        },

        async loadOnlineUsers() {
            try {
                const connection = (this as any).connection as HubConnection;
                this.onlineUsers = await connection.invoke('GetOnlineUsers');
            } catch (error) {
                console.error('加载在线用户失败:', error);
            }
        },

        async loadUnreadMessages() {
            try {
                const connection = (this as any).connection as HubConnection;
                this.unreadMessages = await connection.invoke('GetUnreadMessages');
            } catch (error) {
                console.error('加载未读消息失败:', error);
            }
        },

        async sendMessage(content: string) {
            if (!this.connected) return;

            const message: Partial<ChatMessage> = {
                content,
                type: 1,
                receiverId: this.selectedUser?.id ?? 0,
                receiverName: this.selectedUser?.userName ?? 'All'
            };

            try {
                const connection = (this as any).connection as HubConnection;
                await connection.invoke('SendMessage', message);
            } catch (error) {
                console.error('发送消息失败:', error);
                throw error;
            }
        },

        async markAsRead(messageId: number) {
            try {
                const connection = (this as any).connection as HubConnection;
                await connection.invoke('MarkAsRead', messageId);
                const index = this.unreadMessages.findIndex(m => m.id === messageId);
                if (index !== -1) {
                    this.unreadMessages.splice(index, 1);
                }
            } catch (error) {
                console.error('标记消息已读失败:', error);
            }
        },

        async updateClientInfo() {
            try {
                const resolution = `${window.screen.width}x${window.screen.height}`;
                const networkType = (navigator as any).connection?.type || 'unknown';
                const connection = (this as any).connection as HubConnection;
                await connection.invoke('UpdateClientInfo', resolution, networkType);
            } catch (error) {
                console.error('更新客户端信息失败:', error);
            }
        },

        setSelectedUser(user: OnlineUser | null) {
            this.selectedUser = user;
        },

        // 添加强制下线方法
        async forceOffline(userId: number) {
            if (!this.isAdmin) {
                message.error('没有权限执行此操作');
                return;
            }

            try {
                const connection = (this as any).connection as HubConnection;
                await connection.invoke('ForceOffline', userId);
                message.success('强制下线成功');
            } catch (error) {
                console.error('强制下线失败:', error);
                message.error('强制下线失败');
            }
        },

        // 添加批量强制下线方法
        async forceOfflineBatch(userIds: number[]) {
            if (!this.isAdmin) {
                message.error('没有权限执行此操作');
                return;
            }

            try {
                const connection = (this as any).connection as HubConnection;
                await connection.invoke('ForceOfflineBatch', userIds);
                message.success('批量强制下线成功');
            } catch (error) {
                console.error('批量强制下线失败:', error);
                message.error('批量强制下线失败');
            }
        },

        // 添加获取用户连接信息方法
        async getUserConnectionInfo(userId: number) {
            try {
                const connection = (this as any).connection as HubConnection;
                return await connection.invoke('GetUserConnectionInfo', userId);
            } catch (error) {
                console.error('获取用户连接信息失败:', error);
                return null;
            }
        },

        // 设置管理员权限
        setAdmin(isAdmin: boolean) {
            this.isAdmin = isAdmin;
        }
    }
}); 
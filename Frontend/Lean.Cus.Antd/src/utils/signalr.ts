import * as signalR from '@microsoft/signalr';
import { useUserStore } from '@/stores/user';

class SignalRService {
  private connection: signalR.HubConnection | null = null;
  private reconnectAttempts = 0;
  private maxReconnectAttempts = 5;
  private reconnectInterval = 5000; // 5秒

  async start() {
    if (this.connection) return;

    const userStore = useUserStore();
    const token = userStore.token;

    this.connection = new signalR.HubConnectionBuilder()
      .withUrl('/signalr/workflow', {
        accessTokenFactory: () => token
      })
      .withAutomaticReconnect({
        nextRetryDelayInMilliseconds: (retryContext) => {
          if (retryContext.previousRetryCount >= this.maxReconnectAttempts) {
            return null; // 停止重连
          }

          // 指数退避重连
          return Math.min(1000 * Math.pow(2, retryContext.previousRetryCount), 30000);
        }
      })
      .build();

    // 注册重连事件处理
    this.connection.onreconnecting((error) => {
      console.log('正在重新连接...', error);
      this.reconnectAttempts++;
    });

    this.connection.onreconnected((connectionId) => {
      console.log('重新连接成功', connectionId);
      this.reconnectAttempts = 0;
    });

    this.connection.onclose((error) => {
      console.log('连接关闭', error);
      if (this.reconnectAttempts < this.maxReconnectAttempts) {
        setTimeout(() => this.start(), this.reconnectInterval);
      }
    });

    try {
      await this.connection.start();
      console.log('SignalR连接成功');
      this.reconnectAttempts = 0;
    } catch (err) {
      console.error('SignalR连接失败:', err);
      if (this.reconnectAttempts < this.maxReconnectAttempts) {
        setTimeout(() => this.start(), this.reconnectInterval);
      }
    }
  }

  // 订阅工作流状态变更
  onWorkflowStateChanged(callback: (data: any) => void) {
    this.connection?.on('WorkflowStateChanged', callback);
  }

  // 订阅新任务通知
  onNewTaskAssigned(callback: (data: any) => void) {
    this.connection?.on('NewTaskAssigned', callback);
  }

  // 订阅任务状态变更
  onTaskStateChanged(callback: (data: any) => void) {
    this.connection?.on('TaskStateChanged', callback);
  }

  // 取消订阅
  off(methodName: string) {
    this.connection?.off(methodName);
  }

  // 停止连接
  async stop() {
    if (this.connection) {
      try {
        await this.connection.stop();
        this.connection = null;
      } catch (err) {
        console.error('停止SignalR连接失败:', err);
      }
    }
  }
}

export const signalRService = new SignalRService(); 
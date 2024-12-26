export interface OnlineUser {
    id: number;
    userName: string;
    connectionId: string;
    lastActiveTime: string;
    status: number;
    ipAddress: string;
    clientType: string;
    clientVersion: string;
    osInfo: string;
    browserInfo: string;
    deviceId: string;
    loginTime: string;
    resolution: string;
    networkType: string;
    location: string;
    language: string;
    timeZone: string;
}

export interface ChatMessage {
    id: number;
    senderId: number;
    senderName: string;
    receiverId: number;
    receiverName: string;
    content: string;
    type: number;
    status: number;
    createTime: string;
}

export interface ChatState {
    onlineUsers: OnlineUser[];
    messages: ChatMessage[];
    currentUser: OnlineUser | null;
    selectedUser: OnlineUser | null;
    unreadMessages: ChatMessage[];
    connected: boolean;
    isAdmin: boolean;
} 
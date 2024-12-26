using System.Windows.Forms;
using Microsoft.AspNetCore.SignalR.Client;
using Lean.Cus.Domain.Entities.Chat;
using System.Management;
using System.Net.NetworkInformation;

namespace Lean.Cus.WinForm.Forms;

public partial class ChatForm : Form
{
    private readonly HubConnection _hubConnection;
    private List<LeanOnlineUser> _onlineUsers;
    private long _currentUserId;
    private string _currentUserName;
    private System.Windows.Forms.Timer _updateTimer;

    public ChatForm(long userId, string userName)
    {
        InitializeComponent();
        _currentUserId = userId;
        _currentUserName = userName;
        _onlineUsers = new List<LeanOnlineUser>();

        // 初始化SignalR连接
        _hubConnection = new HubConnectionBuilder()
            .WithUrl("http://localhost:5000/chatHub")
            .WithAutomaticReconnect()
            .Build();

        // 注册SignalR事件处理程序
        RegisterHubHandlers();

        // 初始化更新定时器
        InitializeUpdateTimer();

        // 启动连接
        ConnectToHub();
    }

    private void InitializeUpdateTimer()
    {
        _updateTimer = new System.Windows.Forms.Timer();
        _updateTimer.Interval = 30000; // 每30秒更新一次
        _updateTimer.Tick += async (s, e) => await UpdateClientInfo();
        _updateTimer.Start();
    }

    private async Task UpdateClientInfo()
    {
        try
        {
            var resolution = $"{Screen.PrimaryScreen.Bounds.Width}x{Screen.PrimaryScreen.Bounds.Height}";
            var networkType = GetNetworkType();
            await _hubConnection.InvokeAsync("UpdateClientInfo", resolution, networkType);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"更新客户端信息失败: {ex.Message}");
        }
    }

    private string GetNetworkType()
    {
        try
        {
            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                if (ni.OperationalStatus == OperationalStatus.Up)
                {
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Wireless80211)
                        return "WiFi";
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Ethernet)
                        return "Ethernet";
                    if (ni.NetworkInterfaceType == NetworkInterfaceType.Ppp)
                        return "PPP";
                }
            }
        }
        catch
        {
            // 忽略错误
        }
        return "Unknown";
    }

    private async void ConnectToHub()
    {
        try
        {
            await _hubConnection.StartAsync();
            await LoadOnlineUsers();
            await LoadUnreadMessages();
            await UpdateClientInfo(); // 连接后立即发送客户端信息
        }
        catch (Exception ex)
        {
            MessageBox.Show($"连接服务器失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void RegisterHubHandlers()
    {
        _hubConnection.On<LeanOnlineUser>("UserConnected", user =>
        {
            BeginInvoke(new Action(() =>
            {
                _onlineUsers.Add(user);
                UpdateOnlineUsersList();
            }));
        });

        _hubConnection.On<string>("UserDisconnected", connectionId =>
        {
            BeginInvoke(new Action(() =>
            {
                var user = _onlineUsers.FirstOrDefault(u => u.ConnectionId == connectionId);
                if (user != null)
                {
                    _onlineUsers.Remove(user);
                    UpdateOnlineUsersList();
                }
            }));
        });

        _hubConnection.On<LeanMessage>("ReceiveMessage", message =>
        {
            BeginInvoke(new Action(() =>
            {
                AppendMessage(message);
            }));
        });
    }

    private async Task LoadOnlineUsers()
    {
        _onlineUsers = await _hubConnection.InvokeAsync<List<LeanOnlineUser>>("GetOnlineUsers");
        UpdateOnlineUsersList();
    }

    private async Task LoadUnreadMessages()
    {
        var messages = await _hubConnection.InvokeAsync<List<LeanMessage>>("GetUnreadMessages");
        foreach (var message in messages)
        {
            AppendMessage(message);
        }
    }

    private void UpdateOnlineUsersList()
    {
        lstOnlineUsers.Items.Clear();
        foreach (var user in _onlineUsers)
        {
            lstOnlineUsers.Items.Add(user.UserName);
        }
    }

    private void AppendMessage(LeanMessage message)
    {
        string formattedMessage = $"[{message.CreateTime:HH:mm:ss}] {message.SenderName}: {message.Content}\r\n";
        txtMessages.AppendText(formattedMessage);
    }

    private async void btnSend_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(txtMessage.Text))
            return;

        var message = new LeanMessage
        {
            Content = txtMessage.Text,
            Type = 1, // 文本消息
            ReceiverId = lstOnlineUsers.SelectedIndex >= 0 ? _onlineUsers[lstOnlineUsers.SelectedIndex].Id : 0,
            ReceiverName = lstOnlineUsers.SelectedIndex >= 0 ? _onlineUsers[lstOnlineUsers.SelectedIndex].UserName : "All"
        };

        try
        {
            await _hubConnection.InvokeAsync("SendMessage", message);
            txtMessage.Clear();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"发送消息失败: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);
        _updateTimer?.Stop();
        _updateTimer?.Dispose();
        if (_hubConnection.State == HubConnectionState.Connected)
        {
            try
            {
                _hubConnection.StopAsync().Wait();
            }
            catch
            {
                // 忽略关闭时的错误
            }
        }
    }

    private void InitializeComponent()
    {
        this.lstOnlineUsers = new System.Windows.Forms.ListBox();
        this.txtMessages = new System.Windows.Forms.TextBox();
        this.txtMessage = new System.Windows.Forms.TextBox();
        this.btnSend = new System.Windows.Forms.Button();
        this.SuspendLayout();
        
        // lstOnlineUsers
        this.lstOnlineUsers.Dock = System.Windows.Forms.DockStyle.Left;
        this.lstOnlineUsers.FormattingEnabled = true;
        this.lstOnlineUsers.ItemHeight = 17;
        this.lstOnlineUsers.Location = new System.Drawing.Point(0, 0);
        this.lstOnlineUsers.Name = "lstOnlineUsers";
        this.lstOnlineUsers.Size = new System.Drawing.Size(150, 450);
        this.lstOnlineUsers.TabIndex = 0;
        
        // txtMessages
        this.txtMessages.Dock = System.Windows.Forms.DockStyle.Top;
        this.txtMessages.Location = new System.Drawing.Point(150, 0);
        this.txtMessages.Multiline = true;
        this.txtMessages.Name = "txtMessages";
        this.txtMessages.ReadOnly = true;
        this.txtMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
        this.txtMessages.Size = new System.Drawing.Size(650, 380);
        this.txtMessages.TabIndex = 1;
        
        // txtMessage
        this.txtMessage.Location = new System.Drawing.Point(150, 385);
        this.txtMessage.Multiline = true;
        this.txtMessage.Name = "txtMessage";
        this.txtMessage.Size = new System.Drawing.Size(570, 60);
        this.txtMessage.TabIndex = 2;
        
        // btnSend
        this.btnSend.Location = new System.Drawing.Point(725, 385);
        this.btnSend.Name = "btnSend";
        this.btnSend.Size = new System.Drawing.Size(75, 60);
        this.btnSend.TabIndex = 3;
        this.btnSend.Text = "发送";
        this.btnSend.UseVisualStyleBackColor = true;
        this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
        
        // ChatForm
        this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(800, 450);
        this.Controls.Add(this.btnSend);
        this.Controls.Add(this.txtMessage);
        this.Controls.Add(this.txtMessages);
        this.Controls.Add(this.lstOnlineUsers);
        this.Name = "ChatForm";
        this.Text = "聊天室";
        this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
        this.ResumeLayout(false);
        this.PerformLayout();
    }

    private System.Windows.Forms.ListBox lstOnlineUsers;
    private System.Windows.Forms.TextBox txtMessages;
    private System.Windows.Forms.TextBox txtMessage;
    private System.Windows.Forms.Button btnSend;
} 
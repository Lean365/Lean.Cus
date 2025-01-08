# Windows Forms 客户端开发规范

## 目录
- [开发环境](#开发环境)
- [项目结构](#项目结构)
- [命名规范](#命名规范)
- [代码规范](#代码规范)
- [UI设计规范](#ui设计规范)
- [最佳实践](#最佳实践)

## 开发环境
- Visual Studio 2022
- .NET 8.0 SDK
- Windows Forms
- NuGet包管理

## 项目结构
```
Lean.Cus.WinForm/
├── Forms/                 # 窗体文件
│   ├── Main/            # 主窗体
│   ├── System/          # 系统管理
│   └── Business/        # 业务窗体
│
├── Models/               # 数据模型
│   ├── Entities/        # 实体类
│   ├── Dtos/           # 数据传输对象
│   └── Params/         # 参数模型
│
├── Services/            # 服务类
│   ├── System/         # 系统服务
│   ├── Business/       # 业务服务
│   └── Interfaces/     # 服务接口
│
├── Utils/              # 工具类
│   ├── Http/          # HTTP 请求
│   ├── Cache/         # 缓存操作
│   ├── Security/      # 安全相关
│   └── Extensions/    # 扩展方法
```

## 命名规范
### 窗体命名
- 以Form结尾
- 使用PascalCase命名
- 按功能模块组织

### 控件命名
- Button: btn前缀
- TextBox: txt前缀
- Label: lbl前缀
- DataGridView: dgv前缀
- ComboBox: cmb前缀

## 代码规范
### 基本原则
- 使用异步方法
- 实现错误处理
- 添加日志记录
- 使用依赖注入

### 示例代码
```csharp
public partial class UserListForm : Form
{
    private readonly IUserService _userService;
    private readonly ILogger<UserListForm> _logger;

    public UserListForm(IUserService userService, ILogger<UserListForm> logger)
    {
        InitializeComponent();
        _userService = userService;
        _logger = logger;
    }

    private async Task LoadDataAsync()
    {
        try
        {
            var data = await _userService.GetListAsync();
            dgvUsers.DataSource = data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "加载用户数据失败");
            MessageBox.Show("加载数据失败，请重试", "错误", 
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
```

## UI设计规范
### 布局原则
- 统一的间距和对齐
- 合理的控件分组
- 清晰的视觉层次
- 响应式设计

### 交互设计
- 即时反馈
- 错误提示
- 加载状态
- 快捷键支持

## 最佳实践
### 性能优化
- 异步加载
- 数据缓存
- 延迟加载
- 控件复用

### 用户体验
- 友好的错误提示
- 操作确认对话框
- 进度显示
- 状态栏信息 
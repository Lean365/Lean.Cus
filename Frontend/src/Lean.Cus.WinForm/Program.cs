using Lean.Cus.WinForm.Forms;

namespace Lean.Cus.WinForm;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        
        // TODO: 这里应该从登录表单获取用户ID和用户名
        var userId = 1L;
        var userName = "TestUser";
        
        Application.Run(new ChatForm(userId, userName));
    }
} 
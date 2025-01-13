using Lean.Cus.Common.Enums;
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Generator.Entities.Designer;
using Lean.Cus.Generator.Entities.Generator;
using Lean.Cus.Generator.Entities.Import;
using Lean.Cus.Workflow.Entities.Definition;
using Lean.Cus.Workflow.Enums;
using SqlSugar;

namespace Lean.Cus.Infrastructure.Data.Seeds;

/// <summary>
/// 数据种子初始化
/// </summary>
public class LeanDataSeedContributor
{
    private readonly ISqlSugarClient _db;

    public LeanDataSeedContributor(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 初始化种子数据
    /// </summary>
    public async Task SeedAsync()
    {
        // 初始化用户数据
        await SeedUsersAsync();

        // 初始化部门数据
        await SeedDepartmentsAsync();

        // 初始化角色数据
        await SeedRolesAsync();

        // 初始化菜单数据
        await SeedMenusAsync();

        // 初始化权限数据
        await SeedPermissionsAsync();

        // 初始化代码生成器模板数据
        await SeedGeneratorTemplatesAsync();

        // 初始化工作流定义数据
        await SeedWorkflowDefinitionsAsync();

        // 初始化代码生成器设计器数据
        await SeedTableDesignerAsync();

        // 初始化代码生成器导入数据
        await SeedTableImportAsync();

        // 初始化用户扩展数据
        await SeedUserExtendAsync();

        // 初始化设备扩展数据
        await SeedDevicesExtendAsync();
    }

    private async Task SeedUsersAsync()
    {
        if (!await _db.Queryable<LeanUser>().AnyAsync())
        {
            var admin = new LeanUser
            {
                UserName = "admin",
                Password = "123456", // 实际应用中应该加密
                RealName = "系统管理员",
                Status = LeanUserStatus.Enabled,
                UserType = LeanUserType.SuperAdmin,
                Email = "admin@lean365.com",
                Mobile = "13800138000"
            };
            await _db.Insertable(admin).ExecuteCommandAsync();
        }
    }

    private async Task SeedDepartmentsAsync()
    {
        if (!await _db.Queryable<LeanDepartment>().AnyAsync())
        {
            var rootDept = new LeanDepartment
            {
                DepartmentName = "Lean365科技",
                ParentId = 0,
                OrderNum = 1,
                Status = LeanStatus.Enabled,
                DepartmentType = LeanDepartmentType.Company
            };
            await _db.Insertable(rootDept).ExecuteCommandAsync();
        }
    }

    private async Task SeedRolesAsync()
    {
        if (!await _db.Queryable<LeanRole>().AnyAsync())
        {
            var adminRole = new LeanRole
            {
                RoleName = "超级管理员",
                RoleCode = "admin",
                Status = LeanStatus.Enabled,
                RoleType = LeanRoleType.System,
                OrderNum = 1
            };
            await _db.Insertable(adminRole).ExecuteCommandAsync();
        }
    }

    private async Task SeedMenusAsync()
    {
        if (!await _db.Queryable<LeanMenu>().AnyAsync())
        {
            var systemMenu = new LeanMenu
            {
                MenuName = "系统管理",
                MenuCode = "system",
                Path = "/system",
                Component = "Layout",
                IsCache = 0,
                IsFrame = 0,
                MenuType = LeanMenuType.Directory,
                Visible = 1,
                Status = LeanStatus.Enabled,
                Icon = "system"
            };
            await _db.Insertable(systemMenu).ExecuteCommandAsync();
        }
    }

    private async Task SeedPermissionsAsync()
    {
        if (!await _db.Queryable<LeanPermission>().AnyAsync())
        {
            var permission = new LeanPermission
            {
                PermissionName = "系统管理",
                PermissionCode = "system",
                Type = LeanPermissionType.Menu,
                Status = LeanStatus.Enabled
            };
            await _db.Insertable(permission).ExecuteCommandAsync();
        }
    }

    private async Task SeedGeneratorTemplatesAsync()
    {
        if (!await _db.Queryable<LeanTemplate>().AnyAsync())
        {
            var template = new LeanTemplate
            {
                TemplateType = "csharp",
                FileName = "Default.cs",
                TemplateContent = "// 默认C#模板内容",
                Remark = "默认C#模板"
            };
            await _db.Insertable(template).ExecuteCommandAsync();
        }
    }

    private async Task SeedWorkflowDefinitionsAsync()
    {
        if (!await _db.Queryable<LeanWorkflowDefinition>().AnyAsync())
        {
            var workflowDefinition = new LeanWorkflowDefinition
            {
                Name = "默认工作流",
                Description = "默认工作流描述",
                Definition = "{}",
                Status = (WorkflowStatus)LeanStatus.Enabled
            };
            await _db.Insertable(workflowDefinition).ExecuteCommandAsync();
        }
    }

    private async Task SeedTableDesignerAsync()
    {
        if (!await _db.Queryable<LeanTableDesigner>().AnyAsync())
        {
            var designer = new LeanTableDesigner
            {
                TableName = "demo_table",
                TableDesc = "示例表",
                EntityName = "DemoTable",
                Author = "Lean365",
                Template = "default",
                FrontendTemplate = "default",
                NamespacePrefix = "Lean.Cus",
                ModuleName = "Demo",
                BusinessName = "demo",
                FunctionName = "示例",
                ParentMenuId = 0,
                OrderField = "Id",
                OrderType = 0,
                UseSnowflakeId = 1,
                GenerateType = 0,
                IsMultiLevel = 0,
                IsGenerateRepository = 1,
                IsGenerateSqlLog = 1,
                PermissionPrefix = "demo",
                ButtonStyle = 0,
                Functions = "add,edit,delete,export,view,import"
            };
            await _db.Insertable(designer).ExecuteCommandAsync();
        }
    }

    private async Task SeedTableImportAsync()
    {
        if (!await _db.Queryable<LeanTableImport>().AnyAsync())
        {
            var import = new LeanTableImport
            {
                TableName = "demo_import",
                TableDesc = "示例导入表",
                EntityName = "DemoImport",
                Author = "Lean365",
                Template = "default",
                FrontendTemplate = "default",
                NamespacePrefix = "Lean.Cus",
                ModuleName = "Demo",
                BusinessName = "demoImport",
                FunctionName = "示例导入",
                ParentMenuId = 0,
                OrderField = "Id",
                OrderType = 0,
                UseSnowflakeId = 1,
                GenerateType = 0,
                IsMultiLevel = 0,
                IsGenerateRepository = 1,
                IsGenerateSqlLog = 1,
                PermissionPrefix = "demoImport",
                ButtonStyle = 0,
                Functions = "add,edit,delete,export,view,import"
            };
            await _db.Insertable(import).ExecuteCommandAsync();
        }
    }

    private async Task SeedUserExtendAsync()
    {
        if (!await _db.Queryable<LeanUserExtend>().AnyAsync())
        {
            var userExtend = new LeanUserExtend
            {
                UserId = 1, // admin用户的ID
                FirstLoginTime = DateTime.Now,
                FirstLoginIp = "127.0.0.1",
                FirstLoginDevice = "PC",
                FirstLoginLocation = "内网IP",
                FirstLoginBrowser = "Chrome",
                FirstLoginOs = "Windows",
                LastLoginTime = DateTime.Now,
                LastLoginIp = "127.0.0.1",
                LastLoginDevice = "PC",
                LastLoginLocation = "内网IP",
                LastLoginBrowser = "Chrome",
                LastLoginOs = "Windows"
            };
            await _db.Insertable(userExtend).ExecuteCommandAsync();
        }
    }

    private async Task SeedDevicesExtendAsync()
    {
        if (!await _db.Queryable<LeanDevicesExtend>().AnyAsync())
        {
            var deviceExtend = new LeanDevicesExtend
            {
                DeviceId = "PC_001",
                DeviceName = "办公电脑",
                DeviceType = LeanDeviceType.PC,
                OperatingSystem = "Windows",
                Browser = "Chrome",
                IpAddress = "127.0.0.1",
                Location = "内网IP",
                LastOnlineTime = DateTime.Now,
                OnlineStatus = LeanOnlineStatus.Online,
                UserId = 1 // admin用户的ID
            };
            await _db.Insertable(deviceExtend).ExecuteCommandAsync();
        }
    }
}
//===================================================
// 项目名称：Lean.Cus.Api
// 文件名称：Program
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：程序入口
//===================================================

using System.Text;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Lean.Cus.Domain.Interfaces;
using Lean.Cus.Infrastructure.Authentication;
using Lean.Cus.Api.Authorization;
using Lean.Cus.Api.Extensions;
using Lean.Cus.Api.Filters;
using Lean.Cus.Api.Middlewares;
using Lean.Cus.Domain.Configurations;
using Lean.Cus.Domain.IRepositories.Chat;
using Lean.Cus.Infrastructure.Repositories.Chat;
using Lean.Cus.Api.Hubs;
using SqlSugar;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.SignalR;
using Lean.Cus.Infrastructure.SqlSugar;
using Lean.Cus.Application.Services.Auth;
using Lean.Cus.Domain.IRepositories.Monitor;
using Lean.Cus.Infrastructure.Repositories.Monitor;
using Swashbuckle.AspNetCore.SwaggerGen;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("init main");

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add encoding providers
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

    // 添加服务到容器
    builder.Services.AddControllers(options =>
    {
        // 添加操作日志过滤器
        options.Filters.Add<LeanOperLogFilter>();
    });
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSignalR();

    // 配置 Swagger
    builder.Services.AddSwaggerGen(c =>
    {
        // Admin 分组
        c.SwaggerDoc("admin", new OpenApiInfo 
        { 
            Title = "Lean.Cus Admin API", 
            Version = "v1",
            Description = "管理接口"
        });

        // Monitor 分组
        c.SwaggerDoc("monitor", new OpenApiInfo 
        { 
            Title = "Lean.Cus Monitor API", 
            Version = "v1",
            Description = "监控接口"
        });

        // Chat 分组
        c.SwaggerDoc("chat", new OpenApiInfo 
        { 
            Title = "Lean.Cus Chat API", 
            Version = "v1",
            Description = "聊天接口"
        });
        
        // 添加 XML 注释
        var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);

        // 按目录分组
        c.DocInclusionPredicate((docName, apiDesc) =>
        {
            if (!apiDesc.TryGetMethodInfo(out var methodInfo)) return false;
            
            var controllerNamespace = methodInfo.DeclaringType?.Namespace ?? string.Empty;
            var group = controllerNamespace.Split('.').LastOrDefault()?.ToLower() ?? string.Empty;
            
            return docName.Equals(group, StringComparison.OrdinalIgnoreCase);
        });
    });

    // 配置数据库
    try
    {
        logger.Debug("Configuring database...");
        LeanDbContext.Configure(builder.Services, builder.Configuration);
        logger.Debug("Database configured successfully.");
    }
    catch (Exception ex)
    {
        logger.Error(ex, "Error configuring database");
        throw;
    }

    // 注册仓储
    builder.Services.AddScoped<IOnlineUserRepository, OnlineUserRepository>();
    builder.Services.AddScoped<IMessageRepository, MessageRepository>();
    builder.Services.AddScoped<ILeanLoginLogRepository, LeanLoginLogRepository>();

    // 注册认证服务
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddScoped<ILeanAuthService, LeanAuthService>();

    // 配置授权
    builder.Services.AddAuthorization(options =>
    {
        // 基于角色的策略
        options.AddPolicy("LeanPolicy", policy =>
        {
            policy.Requirements.Add(new LeanRequirement());
        });

        // 基于权限的策略 - 动态添加
        options.AddPolicy("Permission", policy =>
        {
            policy.Requirements.Add(new LeanPermissionRequirement(string.Empty));
        });
    });

    // 注册授权处理器
    builder.Services.AddScoped<IAuthorizationHandler, LeanAuthorizationHandler>();
    builder.Services.AddScoped<IAuthorizationHandler, LeanPermissionHandler>();

    // 配置 CORS
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
    });

    var app = builder.Build();

    // 初始化数据库
    try
    {
        logger.Debug("Initializing database...");
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<LeanDbContext>();
            dbContext.InitDatabase();
        }
        logger.Debug("Database initialized successfully.");
    }
    catch (Exception ex)
    {
        logger.Error(ex, "Error initializing database");
        throw;
    }

    // 配置HTTP请求管道
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/admin/swagger.json", "管理接口");
            c.SwaggerEndpoint("/swagger/monitor/swagger.json", "监控接口");
            c.SwaggerEndpoint("/swagger/chat/swagger.json", "聊天接口");
            // c.RoutePrefix = string.Empty; // 注释掉这行，使用默认的 swagger 路由
        });
    }

    app.UseCors("AllowAll");
    app.UseHttpsRedirection();
    app.UseRouting(); // 添加路由中间件
    app.UseAuthorization();

    // 添加全局���常处理中间件
    app.UseMiddleware<LeanGlobalExceptionMiddleware>();

    app.MapControllers();
    app.MapHub<ChatHub>("/chatHub");

    // 启动应用程序
    logger.Debug("Starting application...");
    app.Run();
    logger.Debug("Application started successfully.");
}
catch (Exception ex)
{
    logger.Error(ex, "Application startup failed");
    throw;
}

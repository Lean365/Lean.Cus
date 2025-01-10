using Lean.Cus.Api.Extensions;
using Lean.Cus.Api.Configurations;
using Lean.Cus.Infrastructure.Data.Configurations;
using Lean.Cus.Infrastructure.Filters;
using Lean.Cus.Common.Exceptions;
using Lean.Cus.Common.Logging;
using NLog;
using NLog.Web;
using SqlSugar;
using Lean.Cus.Generator.Services.Designer;
using Lean.Cus.Generator.Services.Generator;

// 初始化NLog
var logger = LogManager.Setup()
    .LoadConfigurationFromAppSettings()
    .GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // 配置NLog
    builder.Host.AddLeanNLog();

    // 注册服务
    builder.Services
        .AddControllers(options =>
        {
            // 添加全局过滤器
            options.Filters.Add<LeanSqlInjectionFilterAttribute>();
            options.Filters.Add<LeanAntiForgeryFilterAttribute>();
            options.Filters.Add<LeanRateLimitFilterAttribute>();
            options.Filters.Add<LeanDataPermissionFilterAttribute>();
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = null;
            options.JsonSerializerOptions.DictionaryKeyPolicy = null;
        });

    // 注册Swagger服务
    builder.Services.AddLeanSwagger();

    // 注册SqlSugar服务
    builder.Services.AddLeanSqlSugar(builder.Configuration);

    // 注册应用服务
    builder.Services.AddLeanServices();

    // 注册跨域服务
    builder.Services.AddLeanCors(builder.Configuration);

    // 注册认证服务
    builder.Services.AddLeanAuthentication(builder.Configuration);

    // 注册授权服务
    builder.Services.AddLeanAuthorization();

    // 注册缓存服务
    builder.Services.AddLeanCache(builder.Configuration);

    // 注册代码生成器服务
    builder.Services.AddLeanGenerator();

    // 注册工作流服务
    builder.Services.AddLeanWorkflow();

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // 初始化数据库
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<ISqlSugarClient>();
        logger.Info("正在初始化数据库...");
    }

    // 配置HTTP请求管道
    if (app.Environment.IsDevelopment())
    {
        app.UseLeanSwagger();
    }

    // 全局异常处理
    app.UseLeanExceptionHandler();

    // 启用跨域
    app.UseCors("LeanCorsPolicy");

    // 启用HTTPS重定向
    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    // 启用认证
    app.UseAuthentication();

    // 启用授权
    app.UseAuthorization();

    // 启用路由
    app.MapControllers();

    // 启动应用
    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "程序启动失败");
    throw;
}
finally
{
    LogManager.Shutdown();
} 
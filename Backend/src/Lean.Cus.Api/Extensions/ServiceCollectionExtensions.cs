using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Application.Services.Admin;
using Lean.Cus.Domain.IRepositories;
using Lean.Cus.Infrastructure.Repositories;
using Lean.Cus.Common.Configs;

namespace Lean.Cus.Api.Extensions;

/// <summary>
/// 服务集合扩展类
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// 注册应用服务
    /// </summary>
    public static IServiceCollection AddLeanServices(this IServiceCollection services)
    {
        // 注册仓储
        services.AddScoped(typeof(ILeanRepository<>), typeof(LeanRepository<>));

        // 注册应用服务
        services.AddScoped<ILeanUserService, LeanUserService>();
        services.AddScoped<ILeanRoleService, LeanRoleService>();
        services.AddScoped<ILeanPositionService, LeanPositionService>();
        // TODO: 注册其他服务...

        return services;
    }

    /// <summary>
    /// 注册跨域服务
    /// </summary>
    public static IServiceCollection AddLeanCors(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("LeanCorsPolicy", builder =>
            {
                builder.WithOrigins(configuration.GetSection("Cors:Origins").Get<string[]>() ?? Array.Empty<string>())
                       .AllowAnyMethod()
                       .AllowAnyHeader()
                       .AllowCredentials();
            });
        });

        return services;
    }

    /// <summary>
    /// 注册认证服务
    /// </summary>
    public static IServiceCollection AddLeanAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfig = configuration.GetSection("Jwt").Get<LeanJwtConfig>();
        if (jwtConfig == null)
            throw new ArgumentNullException(nameof(jwtConfig), "JWT配置不能为空");

        services.Configure<LeanJwtConfig>(configuration.GetSection("Jwt"));

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecretKey)),
                ValidateIssuer = true,
                ValidIssuer = jwtConfig.Issuer,
                ValidateAudience = true,
                ValidAudience = jwtConfig.Audience,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        return services;
    }

    /// <summary>
    /// 注册授权服务
    /// </summary>
    public static IServiceCollection AddLeanAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            // 添加策略
            options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            options.AddPolicy("UserOnly", policy => policy.RequireRole("User"));
        });

        return services;
    }

    /// <summary>
    /// 注册缓存服务
    /// </summary>
    public static IServiceCollection AddLeanCache(this IServiceCollection services, IConfiguration configuration)
    {
        // TODO: 配置Redis缓存
        return services;
    }
} 
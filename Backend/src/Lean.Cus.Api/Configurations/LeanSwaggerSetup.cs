using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Lean.Cus.Api.Configurations;

/// <summary>
/// Swagger配置
/// </summary>
public static class LeanSwaggerSetup
{
    /// <summary>
    /// 添加Swagger服务
    /// </summary>
    public static void AddLeanSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Lean.Cus API",
                Version = "v1",
                Description = "Lean.Cus API 接口文档",
                Contact = new OpenApiContact
                {
                    Name = "Lean365",
                    Email = "support@lean365.com",
                    Url = new Uri("https://www.lean365.com")
                }
            });

            // 添加JWT认证
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization header using the Bearer scheme.",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }
            });

            // 添加XML注释
            var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var xmlFiles = Directory.GetFiles(baseDirectory, "Lean.Cus.*.xml");
            foreach (var xmlFile in xmlFiles)
            {
                c.IncludeXmlComments(xmlFile, true);
            }
        });
    }

    /// <summary>
    /// 使用Swagger服务
    /// </summary>
    public static void UseLeanSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Lean.Cus API v1");
            c.RoutePrefix = "swagger";
            c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            c.DefaultModelsExpandDepth(-1); // 隐藏Models
            c.DefaultModelRendering(Swashbuckle.AspNetCore.SwaggerUI.ModelRendering.Model);
        });
    }
} 
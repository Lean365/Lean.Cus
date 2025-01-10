using System;
using System.Threading.Tasks;
using SqlSugar;
using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Infrastructure.Data.Seeds;

/// <summary>
/// 数据种子贡献者
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
        // 检查是否需要初始化
        if (await _db.Queryable<LeanUser>().AnyAsync())
        {
            return;
        }

        // 初始化管理员用户
        var admin = new LeanUser
        {
            UserName = "admin",
            Password = "123456", // 注意：实际应用中应该使用加密后的密码
            RealName = "系统管理员",
            Email = "admin@lean365.com",
            Mobile = "13800138000",
            Status = LeanUserStatus.Enabled,
            CreateTime = DateTime.Now,
            UpdateTime = DateTime.Now,
            CreateBy = "0",
            UpdateBy = "0"
        };

        await _db.Insertable(admin).ExecuteCommandAsync();
    }
} 
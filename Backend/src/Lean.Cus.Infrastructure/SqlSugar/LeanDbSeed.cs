//===================================================
// 项目名称：Lean.Cus.Infrastructure.SqlSugar
// 文件名称：LeanDbSeed
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：数据库种子数据
//===================================================

using Lean.Cus.Common.Security;
using Lean.Cus.Domain.Entities.Admin;
using SqlSugar;

namespace Lean.Cus.Infrastructure.SqlSugar;

/// <summary>
/// 数据库种子数据
/// </summary>
public class LeanDbSeed
{
    private readonly ISqlSugarClient _db;

    public LeanDbSeed(ISqlSugarClient db)
    {
        _db = db;
    }

    /// <summary>
    /// 初始化数据
    /// </summary>
    public void Initialize()
    {
        // 初始化超级管理员
        InitSuperAdmin();
    }

    /// <summary>
    /// 初始化超级管理员
    /// </summary>
    private void InitSuperAdmin()
    {
        var exists = _db.Queryable<LeanSysUser>()
            .Any(u => u.UserType == 0); // 0=超级管理员

        if (!exists)
        {
            var (hash, salt) = LeanSecurityHelper.CreatePasswordHash("admin123");

            var superAdmin = new LeanSysUser
            {
                Id = 1,
                UserCode = "admin",
                UserName = "admin",
                NickName = "超级管理员",
                UserType = 0, // 超级管理员
                Email = "admin@lean.com",
                Gender = 0,
                Password = hash,
                Salt = salt,
                IsDeleted = 0,
                Remark = "Lean365,不可删除",
                Status = 1, // 启用
                CreateId = 1,
                CreateBy = "admin",
                CreateTime = DateTime.Now
            };

            _db.Insertable(superAdmin).ExecuteCommand();
        }
    }
} 
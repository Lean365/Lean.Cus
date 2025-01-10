using SqlSugar;
using System;

namespace Lean.Cus.Infrastructure.Data.Configurations;

/// <summary>
/// 实体配置基类
/// </summary>
public abstract class LeanEntityTypeConfiguration
{
    /// <summary>
    /// 配置实体
    /// </summary>
    /// <param name="entityInfo">实体信息</param>
    public abstract void Configure(EntityInfo entityInfo);

    /// <summary>
    /// 配置审计字段
    /// </summary>
    protected void ConfigureAuditFields(EntityInfo entityInfo)
    {
        // 创建时间
        if (entityInfo.Type.GetProperty("CreateTime") != null)
        {
            entityInfo.Columns.Find(it => it.PropertyName == "CreateTime").DefaultValue = "CURRENT_TIMESTAMP";
        }

        // 更新时间
        if (entityInfo.Type.GetProperty("UpdateTime") != null)
        {
            entityInfo.Columns.Find(it => it.PropertyName == "UpdateTime").DefaultValue = "CURRENT_TIMESTAMP";
        }

        // 创建者ID
        if (entityInfo.Type.GetProperty("CreateBy") != null)
        {
            entityInfo.Columns.Find(it => it.PropertyName == "CreateBy").DefaultValue = "0";
        }

        // 更新者ID
        if (entityInfo.Type.GetProperty("UpdateBy") != null)
        {
            entityInfo.Columns.Find(it => it.PropertyName == "UpdateBy").DefaultValue = "0";
        }
    }
} 
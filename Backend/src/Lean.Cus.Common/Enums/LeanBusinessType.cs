using System.ComponentModel;

namespace Lean.Cus.Common.Enums;

/// <summary>
/// 业务操作类型枚举
/// </summary>
public enum LeanBusinessType
{
    /// <summary>
    /// 其他
    /// </summary>
    [Description("其他")]
    Other = 0,

    /// <summary>
    /// 新增
    /// </summary>
    [Description("新增")]
    Insert = 1,

    /// <summary>
    /// 修改
    /// </summary>
    [Description("修改")]
    Update = 2,

    /// <summary>
    /// 删除
    /// </summary>
    [Description("删除")]
    Delete = 3,

    /// <summary>
    /// 授权
    /// </summary>
    [Description("授权")]
    Grant = 4,

    /// <summary>
    /// 导出
    /// </summary>
    [Description("导出")]
    Export = 5,

    /// <summary>
    /// 导入
    /// </summary>
    [Description("导入")]
    Import = 6,

    /// <summary>
    /// 强制退出
    /// </summary>
    [Description("强制退出")]
    ForceLogout = 7,

    /// <summary>
    /// 生成代码
    /// </summary>
    [Description("生成代码")]
    CodeGenerate = 8,

    /// <summary>
    /// 清空数据
    /// </summary>
    [Description("清空数据")]
    Clean = 9,

    /// <summary>
    /// 审核
    /// </summary>
    [Description("审核")]
    Audit = 10,

    /// <summary>
    /// 解锁
    /// </summary>
    [Description("解锁")]
    Unlock = 11
} 
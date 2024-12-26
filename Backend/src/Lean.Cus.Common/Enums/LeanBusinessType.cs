//===================================================
// 项目名称：Lean.Cus.Common.Enums
// 文件名称：LeanBusinessTypeEnum
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：业务操作类型枚举
//===================================================

namespace Lean.Cus.Common.Enums;

/// <summary>
/// 业务操作类型枚举
/// </summary>
public enum LeanBusinessTypeEnum
{
    /// <summary>
    /// 其他
    /// </summary>
    Other = 0,

    /// <summary>
    /// 新建
    /// <para>创建新的数据记录</para>
    /// </summary>
    Create = 1,

    /// <summary>
    /// 插入
    /// <para>插入数据记录</para>
    /// </summary>
    Insert = 2,

    /// <summary>
    /// 添加
    /// <para>添加数据记录</para>
    /// </summary>
    Add = 3,

    /// <summary>
    /// 修改
    /// <para>更新已有的数据记录</para>
    /// </summary>
    Update = 4,

    /// <summary>
    /// 删除
    /// <para>删除已有的数据记录</para>
    /// </summary>
    Delete = 5,

    /// <summary>
    /// 导入
    /// <para>从外部导入数据</para>
    /// </summary>
    Import = 6,

    /// <summary>
    /// 导出
    /// <para>将数据导出到外部</para>
    /// </summary>
    Export = 7,

    /// <summary>
    /// 打印
    /// <para>打印数据或报表</para>
    /// </summary>
    Print = 8,

    /// <summary>
    /// 查询
    /// <para>查询或检索数据</para>
    /// </summary>
    Query = 9,

    /// <summary>
    /// 生成
    /// <para>生成数据或文档</para>
    /// </summary>
    Generate = 10,

    /// <summary>
    /// 上传
    /// <para>上传文件或数据</para>
    /// </summary>
    Upload = 11,

    /// <summary>
    /// 下载
    /// <para>下载文件或数据</para>
    /// </summary>
    Download = 12,

    /// <summary>
    /// 审核
    /// <para>审核业务数据</para>
    /// </summary>
    Audit = 13,

    /// <summary>
    /// 驳回
    /// <para>驳回业务数据</para>
    /// </summary>
    Reject = 14,

    /// <summary>
    /// 提交
    /// <para>提交业务数据</para>
    /// </summary>
    Submit = 15,

    /// <summary>
    /// 确认
    /// <para>确认业务操作</para>
    /// </summary>
    Confirm = 16,

    /// <summary>
    /// 取消
    /// <para>取消业务操作</para>
    /// </summary>
    Cancel = 17,

    /// <summary>
    /// 授权
    /// <para>授予权限或角色</para>
    /// </summary>
    Grant = 18,

    /// <summary>
    /// 取消授权
    /// <para>撤销权限或角色</para>
    /// </summary>
    Revoke = 19,

    /// <summary>
    /// 启用
    /// <para>启用数据或功能</para>
    /// </summary>
    Enable = 20,

    /// <summary>
    /// 禁用
    /// <para>禁用数据或功能</para>
    /// </summary>
    Disable = 21,

    /// <summary>
    /// 登录
    /// <para>用户登录系统</para>
    /// </summary>
    Login = 22,

    /// <summary>
    /// 登出
    /// <para>用户退出系统</para>
    /// </summary>
    Logout = 23,

    /// <summary>
    /// 重置密码
    /// <para>重置用户密码</para>
    /// </summary>
    ResetPassword = 24,

    /// <summary>
    /// 修改密码
    /// <para>修改用户密码</para>
    /// </summary>
    ChangePassword = 25,

    /// <summary>
    /// 刷新令牌
    /// <para>刷新访问令牌</para>
    /// </summary>
    RefreshToken = 26,

    /// <summary>
    /// 清理
    /// <para>清理数据或缓存</para>
    /// </summary>
    Clean = 27,

    /// <summary>
    /// 备份
    /// <para>备份数据或文件</para>
    /// </summary>
    Backup = 28,

    /// <summary>
    /// 还���
    /// <para>还原数据或文件</para>
    /// </summary>
    Restore = 29,

    /// <summary>
    /// 同步
    /// <para>同步数据或状态</para>
    /// </summary>
    Sync = 30,

    /// <summary>
    /// 归档
    /// <para>归档数据或文件</para>
    /// </summary>
    Archive = 31,

    /// <summary>
    /// 锁定
    /// <para>锁定数据或账号</para>
    /// </summary>
    Lock = 32,

    /// <summary>
    /// 解锁
    /// <para>解锁数据或账号</para>
    /// </summary>
    Unlock = 33,

    /// <summary>
    /// 合并
    /// <para>合并数据或文件</para>
    /// </summary>
    Merge = 34,

    /// <summary>
    /// 分离
    /// <para>分离数据或文件</para>
    /// </summary>
    Split = 35,

    /// <summary>
    /// 复制
    /// <para>复制数据或文件</para>
    /// </summary>
    Copy = 36,

    /// <summary>
    /// 移动
    /// <para>移动数据或文件</para>
    /// </summary>
    Move = 37,

    /// <summary>
    /// 重命名
    /// <para>重命名数据或文件</para>
    /// </summary>
    Rename = 38
} 
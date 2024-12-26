using SqlSugar;

namespace Lean.Cus.Domain.Entities.Chat;

/// <summary>
/// 消息实体
/// </summary>
[SugarTable("Lean_message", "消息表")]
public class LeanMessage : LeanLongEntity
{
    /// <summary>
    /// 发送者ID
    /// </summary>
    [SugarColumn(ColumnDescription = "发送者ID")]
    public long SenderId { get; set; }

    /// <summary>
    /// 发送者名称
    /// </summary>
    [SugarColumn(ColumnDescription = "发送者名称")]
    public string SenderName { get; set; } = string.Empty;

    /// <summary>
    /// 接收者ID（0表示群发）
    /// </summary>
    [SugarColumn(ColumnDescription = "接收者ID（0表示群发）")]
    public long ReceiverId { get; set; }

    /// <summary>
    /// 接收者名称
    /// </summary>
    [SugarColumn(ColumnDescription = "接收者名称")]
    public string ReceiverName { get; set; } = string.Empty;

    /// <summary>
    /// 消息内容
    /// </summary>
    [SugarColumn(ColumnDescription = "消息内容")]
    public string Content { get; set; } = string.Empty;

    /// <summary>
    /// 消息类型（1：文本，2：图��，3：文件）
    /// </summary>
    [SugarColumn(ColumnDescription = "消息类型（1：文本，2：图片，3：文件）")]
    public int Type { get; set; }

    /// <summary>
    /// 消息状态（0：未读，1：已读）
    /// </summary>
    [SugarColumn(ColumnDescription = "消息状态（0：未读，1：已读）")]
    public int Status { get; set; }
} 
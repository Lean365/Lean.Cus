using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 菜单权限关联表
/// </summary>
[Table("lean_admin_menu_permission")]
public class LeanMenuPermission : LeanBaseEntity
{
    /// <summary>
    /// 菜单ID
    /// </summary>
    [Required]
    [Column("menu_id")]
    public long MenuId { get; set; }

    /// <summary>
    /// 权限ID
    /// </summary>
    [Required]
    [Column("permission_id")]
    public long PermissionId { get; set; }
} 
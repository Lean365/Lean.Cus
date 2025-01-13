using SqlSugar;

namespace Lean.Cus.Domain.Entities.Admin;

/// <summary>
/// 用户部门关联表
/// </summary>
[SugarTable("lean_admin_user_department", "用户部门关联表")]
[SugarIndex("IX_LeanUserDepartment", nameof(UserId), OrderByType.Asc, nameof(DepartmentId), OrderByType.Asc, true)]
public class LeanUserDepartment : LeanBaseEntity
{
    /// <summary>
    /// 用户ID
    /// </summary>
    [SugarColumn(ColumnName = "user_id", ColumnDescription = "用户ID")]
    public long UserId { get; set; }

    /// <summary>
    /// 部门ID
    /// </summary>
    [SugarColumn(ColumnName = "department_id", ColumnDescription = "部门ID")]
    public long DepartmentId { get; set; }
} 
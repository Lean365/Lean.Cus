//===================================================
// 项目名称：Lean.Cus.Common.Enums
// 文件名称：LeanResultCodeEnum
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：结果代码枚举
//===================================================

namespace Lean.Cus.Common.Enums;

/// <summary>
/// 结果代码枚举
/// <para>定义系统中所有业务操作的结果代码</para>
/// <para>用于标识业务操作的处理结果</para>
/// </summary>
public enum LeanResultCodeEnum
{
    /// <summary>
    /// 成功
    /// <para>操作成功完成</para>
    /// </summary>
    Success = 200,

    /// <summary>
    /// 失败
    /// <para>操作未能完成</para>
    /// </summary>
    Fail = 400,

    /// <summary>
    /// 未授权
    /// <para>用户未登录或登录已过期</para>
    /// </summary>
    Unauthorized = 401,

    /// <summary>
    /// 禁止访问
    /// <para>用户无权限执行此操作</para>
    /// </summary>
    Forbidden = 403,

    /// <summary>
    /// 未找到
    /// <para>请求的资源不存���</para>
    /// </summary>
    NotFound = 404,

    /// <summary>
    /// 参数错误
    /// <para>请求参数验证失败</para>
    /// </summary>
    InvalidParameter = 422,

    /// <summary>
    /// 服务器错误
    /// <para>服务器内部错误</para>
    /// </summary>
    Error = 500,

    /// <summary>
    /// 服务不可用
    /// <para>服务暂时不可用</para>
    /// </summary>
    ServiceUnavailable = 503,

    /// <summary>
    /// 数据已存在
    /// <para>要创建的数据已经存在</para>
    /// </summary>
    DataExists = 600,

    /// <summary>
    /// 数据不存在
    /// <para>要操作的数据不存在</para>
    /// </summary>
    DataNotExists = 601,

    /// <summary>
    /// 数据已过期
    /// <para>数据已经过期或失效</para>
    /// </summary>
    DataExpired = 602,

    /// <summary>
    /// 数据冲突
    /// <para>数据存在冲突无法操作</para>
    /// </summary>
    DataConflict = 603,

    /// <summary>
    /// 数据已锁定
    /// <para>数据已被锁定无法操作</para>
    /// </summary>
    DataLocked = 604,

    /// <summary>
    /// 数据已删除
    /// <para>数据已经被删除</para>
    /// </summary>
    DataDeleted = 605,

    /// <summary>
    /// 数据已审核
    /// <para>数据已经审��通过</para>
    /// </summary>
    DataAudited = 606,

    /// <summary>
    /// 数据未审核
    /// <para>数据尚未审核通过</para>
    /// </summary>
    DataUnaudited = 607,

    /// <summary>
    /// 数据已提交
    /// <para>数据已经提交审核</para>
    /// </summary>
    DataSubmitted = 608,

    /// <summary>
    /// 数据未提交
    /// <para>数据尚未提交审核</para>
    /// </summary>
    DataUnsubmitted = 609,

    /// <summary>
    /// 数据已发布
    /// <para>数据已经发布</para>
    /// </summary>
    DataPublished = 610,

    /// <summary>
    /// 数据未发布
    /// <para>数据尚未发布</para>
    /// </summary>
    DataUnpublished = 611,

    /// <summary>
    /// 数据已归档
    /// <para>数据已经归档</para>
    /// </summary>
    DataArchived = 612,

    /// <summary>
    /// 数据未归档
    /// <para>数据尚未归档</para>
    /// </summary>
    DataUnarchived = 613,

    /// <summary>
    /// 数据已禁用
    /// <para>数据已经被禁用</para>
    /// </summary>
    DataDisabled = 614,

    /// <summary>
    /// 数据未禁用
    /// <para>数据未被禁用</para>
    /// </summary>
    DataEnabled = 615,

    /// <summary>
    /// 数据已完成
    /// <para>数据处理已完成</para>
    /// </summary>
    DataCompleted = 616,

    /// <summary>
    /// 数据未完成
    /// <para>数据处理未完成</para>
    /// </summary>
    DataUncompleted = 617,

    /// <summary>
    /// 数据处理中
    /// <para>数据正在处理中</para>
    /// </summary>
    DataProcessing = 618,

    /// <summary>
    /// 数据处理失败
    /// <para>数据处理失败</para>
    /// </summary>
    DataProcessFailed = 619,

    /// <summary>
    /// 数据验证失败
    /// <para>数据验证未通过</para>
    /// </summary>
    DataValidationFailed = 620,

    /// <summary>
    /// 数据格式错误
    /// <para>数据格式不正确</para>
    /// </summary>
    DataFormatError = 621,

    /// <summary>
    /// 数据关联错误
    /// <para>数据关联关系错误</para>
    /// </summary>
    DataRelationError = 622,

    /// <summary>
    /// 数据权限错误
    /// <para>没有操作数据的权限</para>
    /// </summary>
    DataPermissionError = 623,

    /// <summary>
    /// 数据状态错误
    /// <para>数据状态不正确</para>
    /// </summary>
    DataStatusError = 624,

    /// <summary>
    /// 数据类型错误
    /// <para>数据类型不正确</para>
    /// </summary>
    DataTypeError = 625,

    /// <summary>
    /// 数据范围错误
    /// <para>数据超出允许的范围</para>
    /// </summary>
    DataRangeError = 626,

    /// <summary>
    /// 数据重复错误
    /// <para>数据存在重复</para>
    /// </summary>
    DataDuplicateError = 627,

    /// <summary>
    /// 数据依赖错误
    /// <para>数据存在依赖关系</para>
    /// </summary>
    DataDependencyError = 628,

    /// <summary>
    /// 数据完整性错误
    /// <para>数据完整性校验失败</para>
    /// </summary>
    DataIntegrityError = 629,

    /// <summary>
    /// 数据一致性错误
    /// <para>数据一致性校验失败</para>
    /// </summary>
    DataConsistencyError = 630
} 
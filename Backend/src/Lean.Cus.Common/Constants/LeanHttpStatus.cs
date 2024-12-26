//===================================================
// 项目名称：Lean.Cus.Common.Constants
// 文件名称：LeanHttpStatus
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：HTTP状态码常量
//===================================================

namespace Lean.Cus.Common.Constants;

/// <summary>
/// HTTP状态码常量
/// <para>定义常用的HTTP状态码</para>
/// <para>用于统一API响应的状态码</para>
/// </summary>
public static class LeanHttpStatus
{
    /// <summary>
    /// 成功 (200)
    /// <para>请求已成功，请求所希望的响应头或数据体将随此响应返回</para>
    /// </summary>
    public const int OK = 200;

    /// <summary>
    /// 已创建 (201)
    /// <para>请求已经被实现，而且有一个新的资源已经依据请求的需要而创建</para>
    /// </summary>
    public const int Created = 201;

    /// <summary>
    /// 已接受 (202)
    /// <para>服务器已接受请求，但尚未处理。最终该请求可能会也可能不会被执行</para>
    /// </summary>
    public const int Accepted = 202;

    /// <summary>
    /// 无内容 (204)
    /// <para>服务器成功处理了请求，但不需要返回任何实体内容</para>
    /// </summary>
    public const int NoContent = 204;

    /// <summary>
    /// 错误请求 (400)
    /// <para>由于明显的客户端错误，服务器不能或不会处理该请求</para>
    /// </summary>
    public const int BadRequest = 400;

    /// <summary>
    /// 未授权 (401)
    /// <para>当前请求需要用户验证。该响应必须包含一个适用于被请求资源的WWW-Authenticate信息头</para>
    /// </summary>
    public const int Unauthorized = 401;

    /// <summary>
    /// 禁止访问 (403)
    /// <para>服务器已经理解请求，但是拒绝执行它。与401响应不同的是，身份验证并不能提供任何帮助</para>
    /// </summary>
    public const int Forbidden = 403;

    /// <summary>
    /// 未找到 (404)
    /// <para>请求失败，请求所希望得到的资源未被在服务器上发现</para>
    /// </summary>
    public const int NotFound = 404;

    /// <summary>
    /// 方法不允许 (405)
    /// <para>请求行中指定的请求方法不能被用于请求相应的资源</para>
    /// </summary>
    public const int MethodNotAllowed = 405;

    /// <summary>
    /// 不可接受 (406)
    /// <para>请求的资源的内容特性无法满足请求头中的条件，因而无法生成响应实体</para>
    /// </summary>
    public const int NotAcceptable = 406;

    /// <summary>
    /// 请求超时 (408)
    /// <para>请求超时。客户端没有在服务器预备等待的时间内完成一个请求的发送</para>
    /// </summary>
    public const int RequestTimeout = 408;

    /// <summary>
    /// 冲突 (409)
    /// <para>由于和被请求的资源的当前状态之间存在冲突，请求无法完成</para>
    /// </summary>
    public const int Conflict = 409;

    /// <summary>
    /// 服务器错误 (500)
    /// <para>服务器遇到了一个未曾预料的状况，导致了它无法完成对请求的处理</para>
    /// </summary>
    public const int InternalServerError = 500;

    /// <summary>
    /// 未实现 (501)
    /// <para>服务器不支持当前请求所需要的某个功能</para>
    /// </summary>
    public const int NotImplemented = 501;

    /// <summary>
    /// 错误网关 (502)
    /// <para>作为网关或者代理工作的服务器尝试执行请求时，从上游服务器接收到无效的响应</para>
    /// </summary>
    public const int BadGateway = 502;

    /// <summary>
    /// 服务不可用 (503)
    /// <para>由于临时的服务器维护或��过载，服务器当前无法处理请求</para>
    /// </summary>
    public const int ServiceUnavailable = 503;

    /// <summary>
    /// 网关超时 (504)
    /// <para>作为网关或者代理工作的服务器尝试执行请求时，未能及时从上游服务器收到响应</para>
    /// </summary>
    public const int GatewayTimeout = 504;
} 
//===================================================
// 项目名称：Lean.Cus.Common.Utils
// 文件名称：HttpUtil
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：HTTP工具类
//===================================================

using System.Net;
using System.Text;
using System.Text.Json;

namespace Lean.Cus.Common.Utils;

/// <summary>
/// HTTP工具类
/// </summary>
public static class HttpUtil
{
    private static readonly HttpClient _httpClient = new();

    /// <summary>
    /// GET请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="headers">请求头</param>
    /// <returns>响应内容</returns>
    public static async Task<string> GetAsync(string url, Dictionary<string, string>? headers = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, url);
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// POST请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="content">请求内容</param>
    /// <param name="headers">请求头</param>
    /// <returns>响应内容</returns>
    public static async Task<string> PostAsync(string url, string content, Dictionary<string, string>? headers = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// PUT请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="content">请求内容</param>
    /// <param name="headers">请求头</param>
    /// <returns>响应内容</returns>
    public static async Task<string> PutAsync(string url, string content, Dictionary<string, string>? headers = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Put, url)
        {
            Content = new StringContent(content, Encoding.UTF8, "application/json")
        };

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// DELETE请求
    /// </summary>
    /// <param name="url">请求地址</param>
    /// <param name="headers">请求头</param>
    /// <returns>响应内容</returns>
    public static async Task<string> DeleteAsync(string url, Dictionary<string, string>? headers = null)
    {
        using var request = new HttpRequestMessage(HttpMethod.Delete, url);
        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// GET请求（泛型）
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="url">请求地址</param>
    /// <param name="headers">请求头</param>
    /// <returns>响应内容</returns>
    public static async Task<T?> GetAsync<T>(string url, Dictionary<string, string>? headers = null)
    {
        var content = await GetAsync(url, headers);
        return JsonSerializer.Deserialize<T>(content);
    }

    /// <summary>
    /// POST请求（泛型）
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="url">请求地址</param>
    /// <param name="content">请求内容</param>
    /// <param name="headers">请求头</param>
    /// <returns>响应内容</returns>
    public static async Task<T?> PostAsync<T>(string url, string content, Dictionary<string, string>? headers = null)
    {
        var responseContent = await PostAsync(url, content, headers);
        return JsonSerializer.Deserialize<T>(responseContent);
    }

    /// <summary>
    /// PUT请求（泛型）
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="url">请求地址</param>
    /// <param name="content">请求内容</param>
    /// <param name="headers">请求头</param>
    /// <returns>响应内容</returns>
    public static async Task<T?> PutAsync<T>(string url, string content, Dictionary<string, string>? headers = null)
    {
        var responseContent = await PutAsync(url, content, headers);
        return JsonSerializer.Deserialize<T>(responseContent);
    }

    /// <summary>
    /// DELETE请求（泛型）
    /// </summary>
    /// <typeparam name="T">返回类型</typeparam>
    /// <param name="url">请求地址</param>
    /// <param name="headers">请求头</param>
    /// <returns>响应内容</returns>
    public static async Task<T?> DeleteAsync<T>(string url, Dictionary<string, string>? headers = null)
    {
        var content = await DeleteAsync(url, headers);
        return JsonSerializer.Deserialize<T>(content);
    }

    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="url">文件地址</param>
    /// <param name="savePath">保存路径</param>
    /// <param name="headers">请求头</param>
    /// <returns>是否成功</returns>
    public static async Task<bool> DownloadFileAsync(string url, string savePath, Dictionary<string, string>? headers = null)
    {
        try
        {
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            using var response = await _httpClient.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var directory = Path.GetDirectoryName(savePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            using var fileStream = File.Create(savePath);
            await stream.CopyToAsync(fileStream);

            return true;
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// 上传文件
    /// </summary>
    /// <param name="url">上传地址</param>
    /// <param name="filePath">文件路径</param>
    /// <param name="headers">请求头</param>
    /// <returns>响应内容</returns>
    public static async Task<string> UploadFileAsync(string url, string filePath, Dictionary<string, string>? headers = null)
    {
        using var content = new MultipartFormDataContent();
        using var fileStream = File.OpenRead(filePath);
        content.Add(new StreamContent(fileStream), "file", Path.GetFileName(filePath));

        using var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = content
        };

        if (headers != null)
        {
            foreach (var header in headers)
            {
                request.Headers.Add(header.Key, header.Value);
            }
        }

        using var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    /// <summary>
    /// 获取IP地址
    /// </summary>
    /// <returns>IP地址</returns>
    public static string GetIpAddress()
    {
        var hostName = Dns.GetHostName();
        var ipEntry = Dns.GetHostEntry(hostName);
        foreach (var address in ipEntry.AddressList)
        {
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return address.ToString();
            }
        }
        return string.Empty;
    }

    /// <summary>
    /// 获取MAC地址
    /// </summary>
    /// <returns>MAC地址</returns>
    public static string GetMacAddress()
    {
        var macAddress = string.Empty;
        var networkInterfaces = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
        foreach (var adapter in networkInterfaces)
        {
            if (adapter.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
            {
                macAddress = adapter.GetPhysicalAddress().ToString();
                if (!string.IsNullOrEmpty(macAddress))
                {
                    break;
                }
            }
        }
        return macAddress;
    }
} 
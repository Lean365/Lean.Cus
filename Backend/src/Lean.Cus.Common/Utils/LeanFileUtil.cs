//===================================================
// 项目名称：Lean.Cus.Common.Utils
// 文件名称：FileUtil
// 文件版本：V1.0.0
// 创建人员：Lean
// 创建时间：2024.01.01
// 更新时间：2024.01.01
// 备注说明：文件操作工具类
//===================================================

using System.Text;

namespace Lean.Cus.Common.Utils;

/// <summary>
/// 文件操作工具类
/// </summary>
public static class FileUtil
{
    /// <summary>
    /// 创建目录
    /// </summary>
    /// <param name="path">目录路径</param>
    public static void CreateDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    /// <summary>
    /// 删除目录
    /// </summary>
    /// <param name="path">目录路径</param>
    /// <param name="recursive">是否递归删除</param>
    public static void DeleteDirectory(string path, bool recursive = true)
    {
        if (Directory.Exists(path))
        {
            Directory.Delete(path, recursive);
        }
    }

    /// <summary>
    /// 复制��录
    /// </summary>
    /// <param name="sourcePath">源目录路径</param>
    /// <param name="destinationPath">目标目录路径</param>
    /// <param name="overwrite">是否覆盖</param>
    public static void CopyDirectory(string sourcePath, string destinationPath, bool overwrite = true)
    {
        if (!Directory.Exists(sourcePath))
            return;

        if (!Directory.Exists(destinationPath))
            Directory.CreateDirectory(destinationPath);

        foreach (var file in Directory.GetFiles(sourcePath))
        {
            var fileName = Path.GetFileName(file);
            var destFile = Path.Combine(destinationPath, fileName);
            File.Copy(file, destFile, overwrite);
        }

        foreach (var dir in Directory.GetDirectories(sourcePath))
        {
            var dirName = Path.GetFileName(dir);
            var destDir = Path.Combine(destinationPath, dirName);
            CopyDirectory(dir, destDir, overwrite);
        }
    }

    /// <summary>
    /// 移动目录
    /// </summary>
    /// <param name="sourcePath">源目录路径</param>
    /// <param name="destinationPath">目标目录路径</param>
    public static void MoveDirectory(string sourcePath, string destinationPath)
    {
        if (!Directory.Exists(sourcePath))
            return;

        if (!Directory.Exists(destinationPath))
            Directory.CreateDirectory(destinationPath);

        Directory.Move(sourcePath, destinationPath);
    }

    /// <summary>
    /// 创建文件
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="content">文件内容</param>
    /// <param name="encoding">编码格式</param>
    public static void CreateFile(string path, string content, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            CreateDirectory(directory);
        }
        File.WriteAllText(path, content, encoding);
    }

    /// <summary>
    /// 删除文件
    /// </summary>
    /// <param name="path">文件路径</param>
    public static void DeleteFile(string path)
    {
        if (File.Exists(path))
        {
            File.Delete(path);
        }
    }

    /// <summary>
    /// 复制文件
    /// </summary>
    /// <param name="sourcePath">源文件路径</param>
    /// <param name="destinationPath">目标文件路径</param>
    /// <param name="overwrite">是否覆盖</param>
    public static void CopyFile(string sourcePath, string destinationPath, bool overwrite = true)
    {
        if (!File.Exists(sourcePath))
            return;

        var directory = Path.GetDirectoryName(destinationPath);
        if (!string.IsNullOrEmpty(directory))
        {
            CreateDirectory(directory);
        }
        File.Copy(sourcePath, destinationPath, overwrite);
    }

    /// <summary>
    /// 移动文件
    /// </summary>
    /// <param name="sourcePath">源文件路径</param>
    /// <param name="destinationPath">目标文件路径</param>
    public static void MoveFile(string sourcePath, string destinationPath)
    {
        if (!File.Exists(sourcePath))
            return;

        var directory = Path.GetDirectoryName(destinationPath);
        if (!string.IsNullOrEmpty(directory))
        {
            CreateDirectory(directory);
        }
        File.Move(sourcePath, destinationPath);
    }

    /// <summary>
    /// 读取文件内容
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="encoding">编码格式</param>
    /// <returns>文件内容</returns>
    public static string ReadFile(string path, Encoding? encoding = null)
    {
        encoding ??= Encoding.UTF8;
        return File.ReadAllText(path, encoding);
    }

    /// <summary>
    /// 读取文件字节
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>��件字节</returns>
    public static byte[] ReadFileBytes(string path)
    {
        return File.ReadAllBytes(path);
    }

    /// <summary>
    /// 写入文件内容
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="content">文件内容</param>
    /// <param name="encoding">编码格式</param>
    /// <param name="append">是否追加</param>
    public static void WriteFile(string path, string content, Encoding? encoding = null, bool append = false)
    {
        encoding ??= Encoding.UTF8;
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            CreateDirectory(directory);
        }
        if (append)
        {
            File.AppendAllText(path, content, encoding);
        }
        else
        {
            File.WriteAllText(path, content, encoding);
        }
    }

    /// <summary>
    /// 写入文件字节
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="bytes">文件字节</param>
    /// <param name="append">是否追加</param>
    public static void WriteFileBytes(string path, byte[] bytes, bool append = false)
    {
        var directory = Path.GetDirectoryName(path);
        if (!string.IsNullOrEmpty(directory))
        {
            CreateDirectory(directory);
        }
        if (append)
        {
            using var stream = new FileStream(path, FileMode.Append);
            stream.Write(bytes, 0, bytes.Length);
        }
        else
        {
            File.WriteAllBytes(path, bytes);
        }
    }

    /// <summary>
    /// 获取文件大小
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>文件大小（字节）</returns>
    public static long GetFileSize(string path)
    {
        if (!File.Exists(path))
            return 0;

        var fileInfo = new FileInfo(path);
        return fileInfo.Length;
    }

    /// <summary>
    /// 获取文件扩展名
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>文件扩展名</returns>
    public static string GetFileExtension(string path)
    {
        return Path.GetExtension(path);
    }

    /// <summary>
    /// 获取文件名
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <param name="withExtension">是否包含扩展名</param>
    /// <returns>文件名</returns>
    public static string GetFileName(string path, bool withExtension = true)
    {
        return withExtension ? Path.GetFileName(path) : Path.GetFileNameWithoutExtension(path);
    }

    /// <summary>
    /// 获取文件创建时间
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>文件创建时间</returns>
    public static DateTime GetFileCreationTime(string path)
    {
        return File.GetCreationTime(path);
    }

    /// <summary>
    /// 获取文件最后写入时间
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>文件最后写入时间</returns>
    public static DateTime GetFileLastWriteTime(string path)
    {
        return File.GetLastWriteTime(path);
    }

    /// <summary>
    /// 获取文件最后访问时间
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>文件最后访问时间</returns>
    public static DateTime GetFileLastAccessTime(string path)
    {
        return File.GetLastAccessTime(path);
    }
} 
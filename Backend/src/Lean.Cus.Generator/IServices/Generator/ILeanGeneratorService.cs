using Lean.Cus.Generator.Dtos.Designer;

namespace Lean.Cus.Generator.IServices.Generator
{
    /// <summary>
    /// 代码生成器服务接口
    /// </summary>
    public interface ILeanGeneratorService
    {
        /// <summary>
        /// 生成代码
        /// </summary>
        /// <param name="id">表设计ID</param>
        /// <returns>生成的代码文件路径</returns>
        Task<string> GenerateCodeAsync(long id);

        /// <summary>
        /// 预览代码
        /// </summary>
        /// <param name="id">表设计ID</param>
        /// <returns>预览代码内容</returns>
        Task<Dictionary<string, string>> PreviewCodeAsync(long id);

        /// <summary>
        /// 同步数据库
        /// </summary>
        /// <param name="id">表设计ID</param>
        /// <returns>是否成功</returns>
        Task<bool> SyncDatabaseAsync(long id);

        /// <summary>
        /// 获取模板列表
        /// </summary>
        /// <returns>模板列表</returns>
        Task<List<string>> GetTemplatesAsync();

        /// <summary>
        /// 获取前端模板列表
        /// </summary>
        /// <returns>前端模板列表</returns>
        Task<List<string>> GetFrontendTemplatesAsync();
    }
} 
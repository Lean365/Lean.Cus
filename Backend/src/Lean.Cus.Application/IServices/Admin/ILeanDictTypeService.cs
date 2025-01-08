using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Domain.Entities.Admin;

namespace Lean.Cus.Application.IServices.Admin;

/// <summary>
/// 字典类型服务接口
/// </summary>
public interface ILeanDictTypeService : ILeanService<LeanDictType, LeanDictTypeDto, LeanDictTypeQueryDto, LeanDictTypeCreateDto, LeanDictTypeUpdateDto>
{
    /// <summary>
    /// 检查字典类型编码是否唯一
    /// </summary>
    /// <param name="code">字典类型编码</param>
    /// <param name="id">字典类型ID</param>
    /// <returns>是否唯一</returns>
    Task<bool> CheckDictTypeCodeUniqueAsync(string code, long? id = null);

    /// <summary>
    /// 导出字典类型数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>字典类型数据</returns>
    Task<List<LeanDictTypeDto>> ExportDictTypesAsync(LeanDictTypeQueryDto queryDto);
} 
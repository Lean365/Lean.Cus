using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Domain.Entities.Admin;

namespace Lean.Cus.Application.IServices.Admin;

/// <summary>
/// 字典数据服务接口
/// </summary>
public interface ILeanDictDataService : ILeanService<LeanDictData, LeanDictDataDto, LeanDictDataQueryDto, LeanDictDataCreateDto, LeanDictDataUpdateDto>
{
    /// <summary>
    /// 根据字典类型编码获取字典数据列表
    /// </summary>
    /// <param name="dictTypeCode">字典类型编码</param>
    /// <returns>字典数据列表</returns>
    Task<List<LeanDictDataDto>> GetDictDataListByTypeCodeAsync(string dictTypeCode);

    /// <summary>
    /// 导出字典数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>字典数据</returns>
    Task<List<LeanDictDataDto>> ExportDictDataAsync(LeanDictDataQueryDto queryDto);
} 
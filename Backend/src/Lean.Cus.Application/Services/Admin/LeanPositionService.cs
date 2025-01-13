using Lean.Cus.Domain.Entities.Admin;
using Lean.Cus.Domain.IRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Common.Excel;
using Lean.Cus.Common.Paging;
using System.IO;
using Lean.Cus.Application.Interfaces.Admin;
using Lean.Cus.Application.Dtos.Admin;
using Mapster;
using SqlSugar;
using System;
using System.Linq;
using Lean.Cus.Common.Enums;

namespace Lean.Cus.Application.Services.Admin;

/// <summary>
/// 职位服务实现
/// </summary>
public class LeanPositionService : ILeanPositionService
{
    private readonly ILeanRepository<LeanPosition> _positionRepository;
    private readonly ILeanRepository<LeanUserPosition> _userPositionRepository;

    /// <summary>
    /// 初始化职位服务
    /// </summary>
    public LeanPositionService(
        ILeanRepository<LeanPosition> positionRepository,
        ILeanRepository<LeanUserPosition> userPositionRepository)
    {
        _positionRepository = positionRepository;
        _userPositionRepository = userPositionRepository;
    }

    /// <summary>
    /// 新增职位
    /// </summary>
    public async Task<LeanPositionDto> CreateAsync(LeanPositionDto dto)
    {
        var position = dto.Adapt<LeanPosition>();
        await _positionRepository.InsertAsync(position);
        return position.Adapt<LeanPositionDto>();
    }

    /// <summary>
    /// 更新职位
    /// </summary>
    public async Task<bool> UpdateAsync(LeanPositionDto dto)
    {
        var position = dto.Adapt<LeanPosition>();
        return await _positionRepository.UpdateAsync(position) > 0;
    }

    /// <summary>
    /// 删除职位
    /// </summary>
    public async Task<bool> DeleteAsync(long id)
    {
        return await _positionRepository.DeleteAsync(u => u.Id == id) > 0;
    }

    /// <summary>
    /// 获取职位
    /// </summary>
    public async Task<LeanPositionDto> GetAsync(long id)
    {
        var position = await _positionRepository.GetAsync(x => x.Id == id);
        return position.Adapt<LeanPositionDto>();
    }

    /// <summary>
    /// 获取职位列表
    /// </summary>
    public async Task<List<LeanPositionDto>> GetListAsync(LeanPositionQueryDto queryDto)
    {
        var positions = await _positionRepository.GetListAsync(u =>
            (string.IsNullOrEmpty(queryDto.PositionName) || u.Name.Contains(queryDto.PositionName)) &&
            (string.IsNullOrEmpty(queryDto.PositionCode) || u.Code.Contains(queryDto.PositionCode)) &&
            (!queryDto.Status.HasValue || u.Status == queryDto.Status.Value) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value));
        return positions.Adapt<List<LeanPositionDto>>();
    }

    /// <summary>
    /// 分页查询职位
    /// </summary>
    public async Task<PagedResults<LeanPositionDto>> GetPagedListAsync(LeanPositionQueryDto queryDto)
    {
        RefAsync<int> total = 0;
        var list = await _positionRepository.GetPageListAsync(u =>
            (string.IsNullOrEmpty(queryDto.PositionName) || u.Name.Contains(queryDto.PositionName)) &&
            (string.IsNullOrEmpty(queryDto.PositionCode) || u.Code.Contains(queryDto.PositionCode)) &&
            (!queryDto.Status.HasValue || u.Status == queryDto.Status.Value) &&
            (!queryDto.CreatedTimeStart.HasValue || u.CreateTime >= queryDto.CreatedTimeStart.Value) &&
            (!queryDto.CreatedTimeEnd.HasValue || u.CreateTime <= queryDto.CreatedTimeEnd.Value),
            queryDto.PageIndex,
            queryDto.PageSize,
            total);

        var positionDtos = list.Adapt<List<LeanPositionDto>>();
        return new PagedResults<LeanPositionDto>
        {
            Items = positionDtos,
            Total = total,
            PageIndex = queryDto.PageIndex,
            PageSize = queryDto.PageSize
        };
    }

    /// <summary>
    /// 导入职位数据
    /// </summary>
    public async Task<(List<LeanPositionDto> SuccessItems, List<string> ErrorMessages)> ImportAsync(byte[] fileBytes)
    {
        using var stream = new MemoryStream(fileBytes);
        var result = await LeanExcelHelper.ImportAsync<LeanPosition>(stream);
        if (result.SuccessItems.Count > 0)
        {
            await _positionRepository.InsertRangeAsync(result.SuccessItems);
        }
        return (result.SuccessItems.Adapt<List<LeanPositionDto>>(), result.ErrorMessages);
    }

    /// <summary>
    /// 导出职位数据
    /// </summary>
    public async Task<byte[]> ExportAsync(LeanPositionQueryDto queryDto)
    {
        var list = await GetListAsync(queryDto);
        var exportList = list.Adapt<List<LeanPositionExportDto>>();
        return await LeanExcelHelper.ExportAsync(exportList);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    public async Task<byte[]> GetImportTemplateAsync()
    {
        return await LeanExcelHelper.GetImportTemplateAsync<LeanPosition>();
    }

    /// <summary>
    /// 获取用户职位列表
    /// </summary>
    public async Task<List<LeanPositionDto>> GetUserPositionsAsync(long userId)
    {
        var positions = await _positionRepository.GetListAsync(p => p.Status == LeanStatus.Enabled);
        return positions.Adapt<List<LeanPositionDto>>();
    }

    /// <summary>
    /// 更新职位状态
    /// </summary>
    public async Task<bool> UpdateStatusAsync(LeanPositionStatusUpdateDto input)
    {
        var position = await _positionRepository.GetByIdAsync(input.Id);
        if (position == null)
            return false;

        position.Status = input.Status;
        return await _positionRepository.UpdateAsync(position) > 0;
    }
} 
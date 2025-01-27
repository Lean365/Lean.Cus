//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成
//     如果重新生成代码，将丢失对此文件的所有修改。
// </auto-generated>
//
// <copyright file="LeanMenuController.cs" company="Lean365">
//     Copyright (c) Lean365. All rights reserved.
// </copyright>
// <author>Lean365</author>
// <date>2024-01-09</date>
// <summary>
// 菜单控制器
// </summary>
//------------------------------------------------------------------------------

using Microsoft.AspNetCore.Mvc;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.Services.Admin;
using Lean.Cus.Common.Attributes;
using Lean.Cus.Common.Paging;

namespace Lean.Cus.WebApi.Controllers.Admin;

/// <summary>
/// 菜单控制器
/// </summary>
[ApiController]
[Route("api/admin/menu")]
public class LeanMenuController : ControllerBase
{
    private readonly ILeanMenuService _menuService;

    public LeanMenuController(ILeanMenuService menuService)
    {
        _menuService = menuService;
    }

    /// <summary>
    /// 获取菜单列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>菜单列表</returns>
    [HttpGet("list")]
    [Permission("system:menu:list")]
    public async Task<ActionResult<PagedList<LeanMenuDto>>> GetPagedListAsync([FromQuery] LeanMenuQueryDto queryDto)
    {
        var result = await _menuService.GetPagedListAsync(queryDto);
        return Ok(result);
    }

    /// <summary>
    /// 获取菜单树
    /// </summary>
    /// <returns>菜单树</returns>
    [HttpGet("tree")]
    [Permission("system:menu:list")]
    public async Task<ActionResult<List<LeanMenuDto>>> GetTreeAsync()
    {
        var result = await _menuService.GetTreeAsync();
        return Ok(result);
    }

    /// <summary>
    /// 获取菜单详情
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>菜单详情</returns>
    [HttpGet("{id}")]
    [Permission("system:menu:query")]
    public async Task<ActionResult<LeanMenuDto>> GetAsync(long id)
    {
        var result = await _menuService.GetAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 创建菜单
    /// </summary>
    /// <param name="createDto">创建对象</param>
    /// <returns>菜单ID</returns>
    [HttpPost]
    [Permission("system:menu:add")]
    public async Task<ActionResult<long>> CreateAsync([FromBody] LeanMenuCreateDto createDto)
    {
        var result = await _menuService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 更新菜单
    /// </summary>
    /// <param name="updateDto">更新对象</param>
    /// <returns>是否成功</returns>
    [HttpPut]
    [Permission("system:menu:edit")]
    public async Task<ActionResult<bool>> UpdateAsync([FromBody] LeanMenuUpdateDto updateDto)
    {
        var result = await _menuService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除菜单
    /// </summary>
    /// <param name="id">菜单ID</param>
    /// <returns>是否成功</returns>
    [HttpDelete("{id}")]
    [Permission("system:menu:remove")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        var result = await _menuService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 批量删除菜单
    /// </summary>
    /// <param name="ids">菜单ID集合</param>
    /// <returns>是否成功</returns>
    [HttpDelete("batch")]
    [Permission("system:menu:remove")]
    public async Task<ActionResult<bool>> BatchDeleteAsync([FromBody] long[] ids)
    {
        var result = await _menuService.BatchDeleteAsync(ids);
        return Ok(result);
    }
} 
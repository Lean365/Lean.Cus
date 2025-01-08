using System.Collections.Generic;
using System.Threading.Tasks;
using Lean.Cus.Application.Dtos.Admin;
using Lean.Cus.Application.IServices.Admin;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Admin;

/// <summary>
/// 部门管理
/// </summary>
[Route("api/admin/[controller]")]
[ApiController]
[Authorize]
public class LeanDeptController : ControllerBase
{
    private readonly ILeanDeptService _deptService;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="deptService">部门服务</param>
    public LeanDeptController(ILeanDeptService deptService)
    {
        _deptService = deptService;
    }

    /// <summary>
    /// 获取部门列表
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>部门列表</returns>
    [HttpGet("list")]
    public async Task<ActionResult<List<LeanDeptDto>>> GetListAsync([FromQuery] LeanDeptQueryDto queryDto)
    {
        var list = await _deptService.GetListAsync(queryDto);
        return Ok(list);
    }

    /// <summary>
    /// 获取部门树形结构
    /// </summary>
    /// <returns>部门树形结构</returns>
    [HttpGet("tree")]
    public async Task<ActionResult<List<LeanDeptDto>>> GetTreeAsync()
    {
        var tree = await _deptService.GetDeptTreeAsync();
        return Ok(tree);
    }

    /// <summary>
    /// 获取部门信息
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <returns>部门信息</returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<LeanDeptDto>> GetAsync(long id)
    {
        var dept = await _deptService.GetAsync(id);
        return Ok(dept);
    }

    /// <summary>
    /// 新增部门
    /// </summary>
    /// <param name="createDto">创建信息</param>
    /// <returns>操作结果</returns>
    [HttpPost]
    public async Task<ActionResult<bool>> CreateAsync([FromBody] LeanDeptCreateDto createDto)
    {
        // 检查部门编码是否唯一
        if (await _deptService.CheckDeptCodeUniqueAsync(createDto.Code))
        {
            return BadRequest("部门编码已存在");
        }

        var result = await _deptService.CreateAsync(createDto);
        return Ok(result);
    }

    /// <summary>
    /// 更新部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <param name="updateDto">更新信息</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> UpdateAsync(long id, [FromBody] LeanDeptUpdateDto updateDto)
    {
        if (id != updateDto.Id)
        {
            return BadRequest("ID不匹配");
        }

        // 检查部门编码是否唯一
        if (await _deptService.CheckDeptCodeUniqueAsync(updateDto.Code, id))
        {
            return BadRequest("部门编码已存在");
        }

        var result = await _deptService.UpdateAsync(updateDto);
        return Ok(result);
    }

    /// <summary>
    /// 删除部门
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <returns>操作结果</returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> DeleteAsync(long id)
    {
        // 检查是否存在子部门
        var children = await _deptService.GetChildrenAsync(id);
        if (children.Count > 0)
        {
            return BadRequest("存在子部门，无法删除");
        }

        // 检查是否存在用户
        var users = await _deptService.GetDeptUsersAsync(id);
        if (users.Count > 0)
        {
            return BadRequest("部门下存在用户，无法删除");
        }

        var result = await _deptService.DeleteAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// 修改部门状态
    /// </summary>
    /// <param name="id">部门ID</param>
    /// <param name="status">状态</param>
    /// <returns>操作结果</returns>
    [HttpPut("{id}/status")]
    public async Task<ActionResult<bool>> UpdateStatusAsync(long id, [FromBody] LeanEnabledStatus status)
    {
        var result = await _deptService.UpdateStatusAsync(id, status);
        return Ok(result);
    }

    /// <summary>
    /// 导出部门数据
    /// </summary>
    /// <param name="queryDto">查询条件</param>
    /// <returns>部门数据</returns>
    [HttpGet("export")]
    public async Task<ActionResult<List<LeanDeptDto>>> ExportAsync([FromQuery] LeanDeptQueryDto queryDto)
    {
        var depts = await _deptService.ExportDeptsAsync(queryDto);
        return Ok(depts);
    }

    /// <summary>
    /// 获取导入模板
    /// </summary>
    /// <returns>导入模板</returns>
    [HttpGet("import/template")]
    public async Task<ActionResult<List<LeanDeptDto>>> GetImportTemplateAsync()
    {
        var template = await _deptService.GetImportTemplateAsync();
        return Ok(template);
    }

    /// <summary>
    /// 导入部门数据
    /// </summary>
    /// <param name="dtos">部门数据</param>
    /// <returns>导入结果</returns>
    [HttpPost("import")]
    public async Task<ActionResult<(int Success, int Fail)>> ImportAsync([FromBody] List<LeanDeptCreateDto> dtos)
    {
        var result = await _deptService.ImportDeptsAsync(dtos);
        return Ok(result);
    }
} 
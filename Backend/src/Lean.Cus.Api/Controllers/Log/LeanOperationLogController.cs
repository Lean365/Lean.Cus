using System.Threading.Tasks;
using System.Collections.Generic;
using Lean.Cus.Application.Dtos.Log;
using Lean.Cus.Application.Interfaces.Log;
using Lean.Cus.Common.Models;
using Lean.Cus.Common.Paging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers.Log
{
    /// <summary>
    /// 操作日志控制器
    /// </summary>
    [Route("api/log/operation")]
    [ApiController]
    [Authorize]
    public class LeanOperationLogController : ControllerBase
    {
        private readonly ILeanOperationLogService _operationLogService;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="operationLogService">操作日志服务</param>
        public LeanOperationLogController(ILeanOperationLogService operationLogService)
        {
            _operationLogService = operationLogService;
        }

        /// <summary>
        /// 添加操作日志
        /// </summary>
        /// <param name="input">创建对象</param>
        /// <returns>结果</returns>
        [HttpPost]
        public async Task<ActionResult<LeanApiResult<LeanOperationLogDto>>> Add([FromBody] LeanOperationLogCreateDto input)
        {
            var result = await _operationLogService.CreateAsync(input);
            return Ok(LeanApiResult<LeanOperationLogDto>.Ok(result));
        }

        /// <summary>
        /// 删除操作日志
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>结果</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<LeanApiResult>> Delete(long id)
        {
            var result = await _operationLogService.DeleteAsync(id);
            return Ok(result ? LeanApiResult.Ok("删除成功") : LeanApiResult.Fail("删除失败"));
        }

        /// <summary>
        /// 获取操作日志
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>结果</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<LeanApiResult<LeanOperationLogDto>>> Get(long id)
        {
            var result = await _operationLogService.GetAsync(id);
            return Ok(LeanApiResult<LeanOperationLogDto>.Ok(result));
        }

        /// <summary>
        /// 获取操作日志列表
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>结果</returns>
        [HttpGet]
        public async Task<ActionResult<LeanApiResult<List<LeanOperationLogDto>>>> GetList([FromQuery] LeanOperationLogQueryDto query)
        {
            var result = await _operationLogService.GetListAsync(query);
            return Ok(LeanApiResult<List<LeanOperationLogDto>>.Ok(result));
        }

        /// <summary>
        /// 分页查询操作日志
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>结果</returns>
        [HttpGet("page")]
        public async Task<ActionResult<LeanApiResult<PagedResults<LeanOperationLogDto>>>> GetPageList([FromQuery] LeanOperationLogQueryDto query)
        {
            var result = await _operationLogService.GetPagedListAsync(query);
            return Ok(LeanApiResult<PagedResults<LeanOperationLogDto>>.Ok(result));
        }

        /// <summary>
        /// 导出操作日志
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns>结果</returns>
        [HttpGet("export")]
        public async Task<IActionResult> Export([FromQuery] LeanOperationLogQueryDto query)
        {
            var result = await _operationLogService.ExportAsync(query);
            return File(result, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "操作日志.xlsx");
        }

        /// <summary>
        /// 清空操作日志
        /// </summary>
        /// <returns>结果</returns>
        [HttpPost("clear")]
        public async Task<ActionResult<LeanApiResult>> Clear()
        {
            var result = await _operationLogService.ClearAsync();
            return Ok(result ? LeanApiResult.Ok("清空成功") : LeanApiResult.Fail("清空失败"));
        }
    }
} 
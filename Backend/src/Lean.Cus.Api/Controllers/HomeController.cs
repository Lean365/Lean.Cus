using Microsoft.AspNetCore.Mvc;

namespace Lean.Cus.Api.Controllers;

/// <summary>
/// 首页控制器
/// </summary>
[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    /// <summary>
    /// 健康检查
    /// </summary>
    /// <returns>服务状态</returns>
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Status = "Running",
            Time = DateTime.Now,
            Message = "服务正常运行中"
        });
    }
} 
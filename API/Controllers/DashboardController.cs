using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize,]
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : ControllerBase
    {
        [HttpGet("{userId}")]
        public IActionResult GetInfo(int userId)
        {
            try
            {
                return Ok();
            }
            catch
            {
                return Problem();
            }
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PFS.Applications;

namespace API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("dashboard")]
    public class DashboardController : ControllerBase
    {
        private readonly int _userId;
        private readonly string _connectionStringMG;
        private readonly string _connectionStringCore;
        private readonly DashboardApplication _application;
        
        public DashboardController()
        {
            _connectionStringMG = Environment.GetEnvironmentVariable("ConnectionStringMG") ??
                          "Host=localhost;Port=5432;UserName=postgres;Password=root;Database=PFS-MG";
            _connectionStringCore = Environment.GetEnvironmentVariable("ConnectionStringCORE") ??
                                    "Host=localhost;Port=5432;UserName=postgres;Password=root;Database=PFS-MG";
            
            _application = new(_connectionStringMG,_connectionStringCore);
        }
        
        [HttpGet]
        [Route("[controller]/info/{painelId}")]
        public IActionResult GetInfo(int painelId)
        {
            // var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            try
            {
                var dashboardInfoResult = _application.GetDashboardInfo(painelId);
                if (!dashboardInfoResult.success)
                    return Problem(JsonConvert.SerializeObject(dashboardInfoResult.errors));

                var dashboardInfo = dashboardInfoResult.obj.FirstOrDefault();
                return Ok(dashboardInfo);
            }
            catch
            {
                return Problem();
            }
        }
    }
}
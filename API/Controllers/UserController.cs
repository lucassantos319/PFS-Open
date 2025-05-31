using Microsoft.AspNetCore.Authorization;
using PFS.Applications;
using PFS.Domain.Models.RequestBody;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private readonly UserApplication _application;
        private readonly ILogger<UserController> _logger;
        private readonly string _connectionString;
        
        public UserController(IConfiguration configuration,ILogger<UserController> logger)
        {
            _logger = logger;
            _connectionString = configuration["ConnectionStringMG"];
            if (_application == null)
                _application = new UserApplication(_connectionString);
        }

        [HttpGet]
        [Route("{id}")]
        [Authorize]
        public IActionResult Get([FromQuery] int id)
        {
            try
            {
                var user = _application.GetByFilter(new()
                {
                    id = id
                });
                
                return Ok(user);
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody] UserLogin login)
        {
            try
            {
                var token = _application.Login(new()
                {
                    email = login.email,
                    password = login.password,
                    status = new List<int> { 1 }
                });

                if (!string.IsNullOrEmpty(token))
                    return Ok(new
                    {
                        token = token
                    });

                return Problem();
            }
            catch (Exception ex)
            {
                return Problem();
            }
        }
    }
}
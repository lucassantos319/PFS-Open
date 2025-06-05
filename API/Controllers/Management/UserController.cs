using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PFS.Application.Applications.Management;
using PFS.Domain.Models.RequestBody;

namespace WebApplication1.Controllers.Management;

[ApiController]
[Route("users")]
public class UserController : ControllerBase
{
    private readonly UserApplication _application;
    private readonly ILogger<UserController> _logger;

    public UserController(IConfiguration configuration,ILogger<UserController> logger)
    {
        _logger = logger;
        var connectionString = configuration["ConnectionStringMG"];
            
        if (_application == null)
            _application = new UserApplication(connectionString ?? throw new InvalidOperationException());
    }

    [HttpGet]
    [Route("{id}")]
    [Authorize]
    public IActionResult Get([FromQuery] int id)
    {
        var user = _application.Get(new()
        {
            id = id
        });
                
        return Ok(user);
    }

    [HttpPost]
    [Route("login")]
    public IActionResult Login([FromBody] UserLogin login)
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
}
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PFS.Applications;
using PFS.Domain.Models.Entities.Management;
using PFS.Domain.Models.Enums;
using PFS.Domain.Models.Errors;

namespace API.Controllers;

[Authorize]
[ApiController]
[Route("painel")]
public class PainelController : ControllerBase
{
    private readonly string _connection;
    private readonly PainelApplication _application;

    public PainelController()
    {
        _connection = Environment.GetEnvironmentVariable("ConnectionStringMG") ??
                      "Host=localhost;Port=5432;UserName=postgres;Password=root;Database=PFS-MG";
        _application = new(_connection);
    }

    [HttpGet]
    [ProducesResponseType<IEnumerable<Painel>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    
    public IActionResult Get()
    {
        var user = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                     ?? throw new Exception("User ID not found in token.");
        
        
        var painelsResponse = _application.Get(new()
        {
            users = new int[]{int.Parse(user)}
        });

        if (painelsResponse.success)
            return Ok(painelsResponse.obj);
        
        
        var mainError = string.Join(" ==> ",painelsResponse.errors.Select(x=>x));
        return NotFound(new ErrorType
        {
            title = mainError,
            detail = mainError,
            status = 404
        });
    } 
    
    
    [HttpPost]
    [ProducesResponseType<Painel>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public IActionResult Post([FromBody] PainelCreation obj)
    {
        var painel = JsonConvert.DeserializeObject<Painel>(obj.painel);
        if ( painel == null )
            return BadRequest(new
            {
                error = "Objeto não foi deserializado corretamente"
            });
        
        var createdPainel = _application.AddPainel(painel,obj.email,(ERole) obj.role);
        if ( createdPainel.success )
            return Ok(createdPainel);
        
        return BadRequest(new 
        {
            type = "https://httpstatuses.com/500",
            title = "An unexpected error occurred!",
            status = 500,
            detail = createdPainel?.errors.Select(x=>x)
        });
    }
    
    
}

public class PainelCreation
{
    public string painel { get; set; }
    public string email { get; set; }
    public int role { get; set; }
}
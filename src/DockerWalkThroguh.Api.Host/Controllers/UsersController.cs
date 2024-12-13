using Microsoft.AspNetCore.Mvc;

namespace DockerWalkThroguh.Api.Host.Controllers;

[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public UsersController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(_configuration.GetConnectionString("DefaultConnection"));
    }
    
}
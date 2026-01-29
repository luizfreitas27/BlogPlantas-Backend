using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlantasDomesticas.Api.Controllers;

[ApiController]
public class BaseController : ControllerBase
{
    protected Guid GetUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    
}
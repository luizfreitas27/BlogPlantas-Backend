using BlogPlantasDomesticas.Api.Dtos.Auth.Request;
using BlogPlantasDomesticas.Api.Dtos.Auth.Response;
using BlogPlantasDomesticas.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace BlogPlantasDomesticas.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase 
{
    
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost]
    [Route("sign-in")]
    [ProducesResponseType(typeof(LoginResponseDto), 200)]
    public async Task<IActionResult> Loginuser([FromBody] LoginRequestDto dto, CancellationToken cancellationToken)
    {
        var response = await _authService.LoginAsync(dto, cancellationToken);
        
        return Ok(response);
    }

    [HttpPost]
    [Route("refresh-token")]
    [ProducesResponseType(typeof(LoginResponseDto), 200)]
    public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenRequestDto dto,
        CancellationToken cancellationToken)
    {
        var response = await _authService.RefreshTokenAsync(dto, cancellationToken);
        return Ok(response);
    }
}
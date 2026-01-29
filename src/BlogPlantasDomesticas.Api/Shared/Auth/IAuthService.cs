using BlogPlantasDomesticas.Api.Dtos.Auth.Request;
using BlogPlantasDomesticas.Api.Dtos.Auth.Response;

namespace BlogPlantasDomesticas.Api.Shared.Auth;

public interface IAuthService
{
    Task<LoginResponseDto> LoginAsync(LoginRequestDto dto, CancellationToken cancellationToken); 
   
    Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto dto, CancellationToken cancellationToken); 
}
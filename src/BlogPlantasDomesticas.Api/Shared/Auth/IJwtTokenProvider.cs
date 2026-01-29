using BlogPlantasDomesticas.Api.Models;

namespace BlogPlantasDomesticas.Api.Shared.Auth;

public interface IJwtTokenProvider
{
    (string token, DateTime ExpiresAt) GenerateToken(User user);
    string GenerateRefreshToken(); 
}
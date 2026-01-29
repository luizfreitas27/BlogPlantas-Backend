using BlogPlantasDomesticas.Api.Enums;

namespace BlogPlantasDomesticas.Api.Dtos.Auth.Response;

public class LoginResponseDto
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTime ExpiresAt { get; set; }
    public required string Username { get; set; }
    public required RoleType RoleId { get; set; }
    public required string RoleName { get; set; }
}

using BlogPlantasDomesticas.Api.Enums;

namespace BlogPlantasDomesticas.Api.Models;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public required int RoleId { get; set; }
    public string? RefreshToken { get; set; } 
    public DateTime? ExpiresAt { get; set; }

    public Role Role { get; set; } = null!;
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public RoleType RoleType => (RoleType)RoleId;
}

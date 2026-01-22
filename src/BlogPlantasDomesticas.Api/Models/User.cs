namespace BlogPlantasDomesticas.Api.Models;

public class User : BaseEntity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string? AvatarUrl { get; set; }
    public string? Bio { get; set; }
    public required Guid RoleId { get; set; }

    // Navigation properties
    public Role Role { get; set; } = null!;
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

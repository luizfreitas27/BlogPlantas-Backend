namespace BlogPlantasDomesticas.Api.Models;

public class Role : BaseEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    // Navigation properties
    public ICollection<User> Users { get; set; } = new List<User>();
}

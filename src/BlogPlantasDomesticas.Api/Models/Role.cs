namespace BlogPlantasDomesticas.Api.Models;

public class Role 
{
    
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required DateTime CreatedAt { get; set; }
    public DateTime?  UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<User> Users { get; set; } = new List<User>();
}

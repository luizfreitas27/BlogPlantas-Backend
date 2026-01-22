namespace BlogPlantasDomesticas.Api.Models;

public class Category : BaseEntity
{
    public required string Name { get; set; }
    public required string Slug { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }

    // Navigation properties
    public ICollection<Post> Posts { get; set; } = new List<Post>();
}

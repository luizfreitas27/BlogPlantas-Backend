namespace BlogPlantasDomesticas.Api.Models;

public class Tag : BaseEntity
{
    public required string Name { get; set; }
    public required string Slug { get; set; }

    // Navigation properties
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}

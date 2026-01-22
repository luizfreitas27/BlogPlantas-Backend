namespace BlogPlantasDomesticas.Api.Models;

public class PostTag
{
    public required Guid PostId { get; set; }
    public required Guid TagId { get; set; }

    // Navigation properties
    public Post Post { get; set; } = null!;
    public Tag Tag { get; set; } = null!;
}

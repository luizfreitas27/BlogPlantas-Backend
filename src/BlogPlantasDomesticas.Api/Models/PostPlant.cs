namespace BlogPlantasDomesticas.Api.Models;

public class PostPlant
{
    public required Guid PostId { get; set; }
    public required Guid PlantId { get; set; }

    // Navigation properties
    public Post Post { get; set; } = null!;
    public Plant Plant { get; set; } = null!;
}

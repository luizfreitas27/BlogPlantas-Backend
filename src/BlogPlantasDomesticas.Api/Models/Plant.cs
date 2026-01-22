using BlogPlantasDomesticas.Api.Models.Enums;

namespace BlogPlantasDomesticas.Api.Models;

public class Plant : BaseEntity
{
    public required string CommonName { get; set; }
    public string? ScientificName { get; set; }
    public required string Slug { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public CareLevel? CareLevel { get; set; }
    public LightNeeds? LightNeeds { get; set; }
    public string? WaterFrequency { get; set; }
    public bool? IsPetSafe { get; set; }
    public string? IdealEnvironment { get; set; }

    // Navigation properties
    public ICollection<PostPlant> PostPlants { get; set; } = new List<PostPlant>();
}

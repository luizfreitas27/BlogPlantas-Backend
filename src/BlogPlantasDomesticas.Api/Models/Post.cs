using BlogPlantasDomesticas.Api.Models.Enums;

namespace BlogPlantasDomesticas.Api.Models;

public class Post : BaseEntity
{
    public required Guid AuthorId { get; set; }
    public Guid? CategoryId { get; set; }
    public required string Title { get; set; }
    public required string Slug { get; set; }
    public string? Excerpt { get; set; }
    public required string Content { get; set; }
    public string? CoverImageUrl { get; set; }
    public PostStatus Status { get; set; } = PostStatus.Draft;
    public int ViewsCount { get; set; } = 0;
    public DateTime? PublishedAt { get; set; }

    // Navigation properties
    public User Author { get; set; } = null!;
    public Category? Category { get; set; }
    public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
    public ICollection<PostPlant> PostPlants { get; set; } = new List<PostPlant>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
}

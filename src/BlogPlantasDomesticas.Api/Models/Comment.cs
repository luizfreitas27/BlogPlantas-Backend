namespace BlogPlantasDomesticas.Api.Models;

public class Comment : BaseEntity
{
    public required Guid PostId { get; set; }
    public Guid? UserId { get; set; }
    public Guid? ParentId { get; set; }
    public string? AuthorName { get; set; }
    public string? AuthorEmail { get; set; }
    public required string Content { get; set; }
    public bool IsApproved { get; set; } = false;

    // Navigation properties
    public Post Post { get; set; } = null!;
    public User? User { get; set; }
    public Comment? Parent { get; set; }
    public ICollection<Comment> Replies { get; set; } = new List<Comment>();
}

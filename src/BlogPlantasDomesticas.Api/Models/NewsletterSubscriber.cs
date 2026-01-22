namespace BlogPlantasDomesticas.Api.Models;

public class NewsletterSubscriber : BaseEntity
{
    public required string Email { get; set; }
    public string? Name { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTime SubscribedAt { get; set; } = DateTime.UtcNow;
}

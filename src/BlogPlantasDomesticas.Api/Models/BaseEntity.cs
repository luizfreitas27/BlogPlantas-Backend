namespace BlogPlantasDomesticas.Api.Models;

public class BaseEntity
{
    public Guid Id { get; set; } =  Guid.NewGuid();
    
    public required  DateTime CreatedAt { get; set; } 
    public DateTime? UpdatedAt { get; set; } 
}
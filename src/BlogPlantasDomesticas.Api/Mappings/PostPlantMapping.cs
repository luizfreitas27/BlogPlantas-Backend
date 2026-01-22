using BlogPlantasDomesticas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlantasDomesticas.Api.Mappings;

public class PostPlantMapping : IEntityTypeConfiguration<PostPlant>
{
    public void Configure(EntityTypeBuilder<PostPlant> builder)
    {
        builder.ToTable("post_plants");

        builder.HasKey(pp => new { pp.PostId, pp.PlantId });

        builder.Property(pp => pp.PostId)
            .HasColumnName("post_id");

        builder.Property(pp => pp.PlantId)
            .HasColumnName("plant_id");

        // Relationships
        builder.HasOne(pp => pp.Post)
            .WithMany(p => p.PostPlants)
            .HasForeignKey(pp => pp.PostId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(pp => pp.Plant)
            .WithMany(p => p.PostPlants)
            .HasForeignKey(pp => pp.PlantId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using BlogPlantasDomesticas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlantasDomesticas.Api.Mappings;

public class PlantMapping : IEntityTypeConfiguration<Plant>
{
    public void Configure(EntityTypeBuilder<Plant> builder)
    {
        builder.ToTable("plants");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.CommonName)
            .HasColumnName("common_name")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(p => p.ScientificName)
            .HasColumnName("scientific_name")
            .HasMaxLength(150);

        builder.Property(p => p.Slug)
            .HasColumnName("slug")
            .HasMaxLength(120)
            .IsRequired();

        builder.Property(p => p.Description)
            .HasColumnName("description")
            .HasColumnType("text");

        builder.Property(p => p.ImageUrl)
            .HasColumnName("image_url")
            .HasMaxLength(500);

        builder.Property(p => p.CareLevel)
            .HasColumnName("care_level")
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(p => p.LightNeeds)
            .HasColumnName("light_needs")
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(p => p.WaterFrequency)
            .HasColumnName("water_frequency")
            .HasMaxLength(100);

        builder.Property(p => p.IsPetSafe)
            .HasColumnName("is_pet_safe");

        builder.Property(p => p.IdealEnvironment)
            .HasColumnName("ideal_environment")
            .HasMaxLength(200);

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("TIMESTAMPTZ")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("TIMESTAMPTZ")
            .HasColumnName("updated_at");

        // Indexes
        builder.HasIndex(p => p.Slug)
            .IsUnique();

        builder.HasIndex(p => p.CareLevel);

        builder.HasIndex(p => p.IsPetSafe);
    }
}

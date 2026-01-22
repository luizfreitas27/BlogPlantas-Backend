using BlogPlantasDomesticas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlantasDomesticas.Api.Mappings;

public class CategoryMapping : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("categories");

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Id)
            .HasColumnName("id");

        builder.Property(c => c.Name)
            .HasColumnName("name")
            .HasMaxLength(80)
            .IsRequired();

        builder.Property(c => c.Slug)
            .HasColumnName("slug")
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasMaxLength(300);

        builder.Property(c => c.ImageUrl)
            .HasColumnName("image_url")
            .HasMaxLength(500);

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("TIMESTAMPTZ")
            .IsRequired();

        builder.Property(c => c.UpdatedAt)
            .HasColumnType("TIMESTAMPTZ")
            .HasColumnName("updated_at");

        // Indexes
        builder.HasIndex(c => c.Name)
            .IsUnique();

        builder.HasIndex(c => c.Slug)
            .IsUnique();
    }
}

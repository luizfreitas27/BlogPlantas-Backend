using BlogPlantasDomesticas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlantasDomesticas.Api.Mappings;

public class TagMapping : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable("tags");

        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id)
            .HasColumnName("id");

        builder.Property(t => t.Name)
            .HasColumnName("name")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(t => t.Slug)
            .HasColumnName("slug")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(t => t.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("TIMESTAMPTZ")
            .IsRequired();

        builder.Property(t => t.UpdatedAt)
            .HasColumnType("TIMESTAMPTZ")
            .HasColumnName("updated_at");

        // Indexes
        builder.HasIndex(t => t.Name)
            .IsUnique();

        builder.HasIndex(t => t.Slug)
            .IsUnique();
    }
}

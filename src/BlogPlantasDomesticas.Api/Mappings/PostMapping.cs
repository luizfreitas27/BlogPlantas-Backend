using BlogPlantasDomesticas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlantasDomesticas.Api.Mappings;

public class PostMapping : IEntityTypeConfiguration<Post>
{
    public void Configure(EntityTypeBuilder<Post> builder)
    {
        builder.ToTable("posts");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Id)
            .HasColumnName("id");

        builder.Property(p => p.AuthorId)
            .HasColumnName("author_id")
            .IsRequired();

        builder.Property(p => p.CategoryId)
            .HasColumnName("category_id");

        builder.Property(p => p.Title)
            .HasColumnName("title")
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(p => p.Slug)
            .HasColumnName("slug")
            .HasMaxLength(250)
            .IsRequired();

        builder.Property(p => p.Excerpt)
            .HasColumnName("excerpt")
            .HasMaxLength(500);

        builder.Property(p => p.Content)
            .HasColumnName("content")
            .HasColumnType("text")
            .IsRequired();

        builder.Property(p => p.CoverImageUrl)
            .HasColumnName("cover_image_url")
            .HasMaxLength(500);

        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(p => p.ViewsCount)
            .HasColumnName("views_count")
            .HasDefaultValue(0);

        builder.Property(p => p.PublishedAt)
            .HasColumnName("published_at");

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("TIMESTAMPTZ")
            .IsRequired();

        builder.Property(p => p.UpdatedAt)
            .HasColumnType("TIMESTAMPTZ")
            .HasColumnName("updated_at");

        // Relationships
        builder.HasOne(p => p.Author)
            .WithMany(u => u.Posts)
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Posts)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        // Indexes
        builder.HasIndex(p => p.Slug)
            .IsUnique();

        builder.HasIndex(p => new { p.Status, p.PublishedAt });

        builder.HasIndex(p => p.AuthorId);
    }
}

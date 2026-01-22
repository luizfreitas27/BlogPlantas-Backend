using BlogPlantasDomesticas.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogPlantasDomesticas.Api.Mappings;

public class NewsletterSubscriberMapping : IEntityTypeConfiguration<NewsletterSubscriber>
{
    public void Configure(EntityTypeBuilder<NewsletterSubscriber> builder)
    {
        builder.ToTable("newsletter_subscribers");

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Id)
            .HasColumnName("id");

        builder.Property(n => n.Email)
            .HasColumnName("email")
            .HasMaxLength(150)
            .IsRequired();

        builder.Property(n => n.Name)
            .HasColumnName("name")
            .HasMaxLength(100);

        builder.Property(n => n.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);

        builder.Property(n => n.SubscribedAt)
            .HasColumnName("subscribed_at")
            .IsRequired();

        builder.Property(n => n.CreatedAt)
            .HasColumnName("created_at")
            .HasColumnType("TIMESTAMPTZ")
            .IsRequired();

        builder.Property(n => n.UpdatedAt)
            .HasColumnType("TIMESTAMPTZ")
            .HasColumnName("updated_at");

        // Indexes
        builder.HasIndex(n => n.Email)
            .IsUnique();
    }
}

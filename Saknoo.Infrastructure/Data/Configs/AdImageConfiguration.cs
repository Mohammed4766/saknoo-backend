using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saknoo.Domain.Entities;

namespace Saknoo.Infrastructure.Data.Configs;

public class AdImageConfiguration : IEntityTypeConfiguration<AdImage>
{
    public void Configure(EntityTypeBuilder<AdImage> builder)
    {
        builder.HasKey(ai => ai.Id);

        builder.Property(ai => ai.ImageUrl)
               .IsRequired();

        builder.HasOne(ai => ai.Ad)
               .WithMany(a => a.Images)
               .HasForeignKey(ai => ai.AdId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

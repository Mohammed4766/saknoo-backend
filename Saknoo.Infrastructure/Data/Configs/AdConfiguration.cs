using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saknoo.Domain.Entities;

namespace Saknoo.Infrastructure.Data.Configs;

public class AdConfiguration : IEntityTypeConfiguration<Ad>
{
       public void Configure(EntityTypeBuilder<Ad> builder)
       {
              builder.HasKey(a => a.Id);
              builder.Property(a => a.Title).IsRequired();
              builder.Property(a => a.CityId).IsRequired();

              builder.HasOne(a => a.City)
                     .WithMany()
                     .HasForeignKey(a => a.CityId);

              builder.HasMany(a => a.Images)
                     .WithOne()
                     .HasForeignKey(ai => ai.AdId);

              builder.HasMany(a => a.AdNeighborhoods)
                     .WithOne()
                     .HasForeignKey(an => an.AdId);

              builder.HasOne(a => a.User)
      .WithMany(u => u.Ads)
      .HasForeignKey(a => a.UserId)
      .OnDelete(DeleteBehavior.Restrict);

      

       }
}


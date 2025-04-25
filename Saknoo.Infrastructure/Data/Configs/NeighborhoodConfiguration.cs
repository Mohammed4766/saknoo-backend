using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saknoo.Domain.Entities;

namespace Saknoo.Infrastructure.Data.Configs;

public class NeighborhoodConfiguration : IEntityTypeConfiguration<Neighborhood>
{
    public void Configure(EntityTypeBuilder<Neighborhood> builder)
    {
        builder.HasKey(n => n.Id);
        builder.Property(n => n.Name).IsRequired();
        builder.HasOne(n => n.City)
               .WithMany()
               .HasForeignKey(n => n.CityId);

        builder.HasMany(n => n.AdNeighborhoods)
.WithOne(an => an.Neighborhood)
.HasForeignKey(an => an.NeighborhoodId);



    }
}

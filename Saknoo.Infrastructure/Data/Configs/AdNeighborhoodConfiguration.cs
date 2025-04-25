using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saknoo.Domain.Entities;

namespace Saknoo.Infrastructure.Data.Configs;

public class AdNeighborhoodConfiguration : IEntityTypeConfiguration<AdNeighborhood>
{
    public void Configure(EntityTypeBuilder<AdNeighborhood> builder)
    {
        builder.HasKey(an => new { an.AdId, an.NeighborhoodId });

        builder.HasOne(an => an.Ad)
            .WithMany(a => a.AdNeighborhoods)
            .HasForeignKey(an => an.AdId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(an => an.Neighborhood)
            .WithMany(n => n.AdNeighborhoods)
            .HasForeignKey(an => an.NeighborhoodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}



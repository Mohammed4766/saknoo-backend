using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saknoo.Domain.Entities;

namespace Saknoo.Infrastructure.Data.Configs;

    public class NationalityConfiguration : IEntityTypeConfiguration<Nationality>
    {
        public void Configure(EntityTypeBuilder<Nationality> builder)
        {
            builder.HasKey(n => n.Id);
            builder.Property(n => n.Name).IsRequired();
        }
    }

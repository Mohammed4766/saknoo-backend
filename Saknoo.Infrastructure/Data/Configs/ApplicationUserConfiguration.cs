using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saknoo.Domain.Entities;

namespace Saknoo.Infrastructure.Data.Configs;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(u => u.Id);
        builder.Property(u => u.UserName).IsRequired();
        builder.HasOne(u => u.Nationality)
               .WithMany()
               .HasForeignKey(u => u.NationalityId);

        builder.HasOne(u => u.Nationality)
.WithMany(n => n.Users)
.HasForeignKey(u => u.NationalityId)
.OnDelete(DeleteBehavior.Restrict);

        // Configure the relationship between ApplicationUser and MatchingAnswer.
        builder.HasMany(u => u.Answers)
               .WithOne(a => a.User) // Each answer belongs to one user.
               .HasForeignKey(a => a.UserId) // Foreign key on MatchingAnswer.
               .OnDelete(DeleteBehavior.Cascade); // Delete answers when the user is deleted.

    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saknoo.Domain.Entities;

namespace Saknoo.Infrastructure.Data.Configs
{
    public class MatchingOptionConfiguration : IEntityTypeConfiguration<MatchingOption>
    {
        public void Configure(EntityTypeBuilder<MatchingOption> builder)
        {
            // Define the primary key for the MatchingOption entity.
            builder.HasKey(o => o.Id);

            // Set the Text property as required and set a maximum length of 500 characters.
            builder.Property(o => o.Text)
                   .IsRequired() // This ensures that the option text cannot be null.
                   .HasMaxLength(500); // Limits the option text to 500 characters.

            // Configure the relationship between MatchingOption and MatchingQuestion.
            builder.HasOne(o => o.MatchingQuestion)
                   .WithMany(q => q.Options) // Each option belongs to one matching question.
                   .HasForeignKey(o => o.MatchingQuestionId) // Foreign key on MatchingOption.
                   .OnDelete(DeleteBehavior.Cascade); // Delete options when the question is deleted.
        }
    }
}

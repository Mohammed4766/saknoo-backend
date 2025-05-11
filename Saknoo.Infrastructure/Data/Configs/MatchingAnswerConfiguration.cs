using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saknoo.Domain.Entities;

namespace Saknoo.Infrastructure.Data.Configs
{
    public class MatchingAnswerConfiguration : IEntityTypeConfiguration<MatchingAnswer>
    {
        public void Configure(EntityTypeBuilder<MatchingAnswer> builder)
        {
            // Define the primary key for the MatchingAnswer entity.
            builder.HasKey(a => a.Id);

            // Set the Answer property as required and set a maximum length of 500 characters.
            builder.Property(a => a.Answer)
                   .IsRequired() // This ensures that the answer cannot be null.
                   .HasMaxLength(500); // Limits the answer to 500 characters.

            // Configure the relationship between MatchingAnswer and MatchingQuestion.
            builder.HasOne(a => a.MatchingQuestion)
                   .WithMany(q => q.Answers) // Each answer belongs to one matching question.
                   .HasForeignKey(a => a.MatchingQuestionId) // Foreign key on MatchingAnswer.
                   .OnDelete(DeleteBehavior.Cascade); // Delete answers when the question is deleted.

            // Configure the relationship between MatchingAnswer and ApplicationUser.
            builder.HasOne(a => a.User)
                   .WithMany() // Each answer is related to one user.
                   .HasForeignKey(a => a.UserId) // Foreign key on MatchingAnswer.
                   .OnDelete(DeleteBehavior.Cascade); // Delete answers when the user is deleted.
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Saknoo.Domain.Entities;

namespace Saknoo.Infrastructure.Data.Configs
{
    public class MatchingQuestionConfiguration : IEntityTypeConfiguration<MatchingQuestion>
    {
        public void Configure(EntityTypeBuilder<MatchingQuestion> builder)
        {
            // Define the primary key for the MatchingQuestion entity.
            builder.HasKey(q => q.Id);

            // Set the QuestionText property as required and set a maximum length of 500 characters.
            builder.Property(q => q.QuestionText)
                   .IsRequired() // This ensures that the question text cannot be null.
                   .HasMaxLength(500); // Limits the question text to 500 characters.

            // Configure the QuestionType property as required.
            builder.Property(q => q.Type)
                   .IsRequired(); // Ensures the question type must be defined.

            // Configure the relationship between MatchingQuestion and MatchingOption.
            builder.HasMany(q => q.Options)
                   .WithOne(o => o.MatchingQuestion) // Each option belongs to one matching question.
                   .HasForeignKey(o => o.MatchingQuestionId) // The foreign key on the MatchingOption side.
                   .OnDelete(DeleteBehavior.Cascade); // Deletes associated options when a question is deleted.

            // Configure the relationship between MatchingQuestion and MatchingAnswer.
            builder.HasMany(q => q.Answers)
                   .WithOne(a => a.MatchingQuestion) // Each answer belongs to one matching question.
                   .HasForeignKey(a => a.MatchingQuestionId) // The foreign key on the MatchingAnswer side.
                   .OnDelete(DeleteBehavior.Cascade); // Deletes associated answers when a question is deleted.
        }
    }
}

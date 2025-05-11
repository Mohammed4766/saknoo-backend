using Microsoft.EntityFrameworkCore;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Repositories;
using Saknoo.Infrastructure.Data;

namespace Saknoo.Infrastructure.Repositories;

public class MatchingRepository(ApplicationDbContext context) : IMatchingRepository
{
    public async Task<List<MatchingQuestion>> GetAllQuestionsAsync()
    {
        return await context.MatchingQuestions
            .Include(q => q.Options)
            .ToListAsync();
    }

    public async Task AddAnswersAsync(List<MatchingAnswer> answers)
    {
        await context.MatchingAnswers.AddRangeAsync(answers);
        await context.SaveChangesAsync();
    }

    public async Task<List<MatchingAnswer>> GetUserAnswersAsync(string userId)
    {
        return await context.MatchingAnswers
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    public async Task UpdateAnswersAsync(List<MatchingAnswer> answers)
    {
        context.MatchingAnswers.UpdateRange(answers);
        await context.SaveChangesAsync();
    }

    public async Task<List<MatchingAnswer>> GetAnswersByUsersAsync(string userId1, string userId2)
    {
        return await context.MatchingAnswers
            .Where(a => a.UserId == userId1 || a.UserId == userId2)
            .ToListAsync();
    }

}

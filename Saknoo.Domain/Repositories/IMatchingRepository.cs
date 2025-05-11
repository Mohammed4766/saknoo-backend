using System;
using Saknoo.Domain.Entities;

namespace Saknoo.Domain.Repositories;

public interface IMatchingRepository
{
    Task<List<MatchingQuestion>> GetAllQuestionsAsync();
    Task AddAnswersAsync(List<MatchingAnswer> answers);
    Task<List<MatchingAnswer>> GetUserAnswersAsync(string userId);

    Task UpdateAnswersAsync(List<MatchingAnswer> answers);

    Task<List<MatchingAnswer>> GetAnswersByUsersAsync(string userId1, string userId2);


}

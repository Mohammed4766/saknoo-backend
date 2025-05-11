using MediatR;
using Saknoo.Application.User.Dtos;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.User.Queries.GetAllMatchingQuestions;

public class GetAllMatchingQuestionsQueryHandler(
    IMatchingRepository matchingRepository
) : IRequestHandler<GetAllMatchingQuestionsQuery, List<MatchingQuestionDto>>
{
    public async Task<List<MatchingQuestionDto>> Handle(GetAllMatchingQuestionsQuery request, CancellationToken cancellationToken)
    {
        var questions = await matchingRepository.GetAllQuestionsAsync();

        return questions.Select(q => new MatchingQuestionDto
        {
            Id = q.Id,
            QuestionText = q.QuestionText,
            Type = q.Type.ToString(),
            Options = q.Options?.Select(o => o.Text).ToList()
        }).ToList();
    }
}

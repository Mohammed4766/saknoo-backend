using MediatR;
using Saknoo.Domain.Repositories;

namespace Saknoo.Application.User.Queries.GetMatchingPercentage;

public class GetMatchingPercentageQueryHandler(
    IMatchingRepository matchingRepository
) : IRequestHandler<GetMatchingPercentageQuery, double>
{
    public async Task<double> Handle(GetMatchingPercentageQuery request, CancellationToken cancellationToken)
    {
        var answers = await matchingRepository.GetAnswersByUsersAsync(request.UserId1, request.UserId2);

        var grouped = answers
            .GroupBy(a => a.MatchingQuestionId)
            .Where(g => g.Count() == 2)
            .ToList();

        if (!grouped.Any())
            return 0;

        int matchingAnswers = grouped.Count(g =>
        {
            var answerList = g.ToList();
            return answerList[0].Answer == answerList[1].Answer;
        });

        double percentage = (double)matchingAnswers / grouped.Count * 100;
        return Math.Round(percentage, 2);
    }
}

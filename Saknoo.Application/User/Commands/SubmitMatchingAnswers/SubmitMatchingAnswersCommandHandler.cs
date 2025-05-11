using MediatR;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Repositories;
using Saknoo.Application.User;

namespace Saknoo.Application.User.Commands.SubmitMatchingAnswers;

public class SubmitMatchingAnswersCommandHandler(
    IMatchingRepository matchingRepository,
    IUserContext userContext
) : IRequestHandler<SubmitMatchingAnswersCommand, bool>
{
    public async Task<bool> Handle(SubmitMatchingAnswersCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        if (currentUser == null)
        {
            throw new UnauthorizedAccessException("You are not authorized.");
        }

        var existingAnswers = await matchingRepository.GetUserAnswersAsync(currentUser.UserId);
        var answerDict = existingAnswers.ToDictionary(a => a.MatchingQuestionId);

        var allQuestions = await matchingRepository.GetAllQuestionsAsync();
        var validQuestionIds = allQuestions.Select(q => q.Id).ToHashSet();

        var toUpdate = new List<MatchingAnswer>();
        var toInsert = new List<MatchingAnswer>();

        foreach (var answer in request.Answers)
        {
            if (!validQuestionIds.Contains(answer.MatchingQuestionId))
                continue;

            if (answerDict.TryGetValue(answer.MatchingQuestionId, out var existingAnswer))
            {
                existingAnswer.Answer = answer.Answer;
                toUpdate.Add(existingAnswer);
            }
            else
            {
                toInsert.Add(new MatchingAnswer
                {
                    UserId = currentUser.UserId,
                    MatchingQuestionId = answer.MatchingQuestionId,
                    Answer = answer.Answer
                });
            }
        }

        if (toUpdate.Any())
            await matchingRepository.UpdateAnswersAsync(toUpdate);

        if (toInsert.Any())
            await matchingRepository.AddAnswersAsync(toInsert);

        return true;
    }
}

using MediatR;
using Microsoft.Extensions.Logging;
using Saknoo.Domain.Entities;
using Saknoo.Domain.Repositories;


namespace Saknoo.Application.User.Commands.SubmitMatchingAnswers;

public class SubmitMatchingAnswersCommandHandler(
    IMatchingRepository matchingRepository,
    IUserContext userContext,
    ILogger<SubmitMatchingAnswersCommandHandler> logger
) : IRequestHandler<SubmitMatchingAnswersCommand, bool>
{
    public async Task<bool> Handle(SubmitMatchingAnswersCommand request, CancellationToken cancellationToken)
    {
        var currentUser = userContext.GetCurrentUser();
        if (currentUser == null)
        {
            logger.LogWarning("Unauthorized access attempt to submit matching answers.");
            throw new UnauthorizedAccessException("You are not authorized.");
        }

        logger.LogInformation("User {UserId} is submitting matching answers.", currentUser.UserId);

        var existingAnswers = await matchingRepository.GetUserAnswersAsync(currentUser.UserId);
        var answerDict = existingAnswers.ToDictionary(a => a.MatchingQuestionId);

        var allQuestions = await matchingRepository.GetAllQuestionsAsync();
        var validQuestionIds = allQuestions.Select(q => q.Id).ToHashSet();

        var toUpdate = new List<MatchingAnswer>();
        var toInsert = new List<MatchingAnswer>();

        foreach (var answer in request.Answers)
        {
            if (!validQuestionIds.Contains(answer.MatchingQuestionId))
            {
                logger.LogWarning("User {UserId} submitted answer for invalid question id {QuestionId}", currentUser.UserId, answer.MatchingQuestionId);
                continue;
            }

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
        {
            logger.LogInformation("Updating {Count} matching answers for user {UserId}", toUpdate.Count, currentUser.UserId);
            await matchingRepository.UpdateAnswersAsync(toUpdate);
        }

        if (toInsert.Any())
        {
            logger.LogInformation("Inserting {Count} new matching answers for user {UserId}", toInsert.Count, currentUser.UserId);
            await matchingRepository.AddAnswersAsync(toInsert);
        }

        logger.LogInformation("Finished processing matching answers for user {UserId}", currentUser.UserId);
        return true;
    }
}

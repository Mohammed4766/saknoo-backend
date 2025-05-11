using MediatR;
using Saknoo.Application.User.Dtos;

namespace Saknoo.Application.User.Commands.SubmitMatchingAnswers;

public class SubmitMatchingAnswersCommand : IRequest<bool>
{

    public List<MatchingAnswerDto> Answers { get; set; } = new();
}

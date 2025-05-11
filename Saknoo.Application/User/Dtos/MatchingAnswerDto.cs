using System;

namespace Saknoo.Application.User.Dtos;

public class MatchingAnswerDto
{
    public int MatchingQuestionId { get; set; }
    public string Answer { get; set; } = null!;
}

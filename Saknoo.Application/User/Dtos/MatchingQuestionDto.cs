using System;

namespace Saknoo.Application.User.Dtos;

public class MatchingQuestionDto
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = null!;
    public string Type { get; set; } = null!;
    public List<string>? Options { get; set; }
}

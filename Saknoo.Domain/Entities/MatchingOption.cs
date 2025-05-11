using System;

namespace Saknoo.Domain.Entities;

public class MatchingOption
{
    public int Id { get; set; }
    public string Text { get; set; } = null!;
    public int MatchingQuestionId { get; set; }
    public MatchingQuestion MatchingQuestion { get; set; } = null!;
}


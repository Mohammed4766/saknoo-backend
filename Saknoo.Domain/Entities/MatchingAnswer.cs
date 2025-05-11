using System;

namespace Saknoo.Domain.Entities;

public class MatchingAnswer
{
    public int Id { get; set; }
    public string UserId { get; set; } = null!;
    public int MatchingQuestionId { get; set; }
    public string Answer { get; set; } = null!;
    
    // Navigation properties
    public MatchingQuestion MatchingQuestion { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
}


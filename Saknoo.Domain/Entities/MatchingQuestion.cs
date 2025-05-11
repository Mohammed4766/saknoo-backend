using System;

namespace Saknoo.Domain.Entities;

public class MatchingQuestion
{
    public int Id { get; set; }
    public string QuestionText { get; set; } = null!;
    public QuestionType Type { get; set; } 
  
    // Adding the navigation property for MatchingAnswer.
    public List<MatchingAnswer>? Answers { get; set; } // Each question can have multiple answers.
    
    public List<MatchingOption>? Options { get; set; } // Each question can have multiple options.
}

public enum QuestionType
{
    Text,
    Number,
    Choice,
    MultiChoice
}


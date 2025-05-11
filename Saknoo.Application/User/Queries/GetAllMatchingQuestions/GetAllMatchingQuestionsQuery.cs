using System;
using MediatR;
using Saknoo.Application.User.Dtos;

namespace Saknoo.Application.User.Queries.GetAllMatchingQuestions;

public class GetAllMatchingQuestionsQuery : IRequest<List<MatchingQuestionDto>> { }

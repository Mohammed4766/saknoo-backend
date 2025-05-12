using System;
using MediatR;

namespace Saknoo.Application.User.Queries.GetMatchingPercentage;

public class GetMatchingPercentageQuery : IRequest<double>
{
    public string UserId1 { get; set; } = null!;
    public string UserId2 { get; set; } = null!;
}
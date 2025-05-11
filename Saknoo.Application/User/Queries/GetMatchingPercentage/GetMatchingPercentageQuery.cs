using System;
using MediatR;

namespace Saknoo.Application.User.Queries.GetMatchingPercentage;

public class GetMatchingPercentageQuery : IRequest<double>
{
    public string UserId1 { get; set; }
    public string UserId2 { get; set; }
}
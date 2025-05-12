using FluentValidation;


namespace Saknoo.Application.Ads.Queries.GetAllAdsQuery;

public class GetAllAdsQueryValidator : AbstractValidator<GetAllAdsQuery>
{
    private readonly int[] validPageSizes = [5, 10, 15, 20, 30];
    private readonly string[] validSortBy = ["Price", "CreatedAt", "PriceFrom"];
    public GetAllAdsQueryValidator()
    {
        RuleFor(x => x.PageNumber)
        .GreaterThanOrEqualTo(1)
        .WithMessage("Page number must be greater than 0.");

        RuleFor(x => x.PageSize)
        .Must(value => validPageSizes.Contains(value))
        .WithMessage($"Page Size must be in [{string.Join(',', validPageSizes)}].");

        RuleFor(x => x.SortBy)
       .Must(value => validSortBy.Contains(value))
       .When(x => x.SortBy is not null)
       .WithMessage($"Sort By is optional , or must be in [{string.Join(',', validSortBy)}].");


        RuleFor(x => x.CityId)
     .GreaterThan(0)
     .When(x => x.CityId.HasValue)
     .WithMessage("CityId must be greater than 0.");

        RuleFor(x => x.NeighborhoodIds)
            .Must(ids => ids == null || !ids.Any() || ids.All(id => id > 0))
            .WithMessage("All Neighborhood IDs must be greater than 0.");

    }
}

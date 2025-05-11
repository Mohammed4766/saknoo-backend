using FluentValidation;

namespace Saknoo.Application.Ads.Commands.CreateAdCommand;

public class CreateAdCommandValidator : AbstractValidator<CreateAdCommand>
{
    public CreateAdCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.");

        RuleFor(x => x.CityId)
            .GreaterThan(0).WithMessage("City must be selected.");

        RuleFor(x => x.HasApartment)
            .NotNull().WithMessage("You must specify whether you have an apartment.");

        RuleFor(x => x.Images)
            .Must(images => images.Count <= 4)
            .WithMessage("You can upload up to 4 images only.");

        // Rules for users who HAVE an apartment
        When(x => x.HasApartment, () =>
        {
            RuleFor(x => x.Price)
                .NotNull().WithMessage("Price is required when you have an apartment.");

            RuleFor(x => x.NeighborhoodIds)
                .Must(list => list != null && list.Count == 1)
                .WithMessage("You must select exactly one neighborhood when you have an apartment.");

            RuleFor(x => x.Images)
                .Must(images => images != null && images.Count >= 1)
                .WithMessage("You must upload at least one image when you have an apartment.");
        });

        // Rules for users who DON'T HAVE an apartment
        When(x => !x.HasApartment, () =>
        {
            RuleFor(x => x.Price)
                .Must(price => price == null || price == 0)
                .WithMessage("You should not set a fixed price when you don't have an apartment.");

            RuleFor(x => x.PriceFrom)
                .NotNull().WithMessage("Minimum price is required when you don't have an apartment.");

            RuleFor(x => x.PriceTo)
                .NotNull().WithMessage("Maximum price is required when you don't have an apartment.");

            RuleFor(x => x.NeighborhoodIds)
                .Must(list => list != null && list.Any())
                .WithMessage("You must select at least one neighborhood when you don't have an apartment.");

            RuleFor(x => x.Images)
                .Must(images => images == null || images.Count == 0)
                .WithMessage("You must not upload any images when you don't have an apartment.");
        });
    }
}


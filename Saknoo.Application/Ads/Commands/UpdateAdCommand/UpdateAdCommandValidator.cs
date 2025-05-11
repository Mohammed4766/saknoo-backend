using FluentValidation;
using Saknoo.Application.Ads.Dtos;


namespace Saknoo.Application.Ads.Commands.UpdateAdCommand
{
    public class UpdateAdDtoValidator : AbstractValidator<UpdateAdDto>
    {
        public UpdateAdDtoValidator(bool hasApartment)
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .When(x => x.Title != null)
                .WithMessage("Title cannot be empty.");

            RuleFor(x => x.Description)
                .NotEmpty()
                .When(x => x.Description != null)
                .WithMessage("Description cannot be empty.");

            RuleFor(x => x.CityId)
                .GreaterThan(0)
                .When(x => x.CityId != null)
                .WithMessage("City must be selected.");

            if (hasApartment)
            {
                RuleFor(x => x.PriceFrom)
                    .Null()
                    .WithMessage("You cannot set a price range when you have an apartment.");

                RuleFor(x => x.PriceTo)
                    .Null()
                    .WithMessage("You cannot set a price range when you have an apartment.");

                RuleFor(x => x.Price)
                    .NotNull()
                    .When(x => x.Price != null)
                    .WithMessage("Fixed price must be specified when you have an apartment.");
            }
            else
            {
                RuleFor(x => x.Price)
                    .Null()
                    .WithMessage("You cannot set a fixed price when you don't have an apartment.");

                RuleFor(x => x.PriceFrom)
                    .NotNull()
                    .WithMessage("Minimum price must be specified.");

                RuleFor(x => x.PriceTo)
                    .NotNull()
                    .WithMessage("Maximum price must be specified.");
            }
        }
    }
}

using FluentValidation;
using HotpotWebApplication.DTOs.Restaurant;

namespace HotpotWebApplication.Validators
{
    public class CreateRestaurantValidator:AbstractValidator<CreateRestaurantDto>
    {
        public CreateRestaurantValidator()
        {
            RuleFor(x => x.RestaurantName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Location)
                .NotEmpty();

            RuleFor(x => x.ContactNumber)
                .NotEmpty();

            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Description)
                .NotEmpty();
        }
    }
}

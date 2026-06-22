using FluentValidation;

using HotpotWebApplication.DTOs.MenuItem;

namespace HotpotWebApplication.Validators
{
    public class CreateMenuItemValidator:AbstractValidator<CreateMenuDto>
    {
        public CreateMenuItemValidator()
        {
            RuleFor(x => x.ItemName)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0);

            RuleFor(x => x.CookingTime)
                .GreaterThan(0);

            RuleFor(x => x.Calories)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Protein)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Carbs)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.Fat)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.RestaurantId)
                .GreaterThan(0);

            RuleFor(x => x.CategoryId)
                .GreaterThan(0);
        }

    }
}

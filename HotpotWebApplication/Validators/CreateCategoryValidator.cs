using FluentValidation;
using HotpotWebApplication.DTOs.Category;

namespace HotpotWebApplication.Validators
{
    public class CreateCategoryValidator:AbstractValidator<CreateCategoryDto>
    {
        public CreateCategoryValidator()
        {
            RuleFor(x => x.CategoryName)
                .NotEmpty()
                .WithMessage("Category Name is required")
                .MaximumLength(50);

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MaximumLength(200);
        }
    }
}

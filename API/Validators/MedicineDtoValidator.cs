using API.Dtos;
using FluentValidation;

namespace API.Validators
{
    public class MedicineDtoValidator : AbstractValidator<MedicineDto>
    {
        public MedicineDtoValidator()
        {
            RuleFor(m => m.Name).NotEmpty().WithMessage("Name is required.")
                                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(m => m.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
        }
    }
}

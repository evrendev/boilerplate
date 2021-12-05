using FluentValidation;
using EvrenDev.Application.DTOS.Settings.Department;

namespace EvrenDev.Api.Validators.Settings.Department
{
    public class CreateDepartmentValidator : AbstractValidator<CreateDepartmentCommand>
    {
        public CreateDepartmentValidator()
        {
            RuleFor(d => d.Title)
                .NotEmpty().WithMessage("Başlık alanını girmeniz gerekiyor.")
                .NotNull()
                .MaximumLength(255).WithMessage("Başlık alanı en faz 255 karakter olabilir.");
        }
    }
}
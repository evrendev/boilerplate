using FluentValidation;
using EvrenDev.Application.DTOS.Settings.Department;

namespace EvrenDev.Api.Validators.Settings.Department
{
    public class UpdateDepartmentValidator : AbstractValidator<UpdateDepartmentCommand>
    {
        public UpdateDepartmentValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage("Id alanı boş bırakılamaz.")
                .NotNull();

            RuleFor(d => d.Title)
                .NotEmpty().WithMessage("Başlık alanını girmeniz gerekiyor.")
                .NotNull()
                .MaximumLength(255).WithMessage("Başlık alanı en faz 255 karakter olabilir.");
        }
    }
}
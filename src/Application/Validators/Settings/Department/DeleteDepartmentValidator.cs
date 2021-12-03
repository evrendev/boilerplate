using FluentValidation;
using EvrenDev.Application.DTOS.Settings.Department;

namespace EvrenDev.Api.Validators.Settings.Department
{
    public class DeleteDepartmentValidator : AbstractValidator<DeleteDepartmentCommand>
    {
        public DeleteDepartmentValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage("Id alanı boş bırakılamaz.")
                .NotNull();
        }
    }
}
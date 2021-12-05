using FluentValidation;
using EvrenDev.Application.DTOS.User;

namespace EvrenDev.Api.Validators.Settings.Department
{
    public class DeleteApplicationUserValidator : AbstractValidator<DeleteApplicationUserCommand>
    {
        public DeleteApplicationUserValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage("Id alanı boş bırakılamaz.")
                .NotNull();
        }
    }
}
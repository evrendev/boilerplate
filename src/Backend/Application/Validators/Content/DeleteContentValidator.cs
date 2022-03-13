using FluentValidation;
using EvrenDev.Application.DTOS.Content;

namespace EvrenDev.Application.Validators.Content
{
    public class DeleteContentValidator : AbstractValidator<DeleteContentCommand>
    {
        public DeleteContentValidator()
        {
            RuleFor(d => d.Id)
                .NotEmpty().WithMessage("Id alanı boş bırakılamaz.")
                .NotNull();
        }
    }
}
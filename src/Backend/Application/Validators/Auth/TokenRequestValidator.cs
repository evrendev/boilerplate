using EvrenDev.Application.DTOS.Auth;
using FluentValidation;
using System;

namespace EvrenDev.Application.Validators.Token
{
    public class TokenRequestValidator : AbstractValidator<TokenRequest>
    {
        public TokenRequestValidator()
        {
            RuleFor(tr => tr.Email)
                .NotNull()
                .NotEmpty().WithMessage("Eposta adresini girmeniz gerekiyor.")
                .EmailAddress().WithMessage("Geçerli bir eposta adresi girmeniz gerekiyor.");

            RuleFor(d => d.Password)
                .NotEmpty().WithMessage("Parola alanını girmeniz gerekiyor.")
                .NotNull()
                .MinimumLength(8).WithMessage("Parola alanı en az 8 karakter olmalıdır.");
        }
    }
}
using FluentValidation;
using EvrenDev.Application.DTOS.User;

namespace EvrenDev.Api.Validators.Settings.Department
{
    public class UpdateApplicationUserValidator : AbstractValidator<UpdateApplicationUserCommand>
    {
        public UpdateApplicationUserValidator()
        {
            RuleFor(d => d.Id)
                .NotNull()
                .NotEmpty().WithMessage("Id alanı boş bırakılamaz.");

            RuleFor(au => au.DepartmentId)
                .NotNull()
                .NotEmpty().WithMessage("Departman seçimi boş bırakılamaz.");

            RuleFor(au => au.Email)
                .NotNull()
                .NotEmpty().WithMessage("Eposta adresini girmeniz gerekiyor.")
                .EmailAddress().WithMessage("Geçerli bir eposta adresi girmeniz gerekiyor.");

            RuleFor(au => au.FirstName)
                .NotNull()
                .NotEmpty().WithMessage("Adı alanı boş bırakılamaz.")
                .MaximumLength(24).WithMessage("Kullanıcı adı en fazla 24 karakter olabilir.");

            RuleFor(au => au.LastName)
                .NotNull()
                .NotEmpty().WithMessage("Soyadı alanı boş bırakılamaz.")
                .MaximumLength(24).WithMessage("Kullanıcı soyadı en fazla 24 karakter olabilir.");

            RuleFor(au => au.Roles)
                .NotNull()
                .Must(au => au.Count >= 1)
                .WithMessage("Kullanıcıya en az bir adet rol atamanız gerekmektedir.");
        }
    }
}
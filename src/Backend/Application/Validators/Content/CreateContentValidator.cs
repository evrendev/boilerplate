using FluentValidation;
using EvrenDev.Application.DTOS.Content;
using System;
using EvrenDev.Application.Enums.Language;

namespace EvrenDev.Api.Validators.Settings.Content
{
    public class CreateContentValidator : AbstractValidator<CreateContentCommand>
    {
        public CreateContentValidator()
        {
            RuleFor(d => d.LanguageId)
                .NotEmpty().WithMessage("Dil boş bırakılamaz.")
                .NotNull()
                .Must(language => language == Languages.Turkish.Id || language == Languages.English.Id).WithMessage("Dil Türkçe veya İngilizce seçilmelidir.");

            RuleFor(d => d.Title)
                .NotEmpty().WithMessage("Başlık alanını girmeniz gerekiyor.")
                .NotNull()
                .MaximumLength(128).WithMessage("Başlık alanı en faz 128 karakter olabilir.");

            RuleFor(d => d.Overview)
                .NotEmpty().WithMessage("Önizleme alanını girmeniz gerekiyor.")
                .NotNull()
                .MaximumLength(255).WithMessage("Önizleme alanı en faz 255 karakter olabilir.");

            RuleFor(d => d.Body)
                .NotEmpty().WithMessage("Önizleme alanını girmeniz gerekiyor.")
                .NotNull();

            RuleFor(d => d.Image)
                .NotEmpty().WithMessage("Görsel boş bırakılamaz.")
                .NotNull()
                .MaximumLength(24).WithMessage("Görsel adı geçerli gözükmüyor.");

            RuleFor(d => d.MenuPosition)
                .NotNull().WithMessage("En az bir tane menü konumu seçmeniz gerekmektedir..");

            RuleFor(d => d.PublishDate)
                .NotEmpty().WithMessage("Yayın tarihini girmeniz gerekiyor.")
                .NotNull()
                .Must(BeAValidDate).WithMessage("Yayın tarihi geçerli bir tarih değeri olmalıdır.");
        }

        private bool BeAValidDate(DateTime date)
        {
            return !date.Equals(default(DateTime));
        }
    }
}
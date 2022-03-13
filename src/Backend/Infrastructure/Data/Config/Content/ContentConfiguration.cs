using System;
using System.Linq;
using EvrenDev.Application.Enums.Language;
using EvrenDev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EvrenDev.Infrastructure.Data.Config
{
    public class ContentConfiguration : IEntityTypeConfiguration<Content>
    {
        public void Configure(EntityTypeBuilder<Content> entity)
        {
            var strConverter = new ValueConverter<string[], string>(
                v => string.Join(";",v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).ToArray()); 

            var intConverter = new ValueConverter<int[], string>(
                v => string.Join(";",v),
                v => v.Split(";", StringSplitOptions.RemoveEmptyEntries).Select(val=> int.Parse(val)).ToArray()); 

            entity.Property(e => e.PublishDate)
                .IsRequired(true);

            entity.Property(e => e.Title)
                .IsRequired(true)
                .HasMaxLength(128);

            entity.Property(e => e.Overview)
                .IsRequired(true)
                .HasMaxLength(255);

            entity.Property(e => e.Body)
                .IsRequired(true);

            entity.Property(e => e.Image)
                .IsRequired(true)
                .HasMaxLength(24);

            entity.Property(e => e.MenuPosition)
                .HasConversion(intConverter);

            entity.Property(e => e.LanguageId)
                .IsRequired(true)
                .HasMaxLength(1)
                .HasDefaultValue(Languages.Turkish.Id);

            entity.Property(e => e.MetaTitle)
                .IsRequired(false)
                .HasMaxLength(60)
                .IsUnicode(false);

            entity.Property(e => e.MetaDescription)
                .IsRequired(false)
                .HasMaxLength(160)
                .IsUnicode(false);

            entity.Property(e => e.MetaKeywords)
                .HasConversion(strConverter);

            entity.ToTable("Contents");
        }
    }
}

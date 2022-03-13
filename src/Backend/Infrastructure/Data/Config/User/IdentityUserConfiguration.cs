using EvrenDev.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvrenDev.Infrastructure.Data.Config
{
    public class IdentityUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> entity)
        {
            entity.Property(e => e.FirstName)
                .IsRequired(true)
                .HasMaxLength(24);

            entity.Property(e => e.LastName)
                .IsRequired(true)
                .HasMaxLength(24);

            entity.Property(e => e.Deleted)
                .IsRequired(true)
                .HasDefaultValue(false);

            entity.ToTable("Users");
        }
    }
}

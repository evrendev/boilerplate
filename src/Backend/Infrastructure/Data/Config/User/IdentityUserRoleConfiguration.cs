using EvrenDev.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvrenDev.Infrastructure.Data.Config
{
    public class IdentityUserRoleConfiguration : IEntityTypeConfiguration<ApplicationUserRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationUserRole> entity)
        {
            entity.HasKey(ur => new { ur.UserId, ur.RoleId });

            entity
                .HasOne(e => e.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(e => e.RoleId);
            entity
                .HasOne(e => e.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(e => e.UserId);

            entity.ToTable("UserRoles");
        }
    }
}

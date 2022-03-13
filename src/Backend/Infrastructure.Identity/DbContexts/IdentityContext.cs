using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Identity;
using EvrenDev.Infrastructure.Identity.Model;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;

namespace EvrenDev.Infrastructure.Identity
{
    public class IdentityContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>,
        ApplicationUserRole, IdentityUserLogin<Guid>,
        IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("Identity"); 
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<ApplicationUser>(entity =>
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

                entity.ToTable(name: "Users");
            });

            modelBuilder.Entity<ApplicationRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });
            modelBuilder.Entity<ApplicationUserRole>(entity =>
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
            });

            modelBuilder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable("UserLogins");     
            });

            modelBuilder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable("RoleClaims");

            });

            modelBuilder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable("UserTokens");
            });
        }
    }
}
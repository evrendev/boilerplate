using EvrenDev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace EvrenDev.Infrastructure.Data.Config
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> entity)
        {
            entity.Property(e => e.Title)
                .IsRequired(true)
                .HasMaxLength(255);

            entity.HasMany(e => e.Users)
                .WithOne(u => u.Department)
                .HasForeignKey(e => e.DepartmentId);

            entity.ToTable("Departments");
        }
    }
}

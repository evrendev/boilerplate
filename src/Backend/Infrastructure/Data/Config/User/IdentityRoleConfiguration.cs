using EvrenDev.Infrastructure.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EvrenDev.Infrastructure.Data.Config
{
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<ApplicationRole>
    {
        public void Configure(EntityTypeBuilder<ApplicationRole> entity)
        {
            entity.ToTable("Roles");
        }
    }
}

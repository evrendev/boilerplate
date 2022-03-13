using EvrenDev.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using EvrenDev.Domain.Shared;
using EvrenDev.Application.Interfaces.Shared;
using Microsoft.AspNetCore.Http;

namespace EvrenDev.Infrastructure.Persistence
{
    public class ApplicationContext : DbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationContext(DbContextOptions<ApplicationContext> options,
            IDateTimeService dateTime,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _dateTime = dateTime;
            _httpContextAccessor = httpContextAccessor;
        }

        public DbSet<Content> Contents { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            var user = _httpContextAccessor.HttpContext?.User == null ? null : _httpContextAccessor.HttpContext?.User?.Identity.Name;

            foreach (var entry in ChangeTracker.Entries<BaseEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreationDateTime = _dateTime.NowUtc;
                        entry.Entity.Creator = user;

                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModificationTime = _dateTime.NowUtc;
                        entry.Entity.LastModifier = user;
                        entry.Entity.DeletionTime = entry.Entity.Deleted ? _dateTime.NowUtc : (DateTime?)null;
                        entry.Entity.Deleter = entry.Entity.Deleted ? user : null;

                        break;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}